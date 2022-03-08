# Backend - Dokumentation

## Registrierung
FÜR DIE ERSTE REGISTRIERUNG!<br>
<u><b>Api:</b></u> <b>POST</b> /api/Auth/register
<br>
<u><b>Anwendung:</b></u>
<br>
Im Body wird folgendes mitgesendet:<br>
```json
{
  "user": {
    "firstName": "Vorname",
    "lastName": "Nachname",
    "company": {
      "companyName": "Firmenname",
      "email": "Firmenmail",
      "phoneNumber": "Firmentelefonnummer",
      "addresses": [
        {
          "street": "Firmenstraße mit Hausnummer",
          "zipCode": "Postleitzahl",
          "city": "Stadt",
          "country": "Land"
        }
      ]
    },
    "email": "Useremail"
  },
  "password": "Passwort"
}
```


## Login
<u><b>Api:</b></u> <b>GET</b> /api/Auth/login
<br>
Im Header Email und Passwort in Base64-Format mitschicken! 

## Companies
| API      | METHOD | HEADER | BODY | BESCHREIBUNG 
| ----------- | ----------- |  ----------- |  ----------- |   ----------- |
| /api/Companies/ | GET | Auth-Token | Leer | Liefert die Firma.|
| /api/Companies/ | PUT |  Auth-Token | Company | Um die Firma zu bearbeiten.|
| /api/Companies/ | DELETE |  Auth-Token | Leer | Um die Firma zu löschen.|
| /api/Companies/add-address/{companyId} | PUT |  Auth-Token | Address-Object | Adresse hinzufügen.|
| /api/Companies/add-contact/{companyId} | PUT |  Auth-Token | Contact-Object | Kontakt hinzufügen.|
| /api/Companies/add-delivery-note/{companyId} | PUT |  Auth-Token | DeliveryNote-Object | Lieferschein hinzufügen.|
| /api/Companies/add-invoice/{companyId} | PUT |  Auth-Token | Invoice-Object | Rechnung hinzufügen.|
| /api/Companies/add-offer/{companyId} | PUT |  Auth-Token | Offer-Object | Angebot hinzufügen.|
| /api/Companies/add-order-confirmation/{companyId} | PUT |  Auth-Token | OrderConfirmation-Object | Auftragsbestätigung hinzufügen.|
| /api/Companies/add-product/{companyId} | PUT |  Auth-Token | Product-Object | Produkt hinzufügen.|
| /api/Companies/add-user/{companyId} | PUT |  Auth-Token | UserAddDto-Object | Benutzer hinzufügen.|
| /api/Companies/delete-address/{companyId}/{addressId} | PUT |  Auth-Token | Leer | Adresse löschen.|
| ... | ... |  ... | ... | ... |


