import 'dart:convert';
import 'package:demo5/contact/contacts.dart';
import 'package:demo5/network/networkHandler.dart';
import 'package:demo5/products/product.dart';
import 'package:demo5/user/user.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:select_form_field/select_form_field.dart';
import '../deliveryNote/dynamicWidget.dart';
import 'invoice.dart';

class AddInvoice extends StatefulWidget {
  const AddInvoice({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _AddInvoicesState();
}

class _AddInvoicesState extends State<AddInvoice> {
  final formGlobalKey = GlobalKey<FormState>();
  List<GlobalKey<FormState>> keys = [];
  String user = "";
  String user2 = "";
  String contact = "";
  String contact2 = "";
  int count = 0;
  bool productsIsEmpty = false;
  List<DynamicWidget> dynamicList = [];
  List<String> productPosition = [];
  List<String> quantityPosition = [];
  List<String> discountPosition = [];
  List<String> typeOfDiscountPosition = [];

  TextEditingController invoiceNumber = TextEditingController();
  TextEditingController invoiceDate = TextEditingController();
  TextEditingController paymentTerm = TextEditingController();
  TextEditingController headerText = TextEditingController();
  TextEditingController flowText = TextEditingController();
  TextEditingController subject = TextEditingController();

  TextEditingController typeOfDiscount = TextEditingController();
  TextEditingController totalDiscount = TextEditingController();
  TextEditingController tax = TextEditingController();

  DateTime date = DateTime(2022, 10, 6);
  DateTime payDate = DateTime(2022, 10, 18);

  @override
  Widget build(BuildContext context) {
    List<Map<String, dynamic>> _typeOfDiscount = [
      {'value': 0, 'label': 'Euro'},
      {'value': 1, 'label': 'Prozent'}
    ];

    Widget dynamicTextField = SizedBox(
      width: 400,
      child: ListView.builder(
        itemCount: dynamicList.length,
        shrinkWrap: true,
        itemBuilder: (_, index) => dynamicList[index],
      ),
    );

    return SafeArea(
        child: Scaffold(
            appBar: AppBar(
              title: const Text('Rechnung anlegen',
                  style: TextStyle(
                      height: 1.00, fontSize: 25.00, color: Colors.white)),
              centerTitle: true,
            ),
            body: SingleChildScrollView(
                child: Column(children: <Widget>[
              Container(
                  padding: const EdgeInsets.all(10),
                  child: Form(
                      key: formGlobalKey,
                      child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            const Align(
                              alignment: Alignment(-0.95, 1),
                              child: Text('Rechnungsnummer',
                                  style: TextStyle(fontSize: 20.00)),
                            ),
                            TextFormField(
                              controller: invoiceNumber,
                              autofocus: false,
                              decoration: InputDecoration(
                                  border: OutlineInputBorder(
                                      borderRadius:
                                          BorderRadius.circular(100.0)),
                                  hintText: 'Rechnungsnummer eingeben',
                                  hintStyle: const TextStyle(fontSize: 20.00)),
                              style: const TextStyle(fontSize: 20.00),
                            ),
                            const SizedBox(
                              height: 25.00,
                            ),
                            const Align(
                              alignment: Alignment(-0.95, 1),
                              child: Text(
                                'Datum *',
                                style: TextStyle(fontSize: 20.00),
                              ),
                            ),
                            TextFormField(
                              readOnly: true,
                              controller: invoiceDate,
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return "Bitte Datum auswählen!";
                                } else {
                                  return null;
                                }
                              },
                              decoration: InputDecoration(
                                  border: OutlineInputBorder(
                                      borderRadius:
                                          BorderRadius.circular(100.0)),
                                  hintText: "Datum auswählen!",
                                  hintStyle: const TextStyle(fontSize: 20.00)),
                              style: const TextStyle(fontSize: 20.00),
                            ),
                            ElevatedButton(
                                onPressed: () async {
                                  DateTime? newDate = await showDatePicker(
                                      cancelText: "Abbrechen",
                                      context: context,
                                      initialDate: DateTime.now(),
                                      currentDate: date,
                                      firstDate: DateTime(2000),
                                      lastDate: DateTime(2100));
                                  if (newDate == null) return;

                                  setState(() {
                                    date = newDate;
                                    invoiceDate.text =
                                        '${date.day}.${date.month}.${date.year}';
                                  });
                                },
                                child: const Text('Datum auswählen')),
                            const SizedBox(
                              height: 25.00,
                            ),
                            TextFormField(
                              readOnly: true,
                              controller: paymentTerm,
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return "Bitte Zahlungsfrist auswählen!";
                                } else {
                                  return null;
                                }
                              },
                              decoration: InputDecoration(
                                  border: OutlineInputBorder(
                                      borderRadius:
                                          BorderRadius.circular(100.0)),
                                  hintText: "Zahlungsfrist auswählen!",
                                  hintStyle: const TextStyle(fontSize: 20.00)),
                              style: const TextStyle(fontSize: 20.00),
                            ),
                            ElevatedButton(
                                onPressed: () async {
                                  DateTime? newDate = await showDatePicker(
                                      cancelText: "Abbrechen",
                                      context: context,
                                      initialDate: DateTime.now(),
                                      currentDate: payDate,
                                      firstDate: DateTime(2000),
                                      lastDate: DateTime(2100));
                                  if (newDate == null) return;

                                  setState(() {
                                    payDate = newDate;
                                    paymentTerm.text =
                                        '${payDate.day}.${payDate.month}.${payDate.year}';
                                  });
                                },
                                child: const Text('Zahlungsfrist auswählen')),
                            const SizedBox(
                              height: 25.00,
                            ),
                            const Align(
                              alignment: Alignment(-0.95, 1),
                              child: Text(
                                'Kopftext',
                                style: TextStyle(fontSize: 20.00),
                              ),
                            ),
                            TextFormField(
                              controller: headerText,
                              maxLines: null,
                              autofocus: false,
                              decoration: InputDecoration(
                                  border: OutlineInputBorder(
                                      borderRadius:
                                          BorderRadius.circular(50.0)),
                                  hintText: 'Kopftext eingeben',
                                  hintStyle: const TextStyle(fontSize: 20.00)),
                              style: const TextStyle(fontSize: 20.00),
                            ),
                            const SizedBox(
                              height: 25.00,
                            ),
                            const Align(
                              alignment: Alignment(-0.95, 1),
                              child: Text(
                                'Betreff',
                                style: TextStyle(fontSize: 20.00),
                              ),
                            ),
                            TextFormField(
                              controller: subject,
                              autofocus: false,
                              decoration: InputDecoration(
                                  border: OutlineInputBorder(
                                      borderRadius:
                                          BorderRadius.circular(100.0)),
                                  hintText: 'Betreff eingeben',
                                  hintStyle: const TextStyle(fontSize: 20.00)),
                              style: const TextStyle(fontSize: 20.00),
                            ),
                            const SizedBox(
                              height: 25.00,
                            ),
                            const Align(
                              alignment: Alignment(-0.95, 1),
                              child: Text(
                                'Fließtext',
                                style: TextStyle(fontSize: 20.00),
                              ),
                            ),
                            TextFormField(
                              controller: flowText,
                              maxLines: null,
                              autofocus: false,
                              decoration: InputDecoration(
                                  border: OutlineInputBorder(
                                      borderRadius:
                                          BorderRadius.circular(50.0)),
                                  hintText: 'Fließtext eingeben',
                                  hintStyle: const TextStyle(fontSize: 20.00)),
                              style: const TextStyle(fontSize: 20.00),
                            ),
                            const SizedBox(
                              height: 25.00,
                            ),
                            const Align(
                              alignment: Alignment(-0.95, 1),
                              child: Text(
                                'Gesamtermäßigung',
                                style: TextStyle(fontSize: 20.00),
                              ),
                            ),
                            TextFormField(
                              controller: totalDiscount,
                              autofocus: false,
                              decoration: InputDecoration(
                                  border: OutlineInputBorder(
                                      borderRadius:
                                          BorderRadius.circular(100.0)),
                                  hintText: 'Gesamtermäßigung eingeben',
                                  hintStyle: const TextStyle(fontSize: 20.00)),
                              style: const TextStyle(fontSize: 20.00),
                            ),
                            const SizedBox(
                              height: 25.00,
                            ),
                            const Align(
                              alignment: Alignment(-0.95, 1),
                              child: Text('Ermäßigungstyp',
                                  style: TextStyle(fontSize: 20.00)),
                            ),
                            SelectFormField(
                              controller: typeOfDiscount,
                              type: SelectFormFieldType.dropdown,
                              labelText: 'Ermäßigungstyp',
                              items: _typeOfDiscount,
                              decoration: InputDecoration(
                                  border: OutlineInputBorder(
                                      borderRadius:
                                          BorderRadius.circular(100.0)),
                                  hintText: 'Ermäßigungstyp auswählen',
                                  hintStyle: const TextStyle(fontSize: 20.00)),
                              onChanged: (val) => typeOfDiscount.text = val,
                              onSaved: (val) => val!.isNotEmpty
                                  ? typeOfDiscount.text = val
                                  : val,
                            ),
                            const SizedBox(
                              height: 25.00,
                            ),
                            const Align(
                              alignment: Alignment(-0.95, 1),
                              child: Text(
                                'Steuer *',
                                style: TextStyle(fontSize: 20.00),
                              ),
                            ),
                            TextFormField(
                              controller: tax,
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return "Bitte Steuer eingeben!";
                                }
                                return null;
                              },
                              autofocus: false,
                              decoration: InputDecoration(
                                  border: OutlineInputBorder(
                                      borderRadius:
                                          BorderRadius.circular(100.0)),
                                  hintText: 'Steuer eingeben',
                                  hintStyle: const TextStyle(fontSize: 20.00)),
                              style: const TextStyle(fontSize: 20.00),
                            ),
                            const SizedBox(
                              height: 25.00,
                            ),
                            const Align(
                              alignment: Alignment(-0.95, 1),
                              child: Text('Kunde *',
                                  style: TextStyle(fontSize: 20.00)),
                            ),
                            FutureBuilder<List<Contact>>(
                                future: NetworkHandler.getClients(),
                                builder: ((context, AsyncSnapshot snapshot) {
                                  if (snapshot.hasData &&
                                      snapshot.connectionState ==
                                          ConnectionState.done) {
                                    if (contact.isEmpty) {
                                      try {
                                        contact =
                                            "${snapshot.data[0].firstName} ${snapshot.data[0].lastName}";
                                      } catch (e) {
                                        contact = "";
                                      }
                                    } else if (contact.isNotEmpty &&
                                        contact2.isNotEmpty) {
                                      contact = contact2;
                                    }
                                    return DropdownButtonFormField<String>(
                                        alignment: const Alignment(-0.95, 1),
                                        validator: (value) {
                                          if (value == null ||
                                              value.isEmpty && contact != "") {
                                            return "Bitte Kunde auswählen!";
                                          }
                                          return null;
                                        },
                                        decoration: InputDecoration(
                                            border: OutlineInputBorder(
                                                borderRadius:
                                                    BorderRadius.circular(
                                                        100.0)),
                                            hintText: 'Kunde auswählen',
                                            hintStyle: const TextStyle(
                                                fontSize: 20.00)),
                                        isExpanded: true,
                                        hint: const Text("Kunde auswählen"),
                                        value: contact,
                                        onChanged: (newValue) {
                                          setState(() {
                                            contact2 = newValue!;
                                          });
                                        },
                                        items: snapshot.data
                                            .map<DropdownMenuItem<String>>(
                                                (fc) =>
                                                    DropdownMenuItem<String>(
                                                      child: Text(
                                                          '${fc.firstName} ${fc.lastName}'),
                                                      value:
                                                          '${fc.firstName} ${fc.lastName}',
                                                    ))
                                            .toList());
                                  } else {
                                    return const Center(
                                        child:
                                            CircularProgressIndicator()); //SizedBox.shrink()
                                  }
                                })),
                            const SizedBox(
                              height: 25.00,
                            ),
                            const Align(
                              alignment: Alignment(-0.95, 1),
                              child: Text('Ansprechperson *',
                                  style: TextStyle(fontSize: 20.00)),
                            ),
                            FutureBuilder<List<User>>(
                                future: NetworkHandler.getUsers(),
                                builder: ((context, AsyncSnapshot snapshot) {
                                  if (snapshot.hasData &&
                                      snapshot.connectionState ==
                                          ConnectionState.done) {
                                    if (user.isEmpty) {
                                      try {
                                        user =
                                            "${snapshot.data[0].firstName} ${snapshot.data[0].lastName}";
                                      } catch (e) {
                                        user = "";
                                      }
                                    } else if (user.isNotEmpty &&
                                        user2.isNotEmpty) {
                                      user = user2;
                                    }

                                    return DropdownButtonFormField<String>(
                                        alignment: const Alignment(-0.95, 1),
                                        validator: (value) {
                                          if (value == null || value.isEmpty) {
                                            return "Bitte Ansprechperson auswählen!";
                                          }
                                          return null;
                                        },
                                        decoration: InputDecoration(
                                            border: OutlineInputBorder(
                                                borderRadius:
                                                    BorderRadius.circular(
                                                        100.0)),
                                            hintText:
                                                'Ansprechperson auswählen',
                                            hintStyle: const TextStyle(
                                                fontSize: 20.00)),
                                        isExpanded: true,
                                        hint: const Text(
                                            "Ansprechperson auswählen"),
                                        value: user,
                                        onChanged: (newValue) {
                                          setState(() {
                                            user2 = newValue!;
                                          });
                                        },
                                        items: snapshot.data
                                            .map<DropdownMenuItem<String>>(
                                                (fc) =>
                                                    DropdownMenuItem<String>(
                                                      child: Text(
                                                          '${fc.firstName} ${fc.lastName}'),
                                                      value:
                                                          '${fc.firstName} ${fc.lastName}',
                                                    ))
                                            .toList());
                                  } else {
                                    return const Center(
                                        child:
                                            CircularProgressIndicator()); //SizedBox.shrink()
                                  }
                                })),
                            const SizedBox(
                              height: 25.00,
                            ),
                            MaterialButton(
                              onPressed: () async {
                                List<Products> products =
                                    await NetworkHandler.getProducts();
                                if (products.isEmpty) {
                                  productsIsEmpty = true;
                                }
                                addDynamic(
                                    products,
                                    invoiceNumber,
                                    invoiceDate,
                                    paymentTerm,
                                    headerText,
                                    flowText,
                                    subject,
                                    typeOfDiscount,
                                    totalDiscount,
                                    tax,
                                    user,
                                    contact);
                              },
                              shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(100)),
                              color: Colors.redAccent[700],
                              child: const Text('Position hinzufügen',
                                  style:
                                      TextStyle(fontSize: 18.00, height: 1.35)),
                              textColor: Colors.white,
                              height: 50.00,
                              minWidth: 200.00,
                            ),
                            const SizedBox(
                              height: 25.00,
                            ),
                            dynamicTextField,
                            const SizedBox(
                              height: 25.00,
                            ),
                            MaterialButton(
                              onPressed: () async {
                                submitData();
                                int count = 0;
                                if (!formGlobalKey.currentState!.validate()) {
                                  count++;
                                }
                                for (var key in keys) {
                                  if (!key.currentState!.validate()) {
                                    count++;
                                  }
                                }

                                String message = "";
                                if (contact == "" ||
                                    user == "" ||
                                    productsIsEmpty) {
                                  message =
                                      "Es ist kein/keine Kunde/Ansprechperson/Produkt vorhanden!\nRechnung Anlegen nicht möglich!";
                                  showAlertDialog(context, message);
                                  return;
                                }

                                if (!productsIsEmpty &&
                                    productPosition.isEmpty) {
                                  message =
                                      "Sie müssen mindestens eine Position hinzufügen!";
                                  showAlertDialog(context, message);
                                  return;
                                }

                                if (count > 0) {
                                  return;
                                }

                                int invDay =
                                    int.parse(invoiceDate.text.split('.')[0]);
                                int invMonth =
                                    int.parse(invoiceDate.text.split('.')[1]);
                                int invYear =
                                    int.parse(invoiceDate.text.split('.')[2]);
                                int payDay =
                                    int.parse(paymentTerm.text.split('.')[0]);
                                int payMonth =
                                    int.parse(paymentTerm.text.split('.')[1]);
                                int payYear =
                                    int.parse(paymentTerm.text.split('.')[2]);

                                if (invYear > payYear ||(invYear == payYear && invMonth > payMonth) ||(invYear == payYear && invMonth == payMonth && invDay > payDay)) {
                                  message =
                                      "Datum darf nicht später als die Zahlungsfrist sein!";
                                  showAlertDialog(context, message);
                                  return;
                                }

                                await addInvoice(
                                    invoiceNumber.text,
                                    invoiceDate.text,
                                    paymentTerm.text,
                                    subject.text,
                                    headerText.text,
                                    flowText.text,
                                    totalDiscount.text,
                                    typeOfDiscount.text,
                                    tax.text,
                                    contact,
                                    user,
                                    quantityPosition,
                                    discountPosition,
                                    typeOfDiscountPosition,
                                    productPosition);
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                      builder: (context) => const Invoice()),
                                );
                              },
                              shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(100)),
                              color: Colors.redAccent[700],
                              child: const Text('Rechnung anlegen',
                                  style:
                                      TextStyle(fontSize: 22.00, height: 1.35)),
                              textColor: Colors.white,
                              height: 50.00,
                              minWidth: 477.00,
                            ),
                          ])))
            ]))));
  }

  showAlertDialog(BuildContext context, String message) {
    AlertDialog alert = AlertDialog(
      title: const Text("Achtung!"),
      content: Text(message),
      actions: [
        TextButton(
          child: const Text("OK"),
          onPressed: () {
            Navigator.of(context).pop();
          },
        )
      ],
    );

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return alert;
      },
    );
  }

  addDynamic(
      List<Products> products,
      TextEditingController invoiceNumber,
      TextEditingController invoiceDate,
      TextEditingController paymentTerm,
      TextEditingController headerText,
      TextEditingController flowText,
      TextEditingController subject,
      TextEditingController typeOfDiscount,
      TextEditingController totalDiscount,
      TextEditingController tax,
      String user,
      String contact) {
    setState(() {
      this.invoiceNumber = invoiceNumber;
      this.invoiceDate = invoiceDate;
      this.paymentTerm = paymentTerm;
      this.headerText = headerText;
      this.flowText = flowText;
      this.subject = subject;
      this.typeOfDiscount = typeOfDiscount;
      this.totalDiscount = totalDiscount;
      this.tax = tax;
      this.user = user;
      this.contact = contact;
    });
    dynamicList.add(DynamicWidget(
      products: products,
    ));
  }

  submitData() {
    productPosition = [];
    quantityPosition = [];
    discountPosition = [];
    typeOfDiscountPosition = [];
    keys = [];

    for (var widget in dynamicList) {
      productPosition.add(widget.productPosition.text);
      quantityPosition.add(widget.quantityPosition.text);
      discountPosition.add(widget.discountPosition.text);
      typeOfDiscountPosition.add(widget.typeOfDiscountPosition.text);
      keys.add(widget.formGlobalKey);
    }
  }

  Future<int> addInvoice(
      String invoiceNum,
      String invoiceDate,
      String paymentTerm,
      String subject,
      String headerText,
      String flowText,
      String totalDiscount,
      String typeOfDiscount,
      String tax,
      String clientId,
      String contactPersonId,
      List<String> quantityPosition,
      List<String> discountPosition,
      List<String> typeOfDiscountPosition,
      List<String> productPosition) async {
    String url = "https://backend.invoicer.at/api/Companies/add-invoice";
    Uri uri = Uri.parse(url);
    List<Products> products = await NetworkHandler.getProducts();
    List<Contact> contacts = await NetworkHandler.getClients();
    List<User> users = await NetworkHandler.getUsers();

    String firstName = "";
    String lastName = "";
    String idClient = "";

    for (var client in contacts) {
      firstName = clientId.split(' ')[0];
      lastName = clientId.split(' ')[1];
      if (client.firstName == firstName && client.lastName == lastName) {
        idClient = client.contactId;
      }
    }

    String contactId = "";
    for (var user in users) {
      if ('${user.firstName} ${user.lastName}' == contactPersonId) {
        contactId = user.userId;
      }
    }

    List<String> _delTypeOfDiscount = ['Euro', 'Percent'];
    int typeOfDisIndex = 0;
    String delTypeOfDiscount = "";
    if (typeOfDiscount.isNotEmpty) {
      typeOfDisIndex = int.parse(typeOfDiscount);
      if (typeOfDisIndex == 0) {
        delTypeOfDiscount = _delTypeOfDiscount[0];
      } else if (typeOfDisIndex == 1) {
        delTypeOfDiscount = _delTypeOfDiscount[1];
      }
    }

    double delTax = double.parse(tax);

    List<Map<String, dynamic>> _positions = [];

    for (int i = 0; i < quantityPosition.length; i++) {
      int typeOfDisIndex = 0;
      Map<String, dynamic> positionBody = {};
      String typeOfDis = "";
      if (typeOfDiscountPosition[i].isNotEmpty) {
        typeOfDisIndex = int.parse(typeOfDiscountPosition[i]);
        if (typeOfDisIndex == 0) {
          typeOfDis = _delTypeOfDiscount[0];
        } else if (typeOfDisIndex == 1) {
          typeOfDis = _delTypeOfDiscount[1];
        }
        positionBody["typeOfDiscount"] = typeOfDis;
      }

      int prodIndex = int.parse(productPosition[i]);
      Products product = products.elementAt(prodIndex);

      positionBody["quantity"] = quantityPosition[i];
      positionBody["productId"] = product.productId;
      String discountPos = "";
      if (discountPosition[i].isNotEmpty) {
        discountPos = discountPosition[i];
        positionBody["discount"] = discountPos;
      }

      _positions.add(positionBody);
    }

    String? token = await NetworkHandler.getToken();
    if (token!.isNotEmpty) {
      token = token.toString();

      var body = {};
      String invDateSplit =
          "${invoiceDate.split('.')[2]}-${invoiceDate.split('.')[1]}-${invoiceDate.split('.')[0]}";
      body["invoiceDate"] = invDateSplit;

      String paymentTermSplit =
          "${paymentTerm.split('.')[2]}-${paymentTerm.split('.')[1]}-${paymentTerm.split('.')[0]}";
      body["paymentTerm"] = paymentTermSplit;

      body["documentInformation"] = {
        "tax": delTax,
        "clientId": idClient,
        "contactPersonId": contactId,
        "positions": _positions
      };

      if (invoiceNum.isNotEmpty) {
        body["invoiceNumber"] = invoiceNum;
      }
      if (delTypeOfDiscount.isNotEmpty) {
        body["documentInformation"]["typeOfDiscount"] = delTypeOfDiscount;
      }
      if (subject.isNotEmpty) {
        body["subject"] = subject;
      }
      if (headerText.isNotEmpty) {
        body["headerText"] = headerText;
      }
      if (flowText.isNotEmpty) {
        body["flowText"] = flowText;
      }
      if (totalDiscount.isNotEmpty) {
        body["documentInformation"]["totalDiscount"] = totalDiscount;
      }

      var jsonBody = json.encode(body);

      final response = await http.put(uri,
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': 'Bearer $token'
          },
          body: jsonBody);

      print(response.statusCode);
      print(response.body);
    }

    return 0;
  }
}
