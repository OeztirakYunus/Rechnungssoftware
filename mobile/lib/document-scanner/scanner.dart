// ignore_for_file: file_names
import 'dart:io';
import 'package:path_provider/path_provider.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:demo5/document-scanner/display_delivery_note.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';
import 'package:document_scanner/document_scanner.dart';
import 'package:permission_handler/permission_handler.dart';

import '../network/networkHandler.dart';

class Scanner extends StatefulWidget {
  const Scanner({Key? key}) : super(key: key);

  @override
  _ScannerState createState() => _ScannerState();
}

class _ScannerState extends State<Scanner> {
  File? scannedDocument;
  Future<PermissionStatus>? cameraPermissionFuture;
  late TextEditingController documentName;

  @override
  void initState() {
    cameraPermissionFuture = Permission.camera.request();
    super.initState();
    documentName = TextEditingController();
  }

  @override
  Widget build(BuildContext context) {
    MaterialColor createMaterialColor(Color color) {
      List strengths = <double>[.05];
      final swatch = <int, Color>{};
      final int r = color.red, g = color.green, b = color.blue;

      for (int i = 1; i < 10; i++) {
        strengths.add(0.1 * i);
      }

      for (var strength in strengths) {
        final double ds = 0.5 - strength;
        swatch[(strength * 1000).round()] = Color.fromRGBO(
          r + ((ds < 0 ? r : (255 - r)) * ds).round(),
          g + ((ds < 0 ? g : (255 - g)) * ds).round(),
          b + ((ds < 0 ? b : (255 - b)) * ds).round(),
          1,
        );
      }
      return MaterialColor(color.value, swatch);
    }

    return MaterialApp(
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
          primarySwatch:
              createMaterialColor(const Color.fromARGB(255, 239, 5, 20))),
      home: Scaffold(
          appBar: AppBar(
            title: const Text('Dokumenten-Scanner'),
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
                                    noGrayScale: true,
                                    onDocumentScanned:
                                        (ScannedImage scannedImage) {
                                      setState(() {
                                        scannedDocument = scannedImage
                                            .getScannedDocumentAsFile();
                                      });
                                    },
                                  ),
                          ),
                        ],
                      ),
                      scannedDocument != null
                          ? Stack(children: [
                              Positioned(
                                bottom: 0,
                                left: 10,
                                right: 140,
                                child: ElevatedButton(
                                    child: const Text("Wiederholen"),
                                    onPressed: () {
                                      setState(() {
                                        scannedDocument = null;
                                      });
                                    }),
                              ),
                              Positioned(
                                  bottom: 0,
                                  right: 10,
                                  child: ElevatedButton(
                                    child: const Text("Speichern"),
                                    onPressed: () async {
                                      await openDialog();
                                    },
                                  ))
                            ])
                          : Container(),
                    ],
                  );
                } else {
                  return const Center(
                    child: Text("Kamera Zugriff abgelehnt"),
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

  Future openDialog() => showDialog(
      context: context,
      builder: (context) => AlertDialog(
            title: const Text("Dokumentname"),
            content: TextFormField(
              controller: documentName,
              validator: (value) {
                if (value == null || value.isEmpty) {
                  return "Bitte Dokumentname eingeben!";
                } else {
                  return null;
                }
              },
              autofocus: true,
              decoration: InputDecoration(
                  border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(100.0)),
                  hintText: 'Dokumentname eingeben',
                  hintStyle: const TextStyle(fontSize: 20.00)),
              style: const TextStyle(fontSize: 20.00),
            ),
            actions: [
              TextButton(
                  onPressed: () async {
                    await addFile(scannedDocument!, documentName.text);
                    Navigator.of(context).pushAndRemoveUntil(
                      MaterialPageRoute(builder: (context) => const Document()),
                      (route) => false,
                    );
                  },
                  child: const Text("Speichern")),
              TextButton(
                onPressed: () => Navigator.of(context).pop(),
                child: const Text('Abbrechen'),
              ),
            ],
          ));

  Future<File> getPdf(File screenShot, String fileName) async {
    pw.Document pdf = pw.Document();
    final image = pw.MemoryImage(screenShot.readAsBytesSync());
    pdf.addPage(pw.Page(build: (pw.Context context) {
      return pw.FullPage(
        ignoreMargins: true,
        child: pw.Image(image, fit: pw.BoxFit.fill),
      ); // Center
    }));
    final output = await getTemporaryDirectory();
    File pdfFile = File('${output.path}/$fileName.pdf');
    await pdfFile.writeAsBytes(await pdf.save());
    return pdfFile;
  }

  Future<int> addFile(File scannedDocument, String fileName) async {
    String url = "https://backend.invoicer.at/api/Files";
    Uri uri = Uri.parse(url);
    String? token = await NetworkHandler.getToken();
    int responseCode = 0;
    if (token!.isNotEmpty) {
      token = token.toString();
      File pdfFile = await getPdf(scannedDocument, fileName);

      Map<String, String> headers = {'Authorization': 'Bearer $token'};
      var request = http.MultipartRequest('POST', uri);
      request.headers.addAll(headers);
      request.files.add(http.MultipartFile.fromBytes(
          'file', File(pdfFile.path).readAsBytesSync(),
          filename: '$fileName.pdf'));
      var response = await request.send();
      responseCode = response.statusCode;
      print(response.statusCode);
    }
    return responseCode;
  }
}
