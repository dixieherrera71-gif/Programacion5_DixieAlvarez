Public MustInherit Class Cuenta

    Public Property Numero As String
    Public Property NombreAsociado As String
    Public Property Saldo As Decimal

    Public Sub New(numero As String, nombreAsociado As String, saldoInicial As Decimal)

        If String.IsNullOrWhiteSpace(numero) Then
            Throw New ArgumentException("El número de cuenta es obligatorio.")
        End If

        If String.IsNullOrWhiteSpace(nombreAsociado) Then
            Throw New ArgumentException("El nombre del asociado es obligatorio.")
        End If

        If saldoInicial < 0 Then
            Throw New ArgumentException("El saldo inicial no puede ser negativo.")
        End If

        Me.Numero = numero
        Me.NombreAsociado = nombreAsociado
        Me.Saldo = saldoInicial

    End Sub

    Public MustOverride Function CalcularInteresAnual() As Decimal

    Public MustOverride Function PuedeRetirar(monto As Decimal, mes As Integer) As Boolean

    Public Overridable Sub Retirar(monto As Decimal, mes As Integer)

        If monto <= 0 Then
            Throw New ArgumentException("El monto del retiro debe ser mayor que cero.")
        End If

        If monto > Saldo Then
            Throw New InvalidOperationException("El retiro no puede superar el saldo disponible.")
        End If

        If Not PuedeRetirar(monto, mes) Then
            Throw New InvalidOperationException("El retiro no está permitido para esta cuenta.")
        End If

        Saldo -= monto

    End Sub

    Public Overrides Function ToString() As String
        Return $"{Numero} - {NombreAsociado} - Saldo: ₡{Saldo:N2}"
    End Function

End Class