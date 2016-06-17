Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

<ToolboxBitmap("ZoomPictureBox.bmp")> _
Public Class ZoomPictureBox
    Inherits UserControl

#Region "Constructor"

    Public Sub New()
        Me.DoubleBuffered = True
        Me.BackColor = Color.FromKnownColor(KnownColor.AppWorkspace)
        Me.Size = New Size(200, 200)
    End Sub

#End Region



#Region "Private Variables"
    Private _isAutoZoomWhileLoadPic As Boolean = True
    Private _WindowAreaHeight As Integer = 1040
    Private _WindowAreaWidth As Integer = 1920

    Private _ImageBounds As Rectangle
    Private _ZoomFactor As Double
    Private _Image As Image
    Private _startDrag As Point
    Private _dragging As Boolean
    Private _imageInitialized As Boolean
    Private _ZoomMode As ZoomType = ZoomType.MousePosition
	Private _previousZoomfactor As Double
	Private _MouseWheelDivisor As Integer = 4000
	Private _MinimumImageWidth As Integer = 10
	Private _MinimumImageHeight As Integer = 10
	Private _MaximumZoomFactor As Double = 64
	Private _EnableMouseWheelZooming As Boolean = True
    Private _EnableMouseWheelDragging As Boolean = True
#End Region

#Region "Enums"

    Public Enum ZoomType
        MousePosition
        ControlCenter
        ImageCenter
    End Enum

#End Region

#Region "Event overrides"

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        Me.Select()
        If EnableMouseDragging AndAlso e.Button = MouseButtons.Left Then
            _startDrag = e.Location
            _dragging = True
        End If
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        If _dragging Then
            Me.Invalidate(_ImageBounds)
            _ImageBounds.X += e.X - _startDrag.X
            _ImageBounds.Y += e.Y - _startDrag.Y
            _startDrag = e.Location
            Me.Invalidate(_ImageBounds)
        End If
        MyBase.OnMouseMove(e)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As System.EventArgs)
        Me.Select()
        MyBase.OnSizeChanged(e)
    End Sub

    Protected Overrides Sub OnMove(ByVal e As System.EventArgs)
        Me.Select()
        MyBase.OnMove(e)
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        If _dragging Then
            _dragging = False
            Me.Invalidate()
        End If
        MyBase.OnMouseUp(e)
    End Sub

    'The control only raises MouseWheel events when it has Focus.
    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        Me.Select()
        MyBase.OnMouseEnter(e)
    End Sub

    'Mouse wheel zooming.
    Protected Overrides Sub OnMouseWheel(ByVal e As System.Windows.Forms.MouseEventArgs)
        If EnableMouseWheelZooming AndAlso
        Me.ClientRectangle.Contains(e.Location) Then
            Dim zoom As Double = _ZoomFactor
            zoom *= 1 + e.Delta / MouseWheelDivisor
            ZoomFactor = zoom
        End If
        MyBase.OnMouseWheel(e)
    End Sub

    'Render the image in the _ImageBounds rectangle.
    Protected Overrides Sub OnPaint(ByVal pe As System.Windows.Forms.PaintEventArgs)
        If _ZoomFactor > 4 Then
            pe.Graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.Half
            pe.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        Else
            pe.Graphics.InterpolationMode = Drawing2D.InterpolationMode.Default
        End If
        If Me.Image IsNot Nothing Then
            pe.Graphics.DrawImage(Me.Image, _ImageBounds)
        End If
        '
        MyBase.OnPaint(pe)
        'Me.Location = New Point(_ImageBounds.X, _ImageBounds.Y)
    End Sub

#End Region

