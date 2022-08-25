import 'dart:convert';

import 'package:demo5/network/networkHandler.dart';
import 'package:demo5/user/addUser.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import '../NavBar.dart';

class Users extends StatefulWidget {
  final int roleIndex;
  final String role;
  const Users({Key? key, required this.roleIndex, required this.role})
      : super(key: key);

  @override
  State<StatefulWidget> createState() => _UsersState();
}

class _UsersState extends State<Users> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    setState(() {});
    return SafeArea(
      child: Scaffold(
        appBar: AppBar(
          title: const Text('Benutzer',
              style: TextStyle(
                  height: 1.00, fontSize: 25.00, color: Colors.white)),
          centerTitle: true,
        ),
        drawer: const NavBar(),
        body: FutureBuilder<List<User>>(
            future: getUsers(),
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
                                    "Möchten Sie wirklich diesen Benutzer löschen?"),
                                actions: [
                                  TextButton(
                                    child: const Text("Löschen"),
                                    onPressed: () async {
                                      await deleteUser(
                                          snapshot.data?[index].userId);
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
              MaterialPageRoute(builder: (context) => const AddUser()),
            );
          },
          backgroundColor: Colors.purple,
          child: const Icon(Icons.add),
        ),
      ),
    );
  }

  Future<List<User>> getUsers() async {
    String url = "https://backend.invoicer.at/api/Users";
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    List<User> users = [];
    List<String> roles = ["Admin", "User"];

    if (token!.isNotEmpty) {
      token = token.toString();

      final response = await http.get(uri, headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        "Authorization": "Bearer $token"
      });

      print(response.statusCode);

      List data = await json.decode(response.body) as List;
      for (var element in data) {
        Map obj = element;
        String firstName = obj["firstName"];
        String lastName = obj["lastName"];
        String email = obj["email"];
        String role = obj["role"];
        String userId = obj["id"];
        User user = User(firstName, lastName, email, role, userId);
        if (roles[widget.roleIndex] == user.role) {
          users.add(user);
        }
      }
    }
    return users;
  }

  Future<void> deleteUser(String userId) async {
    String url =
        "https://backend.invoicer.at/api/Companies/delete-user/" + userId;
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
  }
}

class User {
  final String firstName;
  final String lastName;
  final String email;
  final String role;
  final String userId;

  User(this.firstName, this.lastName, this.email, this.role, this.userId);
}
