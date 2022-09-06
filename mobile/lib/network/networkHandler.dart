// ignore: file_names
import 'dart:convert';
import 'package:demo5/address/address.dart';
import 'package:demo5/contact/contacts.dart';
import 'package:demo5/user/user.dart';
import 'package:http/http.dart' as http;
import 'package:demo5/products/product.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class NetworkHandler {
  static final storage = FlutterSecureStorage();
  static String userRole = "";
  static List<Contact> contactList = [];

  static void storeToken(String token) async {
    await storage.write(key: "token", value: token);
  }

  static void storeRole(String role) {
    userRole = role;
  }

  static Future<List<Products>> getProducts() async {
    String url = "https://backend.invoicer.at/api/Products";
    Uri uri = Uri.parse(url);

    String? token = await getToken();
    List<Products> products = [];

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
        products.add(product);
        print(productId);
      }
    }
    return products;
  }

  static Future<List<Contact>> getContacts() async {
    String url = "https://backend.invoicer.at/api/Contacts";
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    List<Contact> contacts = [];

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
        String typeOfContact = obj["typeOfContactEnum"];
        String gender = obj["gender"];
        String? title = obj["title"];
        String firstName = obj["firstName"];
        String lastName = obj["lastName"];
        String nameOfOrganisation = obj["nameOfOrganisation"];
        String phoneNumber = obj["phoneNumber"];
        String email = obj["email"];
        String contactId = obj["id"];
        Address address = Address(
            obj["address"]["street"],
            obj["address"]["zipCode"],
            obj["address"]["city"],
            obj["address"]["country"]);
        Contact contact = Contact(
            contactId,
            typeOfContact,
            gender,
            title,
            firstName,
            lastName,
            nameOfOrganisation,
            phoneNumber,
            email,
            address);
        contacts.add(contact);
      }
    }
    return contacts;
  }

  static Future<List<User>> getUsers() async {
    String url = "https://backend.invoicer.at/api/Users";
    Uri uri = Uri.parse(url);

    String? token = await NetworkHandler.getToken();
    List<User> users = [];

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
        String firstName = obj["firstName"];
        String lastName = obj["lastName"];
        String email = obj["email"];
        String role = obj["role"];
        String userId = obj["id"];
        User user = User(firstName, lastName, email, role, userId);
        users.add(user);
      }
    }
    return users;
  }

  static String getUserRole() {
    return userRole;
  }

  static Future<String?> getToken() async {
    return await storage.read(key: "token");
  }
}
