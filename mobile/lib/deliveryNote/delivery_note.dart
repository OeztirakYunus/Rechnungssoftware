import 'dart:convert';

import 'package:demo5/NavBar.dart';
import 'package:demo5/deliveryNote/addDeliveryNote.dart';
import 'package:demo5/document-scanner/scanner.dart';
import 'package:demo5/network/networkHandler.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

class DeliveryNote extends StatefulWidget {
  const DeliveryNote({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _DeliveryNotesState();
}

class _DeliveryNotesState extends State<DeliveryNote> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Lieferscheine',
            style:
                TextStyle(height: 1.00, fontSize: 25.00, color: Colors.white)),
        centerTitle: true,
      ),
      drawer: const NavBar(),
      body: FutureBuilder<List<DeliveryNotes>>(
          future: getDeliveryNotes(),
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
                        snapshot.data?[index].deliveryNoteNumber,
                      ),
                      subtitle: Text(
                        "Datum: ${snapshot.data?[index].deliveryNoteDate}  Status: ${snapshot.data?[index].status}",
                      ),
                      trailing: Row(mainAxisSize: MainAxisSize.min, children: [
                        OutlinedButton(
                          onPressed: () => {
                            alert = AlertDialog(
                              title: const Text("Achtung!"),
                              content: const Text(
                                  "Möchten Sie wirklich dieses Lieferschein löschen?"),
                              actions: [
                                TextButton(
                                  child: const Text("Löschen"),
                                  onPressed: () async {
                                    await deleteDeliveryNote(
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
                        ),
                        /*OutlinedButton(
                          onPressed: () => {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                  builder: (context) => EditProduct(
                                        productName:
                                            snapshot.data?[index].productName,
                                        description:
                                            snapshot.data?[index].description,
                                        articleNumber:
                                            snapshot.data?[index].articleNumber,
                                        sellingPriceNet: snapshot
                                            .data?[index].sellingPriceNet,
                                        category:
                                            snapshot.data?[index].category,
                                        unitOld: snapshot.data?[index].unit,
                                        productId:
                                            snapshot.data?[index].productId,
                                        companyId:
                                            snapshot.data?[index].companyId,
                                      )),
                            )
                          },
                          child: Icon(Icons.edit),
                        ),*/
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
            MaterialPageRoute(builder: (context) => const AddDeliveryNote()),
          );
        },
        backgroundColor: Colors.redAccent[700],
        child: const Icon(Icons.add),
      ),
    ));
  }

  Future<List<DeliveryNotes>> getDeliveryNotes() async {
    String url = "https://backend.invoicer.at/api/DeliveryNotes";
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    List<DeliveryNotes> deliveryNotes = [];
    if (token!.isNotEmpty) {
      token = token.toString();

      final response = await http.get(uri, headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        "Authorization": "Bearer $token"
      });
      print(response.statusCode);

      List data = await json.decode(response.body) as List;
      for (var element in data) {
        Map obj = element;
        String deliveryNoteNumber = obj["deliveryNoteNumber"];
        String id = obj["id"];
        String deliveryNoteDate = obj["deliveryNoteDate"];
        String status = obj["status"];
        DeliveryNotes deliveryNote =
            DeliveryNotes(id, deliveryNoteNumber, deliveryNoteDate, status);
        deliveryNotes.add(deliveryNote);
      }
    }

    return deliveryNotes;
  }

  Future<int> deleteDeliveryNote(String id) async {
    String url =
        "https://backend.invoicer.at/api/Companies/delete-delivery-note/" + id;
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    if (token!.isNotEmpty) {
      token = token.toString();
      final response = await http.put(uri, headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'Bearer $token'
      });
      print(response.statusCode);
    }

    return 0;
  }
}

class DeliveryNotes {
  final String id;
  final String deliveryNoteNumber;
  final String deliveryNoteDate;
  final String status;

  DeliveryNotes(
      this.id, this.deliveryNoteNumber, this.deliveryNoteDate, this.status);
}


/*appBar: AppBar(
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
        ),*/