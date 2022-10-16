import 'package:demo5/products/categoryList.dart';
import 'package:demo5/authentication/signup.dart';
import 'package:flutter/gestures.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';
import 'dart:convert';
import 'package:demo5/network/networkHandler.dart';

class Login extends StatefulWidget {
  const Login({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _LoginState();
}

class _LoginState extends State<Login> {
  bool? changed = false;
  final formGlobalKey = GlobalKey<FormState>();
  TextEditingController userMail = TextEditingController();
  TextEditingController userPsw = TextEditingController();
  bool _passwordVisible = true;
  bool isLoading = false;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Anmeldung',
            style:
                TextStyle(height: 1.00, fontSize: 25.00, color: Colors.white)),
        centerTitle: true,
      ),
      body: SingleChildScrollView(
        child: Column(
          children: <Widget>[
            Container(
                height: 150,
                margin: const EdgeInsets.all(10),
                child: ClipRRect(
                  borderRadius: BorderRadius.circular(100),
                  child: Image.asset(
                    'lib/assets/kassa.png',
                    width: 150,
                  ),
                )),
            Container(
              height: 310,
              padding: const EdgeInsets.all(10),
              child: Form(
                key: formGlobalKey,
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Align(
                      alignment: Alignment(-0.95, 1),
                      child: Text(
                        'Email',
                        style: TextStyle(fontSize: 20.00),
                      ),
                    ),
                    TextFormField(
                      controller: userMail,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return "Bitte Email eingeben";
                        } else if (isEmailValid(value.toString())) {
                          return "Email ist nicht gültig";
                        }
                        return null;
                      },
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
                        }
                        return null;
                      },
                      obscureText: !_passwordVisible,
                      decoration: InputDecoration(
                        border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(100.0)),
                        hintText: 'Passwort eingeben',
                        hintStyle: const TextStyle(fontSize: 20.00),
                        suffixIcon: IconButton(
                          icon: Icon(
                            _passwordVisible
                                ? Icons.visibility
                                : Icons.visibility_off,
                            color: Theme.of(context).primaryColorDark,
                          ),
                          onPressed: () {
                            setState(() {
                              _passwordVisible = !_passwordVisible;
                            });
                          },
                        ),
                      ),
                      style: const TextStyle(fontSize: 20.00),
                    ),
                    const SizedBox(
                      height: 10.00,
                    ),
                    const SizedBox(
                      height: 25.00,
                    ),
                    isLoading
                        ? const Center(child: CircularProgressIndicator())
                        : MaterialButton(
                            onPressed: () async {
                              if (!formGlobalKey.currentState!.validate()) {
                                return;
                              }
                              setState(() {
                                isLoading = true;
                              });
                              int statusCode =
                                  await loginUser(userMail.text, userPsw.text);
                              if (statusCode == 200) {
                                Navigator.of(context).pushAndRemoveUntil(
                                  MaterialPageRoute(
                                      builder: (context) => const Categories()),
                                  (route) => false,
                                );
                              } else if (statusCode != 200) {
                                String message =
                                    "Benutzer/Passwort ist falsch!";
                                showAlertDialog(context, message);
                                setState(() {
                                  isLoading = false;
                                });
                                return;
                              }
                              setState(() {
                                isLoading = false;
                              });
                            },
                            shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(100)),
                            color: Colors.redAccent[700],
                            child: const Text('Anmelden',
                                style:
                                    TextStyle(fontSize: 22.00, height: 1.35)),
                            textColor: Colors.white,
                            height: 50.00,
                            minWidth: 477.00,
                          ),
                  ],
                ),
              ),
            ),
            Container(
              alignment: Alignment.bottomLeft,
              width: 250,
              child: RichText(
                text: TextSpan(
                    text: 'Noch kein Konto?',
                    recognizer: TapGestureRecognizer()
                      ..onTap = () => Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => const SignUp())),
                    style: const TextStyle(
                      color: Colors.blueAccent,
                      fontSize: 18,
                    )),
              ),
            )
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

  showAlertDialog(BuildContext context, String message) {
    AlertDialog alert = AlertDialog(
      title: const Text("Achtung!"),
      content: Text(message),
      actions: [
        TextButton(
          child: const Text("OK"),
          onPressed: () {
            Navigator.of(context).pop();
          },
        )
      ],
    );

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return alert;
      },
    );
  }

  Future<int> loginUser(String email, String password) async {
    String url = "https://backend.invoicer.at/api/Auth/login";
    Uri uri = Uri.parse(url);

    String authorization = email + ":" + password;
    String encodedAuthorization = base64.encode(utf8.encode(authorization));

    final response = await http
        .get(uri, headers: {"Authorization": "Basic " + encodedAuthorization});
    print(response.statusCode);

    if (response.statusCode == 200) {
      var responseString = json.decode(response.body);
      var responseToken = responseString["auth_token"];
      String userRole = responseString["userRoles"];
      NetworkHandler.storeToken(responseToken);
      NetworkHandler.storeRole(userRole);
      print("Token: $responseToken");
      var readToken = await NetworkHandler.getToken();
      readToken = readToken.toString();
      if (readToken.isNotEmpty) {
        print("READ TOKEN $readToken");
        print(responseString["role"]);
      }
    }

    return response.statusCode;
  }
}
