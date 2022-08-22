import 'package:demo5/home/home.dart';
import 'package:demo5/products/addProduct.dart';
import 'package:demo5/products/categoryList.dart';
import 'package:demo5/products/product.dart';
import 'package:demo5/authentication/signup.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/gestures.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';
import 'dart:convert';
import 'package:demo5/network/networkHandler.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class Login extends StatefulWidget {
  const Login({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _LoginState();
}

class _LoginState extends State<Login> {
  bool? changed = false;
  TextEditingController userMail = TextEditingController();
  TextEditingController userPsw = TextEditingController();
  bool _passwordVisible = true;

  @override
  void initState() {
    _passwordVisible = true;
  }

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
                margin: const EdgeInsets.all(10),
                child: ClipRRect(
                  borderRadius: BorderRadius.circular(100),
                  child: Image.asset(
                    'lib/assets/kassa.png',
                    width: 150,
                  ),
                )),
            Container(
              padding: const EdgeInsets.all(10),
              child: Form(
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
                      obscureText: !_passwordVisible,
                      decoration: InputDecoration(
                        border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(100.0)),
                        hintText: 'Passwort eingeben',
                        hintStyle: const TextStyle(fontSize: 20.00),
                        suffixIcon: IconButton(
                          icon: Icon(
                            // Based on passwordVisible state choose the icon
                            _passwordVisible
                                ? Icons.visibility
                                : Icons.visibility_off,
                            color: Theme.of(context).primaryColorDark,
                          ),
                          onPressed: () {
                            // Update the state i.e. toogle the state of passwordVisible variable
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
                    Column(
                      children: [
                        SizedBox(
                          width: MediaQuery.of(context).size.width - 10,
                          child: CheckboxListTile(
                            title: const Text('Anmeldedaten merken'),
                            value: changed,
                            onChanged: (changedValue) {
                              setState(() {
                                changed = changedValue;
                              });
                            },
                            controlAffinity: ListTileControlAffinity.leading,
                          ),
                        ),
                        SizedBox(
                          width: 350,
                          child: RichText(
                            textAlign: TextAlign.start,
                            text: TextSpan(
                                text: 'Passwort vergessen?',
                                recognizer: TapGestureRecognizer()
                                  ..onTap = () => Navigator.push(
                                      context,
                                      MaterialPageRoute(
                                          builder: (context) => SignUp())),
                                style: const TextStyle(
                                  color: Colors.blueAccent,
                                  fontSize: 18,
                                )),
                          ),
                        ),
                      ],
                    ),
                    const SizedBox(
                      height: 25.00,
                    ),
                    MaterialButton(
                      onPressed: () async {
                        int statusCode =
                            await loginUser(userMail.text, userPsw.text);
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
                      color: Colors.purpleAccent[700],
                      child: const Text('Anmelden',
                          style: TextStyle(fontSize: 22.00, height: 1.35)),
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
              width: 350,
              child: RichText(
                text: TextSpan(
                    text: 'Noch kein Konto?',
                    recognizer: TapGestureRecognizer()
                      ..onTap = () => Navigator.push(context,
                          MaterialPageRoute(builder: (context) => SignUp())),
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
      NetworkHandler.storeToken(responseToken);
      print("Token: $responseToken");
      var readToken = await NetworkHandler.getToken();
      readToken = readToken.toString();
      if (readToken.isNotEmpty) {
        print("READ TOKEN $readToken");
      }
    }

    return response.statusCode;
  }
}
