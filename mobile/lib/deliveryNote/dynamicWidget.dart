import 'package:flutter/material.dart';
import 'package:select_form_field/select_form_field.dart';

import '../products/product.dart';

class DynamicWidget extends StatelessWidget {
  List<Products> products;
  GlobalKey<FormState> formGlobalKey = GlobalKey<FormState>();
  TextEditingController productPosition = TextEditingController();
  TextEditingController quantityPosition = TextEditingController();
  TextEditingController discountPosition = TextEditingController();
  TextEditingController typeOfDiscountPosition = TextEditingController();
  bool isEnabled = true;

  DynamicWidget({Key? key, required this.products}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    List<Map<String, dynamic>> _products = [];
    if (products.isEmpty) {
      isEnabled = false;
    }
    for (int i = 0; i < products.length; i++) {
      _products.add({'value': i, 'label': products[i].productName});
    }
    List<Map<String, dynamic>> _typeOfDiscount = [
      {'value': 0, 'label': 'Euro'},
      {'value': 1, 'label': 'Prozent'}
    ];

    return Container(
        decoration: const BoxDecoration(
          border: Border(
              bottom: BorderSide(
            color: Colors.red,
            width: 3,
          )),
        ),
        padding: const EdgeInsets.all(10),
        child: Form(
          key: formGlobalKey,
          child:
              Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
            const SizedBox(
              height: 20.00,
            ),
            const Align(
              alignment: Alignment(-0.95, 1),
              child: Text('Produkt *', style: TextStyle(fontSize: 20.00)),
            ),
            SelectFormField(
              controller: productPosition,
              enabled: isEnabled,
              type: SelectFormFieldType.dropdown,
              labelText: 'Produkt',
              validator: (value) {
                if (value == null || value.isEmpty) {
                  return 'Bitte Produkt auswählen!';
                }
                return null;
              },
              items: _products,
              decoration: InputDecoration(
                  border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(100.0)),
                  hintText: 'Produkt auswählen',
                  hintStyle: const TextStyle(fontSize: 20.00)),
              onChanged: (val) => productPosition.text = val,
              onSaved: (val) =>
                  val!.isNotEmpty ? productPosition.text = val : val,
            ),
            const SizedBox(
              height: 25.00,
            ),
            const Align(
              alignment: Alignment(-0.95, 1),
              child: Text(
                'Quantität *',
                style: TextStyle(fontSize: 20.00),
              ),
            ),
            TextFormField(
              controller: quantityPosition,
              autofocus: false,
              validator: (value) {
                if (value == null || value.isEmpty) {
                  return 'Bitte Quantität eingeben!';
                } else if (int.parse(value.trim()) <= 0) {
                  return 'Bitte geben Sie eine Zahl größer 0 ein!';
                }
                return null;
              },
              keyboardType: TextInputType.number,
              decoration: InputDecoration(
                  border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(100.0)),
                  hintText: 'Quantität eingeben',
                  hintStyle: const TextStyle(fontSize: 20.00)),
              style: const TextStyle(fontSize: 20.00),
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
              controller: discountPosition,
              autofocus: false,
              keyboardType: TextInputType.number,
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
              child: Text('Ermäßigungstyp', style: TextStyle(fontSize: 20.00)),
            ),
            SelectFormField(
              controller: typeOfDiscountPosition,
              type: SelectFormFieldType.dropdown,
              labelText: 'Ermäßigungstyp',
              items: _typeOfDiscount,
              decoration: InputDecoration(
                  border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(100.0)),
                  hintText: 'Ermäßigungstyp auswählen',
                  hintStyle: const TextStyle(fontSize: 20.00)),
              onChanged: (val) => typeOfDiscountPosition.text = val,
              onSaved: (val) =>
                  val!.isNotEmpty ? typeOfDiscountPosition.text = val : val,
            ),
            const SizedBox(
              height: 20.00,
            ),
          ]),
        ));
  }
}
