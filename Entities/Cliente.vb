Public Class Cliente
    Public Property ID As Integer
    Public Property Cliente As String
    Public Property Telefono As String
    Public Property Correo As String

    Public Sub New(ID As Integer, cliente As String, telefono As String, correo As String)
        Me.ID = ID
        Me.Cliente = cliente
        Me.Telefono = telefono
        Me.Correo = correo
    End Sub

End Class
