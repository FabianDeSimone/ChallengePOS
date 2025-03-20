
Imports DataLayer
Imports Entities

Public Class ClienteCRUD
    Implements ICRUD(Of Cliente)

    Public Function Create(entity As Cliente) As Integer Implements ICRUD(Of Cliente).Create
        Dim listaCliente = Me.Read(0)
        For Each cliente In listaCliente
            If cliente.Correo = entity.Correo Then
                MessageBox.Show("El correo ya está registrado")
                Return False
            End If
        Next
        Dim daCliente As New DACliente()
        Dim resultado = daCliente.AgregarCliente(entity)
        If Not resultado Then
            Return 0
        End If
        Return 1
    End Function

    Public Function Read(id As Integer) As List(Of Cliente) Implements ICRUD(Of Cliente).Read
        Dim clienteDA As New DACliente()
        Dim listaCliente = clienteDA.ObtenerClientes()
        Return listaCliente
    End Function

    Public Function Update(entity As Cliente) As Boolean Implements ICRUD(Of Cliente).Update
        Dim listaCliente = Me.Read(0)
        For Each cliente In listaCliente
            If cliente.Correo = entity.Correo AndAlso cliente.ID <> entity.ID Then
                MessageBox.Show("El mail está siendo utilizado por otro cliente")
                Return False
            End If
        Next
        Dim daCliente As New DACliente()
        Dim resultado = daCliente.ModificarCliente(entity)
        Return True
    End Function

    Public Function Delete(ID As Integer) As Boolean Implements ICRUD(Of Cliente).Delete
        Dim daCliente As New DACliente()
        Dim resultado = daCliente.EliminarCliente(ID)
        If resultado Then
            Return True
        End If
        Return False
    End Function

End Class
