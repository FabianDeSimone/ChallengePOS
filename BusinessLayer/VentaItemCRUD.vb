Imports Entities
Imports DataLayer


Public Class VentaItemCRUD
    Implements ICRUD(Of VentaItem)

    Public Function Create(entity As VentaItem) As Integer Implements ICRUD(Of VentaItem).Create
        Dim daVentaItem As New DAVentaItem()
        Dim resultado = daVentaItem.AgregarVentaItem(entity)
        If Not resultado Then
            Return 0
        End If
        Return 1

    End Function

    Public Function Read(id As Integer) As List(Of VentaItem) Implements ICRUD(Of VentaItem).Read
        Dim daVentaItem As New DAVentaItem()
        Dim listaVentaItems = daVentaItem.ObtenerVentaItems(id)
        Return listaVentaItems

    End Function

    Public Function Update(entity As VentaItem) As Boolean Implements ICRUD(Of VentaItem).Update
        Dim daVentaItem As New DAVentaItem()
        Dim resultado = daVentaItem.modificarVentaItem(entity)
    End Function

    Public Function Delete(id As Integer) As Boolean Implements ICRUD(Of VentaItem).Delete
        Dim daVentaItem As New DAVentaItem()
        daVentaItem.eliminarVentaItem(id)
    End Function
End Class