#Region "Private methods"

    Private Sub InitializeImage()
        If _Image IsNot Nothing Then
            If _isAutoZoomWhileLoadPic = True Then
                ZoomFactor = FitImageToWorkArea()
            Else
                ZoomFactor = FitImageToControl()
            End If
            _ImageBounds = CenterImageBounds()
        End If
        _imageInitialized = True
        Me.Invalidate()
    End Sub

    'Apply the maximum and minimum zoom limits:
    Private Function ValidateZoomFactor(ByVal zoom As Double) As Double
        zoom = Math.Min(zoom, MaximumZoomFactor)
        If _Image IsNot Nothing Then
            If CInt(_Image.Width * zoom) < MinimumImageWidth Then
                zoom = MinimumImageWidth / _Image.Width
            End If
            If CInt(_Image.Height * zoom) < MinimumImageHeight Then
                zoom = MinimumImageHeight / _Image.Height
            End If
        End If
        Return zoom
    End Function

    'Get the initial ZoomFactor to fit the image to the control.
    Private Function FitImageToControl() As Double
        If Me.ClientSize = Size.Empty Then Return 1
        Dim sourceAspect As Double = _Image.Width / _Image.Height
        Dim targetAspect As Double = Me.ClientSize.Width / Me.ClientSize.Height
        If sourceAspect > targetAspect Then
            Return Me.ClientSize.Width / _Image.Width
        Else
            Return Me.ClientSize.Height / _Image.Height
        End If
    End Function

    'Get the initial ZoomFactor to fit the image to the WorkArea.
    Private Function FitImageToWorkArea() As Double
        If Me.ClientSize = Size.Empty Then Return 1
        Dim sourceAspect As Double = _Image.Width / _Image.Height
        Dim targetAspect As Double = _WindowAreaWidth / _WindowAreaHeight

        Dim ratioAdjust As Double = 0.05
        If sourceAspect > targetAspect Then
            Return (_WindowAreaWidth / _Image.Width) - ratioAdjust
        Else
            Return (_WindowAreaHeight / _Image.Height) - ratioAdjust
        End If
    End Function

    'Center the zoomed image in the control bounds.
    Private Function CenterImageBounds() As Rectangle
        Dim w As Integer = CInt(_Image.Width * _ZoomFactor)
        Dim h As Integer = CInt(_Image.Height * _ZoomFactor)
        Dim x As Integer = (Me.ClientSize.Width - w) \ 2
        Dim y As Integer = (Me.ClientSize.Height - h) \ 2
        Return New Rectangle(x, y, w, h)
    End Function

    'Calculate the image bounds for a given ZoomFactor,  
    Private Function GetZoomedBounds() As Rectangle

        'Find the zoom center relative to the image bounds.
        Dim imageCenter As Point = FindZoomCenter(_ZoomMode)

        'Calculate the new size of the the image bounds.
        _previousZoomfactor = _ImageBounds.Width / _Image.Width
        If Math.Abs(_ZoomFactor - _previousZoomfactor) > 0.001 Then
            Dim zoomRatio As Double = _ZoomFactor / _previousZoomfactor
            _ImageBounds.Width = CInt(_ImageBounds.Width * zoomRatio)
            _ImageBounds.Height = CInt(_ImageBounds.Height * zoomRatio)

            'Find the resulting position of the zoom center prior to correction.
            Dim newPRelative As Point
            newPRelative.X = CInt(imageCenter.X * zoomRatio)
            newPRelative.Y = CInt(imageCenter.Y * zoomRatio)

            'Apply a correction to return the zoom center to its previous position.
            _ImageBounds.X += imageCenter.X - newPRelative.X
            _ImageBounds.Y += imageCenter.Y - newPRelative.Y

        End If
        _previousZoomfactor = _ZoomFactor
        Return _ImageBounds
    End Function

    'Find the zoom centre relative to the image bounds, depending on zoom mode.
    Private Function FindZoomCenter(ByVal type As ZoomType) As Point
        'Find the zoom center relative to the image bounds.
        Dim p As Point
        Select Case type
            Case ZoomType.ControlCenter
                p.X = Me.Width \ 2 - _ImageBounds.X
                p.Y = Me.Height \ 2 - _ImageBounds.Y
            Case ZoomType.ImageCenter
                p.X = _ImageBounds.Width \ 2
                p.Y = _ImageBounds.Height \ 2
            Case ZoomType.MousePosition
                Dim mp As Point = Me.PointToClient(MousePosition)
                p.X = mp.X - _ImageBounds.X
                p.Y = mp.Y - _ImageBounds.Y
            Case Else
                p = Point.Empty
        End Select
        Return p
    End Function

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'ZoomPictureBox
        '
        Me.Name = "ZoomPictureBox"
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
