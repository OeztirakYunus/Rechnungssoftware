// ignore_for_file: file_names

import 'dart:io';

import 'package:demo5/document-scanner/scanner.dart';
import 'package:flutter/material.dart';
import 'package:syncfusion_flutter_pdf/pdf.dart';
import 'package:open_file/open_file.dart';
import 'package:path_provider/path_provider.dart';

class DeliveryNote extends StatelessWidget {
  const DeliveryNote({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        appBar: AppBar(
          title: const Text('Lieferscheine',
              style: TextStyle(
                  height: 1.00, fontSize: 25.00, color: Colors.white)),
          centerTitle: true,
        ),
        body: Center(
          child: MaterialButton(
              child: const Text(
                "Lieferschein scannen",
                style: TextStyle(color: Colors.white),
              ),
              color: Colors.purple,
              onPressed: () {
                Navigator.push(context,
                    MaterialPageRoute(builder: (context) => const Scanner()));
              }),
        ),
      ),
    );
  }

  /*Future<void> _createPDF() async {
    PdfDocument document = PdfDocument();
    document.pages.add();

    List<int> bytes = document.save();
    document.dispose();
    saveAndLaunchFile(bytes, 'Output.pdf');
  }

  Future<void> saveAndLaunchFile(List<int> bytes, String filename) async {
    final path = (await getExternalStorageDirectory())!.path;
    final file = File('$path/$filename');
    await file.writeAsBytes(bytes, flush: true);
    OpenFile.open(file.path);
  }*/
}
