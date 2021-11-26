// ignore_for_file: file_names
import 'dart:io';

import 'package:demo5/document-scanner/documents_list.dart';
import 'package:flutter/material.dart';
import 'package:document_scanner/document_scanner.dart';
import 'package:permission_handler/permission_handler.dart';

class Scanner extends StatefulWidget {
  const Scanner({Key? key}) : super(key: key);

  @override
  _ScannerState createState() => _ScannerState();
}

class _ScannerState extends State<Scanner> {
  File? scannedDocument;
  Future<PermissionStatus>? cameraPermissionFuture;

  @override
  void initState() {
    cameraPermissionFuture = Permission.camera.request();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
          appBar: AppBar(
            title: const Text('Lieferschein-Scanner'),
          ),
          body: FutureBuilder<PermissionStatus>(
            future: cameraPermissionFuture,
            builder: (BuildContext context,
                AsyncSnapshot<PermissionStatus> snapshot) {
              if (snapshot.connectionState == ConnectionState.done) {
                if (snapshot.data!.isGranted) {
                  return Stack(
                    children: <Widget>[
                      Column(
                        children: <Widget>[
                          Expanded(
                            child: scannedDocument != null
                                ? Image(
                                    image: FileImage(scannedDocument!),
                                  )
                                : DocumentScanner(
                                    // documentAnimation: false,
                                    noGrayScale: true,
                                    onDocumentScanned:
                                        (ScannedImage scannedImage) {
                                      print("document : " +
                                          scannedImage.croppedImage!);

                                      setState(() {
                                        scannedDocument = scannedImage
                                            .getScannedDocumentAsFile();
                                        // imageLocation = image;
                                      });
                                    },
                                  ),
                          ),
                        ],
                      ),
                      scannedDocument != null
                          ? Stack(children: [
                              Positioned(
                                bottom: 20,
                                left: 10,
                                right: 0,
                                child: ElevatedButton(
                                    child: const Text("Nochmal scannen"),
                                    onPressed: () {
                                      setState(() {
                                        scannedDocument = null;
                                      });
                                    }),
                              ),
                              Positioned(
                                  bottom: 20,
                                  right: 10,
                                  child: ElevatedButton(
                                    child: const Text("Speichern"),
                                    onPressed: () {
                                      Navigator.of(context).push(
                                          MaterialPageRoute(
                                              builder: (context) => DocumentList(
                                                  scannedDocument:
                                                      scannedDocument)));
                                    },
                                  ))
                            ])
                          : Container(),
                    ],
                  );
                } else {
                  return const Center(
                    child: Text("camera permission denied"),
                  );
                }
              } else {
                return const Center(
                  child: CircularProgressIndicator(),
                );
              }
            },
          )),
    );
  }
}
