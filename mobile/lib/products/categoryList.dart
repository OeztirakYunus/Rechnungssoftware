// ignore_for_file: prefer_const_literals_to_create_immutables, prefer_const_constructors
import 'package:demo5/products/product.dart';
import 'package:flutter/material.dart';

import '../NavBar.dart';

class Categories extends StatefulWidget {
  const Categories({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _CategoriesState();
}

class _CategoriesState extends State<Categories> {
  final Category _products = Category("", "");

  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    _products.initializeCategoryImageList();
    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Produktkategorien',
            style:
                TextStyle(height: 1.00, fontSize: 25.00, color: Colors.white)),
        centerTitle: true,
      ),
      drawer: NavBar(),
      body: ListView.builder(
          itemBuilder: _itemBuilder,
          itemCount: _products.getCategoryListLength()),
    ));
  }

  Widget _itemBuilder(BuildContext context, int index) {
    return InkWell(
      onTap: () {
        Navigator.push(
          context,
          MaterialPageRoute(
              builder: (context) => Product(
                    productCategory: _products.getProductCategoryName(index),
                  )),
        );
      },
      child: Card(
        child: Container(
            decoration: BoxDecoration(
                image: DecorationImage(
                    image: AssetImage(_products.getProductCategoryImage(index)),
                    fit: BoxFit.scaleDown)),
            child: ListTile(
              leading: Text(_products.getProductCategoryName(index)),
            )),
        shape:
            RoundedRectangleBorder(borderRadius: BorderRadius.circular(10.0)),
      ),
    );
  }
}

class Category {
  Category(this.categoryName, this.categoryImage);

  final String categoryName;
  final String categoryImage;

  List<Category> categoryList = [];

  void initializeCategoryImageList() {
    categoryList.add(Category("Artikel", "lib/assets/fruit.png"));
    categoryList.add(Category("Dienstleistungen", "lib/assets/drink.jpg"));
  }

  String getProductCategoryImage(int index) {
    return categoryList[index].categoryImage;
  }

  String getProductCategoryName(int index) {
    return categoryList[index].categoryName;
  }

  int getCategoryListLength() {
    return categoryList.length;
  }
}
