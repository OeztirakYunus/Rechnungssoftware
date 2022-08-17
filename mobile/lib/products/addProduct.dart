// ignore_for_file: file_names

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:select_form_field/select_form_field.dart';
import 'dart:convert';

class AddProduct extends StatelessWidget {
  const AddProduct({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final List<Map<String, dynamic>> _items = [
      {'value': '0', 'label': 'Getränk'},
      {'value': 1, 'label': 'Obst'},
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
                        height: 10.00,
                      ),
                      Column(
                        children: [
                          const SizedBox(
                            height: 25.00,
                          ),
                          MaterialButton(
                            onPressed: () async {
                              addProduct(articleNumber.text, productName.text,
                                  sellingPriceNet.text, productCategory.text);
                              /*Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => const Categories()),
                        );*/
                            },
                            shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(100)),
                            color: Colors.purpleAccent[700],
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

  Future<int> addProduct(String articleNumber, String productName,
      String sellingPriceNet, String productCategory) async {
    var sellingPrice = double.parse(sellingPriceNet);
    print("ARCTICLENUMBER: $articleNumber");
    print("PRODUCTNAME: $productName");
    print("SELLINGPRICENET: $sellingPrice");
    print("PRODUCTCATEGORY: $productCategory");
    return 0;
  }
}
