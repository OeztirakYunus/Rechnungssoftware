import 'package:demo5/contact/addContact.dart';
import 'package:demo5/navbar.dart';
import 'package:flutter/material.dart';

class Contacts extends StatelessWidget {
  const Contacts({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
          appBar: AppBar(
            title: const Text('Kontakte',
                style: TextStyle(
                    height: 1.00, fontSize: 25.00, color: Colors.white)),
            centerTitle: true,
          ),
          drawer: const NavBar(),
          body: SingleChildScrollView(
            child: MaterialButton(
              child: const Text(
                "Kontakt hinzufügen",
                style: TextStyle(color: Colors.white),
              ),
              color: Colors.purple,
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => const AddContact()),
                );
              },
            ),
          )),
    );
  }
}
