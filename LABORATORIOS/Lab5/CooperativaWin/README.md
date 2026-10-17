# CooperativaWin

Aplicación de Windows Forms desarrollada en Visual Basic .NET para gestionar cuentas de una cooperativa de ahorro y crédito.

## Funcionalidades

- Registro de cuentas a la vista y cuentas navideñas.
- Validación de datos de las cuentas.
- Cálculo del interés anual.
- Realización de retiros según las reglas de cada cuenta.
- Consulta de historial crediticio de manera asíncrona.

## Preguntas de análisis

### 1. Cuenta juvenil

Si la cooperativa agrega una cuenta juvenil con un 4 % anual y retiros máximos de 25 000 por operación, agregaría una nueva clase derivada de Cuenta, por ejemplo CuentaJuvenil.vb.

En esa clase se definiría el cálculo del interés del 4 % y la regla del retiro máximo de 25 000.

También sería necesario modificar la parte donde se crea la cuenta seleccionada por el usuario para permitir registrar el nuevo tipo de cuenta.

No sería necesario modificar CuentaVista.vb ni CuentaNavidena.vb, porque cada clase mantiene sus propias reglas.

### 2. Validación del saldo negativo

La validación del saldo inicial negativo se colocó en la clase Cuenta y no en el evento del botón.

Esto permite mantener las reglas del negocio fuera del formulario y garantiza que ninguna cuenta pueda crearse con un saldo inicial negativo, independientemente del lugar desde donde se intente crear.

### 3. Consulta sin Await

Si la consulta crediticia utilizara una pausa que bloqueara el hilo y no utilizara Await, la ventana dejaría de responder durante los tres segundos.

El usuario no podría mover la ventana ni interactuar con los controles hasta que terminara la consulta.

Con Await la interfaz continúa respondiendo mientras se realiza la espera.

## Uso de inteligencia artificial

Se utilizó inteligencia artificial generativa como apoyo para comprender los requisitos de la práctica, organizar la estructura de las clases y revisar el código durante el desarrollo.

## Pruebas realizadas

La aplicación fue probada con los casos indicados en la práctica:

- Cálculo de interés para cuenta a la vista.
- Cálculo de interés para cuenta navideña.
- Retiros permitidos y rechazados según saldo y mes.
- Validación de números de cuenta duplicados.
- Validación de saldo inicial negativo.
- Consulta de historial crediticio con y sin morosidad.
- Verificación de que la interfaz continúa respondiendo durante Actrla consulta.