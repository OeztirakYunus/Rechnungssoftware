// ignore: file_names
import 'dart:io';

import 'package:demo5/contact/contacts.dart';
import 'package:flutter/material.dart';
import '../NavBar.dart';

class CategoryContact extends StatefulWidget {
  const CategoryContact({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _CategoryContactsState();
}

class _CategoryContactsState extends State<CategoryContact> {
  final Category _contacts = Category("");

  Future<bool> _onWillPop() async {
    return (await showDialog(
          context: context,
          builder: (context) => AlertDialog(
            title: const Text('App verlassen?'),
            content: const Text('Möchten Sie die App verlassen?'),
            actions: <Widget>[
              TextButton(
                onPressed: () => Navigator.of(context).pop(false),
                child: const Text('Nein'),
              ),
              TextButton(
                onPressed: () => exit(0),
                child: const Text('Ja'),
              ),
            ],
          ),
        )) ??
        false;
  }

  @override
  Widget build(BuildContext context) {
    _contacts.initializeCategory();
    return WillPopScope(
        onWillPop: _onWillPop,
        child: SafeArea(
            child: Scaffold(
          appBar: AppBar(
            title: const Text('Kontaktarten',
                style: TextStyle(
                    height: 1.00, fontSize: 25.00, color: Colors.white)),
            centerTitle: true,
          ),
          drawer: const NavBar(),
          body: ListView.builder(
              itemBuilder: _itemBuilder,
              itemCount: _contacts.getCategoryListLength()),
        )));
  }

  Widget _itemBuilder(BuildContext context, int index) {
    return InkWell(
      onTap: () {
        Navigator.push(
          context,
          MaterialPageRoute(
              builder: (context) => Contacts(
                    categoryIndex: index,
                  )),
        );
      },
      child: Card(
        child: ListTile(
          leading: Text(_contacts.getProductCategoryName(index)),
        ),
        shape:
            RoundedRectangleBorder(borderRadius: BorderRadius.circular(10.0)),
      ),
    );
  }
}

class Category {
  Category(this.contactCategory);

  final String contactCategory;

  List<Category> categoryList = [];

  void initializeCategory() {
    categoryList.add(Category("Anbieter"));
    categoryList.add(Category("Kunde"));
    categoryList.add(Category("Partner"));
    categoryList.add(Category("Möglicher Kunde"));
    categoryList.add(Category("Keine Zielgruppe"));
  }

  String getProductCategoryName(int index) {
    return categoryList[index].contactCategory;
  }

  int getCategoryListLength() {
    return categoryList.length;
  }
}
