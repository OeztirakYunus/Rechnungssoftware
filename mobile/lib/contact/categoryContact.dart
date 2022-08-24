import 'package:demo5/contact/contacts.dart';
import 'package:flutter/material.dart';
import '../NavBar.dart';

class CategoryContact extends StatelessWidget {
  final Category _contacts = Category("");

  CategoryContact({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    _contacts.initializeCategory();
    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Kontaktarten',
            style:
                TextStyle(height: 1.00, fontSize: 25.00, color: Colors.white)),
        centerTitle: true,
      ),
      drawer: const NavBar(),
      body: ListView.builder(
          itemBuilder: _itemBuilder,
          itemCount: _contacts.getCategoryListLength()),
    ));
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
    categoryList.add(Category("Supplier"));
    categoryList.add(Category("Client"));
    categoryList.add(Category("Partner"));
    categoryList.add(Category("ProspectiveClient"));
    categoryList.add(Category("NoTargetGroup"));
  }

  String getProductCategoryName(int index) {
    return categoryList[index].contactCategory;
  }

  int getCategoryListLength() {
    return categoryList.length;
  }
}
