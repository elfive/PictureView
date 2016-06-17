<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DeleteReg = New System.Windows.Forms.Button()
        Me.InitializeReg = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Folder_Associate = New System.Windows.Forms.CheckBox()
        Me.All_Associate = New System.Windows.Forms.CheckBox()
        Me.BMP_Associate = New System.Windows.Forms.CheckBox()
        Me.PNG_Associate = New System.Windows.Forms.CheckBox()
        Me.JPEG_Associate = New System.Windows.Forms.CheckBox()
        Me.JPG_Associate = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.ExePath = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DeleteReg)
        Me.GroupBox1.Controls.Add(Me.InitializeReg)
        Me.GroupBox1.Enabled = False
        Me.GroupBox1.Location = New System.Drawing.Point(14, 52)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(257, 85)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "注册表"
        '
        'DeleteReg
        '
        Me.DeleteReg.Location = New System.Drawing.Point(143, 31)
        Me.DeleteReg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DeleteReg.Name = "DeleteReg"
        Me.DeleteReg.Size = New System.Drawing.Size(87, 33)
        Me.DeleteReg.TabIndex = 1
        Me.DeleteReg.Text = "删除"
        Me.DeleteReg.UseVisualStyleBackColor = True
        '
        'InitializeReg
        '
        Me.InitializeReg.Location = New System.Drawing.Point(24, 31)
        Me.InitializeReg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.InitializeReg.Name = "InitializeReg"
        Me.InitializeReg.Size = New System.Drawing.Size(87, 33)
        Me.InitializeReg.TabIndex = 0
        Me.InitializeReg.Text = "初始化"
        Me.InitializeReg.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Folder_Associate)
        Me.GroupBox2.Controls.Add(Me.All_Associate)
        Me.GroupBox2.Controls.Add(Me.BMP_Associate)
        Me.GroupBox2.Controls.Add(Me.PNG_Associate)
        Me.GroupBox2.Controls.Add(Me.JPEG_Associate)
        Me.GroupBox2.Controls.Add(Me.JPG_Associate)
        Me.GroupBox2.Enabled = False
        Me.GroupBox2.Location = New System.Drawing.Point(14, 161)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(257, 149)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "关联选项"
        '
        'Folder_Associate
        '
        Me.Folder_Associate.AutoSize = True
        Me.Folder_Associate.Location = New System.Drawing.Point(163, 105)
        Me.Folder_Associate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Folder_Associate.Name = "Folder_Associate"
        Me.Folder_Associate.Size = New System.Drawing.Size(87, 21)
        Me.Folder_Associate.TabIndex = 5
        Me.Folder_Associate.Text = "文件夹右键"
        Me.Folder_Associate.UseVisualStyleBackColor = True
        '
        'All_Associate
        '
        Me.All_Associate.AutoSize = True
        Me.All_Associate.Location = New System.Drawing.Point(44, 105)
        Me.All_Associate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.All_Associate.Name = "All_Associate"
        Me.All_Associate.Size = New System.Drawing.Size(75, 21)
        Me.All_Associate.TabIndex = 4
        Me.All_Associate.Text = "关联所有"
        Me.All_Associate.UseVisualStyleBackColor = True
        '
        'BMP_Associate
        '
        Me.BMP_Associate.AutoSize = True
        Me.BMP_Associate.Location = New System.Drawing.Point(163, 71)
        Me.BMP_Associate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BMP_Associate.Name = "BMP_Associate"
        Me.BMP_Associate.Size = New System.Drawing.Size(54, 21)
        Me.BMP_Associate.TabIndex = 3
        Me.BMP_Associate.Text = "BMP"
        Me.BMP_Associate.UseVisualStyleBackColor = True
        '
        'PNG_Associate
        '
        Me.PNG_Associate.AutoSize = True
        Me.PNG_Associate.Location = New System.Drawing.Point(44, 71)
        Me.PNG_Associate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PNG_Associate.Name = "PNG_Associate"
        Me.PNG_Associate.Size = New System.Drawing.Size(53, 21)
        Me.PNG_Associate.TabIndex = 2
        Me.PNG_Associate.Text = "PNG"
        Me.PNG_Associate.UseVisualStyleBackColor = True
        '
        'JPEG_Associate
        '
        Me.JPEG_Associate.AutoSize = True
        Me.JPEG_Associate.Location = New System.Drawing.Point(163, 36)
        Me.JPEG_Associate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.JPEG_Associate.Name = "JPEG_Associate"
        Me.JPEG_Associate.Size = New System.Drawing.Size(55, 21)
        Me.JPEG_Associate.TabIndex = 1
        Me.JPEG_Associate.Text = "JPEG"
        Me.JPEG_Associate.UseVisualStyleBackColor = True
        '
        'JPG_Associate
        '
        Me.JPG_Associate.AutoSize = True
        Me.JPG_Associate.Location = New System.Drawing.Point(44, 36)
        Me.JPG_Associate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.JPG_Associate.Name = "JPG_Associate"
        Me.JPG_Associate.Size = New System.Drawing.Size(48, 21)
        Me.JPG_Associate.TabIndex = 0
        Me.JPG_Associate.Text = "JPG"
        Me.JPG_Associate.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Location = New System.Drawing.Point(14, 418)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(257, 142)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "其他"
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog"
        Me.OpenFileDialog.Filter = "PictureViewer文件|PictureViewer.exe"
        '
        'ExePath
        '
        Me.ExePath.ForeColor = System.Drawing.Color.Gray
        Me.ExePath.Location = New System.Drawing.Point(14, 16)
        Me.ExePath.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ExePath.Name = "ExePath"
        Me.ExePath.Size = New System.Drawing.Size(256, 23)
        Me.ExePath.TabIndex = 3
        Me.ExePath.Text = "双击选择PictureViewer.exe路径"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(285, 322)
        Me.Controls.Add(Me.ExePath)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Form1"
        Me.Text = "PictureViewerSetup"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DeleteReg As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents BMP_Associate As CheckBox
    Friend WithEvents PNG_Associate As CheckBox
    Friend WithEvents JPEG_Associate As CheckBox
    Friend WithEvents JPG_Associate As CheckBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents InitializeReg As Button
    Friend WithEvents All_Associate As CheckBox
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents ExePath As TextBox
    Friend WithEvents Folder_Associate As CheckBox
End Class
