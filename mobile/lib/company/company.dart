import 'dart:convert';
import 'dart:io';

import 'package:demo5/address/address.dart';
import 'package:demo5/navbar.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import '../network/networkHandler.dart';

class Companies extends StatefulWidget {
  const Companies({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _CompaniesState();
}

class _CompaniesState extends State<Companies> {
  late AlertDialog alert;
  Future<bool> _onWillPop() async {
    return (await showDialog(
          context: context,
          builder: (context) => AlertDialog(
            title: const Text('App verlassen?'),
            content: const Text('Möchten Sie die App verlassen?'),
            actions: <Widget>[
              TextButton(
                onPressed: () => Navigator.of(context).pop(false),
                child: const Text('Nein'),
              ),
              TextButton(
                onPressed: () => exit(0),
                child: const Text('Ja'),
              ),
            ],
          ),
        )) ??
        false;
  }

  @override
  Widget build(BuildContext context) {
    return WillPopScope(
        onWillPop: _onWillPop,
        child: SafeArea(
          child: Scaffold(
            appBar: AppBar(
                title: const Text('Unternehmen',
                    style: TextStyle(
                        height: 1.00, fontSize: 25.00, color: Colors.white)),
                centerTitle: true,
                actions: <Widget>[
                  Padding(
                      padding: const EdgeInsets.only(right: 20),
                      child: GestureDetector(
                        onTap: () => {
                          /*Navigator.push(
                                          context,
                                          MaterialPageRoute(
                                              builder: (context) => EditInvoice(
                                                  id: snapshot.data?[index].id,
                                                  documentInformationId: snapshot
                                                      .data?[index]
                                                      .documentInformationId,
                                                  invoiceNum: snapshot
                                                      .data?[index].invoiceNum,
                                                  invoiceDate: snapshot
                                                      .data?[index].invoiceDate,
                                                  paymentTerm: snapshot
                                                      .data?[index].paymentTerm,
                                                  status: snapshot
                                                      .data?[index].status,
                                                  subject: snapshot
                                                      .data?[index].subject,
                                                  headerText: snapshot
                                                      .data?[index].headerText,
                                                  flowText: snapshot
                                                      .data?[index].flowText,
                                                  totalDiscount: snapshot
                                                      .data?[index]
                                                      .totalDiscount,
                                                  typeOfDiscount: snapshot
                                                      .data?[index]
                                                      .typeOfDiscount,
                                                  tax:
                                                      snapshot.data?[index].tax,
                                                  clientId: snapshot
                                                      .data?[index].clientId,
                                                  contactPersonId: snapshot
                                                      .data?[index]
                                                      .contactPersonId,
                                                  quantityPosition: snapshot
                                                      .data?[index]
                                                      .quantityPosition,
                                                  discountPosition: snapshot
                                                      .data?[index]
                                                      .discountPosition,
                                                  typeOfDiscountPosition: snapshot
                                                      .data?[index]
                                                      .typeOfDiscountPosition,
                                                  productPosition: snapshot
                                                      .data?[index]
                                                      .productPosition,
                                                  products: snapshot
                                                      .data?[index].products)),
                                        )*/
                        },
                        child: const Icon(Icons.edit),
                      )),
                  Padding(
                      padding: const EdgeInsets.only(right: 20),
                      child: GestureDetector(
                        onTap: () => {
                          alert = AlertDialog(
                            title: const Text("Achtung!"),
                            content: const Text(
                                "Möchten Sie wirklich dieses Unternehmen löschen?"),
                            actions: [
                              TextButton(
                                child: const Text("Löschen"),
                                onPressed: () async {
                                  await deleteCompany();
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
                      )),
                ]),
            drawer: const NavBar(),
            body: FutureBuilder<Company>(
                future: getCompany(),
                builder: (context, AsyncSnapshot snapshot) {
                  if (snapshot.hasData &&
                      snapshot.connectionState == ConnectionState.done) {
                    return ListView.builder(
                        itemCount: 1,
                        itemBuilder: (context, index) {
                          return Container(
                              padding: const EdgeInsets.all(10),
                              child: Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  const Align(
                                    alignment: Alignment(-0.95, 1),
                                    child: Text('Name',
                                        style: TextStyle(fontSize: 20.00)),
                                  ),
                                  TextFormField(
                                    initialValue: snapshot.data?.companyName,
                                    readOnly: true,
                                    autofocus: false,
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(100.0)),
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
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
                                    initialValue: snapshot.data?.email,
                                    readOnly: true,
                                    autofocus: false,
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(50.0)),
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
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
                                    initialValue: snapshot.data?.phoneNumber,
                                    readOnly: true,
                                    autofocus: false,
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(50.0)),
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
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
                                    initialValue: snapshot.data?.ustNumber,
                                    readOnly: true,
                                    autofocus: false,
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(50.0)),
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
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
                                    initialValue:
                                        snapshot.data?.addresses.street,
                                    readOnly: true,
                                    autofocus: false,
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(50.0)),
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
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
                                    initialValue:
                                        snapshot.data?.addresses.zipCode,
                                    readOnly: true,
                                    autofocus: false,
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(50.0)),
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
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
                                    initialValue: snapshot.data?.addresses.city,
                                    readOnly: true,
                                    autofocus: false,
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(50.0)),
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
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
                                    initialValue:
                                        snapshot.data?.addresses.country,
                                    readOnly: true,
                                    autofocus: false,
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(50.0)),
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
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
                                    initialValue:
                                        snapshot.data?.bankInformation.bankName,
                                    readOnly: true,
                                    autofocus: false,
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(50.0)),
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
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
                                    initialValue:
                                        snapshot.data?.bankInformation.iban,
                                    readOnly: true,
                                    autofocus: false,
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(50.0)),
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
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
                                    initialValue:
                                        snapshot.data?.bankInformation.bic,
                                    readOnly: true,
                                    autofocus: false,
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(50.0)),
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
                                    style: const TextStyle(fontSize: 20.00),
                                  ),
                                ],
                              ));
                        });
                  } else {
                    return const Center(child: CircularProgressIndicator());
                  }
                }),
          ),
        ));
  }
}

