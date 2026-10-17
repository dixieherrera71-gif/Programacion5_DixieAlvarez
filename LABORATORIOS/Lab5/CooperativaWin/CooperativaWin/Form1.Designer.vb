<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        lblTipoCuenta = New Label()
        cboTipoCuenta = New ComboBox()
        lblNumeroCuenta = New Label()
        lblNombre = New Label()
        lblSaldoInicial = New Label()
        txtNumeroCuenta = New TextBox()
        txtNombre = New TextBox()
        txtSaldoInicial = New TextBox()
        btnRegistrar = New Button()
        btnInteres = New Button()
        lblInteresTitulo = New Label()
        lblInteres = New Label()
        lstCuentas = New ListBox()
        grpRegistro = New GroupBox()
        lblMontoRetiro = New Label()
        lblMes = New Label()
        txtMontoRetiro = New TextBox()
        cboMes = New ComboBox()
        btnRetirar = New Button()
        grpRetiro = New GroupBox()
        grpCuentas = New GroupBox()
        grpHistorial = New GroupBox()
        lblCedula = New Label()
        lblResultadoHistorial = New Label()
        txtCedula = New TextBox()
        btnConsultarHistorial = New Button()
        SuspendLayout()
        ' 
        ' lblTipoCuenta
        ' 
        lblTipoCuenta.AutoSize = True
        lblTipoCuenta.Location = New Point(38, 81)
        lblTipoCuenta.Margin = New Padding(4, 0, 4, 0)
        lblTipoCuenta.Name = "lblTipoCuenta"
        lblTipoCuenta.Size = New Size(131, 23)
        lblTipoCuenta.TabIndex = 0
        lblTipoCuenta.Text = "Tipo de Cuenta:"
        ' 
        ' cboTipoCuenta
        ' 
        cboTipoCuenta.FormattingEnabled = True
        cboTipoCuenta.Items.AddRange(New Object() {"A la vista", " Navideña"})
        cboTipoCuenta.Location = New Point(188, 78)
        cboTipoCuenta.Margin = New Padding(4, 3, 4, 3)
        cboTipoCuenta.Name = "cboTipoCuenta"
        cboTipoCuenta.Size = New Size(236, 31)
        cboTipoCuenta.TabIndex = 1
        ' 
        ' lblNumeroCuenta
        ' 
        lblNumeroCuenta.AutoSize = True
        lblNumeroCuenta.Location = New Point(38, 139)
        lblNumeroCuenta.Margin = New Padding(4, 0, 4, 0)
        lblNumeroCuenta.Name = "lblNumeroCuenta"
        lblNumeroCuenta.Size = New Size(161, 23)
        lblNumeroCuenta.TabIndex = 2
        lblNumeroCuenta.Text = "Número de Cuenta:"
        ' 
        ' lblNombre
        ' 
        lblNombre.AutoSize = True
        lblNombre.Location = New Point(38, 211)
        lblNombre.Margin = New Padding(4, 0, 4, 0)
        lblNombre.Name = "lblNombre"
        lblNombre.Size = New Size(179, 23)
        lblNombre.TabIndex = 3
        lblNombre.Text = "Nombre del Asociado:"
        ' 
        ' lblSaldoInicial
        ' 
        lblSaldoInicial.AutoSize = True
        lblSaldoInicial.Location = New Point(38, 277)
        lblSaldoInicial.Margin = New Padding(4, 0, 4, 0)
        lblSaldoInicial.Name = "lblSaldoInicial"
        lblSaldoInicial.Size = New Size(105, 23)
        lblSaldoInicial.TabIndex = 4
        lblSaldoInicial.Text = "Saldo Inicial:"
        ' 
        ' txtNumeroCuenta
        ' 
        txtNumeroCuenta.Location = New Point(223, 136)
        txtNumeroCuenta.Margin = New Padding(4, 3, 4, 3)
        txtNumeroCuenta.Name = "txtNumeroCuenta"
        txtNumeroCuenta.Size = New Size(234, 30)
        txtNumeroCuenta.TabIndex = 5
        ' 
        ' txtNombre
        ' 
        txtNombre.Location = New Point(225, 211)
        txtNombre.Margin = New Padding(4, 3, 4, 3)
        txtNombre.Name = "txtNombre"
        txtNombre.Size = New Size(264, 30)
        txtNombre.TabIndex = 6
        ' 
        ' txtSaldoInicial
        ' 
        txtSaldoInicial.Location = New Point(168, 277)
        txtSaldoInicial.Margin = New Padding(4, 3, 4, 3)
        txtSaldoInicial.Name = "txtSaldoInicial"
        txtSaldoInicial.Size = New Size(198, 30)
        txtSaldoInicial.TabIndex = 7
        ' 
        ' btnRegistrar
        ' 
        btnRegistrar.Location = New Point(116, 348)
        btnRegistrar.Margin = New Padding(4, 3, 4, 3)
        btnRegistrar.Name = "btnRegistrar"
        btnRegistrar.Size = New Size(209, 55)
        btnRegistrar.TabIndex = 8
        btnRegistrar.Text = "Registrar Cuenta"
        btnRegistrar.UseVisualStyleBackColor = True
        ' 
        ' btnInteres
        ' 
        btnInteres.Location = New Point(54, 773)
        btnInteres.Margin = New Padding(4, 3, 4, 3)
        btnInteres.Name = "btnInteres"
        btnInteres.Size = New Size(188, 60)
        btnInteres.TabIndex = 9
        btnInteres.Text = "Calcular Interés"
        btnInteres.UseVisualStyleBackColor = True
        ' 
        ' lblInteresTitulo
        ' 
        lblInteresTitulo.AutoSize = True
        lblInteresTitulo.Location = New Point(281, 783)
        lblInteresTitulo.Margin = New Padding(4, 0, 4, 0)
        lblInteresTitulo.Name = "lblInteresTitulo"
        lblInteresTitulo.Size = New Size(115, 23)
        lblInteresTitulo.TabIndex = 10
        lblInteresTitulo.Text = "Interés Anual:"
        ' 
        ' lblInteres
        ' 
        lblInteres.AutoSize = True
        lblInteres.Location = New Point(428, 783)
        lblInteres.Margin = New Padding(4, 0, 4, 0)
        lblInteres.Name = "lblInteres"
        lblInteres.Size = New Size(50, 23)
        lblInteres.TabIndex = 11
        lblInteres.Text = "$0.00"
        ' 
        ' lstCuentas
        ' 
        lstCuentas.FormattingEnabled = True
        lstCuentas.Location = New Point(38, 519)
        lstCuentas.Name = "lstCuentas"
        lstCuentas.Size = New Size(328, 211)
        lstCuentas.TabIndex = 12
        ' 
        ' grpRegistro
        ' 
        grpRegistro.Location = New Point(38, 22)
        grpRegistro.Name = "grpRegistro"
        grpRegistro.Size = New Size(250, 34)
        grpRegistro.TabIndex = 13
        grpRegistro.TabStop = False
        grpRegistro.Text = "Registro de Cuenta"
        ' 
        ' lblMontoRetiro
        ' 
        lblMontoRetiro.AutoSize = True
        lblMontoRetiro.Location = New Point(695, 78)
        lblMontoRetiro.Name = "lblMontoRetiro"
        lblMontoRetiro.Size = New Size(132, 23)
        lblMontoRetiro.TabIndex = 14
        lblMontoRetiro.Text = "Monto a retirar:"
        ' 
        ' lblMes
        ' 
        lblMes.AutoSize = True
        lblMes.Location = New Point(695, 143)
        lblMes.Name = "lblMes"
        lblMes.Size = New Size(46, 23)
        lblMes.TabIndex = 15
        lblMes.Text = "Mes:"
        ' 
        ' txtMontoRetiro
        ' 
        txtMontoRetiro.Location = New Point(839, 75)
        txtMontoRetiro.Name = "txtMontoRetiro"
        txtMontoRetiro.Size = New Size(125, 30)
        txtMontoRetiro.TabIndex = 16
        ' 
        ' cboMes
        ' 
        cboMes.DropDownStyle = ComboBoxStyle.DropDownList
        cboMes.FormattingEnabled = True
        cboMes.Items.AddRange(New Object() {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"})
        cboMes.Location = New Point(765, 143)
        cboMes.Name = "cboMes"
        cboMes.Size = New Size(151, 31)
        cboMes.TabIndex = 17
        ' 
        ' btnRetirar
        ' 
        btnRetirar.Location = New Point(791, 224)
        btnRetirar.Name = "btnRetirar"
        btnRetirar.Size = New Size(189, 51)
        btnRetirar.TabIndex = 18
        btnRetirar.Text = "Retirar"
        btnRetirar.UseVisualStyleBackColor = True
        ' 
        ' grpRetiro
        ' 
        grpRetiro.Location = New Point(695, 22)
        grpRetiro.Name = "grpRetiro"
        grpRetiro.Size = New Size(250, 34)
        grpRetiro.TabIndex = 19
        grpRetiro.TabStop = False
        grpRetiro.Text = "Retiro"
        ' 
        ' grpCuentas
        ' 
        grpCuentas.Location = New Point(38, 448)
        grpCuentas.Name = "grpCuentas"
        grpCuentas.Size = New Size(250, 37)
        grpCuentas.TabIndex = 20
        grpCuentas.TabStop = False
        grpCuentas.Text = "Cuentas Registradas"
        ' 
        ' grpHistorial
        ' 
        grpHistorial.Location = New Point(695, 401)
        grpHistorial.Name = "grpHistorial"
        grpHistorial.Size = New Size(250, 39)
        grpHistorial.TabIndex = 21
        grpHistorial.TabStop = False
        grpHistorial.Text = "Historial Crediticio"
        ' 
        ' lblCedula
        ' 
        lblCedula.AutoSize = True
        lblCedula.Location = New Point(695, 462)
        lblCedula.Name = "lblCedula"
        lblCedula.Size = New Size(67, 23)
        lblCedula.TabIndex = 22
        lblCedula.Text = "Cédula:"
        ' 
        ' lblResultadoHistorial
        ' 
        lblResultadoHistorial.AutoSize = True
        lblResultadoHistorial.Location = New Point(695, 639)
        lblResultadoHistorial.Name = "lblResultadoHistorial"
        lblResultadoHistorial.Size = New Size(196, 23)
        lblResultadoHistorial.TabIndex = 23
        lblResultadoHistorial.Text = "Resultado: Sin Consultar"
        ' 
        ' txtCedula
        ' 
        txtCedula.Location = New Point(778, 462)
        txtCedula.Name = "txtCedula"
        txtCedula.Size = New Size(186, 30)
        txtCedula.TabIndex = 24
        ' 
        ' btnConsultarHistorial
        ' 
        btnConsultarHistorial.Location = New Point(765, 542)
        btnConsultarHistorial.Name = "btnConsultarHistorial"
        btnConsultarHistorial.Size = New Size(180, 55)
        btnConsultarHistorial.TabIndex = 25
        btnConsultarHistorial.Text = "Consultar Historial"
        btnConsultarHistorial.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 23F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1310, 845)
        Controls.Add(btnConsultarHistorial)
        Controls.Add(txtCedula)
        Controls.Add(lblResultadoHistorial)
        Controls.Add(lblCedula)
        Controls.Add(grpHistorial)
        Controls.Add(grpCuentas)
        Controls.Add(grpRetiro)
        Controls.Add(btnRetirar)
        Controls.Add(cboMes)
        Controls.Add(txtMontoRetiro)
        Controls.Add(lblMes)
        Controls.Add(lblMontoRetiro)
        Controls.Add(grpRegistro)
        Controls.Add(lstCuentas)
        Controls.Add(lblInteres)
        Controls.Add(lblInteresTitulo)
        Controls.Add(btnInteres)
        Controls.Add(btnRegistrar)
        Controls.Add(txtSaldoInicial)
        Controls.Add(txtNombre)
        Controls.Add(txtNumeroCuenta)
        Controls.Add(lblSaldoInicial)
        Controls.Add(lblNombre)
        Controls.Add(lblNumeroCuenta)
        Controls.Add(cboTipoCuenta)
        Controls.Add(lblTipoCuenta)
        Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        ForeColor = SystemColors.ActiveCaptionText
        Margin = New Padding(4, 3, 4, 3)
        Name = "Form1"
        Text = "Cooperativa de Ahorrro y Crédito del Sur"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTipoCuenta As Label
    Friend WithEvents cboTipoCuenta As ComboBox
    Friend WithEvents lblNumeroCuenta As Label
    Friend WithEvents lblNombre As Label
    Friend WithEvents lblSaldoInicial As Label
    Friend WithEvents txtNumeroCuenta As TextBox
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents txtSaldoInicial As TextBox
    Friend WithEvents btnRegistrar As Button
    Friend WithEvents btnInteres As Button
    Friend WithEvents lblInteresTitulo As Label
    Friend WithEvents lblInteres As Label
    Friend WithEvents lstCuentas As ListBox
    Friend WithEvents grpRegistro As GroupBox
    Friend WithEvents lblMontoRetiro As Label
    Friend WithEvents lblMes As Label
    Friend WithEvents txtMontoRetiro As TextBox
    Friend WithEvents cboMes As ComboBox
    Friend WithEvents btnRetirar As Button
    Friend WithEvents grpRetiro As GroupBox
    Friend WithEvents grpCuentas As GroupBox
    Friend WithEvents grpHistorial As GroupBox
    Friend WithEvents lblCedula As Label
    Friend WithEvents lblResultadoHistorial As Label
    Friend WithEvents txtCedula As TextBox
    Friend WithEvents btnConsultarHistorial As Button

End Class
