import 'dart:convert';
import 'dart:isolate';
import 'dart:ui';

import 'package:demo5/offer/addOffer.dart';
import 'package:demo5/orderConfirmation/order_confirmation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_downloader/flutter_downloader.dart';
import 'package:path_provider/path_provider.dart';
import 'package:permission_handler/permission_handler.dart';
import 'package:http/http.dart' as http;
import '../NavBar.dart';
import '../contact/contacts.dart';
import '../network/networkHandler.dart';
import '../products/product.dart';
import '../user/user.dart';
import 'editOffer.dart';

class Offer extends StatefulWidget {
  const Offer({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _OffersState();
}

class _OffersState extends State<Offer> {
  final ReceivePort _port = ReceivePort();
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
    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Angebote',
            style:
                TextStyle(height: 1.00, fontSize: 25.00, color: Colors.white)),
        centerTitle: true,
      ),
      drawer: const NavBar(),
      body: FutureBuilder<List<Offers>>(
          future: getOffers(),
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
                        snapshot.data?[index].offerNum,
                      ),
                      subtitle: Text(
                        "${snapshot.data?[index].status}",
                      ),
                      trailing: Row(mainAxisSize: MainAxisSize.min, children: [
                        SizedBox(
                          width: 50.0,
                          child: OutlinedButton(
                            onPressed: () async {
                              String orderConfirmationNumber =
                                  await offerToOrderConfirmation(
                                      snapshot.data?[index].id);
                              alert = AlertDialog(
                                title:
                                    const Text("Auftragsbestätigung erzeugt!"),
                                content: Text(
                                    "Die Auftragsbestätigung wurde erfolgreich erzeugt. Die Angebotsnummer lautet $orderConfirmationNumber"),
                                actions: [
                                  TextButton(
                                    child: const Text("Ok"),
                                    onPressed: () async {
                                      Navigator.of(context).pop();
                                    },
                                  ),
                                ],
                              );
                              showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return alert;
                                },
                              );
                            },
                            child: Image.asset(
                              "lib/assets/order.png",
                              height: 35,
                            ),
                          ),
                        ),
                        SizedBox(
                            width: 50.0,
                            child: OutlinedButton(
                              onPressed: () async {
                                await getAsWord(snapshot.data?[index].id,
                                    snapshot.data?[index].offerNum);
                              },
                              child: Image.asset(
                                "lib/assets/word.png",
                                height: 35,
                              ),
                            )),
                        SizedBox(
                            width: 50.0,
                            child: OutlinedButton(
                              onPressed: () async {
                                await getAsPdf(snapshot.data?[index].id,
                                    snapshot.data?[index].offerNum);
                              },
                              child: Image.asset(
                                "lib/assets/pdf.png",
                                height: 25,
                              ),
                            )),
                        SizedBox(
                            width: 50.0,
                            child: OutlinedButton(
                              onPressed: () => {
                                alert = AlertDialog(
                                  title: const Text("Achtung!"),
                                  content: const Text(
                                      "Möchten Sie wirklich dieses Angebot löschen?"),
                                  actions: [
                                    TextButton(
                                      child: const Text("Löschen"),
                                      onPressed: () async {
                                        await deleteOffer(
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
                        SizedBox(
                            width: 50.0,
                            child: OutlinedButton(
                              onPressed: () => {
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                      builder: (context) => EditOffer(
                                          id: snapshot.data?[index].id,
                                          documentInformationId: snapshot
                                              .data?[index]
                                              .documentInformationId,
                                          offerNum:
                                              snapshot.data?[index].offerNum,
                                          offerDate:
                                              snapshot.data?[index].offerDate,
                                          validUntil:
                                              snapshot.data?[index].validUntil,
                                          status: snapshot.data?[index].status,
                                          subject:
                                              snapshot.data?[index].subject,
                                          headerText:
                                              snapshot.data?[index].headerText,
                                          flowText:
                                              snapshot.data?[index].flowText,
                                          totalDiscount: snapshot
                                              .data?[index].totalDiscount,
                                          typeOfDiscount: snapshot
                                              .data?[index].typeOfDiscount,
                                          tax: snapshot.data?[index].tax,
                                          clientId:
                                              snapshot.data?[index].clientId,
                                          contactPersonId: snapshot
                                              .data?[index].contactPersonId,
                                          quantityPosition: snapshot
                                              .data?[index].quantityPosition,
                                          discountPosition: snapshot
                                              .data?[index].discountPosition,
                                          typeOfDiscountPosition: snapshot
                                              .data?[index]
                                              .typeOfDiscountPosition,
                                          productPosition: snapshot
                                              .data?[index].productPosition,
                                          products:
                                              snapshot.data?[index].products)),
                                )
                              },
                              child: const Icon(Icons.edit),
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
            MaterialPageRoute(builder: (context) => const AddOffer()),
          );
        },
        backgroundColor: Colors.redAccent[700],
        child: const Icon(Icons.add),
      ),
    ));
  }

  Future<String> offerToOrderConfirmation(String offerId) async {
    String url =
        "https://backend.invoicer.at/api/Offers/offer-to-order-confirmation/" +
            offerId;

    Uri uri = Uri.parse(url);
    String? token = await NetworkHandler.getToken();
    String orderConfirmationNumber = "";
    if (token!.isNotEmpty) {
      token = token.toString();
      final response = await http.post(uri, headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        "Authorization": "Bearer $token"
      });
      print(response.statusCode);

      Map<String, dynamic> data = await json.decode(response.body);

      if (response.statusCode == 200) {
        data.forEach((key, value) {
          if (key == "orderConfirmationNumber") {
            orderConfirmationNumber = value;
          }
        });
      }
    }
    return orderConfirmationNumber;
  }

  Future<int> deleteOffer(String id) async {
    String url = "https://backend.invoicer.at/api/Companies/delete-offer/" + id;
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

  Future getAsPdf(String offerId, String fileName) async {
    var status = await Permission.storage.request();
    if (status.isGranted) {
      final baseStorage = await getExternalStorageDirectory();
      String url =
          "https://backend.invoicer.at/api/Offers/get-as-pdf/" + offerId;

      Uri uri = Uri.parse(url);

      String? token = await NetworkHandler.getToken();

      if (token!.isNotEmpty) {
        token = token.toString();

        await FlutterDownloader.enqueue(
          url: url,
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            "Authorization": "Bearer $token"
          },
          fileName: fileName + ".pdf",
          savedDir: baseStorage!.path,
          showNotification: true,
          openFileFromNotification: true,
        );
      }
    }
  }

  Future getAsWord(String offerId, String fileName) async {
    var status = await Permission.storage.request();
    if (status.isGranted) {
      final baseStorage = await getExternalStorageDirectory();
      String url =
          "https://backend.invoicer.at/api/Offers/get-as-word/" + offerId;

      String? token = await NetworkHandler.getToken();

      if (token!.isNotEmpty) {
        token = token.toString();

        await FlutterDownloader.enqueue(
          url: url,
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            "Authorization": "Bearer $token"
          },
          fileName: fileName + ".docx",
          savedDir: baseStorage!.path,
          showNotification: true,
          openFileFromNotification: true,
        );
      }
    }
  }

