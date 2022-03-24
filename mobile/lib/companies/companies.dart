import 'package:demo5/address/address.dart';
import 'package:demo5/companies/addCompany.dart';
import 'package:demo5/navbar.dart';
import 'package:flutter/material.dart';
import 'package:json_annotation/json_annotation.dart';

class Companies extends StatefulWidget {
  const Companies({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _CompaniesState();
}

class _CompaniesState extends State<Companies> {
  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        appBar: AppBar(
          title: const Text('Unternehmen',
              style: TextStyle(
                  height: 1.00, fontSize: 25.00, color: Colors.white)),
          centerTitle: true,
        ),
        drawer: const NavBar(),
      ),
    );
  }
}

@JsonSerializable(explicitToJson: true)
class Company {
  final String companyName;
  final String email;
  final String phoneNumber;
  final Address addresses;
  String iban = "fijo343fesf";
  String bic = "fsjjnjioe";
  String bankname = "oberbank";

  Company(this.companyName, this.email, this.phoneNumber, this.addresses);

  Map<String, dynamic> toJson() => _$CompanyToJson(this);

  Map<String, dynamic> _$CompanyToJson(Company instance) => <String, dynamic>{
        "companyName": companyName,
        "email": email,
        "phoneNumber": phoneNumber,
        "bankName": bankname,
        "iban": iban,
        "bic": bic,
        "address": addresses
      };
}
