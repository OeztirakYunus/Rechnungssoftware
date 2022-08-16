import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class NetworkHandler {
  static final storage = FlutterSecureStorage();

  static void storeToken(String token) async {
    await storage.write(key: "token", value: token);
  }

  static Future<String?> getToken() async {
    return await storage.read(key: "token");
  }
}
