import 'package:json_annotation/json_annotation.dart';

@JsonSerializable(explicitToJson: true)
class Address {
  final String street;
  final String zipCode;
  final String city;
  final String country;

  Address(this.street, this.zipCode, this.city, this.country);

  Map<String, dynamic> toJson() => _$AddressToJson(this);

  Map<String, dynamic> _$AddressToJson(Address instance) => <String, dynamic>{
        "street": street,
        "zipCode": zipCode,
        "city": city,
        "country": country
      };
}
