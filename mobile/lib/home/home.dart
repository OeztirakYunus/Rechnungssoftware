import 'package:flutter/material.dart';
import '../size_config.dart';

class Home extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SingleChildScrollView(
        child: Column(
          children: [
            Row(children: [
              Container(
                decoration: BoxDecoration(
                    color: Colors.grey.shade50,
                    borderRadius: BorderRadius.circular(100)),
              )
            ]),
          ],
        ),
      ),
    );
  }
}
