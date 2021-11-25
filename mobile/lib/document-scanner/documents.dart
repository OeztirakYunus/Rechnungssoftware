import 'dart:io';

import 'package:flutter/material.dart';

class Documents extends StatefulWidget {
  final File? scannedDocument;
  const Documents({Key? key, this.scannedDocument}) : super(key: key);

  @override
  _DocumentsState createState() => _DocumentsState();
}

class _DocumentsState extends State<Documents> {
  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Gescannte Lieferscheine'),
      ),
      body: ListView(
        children: [
          Card(
            child: Row(
              children: [
                const Icon(Icons.image),
                Text(widget.scannedDocument.toString()),
                const Icon(Icons.edit)
              ],
            ),
          )
        ],
      ),
    ));
  }
}
