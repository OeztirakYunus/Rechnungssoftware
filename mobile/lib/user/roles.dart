import 'dart:io';

import 'package:demo5/network/networkHandler.dart';
import 'package:demo5/user/user.dart';
import 'package:flutter/material.dart';

import '../NavBar.dart';

class UserRole extends StatefulWidget {
  const UserRole({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _UserRolesState();
}

class _UserRolesState extends State<UserRole> {
  final Role _roles = Role("");

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
    _roles.initializeRoles();
    return WillPopScope(
        onWillPop: _onWillPop,
        child: SafeArea(
            child: Scaffold(
          appBar: AppBar(
            title: const Text('Benutzerrollen',
                style: TextStyle(
                    height: 1.00, fontSize: 25.00, color: Colors.white)),
            centerTitle: true,
          ),
          drawer: const NavBar(),
          body: ListView.builder(
              itemBuilder: _itemBuilder,
              itemCount: _roles.getRolesListLength()),
        )));
  }

  Widget _itemBuilder(BuildContext context, int index) {
    return InkWell(
      onTap: () {
        String role = NetworkHandler.getUserRole();
        if (index == 0) {
          Navigator.push(
            context,
            MaterialPageRoute(
                builder: (context) => Users(
                      roleIndex: index,
                      role: role,
                    )),
          );
        } else {
          Navigator.push(
            context,
            MaterialPageRoute(
                builder: (context) => Users(
                      roleIndex: index,
                      role: role,
                    )),
          );
        }
      },
      child: Card(
        child: ListTile(
          leading: Text(_roles.getRoleName(index)),
        ),
        shape:
            RoundedRectangleBorder(borderRadius: BorderRadius.circular(10.0)),
      ),
    );
  }
}

class Role {
  Role(this.role);

  final String role;

  List<Role> rolesList = [];

  void initializeRoles() {
    rolesList.add(Role("Admin"));
    rolesList.add(Role("User"));
  }

  String getRoleName(int index) {
    return rolesList[index].role;
  }

  int getRolesListLength() {
    return rolesList.length;
  }
}
