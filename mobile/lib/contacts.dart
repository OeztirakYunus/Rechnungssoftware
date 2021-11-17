import 'package:demo5/navbar.dart';
import 'package:flutter/material.dart';

class Contacts extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        appBar: AppBar(
        title: const Text('Kontakte',
            style:
                TextStyle(height: 1.00, fontSize: 25.00, color: Colors.white)),
        centerTitle: true,
        
      ),
      drawer: NavBar(),
      ),
      
    );
  }
}
