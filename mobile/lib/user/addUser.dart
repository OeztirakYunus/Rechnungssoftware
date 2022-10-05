// ignore: file_names
import 'dart:convert';
import 'package:demo5/network/networkHandler.dart';
import 'package:demo5/user/user.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:select_form_field/select_form_field.dart';

class AddUser extends StatefulWidget {
  const AddUser({
    Key? key,
  }) : super(key: key);

  @override
  State<StatefulWidget> createState() => _AddUserState();
}

class _AddUserState extends State<AddUser> {
  @override
  Widget build(BuildContext context) {
    final formGlobalKey = GlobalKey<FormState>();
    bool _passwordVisible = true;
    TextEditingController firstName = TextEditingController();
    TextEditingController lastName = TextEditingController();
    TextEditingController email = TextEditingController();
    TextEditingController role = TextEditingController();
    TextEditingController password = TextEditingController();

    List<Map<String, dynamic>> _roles = [
      {'value': 0, 'label': 'Admin'},
      {'value': 1, 'label': 'User'},
    ];

    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Benutzer hinzufügen',
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
                          child:
                              Text('Rolle', style: TextStyle(fontSize: 20.00)),
                        ),
                        SelectFormField(
                          controller: role,
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return "Rolle darf nicht leer sein!";
                            } else {
                              return value;
                            }
                          },
                          type: SelectFormFieldType.dropdown,
                          labelText: 'Rolle',
                          items: _roles,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Rolle auswählen',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          onChanged: (val) => role.text = val,
                          onSaved: (val) =>
                              val!.isNotEmpty ? role.text = val : val,
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
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return "Bitte Vorname eingeben!";
                            } else {
                              return value;
                            }
                          },
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
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return "Bitte Nachname eingeben!";
                            } else {
                              return value;
                            }
                          },
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
                            'Email',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: email,
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return "Bitte Email eingeben!";
                            } else {
                              return value;
                            }
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
                          child: Text(
                            'Passwort',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: password,
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
                          height: 2.00,
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
                                int roleIndex = await addUser(
                                    firstName.text,
                                    lastName.text,
                                    email.text,
                                    role.text,
                                    password.text);
                                Navigator.of(context).pushAndRemoveUntil(
                                  MaterialPageRoute(
                                      builder: (context) => Users(
                                          roleIndex: roleIndex,
                                          role: role.text)),
                                  (route) => false,
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

  Future<int> addUser(String firstName, String lastName, String email,
      String role, String password) async {
    String url = "https://backend.invoicer.at/api/Companies/add-user";
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    List<String> roles = [
      "Admin",
      "User",
    ];
    int roleIndex = 0;

    if (token!.isNotEmpty) {
      token = token.toString();

      for (int i = 0; i < roles.length; i++) {
        if (roles[i] == role && roleIndex <= roles.length - 1) {
          roleIndex = i;
        }
      }

      var body = {};
      body["firstName"] = firstName;
      body["lastName"] = lastName;
      body["email"] = email;
      body["role"] = role;
      body["password"] = password;

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
    return roleIndex;
  }
}
