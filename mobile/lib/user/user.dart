import '../companies/companies.dart';

class User {
  final String firstName;
  final String lastName;
  final Company company;
  final String email;

  User(this.firstName, this.lastName, this.email, this.company);
}
