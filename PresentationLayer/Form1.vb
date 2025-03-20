Imports DataLayer
Public Class Form1
    Private Sub ListadoDeClientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListadoDeClientesToolStripMenuItem.Click
        Dim frmCliente As New frmClientes()
        AbrirFormulario(frmCliente, contentPanel)
    End Sub

    Private Sub NuevaVentaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevaVentaToolStripMenuItem.Click
        Dim frmNuevaVenta As New frmNuevaVenta()
        AbrirFormulario(frmNuevaVenta, contentPanel)

    End Sub


    Public Sub AbrirFormulario(ByVal formulario As Form, ByVal contenedor As Panel)
        formulario.TopLevel = False
        formulario.FormBorderStyle = FormBorderStyle.None
        formulario.Dock = DockStyle.Fill
        contenedor.Controls.Clear()
        contenedor.Height = formulario.Height
        contenedor.Width = formulario.Width
        contenedor.Controls.Add(formulario)
        formulario.Show()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub GestionarProductosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GestionarProductosToolStripMenuItem.Click
        Dim frmProducto As New frmProductos()
        AbrirFormulario(frmProducto, contentPanel)
    End Sub

    Private Sub GestionarVentasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GestionarVentasToolStripMenuItem.Click
        Dim frmVentas As New frmVentas()
        AbrirFormulario(frmVentas, contentPanel)
    End Sub

    Private Sub ReportesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportesToolStripMenuItem.Click
        Dim frmReportes As New frmReportes()
        AbrirFormulario(frmReportes, contentPanel)
    End Sub
End Class
