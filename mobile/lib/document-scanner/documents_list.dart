import 'dart:io';
import 'package:flutter/material.dart';

class DocumentList extends StatefulWidget {
  final File? scannedDocument;
  const DocumentList({Key? key, this.scannedDocument}) : super(key: key);

  @override
  _DocumentListState createState() => _DocumentListState();
}

class _DocumentListState extends State<DocumentList> {
  
  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Scaffold(
      appBar: AppBar(
        title: const Text('Gescannte Dokumente'),
      ),
      body: ListView(
        children: [
          ListTile(
              leading: const Icon(Icons.image),
              title: Text(widget.scannedDocument!.path.split('/').last),
              onTap: () => {
                    /*Navigator.of(context).push(MaterialPageRoute(
                        builder: (context) =>
                            Document(scannedDocument: widget.scannedDocument)))*/
                  },
                  
              trailing: const Icon(Icons.edit))
        ],
      ),
    ));
  }
}



/*appBar: AppBar(
          title: const Text('Lieferscheine',
              style: TextStyle(
                  height: 1.00, fontSize: 25.00, color: Colors.white)),
          centerTitle: true,
        ),
        body: Center(
          child: MaterialButton(
              child: const Text(
                "Lieferschein scannen",
                style: TextStyle(color: Colors.white),
              ),
              color: Colors.purple,
              onPressed: () {
                Navigator.push(context,
                    MaterialPageRoute(builder: (context) => const Scanner()));
              }),
        ),*/
