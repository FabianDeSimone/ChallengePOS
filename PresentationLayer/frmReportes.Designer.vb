<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportes
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnReporteVentas = New System.Windows.Forms.Button()
        Me.btnReporteProductos = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnReporteVentas
        '
        Me.btnReporteVentas.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReporteVentas.Location = New System.Drawing.Point(81, 169)
        Me.btnReporteVentas.Name = "btnReporteVentas"
        Me.btnReporteVentas.Size = New System.Drawing.Size(218, 122)
        Me.btnReporteVentas.TabIndex = 0
        Me.btnReporteVentas.Text = "Ventas"
        Me.btnReporteVentas.UseVisualStyleBackColor = True
        '
        'btnReporteProductos
        '
        Me.btnReporteProductos.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReporteProductos.Location = New System.Drawing.Point(426, 169)
        Me.btnReporteProductos.Name = "btnReporteProductos"
        Me.btnReporteProductos.Size = New System.Drawing.Size(218, 122)
        Me.btnReporteProductos.TabIndex = 1
        Me.btnReporteProductos.Text = "Productos"
        Me.btnReporteProductos.UseVisualStyleBackColor = True
        '
        'frmReportes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnReporteProductos)
        Me.Controls.Add(Me.btnReporteVentas)
        Me.Name = "frmReportes"
        Me.Text = "frmReportes"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnReporteVentas As Button
    Friend WithEvents btnReporteProductos As Button
End Class
