Public Class Venta
    Public Property ID As Integer
    Public Property IDCliente As Integer
    Public Property Fecha As Date
    Public Property Total As Decimal

    Public Sub New(ID As Integer, IDCliente As Integer, fecha As Date, total As Decimal)
        Me.ID = ID
        Me.IDCliente = IDCliente
        Me.Fecha = fecha
        Me.Total = total
    End Sub

End Class
