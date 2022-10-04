// ignore_for_file: file_names

import 'package:demo5/network/networkHandler.dart';
import 'package:demo5/products/product.dart';
import 'package:flutter/material.dart';
import 'package:select_form_field/select_form_field.dart';
import 'dart:convert';
import 'package:http/http.dart' as http;

class AddProduct extends StatelessWidget {
  AddProduct({Key? key}) : super(key: key);
  final formGlobalKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    final List<Map<String, dynamic>> _items = [
      {'value': '0', 'label': 'Artikel'},
      {'value': 1, 'label': 'Dienstleistungen'},
    ];

    final List<Map<String, dynamic>> _units = [
      {'value': 0, 'label': 'Stück'},
      {'value': 1, 'label': 'Quadratmeter'},
      {'value': 2, 'label': 'Meter'},
      {'value': 3, 'label': 'Kilogram'},
      {'value': 4, 'label': 'Tonnen'},
      {'value': 5, 'label': 'Pauschal'},
      {'value': 6, 'label': 'Kubikmeter'},
      {'value': 7, 'label': 'Stunden'},
      {'value': 8, 'label': 'Kilometer'},
      {'value': 9, 'label': 'Prozent'},
      {'value': 10, 'label': 'Tage'},
      {'value': 11, 'label': 'Liter'},
    ];

