// ignore_for_file: prefer_const_literals_to_create_immutables, prefer_const_constructors
import 'package:demo5/products/addProduct.dart';
import 'package:flutter/material.dart';
import '../NavBar.dart';

class Product extends StatefulWidget {
  final String productCategory;

  const Product({Key? key, required this.productCategory}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _ProductsState();
}

class _ProductsState extends State<Product> {
  final Products _products = Products("", "", "", "", 0.0);

  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    _products.getList();
    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Produkte',
            style:
                TextStyle(height: 1.00, fontSize: 25.00, color: Colors.white)),
        centerTitle: true,
      ),
      drawer: NavBar(),
      body: ListView.builder(
          itemBuilder: _itemBuilder,
          itemCount:
              _products.getMatchingCategoryCount(widget.productCategory)),
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

  Widget _itemBuilder(BuildContext context, int index) {
    return InkWell(
      onTap: () {
        Navigator.push(
          context,
          MaterialPageRoute(builder: (context) => const AddProduct()),
        );
      },
      child: Card(
        child: ListTile(
          leading: Text(_products.getNameByCategory(index)),
        ),
        shape:
            RoundedRectangleBorder(borderRadius: BorderRadius.circular(10.0)),
      ),
    );
  }
}

class Products {
  Products(this.productName, this.category, this.description,
      this.articleNumber, this.sellingPriceNet);

  final String productName;
  final String description;
  final String articleNumber;
  final double sellingPriceNet;
  final String category;
  int categoryProductsCount = 0;

  List<Products> productsList = [];
  List<Products> matchingCategory = [];

  void getList() {
    productsList.add(
        Products("Red Bull", "Getränk", "Koffeinhalitges Getränk", "", 1.20));
    productsList
        .add(Products("Äpfel", "Obst", "Frische Äpfel aus Spanien", "", 1));
    productsList
        .add(Products("Eistee", "Getränk", "Zuckerhalitges Getränk", "", 2));
    productsList.add(
        Products("Birnen", "Obst", "Frische Birnen aus Deutschland", "", 1));
    productsList
        .add(Products("Fanta", "Getränk", "Zuckerhalitges Getränk", "", 2));
  }

  String getName(int index) {
    return productsList[index].productName;
  }

  String getNameByCategory(int index) {
    return matchingCategory[index].productName;
  }

  int getMatchingCategoryCount(String category) {
    for (int i = 0; i < productsList.length; i++) {
      if (productsList[i].category == category) {
        matchingCategory.add(productsList[i]);
        categoryProductsCount++;
      }
    }
    if (categoryProductsCount > 0) {
      return categoryProductsCount;
    }
    throw Exception("Kategorie ist nicht vorhanden");
  }

  int getProductListLength() {
    return productsList.length;
  }
}
