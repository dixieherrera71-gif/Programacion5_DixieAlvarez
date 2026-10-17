Public Class CuentaNavidena
    Inherits Cuenta

    Public Sub New(numero As String, nombreAsociado As String, saldoInicial As Decimal)
        MyBase.New(numero, nombreAsociado, saldoInicial)
    End Sub

    Public Overrides Function CalcularInteresAnual() As Decimal
        Return Saldo * 0.06D
    End Function

    Public Overrides Function PuedeRetirar(monto As Decimal, mes As Integer) As Boolean
        Return mes = 12
    End Function

    Public Overrides Function ToString() As String
        Return $"{Numero} - {NombreAsociado} - Navideña - ₡{Saldo:N2}"
    End Function

End Class