  Future<List<Offers>> getOffers() async {
    String url = "https://backend.invoicer.at/api/Offers";
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    List<Offers> offers = [];
    if (token!.isNotEmpty) {
      token = token.toString();

      final response = await http.get(uri, headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        "Authorization": "Bearer $token"
      });
      print(response.statusCode);

      List data = await json.decode(response.body) as List;

      List<Contact> contacts = await NetworkHandler.getContacts();
      List<User> users = await NetworkHandler.getUsers();
      List<Products> products = await NetworkHandler.getProducts();

      String clientIdPos = "";
      String contactIdPos = "";

      if (response.statusCode == 200) {
        for (var element in data) {
          List<String> quantityPosition = [];
          List<String> discountPosition = [];
          List<String> typeOfDiscountPosition = [];
          List<String> productIdPosition = [];
          List<String> productPosition = [];

          Map obj = element;
          String id = obj["id"];
          String documentInformationId = obj["documentInformationId"];
          String offerNumber = obj["offerNumber"];
          String offerDate = obj["offerDate"];
          String validUntil = obj["validUntil"];
          String status = obj["status"];
          if (status.isNotEmpty && status == "OPEN") {
            status = "geöffnet";
          } else if (status.isNotEmpty && status == "CLOSED") {
            status = "geschlossen";
          }
          String subject = obj["subject"];
          String headerText = obj["headerText"];
          String flowText = obj["flowText"];
          String totalDiscount =
              obj["documentInformation"]["totalDiscount"].toString();
          String typeOfDiscount =
              obj["documentInformation"]["typeOfDiscount"].toString();
          if (typeOfDiscount.isNotEmpty && typeOfDiscount == "Percent") {
            typeOfDiscount = "Prozent";
          }
          String tax = obj["documentInformation"]["tax"].toString();
          String clientId = obj["documentInformation"]["clientId"];
          String contactPersonId =
              obj["documentInformation"]["contactPersonId"];
          for (var position in obj["documentInformation"]["positions"]) {
            quantityPosition.add(position["quantity"].toString());
            discountPosition.add(position["discount"].toString());
            typeOfDiscountPosition.add(position["typeOfDiscount"]);
            productIdPosition.add(position["productId"]);
          }
          for (var id in productIdPosition) {
            for (var product in products) {
              if (id == product.productId) {
                productPosition.add(product.productName);
              }
            }
          }
          for (var user in users) {
            if (user.userId == contactPersonId) {
              contactIdPos = "${user.firstName} ${user.lastName}";
            }
          }
          for (var contact in contacts) {
            if (contact.contactId == clientId) {
              clientIdPos = "${contact.firstName} ${contact.lastName}";
            }
          }
          Offers offer = Offers(
              id,
              documentInformationId,
              offerNumber,
              offerDate,
              validUntil,
              status,
              subject,
              headerText,
              flowText,
              totalDiscount,
              typeOfDiscount,
              tax,
              clientIdPos,
              contactIdPos,
              quantityPosition,
              discountPosition,
              typeOfDiscountPosition,
              productPosition,
              products);
          offers.add(offer);
        }
      }
    }

    return offers;
  }
}

class Offers {
  final String id;
  final String documentInformationId;
  final String offerNum;
  final String offerDate;
  final String validUntil;
  final String status;
  final String subject;
  final String headerText;
  final String flowText;
  final String totalDiscount;
  final String typeOfDiscount;
  final String tax;
  final String clientId;
  final String contactPersonId;
  final List<String> quantityPosition;
  final List<String> discountPosition;
  final List<String> typeOfDiscountPosition;
  final List<String> productPosition;
  final List<Products> products;

  Offers(
      this.id,
      this.documentInformationId,
      this.offerNum,
      this.offerDate,
      this.validUntil,
      this.status,
      this.subject,
      this.headerText,
      this.flowText,
      this.totalDiscount,
      this.typeOfDiscount,
      this.tax,
      this.clientId,
      this.contactPersonId,
      this.quantityPosition,
      this.discountPosition,
      this.typeOfDiscountPosition,
      this.productPosition,
      this.products);
}
