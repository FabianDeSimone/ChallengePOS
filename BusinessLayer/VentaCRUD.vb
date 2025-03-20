Imports DataLayer
Imports Entities

Public Class VentaCRUD
    Implements ICRUD(Of Venta)

    Public Function Create(entity As Venta) As Integer Implements ICRUD(Of Venta).Create
        Dim daVenta As New DAVenta()
        Dim idVenta = daVenta.AgregarVenta(entity)
        If idVenta <> False Then
            Return idVenta
        End If
        Return False
    End Function

    Public Function Read(id As Integer) As List(Of Venta) Implements ICRUD(Of Venta).Read
        Dim daVenta As New DAVenta()
        Dim listaVentas = daVenta.ObtenerVentas()
        Return listaVentas
    End Function

    Public Function Update(entity As Venta) As Boolean Implements ICRUD(Of Venta).Update
        Dim daVenta As New DAVenta()
        Dim resultado = daVenta.modificarVenta(entity)
    End Function

    Public Function Delete(id As Integer) As Boolean Implements ICRUD(Of Venta).Delete
        Dim daVenta As New DAVenta()
        Dim resultado = daVenta.EliminarVenta(id)
    End Function
End Class
