import 'dart:convert';

import 'package:demo5/address/address.dart';
import 'package:demo5/companies/companies.dart';
import 'package:demo5/products/categoryList.dart';
import 'package:demo5/user/user.dart';
import 'package:http/http.dart' as http;
import 'package:demo5/products/product.dart';
import 'package:flutter/material.dart';

import '../network/networkHandler.dart';

class SignUp extends StatelessWidget {
  SignUp({Key? key}) : super(key: key);
  final formGlobalKey = GlobalKey<FormState>();
  TextEditingController firstName = TextEditingController();
  TextEditingController lastName = TextEditingController();
  TextEditingController userMail = TextEditingController();
  TextEditingController userPsw = TextEditingController();
  TextEditingController companyName = TextEditingController();
  TextEditingController companyMail = TextEditingController();
  TextEditingController companyPhoneNumber = TextEditingController();
  TextEditingController companyUst = TextEditingController();
  TextEditingController companyAddress = TextEditingController();
  TextEditingController companyPostalCode = TextEditingController();
  TextEditingController companyCity = TextEditingController();
  TextEditingController companyCountry = TextEditingController();
  TextEditingController companyBankName = TextEditingController();
  TextEditingController companyIban = TextEditingController();
  TextEditingController companyBic = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Registrierung',
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
                      child: Text(
                        'Vorname',
                        style: TextStyle(fontSize: 20.00),
                      ),
                    ),
                    TextFormField(
                      controller: firstName,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return "Bitte Vorname eingeben";
                        } else if (value.length < 2) {
                          return "Vorname ist zu kurz";
                        }
                        return null;
                      },
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
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return "Bitte Nachname eingeben";
                        } else if (value.length < 2) {
                          return "Nachname ist zu kurz";
                        }
                        return null;
                      },
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
                        'Email',
                        style: TextStyle(fontSize: 20.00),
                      ),
                    ),
                    TextFormField(
                      controller: userMail,
                      autofocus: false,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return "Bitte Email eingeben";
                        } else if (isEmailValid(value.toString())) {
                          return "Email ist nicht gültig";
                        }
                        return null;
                      },
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
                    const Align(
                      alignment: Alignment(-0.95, 1),
                      child:
                          Text('Passwort', style: TextStyle(fontSize: 20.00)),
                    ),
                    TextFormField(
                      controller: userPsw,
                      autofocus: false,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return "Bitte Passwort eingeben";
                        } else if (value.length < 6) {
                          return "Passwort muss mindestens 6 Zeichen lang sein";
                        }
                        return null;
                      },
                      obscureText: true,
                      decoration: InputDecoration(
                        border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(100.0)),
                        hintText: 'Passwort eingeben',
                        hintStyle: const TextStyle(fontSize: 20.00),
                        prefixIcon: const Padding(
                          padding:
                              EdgeInsets.only(top: 0, right: 12, bottom: 0),
                          child: Icon(Icons.remove_red_eye,
                              size: 22, color: Colors.grey),
                        ),
                      ),
                      style: const TextStyle(fontSize: 20.00),
                    ),
                    const SizedBox(
                      height: 25.00,
                    ),
                    const Align(
                      alignment: Alignment(-0.95, 1),
                      child: Text(
                        'Firmenname',
                        style: TextStyle(fontSize: 20.00),
                      ),
                    ),
                    TextFormField(
                      controller: companyName,
                      autofocus: false,
                      validator: (value) {
                        if (value == null) {
                          companyName.text = "";
                        }
                        return null;
                      },
                      decoration: InputDecoration(
                          border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(100.0)),
                          hintText: 'Firmenname eingeben',
                          hintStyle: const TextStyle(fontSize: 20.00)),
                      style: const TextStyle(fontSize: 20.00),
                    ),
                    const SizedBox(
                      height: 25.00,
                    ),
                    const Align(
                      alignment: Alignment(-0.95, 1),
                      child: Text(
                        'Firmenemail',
                        style: TextStyle(fontSize: 20.00),
                      ),
                    ),
                    TextFormField(
                      controller: companyMail,
                      autofocus: false,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          companyMail.text = "";
                        } else if (value.isNotEmpty &&
                            isEmailValid(value.toString())) {
                          return "Firmenmail ist nicht gültig";
                        }
                        return null;
                      },
                      decoration: InputDecoration(
                          border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(100.0)),
                          hintText: 'Firmenemail eingeben',
                          hintStyle: const TextStyle(fontSize: 20.00)),
                      style: const TextStyle(fontSize: 20.00),
                    ),
                    const SizedBox(
                      height: 25.00,
                    ),
                    const Align(
                      alignment: Alignment(-0.95, 1),
                      child: Text(
                        'Firmentelefonnummer',
                        style: TextStyle(fontSize: 20.00),
                      ),
                    ),
                    TextFormField(
                      controller: companyPhoneNumber,
                      autofocus: false,
                      keyboardType: TextInputType.number,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          companyPhoneNumber.text = "";
                        } else if (value.length < 4) {
                          return "Firmentelefonnummer muss mind. 8 Ziffern lang sein";
                        }
                        return null;
                      },
                      decoration: InputDecoration(
                          border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(100.0)),
                          hintText: 'Firmentelefonnummer eingeben',
                          hintStyle: const TextStyle(fontSize: 20.00)),
                      style: const TextStyle(fontSize: 20.00),
                    ),
                    const SizedBox(
                      height: 25.00,
                    ),
                    const Align(
                      alignment: Alignment(-0.95, 1),
                      child: Text(
                        'UST',
                        style: TextStyle(fontSize: 20.00),
                      ),
                    ),
                    TextFormField(
                      controller: companyUst,
                      autofocus: false,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          companyPhoneNumber.text = "";
                        }
                        return null;
                      },
                      decoration: InputDecoration(
                          border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(100.0)),
                          hintText: 'UST eingeben',
                          hintStyle: const TextStyle(fontSize: 20.00)),
                      style: const TextStyle(fontSize: 20.00),
                    ),
                    const SizedBox(
                      height: 25.00,
                    ),
                    const Align(
                      alignment: Alignment(-0.95, 1),
                      child: Text(
                        'Firmenadresse',
                        style: TextStyle(fontSize: 20.00),
                      ),
                    ),
                    TextFormField(
                      controller: companyAddress,
                      autofocus: false,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          companyAddress.text = "";
                        } else if (value.isNotEmpty &&
                            !value.contains(RegExp(r'[0-9]'))) {
                          return "Bitte Hausnummer eingeben";
                        }
                        return null;
                      },
                      decoration: InputDecoration(
                          border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(100.0)),
                          hintText: 'Firmenadresse mit Hausnummer',
                          hintStyle: const TextStyle(fontSize: 20.00)),
                      style: const TextStyle(fontSize: 20.00),
                    ),
                    const SizedBox(
                      height: 25.00,
                    ),
                    const Align(
                      alignment: Alignment(-0.95, 1),
                      child: Text(
                        'Postleitzahl',
                        style: TextStyle(fontSize: 20.00),
                      ),
                    ),
                    TextFormField(
                      controller: companyPostalCode,
                      autofocus: false,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          companyPostalCode.text = "";
                        } else if (value.isNotEmpty && value.length != 4) {
                          return "Postleitzahl muss 4 Ziffern lang sein";
                        }
                        return null;
                      },
                      keyboardType: TextInputType.number,
                      decoration: InputDecoration(
                          border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(100.0)),
                          hintText: 'Postleitzahl eingeben',
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
                      controller: companyCity,
                      autofocus: false,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          companyCity.text = "";
                        }
                        return null;
                      },
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
                      controller: companyCountry,
                      autofocus: false,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          companyCountry.text = "";
                        }
                        return null;
                      },
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
                      controller: companyBankName,
                      autofocus: false,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          companyBankName.text = "";
                        }
                        return null;
                      },
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
                      controller: companyIban,
                      autofocus: false,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          companyIban.text = "";
                        }
                        return null;
                      },
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
                      controller: companyBic,
                      autofocus: false,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          companyBic.text = "";
                        }
                        return null;
                      },
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
                        if (!formGlobalKey.currentState!.validate()) {
                          return;
                        }
                        int statusCode = await registerUser(
                            firstName.text,
                            lastName.text,
                            userMail.text,
                            userPsw.text,
                            companyName.text,
                            companyMail.text,
                            companyPhoneNumber.text,
                            companyUst.text,
                            companyAddress.text,
                            companyPostalCode.text,
                            companyCity.text,
                            companyCountry.text,
                            companyBankName.text,
                            companyIban.text,
                            companyBic.text);
                        if (statusCode == 200) {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) => Categories()),
                          );
                        }
                      },
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(100)),
                      color: Colors.redAccent[700],
                      child: const Text('Registrieren',
                          style: TextStyle(fontSize: 22.00, height: 1.35)),
                      textColor: Colors.white,
                      height: 50.00,
                      minWidth: 477.00,
                    ),
                  ],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  bool isEmailValid(String email) {
    String pattern =
        '^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))';
    RegExp regex = RegExp(pattern);
    return regex.hasMatch(email);
  }

  Future<int> registerUser(
      String firstName,
      String lastName,
      String userMail,
      String userPsw,
      String? companyName,
      String? companyMail,
      String? companyPhoneNumber,
      String? ustNumber,
      String? companyAddress,
      String? companyPostalCode,
      String? companyCity,
      String? companyCountry,
      String? bankName,
      String? iban,
      String? bic) async {
    String url = "https://backend.invoicer.at/api/Auth/register";

    Uri uri = Uri.parse(url);

    var body = {};
    body["company"] = {
      "companyName": companyName,
      "email": companyMail,
      "phoneNumber": companyPhoneNumber,
      "ustNumber": ustNumber,
      "address": {
        "street": companyAddress,
        "zipCode": companyPostalCode,
        "city": companyCity,
        "country": companyCountry
      },
      "bankInformation": {
        "bankName": bankName,
        "iban": iban,
        "bic": bic,
      }
    };
    body["user"] = {
      "firstName": firstName,
      "lastName": lastName,
      "email": userMail,
      "password": userPsw
    };

    var jsonBody = json.encode(body);

    final response = await http.post(uri,
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
        },
        body: jsonBody);

    if (response.statusCode == 200) {
      var responseString = json.decode(response.body);
      String responseToken = responseString["message"].split(": ")[1];
      //var userRole = responseString["userRoles"];
      NetworkHandler.storeToken(responseToken);
      NetworkHandler.storeRole("Admin");
      print("Token: $responseToken");
      var readToken = await NetworkHandler.getToken();
      readToken = readToken.toString();
      if (readToken.isNotEmpty) {
        print("READ TOKEN $readToken");
      }
    }

    print(response.statusCode);
    print(response.body);

    return response.statusCode;
  }
}
