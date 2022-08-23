// ignore_for_file: prefer_const_literals_to_create_immutables, prefer_const_constructors
import 'package:demo5/network/networkHandler.dart';
import 'package:demo5/products/addProduct.dart';
import 'package:demo5/products/editProduct.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import '../NavBar.dart';
import 'dart:convert';

class Product extends StatefulWidget {
  final int categoryIndex;

  const Product({Key? key, required this.categoryIndex}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _ProductsState();
}

class _ProductsState extends State<Product> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Produkte',
            style:
                TextStyle(height: 1.00, fontSize: 25.00, color: Colors.white)),
        centerTitle: true,
      ),
      drawer: NavBar(),
      body: FutureBuilder<List<Products>>(
          future: getProducts(),
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
                        snapshot.data?[index].productName,
                      ),
                      subtitle: Text(
                        "Preis: ${snapshot.data?[index].sellingPriceNet}€  Art.-Nr.: ${snapshot.data?[index].articleNumber}",
                      ),
                      trailing: Row(mainAxisSize: MainAxisSize.min, children: [
                        OutlinedButton(
                          onPressed: () => {
                            alert = AlertDialog(
                              title: Text("Achtung!"),
                              content: Text(
                                  "Möchten Sie wirklich dieses Produkt löschen?"),
                              actions: [
                                TextButton(
                                  child: Text("Löschen"),
                                  onPressed: () async {
                                    await deleteProduct(
                                        snapshot.data?[index].productId);
                                    Navigator.of(context).pop();
                                    setState(() {});
                                  },
                                ),
                                TextButton(
                                  child: Text("Abbrechen"),
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
                          child: Icon(Icons.delete),
                        ),
                        OutlinedButton(
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
                        ),
                      ]),
                    ),
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10.0)),
                  );
                },
              );
            } else {
              return Center(child: CircularProgressIndicator());
            }
          }),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          Navigator.push(
            context,
            MaterialPageRoute(builder: (context) => const AddProduct()),
          );
        },
        backgroundColor: Colors.purple,
        child: const Icon(Icons.add),
      ),
    ));
  }

  Future<List<Products>> getProducts() async {
    String url = "https://backend.invoicer.at/api/Products";
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    List<Products> products = [];
    List<String> categories = ["Article", "Service"];
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
        String articleNumber = obj['articleNumber'];
        String productName = obj['productName'];
        String sellingPriceNet = obj['sellingPriceNet'].toString();
        String category = obj['category'].toString();
        String description = obj['description'];
        String unit = obj['unit'];
        String productId = obj['id'];
        String companyId = obj['companyId'];
        Products product = Products(
            productName,
            category.toString(),
            description,
            articleNumber,
            sellingPriceNet,
            productId,
            unit,
            companyId);
        print(productId);
        if (categories[widget.categoryIndex] == category) {
          products.add(product);
        }
      }
    }
    return products;
  }

  Future<int> deleteProduct(String productId) async {
    String url =
        "https://backend.invoicer.at/api/Companies/delete-product/" + productId;
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

class Products {
  final String productName;
  final String description;
  final String articleNumber;
  final String sellingPriceNet;
  final String category;
  final String productId;
  final String unit;
  final String companyId;

  Products(
      this.productName,
      this.category,
      this.description,
      this.articleNumber,
      this.sellingPriceNet,
      this.productId,
      this.unit,
      this.companyId);
}
