// ignore_for_file: prefer_const_literals_to_create_immutables, prefer_const_constructors
import 'package:demo5/addProduct.dart';
import 'package:flutter/material.dart';
import 'NavBar.dart';
import 'package:syncfusion_flutter_datagrid/datagrid.dart';
import 'package:syncfusion_flutter_core/theme.dart';

class Product extends StatefulWidget {
  const Product({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _ProductsState();
}

class _ProductsState extends State<Product> {
  late List<Products> _products;
  late ProductsDataSource _productsDataSource;

  @override
  void initState() {
    _products = getProductsData();
    _productsDataSource = ProductsDataSource(_products);
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        appBar: AppBar(
          title: const Text('Produkte',
              style: TextStyle(
                  height: 1.00, fontSize: 25.00, color: Colors.white)),
          centerTitle: true,
        ),
        drawer: NavBar(),
        body: SfDataGridTheme(
          data: SfDataGridThemeData(
              headerColor: Colors.purple,
              brightness: Brightness.light,
              gridLineColor: Colors.black,
              gridLineStrokeWidth: 0.5,
              sortIconColor: Colors.orange,
              selectionColor: Colors.yellow),
          child: SfDataGrid(
            source: _productsDataSource,
            selectionMode: SelectionMode.multiple,
            columnWidthMode: ColumnWidthMode.lastColumnFill,
            allowSorting: true,
            columns: [
              GridColumn(
                  columnName: "productName",
                  label: Container(
                    padding: EdgeInsets.symmetric(horizontal: 16),
                    alignment: Alignment.centerLeft,
                    child: Text(
                      "Produkt Name",
                      overflow: TextOverflow.ellipsis,
                      style: TextStyle(
                          fontWeight: FontWeight.bold, color: Colors.white),
                    ),
                  )),
              GridColumn(
                  columnName: "productCategory",
                  label: Container(
                    padding: EdgeInsets.symmetric(horizontal: 16),
                    alignment: Alignment.centerLeft,
                    child: Text(
                      "Kategorie",
                      overflow: TextOverflow.ellipsis,
                      style: TextStyle(
                          fontWeight: FontWeight.bold, color: Colors.white),
                    ),
                  )),
              GridColumn(
                  columnName: "description",
                  label: Container(
                    padding: EdgeInsets.symmetric(horizontal: 5),
                    alignment: Alignment.centerLeft,
                    child: Text(
                      "Beschreibung",
                      overflow: TextOverflow.ellipsis,
                      style: TextStyle(
                          fontWeight: FontWeight.bold, color: Colors.white),
                    ),
                  ))
            ],
          ),
        ),
        floatingActionButton: FloatingActionButton(
          onPressed: () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => AddProduct()),
            );
          },
          child: Image.asset("lib/assets/add.png"),
        ),
      ),
    );
  }

  List<Products> getProductsData() {
    return [
      Products("Red Bull", "Getränke", "Koffeinhalitges Getränk"),
      Products("Äpfel", "Obst", "Frische Äpfel aus Spanien"),
      Products("Eistee", "Getränke", "Zuckerhalitges Getränk"),
      Products("Eistee2", "Getränke", "Zuckerhalitges Getränk"),
      Products("Eistee", "Getränke", "Zuckerhalitges Getränk"),
      Products("Eistee", "Getränke", "Zuckerhalitges Getränk"),
      Products("Eistee", "Getränke", "Zuckerhalitges Getränk"),
      Products("Eistee", "Getränke", "Zuckerhalitges Getränk"),
      Products("Eistee", "Getränke", "Zuckerhalitges Getränk"),
      Products("Red Bull", "Getränke", "Koffeinhalitges Getränk"),
      Products("Red Bull", "Getränke", "Koffeinhalitges Getränk"),
      Products("Red Bull", "Getränke", "Koffeinhalitges Getränk"),
      Products("Red Bull", "Getränke", "Koffeinhalitges Getränk"),
      Products("Red Bull", "Getränke", "Koffeinhalitges Getränk"),
      Products("Red Bull", "Getränke", "Koffeinhalitges Getränk"),
      Products("Red Bull", "Getränke", "Koffeinhalitges Getränk"),
      Products("Red Bull", "Getränke", "Koffeinhalitges Getränk"),
    ];
  }
}

class ProductsDataSource extends DataGridSource {
  ProductsDataSource(List<Products> products) {
    dataGridRows = products
        .map<DataGridRow>((dataGridRow) => DataGridRow(cells: [
              DataGridCell<String>(
                  columnName: "Produkt Name", value: dataGridRow.productName),
              DataGridCell<String>(
                  columnName: "Kategorie", value: dataGridRow.productCategory),
              DataGridCell<String>(
                  columnName: "Beschreibung", value: dataGridRow.description)
            ]))
        .toList();
  }

  late List<DataGridRow> dataGridRows;

  @override
  List<DataGridRow> get rows => dataGridRows;

  @override
  DataGridRowAdapter buildRow(DataGridRow row) {
    return DataGridRowAdapter(
        cells: row.getCells().map<Widget>((dataGridCell) {
      return Container(
        padding: EdgeInsets.symmetric(horizontal: 16),
        alignment: Alignment.centerLeft,
        child: Text(
          dataGridCell.value.toString(),
          overflow: TextOverflow.ellipsis,
        ),
      );
    }).toList());
  }
}

class Products {
  Products(this.productName, this.productCategory, this.description);

  final String productName;
  final String productCategory;
  final String description;
}
