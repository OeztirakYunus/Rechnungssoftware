import 'package:json_annotation/json_annotation.dart';

import '../companies/companies.dart';

@JsonSerializable(explicitToJson: true)
class User {
  final String firstName;
  final String lastName;
  final Company company;
  final String email;

  User(this.firstName, this.lastName, this.email, this.company);

  Map<String, dynamic> toJson() => _$UserToJson(this);

  Map<String, dynamic> _$UserToJson(User instance) => <String, dynamic>{
        "firstName": firstName,
        "lastName": lastName,
        "company": company,
        "email": email
      };
}
