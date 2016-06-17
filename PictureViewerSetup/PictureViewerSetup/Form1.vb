Imports Microsoft.Win32
Public Class Form1
    Private MyAppPath As String
    Private isInitialEnd As Boolean = False
    Private Command As String
    Private Function SetFolderAssociate() As Boolean
        '设置文件夹右键菜单
        Dim AppKey1 As RegistryKey = Registry.ClassesRoot.CreateSubKey("Directory\\shell\\PictureView")
        AppKey1.SetValue("", "使用PictureView浏览")
        Dim AppSubKey1 = AppKey1.CreateSubKey("command")
        AppSubKey1.SetValue("", Chr(34) & MyAppPath & Chr(34) & " %1")
        SaveSetting("PictureView", "Association", "Folder", 1)             '设置文件关联标志.1代表已经关联、0代表尚未关联
        AppKey1.Close()
        AppSubKey1.Close()
        Return True
    End Function
    Private Function DeleteFolderAssociate() As Boolean
        '取消文件夹右键菜单
        Dim AppKey1 As RegistryKey = Registry.ClassesRoot.CreateSubKey("Directory\\shell\\PictureView")
        If AppKey1 Is Nothing Then Return True
        AppKey1.Close()
        Registry.ClassesRoot.DeleteSubKeyTree("Directory\\shell\\PictureView")
        SaveSetting("PictureView", "Association", "Folder", 0)             '设置文件关联标志.1代表已经关联、0代表尚未关联
        Return True
    End Function
    Private Function SetAllAssociation() As Boolean
        Return (SetAssociation(".jpg", "jpegfile") And SetAssociation(".jpeg", "jpegfile") And SetAssociation(".png", "pngfile") And SetAssociation(".bmp", "Paint.Picture") And SetFolderAssociate())
    End Function
    Private Function DeleteAllAssociate() As Boolean
        Return (DeleteAssociation(".jpg", "jpegfile") And DeleteAssociation(".jpeg", "jpegfile") And DeleteAssociation(".png", "pngfile") And DeleteAssociation(".bmp", "Paint.Picture") And DeleteFolderAssociate())
    End Function
    Public Function SetAssociation(ByVal FileType As String, ByVal DefaultTypename As String) As Boolean
        Dim AppKey As RegistryKey = Registry.ClassesRoot.OpenSubKey(FileType)
        Dim _Typename As String
        If AppKey Is Nothing Then
            AppKey = Registry.ClassesRoot.CreateSubKey(FileType)
            AppKey.SetValue("", DefaultTypename)
            _Typename = DefaultTypename
        Else
            _Typename = AppKey.GetValue("", DefaultTypename)
        End If
        AppKey.Close()
        Dim AppKey2 As RegistryKey = Registry.ClassesRoot.CreateSubKey(_Typename & "\\shell\\open\\command")
        AppKey2.SetValue("", Chr(34) & MyAppPath & Chr(34) & " %1")
        AppKey2.Close()
        SaveSetting("PictureView", "Association", FileType, 1)             '设置文件关联标志.1代表已经关联、0代表尚未关联
        Return True
    End Function
    Private Function DeleteAssociation(ByVal FileType As String, ByVal DefaultTypename As String) As Boolean
        Dim AppKey As RegistryKey = Registry.ClassesRoot.OpenSubKey(FileType)
        Dim _Typename As String
        If AppKey Is Nothing Then
            AppKey = Registry.ClassesRoot.CreateSubKey(FileType)
            AppKey.SetValue("", DefaultTypename)
            _Typename = DefaultTypename
        Else
            _Typename = AppKey.GetValue("", DefaultTypename)
        End If
        AppKey.Close()
        Dim AppKey2 As RegistryKey = Registry.ClassesRoot.CreateSubKey(_Typename & "\\shell\\open\\command")
        AppKey2.SetValue("", "rundll32.exe C:\windows\system32\shimgvw.dll,ImageView_Fullscreen  %1")
        AppKey2.Close()
        SaveSetting("PictureView", "Association", FileType, 0)             '设置文件关联标志.1代表已经关联、0代表尚未关联
        Return True
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MyAppPath = GetSetting("PictureView", "Path", "AppPath", "")
        If MyAppPath <> "" Then
            ExePath.Text = MyAppPath
            ExePath.ForeColor = Color.Black
            GroupBox1.Enabled = True
            GroupBox2.Enabled = True
        End If
        '================获取程序启动命令行==================
        Command = Microsoft.VisualBasic.Command.ToLower
        If Command <> "" Then
            Hide()
            Dim Location As Integer = InStr(Command, "-path")
            If Location > 0 Then
                Dim tmpPath As String = Replace(Command.ToLower.Substring(Location + 5, Len(Command) - Location - 5), Chr(34), "")
                If Dir(tmpPath) <> "" Then MyAppPath = tmpPath
                Debug.Print(MyAppPath)
            End If
            If InStr(Command, "+jpg") > 0 And InStr(Command, "-jpg") <= 0 Then SetAssociation(".jpg", "jpegfile") : Debug.Print("jpg关联成功")
            If InStr(Command, "+jpeg") > 0 And InStr(Command, "-jpeg") <= 0 Then SetAssociation(".jpeg", "jpegfile") : Debug.Print("jpeg关联成功")
            If InStr(Command, "+png") > 0 And InStr(Command, "-png") <= 0 Then SetAssociation(".png", "pngfile") : Debug.Print("png关联成功")
            If InStr(Command, "+bmp") > 0 And InStr(Command, "-bmp") <= 0 Then SetAssociation(".bmp", "Paint.Picture") : Debug.Print("bmp关联成功")
            If InStr(Command, "+f") > 0 And InStr(Command, "-f") <= 0 Then SetFolderAssociate() : Debug.Print("文件夹右键关联成功")
            If InStr(Command, "+a") > 0 And InStr(Command, "-a") <= 0 Then SetAllAssociation() : Debug.Print("关联成功")
            Debug.Print(InStr(Command, "-a"))
            If InStr(Command, "-jpg") > 0 And InStr(Command, "+jpg") <= 0 Then DeleteAssociation(".jpg", "jpegfile") : Debug.Print("jpg取消关联成功")
            If InStr(Command, "-jpeg") > 0 And InStr(Command, "+jpeg") <= 0 Then DeleteAssociation(".jpeg", "jpegfile") : Debug.Print("jpeg取消关联成功")
            If InStr(Command, "-png") > 0 And InStr(Command, "+png") <= 0 Then DeleteAssociation(".png", "pngfile") : Debug.Print("png取消关联成功")
            If InStr(Command, "-bmp") > 0 And InStr(Command, "+bmp") <= 0 Then DeleteAssociation(".bmp", "Paint.Picture") : Debug.Print("bmp取消关联成功")
            If InStr(Command, "-f") > 0 And InStr(Command, "+f") <= 0 Then DeleteFolderAssociate() : Debug.Print("文件夹右键取消关联成功")
            If InStr(Command, "-a") > 0 And InStr(Command, "+a") <= 0 Then DeleteAllAssociate() : Debug.Print("取消关联成功")

            If InStr(Command, "-s") <= 0 Then MsgBox("操作成功！", vbInformation + vbOKOnly, "提示")
            End
        End If
        '================获取程序启动命令行==================


        JPG_Associate.Checked = CBool(GetSetting("PictureView", "Association", ".jpg", 0))             '设置文件关联标志.1代表已经关联、0代表尚未关联
        JPEG_Associate.Checked = CBool(GetSetting("PictureView", "Association", ".jpeg", 0))            '设置文件关联标志.1代表已经关联、0代表尚未关联
        PNG_Associate.Checked = CBool(GetSetting("PictureView", "Association", ".png", 0))             '设置文件关联标志.1代表已经关联、0代表尚未关联
        BMP_Associate.Checked = CBool(GetSetting("PictureView", "Association", ".bmp", 0))             '设置文件关联标志.1代表已经关联、0代表尚未关联
        Folder_Associate.Checked = CBool(GetSetting("PictureView", "Association", "Folder", 0))             '设置文件关联标志.1代表已经关联、0代表尚未关联

        If (JPG_Associate.Checked And JPEG_Associate.Checked And PNG_Associate.Checked And BMP_Associate.Checked) Then
            All_Associate.Checked = True
        Else
            All_Associate.Checked = False
        End If

        isInitialEnd = True
    End Sub
    Private Sub ExePath_LostFocus(sender As Object, e As EventArgs) Handles ExePath.LostFocus
        If Dir(ExePath.Text) = "" Or ExePath.Text = "" Then
            ExePath.Text = "双击选择PictureViewer.exe路径"
            MyAppPath = ""
            ExePath.ForeColor = Color.Gray
            GroupBox1.Enabled = False
            GroupBox2.Enabled = False
        End If
    End Sub
    Private Sub ExePath_GotFocus(sender As Object, e As EventArgs) Handles ExePath.GotFocus
        If ExePath.Text = "双击选择PictureViewer.exe路径" Then
            ExePath.Text = ""
            MyAppPath = ""
            GroupBox1.Enabled = False
            GroupBox2.Enabled = False
        End If
        ExePath.ForeColor = Color.Black
    End Sub
    Private Sub ExePath_DoubleClick(sender As Object, e As EventArgs) Handles ExePath.DoubleClick
        OpenFileDialog.ShowDialog()
        If OpenFileDialog.FileName = "" Then
            Exit Sub
        Else
            ExePath.Text = OpenFileDialog.FileName
            MyAppPath = OpenFileDialog.FileName
            ExePath.ForeColor = Color.Black
            GroupBox1.Enabled = True
            GroupBox2.Enabled = True
        End If
    End Sub
    Private Sub InitializeReg_Click(sender As Object, e As EventArgs) Handles InitializeReg.Click
        If ExePath.Text = "双击选择PictureViewer.exe路径" Or ((Dir(ExePath.Text) <> "" And ExePath.Text.ToLower.EndsWith("PictureViewer.exe"))) Then
            MsgBox("请先选择好PictureViewer.exe路径再执行初始化！", vbCritical + vbOKOnly, "提示")
            Exit Sub
        End If
        SaveSetting("PictureView", "RunApp", "isFirstRunApp", 1)         '初次运行标志.'1代表没有运行过、0代表运行过
        SaveSetting("PictureView", "Association", ".jpg", 0)             '设置文件关联标志.1代表已经关联、0代表尚未关联
        JPG_Associate.Checked = False
        SaveSetting("PictureView", "Association", ".jpeg", 0)            '设置文件关联标志.1代表已经关联、0代表尚未关联
        JPEG_Associate.Checked = False
        SaveSetting("PictureView", "Association", ".png", 0)             '设置文件关联标志.1代表已经关联、0代表尚未关联
        PNG_Associate.Checked = False
        SaveSetting("PictureView", "Association", ".bmp", 0)             '设置文件关联标志.1代表已经关联、0代表尚未关联
        BMP_Associate.Checked = False
        SaveSetting("PictureView", "Association", "Folder", 0)             '设置文件关联标志.1代表已经关联、0代表尚未关联
        Folder_Associate.Checked = False
        SaveSetting("PictureView", "Path", "AppPath", MyAppPath)


        If DeleteAllAssociate() Then
            MsgBox("初始化成功！", vbOKOnly + vbInformation, "提示")
            All_Associate.Checked = False
            JPG_Associate.Checked = False
            JPEG_Associate.Checked = False
            PNG_Associate.Checked = False
            BMP_Associate.Checked = False
            Folder_Associate.Checked = False
        Else
            MsgBox("初始化失败！", vbOKOnly + vbInformation, "提示")
        End If
    End Sub
    Private Sub DeleteReg_Click(sender As Object, e As EventArgs) Handles DeleteReg.Click
        If isInitialEnd Then
            DeleteSetting("PictureView")
            If DeleteAllAssociate() And DeleteFolderAssociate() Then
                GroupBox1.Enabled = False
                GroupBox2.Enabled = False
                ExePath.Text = "双击选择PictureViewer.exe路径"
                ExePath.ForeColor = Color.Gray
                MyAppPath = ""
                MsgBox("删除成功！", vbOKOnly + vbInformation, "提示")

            Else
                MsgBox("删除失败！", vbOKOnly + vbCritical, "提示")
            End If
        End If
    End Sub
    Private Sub JPG_Associate_CheckedChanged(sender As Object, e As EventArgs) Handles JPG_Associate.CheckedChanged
        If isInitialEnd Then
            If JPG_Associate.Checked = True Then                 '按键后的状态
                '关联
                SetAssociation(".jpg", "jpegfile")
            Else
                '取消关联
                DeleteAssociation(".jpg", "jpegfile")
            End If

            If (JPG_Associate.Checked And JPEG_Associate.Checked And PNG_Associate.Checked And BMP_Associate.Checked) Then
                All_Associate.Checked = True
            Else
                BMP_Associate.Checked = False
            End If
        End If
    End Sub
    Private Sub JPEG_Associate_CheckedChanged(sender As Object, e As EventArgs) Handles JPEG_Associate.CheckedChanged
        If isInitialEnd Then
            If JPEG_Associate.Checked = True Then                 '按键后的状态
                '关联
                SetAssociation(".jpeg", "jpegfile")
            Else
                '取消关联
                DeleteAssociation(".jpeg", "jpegfile")
            End If

            If (JPG_Associate.Checked And JPEG_Associate.Checked And PNG_Associate.Checked And BMP_Associate.Checked) Then
                All_Associate.Checked = True
            Else
                BMP_Associate.Checked = False
            End If
        End If
    End Sub
    Private Sub PNG_Associate_CheckedChanged(sender As Object, e As EventArgs) Handles PNG_Associate.CheckedChanged
        If isInitialEnd Then
            If PNG_Associate.Checked = True Then                 '按键后的状态
                '关联
                SetAssociation(".png", "pngfile")
            Else
                '取消关联
                DeleteAssociation(".png", "pngfile")
            End If

            If (JPG_Associate.Checked And JPEG_Associate.Checked And PNG_Associate.Checked And BMP_Associate.Checked) Then
                All_Associate.Checked = True
            Else
                BMP_Associate.Checked = False
            End If
        End If
    End Sub
    Private Sub BMP_Associate_CheckedChanged(sender As Object, e As EventArgs) Handles BMP_Associate.CheckedChanged
        If isInitialEnd Then
            If BMP_Associate.Checked = True Then                 '按键后的状态
                '关联
                SetAssociation(".bmp", "Paint.Picture")
            Else
                '取消关联
                DeleteAssociation(".bmp", "Paint.Picture")
            End If

            If (JPG_Associate.Checked And JPEG_Associate.Checked And PNG_Associate.Checked And BMP_Associate.Checked) Then
                All_Associate.Checked = True
            Else
                All_Associate.Checked = False
            End If
        End If
    End Sub
    Private Sub All_Associate_CheckedChanged(sender As Object, e As EventArgs) Handles All_Associate.CheckedChanged
        If isInitialEnd Then
            If All_Associate.Checked = True Then                 '按键后的状态
                '关联
                If SetAllAssociation() Then
                    JPG_Associate.Checked = True
                    JPEG_Associate.Checked = True
                    PNG_Associate.Checked = True
                    BMP_Associate.Checked = True
                    Folder_Associate.Checked = True
                    MsgBox("关联成功！", vbOKOnly + vbInformation, "提示")
                Else
                    MsgBox("关联失败！", vbOKOnly + vbInformation, "提示")
                End If
            Else
                '取消关联
                If DeleteAllAssociate() Then
                    JPG_Associate.Checked = False
                    JPEG_Associate.Checked = False
                    PNG_Associate.Checked = False
                    BMP_Associate.Checked = False
                    Folder_Associate.Checked = False
                    MsgBox("删除关联成功！", vbOKOnly + vbInformation, "提示")
                Else
                    MsgBox("删除关联失败！", vbOKOnly + vbInformation, "提示")
                End If
            End If
        End If
    End Sub
    Private Sub Folder_Associate_CheckedChanged(sender As Object, e As EventArgs) Handles Folder_Associate.CheckedChanged
        If isInitialEnd Then
            If Folder_Associate.Checked = True Then                 '按键后的状态
                '关联
                If SetFolderAssociate() Then
                    MsgBox("文件夹右键关联成功！", vbInformation + vbOKOnly, "提示")
                Else
                    MsgBox("文件夹右键关联失败！", vbCritical + vbOKOnly, "提示")
                End If
            Else
                '取消关联
                If DeleteFolderAssociate() Then
                    MsgBox("文件夹右键取消关联成功！", vbInformation + vbOKOnly, "提示")
                Else
                    MsgBox("文件夹右键取消关联失败！", vbCritical + vbOKOnly, "提示")
                End If
            End If
        End If
    End Sub

End Class
