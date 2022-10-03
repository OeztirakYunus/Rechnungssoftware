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
  String user = "";
  String user2 = "";
  String contact = "";
  String contact2 = "";
  int count = 0;
  List<DynamicWidget> dynamicList = [];
  List<String> productPosition = [];
  List<String> quantityPosition = [];
  List<String> discountPosition = [];
  List<String> typeOfDiscountPosition = [];

  TextEditingController status = TextEditingController();
  TextEditingController invoiceNumber = TextEditingController();
  TextEditingController invoiceDate = TextEditingController();
  TextEditingController paymentTerm = TextEditingController();
  TextEditingController headerText = TextEditingController();
  TextEditingController flowText = TextEditingController();
  TextEditingController subject = TextEditingController();

  TextEditingController typeOfDiscount = TextEditingController();
  TextEditingController totalDiscount = TextEditingController();
  TextEditingController tax = TextEditingController();

  DateTime date = DateTime(2022, 9, 5);
  DateTime payDate = DateTime(2022, 9, 5);

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
                      child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text('Rechnungsnummer *',
                              style: TextStyle(fontSize: 20.00)),
                        ),
                        TextFormField(
                          controller: invoiceNumber,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
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
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: "Datum auswählen!",
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        ElevatedButton(
                            onPressed: () async {
                              DateTime? newDate = await showDatePicker(
                                  context: context,
                                  initialDate: date,
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
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: "Zahlungsfrist auswählen!",
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        ElevatedButton(
                            onPressed: () async {
                              DateTime? newDate = await showDatePicker(
                                  context: context,
                                  initialDate: payDate,
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
                            'Bezeichnung *',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: headerText,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Bezeichnung eingeben',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          style: const TextStyle(fontSize: 20.00),
                        ),
                        const SizedBox(
                          height: 25.00,
                        ),
                        const Align(
                          alignment: Alignment(-0.95, 1),
                          child: Text(
                            'Thema *',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: subject,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Thema eingeben',
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
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
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
                              return "Status darf nicht leer sein!";
                            }
                            return null;
                          },
                          type: SelectFormFieldType.dropdown,
                          labelText: 'Status *',
                          items: _status,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Status auswählen',
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
                            'Ermäßigung',
                            style: TextStyle(fontSize: 20.00),
                          ),
                        ),
                        TextFormField(
                          controller: totalDiscount,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Ermäßigung eingeben',
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
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Ermäßigungstyp auswählen',
                              hintStyle: const TextStyle(fontSize: 20.00)),
                          onChanged: (val) => typeOfDiscount.text = val,
                          onSaved: (val) =>
                              val!.isNotEmpty ? typeOfDiscount.text = val : val,
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
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
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
                            future: NetworkHandler.getContacts(),
                            builder: ((context, AsyncSnapshot snapshot) {
                              if (snapshot.hasData &&
                                  snapshot.connectionState ==
                                      ConnectionState.done) {
                                if (contact.isEmpty) {
                                  contact =
                                      "${snapshot.data[0].firstName} ${snapshot.data[0].lastName}";
                                } else if (contact.isNotEmpty &&
                                    contact2.isNotEmpty) {
                                  contact = contact2;
                                }
                                return DropdownButtonFormField<String>(
                                    alignment: const Alignment(-0.95, 1),
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(100.0)),
                                        hintText: 'Kunde auswählen',
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
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
                                            (fc) => DropdownMenuItem<String>(
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
                                  user =
                                      "${snapshot.data[0].firstName} ${snapshot.data[0].lastName}";
                                } else if (user.isNotEmpty &&
                                    user2.isNotEmpty) {
                                  user = user2;
                                }

                                return DropdownButtonFormField<String>(
                                    alignment: const Alignment(-0.95, 1),
                                    decoration: InputDecoration(
                                        border: OutlineInputBorder(
                                            borderRadius:
                                                BorderRadius.circular(100.0)),
                                        hintText: 'Ansprechperson auswählen',
                                        hintStyle:
                                            const TextStyle(fontSize: 20.00)),
                                    isExpanded: true,
                                    hint:
                                        const Text("Ansprechperson auswählen"),
                                    value: user,
                                    onChanged: (newValue) {
                                      setState(() {
                                        user2 = newValue!;
                                      });
                                    },
                                    items: snapshot.data
                                        .map<DropdownMenuItem<String>>(
                                            (fc) => DropdownMenuItem<String>(
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
                            addDynamic(
                                products,
                                status,
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
                              style: TextStyle(fontSize: 18.00, height: 1.35)),
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
                            await addInvoice(
                                invoiceNumber.text,
                                date,
                                payDate,
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
                              style: TextStyle(fontSize: 22.00, height: 1.35)),
                          textColor: Colors.white,
                          height: 50.00,
                          minWidth: 477.00,
                        ),
                      ])))
            ]))));
  }

  addDynamic(
      List<Products> products,
      TextEditingController status,
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
      this.status = status;
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

    for (var widget in dynamicList) {
      productPosition.add(widget.productPosition.text);
      quantityPosition.add(widget.quantityPosition.text);
      discountPosition.add(widget.discountPosition.text);
      typeOfDiscountPosition.add(widget.typeOfDiscountPosition.text);
    }
  }

  Future<int> addInvoice(
      String invoiceNum,
      DateTime invoiceDate,
      DateTime paymentTerm,
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
    String url = "https://backend.invoicer.at/api/Companies/add-invoice";
    Uri uri = Uri.parse(url);
    List<Products> products = await NetworkHandler.getProducts();
    List<Contact> contacts = await NetworkHandler.getContacts();
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
    int statusIndex = int.parse(status);
    int typeOfDisIndex = int.parse(typeOfDiscount);
    String invStatus = "";
    String delTypeOfDiscount = "";

    if (statusIndex == 0) {
      invStatus = _status[0];
    } else if (statusIndex == 1) {
      invStatus = _status[1];
    }

    if (typeOfDisIndex == 0) {
      delTypeOfDiscount = _delTypeOfDiscount[0];
    } else if (typeOfDisIndex == 1) {
      delTypeOfDiscount = _delTypeOfDiscount[1];
    }

    double delTax = double.parse(tax);

    Map<String, dynamic> _positions = {};

    for (int i = 0; i < quantityPosition.length; i++) {
      int typeOfDisIndex = int.parse(typeOfDiscountPosition[i]);
      int prodIndex = int.parse(productPosition[i]);
      Products product = products.elementAt(prodIndex);
      String typeOfDis = "";
      if (typeOfDisIndex == 0) {
        typeOfDis = _delTypeOfDiscount[0];
      } else if (typeOfDisIndex == 1) {
        typeOfDis = _delTypeOfDiscount[1];
      }
      _positions.addAll({
        "quantity": double.parse(quantityPosition[i]),
        "discount": double.parse(discountPosition[i]),
        "typeOfDiscount": typeOfDis,
        "productId": product.productId
      });
    }

    String? token = await NetworkHandler.getToken();
    if (token!.isNotEmpty) {
      token = token.toString();

      var body = {};
      body["invoiceNumber"] = invoiceNum;
      if (invoiceDate.month < 10 && invoiceDate.day < 10) {
        body["invoiceDate"] =
            "${invoiceDate.year}-0${invoiceDate.month}-0${invoiceDate.day}";
      } else if (invoiceDate.month < 10) {
        body["invoiceDate"] =
            "${invoiceDate.year}-0${invoiceDate.month}-${invoiceDate.day}";
      } else if (invoiceDate.day < 10) {
        body["invoiceDate"] =
            "${invoiceDate.year}-${invoiceDate.month}-0${invoiceDate.day}";
      }

      if (paymentTerm.month < 10 && paymentTerm.day < 10) {
        body["paymentTerm"] =
            "${paymentTerm.year}-0${paymentTerm.month}-0${paymentTerm.day}";
      } else if (paymentTerm.month < 10) {
        body["paymentTerm"] =
            "${paymentTerm.year}-0${paymentTerm.month}-${paymentTerm.day}";
      } else if (paymentTerm.day < 10) {
        body["paymentTerm"] =
            "${paymentTerm.year}-${paymentTerm.month}-0${paymentTerm.day}";
      }

      body["status"] = invStatus;
      body["subject"] = subject;
      body["headerText"] = headerText;
      body["flowText"] = flowText;
      body["documentInformation"] = {
        "totalDiscount": double.parse(totalDiscount),
        "typeOfDiscount": delTypeOfDiscount,
        "tax": delTax,
        "clientId": idClient,
        "contactPersonId": contactId,
        "positions": [_positions]
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
      print(response.body);
    }

    return 0;
  }
}
