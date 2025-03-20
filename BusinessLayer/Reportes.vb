Imports DataLayer
Imports Entities
Public Class Reportes
    Public Property tipoReporte
    Public Function Read()
        Dim daReportes As New DAReportes()
        If tipoReporte = "ventas" Then
            Dim listaVentas = daReportes.ReporteVentasDetallado()
            Return listaVentas
        End If

    End Function


    Public Sub New(reporte As String)
        tipoReporte = reporte

    End Sub





End Class


