import 'dart:io';

import 'package:flutter/material.dart';

class Document extends StatelessWidget {
  final File? scannedDocument;
  const Document({Key? key, this.scannedDocument}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    var fileName = scannedDocument!.path.split('/').last;
    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: Text(fileName),
      ),
      body: Image(image: FileImage(scannedDocument!)),
    ));
  }
}
