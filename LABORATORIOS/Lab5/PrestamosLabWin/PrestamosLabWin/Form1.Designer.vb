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
        txtTipo = New Label()
        CboTipo = New ComboBox()
        lblCodigo = New Label()
        txtCodigo = New TextBox()
        txtDescripcion = New TextBox()
        lblDescripcion = New Label()
        btnAgregar = New Button()
        lstEquipos = New ListBox()
        txtNumerodehoras = New Label()
        nudHoras = New NumericUpDown()
        btncalcular = New Button()
        Label1 = New Label()
        txtCarnet = New TextBox()
        btnVerificar = New Button()
        lblEstado = New Label()
        lblMulta = New Label()
        lblResultado = New Label()
        CType(nudHoras, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' txtTipo
        ' 
        txtTipo.AutoSize = True
        txtTipo.Location = New Point(51, 34)
        txtTipo.Name = "txtTipo"
        txtTipo.Size = New Size(42, 20)
        txtTipo.TabIndex = 0
        txtTipo.Text = "Tipo:"
        ' 
        ' CboTipo
        ' 
        CboTipo.FormattingEnabled = True
        CboTipo.Location = New Point(123, 34)
        CboTipo.Name = "CboTipo"
        CboTipo.Size = New Size(280, 28)
        CboTipo.TabIndex = 1
        ' 
        ' lblCodigo
        ' 
        lblCodigo.AutoSize = True
        lblCodigo.Location = New Point(51, 99)
        lblCodigo.Name = "lblCodigo"
        lblCodigo.Size = New Size(61, 20)
        lblCodigo.TabIndex = 2
        lblCodigo.Text = "Código:"
        ' 
        ' txtCodigo
        ' 
        txtCodigo.Location = New Point(118, 95)
        txtCodigo.Name = "txtCodigo"
        txtCodigo.Size = New Size(285, 27)
        txtCodigo.TabIndex = 3
        ' 
        ' txtDescripcion
        ' 
        txtDescripcion.Location = New Point(147, 157)
        txtDescripcion.Name = "txtDescripcion"
        txtDescripcion.Size = New Size(285, 27)
        txtDescripcion.TabIndex = 5
        ' 
        ' lblDescripcion
        ' 
        lblDescripcion.AutoSize = True
        lblDescripcion.Location = New Point(51, 160)
        lblDescripcion.Name = "lblDescripcion"
        lblDescripcion.Size = New Size(90, 20)
        lblDescripcion.TabIndex = 6
        lblDescripcion.Text = "Descripcion:"
        ' 
        ' btnAgregar
        ' 
        btnAgregar.Location = New Point(51, 224)
        btnAgregar.Name = "btnAgregar"
        btnAgregar.Size = New Size(381, 67)
        btnAgregar.TabIndex = 7
        btnAgregar.Text = "Agregar equipo"
        btnAgregar.UseVisualStyleBackColor = True
        ' 
        ' lstEquipos
        ' 
        lstEquipos.FormattingEnabled = True
        lstEquipos.Location = New Point(472, 21)
        lstEquipos.Name = "lstEquipos"
        lstEquipos.Size = New Size(405, 424)
        lstEquipos.TabIndex = 8
        ' 
        ' txtNumerodehoras
        ' 
        txtNumerodehoras.AutoSize = True
        txtNumerodehoras.Location = New Point(51, 335)
        txtNumerodehoras.Name = "txtNumerodehoras"
        txtNumerodehoras.Size = New Size(127, 20)
        txtNumerodehoras.TabIndex = 9
        txtNumerodehoras.Text = "Numero de horas:"
        ' 
        ' nudHoras
        ' 
        nudHoras.Location = New Point(193, 335)
        nudHoras.Name = "nudHoras"
        nudHoras.Size = New Size(216, 27)
        nudHoras.TabIndex = 10
        ' 
        ' btncalcular
        ' 
        btncalcular.Location = New Point(51, 416)
        btncalcular.Name = "btncalcular"
        btncalcular.Size = New Size(381, 63)
        btncalcular.TabIndex = 11
        btncalcular.Text = "Calcular Multa"
        btncalcular.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(51, 578)
        Label1.Name = "Label1"
        Label1.Size = New Size(55, 20)
        Label1.TabIndex = 12
        Label1.Text = "Carnet:"
        ' 
        ' txtCarnet
        ' 
        txtCarnet.Location = New Point(123, 575)
        txtCarnet.Name = "txtCarnet"
        txtCarnet.Size = New Size(285, 27)
        txtCarnet.TabIndex = 13
        ' 
        ' btnVerificar
        ' 
        btnVerificar.Location = New Point(51, 621)
        btnVerificar.Name = "btnVerificar"
        btnVerificar.Size = New Size(381, 60)
        btnVerificar.TabIndex = 14
        btnVerificar.Text = "Verificar Carnet"
        btnVerificar.UseVisualStyleBackColor = True
        ' 
        ' lblEstado
        ' 
        lblEstado.AutoSize = True
        lblEstado.Font = New Font("Sylfaen", 10.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblEstado.Location = New Point(43, 708)
        lblEstado.Name = "lblEstado"
        lblEstado.Size = New Size(135, 23)
        lblEstado.TabIndex = 15
        lblEstado.Text = "(Sin Consultar)"
        ' 
        ' lblMulta
        ' 
        lblMulta.AutoSize = True
        lblMulta.Location = New Point(51, 523)
        lblMulta.Name = "lblMulta"
        lblMulta.Size = New Size(47, 20)
        lblMulta.TabIndex = 16
        lblMulta.Text = "Multa"
        ' 
        ' lblResultado
        ' 
        lblResultado.AutoSize = True
        lblResultado.Location = New Point(125, 523)
        lblResultado.Name = "lblResultado"
        lblResultado.Size = New Size(17, 20)
        lblResultado.TabIndex = 17
        lblResultado.Text = "0"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(898, 800)
        Controls.Add(lblResultado)
        Controls.Add(lblMulta)
        Controls.Add(lblEstado)
        Controls.Add(btnVerificar)
        Controls.Add(txtCarnet)
        Controls.Add(Label1)
        Controls.Add(btncalcular)
        Controls.Add(nudHoras)
        Controls.Add(txtNumerodehoras)
        Controls.Add(lstEquipos)
        Controls.Add(btnAgregar)
        Controls.Add(lblDescripcion)
        Controls.Add(txtDescripcion)
        Controls.Add(txtCodigo)
        Controls.Add(lblCodigo)
        Controls.Add(CboTipo)
        Controls.Add(txtTipo)
        Name = "Form1"
        Text = "Prestamo del Laboratorio"
        CType(nudHoras, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtTipo As Label
    Friend WithEvents CboTipo As ComboBox
    Friend WithEvents lblCodigo As Label
    Friend WithEvents txtCodigo As TextBox
    Friend WithEvents txtDescripcion As TextBox
    Friend WithEvents lblDescripcion As Label
    Friend WithEvents btnAgregar As Button
    Friend WithEvents lstEquipos As ListBox
    Friend WithEvents txtNumerodehoras As Label
    Friend WithEvents nudHoras As NumericUpDown
    Friend WithEvents btncalcular As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtCarnet As TextBox
    Friend WithEvents btnVerificar As Button
    Friend WithEvents lblEstado As Label
    Friend WithEvents lblMulta As Label
    Friend WithEvents lblResultado As Label

End Class
