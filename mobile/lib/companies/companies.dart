import 'package:demo5/companies/addCompany.dart';
import 'package:demo5/navbar.dart';
import 'package:flutter/material.dart';

class Companies extends StatefulWidget {
  const Companies({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _CompaniesState();
}

class _CompaniesState extends State<Companies> {
  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        appBar: AppBar(
          title: const Text('Unternehmen',
              style: TextStyle(
                  height: 1.00, fontSize: 25.00, color: Colors.white)),
          centerTitle: true,
        ),
        drawer: const NavBar(),
      ),
    );
  }
}
