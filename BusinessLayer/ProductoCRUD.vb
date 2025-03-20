Imports DataLayer
Imports Entities

Public Class ProductoCRUD
    Implements ICRUD(Of Producto)

    Public Function Create(entity As Producto) As Integer Implements ICRUD(Of Producto).Create
        Dim daProducto As New DAProducto()
        Dim resultado = daProducto.agregarProducto(entity)
        If Not resultado Then
            Return 0
        End If
        Return 1
    End Function

    Public Function Read(id As Integer) As List(Of Producto) Implements ICRUD(Of Producto).Read
        Dim productoDA As New DAProducto()
        Dim listaProducto = productoDA.ObtenerProductos()
        Return listaProducto
    End Function

    Public Function Update(entity As Producto) As Boolean Implements ICRUD(Of Producto).Update
        Dim daProducto As New DAProducto()
        Dim resultado = daProducto.modificarProducto(entity)
    End Function

    Public Function Delete(id As Integer) As Boolean Implements ICRUD(Of Producto).Delete
        Dim daProducto As New DAProducto()
        Dim resultado = daProducto.eliminarProducto(id)
        If resultado Then
            Return True
        End If
        Return False
    End Function
End Class
