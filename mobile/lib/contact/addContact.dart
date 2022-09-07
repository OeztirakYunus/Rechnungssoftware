// ignore: file_names
import 'dart:convert';

import 'package:demo5/address/address.dart';
import 'package:demo5/contact/contacts.dart';
import 'package:demo5/network/networkHandler.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:select_form_field/select_form_field.dart';

class AddContact extends StatelessWidget {
  const AddContact({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    List<Map<String, dynamic>> _typeOfContacts = [
      {'value': 0, 'label': 'Anbieter'},
      {'value': 1, 'label': 'Kunde'},
      {'value': 2, 'label': 'Partner'},
      {'value': 3, 'label': 'Möglicher Kunde'},
      {'value': 4, 'label': 'Keine Zielgruppe'}
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
    TextEditingController street = TextEditingController();
    TextEditingController zipCode = TextEditingController();
    TextEditingController city = TextEditingController();
    TextEditingController country = TextEditingController();

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
                        Row(
                          children: [
                            Expanded(
                              flex: 2,
                              child: ListTile(
                                title: const Text('Straße'),
                                subtitle: TextFormField(
                                  controller: street,
                                  decoration: const InputDecoration(
                                    border: OutlineInputBorder(
                                        borderRadius: BorderRadius.all(
                                            Radius.circular(100))),
                                    hintText: 'Straße',
                                    hintStyle: TextStyle(fontSize: 20.00),
                                  ),
                                  style: const TextStyle(fontSize: 20.00),
                                ),
                              ),
                            ),
                            const SizedBox(
                              width: 1,
                            ),
                            Expanded(
                              flex: 1,
                              child: ListTile(
                                title: const Text('PLZ'),
                                subtitle: TextFormField(
                                  controller: zipCode,
                                  keyboardType: TextInputType.number,
                                  decoration: const InputDecoration(
                                    border: OutlineInputBorder(
                                        borderRadius: BorderRadius.all(
                                            Radius.circular(100))),
                                    hintText: 'PLZ',
                                    hintStyle: TextStyle(fontSize: 20.00),
                                  ),
                                  style: const TextStyle(fontSize: 20.00),
                                ),
                              ),
                            ),
                          ],
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'Stadt',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: city,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Stadt eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'Land',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: country,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Land eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        Column(
                          children: [
                            const SizedBox(
                              height: 25.00,
                            ),
                            MaterialButton(
                              onPressed: () async {
                                Address address = Address(street.text,
                                    zipCode.text, city.text, country.text);
                                int categoryIndex = await addContact(
                                    typeOfContact.text,
                                    gender.text,
                                    title.text,
                                    firstName.text,
                                    lastName.text,
                                    nameOfOrganisation.text,
                                    phoneNumber.text,
                                    email.text,
                                    address);
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                      builder: (context) => Contacts(
                                            categoryIndex: categoryIndex,
                                          )),
                                );
                              },
                              shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(100)),
                              color: Colors.redAccent[700],
                              child: const Text('Hinzufügen',
                                  style:
                                      TextStyle(fontSize: 22.00, height: 1.35)),
                              textColor: Colors.white,
                              height: 50.00,
                              minWidth: 477.00,
                            ),
                          ],
                        ),
                      ]),
                ))
          ],
        ),
      ),
    ));
  }

  Future<int> addContact(
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

    List<String> typeOfContacts = [
      "Supplier",
      "Client",
      "Partner",
      "ProspectiveClient",
      "NoTargetGroup"
    ];
    int categoryIndex = 0;

    String? token = await NetworkHandler.getToken();
    if (token!.isNotEmpty) {
      token = token.toString();

      for (int i = 0; i < typeOfContacts.length; i++) {
        if (typeOfContacts[i] == typeOfContact &&
            categoryIndex <= typeOfContacts.length - 1) {
          categoryIndex = i;
        }
      }

      var body = {};
      body["typeOfContactEnum"] = typeOfContact;
      body["gender"] = gender;
      body["title"] = title;
      body["firstName"] = firstName;
      body["lastName"] = lastName;
      body["nameOfOrganisation"] = nameOfOrganisation;
      body["phoneNumber"] = phoneNumber;
      body["email"] = email;
      body["address"] = {
        "street": address.street,
        "zipCode": address.zipCode,
        "city": address.city,
        "country": address.country
      };
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
    return categoryIndex;
  }
}
