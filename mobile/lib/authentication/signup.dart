import 'package:http/http.dart' as http;
import 'package:demo5/products/product.dart';
import 'package:flutter/material.dart';

class SignUp extends StatelessWidget {
  const SignUp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Registrierung',
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
                        'Vorname',
                        style: TextStyle(fontSize: 20.00),
                      ),
                    ),
                    TextFormField(
                      autofocus: false,
                      decoration: InputDecoration(
                          border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(100.0)),
                          hintText: 'Vorname eingeben',
                          hintStyle: const TextStyle(fontSize: 20.00)),
                      style: const TextStyle(fontSize: 20.00),
                    ),
                    const SizedBox(
                      height: 25.00,
                    ),
                    const Align(
                      alignment: Alignment(-0.95, 1),
                      child: Text(
                        'Nachname',
                        style: TextStyle(fontSize: 20.00),
                      ),
                    ),
                    TextFormField(
                      autofocus: false,
                      decoration: InputDecoration(
                          border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(100.0)),
                          hintText: 'Nachname eingeben',
                          hintStyle: const TextStyle(fontSize: 20.00)),
                      style: const TextStyle(fontSize: 20.00),
                    ),
                    const SizedBox(
                      height: 25.00,
                    ),
                    const Align(
                      alignment: Alignment(-0.95, 1),
                      child: Text(
                        'Email',
                        style: TextStyle(fontSize: 20.00),
                      ),
                    ),
                    TextFormField(
                      autofocus: false,
                      decoration: InputDecoration(
                          border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(100.0)),
                          hintText: 'Email eingeben',
                          hintStyle: const TextStyle(fontSize: 20.00)),
                      style: const TextStyle(fontSize: 20.00),
                    ),
                    const SizedBox(
                      height: 25.00,
                    ),
                    const Align(
                      alignment: Alignment(-0.95, 1),
                      child:
                          Text('Passwort', style: TextStyle(fontSize: 20.00)),
                    ),
                    TextFormField(
                      autofocus: false,
                      obscureText: true,
                      decoration: InputDecoration(
                        border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(100.0)),
                        hintText: 'Passwort eingeben',
                        hintStyle: const TextStyle(fontSize: 20.00),
                        prefixIcon: const Padding(
                          padding:
                              EdgeInsets.only(top: 0, right: 12, bottom: 0),
                          child: Icon(Icons.remove_red_eye,
                              size: 22, color: Colors.grey),
                        ),
                      ),
                      style: const TextStyle(fontSize: 20.00),
                    ),
                    const SizedBox(
                      height: 25.00,
                    ),
                    MaterialButton(
                      onPressed: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => const Product()),
                        );
                      },
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(100)),
                      color: Colors.purpleAccent[700],
                      child: const Text('Registrieren',
                          style: TextStyle(fontSize: 22.00, height: 1.35)),
                      textColor: Colors.white,
                      height: 50.00,
                      minWidth: 477.00,
                    ),
                  ],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  /*Future<Album> fetchAlbum() async {  
    final response = await http.post("/api/Auth/register");

    if (response.statusCode == 200) {
      // If the server did return a 200 OK response,
      // then parse the JSON.
      return Album.fromJson(jsonDecode(response.body));
    } else {
      // If the server did not return a 200 OK response,
      // then throw an exception.
      throw Exception('Failed to load album');
    }
  }*/
}
