import 'dart:convert';

import 'package:demo5/orderConfirmation/order_confirmation.dart';
import 'package:flutter/material.dart';
import 'package:select_form_field/select_form_field.dart';

import 'package:http/http.dart' as http;
import '../contact/contacts.dart';
import '../deliveryNote/dynamicWidget.dart';
import '../deliveryNote/editDynamicWidget.dart';
import '../network/networkHandler.dart';
import '../products/product.dart';
import '../user/user.dart';

class EditOrderConfirmation extends StatefulWidget {
  final String id;
  final String companyId;
  final String documentInformationId;
  final String orderConfirmationNum;
  final String orderConfirmationdate;
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
  const EditOrderConfirmation(
      {Key? key,
      required this.id,
      required this.companyId,
      required this.documentInformationId,
      required this.orderConfirmationNum,
      required this.orderConfirmationdate,
      required this.status,
      required this.subject,
      required this.headerText,
      required this.flowText,
      required this.totalDiscount,
      required this.typeOfDiscount,
      required this.tax,
      required this.clientId,
      required this.contactPersonId,
      required this.quantityPosition,
      required this.discountPosition,
      required this.typeOfDiscountPosition,
      required this.productPosition,
      required this.products})
      : super(key: key);

  @override
  State<StatefulWidget> createState() => _EditOrderConfirmationsState();
}

class _EditOrderConfirmationsState extends State<EditOrderConfirmation> {
  final formGlobalKey = GlobalKey<FormState>();
  List<GlobalKey<FormState>> keys = [];
  String user = "";
  String user2 = "";
  String contact = "";
  String contact2 = "";
  bool productsIsEmpty = false;
  int count = 0;
  List<EditDynamicWidget> dynamicList = [];
  List<DynamicWidget> addDynamicList = [];
  List<String> productPosition = [];
  List<String> quantityPosition = [];
  List<String> discountPosition = [];
  List<String> typeOfDiscountPosition = [];

  TextEditingController status = TextEditingController();
  TextEditingController orderConfirmationNum = TextEditingController();
  TextEditingController orderConfirmationdate = TextEditingController();
  TextEditingController headerText = TextEditingController();
  TextEditingController flowText = TextEditingController();
  TextEditingController subject = TextEditingController();

  TextEditingController typeOfDiscount = TextEditingController();
  TextEditingController totalDiscount = TextEditingController();
  TextEditingController tax = TextEditingController();

  DateTime date = DateTime(2022, 10, 6);
  String typeOfD = "";

  @override
  void initState() {
    super.initState();
    status.text = widget.status;
    orderConfirmationNum.text = widget.orderConfirmationNum;

    String dateSplit = widget.orderConfirmationdate.split('T')[0];
    int year = int.parse(dateSplit.split('-')[0]);
    int month = int.parse(dateSplit.split('-')[1]);
    int day = int.parse(dateSplit.split('-')[2]);
    date = DateTime(year, month, day);
    orderConfirmationdate.text =
        "${day.toString()}.${month.toString()}.${year.toString()}";

    headerText.text = widget.headerText;
    flowText.text = widget.flowText;
    subject.text = widget.subject;
    typeOfDiscount.text = widget.typeOfDiscount;
    totalDiscount.text = widget.totalDiscount;
    tax.text = widget.tax;
    user = widget.contactPersonId;
    contact = widget.clientId;

    if (widget.typeOfDiscount == "Percent") {
      typeOfD = "Prozent";
    } else {
      typeOfD = "Euro";
    }

    for (int i = 0; i < widget.quantityPosition.length; i++) {
      dynamicList.add(EditDynamicWidget(
          products: widget.products,
          pPosition: widget.productPosition[i],
          qPosition: widget.quantityPosition[i],
          dPosition: widget.discountPosition[i],
          typePosition: widget.typeOfDiscountPosition[i]));

      productPosition.add(widget.productPosition[i]);
      quantityPosition.add(widget.quantityPosition[i]);
      discountPosition.add(widget.discountPosition[i]);
      typeOfDiscountPosition.add(widget.typeOfDiscountPosition[i]);
    }
  }

