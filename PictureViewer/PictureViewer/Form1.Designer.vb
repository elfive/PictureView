<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form
    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub



    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PopupMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Menu_PictureModel = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_PlayPicture = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Play = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_IncreaseTimer = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_DecreaseTimer = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.Menu_TimeInterval = New System.Windows.Forms.ToolStripTextBox()
        Me.Menu_SetWallpaper = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Tile = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Center = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Stretch = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.Menu_Auto = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.Menu_EditPicture = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Explorer = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.Menu_Previous = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Next = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Folder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.Menu_jpg = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_jpeg = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_png = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_bmp = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.Menu_All = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.Menu_Exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.PictureBox = New PictureViewer.ZoomPictureBox()
        Me.PopupMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(6, 680)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1244, 26)
        Me.Label1.TabIndex = 7
        Me.Label1.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1300
        '
        'PopupMenu
        '
        Me.PopupMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_PictureModel, Me.Menu_PlayPicture, Me.Menu_SetWallpaper, Me.ToolStripSeparator2, Me.Menu_EditPicture, Me.Menu_Explorer, Me.ToolStripSeparator3, Me.Menu_Previous, Me.Menu_Next, Me.ToolStripSeparator1, Me.ToolStripMenuItem1, Me.ToolStripSeparator6, Me.Menu_Exit})
        Me.PopupMenu.Name = "PopupMenu"
        Me.PopupMenu.Size = New System.Drawing.Size(228, 226)
        '
        'Menu_PictureModel
        '
        Me.Menu_PictureModel.Name = "Menu_PictureModel"
        Me.Menu_PictureModel.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.Menu_PictureModel.Size = New System.Drawing.Size(227, 22)
        Me.Menu_PictureModel.Text = "看图模式(&W)"
        '
        'Menu_PlayPicture
        '
        Me.Menu_PlayPicture.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_Play, Me.Menu_IncreaseTimer, Me.Menu_DecreaseTimer, Me.ToolStripSeparator4, Me.Menu_TimeInterval})
        Me.Menu_PlayPicture.Name = "Menu_PlayPicture"
        Me.Menu_PlayPicture.Size = New System.Drawing.Size(227, 22)
        Me.Menu_PlayPicture.Text = "幻灯片设置"
        '
        'Menu_Play
        '
        Me.Menu_Play.Name = "Menu_Play"
        Me.Menu_Play.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.Menu_Play.Size = New System.Drawing.Size(213, 22)
        Me.Menu_Play.Text = "播放/停止"
        '
        'Menu_IncreaseTimer
        '
        Me.Menu_IncreaseTimer.Name = "Menu_IncreaseTimer"
        Me.Menu_IncreaseTimer.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Up), System.Windows.Forms.Keys)
        Me.Menu_IncreaseTimer.Size = New System.Drawing.Size(213, 22)
        Me.Menu_IncreaseTimer.Text = "间隔增加1秒"
        '
        'Menu_DecreaseTimer
        '
        Me.Menu_DecreaseTimer.Name = "Menu_DecreaseTimer"
        Me.Menu_DecreaseTimer.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Down), System.Windows.Forms.Keys)
        Me.Menu_DecreaseTimer.Size = New System.Drawing.Size(213, 22)
        Me.Menu_DecreaseTimer.Text = "间隔减少1秒"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(210, 6)
        '
        'Menu_TimeInterval
        '
        Me.Menu_TimeInterval.Name = "Menu_TimeInterval"
        Me.Menu_TimeInterval.Size = New System.Drawing.Size(100, 23)
        Me.Menu_TimeInterval.Text = "3.0"
        Me.Menu_TimeInterval.ToolTipText = "手动设置幻灯片间隔时间，以秒为单位"
        '
        'Menu_SetWallpaper
        '
        Me.Menu_SetWallpaper.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_Tile, Me.Menu_Center, Me.Menu_Stretch, Me.ToolStripSeparator5, Me.Menu_Auto})
        Me.Menu_SetWallpaper.Name = "Menu_SetWallpaper"
        Me.Menu_SetWallpaper.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F12), System.Windows.Forms.Keys)
        Me.Menu_SetWallpaper.Size = New System.Drawing.Size(227, 22)
        Me.Menu_SetWallpaper.Text = "设置桌面壁纸(&P)"
        '
        'Menu_Tile
        '
        Me.Menu_Tile.Name = "Menu_Tile"
        Me.Menu_Tile.Size = New System.Drawing.Size(124, 22)
        Me.Menu_Tile.Text = "平铺"
        '
        'Menu_Center
        '
        Me.Menu_Center.Name = "Menu_Center"
        Me.Menu_Center.Size = New System.Drawing.Size(124, 22)
        Me.Menu_Center.Text = "居中"
        '
        'Menu_Stretch
        '
        Me.Menu_Stretch.Name = "Menu_Stretch"
        Me.Menu_Stretch.Size = New System.Drawing.Size(124, 22)
        Me.Menu_Stretch.Text = "拉伸"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(121, 6)
        '
        'Menu_Auto
        '
        Me.Menu_Auto.Name = "Menu_Auto"
        Me.Menu_Auto.Size = New System.Drawing.Size(124, 22)
        Me.Menu_Auto.Text = "自动判断"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(224, 6)
        '
        'Menu_EditPicture
        '
        Me.Menu_EditPicture.Name = "Menu_EditPicture"
        Me.Menu_EditPicture.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.Menu_EditPicture.Size = New System.Drawing.Size(227, 22)
        Me.Menu_EditPicture.Text = "编辑(&E)"
        '
        'Menu_Explorer
        '
        Me.Menu_Explorer.Name = "Menu_Explorer"
        Me.Menu_Explorer.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.Menu_Explorer.Size = New System.Drawing.Size(227, 22)
        Me.Menu_Explorer.Text = "在资源管理器中打开"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(224, 6)
        '
        'Menu_Previous
        '
        Me.Menu_Previous.Name = "Menu_Previous"
        Me.Menu_Previous.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Left), System.Windows.Forms.Keys)
        Me.Menu_Previous.Size = New System.Drawing.Size(227, 22)
        Me.Menu_Previous.Text = "上一张(&P)"
        '
        'Menu_Next
        '
        Me.Menu_Next.Name = "Menu_Next"
        Me.Menu_Next.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Right), System.Windows.Forms.Keys)
        Me.Menu_Next.Size = New System.Drawing.Size(227, 22)
        Me.Menu_Next.Text = "下一张(&N)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(224, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_Folder, Me.ToolStripSeparator7, Me.Menu_jpg, Me.Menu_jpeg, Me.Menu_png, Me.Menu_bmp, Me.ToolStripSeparator8, Me.Menu_All})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(227, 22)
        Me.ToolStripMenuItem1.Text = "文件关联(&Q)"
        '
        'Menu_Folder
        '
        Me.Menu_Folder.Checked = True
        Me.Menu_Folder.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Menu_Folder.Name = "Menu_Folder"
        Me.Menu_Folder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D0), System.Windows.Forms.Keys)
        Me.Menu_Folder.Size = New System.Drawing.Size(193, 22)
        Me.Menu_Folder.Text = "文件夹右键"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(190, 6)
        '
        'Menu_jpg
        '
        Me.Menu_jpg.Checked = True
        Me.Menu_jpg.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Menu_jpg.Name = "Menu_jpg"
        Me.Menu_jpg.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D1), System.Windows.Forms.Keys)
        Me.Menu_jpg.Size = New System.Drawing.Size(193, 22)
        Me.Menu_jpg.Text = "jpg图像"
        '
        'Menu_jpeg
        '
        Me.Menu_jpeg.Checked = True
        Me.Menu_jpeg.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Menu_jpeg.Name = "Menu_jpeg"
        Me.Menu_jpeg.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D2), System.Windows.Forms.Keys)
        Me.Menu_jpeg.Size = New System.Drawing.Size(193, 22)
        Me.Menu_jpeg.Text = "jpeg图像"
        '
        'Menu_png
        '
        Me.Menu_png.Checked = True
        Me.Menu_png.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Menu_png.Name = "Menu_png"
        Me.Menu_png.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D3), System.Windows.Forms.Keys)
        Me.Menu_png.Size = New System.Drawing.Size(193, 22)
        Me.Menu_png.Text = "png图像"
        '
        'Menu_bmp
        '
        Me.Menu_bmp.Checked = True
        Me.Menu_bmp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Menu_bmp.Name = "Menu_bmp"
        Me.Menu_bmp.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D4), System.Windows.Forms.Keys)
        Me.Menu_bmp.Size = New System.Drawing.Size(193, 22)
        Me.Menu_bmp.Text = "bmp位图"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(190, 6)
        '
        'Menu_All
        '
        Me.Menu_All.Checked = True
        Me.Menu_All.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Menu_All.Name = "Menu_All"
        Me.Menu_All.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.Menu_All.Size = New System.Drawing.Size(193, 22)
        Me.Menu_All.Text = "全部关联"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(224, 6)
        '
        'Menu_Exit
        '
        Me.Menu_Exit.Name = "Menu_Exit"
        Me.Menu_Exit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.Menu_Exit.Size = New System.Drawing.Size(227, 22)
        Me.Menu_Exit.Text = "退出(&X)"
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog"
        Me.OpenFileDialog.Filter = "(jpg图像)|*.jpg|(jpeg图像)|*.jpeg|(png图像)|*.png|(bmp位图)|*.bmp"
        '
        'PictureBox
        '
        Me.PictureBox.AutoSize = True
        Me.PictureBox.BackColor = System.Drawing.SystemColors.Control
        Me.PictureBox.ContextMenuStrip = Me.PopupMenu
        Me.PictureBox.EnableMouseDragging = False
        Me.PictureBox.EnableMouseWheelZooming = True
        Me.PictureBox.Image = Nothing
        Me.PictureBox.ImagePosition = New System.Drawing.Point(0, 0)
        Me.PictureBox.isAutoZoomWhileLoadPic = True
        Me.PictureBox.Location = New System.Drawing.Point(9, 12)
        Me.PictureBox.MaximumSize = New System.Drawing.Size(9000, 9000)
        Me.PictureBox.MaximumZoomFactor = 2.0R
        Me.PictureBox.MinimumImageHeight = 100
        Me.PictureBox.MinimumImageWidth = 100
        Me.PictureBox.MouseWheelDivisor = 800
        Me.PictureBox.Name = "PictureBox"
        Me.PictureBox.Size = New System.Drawing.Size(1237, 665)
        Me.PictureBox.TabIndex = 5
        Me.PictureBox.WindowAreaHeight = 1040
        Me.PictureBox.WindowAreaWidth = 1920
        Me.PictureBox.ZoomFactor = 0R
        Me.PictureBox.ZoomMode = PictureViewer.ZoomPictureBox.ZoomType.ControlCenter
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1258, 715)
        Me.ContextMenuStrip = Me.PopupMenu
        Me.Controls.Add(Me.PictureBox)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "图片浏览器"
        Me.PopupMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub





    Friend WithEvents PictureBox As ZoomPictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents PopupMenu As ContextMenuStrip
    Friend WithEvents Menu_PlayPicture As ToolStripMenuItem
    Friend WithEvents Menu_IncreaseTimer As ToolStripMenuItem
    Friend WithEvents Menu_DecreaseTimer As ToolStripMenuItem
    Friend WithEvents Menu_EditPicture As ToolStripMenuItem
    Friend WithEvents Menu_Previous As ToolStripMenuItem
    Friend WithEvents Menu_Next As ToolStripMenuItem
    Friend WithEvents Menu_PictureModel As ToolStripMenuItem
    Friend WithEvents Menu_Exit As ToolStripMenuItem
    Friend WithEvents Menu_Play As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents Menu_Explorer As ToolStripMenuItem
    Friend WithEvents Menu_SetWallpaper As ToolStripMenuItem
    Friend WithEvents Menu_TimeInterval As ToolStripTextBox
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents Menu_Tile As ToolStripMenuItem
    Friend WithEvents Menu_Center As ToolStripMenuItem
    Friend WithEvents Menu_Stretch As ToolStripMenuItem
    Friend WithEvents Menu_Auto As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents Menu_jpg As ToolStripMenuItem
    Friend WithEvents Menu_jpeg As ToolStripMenuItem
    Friend WithEvents Menu_png As ToolStripMenuItem
    Friend WithEvents Menu_bmp As ToolStripMenuItem
    Friend WithEvents Menu_Folder As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents Menu_All As ToolStripMenuItem
End Class