    TextEditingController articleNumber = TextEditingController();
    TextEditingController productName = TextEditingController();
    TextEditingController sellingPriceNet = TextEditingController();
    TextEditingController productCategory = TextEditingController();
    TextEditingController unit = TextEditingController();
    TextEditingController description = TextEditingController();

    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Produkt Hinzufügen',
            style:
                TextStyle(height: 1.00, fontSize: 25.00, color: Colors.white)),
        centerTitle: true,
      ),
      body: SingleChildScrollView(
        child: Column(
          children: <Widget>[
            Container(
                padding: const EdgeInsets.all(10),
                child: Form(
                  key: formGlobalKey,
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Align(
                        alignment: Alignment(-0.95, 1),
                        child: Text(
                          'Artikelnummer',
                          style: TextStyle(fontSize: 20.00),
                        ),
                      ),
                      TextFormField(
                        controller: articleNumber,
                        validator: (value) {
                          if (value == null || value.isEmpty) {
                            return "Bitte Artikelnummer eingeben!";
                          }
                          return null;
                        },
                        autofocus: false,
                        decoration: InputDecoration(
                            border: OutlineInputBorder(
                                borderRadius: BorderRadius.circular(100.0)),
                            hintText: 'Artikelnummer eingeben',
                            hintStyle: const TextStyle(fontSize: 20.00)),
                        style: const TextStyle(fontSize: 20.00),
                      ),
                      const SizedBox(
                        height: 25.00,
                      ),
                      const Align(
                        alignment: Alignment(-0.95, 1),
                        child: Text('Produktname',
                            style: TextStyle(fontSize: 20.00)),
                      ),
                      TextFormField(
                        controller: productName,
                        validator: (value) {
                          if (value == null || value.isEmpty) {
                            return "Bitte Produktname eingeben!";
                          }
                          return null;
                        },
                        autofocus: false,
                        decoration: InputDecoration(
                            border: OutlineInputBorder(
                                borderRadius: BorderRadius.circular(100.0)),
                            hintText: 'Produktname eingeben',
                            hintStyle: const TextStyle(fontSize: 20.00)),
                        style: const TextStyle(fontSize: 20.00),
                      ),
                      const SizedBox(
                        height: 25.00,
                      ),
                      const Align(
                        alignment: Alignment(-0.95, 1),
                        child: Text('Verkaufspreis-Netto',
                            style: TextStyle(fontSize: 20.00)),
                      ),
                      TextFormField(
                        controller: sellingPriceNet,
                        validator: (value) {
                          if (value == null || value.isEmpty) {
                            return "Bitte Verkaufspreis-Netto eingeben!";
                          }
                          return null;
                        },
                        autofocus: false,
                        keyboardType: TextInputType.number,
                        decoration: InputDecoration(
                            border: OutlineInputBorder(
                                borderRadius: BorderRadius.circular(100.0)),
                            hintText: 'Verkaufspreis-Netto eingeben',
                            hintStyle: const TextStyle(fontSize: 20.00)),
                        style: const TextStyle(fontSize: 20.00),
                      ),
                      const SizedBox(
                        height: 25.00,
                      ),
                      const Align(
                        alignment: Alignment(-0.95, 1),
                        child: Text('Produktkategorie',
                            style: TextStyle(fontSize: 20.00)),
                      ),
                      SelectFormField(
                        controller: productCategory,
                        validator: (value) {
                          if (value == null || value.isEmpty) {
                            return "Bitte Produktkategorie auswählen!";
                          }
                          return null;
                        },
                        type: SelectFormFieldType.dropdown,
                        labelText: 'Produktkategorie',
                        items: _items,
                        decoration: InputDecoration(
                            border: OutlineInputBorder(
                                borderRadius: BorderRadius.circular(100.0)),
                            hintText: 'Produktkategorie auswählen',
                            hintStyle: const TextStyle(fontSize: 20.00)),
                        onChanged: (val) => productCategory.text = val,
                        onSaved: (val) =>
                            val!.isNotEmpty ? productCategory.text = val : val,
                      ),
                      const SizedBox(
                        height: 25.00,
                      ),
                      const Align(
                        alignment: Alignment(-0.95, 1),
                        child:
                            Text('Einheit', style: TextStyle(fontSize: 20.00)),
                      ),
                      SelectFormField(
                        controller: unit,
                        validator: (value) {
                          if (value == null || value.isEmpty) {
                            return "Bitte Einheit auswählen!";
                          }
                          return null;
                        },
                        type: SelectFormFieldType.dropdown,
                        labelText: 'Einheit',
                        items: _units,
                        decoration: InputDecoration(
                            border: OutlineInputBorder(
                                borderRadius: BorderRadius.circular(100.0)),
                            hintText: 'Produkteinheit auswählen',
                            hintStyle: const TextStyle(fontSize: 20.00)),
                        onChanged: (val) => unit.text = val,
                        onSaved: (val) =>
                            val!.isNotEmpty ? unit.text = val : val,
                      ),
                      const SizedBox(
                        height: 25.00,
                      ),
                      const Align(
                        alignment: Alignment(-0.95, 1),
                        child: Text('Beschreibung (optional)',
                            style: TextStyle(fontSize: 20.00)),
                      ),
                      TextFormField(
                        controller: description,
                        autofocus: false,
                        decoration: InputDecoration(
                            border: OutlineInputBorder(
                                borderRadius: BorderRadius.circular(100.0)),
                            hintText: 'Beschreibung eingeben',
                            hintStyle: const TextStyle(fontSize: 20.00)),
                        style: const TextStyle(fontSize: 20.00),
                      ),
                      const SizedBox(
                        height: 10.00,
                      ),
                      Column(
                        children: [
                          const SizedBox(
                            height: 25.00,
                          ),
                          MaterialButton(
                            onPressed: () async {
                              if (!formGlobalKey.currentState!.validate()) {
                                return;
                              }
                              int categoryIndex = await addProduct(
                                  articleNumber.text,
                                  productName.text,
                                  sellingPriceNet.text,
                                  productCategory.text,
                                  unit.text,
                                  description.text);
                              Navigator.of(context).pushAndRemoveUntil(
                                MaterialPageRoute(
                                    builder: (context) => Product(
                                          categoryIndex: categoryIndex,
                                        )),
                                (route) => false,
                              );
                            },
                            shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(100)),
                            color: Colors.redAccent[700],
                            child: const Text('Hinzufügen',
                                style:
                                    TextStyle(fontSize: 22.00, height: 1.35)),
                            textColor: Colors.white,
                            height: 50.00,
                            minWidth: 477.00,
                          ),
                        ],
                      ),
                    ],
                  ),
                ))
          ],
        ),
      ),
    ));
  }

  Future<int> addProduct(
      String articleNumber,
      String productName,
      String sellingPriceNet,
      String productCategory,
      String unit,
      String description) async {
    String url = "https://backend.invoicer.at/api/Companies/add-product";
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    List<String> categoriesGerman = ["Artikel", "Dienstleistungen"];
    List<String> categoriesEn = ["Article", "Service"];
    int categoryIndex = 0;

    if (token!.isNotEmpty) {
      token = token.toString();

      for (int i = 0; i < categoriesGerman.length; i++) {
        if (categoriesGerman[i] == productCategory && categoryIndex <= 1) {
          categoryIndex = i;
        }
      }

      var sellingPrice = double.parse(sellingPriceNet);
      var productUnit = int.parse(unit);

      var body = {};
      body["articleNumber"] = articleNumber;
      body["productName"] = productName;
      body["sellingPriceNet"] = sellingPrice;
      body["category"] = categoriesEn[categoryIndex];
      body["unit"] = productUnit;
      body["description"] = description;
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
    return categoryIndex;
  }
}
