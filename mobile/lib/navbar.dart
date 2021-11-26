// ignore_for_file: avoid_returning_null_for_void

import 'package:demo5/contacts.dart';
import 'package:demo5/deliveryNote.dart';
import 'package:flutter/material.dart';

class NavBar extends StatelessWidget {
  const NavBar({Key? key}) : super(key: key);

  /*Widget createMenuItem({required Image icon, required Text title_}) {
    final color = Colors.white;

    return ListTile(leading: icon, title: title_);
  }*/

  void selectedItem(BuildContext context, int index) {
    Navigator.of(context).pop();
    switch (index) {
      case 0:
        Navigator.of(context)
            .push(MaterialPageRoute(builder: (context) => Contacts()));
        return;
      case 2:
        Navigator.of(context)
            .push(MaterialPageRoute(builder: (context) => DeliveryNote()));
        return;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: Material(
          color: const Color.fromRGBO(142, 68, 173, 1),
          child: ListView(
            padding: const EdgeInsets.symmetric(horizontal: 20),
            children: <Widget>[
              const SizedBox(height: 16),
              ListTile(
                leading: Image.asset(
                  "lib/assets/contact.png",
                  scale: 1.0,
                  height: 38.0,
                  width: 38.0,
                ),
                title: const Text(
                  'Kontakte',
                  style: TextStyle(color: Colors.white, fontSize: 18),
                ),
                onTap: () => selectedItem(context, 0),
              ),
              const SizedBox(height: 16),
              ListTile(
                leading: Image.asset(
                  "lib/assets/company.png",
                  scale: 1.0,
                  height: 35.0,
                  width: 35.0,
                ),
                title: const Text('Unternehmen',
                    style: TextStyle(color: Colors.white, fontSize: 18)),
                onTap: () => null,
              ),
              const SizedBox(height: 16),
              ListTile(
                leading: Image.asset(
                  "lib/assets/delivery_report.png",
                  scale: 2.0,
                  height: 35.0,
                  width: 35.0,
                ),
                title: const Text('Lieferscheine',
                    style: TextStyle(color: Colors.white, fontSize: 18)),
                onTap: () => selectedItem(context, 2),
              ),
              const SizedBox(height: 16),
              ListTile(
                leading: Image.asset(
                  "lib/assets/user.png",
                  scale: 2.0,
                  height: 38.0,
                  width: 38.0,
                ),
                title: const Text('Benutzer',
                    style: TextStyle(color: Colors.white, fontSize: 18)),
                onTap: () => null,
              ),
              const SizedBox(height: 16),
              ListTile(
                leading: Image.asset(
                  "lib/assets/settings.png",
                  scale: 2.0,
                  height: 35.0,
                  width: 35.0,
                ),
                title: const Text('Einstellungen',
                    style: TextStyle(color: Colors.white, fontSize: 18)),
                onTap: () => null,
              )
            ],
          )),
    );
  }
}
