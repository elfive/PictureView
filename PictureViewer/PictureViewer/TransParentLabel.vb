Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class TransParentLabel

    Inherits Control
    Public Sub New()
        TabStop = False
        Dim transparencyValue As Integer = 15 '0 is all transparent, 255 is solid
        ForeColor = Color.FromArgb(transparencyValue, ForeColor.R, ForeColor.G, ForeColor.B)
    End Sub

    ' &lt;summary&gt;
    ' Gets the creation parameters.
    ' &lt;/summary&gt;
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H20
            Return cp
        End Get
    End Property

    ' &lt;summary&gt;
    ' Paints the background.
    ' &lt;/summary&gt;
    ' &lt;param name="e"&gt;E.&lt;/param&gt;
    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
        ' do nothing
    End Sub

    ' &lt;summary&gt;
    ' Paints the control.
    ' &lt;/summary&gt;
    ' &lt;param name="e"&gt;E.&lt;/param&gt;
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Using brush As SolidBrush = New SolidBrush(ForeColor)
            e.Graphics.DrawString(Text, Font, brush, -1, 0)
        End Using
    End Sub

End Class