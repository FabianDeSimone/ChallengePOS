Imports BusinessLayer
Imports Entities

Public Class frmDetalleVenta
    Private ventaAMostrar As Integer

    Public Sub New(idVenta As Integer)
        InitializeComponent()
        ventaAMostrar = idVenta
    End Sub

    Private Sub frmDetalleVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ventaItemCRUD As New VentaItemCRUD()
        Dim listaVentaItems As List(Of VentaItem) = ventaItemCRUD.Read(ventaAMostrar)
        For Each item As VentaItem In listaVentaItems
            Dim row As String() = New String() {item.ID, item.IDVenta, item.IDProducto, item.PrecioUnitario, item.Cantidad, item.PrecioTotal}
            dgvVentaItems.Rows.Add(row)
        Next
    End Sub
End Class
