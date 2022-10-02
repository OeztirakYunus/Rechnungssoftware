import 'dart:convert';

import 'package:demo5/address/address.dart';
import 'package:demo5/contact/addContact.dart';
import 'package:demo5/contact/editContact.dart';
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
  Widget build(BuildContext context) {
    setState(() {});
    return SafeArea(
      child: Scaffold(
        appBar: AppBar(
          title: const Text('Kontakte',
              style: TextStyle(
                  height: 1.00, fontSize: 25.00, color: Colors.white)),
          centerTitle: true,
        ),
        drawer: const NavBar(),
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
                        trailing:
                            Row(mainAxisSize: MainAxisSize.min, children: [
                          OutlinedButton(
                            onPressed: () => {
                              alert = AlertDialog(
                                title: const Text("Achtung!"),
                                content: const Text(
                                    "Möchten Sie wirklich diesen Kontakt löschen?"),
                                actions: [
                                  TextButton(
                                    child: const Text("Löschen"),
                                    onPressed: () async {
                                      await deleteContact(
                                          snapshot.data?[index].contactId);
                                      Navigator.of(context).pop();
                                      setState(() {});
                                    },
                                  ),
                                  TextButton(
                                    child: const Text("Abbrechen"),
                                    onPressed: () {
                                      Navigator.of(context).pop();
                                    },
                                  )
                                ],
                              ),
                              showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return alert;
                                },
                              )
                            },
                            child: const Icon(Icons.delete),
                          ),
                          OutlinedButton(
                            onPressed: () => {
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                    builder: (context) => EditContact(
                                        contactId:
                                            snapshot.data?[index].contactId,
                                        typeOfContact:
                                            snapshot.data?[index].typeOfContact,
                                        gender: snapshot.data?[index].gender,
                                        title: snapshot.data?[index].title,
                                        firstName:
                                            snapshot.data?[index].firstName,
                                        lastName:
                                            snapshot.data?[index].lastName,
                                        nameOfOrganisation: snapshot
                                            .data?[index].nameOfOrganisation,
                                        phoneNumber:
                                            snapshot.data?[index].phoneNumber,
                                        email: snapshot.data?[index].email,
                                        address: Address(
                                            snapshot
                                                .data?[index].address.street,
                                            snapshot
                                                .data?[index].address.zipCode,
                                            snapshot.data?[index].address.city,
                                            snapshot.data?[index].address
                                                .country))),
                              )
                            },
                            child: const Icon(Icons.edit),
                          ),
                        ]),
                      ),
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(10.0)),
                    );
                  },
                );
              } else {
                return const Center(child: CircularProgressIndicator());
              }
            }),
        floatingActionButton: FloatingActionButton(
          onPressed: () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => AddContact()),
            );
          },
          backgroundColor: Colors.redAccent[700],
          child: const Icon(Icons.add),
        ),
      ),
    );
  }

  Future<List<Contact>> getContacts() async {
    List<String> typeOfContacts = [
      "Supplier",
      "Client",
      "Partner",
      "ProspectiveClient",
      "NoTargetGroup"
    ];
    List<Contact> categoryContacts = [];
    List<Contact> contactList = await NetworkHandler.getContacts();
    for (int i = 0; i < contactList.length; i++) {
      if (typeOfContacts[widget.categoryIndex] ==
          contactList[i].typeOfContact) {
        categoryContacts.add(contactList[i]);
      }
    }
    return categoryContacts;
  }

  Future<int> deleteContact(String contactId) async {
    String url =
        "https://backend.invoicer.at/api/Companies/delete-contact/" + contactId;
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    if (token!.isNotEmpty) {
      token = token.toString();
      final response = await http.put(uri, headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'Bearer $token'
      });
      print(response.statusCode);
    }

    return 0;
  }
}

class Contact {
  final String contactId;
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
      this.contactId,
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
