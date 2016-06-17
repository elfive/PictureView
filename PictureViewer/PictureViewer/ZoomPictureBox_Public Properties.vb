Imports System.ComponentModel
Imports System.Drawing

'Public properties for the ZoomPictureBox
Partial Public Class ZoomPictureBox
    <Category("_isAutoZoomWhileLoadPic"),
     Description("图片在载入时是否按照图片框大小自动调整大小。必须先设置WindowAreaWidth和WindowAreaHeight参数")>
    Public Property isAutoZoomWhileLoadPic As Boolean
        Get
            Return _isAutoZoomWhileLoadPic
        End Get
        Set(value As Boolean)
            _isAutoZoomWhileLoadPic = value
            If Me.Image IsNot Nothing Then
                If _WindowAreaWidth >= MinimumImageWidth And _WindowAreaHeight >= MinimumImageHeight And _isAutoZoomWhileLoadPic Then InitializeImage()
            End If
        End Set
    End Property

    <Category("_WindowAreaWidth"),
     Description("图片自动缩放时的工作区域宽度。")>
    Public Property WindowAreaWidth As Integer
        Get
            Return _WindowAreaWidth
        End Get
        Set(value As Integer)
            If value >= MinimumImageWidth Then
                _WindowAreaWidth = value
                If Me.Image IsNot Nothing Then
                    If _WindowAreaHeight >= MinimumImageHeight And _isAutoZoomWhileLoadPic Then InitializeImage()
                End If
            End If
        End Set
    End Property
    <Category("_WindowAreaHeight"),
     Description("图片自动缩放时的工作区域高度。")>
    Public Property WindowAreaHeight As Integer
        Get
            Return _WindowAreaHeight
        End Get
        Set(value As Integer)
            If value >= MinimumImageHeight Then
                _WindowAreaHeight = value
                If Me.Image IsNot Nothing Then
                    If _WindowAreaWidth >= MinimumImageWidth And _isAutoZoomWhileLoadPic Then InitializeImage()
                End If
            End If
        End Set
    End Property


    <Category("_ZoomPictureBox"), _
	 Description("Enable dragging. Set to False if you implement other means of image scrolling.")> _
	Public Property EnableMouseDragging As Boolean
		Get
			Return _EnableMouseWheelDragging
		End Get
		Set(value As Boolean)
			_EnableMouseWheelDragging = value
		End Set
	End Property

	<Category("_ZoomPictureBox"), _
	  Description("Enable mouse wheel zooming. Set to false e.g. if you control zooming with a TrackBar.")> _
	Public Property EnableMouseWheelZooming As Boolean
		Get
			Return _EnableMouseWheelZooming
		End Get
		Set(value As Boolean)
            _EnableMouseWheelZooming = value
        End Set
	End Property

	<Category("_ZoomPictureBox"), _
	Description("Image to display in the ZoomPictureBox.")> _
	Public Property Image() As Image
		Get
			Return _Image
		End Get
		Set(ByVal value As Image)
			_Image = value
			If value IsNot Nothing Then
				InitializeImage()
			Else
				_imageInitialized = False
			End If
		End Set
	End Property

    <Browsable(False), _
 Description("The bounding rectangle of the zoomed image relative to the control origin.")> _
 Public ReadOnly Property ImageBounds() As Rectangle
        Get
            Return _ImageBounds
        End Get
    End Property

    <Browsable(False), _
 Description("Location of the top left corner of the zoomed image relative to the control origin.")> _
 Public Property ImagePosition() As Point
        Get
            Return _ImageBounds.Location
        End Get
        Set(ByVal value As Point)
            Me.Invalidate(_ImageBounds)
            _ImageBounds.X = value.X
            _ImageBounds.Y = value.Y
            Me.Invalidate(_ImageBounds)
        End Set
    End Property

	<Category("_ZoomPictureBox"), _
   Description("The maximum zoom magnification.")> _
 Public Property MaximumZoomFactor As Double
		Get
			Return _MaximumZoomFactor
		End Get
		Set(value As Double)
			_MaximumZoomFactor = value
		End Set
	End Property

	<Category("_ZoomPictureBox"), _
	  Description("Minimum height of the zoomed image in pixels.")> _
	Public Property MinimumImageHeight As Integer
		Get
			Return _MinimumImageHeight
		End Get
		Set(value As Integer)
			_MinimumImageHeight = value
		End Set
	End Property

	<Category("_ZoomPictureBox"), _
	  Description("Minimum width of the zoomed image in pixels.")> _
	Public Property MinimumImageWidth As Integer
		Get
			Return _MinimumImageWidth
		End Get
		Set(value As Integer)
			_MinimumImageWidth = value
		End Set
	End Property

	<Category("_ZoomPictureBox"), _
	  Description("Sets the responsiveness of zooming to the mouse wheel. Choose a lower value for faster zooming.")> _
	Public Property MouseWheelDivisor As Integer
		Get
			Return _MouseWheelDivisor
		End Get
		Set(value As Integer)
			_MouseWheelDivisor = value
		End Set
	End Property

	<Browsable(False), _
	Description("Linear size of the zoomed image as a fraction of that of the source Image.")> _
	Public Property ZoomFactor() As Double
		Get
			Return _ZoomFactor
		End Get
		Set(ByVal value As Double)
			_ZoomFactor = ValidateZoomFactor(value)
			If _imageInitialized Then
				Me.Invalidate(_ImageBounds)
				_ImageBounds = GetZoomedBounds()
				Me.Invalidate(_ImageBounds)
			End If
		End Set
	End Property

	<Category("_ZoomPictureBox"), _
 DefaultValue(ZoomType.MousePosition), _
 Description("Image zooming around the mouse position, image center or  control center")> _
 Public Property ZoomMode() As ZoomType
		Get
			Return _ZoomMode
		End Get
		Set(ByVal value As ZoomType)
			_ZoomMode = value
		End Set
	End Property

End Class
