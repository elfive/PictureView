Imports System.Threading
Imports System.IO
Imports Microsoft.Win32

Public Class Form1
    Private Const WM_HOTKEY = &H312
    Private Const MOD_ALT = &H1
    Private Const MOD_CONTROL = &H2
    Private Const MOD_SHIFT = &H4
    Private Const GWL_WNDPROC = (-4)
    Private Declare Auto Function RegisterHotKey Lib "user32.dll" Alias "RegisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Boolean
    Private Declare Auto Function UnRegisterHotKey Lib "user32.dll" Alias "UnregisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean

    Declare Auto Function ShellExecute Lib "shell32.dll" (ByVal hwnd As IntPtr, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As UInteger) As IntPtr
    Private Files() As String
    Private isLoadingImage As Boolean = False
    Private isFirstLoad As Boolean = True
    Private isMaximized As Boolean
    Private SelectedIndex As Integer = -1
    Private MyPicturesFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures).ToLower
    Private WallpaperPath As String = MyPicturesFolder & "\wallpaper.bmp"
    Private FOF As Integer
    Private isFirstLoadPicIndexFind As Boolean = True                      '命令行为文件的，第一次启动需要确定其索引号，这是是否确定的标志
    Private Count As Integer = 0
    Private Command As String
    Private LowCaseCommand As String
    Private SearchFilesThread As New Thread(AddressOf SearchFiles)
    Private StartupIndex As Integer = 0
    Private SegCount As Integer = 10

    Private MyAppPath As String = Application.ExecutablePath
    Private MyAppFolder As String = Application.StartupPath

    Private FormWidthBeforeMax As Integer
    Private FormHeightBeforeMax As Integer
    Private imageWidth As Integer
    Private imageHeight As Integer
    Private PictureBoxTopBeforeMax As Integer
    Private PictureBoxLeftBeforeMax As Integer
    Private SystemWorkAreaWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width
    Private SystemWorkAreaHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height

    Private Const SPI_SETDESKWALLPAPER As Integer = &H14
    Private Const SPIF_UPDATEINIFILE As Integer = &H1
    Private Const SPIF_SENDCHANGE As Integer = &H2
    Private Declare Auto Function SystemParametersInfo Lib "user32.dll" (ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Integer
    Public Style As Integer
    Enum DStyle
        AutoDecide = -1
        Center = 0    '居中
        Stretch = 1   '拉伸
        Tiled = 2     '平铺
    End Enum

#Region "注册&注销热键"
    Private Sub RegHotKey()
        '注册全局热键
        RegisterHotKey(Handle, 0, Nothing, Keys.Right) '第一个热键 右方向键
        RegisterHotKey(Handle, 1, Nothing, Keys.Left) '第二个热键 左方向键
        RegisterHotKey(Handle, 2, Nothing, Keys.F5) '第二个热键 F5
        RegisterHotKey(Handle, 3, Nothing, Keys.Enter) '第二个热键 
    End Sub
    Private Sub unRegHotKey()
        '注销全局热键
        UnRegisterHotKey(Handle, 0)
        UnRegisterHotKey(Handle, 1)
        UnRegisterHotKey(Handle, 2)
        UnRegisterHotKey(Handle, 3)
    End Sub
#End Region

#Region "响应按键事件"
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = 786 Then
            Select Case m.WParam.ToInt32
                Case 0                  '按下了右方向键
                    NextPic()
                Case 1                 '按下了左方向键
                    PreviousPic()
                Case 2                  '按下了F5
                    Timer1.Enabled = Not Timer1.Enabled
                Case 3                  '按下了回车键
                    PictureBox_DoubleClick()
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

#End Region

#Region "切换上下张图片"
    Public Function NextPic() Handles Menu_Next.Click
        If Files Is Nothing Then Return False : Exit Function
        If SelectedIndex = UBound(Files) Then
            SelectedIndex = 0
        Else
            SelectedIndex = SelectedIndex + 1
        End If

        If Count - SelectedIndex < 3 Then                       '预加载下一轮图片，线程继续
            If SearchFilesThread.ThreadState = ThreadState.Suspended Then
                Debug.Print("resumed")
                SearchFilesThread.Resume()

            End If
        End If

        isLoadingImage = True
        LoadPic(Files(SelectedIndex))
        Return True
        isLoadingImage = False
    End Function

    Public Function PreviousPic() Handles Menu_Previous.Click
        If Files Is Nothing Then Return False : Exit Function
        If SelectedIndex = 0 Then
            SelectedIndex = UBound(Files)
        Else
            SelectedIndex = SelectedIndex - 1
        End If
        isLoadingImage = True
        If Not (PictureBox.Image Is Nothing) Then
            PictureBox.Image.Dispose()
            PictureBox.Image = Nothing
        End If
        LoadPic(Files(SelectedIndex))
        Return True
        isLoadingImage = False
    End Function
#End Region

#Region "拖动PictureBox事件"
    Dim MovBoll As Boolean
    Dim CurrX As Integer
    Dim CurrY As Integer
    Dim MousX As Integer
    Dim MousY As Integer
    Private Sub PictureBox_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox.MouseDown
        MousX = e.X
        MousY = e.Y
        MovBoll = True
    End Sub
    Private Sub PictureBox_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox.MouseMove
        If MovBoll = True Then
            CurrX = PictureBox.Left - MousX + e.X
            CurrY = PictureBox.Top - MousY + e.Y
            PictureBox.Location = New Point(CurrX, CurrY)
        End If
    End Sub

    Private Sub PictureBox_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox.MouseUp
        MovBoll = False
    End Sub
#End Region

#Region "创建关联"
    Private Function SetFolderAssociate() As Boolean
        On Error GoTo Err
        '设置文件夹右键菜单
        Dim lngResult As IntPtr = ShellExecute(Handle, "runas", MyAppFolder & "\PictureViewerSetup.exe", "+f", "", 0)
        Return True
Err:
        Return False
    End Function
    Private Function DeleteFolderAssociate() As Boolean
        On Error GoTo Err
        '取消文件夹右键菜单
        Dim lngResult As IntPtr = ShellExecute(Handle, "runas", MyAppFolder & "\PictureViewerSetup.exe", "-f", "", 0)
        Return True
Err:
        Return False
    End Function
    Public Function SetAssociation(ByVal FileType As String) As Boolean
        On Error GoTo Err
        Dim lngResult As IntPtr = ShellExecute(Handle, "runas", MyAppFolder & "\PictureViewerSetup.exe", "+" & FileType, "", 0)
        Return True
Err:
        Return False
    End Function
    Private Function DeleteAssociation(ByVal FileType As String) As Boolean
        On Error GoTo Err
        Dim lngResult As IntPtr = ShellExecute(Handle, "runas", MyAppFolder & "\PictureViewerSetup.exe", "-" & FileType, "", 0)
        Return True
Err:
        Return False
    End Function
    Public Function SetAllAssociationg() As Boolean
        On Error GoTo Err
        '设置文件夹右键菜单
        Dim lngResult As IntPtr = ShellExecute(Handle, "runas", MyAppFolder & "\PictureViewerSetup.exe", "+a", "", 0)
        Return True
Err:
        Return False
    End Function
    Public Function DelelteAllAssociationg() As Boolean
        On Error GoTo Err
        '取消文件夹右键菜单
        Dim lngResult As IntPtr = ShellExecute(Handle, "runas", MyAppFolder & "\PictureViewerSetup.exe", "-a", "", 0)
        Return True
Err:
        Return False
    End Function
    Private Sub Menu_jpg_Click(sender As Object, e As EventArgs) Handles Menu_jpg.Click
        If Menu_jpg.Checked = True Then
            '取消文件关联
            If DeleteAssociation("jpg") Then Menu_jpg.Checked = False
        Else
            '设置jpg关联
            If SetAssociation("jpg") Then Menu_jpg.Checked = True
        End If

        If Menu_Folder.Checked = True And Menu_jpg.Checked = True And Menu_jpeg.Checked = True And Menu_png.Checked = True And Menu_bmp.Checked = True Then
            Menu_All.Checked = True
        Else
            Menu_All.Checked = False
        End If
    End Sub
    Private Sub Menu_jpeg_Click(sender As Object, e As EventArgs) Handles Menu_jpeg.Click
        If Menu_jpeg.Checked = True Then
            '取消文件关联
            If DeleteAssociation("jpeg") Then Menu_jpeg.Checked = False
        Else
            '设置jpeg关联
            If SetAssociation("jpeg") Then Menu_jpeg.Checked = True
        End If

        If Menu_Folder.Checked = True And Menu_jpg.Checked = True And Menu_jpeg.Checked = True And Menu_png.Checked = True And Menu_bmp.Checked = True Then
            Menu_All.Checked = True
        Else
            Menu_All.Checked = False
        End If
    End Sub
    Private Sub Menu_png_Click(sender As Object, e As EventArgs) Handles Menu_png.Click
        If Menu_png.Checked = True Then
            '取消文件关联
            If DeleteAssociation("png") Then Menu_png.Checked = False
        Else
            '设置png关联
            If SetAssociation("png") Then Menu_png.Checked = True
        End If

        If Menu_Folder.Checked = True And Menu_jpg.Checked = True And Menu_jpeg.Checked = True And Menu_png.Checked = True And Menu_bmp.Checked = True Then
            Menu_All.Checked = True
        Else
            Menu_All.Checked = False
        End If
    End Sub
    Private Sub Menu_bmp_Click(sender As Object, e As EventArgs) Handles Menu_bmp.Click
        If Menu_bmp.Checked = True Then
            '取消文件关联
            If DeleteAssociation("bmp") Then Menu_bmp.Checked = False
        Else
            '设置bmp关联
            If SetAssociation("bmp") Then Menu_bmp.Checked = True
        End If

        If Menu_Folder.Checked = True And Menu_jpg.Checked = True And Menu_jpeg.Checked = True And Menu_png.Checked = True And Menu_bmp.Checked = True Then
            Menu_All.Checked = True
        Else
            Menu_All.Checked = False
        End If
    End Sub
    Private Sub Menu_Folder_Click(sender As Object, e As EventArgs) Handles Menu_Folder.Click
        If Menu_Folder.Checked = True Then
            '取消文件夹右键关联
            If DeleteFolderAssociate() Then Menu_Folder.Checked = False
        Else
            '设置文件夹右键关联
            If SetFolderAssociate() Then Menu_Folder.Checked = True
        End If

        If Menu_Folder.Checked = True And Menu_jpg.Checked = True And Menu_jpeg.Checked = True And Menu_png.Checked = True And Menu_bmp.Checked = True Then
            Menu_All.Checked = True
        Else
            Menu_All.Checked = False
        End If

    End Sub
    Private Sub Menu_All_Click(sender As Object, e As EventArgs) Handles Menu_All.Click
        If Menu_All.Checked = True Then
            '取消文件夹右键关联
            If DelelteAllAssociationg() Then Menu_All.Checked = False : Menu_Folder.Checked = False : Menu_jpg.Checked = False : Menu_jpeg.Checked = False : Menu_png.Checked = False : Menu_bmp.Checked = False
        Else
            '设置文件夹右键关联
            If SetAllAssociationg() Then Menu_All.Checked = True : Menu_Folder.Checked = True : Menu_jpg.Checked = True : Menu_jpeg.Checked = True : Menu_png.Checked = True : Menu_bmp.Checked = True
        End If
    End Sub

#End Region
    Public Function isFileOrFolder(ByVal Path As String) As Integer
        If Path.ToLower.EndsWith(".jpg") Or Path.ToLower.EndsWith(".jpeg") Or Path.ToLower.EndsWith(".png") Or Path.ToLower.EndsWith(".bmp") Then                  '是图片文件
            Return 1
        Else
            If File.Exists(Path) Then                 '是文件，但不是图片文件
                Command = GetParentFolder(Command)
                Return 2
            ElseIf Directory.Exists(Path) Then        '是文件夹
                Return 2
            Else
                Return 0
            End If
        End If

    End Function
    Public Function GetParentFolder(ByVal Path As String) As String
        Dim tmp() As String = Split(Path, "\")
        If UBound(tmp) - 1 > 0 Then
            ReDim Preserve tmp(UBound(tmp) - 1)
            Dim result As String = Join(tmp, "\")
            Return result
        Else
            Return ""
        End If
    End Function
    Public Function GetFileName(ByVal Path As String) As String
        Dim tmp() As String = Split(Path, "\")
        Return tmp(UBound(tmp))
    End Function

#Region "Form1事件"
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Hide()
        KeyPreview = True
        isMaximized = False

        RegHotKey()

        '================获取程序启动命令行==================
        Command = Microsoft.VisualBasic.Command
        If Command Is Nothing Then
            Command = MyPicturesFolder
        End If
        LowCaseCommand = Command.ToLower
        '================获取程序启动命令行==================
        FOF = isFileOrFolder(Command)
        Dim FilePath As String
        If FOF = 1 Then
            FilePath = GetParentFolder(Command)
            LoadPic(Command)
            isFirstLoadPicIndexFind = False
            isFirstLoad = False
        ElseIf FOF = 2 Then
            FilePath = Command
        Else
            FilePath = MyPicturesFolder
        End If

        Erase Files
        SearchFilesThread.Start(FilePath)

        Dim isFirstRunApp As Integer = GetSetting("PictureView", "RunApp", "isFirstRunApp", 1)          '1代表没有运行过，0代表运行过
        SaveSetting("PictureView", "RunApp", "isFirstRunApp", 0)
        'DeleteSetting("PictureView")
        If isFirstRunApp = 1 Then
            If MsgBox("是否注册右键及文件关联？", vbYesNo + vbQuestion, "询问") = vbYes Then
                SetAllAssociationg()
                Menu_jpg.Checked = True
                Menu_jpeg.Checked = True
                Menu_png.Checked = True
                Menu_bmp.Checked = True
                Menu_Folder.Checked = True
                Menu_All.Checked = True
            End If
        Else
            Menu_jpg.Checked = CBool(GetSetting("PictureView", "Association", ".jpg", 0))          '1代表没有运行过，2代表运行过。cbool：0代表False，其余代表True
            Menu_jpeg.Checked = CBool(GetSetting("PictureView", "Association", ".jpeg", 0))          '1代表没有运行过，2代表运行过。cbool：0代表False，其余代表True
            Menu_png.Checked = CBool(GetSetting("PictureView", "Association", ".png", 0))          '1代表没有运行过，2代表运行过。cbool：0代表False，其余代表True
            Menu_bmp.Checked = CBool(GetSetting("PictureView", "Association", ".bmp", 0))          '1代表没有运行过，2代表运行过。cbool：0代表False，其余代表True
            Menu_Folder.Checked = CBool(GetSetting("PictureView", "Association", "Folder", 0))          '1代表没有运行过，2代表运行过。cbool：0代表False，其余代表True
            Menu_All.Checked = Menu_jpg.Checked And Menu_jpeg.Checked And Menu_png.Checked And Menu_bmp.Checked
        End If
        SaveSetting("PictureView", "Path", "AppPath", MyAppPath)
        OpenFileDialog.InitialDirectory = MyPicturesFolder

    End Sub
    Private Sub ShowForm()
        Me.Show()
    End Sub
    Private Sub Form1_Closing() Handles Me.Closing, Menu_Exit.Click
        If SearchFilesThread.ThreadState = ThreadState.Suspended Then SearchFilesThread.Resume()
        SearchFilesThread.Abort()
        unRegHotKey()
        End
    End Sub
    Private Sub Form1_LostFocus(sender As Object, e As EventArgs) Handles PictureBox.LostFocus
        'MsgBox("Form1_LostFocus")
        unRegHotKey()
    End Sub
    Private Sub Form1_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus, PictureBox.GotFocus
        'MsgBox("Form1_GotFocus")
        RegHotKey()
    End Sub
    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then Exit Sub
        If Me.Height < 500 Then Me.Height = 500
        If Me.Width < 500 Then Me.Width = 500
        Label1.Width = Me.Width - 30
        If isMaximized = False Then
            PictureBox.WindowAreaHeight = Me.Height
            PictureBox.WindowAreaWidth = Me.Width
        Else
            PictureBox.WindowAreaHeight = SystemWorkAreaHeight
            PictureBox.WindowAreaWidth = SystemWorkAreaWidth
        End If
        Label1.Top = PictureBox.Height + 25
        If Not (PictureBox.Image Is Nothing) Then
            PictureBox.Left = ((Me.Width - PictureBox.Width) / 2)
            PictureBox.Top = ((Me.Height - PictureBox.Height) / 2) - 20
        End If
    End Sub

#End Region

#Region "遍历文件及子文件夹中的文件"
    Private Delegate Sub FirstLoadLoadPic(ByVal Path As String)               '委托定义申明
    Private Delegate Sub ThreadShowForm()
    Public Sub SearchFiles(ByVal Path As String)        '遍历文件

        If Not (Path Is Nothing) Then
            Dim mFileInfo As FileInfo
            Dim mDir As DirectoryInfo
            Dim mDirInfo As New DirectoryInfo(Path)
            For Each mFileInfo In mDirInfo.GetFiles()
                Dim tmpPath As String = mFileInfo.FullName
                Dim tmpEx As String = mFileInfo.Extension.ToLower
                If tmpEx = ".jpg" Or tmpEx = ".jpeg" Or tmpEx = ".png" Or tmpEx = ".bmp" Then

                    If Files IsNot Nothing Then
                        ReDim Preserve Files(UBound(Files) + 1)
                    Else
                        ReDim Files(0)
                        If isFirstLoad Then
                            Me.Invoke(New FirstLoadLoadPic(AddressOf LoadPic), tmpPath)
                            SelectedIndex = 0
                            isFirstLoad = False
                        End If
                    End If
                    Files(UBound(Files)) = tmpPath
                    'Debug.Print(tmpPath)
                    If FOF = 1 And tmpPath = Command Then
                        isFirstLoadPicIndexFind = True
                        SelectedIndex = Count
                        StartupIndex = Count
                        Debug.Print(Count)
                        Me.Invoke(New ThreadShowForm(AddressOf ShowForm))
                    End If
                    Count = Count + 1
                    If (Count Mod SegCount = 0 And isFirstLoadPicIndexFind = True) Then               '每载入SegCount张照片后暂停
                        If StartupIndex < Count - 1 Then
                            If Thread.CurrentThread.ThreadState = ThreadState.Running Then
                                Debug.Print("suspend added " & Count & " files to list")
                                Thread.CurrentThread.Suspend()
                            End If
                        End If
                    End If
                End If
            Next
            For Each mDir In mDirInfo.GetDirectories
                SearchFiles(mDir.FullName)
            Next
        End If
    End Sub
#End Region
    Public Function LoadPic(ByVal PicPath As String)
        Dim FileType As String = System.IO.Path.GetExtension(PicPath).ToLower
        If FileType = ".jpg" Or FileType = ".jpeg" Or FileType = ".png" Or FileType = ".bmp" Then
            If Dir(PicPath) = "" Then Return False : Exit Function
            Label1.Text = PicPath
            Dim BitmapFile As Bitmap
            BitmapFile = New Bitmap(PicPath)
            Dim MaxWidth As Integer = BitmapFile.Width * PictureBox.MaximumZoomFactor
            Dim MaxHeight As Integer = BitmapFile.Height * PictureBox.MaximumZoomFactor
            PictureBox.MaximumSize = New Size(MaxWidth, MaxHeight)

            imageWidth = BitmapFile.Width
            imageHeight = BitmapFile.Height

            If isMaximized = False Then
                PictureBox.WindowAreaHeight = Me.Height
                PictureBox.WindowAreaWidth = Me.Width
            Else
                PictureBox.WindowAreaHeight = SystemWorkAreaHeight
                PictureBox.WindowAreaWidth = SystemWorkAreaWidth
            End If
            With PictureBox
                .Visible = False
                .Width = MaxWidth
                .Height = MaxHeight
                .Left = ((Me.Width - .Width) / 2)
                .Top = ((Me.Height - .Height) / 2) - 20
                If Not (.Image Is Nothing) Then
                    .Image.Dispose()
                    .Image = Nothing
                End If
                .Image = BitmapFile
                .Visible = True
            End With
            Text = "图片浏览器 - " & GetFileName(PicPath) & " - " & GetParentFolder(PicPath) & "\"
            isFirstLoad = False
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        NextPic()
        If SelectedIndex = UBound(Files) Then Timer1.Enabled = False : MsgBox("播放已经停止", vbInformation + vbOKOnly, "提示")
    End Sub
    Private Sub PictureBox_DoubleClick() Handles PictureBox.DoubleClick, MyBase.DoubleClick, Menu_PictureModel.Click
        If Not (PictureBox.Image Is Nothing) Then
            If isMaximized = True Then               '一般大小时
                '窗口模式
                Menu_PictureModel.Text = "看图模式(&W)"
                Me.Width = FormWidthBeforeMax
                Me.Height = FormHeightBeforeMax
                PictureBox.Top = PictureBoxTopBeforeMax
                PictureBox.Left = PictureBoxLeftBeforeMax
                PictureBox.WindowAreaHeight = FormWidthBeforeMax
                PictureBox.WindowAreaWidth = FormHeightBeforeMax
                FormBorderStyle = FormBorderStyle.Sizable
                TransparencyKey = Color.Empty
                Me.WindowState = FormWindowState.Normal
                isMaximized = False
            ElseIf isMaximized = False Then
                '看图模式
                Menu_PictureModel.Text = "窗口模式(&W)"
                FormWidthBeforeMax = Me.Width
                FormHeightBeforeMax = Me.Height
                PictureBoxTopBeforeMax = PictureBox.Top
                PictureBoxLeftBeforeMax = PictureBox.Left
                PictureBox.Left = ((SystemWorkAreaWidth - imageWidth) / 2)
                PictureBox.Top = ((SystemWorkAreaHeight - imageHeight) / 2) - 20
                FormBorderStyle = FormBorderStyle.None
                TransparencyKey = Color.FromKnownColor(KnownColor.Control)
                Me.WindowState = FormWindowState.Maximized
                isMaximized = True
            End If
        Else
            OpenFileDialog.ShowDialog()
            Dim tmp As String = OpenFileDialog.FileName
            If tmp = "" Then Exit Sub
            Dim FilePath As String = tmp
            LoadPic(tmp)
        End If
    End Sub
    Private Sub Menu_Play_Click(sender As Object, e As EventArgs) Handles Menu_Play.Click
        Timer1.Enabled = Not Timer1.Enabled
    End Sub
    Private Sub Menu_EditPicture_Click(sender As Object, e As EventArgs) Handles Menu_EditPicture.Click
        Shell("mspaint " & Label1.Text, vbNormalFocus)
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Menu_Explorer.Click
        Shell("explorer /select, " & Label1.Text, vbNormalFocus)
    End Sub

#Region "幻灯片时间间隔设置"
    Public Sub CheckKeyPress(ByVal TargetTextBox As ToolStripTextBox, ByVal e As KeyPressEventArgs, Optional ByVal Minus As Boolean = False, Optional ByVal DecimalCount As Integer = 0)
        Dim blnHandled As Boolean
        blnHandled = False
        Select Case Asc(e.KeyChar)
            Case Asc("-")                   '   负号：只能在最前头  
                If Not (TargetTextBox.SelectionStart = 0 And Minus = True) Then blnHandled = True
            Case Asc(".")                   '   小数点：小数位数大于0；在字符串中没有“.”，且加了“.”后小数位能满足要求  
                If DecimalCount <= 0 Then
                    blnHandled = True
                Else
                    If Not (InStr(TargetTextBox.Text, ".") = 0 And (Len(TargetTextBox.Text) - TargetTextBox.SelectionStart <= DecimalCount)) Then blnHandled = True
                End If
            Case 8  '退格键，  
            Case 13 ' 回车键  
                SendKeys.Send("{TAB}") '转为tab键  
            Case Asc("0") To Asc("9")       '   0-9  
                If InStr(TargetTextBox.Text, ".") > 0 Then
                    If TargetTextBox.SelectionStart > InStr(TargetTextBox.Text, ".") - 1 Then
                        '   当前字符位置在小数点后，则小数点后的字符数必须小于小数位  
                        If Len(TargetTextBox.Text) - InStr(TargetTextBox.Text, ".") + 1 > DecimalCount Then blnHandled = True

                    End If
                End If
            Case Else
                blnHandled = True
        End Select
        e.Handled = blnHandled
    End Sub
    Private Sub Menu_IncreaseTimer_Click(sender As Object, e As EventArgs) Handles Menu_IncreaseTimer.Click
        Menu_TimeInterval.Text = (CInt(Menu_TimeInterval.Text) + 1).ToString
        Timer1.Interval = CInt(Menu_TimeInterval.Text) * 1000
        Menu_TimeInterval.Text = Format(Val(Menu_TimeInterval.Text), "#########.0").ToString
    End Sub
    Private Sub Menu_DecreaseTimer_Click(sender As Object, e As EventArgs) Handles Menu_DecreaseTimer.Click
        If Timer1.Interval > 1000 Then
            Menu_TimeInterval.Text = (CInt(Menu_TimeInterval.Text) - 1).ToString
            Timer1.Interval = CInt(Menu_TimeInterval.Text) * 1000
        End If
        Menu_TimeInterval.Text = Format(Val(Menu_TimeInterval.Text), "#########.0").ToString
    End Sub
    Private Sub Menu_TimeInterval_LostFocus(sender As Object, e As EventArgs) Handles Menu_TimeInterval.LostFocus
        If CInt(Menu_TimeInterval.Text) = 0 Then
            Menu_TimeInterval.Text = Timer1.Interval.ToString
        Else
            Timer1.Interval = CInt(Menu_TimeInterval.Text) * 1000
        End If
        Menu_TimeInterval.Text = Format(Val(Menu_TimeInterval.Text), "#########.0").ToString
    End Sub
    Private Sub Menu_TimeInterval_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Menu_TimeInterval.KeyPress
        CheckKeyPress(Menu_TimeInterval, e, False, 1)
    End Sub
#End Region

#Region "设置壁纸"
    Private Sub Menu_Tile_Click(sender As Object, e As EventArgs) Handles Menu_Tile.Click
        SetWallpaper(Label1.Text, WallpaperPath, False, 2)
    End Sub

    Private Sub Menu_Center_Click(sender As Object, e As EventArgs) Handles Menu_Center.Click
        SetWallpaper(Label1.Text, WallpaperPath, False, 0)
    End Sub

    Private Sub Menu_Stretch_Click(sender As Object, e As EventArgs) Handles Menu_Stretch.Click
        SetWallpaper(Label1.Text, WallpaperPath, False, 1)
    End Sub

    Private Sub Menu_Auto_Click(sender As Object, e As EventArgs) Handles Menu_Auto.Click
        SetWallpaper(Label1.Text, WallpaperPath, True)
    End Sub

    Public Function SetWallpaper(ByVal Path As String, ByVal Tmp As String, ByVal DecideStyle As Boolean, Optional ByVal WallpaperStyle As DStyle = -1) As Boolean
        'put delay dialog
        If System.IO.File.Exists(Path) = False Then
            Return False
        Else
            If Path.ToLower.EndsWith(".bmp") Then
                Return ChangeWallpaper(Path)
            Else
                Return ChangeWallpaper(ConvertBitmap(Path, Tmp, DecideStyle, WallpaperStyle))
            End If
        End If
    End Function
    Private Function ChangeWallpaper(ByVal path As String) As Boolean
        'maybe switch
        Dim bState As Boolean
        SetStyleRegKey(Style)
        bState = SystemParametersInfo(SPI_SETDESKWALLPAPER, 1, path, SPIF_UPDATEINIFILE Or SPIF_SENDCHANGE)
        Return bState
    End Function

    Private Function ConvertBitmap(ByVal source As String, ByVal Output As String, ByVal DecideStyle As Boolean, Optional ByVal WallpaperStyle As Integer = -1) As String
        Dim sbmp As New Bitmap(source), dbmp As Bitmap, gr As Graphics
        'use some intelligence
        If DecideStyle Then
            If sbmp.Width < My.Computer.Screen.Bounds.Width / 2 Then
                If sbmp.Height < My.Computer.Screen.Bounds.Height / 2 Then
                    Style = DStyle.Tiled
                Else
                    Style = DStyle.Center
                End If
            Else
                Style = DStyle.Stretch
            End If
        Else
            Style = WallpaperStyle
        End If
        'do
        If Style = DStyle.Center Or Style = DStyle.Tiled Then
            dbmp = New Bitmap(sbmp.Width, sbmp.Height)
            gr = Graphics.FromImage(dbmp)
            gr.DrawImage(sbmp, 0, 0, sbmp.Width, sbmp.Height)
        Else
            dbmp = New Bitmap(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
            gr = Graphics.FromImage(dbmp)
            gr.DrawImage(sbmp, 0, 0, My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
        End If
        dbmp.Save(Output, Imaging.ImageFormat.Bmp)
        sbmp.Dispose()
        dbmp.Dispose()
        Return Output
    End Function
    'wallpaper -registry
    Sub SetStyleRegKey(ByVal iStyle As String)
        'center=0
        'stretch=1
        'tiled=2
        '居中 : TileWallpaper=0, WallpaperStyle=1
        '拉伸 : TileWallpaper=0, WallpaperStyle=2
        '平铺: TileWallpaper=1, WallpaperStyle=0             
        Select Case iStyle
            Case DStyle.Center
                SetRegistryKey("TileWallpaper", "0")
                SetRegistryKey("WallpaperStyle", "1")
            Case DStyle.Stretch
                SetRegistryKey("TileWallpaper", "0")
                SetRegistryKey("WallpaperStyle", "2")
            Case DStyle.Tiled
                SetRegistryKey("TileWallpaper", "1")
                SetRegistryKey("WallpaperStyle", "0")
        End Select
    End Sub
    Private Sub SetRegistryKey(ByVal keyName As String, ByVal Value As String)
        Dim deskTopKey As RegistryKey
        deskTopKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
        deskTopKey.SetValue(keyName, Value)
        deskTopKey.Close()
    End Sub






    ' Friend Function SetWallpaper(ByVal img As Image) As Boolean
    'Dim imageLocation As String
    '   imageLocation = My.Computer.FileSystem.CombinePath(My.Computer.FileSystem.SpecialDirectories.MyPictures, WallpaperPath)
    'Try
    '       img.Save(imageLocation, System.Drawing.Imaging.ImageFormat.Bmp)
    '      SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imageLocation, SPIF_UPDATEINIFILE Or SPIF_SENDWININICHANGE)
    'Catch Ex As Exception
    'Return False
    'End Try
    'End Function

    'Private Sub Menu_SetWallpaper_Click(sender As Object, e As EventArgs) Handles Menu_SetWallpaper.Click
    '    SetWallpaper(PictureBox.Image)
    'End Sub
#End Region
End Class