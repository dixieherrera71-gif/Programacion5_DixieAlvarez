Imports System

Module Program
    Sub Main(args As String())

        Dim nombre As String
        Dim edad As Integer
        Dim sueldo As Double

        nombre = "Juan"
        edad = 20
        sueldo = 150000

        sueldo = sueldo * 1.35

        If sueldo > 10000 Then
            Console.WriteLine("Super sueldo")
        Else
            Console.WriteLine("Mal Sueldo")

        End If

        Dim contador As Integer = 0

        While contador < 10

            Console.WriteLine(contador)
            contador += 1 ' contador = contador + 1

        End While

        For index As Integer = 10 To 1 Step -1
            Console.WriteLine(index)
        Next
        Console.WriteLine(sueldo)

        Dim cliente As New Cliente("6-04980455", "Dixie Alvarez Herrera")
        cliente.Nombre = "Yanella"


        Console.WriteLine(cliente)







        Console.WriteLine("Hello World!")
    End Sub
End Module
