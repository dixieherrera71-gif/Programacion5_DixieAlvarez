Public Class Form1

    Private ReadOnly _equipos As New List(Of Equipo)
    Private ReadOnly _servicioCarnet As New ServicioCarnet()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CboTipo.Items.Add("Laptop")
        CboTipo.Items.Add("Proyector")
        CboTipo.SelectedIndex = 0
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim nuevo As Equipo
            If CboTipo.SelectedItem.ToString() = "Labtop" Then
                nuevo = New Labtop(lblCodigo.Text, lblDescripcion.Text)
            Else
                nuevo = New Proyector(lblCodigo.Text, lblDescripcion.Text)
            End If

            For Each existente As Equipo In _equipos
                If existente.Codigo = nuevo.Codigo Then
                    Throw New ArgumentException("Ya existe un equipo con ese codigo")


                End If
            Next

            _equipos.Add(nuevo)
            lstEquipos.Items.Add(nuevo)
            txtCodigo.Clear()
            txtDescripcion.Clear()
            txtCodigo.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Data Invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try
    End Sub


    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btncalcular.Click

        Dim seleccionado As Equipo = TryCast(lstEquipos.SelectedItem, Equipo)
        If (seleccionado Is Nothing) Then
            MessageBox.Show("Seleccione un equipo de la lista", "Aviso", MessageBoxButtons.OK,
            MessageBoxIcon.Information)
            Return
        End If

        Dim horas As Integer = CInt(nudHoras.Value)
        Dim multa As Decimal = seleccionado.CalcularMulta(horas)
        lblResultado.Text = $"Multa: {multa:N2} colones"

    End Sub

    Private Async Sub btnVerificar_Click(sender As Object, e As EventArgs) Handles btnVerificar.Click
        lblEstado.Text = "Consultando..."
        Try
            Dim activo As Boolean = Await _servicioCarnet.EstaActivoAsync(txtCarnet.Text)
            lblEstado.Text = If(activo, "Carnet Activo", "Carnet inactivo")
        Catch ex As Exception
            btnVerificar.Enabled = True
        End Try

    End Sub

End Class


