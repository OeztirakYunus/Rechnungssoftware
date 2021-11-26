import 'dart:io';

import 'package:demo5/document-scanner/display_document.dart';
import 'package:flutter/material.dart';

class DocumentList extends StatefulWidget {
  final File? scannedDocument;
  const DocumentList({Key? key, this.scannedDocument}) : super(key: key);

  @override
  _DocumentListState createState() => _DocumentListState();
}

class _DocumentListState extends State<DocumentList> {
  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Gescannte Lieferscheine'),
      ),
      body: ListView(
        children: [
          ListTile(
              leading: const Icon(Icons.image),
              title: Text(widget.scannedDocument!.path.split('/').last),
              onTap: () => {
                    Navigator.of(context).push(MaterialPageRoute(
                        builder: (context) =>
                            Document(scannedDocument: widget.scannedDocument)))
                  },
              trailing: const Icon(Icons.edit))
        ],
      ),
    ));
  }
}
