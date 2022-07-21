// ignore_for_file: file_names

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:select_form_field/select_form_field.dart';

class AddProduct extends StatelessWidget {
  const AddProduct({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final List<Map<String, dynamic>> _items = [
      {'value': '0', 'label': 'Getränk'},
      {'value': 1, 'label': 'Obst'},
    ];

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
                        type: SelectFormFieldType.dropdown,
                        labelText: 'Produktkategorie',
                        items: _items,
                        decoration: InputDecoration(
                            border: OutlineInputBorder(
                                borderRadius: BorderRadius.circular(100.0)),
                            hintText: 'Produktkategorie auswählen',
                            hintStyle: const TextStyle(fontSize: 20.00)),
                        onChanged: (val) => print(val),
                        onSaved: (val) => print(val),
                      ),
                      const SizedBox(
                        height: 25.00,
                      ),
                      const Align(
                        alignment: Alignment(-0.95, 1),
                        child:
                            Text('Anzahl', style: TextStyle(fontSize: 20.00)),
                      ),
                      TextFormField(
                        autofocus: false,
                        keyboardType: TextInputType.number,
                        inputFormatters: <TextInputFormatter>[
                          FilteringTextInputFormatter.digitsOnly
                        ],
                        decoration: InputDecoration(
                            border: OutlineInputBorder(
                                borderRadius: BorderRadius.circular(100.0)),
                            hintText: 'Anzahl eingeben',
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
}
