// ignore: file_names
import 'dart:convert';

import 'package:demo5/address/address.dart';
import 'package:demo5/network/networkHandler.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:select_form_field/select_form_field.dart';

class AddContact extends StatelessWidget {
  const AddContact({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    List<Map<String, dynamic>> _typeOfContacts = [
      {'value': 0, 'label': 'Supplier'},
      {'value': 1, 'label': 'Client'},
      {'value': 2, 'label': 'Partner'},
      {'value': 3, 'label': 'ProspectiveClient'},
      {'value': 4, 'label': 'NoTargetGroup'}
    ];

    List<Map<String, dynamic>> _genders = [
      {'value': 0, 'label': 'Männlich'},
      {'value': 1, 'label': 'Weiblich'},
      {'value': 2, 'label': 'Anders'},
    ];

    TextEditingController typeOfContact = TextEditingController();
    TextEditingController gender = TextEditingController();
    TextEditingController title = TextEditingController();
    TextEditingController firstName = TextEditingController();
    TextEditingController lastName = TextEditingController();
    TextEditingController nameOfOrganisation = TextEditingController();
    TextEditingController phoneNumber = TextEditingController();
    TextEditingController email = TextEditingController();

    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Kontakt hinzufügen',
            style:
                TextStyle(height: 1.00, fontSize: 25.00, color: Colors.white)),
        centerTitle: true,
      ),
      body: SingleChildScrollView(
        child: Column(
          children: <Widget>[
            Container(
                padding: const EdgeInsets.all(10),
                child: Form(
                  child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text('Kontaktart',
                              style: TextStyle(fontSize: 20.00)),
                        ),
                        SelectFormField(
                          controller: typeOfContact,
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return "Kontaktart darf nicht leer sein!";
                            }
                          },
                          type: SelectFormFieldType.dropdown,
                          labelText: 'Kontaktart',
                          items: _typeOfContacts,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Konaktart auswählen',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          onChanged: (val) => typeOfContact.text = val,
                          onSaved: (val) =>
                              val!.isNotEmpty ? typeOfContact.text = val : val,
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text('Geschlecht',
                              style: TextStyle(fontSize: 20.00)),
                        ),
                        SelectFormField(
                          controller: gender,
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return "Geschlecht darf nicht leer sein!";
                            }
                          },
                          type: SelectFormFieldType.dropdown,
                          labelText: 'Geschelcht',
                          items: _genders,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Geschlecht auswählen',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          onChanged: (val) => gender.text = val,
                          onSaved: (val) =>
                              val!.isNotEmpty ? gender.text = val : val,
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'Titel (optional)',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: title,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Titel (optional) eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'Vorname',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: firstName,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Vorname eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'Nachname',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: lastName,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Nachname eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'Name der Organisation',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: nameOfOrganisation,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Name der Organisation eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'Telefonnummer',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: phoneNumber,
                          autofocus: false,
                          keyboardType: TextInputType.number,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Telefonnummer eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'Email',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: email,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Email eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                      ]),
                ))
          ],
        ),
      ),
    ));
  }

  Future<void> addContact(
      final String typeOfContact,
      final String gender,
      final String? title,
      final String firstName,
      final String lastName,
      final String nameOfOrganisation,
      final String phoneNumber,
      final String email,
      final Address address) async {
    String url = "https://backend.invoicer.at/api/Companies/add-contact";
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    if (token!.isNotEmpty) {
      token = token.toString();

      var body = {};
      body["typeOfContactEnum"] = typeOfContact;
      body["gender"] = gender;
      body["title"] = title;
      body["firstName"] = firstName;
      body["lastName"] = lastName;
      body["nameOfOrganisation"] = nameOfOrganisation;
      body["phoneNumber"] = phoneNumber;
      body["email"] = email;
      body["address"]["street"] = address.street;
      body["address"]["zipCode"] = address.zipCode;
      body["address"]["city"] = address.city;
      body["address"]["country"] = address.country;
      var jsonBody = json.encode(body);

      final response = await http.put(uri,
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': 'Bearer $token'
          },
          body: jsonBody);

      print(response.statusCode);
      print(response.body);
    }
  }
}
