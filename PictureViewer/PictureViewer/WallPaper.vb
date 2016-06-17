Imports System.Runtime.InteropServices
Public Class WallPaper
    Private Const SPI_SETDESKWALLPAPER As Integer = &H14
    Private Const SPIF_UPDATEINFILE As Integer = &H1
    Private Const SPIF_SENDWININICHANGE As Integer = &H2
    Private Declare Auto Function SystemParametersInfo Lib "user32.dll" (ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Integer
    Public Shared Function SetWallpaper(ByVal fileName As String, ByVal isAlways As Boolean) As Boolean
        Try
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, fileName, isAlways)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class