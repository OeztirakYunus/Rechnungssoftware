import 'dart:convert';
import 'dart:io';
import 'dart:isolate';
import 'dart:ui';
import 'package:flutter_downloader/flutter_downloader.dart';
import 'package:http/http.dart' as http;
import 'package:demo5/document-scanner/scanner.dart';
import 'package:demo5/navbar.dart';
import 'package:demo5/network/networkHandler.dart';
import 'package:flutter/material.dart';
import 'package:path_provider/path_provider.dart';
import 'package:permission_handler/permission_handler.dart';

class Document extends StatefulWidget {
  const Document({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _DocumentsState();
}

class _DocumentsState extends State<Document> {
  final ReceivePort _port = ReceivePort();

  Future<bool> _onWillPop() async {
    return (await showDialog(
          context: context,
          builder: (context) => AlertDialog(
            title: const Text('App verlassen?'),
            content: const Text('Möchten Sie die App verlassen?'),
            actions: <Widget>[
              TextButton(
                onPressed: () => Navigator.of(context).pop(false),
                child: const Text('Nein'),
              ),
              TextButton(
                onPressed: () => exit(0),
                child: const Text('Ja'),
              ),
            ],
          ),
        )) ??
        false;
  }

  @override
  void initState() {
    super.initState();

    IsolateNameServer.registerPortWithName(
        _port.sendPort, 'downloader_send_port');
    _port.listen((dynamic data) {
      String id = data[0];
      DownloadTaskStatus status = data[1];
      int progress = data[2];
    });

    FlutterDownloader.registerCallback(downloadCallback);

    super.initState();
  }

  @override
  void dispose() {
    IsolateNameServer.removePortNameMapping('downloader_send_port');
    super.dispose();
  }

  @pragma('vm:entry-point')
  static void downloadCallback(
      String id, DownloadTaskStatus status, int progress) {
    final SendPort? send =
        IsolateNameServer.lookupPortByName('downloader_send_port');
    send!.send([id, status, progress]);
  }

  @override
  Widget build(BuildContext context) {
    return WillPopScope(
        onWillPop: _onWillPop,
        child: SafeArea(
            child: Scaffold(
          appBar: AppBar(
            title: const Text('Dokumente',
                style: TextStyle(
                    height: 1.00, fontSize: 25.00, color: Colors.white)),
            centerTitle: true,
          ),
          drawer: const NavBar(),
          body: FutureBuilder<List<Documents>>(
              future: getFiles(),
              builder: (context, AsyncSnapshot snapshot) {
                if (snapshot.hasData &&
                    snapshot.connectionState == ConnectionState.done) {
                  return ListView.builder(
                    itemCount: snapshot.data!.length,
                    itemBuilder: (context, index) {
                      AlertDialog alert;
                      return Card(
                        child: ListTile(
                          title: Text(
                            snapshot.data?[index].fileName,
                          ),
                          subtitle: Text(
                            "${snapshot.data?[index].creationTime}",
                          ),
                          trailing:
                              Row(mainAxisSize: MainAxisSize.min, children: [
                            SizedBox(
                                width: 50.0,
                                child: OutlinedButton(
                                  onPressed: () async {
                                    await downloadFile(snapshot.data?[index].id,
                                        snapshot.data?[index].fileName);
                                  },
                                  child: const Icon(Icons.download),
                                )),
                            SizedBox(
                                width: 50.0,
                                child: OutlinedButton(
                                  onPressed: () => {
                                    alert = AlertDialog(
                                      title: const Text("Achtung!"),
                                      content: const Text(
                                          "Möchten Sie wirklich dieses Dokument löschen?"),
                                      actions: [
                                        TextButton(
                                          child: const Text("Löschen"),
                                          onPressed: () async {
                                            await deleteFile(
                                                snapshot.data?[index].id);
                                            Navigator.of(context).pop();
                                            setState(() {});
                                          },
                                        ),
                                        TextButton(
                                          child: const Text("Abbrechen"),
                                          onPressed: () {
                                            Navigator.of(context).pop();
                                          },
                                        )
                                      ],
                                    ),
                                    showDialog(
                                      context: context,
                                      builder: (BuildContext context) {
                                        return alert;
                                      },
                                    )
                                  },
                                  child: const Icon(Icons.delete),
                                )),
                          ]),
                        ),
                        shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(10.0)),
                      );
                    },
                  );
                } else {
                  return const Center(child: CircularProgressIndicator());
                }
              }),
          floatingActionButton: FloatingActionButton(
            onPressed: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => const Scanner()),
              );
            },
            backgroundColor: Colors.redAccent[700],
            child: const Icon(Icons.scanner),
          ),
        )));
  }
}

Future<int> deleteFile(String id) async {
  String url = "https://backend.invoicer.at/api/Files/" + id;
  Uri uri = Uri.parse(url);

  String? token = await NetworkHandler.getToken();
  if (token!.isNotEmpty) {
    token = token.toString();
    final response = await http.delete(uri, headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer $token'
    });
    print(response.statusCode);
  }

  return 0;
}

Future downloadFile(String id, String fileName) async {
  var status = await Permission.storage.request();
  if (status.isGranted) {
    final baseStorage = await getExternalStorageDirectory();
    String url = "https://backend.invoicer.at/api/Files/byId/" + id;

    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();

    if (token!.isNotEmpty) {
      token = token.toString();

      await FlutterDownloader.enqueue(
        url: url,
        headers: {"Authorization": "Bearer $token"},
        fileName: fileName,
        savedDir: baseStorage!.path,
        showNotification: true,
        openFileFromNotification: true,
      );
    }
  }
}

Future<List<Documents>> getFiles() async {
  String url = "https://backend.invoicer.at/api/Files";
  Uri uri = Uri.parse(url);

  String? token = await NetworkHandler.getToken();
  List<Documents> documents = [];
  if (token!.isNotEmpty) {
    token = token.toString();

    final response = await http.get(uri, headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      "Authorization": "Bearer $token"
    });
    print(response.statusCode);
    List data = await json.decode(response.body) as List;

    if (response.statusCode == 200) {
      for (var element in data) {
        Map obj = element;
        String fileName = obj["fileName"];
        String creationTime = obj["creationTime"];
        String dateSplit = creationTime.split('T')[0];
        String creationDate =
            "${dateSplit.split('-')[2]}.${dateSplit.split('-')[1]}.${dateSplit.split('-')[0]}";

        String id = obj["id"];
        String companyId = obj["companyId"];
        Documents document = Documents(id, companyId, fileName, creationDate);
        documents.add(document);
      }
    }
  }
  return documents;
}

class Documents {
  final String id;
  final String companyId;
  final String fileName;
  final String creationTime;

  Documents(this.id, this.companyId, this.fileName, this.creationTime);
}