  @override
  Widget build(BuildContext context) {
    List<Map<String, dynamic>> _status = [
      {'value': 0, 'label': 'geöffnet'},
      {'value': 1, 'label': 'geschlossen'}
    ];

    List<Map<String, dynamic>> _typeOfDiscount = [
      {'value': 0, 'label': 'Euro'},
      {'value': 1, 'label': 'Prozent'}
    ];

    Widget dynamicTextField = Container(
      width: 500,
      child: ListView.builder(
        itemCount: dynamicList.length,
        shrinkWrap: true,
        itemBuilder: (_, index) => dynamicList[index],
      ),
    );

    Widget addDynamicTextField = Container(
      width: 500,
      child: ListView.builder(
        itemCount: addDynamicList.length,
        shrinkWrap: true,
        itemBuilder: (_, index) => addDynamicList[index],
      ),
    );

    return SafeArea(
        child: Scaffold(
            appBar: AppBar(
              title: const Text('Auftragsbestätigung bearbeiten',
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
                              child: Text('Auftragsbestätigungsnummer *',
                                  style: TextStyle(fontSize: 20.00)),
                            ),
                            TextFormField(
                              controller: orderConfirmationNum,
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return "Bitte Auftragsbestätigungsnummer eingeben!";
                                } else {
                                  return null;
                                }
                              },
                              autofocus: false,
                              decoration: InputDecoration(
                                  border: OutlineInputBorder(
                                      borderRadius:
                                          BorderRadius.circular(100.0)),
                                  hintText:
                                      'Auftragsbestätigungsnummer eingeben',
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
                              controller: orderConfirmationdate,
                              validator: (value) {
                                if (value == null ||
                                    value.isEmpty ||
                                    orderConfirmationdate.text.isEmpty) {
                                  return "Bitte Datum auswählen!";
                                } else {
                                  return null;
                                }
                              },
                              decoration: InputDecoration(
                                  border: OutlineInputBorder(
                                      borderRadius:
                                          BorderRadius.circular(100.0)),
                                  hintStyle: const TextStyle(fontSize: 20.00)),
                              style: const TextStyle(fontSize: 20.00),
                            ),
                            ElevatedButton(
                                onPressed: () async {
                                  DateTime? newDate = await showDatePicker(
                                      cancelText: "Abbrechen",
                                      context: context,
                                      initialDate: date,
                                      firstDate: DateTime(2000),
                                      lastDate: DateTime(2100));
                                  if (newDate == null) return;

                                  setState(() {
                                    date = newDate;
                                    orderConfirmationdate.text =
                                        '${date.day}.${date.month}.${date.year}';
                                  });
                                },
                                child: const Text('Datum auswählen')),
                            const SizedBox(
                              height: 25.00,
                            ),
                            const Align(
                              alignment: Alignment(-0.95, 1),
                              child: Text(
                                'Kopftext *',
                                style: TextStyle(fontSize: 20.00),
                              ),
                            ),
                            TextFormField(
                              controller: headerText,
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return "Bitte Kopftext eingeben!";
                                } else {
                                  return null;
                                }
                              },
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
                                'Betreff *',
                                style: TextStyle(fontSize: 20.00),
                              ),
                            ),
                            TextFormField(
                              controller: subject,
                              autofocus: false,
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return "Bitte Betreff eingeben!";
                                } else {
                                  return null;
                                }
                              },
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
                                'Fließtext*',
                                style: TextStyle(fontSize: 20.00),
                              ),
                            ),
                            TextFormField(
                              controller: flowText,
                              autofocus: false,
                              maxLines: null,
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return "Bitte Fließtext eingeben!";
                                } else {
                                  return null;
                                }
                              },
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
                              child: Text('Status *',
                                  style: TextStyle(fontSize: 20.00)),
                            ),
                            SelectFormField(
                              controller: status,
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  value = widget.status;
                                }
                                return null;
                              },
                              type: SelectFormFieldType.dropdown,
                              labelText: 'Status *',
                              items: _status,
                              decoration: InputDecoration(
                                  border: OutlineInputBorder(
                                      borderRadius:
                                          BorderRadius.circular(100.0)),
                                  hintText: widget.status,
                                  hintStyle: const TextStyle(fontSize: 20.00)),
                              onChanged: (val) => status.text = val,
                              onSaved: (val) =>
                                  val!.isNotEmpty ? status.text = val : val,
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
                              keyboardType: TextInputType.number,
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
                                  hintText: typeOfD,
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
                                } else {
                                  return null;
                                }
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
                                    status,
                                    orderConfirmationNum,
                                    orderConfirmationdate,
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
                            addDynamicTextField,
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
                                      "Es ist kein/keine Kunde/Ansprechperson/Produkt vorhanden!\nAuftragsbestätigung Bearbeiten nicht möglich!";
                                  showAlertDialog(context, message);
                                  return;
                                }

                                if (!productsIsEmpty &&
                                    productPosition.isEmpty) {
                                  message =
                                      "Sie müssen mindestens eine Position hinzufügen!";
                                  showAlertDialog(context, message);
                                }

                                if (count > 0) {
                                  return;
                                }
                                if (orderConfirmationdate.text == "") {
                                  orderConfirmationdate.text =
                                      "${date.day.toString()}.${date.month.toString()}.${date.year.toString()}";
                                }

                                await editOrderConfirmation(
                                    widget.id,
                                    widget.companyId,
                                    widget.documentInformationId,
                                    orderConfirmationNum.text,
                                    orderConfirmationdate.text,
                                    status.text,
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
                                Navigator.of(context).pushAndRemoveUntil(
                                  MaterialPageRoute(
                                      builder: (context) =>
                                          const OrderConfirmation()),
                                  (route) => false,
                                );
                              },
                              shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(100)),
                              color: Colors.redAccent[700],
                              child: const Text('Auftragsbestätigung speichern',
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
      TextEditingController status,
      TextEditingController deliveryNoteNumber,
      TextEditingController deliveryNoteDate,
      TextEditingController headerText,
      TextEditingController flowText,
      TextEditingController subject,
      TextEditingController typeOfDiscount,
      TextEditingController totalDiscount,
      TextEditingController tax,
      String user,
      String contact) {
    setState(() {
      this.user = user;
      this.contact = contact;
    });
    addDynamicList.add(DynamicWidget(
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
    for (var widget in addDynamicList) {
      productPosition.add(widget.productPosition.text);
      quantityPosition.add(widget.quantityPosition.text);
      discountPosition.add(widget.discountPosition.text);
      typeOfDiscountPosition.add(widget.typeOfDiscountPosition.text);
      keys.add(widget.formGlobalKey);
    }
  }

  Future<int> editOrderConfirmation(
      String id,
      String companyId,
      String documentInformationId,
      String orderConfirmationNum,
      String orderConfirmationDate,
      String status,
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
    String url = "https://backend.invoicer.at/api/OrderConfirmations";
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

    List<String> _status = ['OPEN', 'CLOSED'];
    List<String> _delTypeOfDiscount = ['Euro', 'Percent'];

    String delStatus = "";
    String delTypeOfDiscount = "";

    if (status == "geöffnet" || status == "0") {
      delStatus = _status[0];
    } else if (status == "geschlossen" || status == "1") {
      delStatus = _status[1];
    }

    if (typeOfDiscount == _delTypeOfDiscount[0] || typeOfDiscount == "0") {
      delTypeOfDiscount = _delTypeOfDiscount[0];
    } else if (typeOfDiscount == "Prozent" ||
        typeOfDiscount == "1" ||
        typeOfDiscount == _delTypeOfDiscount[1]) {
      delTypeOfDiscount = _delTypeOfDiscount[1];
    }

    double delTax = double.parse(tax);

    List<Map<String, dynamic>> _positions = [];

    for (int i = 0; i < quantityPosition.length; i++) {
      Map<String, dynamic> positionBody = {};
      positionBody["quantity"] = quantityPosition[i];
      String productId = "";
      if (int.tryParse(productPosition[i]) != null) {
        int prodIndex = int.parse(productPosition[i]);
        Products product = products.elementAt(prodIndex);
        productId = product.productId;
      } else {
        for (var product in products) {
          if (product.productName == productPosition[i]) {
            productId = product.productId;
          }
        }
      }
      positionBody["productId"] = productId;

      String typeOfDis = "";
      if (typeOfDiscountPosition[i].isNotEmpty &&
          int.tryParse(typeOfDiscountPosition[i]) != null) {
        int typeOfDisIndex = int.parse(typeOfDiscountPosition[i]);
        if (typeOfDisIndex == 0) {
          typeOfDis = _delTypeOfDiscount[0];
        } else if (typeOfDisIndex == 1) {
          typeOfDis = _delTypeOfDiscount[1];
        }
        positionBody["typeOfDiscount"] = typeOfDis;
      } else if (typeOfDiscountPosition[i].isNotEmpty &&
          int.tryParse(typeOfDiscountPosition[i]) == null) {
        if (typeOfDiscountPosition[i] == _delTypeOfDiscount[0]) {
          typeOfDis = _delTypeOfDiscount[0];
        } else if (typeOfDiscountPosition[i] == _delTypeOfDiscount[1] ||
            typeOfDiscountPosition[i] == "Prozent") {
          typeOfDis = _delTypeOfDiscount[1];
        }
        positionBody["typeOfDiscount"] = typeOfDis;
      }

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
      body["id"] = id;
      body["orderConfirmationNumber"] = orderConfirmationNum;
      String orderConfirmationDateSplit =
          "${orderConfirmationDate.split('.')[2]}-${orderConfirmationDate.split('.')[1]}-${orderConfirmationDate.split('.')[0]}";
      body["orderConfirmationDate"] = orderConfirmationDateSplit;
      body["status"] = delStatus;
      body["subject"] = subject;
      body["headerText"] = headerText;
      body["flowText"] = flowText;
      body["companyId"] = companyId;
      body["documentInformationId"] = documentInformationId;
      body["documentInformation"] = {
        "totalDiscount": totalDiscount,
        "typeOfDiscount": delTypeOfDiscount,
        "tax": delTax,
        "clientId": idClient,
        "contactPersonId": contactId,
        "positions": _positions
      };
      var jsonBody = json.encode(body);

      final response = await http.put(uri,
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': 'Bearer $token'
          },
          body: jsonBody);

      print(response.statusCode);
    }

    return 0;
  }
}
