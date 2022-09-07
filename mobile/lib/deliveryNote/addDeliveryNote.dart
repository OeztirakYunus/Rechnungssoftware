import 'dart:convert';
import 'package:demo5/contact/contacts.dart';
import 'package:demo5/deliveryNote/delivery_note.dart';
import 'package:demo5/network/networkHandler.dart';
import 'package:demo5/products/product.dart';
import 'package:demo5/user/user.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:select_form_field/select_form_field.dart';
import 'dynamicWidget.dart';

class AddDeliveryNote extends StatefulWidget {
  const AddDeliveryNote({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _AddDeliveryNotesState();
}

class _AddDeliveryNotesState extends State<AddDeliveryNote> {
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
  TextEditingController deliveryNoteNumber = TextEditingController();
  TextEditingController deliveryNoteDate = TextEditingController();
  TextEditingController headerText = TextEditingController();
  TextEditingController flowText = TextEditingController();
  TextEditingController subject = TextEditingController();

  TextEditingController typeOfDiscount = TextEditingController();
  TextEditingController totalDiscount = TextEditingController();
  TextEditingController tax = TextEditingController();

  DateTime date = DateTime(2022, 9, 5);

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
              title: const Text('Lieferschein anlegen',
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
                          child: Text('Lieferscheinnummer *',
                              style: TextStyle(fontSize: 20.00)),
                        ),
                        TextFormField(
                          controller: deliveryNoteNumber,
                          autofocus: false,
                          decoration: InputDecoration(
                              border: OutlineInputBorder(
                                  borderRadius: BorderRadius.circular(100.0)),
                              hintText: 'Lieferscheinnummer eingeben',
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
                          controller: deliveryNoteDate,
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
                                deliveryNoteDate.text =
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
                                deliveryNoteNumber,
                                deliveryNoteDate,
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
                            await addDeliveryNote(
                                deliveryNoteNumber.text,
                                deliveryNoteDate.text,
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
                                  builder: (context) => const DeliveryNote()),
                            );
                          },
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(100)),
                          color: Colors.redAccent[700],
                          child: const Text('Lieferschein anlegen',
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
      this.status = status;
      this.deliveryNoteNumber = deliveryNoteNumber;
      this.deliveryNoteDate = deliveryNoteDate;
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

  Future<int> addDeliveryNote(
      String delNoteNum,
      String delNoteDate,
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
    String url = "https://backend.invoicer.at/api/Companies/add-delivery-note";
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
    String delStatus = "";
    String delTypeOfDiscount = "";

    if (statusIndex == 0) {
      delStatus = _status[0];
    } else if (statusIndex == 1) {
      delStatus = _status[1];
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
        "quantity": quantityPosition[i],
        "discount": discountPosition[i],
        "typeOfDiscount": typeOfDis,
        "productId": product.productId
      });
    }

    String? token = await NetworkHandler.getToken();
    if (token!.isNotEmpty) {
      token = token.toString();

      var body = {};
      body["deliveryNoteNumber"] = delNoteNum;
      body["deliveryNoteDate"] = delNoteDate;
      body["status"] = delStatus;
      body["subject"] = subject;
      body["headerText"] = headerText;
      body["flowText"] = flowText;
      body["documentInformations"] = {
        "totalDiscount": totalDiscount,
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

  /*Widget createWidget(int count) {
    if (count > 0) {
      textCtrlList.add(value)
    }
  }*/
}
