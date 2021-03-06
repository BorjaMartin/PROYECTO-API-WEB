# Pruebas

# Prueba 1

Pedido 1 y 2 tienen misma dirección, ciudad, estado y código postal con diferente Numero de tarjeta

Pedido 1 y 3 tienen mismos datos con diferente identificador, los pedidos serían correctos

JSON 1 

```json
{
    "purchases": [
        {
            "orderId": 1,
            "dealId": 1,
            "emailAddress": "bugs@bunny.com",
            "streetAddress": "123 Sesame St.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 12345689010
        },
        {
            "orderId": 2,
            "dealId": 1,
            "emailAddress": "elmer@fudd.com",
            "streetAddress": "123 Sesame St.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 10987654321
        },
        {
            "orderId": 3,
            "dealId": 2,
            "emailAddress": "bugs@bunny.com",
            "streetAddress": "123 Sesame St.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 12345689010
        }
    ]
}
```

Repuesta 

```json
[
  "1",
  "2"
]
```

# Prueba 2

Pedido 1 y 3 tienen mismos datos con diferente identificador, pero numero de tarjetas diferentes. No son validos

JSON 1 

```json
{
    "purchases": [
        {
            "orderId": 1,
            "dealId": 1,
            "emailAddress": "bugs@bunny.com",
            "streetAddress": "123 Sesame St.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 12345689010
        },
        {
            "orderId": 2,
            "dealId": 1,
            "emailAddress": "elmer@fudd.com",
            "streetAddress": "123 Sesame Rd.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 10987654321
        },
        {
            "orderId": 3,
            "dealId": 2,
            "emailAddress": "bugs@bunny.com",
            "streetAddress": "123 Sesame Street",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 12345689110
        }
    ]
}
```

Repuesta 

```json
[
  "1",
  "3"
]
```

# Prueba 3

Pedido 1 y 3 tienen mismos datos con diferente identificador, pero numero de tarjetas diferentes. No son validos

JSON 1 

```json
{
    "purchases": [
        {
            "orderId": 1,
            "dealId": 1,
            "emailAddress": "bugs@bunny.com",
            "streetAddress": "123 Sesame St.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 12345689010
        },
        {
            "orderId": 2,
            "dealId": 1,
            "emailAddress": "elmer@fudd.com",
            "streetAddress": "123 Sesame Rd.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 10987654321
        },
        {
            "orderId": 3,
            "dealId": 2,
            "emailAddress": "bugs@bunny.com",
            "streetAddress": "123 Sesame Street",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 12345689010
        }
    ]
}
```

Repuesta 

```json
[
  "1",
  "3"
]
```

# Prueba 4

Pedido 1 y 3 tienen mismos datos omitiendo el "." en el correo  del pedido 3

JSON 1 

```json
{
    "purchases": [
        {
            "orderId": 1,
            "dealId": 1,
            "emailAddress": "bugs@bunny.com",
            "streetAddress": "123 Sesame St.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 12345689010
        },
        {
            "orderId": 2,
            "dealId": 1,
            "emailAddress": "elmer@fudd.com",
            "streetAddress": "123 Sesame Rd.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 10987654321
        },
        {
            "orderId": 3,
            "dealId": 2,
            "emailAddress": "bu.gs@bunny.com",
            "streetAddress": "123 Sesame Street",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 12345689010
        }
    ]
}
```

Repuesta 

```json
[]
```

# Prueba 5

Pedido 1 y 3 tienen mismos datos omitiendo el símbolo "+" y posterior en el correo  del pedido 3 pero diferente numero de tarjeta. 

JSON 1 

```json
{
    "purchases": [
        {
            "orderId": 1,
            "dealId": 1,
            "emailAddress": "bugs@bunny.com",
            "streetAddress": "123 Sesame St.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 12345689010
        },
        {
            "orderId": 2,
            "dealId": 1,
            "emailAddress": "elmer@fudd.com",
            "streetAddress": "123 Sesame Rd.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 10987654321
        },
        {
            "orderId": 3,
            "dealId": 2,
            "emailAddress": "bu.gs+ab43@bunny.com",
            "streetAddress": "123 Sesame Street",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 10987653321
        }
    ]
}
```

Repuesta 

```json
[
  "1",
  "3"
]
```

# Prueba 6

Pedido 1 y 3 tienen mismos datos y numero de tarjeta diferente. Mismo estado y dirección escrito en abreviatura. 

JSON 1 

```json
{
    "purchases": [
        {
            "orderId": 1,
            "dealId": 1,
            "emailAddress": "bugs@bunny.com",
            "streetAddress": "123 Sesame St.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 12345689010
        },
        {
            "orderId": 2,
            "dealId": 1,
            "emailAddress": "elmer@fudd.com",
            "streetAddress": "123 Sesame Rd.",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 10987654321
        },
        {
            "orderId": 3,
            "dealId": 2,
            "emailAddress": "bu.gs+ab43@bunny.com",
            "streetAddress": "123 Sesame Street",
            "city": "New York",
            "state": "NY",
            "zipCode": "10011",
            "creditCardNumber": 10987653321
        }
    ]
}
```

Repuesta 

```json
[
  "1",
  "3"
]
```