import 'dart:convert';

import 'package:demo5/address/address.dart';
import 'package:demo5/contact/addContact.dart';
import 'package:demo5/navbar.dart';
import 'package:demo5/network/networkHandler.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

class Contacts extends StatefulWidget {
  final int categoryIndex;

  const Contacts({Key? key, required this.categoryIndex}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _ContactsState();
}

class _ContactsState extends State<Contacts> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        appBar: AppBar(
          title: const Text('Kontakte',
              style: TextStyle(
                  height: 1.00, fontSize: 25.00, color: Colors.white)),
          centerTitle: true,
        ),
        drawer: NavBar(),
        body: FutureBuilder<List<Contact>>(
            future: getContacts(),
            builder: (context, AsyncSnapshot snapshot) {
              if (snapshot.hasData &&
                  snapshot.connectionState == ConnectionState.done) {
                return ListView.builder(
                  itemCount: snapshot.data!.length,
                  itemBuilder: (context, index) {
                    AlertDialog alert;
                    return Card(
                      child: ListTile(
                        leading: const CircleAvatar(
                          backgroundColor: Colors.black,
                          backgroundImage: AssetImage("lib/assets/avatar.png"),
                        ),
                        title: Text(
                          "${snapshot.data?[index].firstName} ${snapshot.data?[index].lastName}",
                        ),
                        subtitle: Text(
                          snapshot.data?[index].email,
                        ),
                      ),
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(10.0)),
                    );
                  },
                );
              } else {
                return Center(child: CircularProgressIndicator());
              }
            }),
        floatingActionButton: FloatingActionButton(
          onPressed: () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => const AddContact()),
            );
          },
          backgroundColor: Colors.purple,
          child: const Icon(Icons.add),
        ),
      ),
    );
  }

  Future<List<Contact>> getContacts() async {
    String url = "https://backend.invoicer.at/api/Contacts";
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    List<Contact> contacts = [];
    List<String> typeOfContacts = [
      "Supplier",
      "Client",
      "Partner",
      "ProspectiveClient",
      "NoTargetGroup"
    ];

    if (token!.isNotEmpty) {
      token = token.toString();

      final response = await http.get(uri, headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        "Authorization": "Bearer $token"
      });
      print(response.statusCode);

      List data = await json.decode(response.body) as List;

      for (var element in data) {
        Map obj = element;
        String typeOfContact = obj["typeOfContactEnum"];
        String gender = obj["gender"];
        String? title = obj["title"];
        String firstName = obj["firstName"];
        String lastName = obj["lastName"];
        String nameOfOrganisation = obj["nameOfOrganisation"];
        String phoneNumber = obj["phoneNumber"];
        String email = obj["email"];
        Address address = Address(
            obj["address"]["street"],
            obj["address"]["zipCode"],
            obj["address"]["city"],
            obj["address"]["country"]);
        Contact contact = Contact(typeOfContact, gender, title, firstName,
            lastName, nameOfOrganisation, phoneNumber, email, address);
        if (typeOfContacts[widget.categoryIndex] == typeOfContact) {
          contacts.add(contact);
        }
      }
    }
    return contacts;
  }
}

class Contact {
  final String typeOfContact;
  final String gender;
  final String? title;
  final String firstName;
  final String lastName;
  final String nameOfOrganisation;
  final String phoneNumber;
  final String email;
  final Address address;

  Contact(
      this.typeOfContact,
      this.gender,
      this.title,
      this.firstName,
      this.lastName,
      this.nameOfOrganisation,
      this.phoneNumber,
      this.email,
      this.address);
}