Future<void> deleteCompany() async {
  String url = "https://backend.invoicer.at/api/Companies";
  Uri uri = Uri.parse(url);
  String? token = await NetworkHandler.getToken();
  if (token!.isNotEmpty) {
    token = token.toString();

    final response = await http.delete(uri, headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      "Authorization": "Bearer $token"
    });
  }
}

Future<Company> getCompany() async {
  String url = "https://backend.invoicer.at/api/Companies";
  Uri uri = Uri.parse(url);
  late Company company;
  String? token = await NetworkHandler.getToken();
  if (token!.isNotEmpty) {
    token = token.toString();

    final response = await http.get(uri, headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      "Authorization": "Bearer $token"
    });
    print(response.statusCode);

    Map data = await json.decode(response.body);
    String companyId = "";
    String companyName = "";
    String email = "";
    String phoneNumber = "";
    String ustNumber = "";
    String bankName = "";
    String iban = "";
    String bic = "";
    String street = "";
    String zipCode = "";
    String city = "";
    String country = "";
    if (response.statusCode == 200) {
      for (var element in data.entries) {
        if (element.key == "id") {
          companyId = element.value;
        } else if (element.key == "companyName") {
          companyName = element.value;
        } else if (element.key == "email") {
          email = element.value;
        } else if (element.key == "phoneNumber") {
          phoneNumber = element.value;
        } else if (element.key == "ustNumber") {
          ustNumber = element.value;
        } else if (element.key == "bankInformation") {
          Map obj = element.value;
          for (var entry in obj.entries) {
            if (entry.key == "bankName") {
              bankName = entry.value;
            } else if (entry.key == "iban") {
              iban = entry.value;
            } else if (entry.key == "bic") {
              bic = entry.value;
            }
          }
        } else if (element.key == "address") {
          Map obj = element.value;
          for (var entry in obj.entries) {
            if (entry.key == "street") {
              street = entry.value;
            } else if (entry.key == "zipCode") {
              zipCode = entry.value;
            } else if (entry.key == "city") {
              city = entry.value;
            } else if (entry.key == "country") {
              country = entry.value;
            }
          }
        }

        /*String email = obj["email"];
        String phoneNumber = obj["phoneNumber"];
        String ustNumber = obj["ustNumber"];
        String bankName = obj["bankInformation"]["bankName"];
        String iban = obj["bankInformation"]["iban"];
        String bic = obj["bankInformation"]["bic"];
        BankInformation bankInformation = BankInformation(bankName, iban, bic);
        String street = obj["address"]["street"];
        String zipCode = obj["address"]["zipCode"];
        String city = obj["address"]["city"];
        String country = obj["address"]["country"];
        Address address = Address(street, zipCode, city, country);
        company = Company(companyId, companyName, email, phoneNumber, ustNumber,
            address, bankInformation);*/
      }
    }
    Address address = Address(street, zipCode, city, country);
    BankInformation bankInformation = BankInformation(bankName, iban, bic);
    company = Company(companyId, companyName, email, phoneNumber, ustNumber,
        address, bankInformation);
  }
  return company;
}

class Company {
  final String companyId;
  final String companyName;
  final String email;
  final String phoneNumber;
  final String ustNumber;
  final Address addresses;
  final BankInformation bankInformation;

  Company(this.companyId, this.companyName, this.email, this.phoneNumber,
      this.ustNumber, this.addresses, this.bankInformation);
}

class BankInformation {
  final String bankName;
  final String iban;
  final String bic;

  BankInformation(this.bankName, this.iban, this.bic);
}
