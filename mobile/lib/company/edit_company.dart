import 'dart:convert';

import 'package:demo5/company/company.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import '../network/networkHandler.dart';

class EditCompany extends StatefulWidget {
  final String companyId;
  final String adressId;
  final String bankInformationId;
  final String companyName;
  final String email;
  final String phoneNumber;
  final String ustNumber;
  final String street;
  final String zipCode;
  final String city;
  final String country;
  final String bankName;
  final String iban;
  final String bic;
  const EditCompany(
      {Key? key,
      required this.companyId,
      required this.adressId,
      required this.bankInformationId,
      required this.companyName,
      required this.email,
      required this.phoneNumber,
      required this.ustNumber,
      required this.street,
      required this.zipCode,
      required this.city,
      required this.country,
      required this.bankName,
      required this.iban,
      required this.bic})
      : super(key: key);

  @override
  State<StatefulWidget> createState() => _EditCompanyState();
}

class _EditCompanyState extends State<EditCompany> {
  TextEditingController companyName = TextEditingController();
  TextEditingController email = TextEditingController();
  TextEditingController phoneNumber = TextEditingController();
  TextEditingController ustNumber = TextEditingController();

  TextEditingController street = TextEditingController();
  TextEditingController zipCode = TextEditingController();
  TextEditingController city = TextEditingController();
  TextEditingController country = TextEditingController();

  TextEditingController bankName = TextEditingController();
  TextEditingController iban = TextEditingController();
  TextEditingController bic = TextEditingController();

  @override
  void initState() {
    super.initState();
    companyName.text = widget.companyName;
    email.text = widget.email;
    phoneNumber.text = widget.phoneNumber;
    ustNumber.text = widget.ustNumber;

    street.text = widget.street;
    zipCode.text = widget.zipCode;
    city.text = widget.city;
    country.text = widget.country;

    bankName.text = widget.bankName;
    iban.text = widget.bankName;
    bic.text = widget.bic;
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Scaffold(
            appBar: AppBar(
              title: const Text('Unternehmen bearbeiten',
                  style: TextStyle(
                      height: 1.00, fontSize: 25.00, color: Colors.white)),
              centerTitle: true,
            ),
            body: SingleChildScrollView(
                child: Column(children: <Widget>[
              Container(
                  padding: const EdgeInsets.all(10),
                  child: Form(
                      child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child:
                              Text('Name', style: TextStyle(fontSize: 20.00)),
                        ),
                        TextFormField(
                          controller: companyName,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Name eingeben',
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
                                  borderRadius: BorderRadius.circular(50.0)),
                              hintText: 'Email eingeben',
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
                            'Ust-Nummer',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: ustNumber,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(50.0)),
                              hintText: 'Ust-Nummer eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'Straße',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: street,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Straße eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'PLZ',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: zipCode,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'PLZ eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
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
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'Bankname',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: bankName,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Bankname eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'IBAN',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: iban,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'IBAN eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'BIC',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: bic,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'BIC eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        MaterialButton(
                          onPressed: () async {
                            await editCompany(
                                widget.companyId,
                                widget.adressId,
                                widget.bankInformationId,
                                companyName.text,
                                email.text,
                                phoneNumber.text,
                                ustNumber.text,
                                street.text,
                                zipCode.text,
                                city.text,
                                country.text,
                                bankName.text,
                                iban.text,
                                bic.text);
                            Navigator.of(context).pushAndRemoveUntil(
                              MaterialPageRoute(
                                  builder: (context) => const Companies()),
                              (route) => false,
                            );
                          },
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(100)),
                          color: Colors.redAccent[700],
                          child: const Text('Speichern',
                              style: TextStyle(fontSize: 22.00, height: 1.35)),
                          textColor: Colors.white,
                          height: 50.00,
                          minWidth: 477.00,
                        ),
                      ])))
            ]))));
  }
}

Future<void> editCompany(
    String companyId,
    String addressId,
    String bankInformationId,
    String companyName,
    String email,
    String phoneNumber,
    String ustNumber,
    String street,
    String zipCode,
    String city,
    String country,
    String bankName,
    String iban,
    String bic) async {
  String url = "https://backend.invoicer.at/api/Companies";
  Uri uri = Uri.parse(url);

  String? token = await NetworkHandler.getToken();
  if (token!.isNotEmpty) {
    token = token.toString();
    var body = {};
    body["id"] = companyId;
    body["addressId"] = addressId;
    body["bankInformationId"] = bankInformationId;
    body["companyName"] = companyName;
    body["email"] = email;
    body["phoneNumber"] = phoneNumber;
    body["ustNumber"] = ustNumber;
    body["bankInformation"] = {"bankName": bankName, "iban": iban, "bic": bic};
    body["address"] = {
      "street": street,
      "zipCode": zipCode,
      "city": city,
      "country": country
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
  }
}
