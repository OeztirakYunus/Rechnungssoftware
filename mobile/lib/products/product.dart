// ignore_for_file: prefer_const_literals_to_create_immutables, prefer_const_constructors
import 'package:demo5/network/networkHandler.dart';
import 'package:demo5/products/addProduct.dart';
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
  /*late final Products _products = Products(
      productName: "",
      category: "",
      description: "",
      articleNumber: "",
      sellingPriceNet: "0");*/

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
      body: FutureBuilder<List<String>>(
          future: getProducts(),
          builder: (context, AsyncSnapshot snapshot) {
            if (snapshot.hasData &&
                snapshot.connectionState == ConnectionState.done) {
              return ListView.builder(
                itemCount: snapshot.data!.length,
                itemBuilder: (context, index) {
                  return Card(
                    child: ListTile(
                      leading: Text(
                        snapshot.data?[index] ?? "got null",
                      ),
                    ),
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10.0)),
                  );
                },
              );
              /*ListView.builder(
                  itemCount: _products.getList(),
                  scrollDirection: Axis.horizontal,
                  itemBuilder: (context, int index) {
                    return InkWell(
                      child: Card(
                        child: ListTile(
                          leading: Text(_products.getNameByCategory(index)),
                        ),
                        shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(10.0)),
                      ),
                    );
                  });*/
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

  Future<List<String>> getProducts() async {
    String url = "https://backend.invoicer.at/api/Products";
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    List<String> productNames = [];
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
        int category = int.parse(obj['category'].toString());
        String description = obj['description'];
        /*Products products = Products(
            articleNumber: articleNumber,
            category: category,
            description: description,
            productName: productName,
            sellingPriceNet: sellingPriceNet);*/
        if (widget.categoryIndex == category) {
          productNames.add(productName);
        }

        print(articleNumber);
        print(productName);
        print(sellingPriceNet);
        print(category);
      }
    }
    return productNames;
  }
}

class Products {
  final String productName;
  final String description;
  final String articleNumber;
  final String sellingPriceNet;
  final String category;

  Products(this.productName, this.category, this.description,
      this.articleNumber, this.sellingPriceNet);

  int categoryProductsCount = 0;

  Future<List<Products>> productsList = [] as Future<List<Products>>;
  Future<List<Products>> matchingCategory = [] as Future<List<Products>>;

  Future<void> addToList(Products products) async {
    await productsList.then((value) => value.add(products));
  }

  Future<List<Products>> getList() async {
    return await productsList;
  }

  Future<String> getName(int index) async {
    return await productsList
        .then((value) => value.elementAt(index).productName);
  }

  Future<void> getNameByCatgry(int index) async {
    List<Products> matchingCatgry =
        await matchingCategory.then((value) => value.toList());

    getNameByCategory(matchingCatgry.elementAt(index).productName);
  }

  String getNameByCategory(String productName) {
    return productName;
  }

  Future<int> getMatchingCategoryCount(String category) async {
    List<Products> products =
        await productsList.then((value) => value.toList());
    List<Products> matchingCatgry =
        await matchingCategory.then((value) => value.toList());

    for (int i = 0; i < products.length; i++) {
      if (products[i].category == category) {
        matchingCatgry.add(products[i]);
        categoryProductsCount++;
      }
    }
    if (categoryProductsCount > 0) {
      return categoryProductsCount;
    }
    throw Exception("Kategorie ist nicht vorhanden");
  }

  Future<int> getProductListLength() async {
    return await productsList.then((value) => value.length);
  }

  /*factory Products.fromJson(Map<String, dynamic> json) {
    return Products(
        productName: json['productName'] as String,
        category: json['category'] as String,
        description: json['description'] as String,
        articleNumber: json['articleNumber'] as String,
        sellingPriceNet: json['sellingPriceNet'] as String);
  }*/
}
