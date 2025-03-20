Imports BusinessLayer

Public Class frmReportes
    Private Sub btnReporteVentas_Click(sender As Object, e As EventArgs) Handles btnReporteVentas.Click
        Dim rpVentas As New Reportes("ventas")
        Dim lista = rpVentas.Read()
    End Sub
End Class