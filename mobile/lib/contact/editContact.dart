import 'dart:convert';

import 'package:demo5/address/address.dart';
import 'package:demo5/contact/contacts.dart';
import 'package:demo5/network/networkHandler.dart';
import 'package:flutter/material.dart';
import 'package:select_form_field/select_form_field.dart';
import 'package:http/http.dart' as http;

class EditContact extends StatelessWidget {
  final formGlobalKey = GlobalKey<FormState>();
  final String typeOfContact;
  final String gender;
  final String? title;
  final String firstName;
  final String lastName;
  final String nameOfOrganisation;
  final String phoneNumber;
  final String email;
  final String contactId;
  final Address address;

  EditContact(
      {Key? key,
      required this.contactId,
      required this.typeOfContact,
      required this.gender,
      this.title,
      required this.firstName,
      required this.lastName,
      required this.nameOfOrganisation,
      required this.phoneNumber,
      required this.email,
      required this.address})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    List<String> typeOfContactsDE = [
      "Anbieter",
      "Kunde",
      "Partner",
      "Möglicher Kunde",
      "Keine Zielgruppe"
    ];

    List<String> typeOfContactsEN = [
      "Supplier",
      "Client",
      "Partner",
      "ProspectiveClient",
      "NoTargetGroup"
    ];
    int index = 0;
    for (int i = 0; i < typeOfContactsEN.length; i++) {
      if (typeOfContactsEN[i] == this.typeOfContact) {
        index = i;
      }
    }
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

    List<String> gendersEn = [
      "Male",
      "Female",
      "Others",
    ];
    List<String> gendersDe = [
      "Männlich",
      "Weiblich",
      "Anders",
    ];

    int genderIndex = 0;
    for (int i = 0; i < gendersEn.length; i++) {
      if (gendersEn[i] == this.gender) {
        genderIndex = i;
      }
    }

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
    if (this.title == null || this.title!.isEmpty) {
      title.text = "";
    } else {
      title.text = this.title!;
    }
    typeOfContact.text = typeOfContactsDE[index];
    gender.text = gendersEn[genderIndex];

    firstName.text = this.firstName;
    lastName.text = this.lastName;
    nameOfOrganisation.text = this.nameOfOrganisation;
    phoneNumber.text = this.phoneNumber;
    email.text = this.email;
    street.text = address.street;
    zipCode.text = address.zipCode;
    city.text = address.city;
    country.text = address.country;

    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Kontakt Bearbeiten',
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
                  key: formGlobalKey,
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
                              return "Bitte Kontaktart auswählen!";
                            }
                            return null;
                          },
                          type: SelectFormFieldType.dropdown,
                          labelText: 'Kontaktart',
                          items: _typeOfContacts,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: typeOfContactsDE[index],
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
                              return "Bitte Geschlecht auswählen!";
                            }
                            return null;
                          },
                          type: SelectFormFieldType.dropdown,
                          labelText: 'Geschlecht',
                          items: _genders,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: gendersDe[genderIndex],
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
                            'Titel',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: title,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Titel eingeben',
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
                                if (!formGlobalKey.currentState!.validate()) {
                                  return;
                                }
                                Address address = Address(street.text,
                                    zipCode.text, city.text, country.text);
                                int categoryIndex = await editContact(
                                    contactId,
                                    typeOfContact.text,
                                    gender.text,
                                    title.text,
                                    firstName.text,
                                    lastName.text,
                                    nameOfOrganisation.text,
                                    phoneNumber.text,
                                    email.text,
                                    address);
                                Navigator.of(context).pushAndRemoveUntil(
                                  MaterialPageRoute(
                                      builder: (context) => Contacts(
                                            categoryIndex: categoryIndex,
                                          )),
                                  (route) => false,
                                );
                              },
                              shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(100)),
                              color: Colors.redAccent[700],
                              child: const Text('Speichern',
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

  Future<int> editContact(
      String contactId,
      String typeOfContact,
      String gender,
      String? title,
      String firstName,
      String lastName,
      String nameOfOrganisation,
      String phoneNumber,
      String email,
      Address address) async {
    String url = "https://backend.invoicer.at/api/Contacts";
    Uri uri = Uri.parse(url);

    List<String> typeOfContacts = [
      "Supplier",
      "Client",
      "Partner",
      "ProspectiveClient",
      "NoTargetGroup"
    ];

    List<String> typeOfContactsDE = [
      "Anbieter",
      "Kunde",
      "Partner",
      "Möglicher Kunde",
      "Keine Zielgruppe"
    ];

    int categoryIndex = 0;

    if (typeOfContact.isEmpty) {
      typeOfContact = this.typeOfContact;
    }
    if (gender.isEmpty) {
      gender = this.gender;
    }

    String? token = await NetworkHandler.getToken();
    if (token!.isNotEmpty) {
      token = token.toString();

      for (int i = 0; i < typeOfContacts.length; i++) {
        if (typeOfContacts[i] == typeOfContact ||
            typeOfContactsDE[i] == typeOfContact ||
            i == int.tryParse(typeOfContact) &&
                categoryIndex <= typeOfContacts.length - 1) {
          categoryIndex = i;
          typeOfContact = typeOfContacts[i];
        }
      }

      var body = {};
      body["id"] = contactId;
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
