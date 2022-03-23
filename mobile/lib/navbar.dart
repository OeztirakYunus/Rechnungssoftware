// ignore_for_file: avoid_returning_null_for_void

import 'package:demo5/companies/companies.dart';
import 'package:demo5/contact/contacts.dart';
import 'package:demo5/invoice/invoice.dart';
import 'package:demo5/products/categoryList.dart';
import 'deliveryNote/delivery_note.dart';
import 'package:demo5/products/product.dart';
import 'package:flutter/material.dart';

class NavBar extends StatelessWidget {
  const NavBar({Key? key}) : super(key: key);

  void selectedItem(BuildContext context, int index) {
    Navigator.of(context).pop();
    switch (index) {
      case 0:
        Navigator.of(context)
            .push(MaterialPageRoute(builder: (context) => const Categories()));
        return;
      case 1:
        Navigator.of(context)
            .push(MaterialPageRoute(builder: (context) => const Contacts()));
        return;
      case 2:
        Navigator.of(context)
            .push(MaterialPageRoute(builder: (context) => const Companies()));
        return;
      case 3:
        Navigator.of(context).push(
            MaterialPageRoute(builder: (context) => const DeliveryNote()));
        return;
      case 4:
        Navigator.of(context)
            .push(MaterialPageRoute(builder: (context) => const Invoice()));
        return;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: Material(
          color: const Color.fromARGB(255, 190, 47, 252),
          child: ListView(
            padding: const EdgeInsets.symmetric(horizontal: 20),
            children: <Widget>[
              const SizedBox(height: 16),
              ListTile(
                leading: Image.asset(
                  "lib/assets/product.png",
                  scale: 1.0,
                  height: 38.0,
                  width: 38.0,
                ),
                title: const Text(
                  'Produkte',
                  style: TextStyle(color: Colors.white, fontSize: 18),
                ),
                onTap: () => selectedItem(context, 0),
              ),
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
                onTap: () => selectedItem(context, 1),
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
                onTap: () => selectedItem(context, 2),
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
                onTap: () => selectedItem(context, 3),
              ),
              const SizedBox(height: 16),
              ListTile(
                leading: Image.asset(
                  "lib/assets/invoice.png",
                  scale: 2.0,
                  height: 35.0,
                  width: 35.0,
                ),
                title: const Text('Rechnungen',
                    style: TextStyle(color: Colors.white, fontSize: 18)),
                onTap: () => selectedItem(context, 4),
              ),
              const SizedBox(height: 16),
              ListTile(
                leading: Image.asset(
                  "lib/assets/document.png",
                  scale: 2.0,
                  height: 35.0,
                  width: 35.0,
                ),
                title: const Text('Dokumente',
                    style: TextStyle(color: Colors.white, fontSize: 18)),
                onTap: () => selectedItem(context, 5),
              ),
              const SizedBox(height: 16),
              ListTile(
                leading: Image.asset(
                  "lib/assets/order.png",
                  scale: 2.0,
                  height: 35.0,
                  width: 35.0,
                ),
                title: const Text('Auftrags-\nbestätigungen',
                    style: TextStyle(
                        color: Colors.white, fontSize: 18, height: 1.40)),
                onTap: () => selectedItem(context, 6),
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
