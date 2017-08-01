<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.mnuGame = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuGame_New = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuFile_Exit = New System.Windows.Forms.ToolStripMenuItem
        Me.pnInfo = New System.Windows.Forms.Panel
        Me.picBgrSel = New System.Windows.Forms.PictureBox
        Me.lblScore = New System.Windows.Forms.Label
        Me.picBgr = New System.Windows.Forms.PictureBox
        Me.pnGame = New System.Windows.Forms.Panel
        Me.mnuInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuInfo_Author = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMain = New System.Windows.Forms.MenuStrip
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuOpen = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSave = New System.Windows.Forms.ToolStripMenuItem
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        Me.imgSmall = New System.Windows.Forms.ImageList(Me.components)
        Me.imgGo = New System.Windows.Forms.ImageList(Me.components)
        Me.TimerGo = New System.Windows.Forms.Timer(Me.components)
        Me.dlgOpen = New System.Windows.Forms.OpenFileDialog
        Me.dlgSave = New System.Windows.Forms.SaveFileDialog
        Me.lstGraph1 = New System.Windows.Forms.ListBox
        Me.lstGraph2 = New System.Windows.Forms.ListBox
        Me.pnInfo.SuspendLayout()
        CType(Me.picBgrSel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBgr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuGame
        '
        Me.mnuGame.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuGame_New, Me.ToolStripMenuItem1, Me.mnuFile_Exit})
        Me.mnuGame.Name = "mnuGame"
        Me.mnuGame.Size = New System.Drawing.Size(63, 20)
        Me.mnuGame.Text = "Trò chơi"
        '
        'mnuGame_New
        '
        Me.mnuGame_New.Name = "mnuGame_New"
        Me.mnuGame_New.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.mnuGame_New.Size = New System.Drawing.Size(205, 22)
        Me.mnuGame_New.Text = "Tạo mới trò chơi"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(202, 6)
        '
        'mnuFile_Exit
        '
        Me.mnuFile_Exit.Name = "mnuFile_Exit"
        Me.mnuFile_Exit.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.mnuFile_Exit.Size = New System.Drawing.Size(205, 22)
        Me.mnuFile_Exit.Text = "Thoát"
        '
        'pnInfo
        '
        Me.pnInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnInfo.Controls.Add(Me.picBgrSel)
        Me.pnInfo.Controls.Add(Me.lblScore)
        Me.pnInfo.Controls.Add(Me.picBgr)
        Me.pnInfo.Location = New System.Drawing.Point(0, 27)
        Me.pnInfo.Name = "pnInfo"
        Me.pnInfo.Size = New System.Drawing.Size(409, 77)
        Me.pnInfo.TabIndex = 3
        '
        'picBgrSel
        '
        Me.picBgrSel.BackgroundImage = Global.prjLine.My.Resources.Resources.bgrPixelSelect
        Me.picBgrSel.Location = New System.Drawing.Point(87, -1)
        Me.picBgrSel.Name = "picBgrSel"
        Me.picBgrSel.Size = New System.Drawing.Size(70, 68)
        Me.picBgrSel.TabIndex = 7
        Me.picBgrSel.TabStop = False
        Me.picBgrSel.Visible = False
        '
        'lblScore
        '
        Me.lblScore.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblScore.Font = New System.Drawing.Font("Courier New", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScore.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblScore.Location = New System.Drawing.Point(227, 9)
        Me.lblScore.Name = "lblScore"
        Me.lblScore.Size = New System.Drawing.Size(192, 68)
        Me.lblScore.TabIndex = 3
        Me.lblScore.Text = "0"
        Me.lblScore.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'picBgr
        '
        Me.picBgr.BackgroundImage = Global.prjLine.My.Resources.Resources.bgrPixel
        Me.picBgr.Location = New System.Drawing.Point(0, 0)
        Me.picBgr.Name = "picBgr"
        Me.picBgr.Size = New System.Drawing.Size(71, 68)
        Me.picBgr.TabIndex = 6
        Me.picBgr.TabStop = False
        Me.picBgr.Visible = False
        '
        'pnGame
        '
        Me.pnGame.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnGame.Location = New System.Drawing.Point(3, 108)
        Me.pnGame.Name = "pnGame"
        Me.pnGame.Size = New System.Drawing.Size(400, 400)
        Me.pnGame.TabIndex = 5
        '
        'mnuInfo
        '
        Me.mnuInfo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuInfo_Author})
        Me.mnuInfo.Name = "mnuInfo"
        Me.mnuInfo.Size = New System.Drawing.Size(71, 20)
        Me.mnuInfo.Text = "Thông tin"
        '
        'mnuInfo_Author
        '
        Me.mnuInfo_Author.Name = "mnuInfo_Author"
        Me.mnuInfo_Author.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.mnuInfo_Author.Size = New System.Drawing.Size(149, 22)
        Me.mnuInfo_Author.Text = "Tác giả"
        '
        'mnuMain
        '
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuGame, Me.mnuInfo, Me.mnuFile})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(406, 24)
        Me.mnuMain.TabIndex = 4
        Me.mnuMain.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOpen, Me.mnuSave})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(56, 20)
        Me.mnuFile.Text = "Tập tin"
        Me.mnuFile.Visible = False
        '
        'mnuOpen
        '
        Me.mnuOpen.Name = "mnuOpen"
        Me.mnuOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.mnuOpen.Size = New System.Drawing.Size(159, 22)
        Me.mnuOpen.Text = "Mở trận"
        '
        'mnuSave
        '
        Me.mnuSave.Name = "mnuSave"
        Me.mnuSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuSave.Size = New System.Drawing.Size(159, 22)
        Me.mnuSave.Text = "Lưu trận"
        '
        'imgList
        '
        Me.imgList.ImageStream = CType(resources.GetObject("imgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList.Images.SetKeyName(0, "1.png")
        Me.imgList.Images.SetKeyName(1, "2.png")
        Me.imgList.Images.SetKeyName(2, "3.png")
        Me.imgList.Images.SetKeyName(3, "4.png")
        '
        'imgSmall
        '
        Me.imgSmall.ImageStream = CType(resources.GetObject("imgSmall.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgSmall.TransparentColor = System.Drawing.Color.Transparent
        Me.imgSmall.Images.SetKeyName(0, "1.png")
        Me.imgSmall.Images.SetKeyName(1, "2.png")
        Me.imgSmall.Images.SetKeyName(2, "3.png")
        Me.imgSmall.Images.SetKeyName(3, "4.png")
        '
        'imgGo
        '
        Me.imgGo.ImageStream = CType(resources.GetObject("imgGo.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgGo.TransparentColor = System.Drawing.Color.Transparent
        Me.imgGo.Images.SetKeyName(0, "popo60.png")
        '
        'TimerGo
        '
        '
        'lstGraph1
        '
        Me.lstGraph1.FormattingEnabled = True
        Me.lstGraph1.Location = New System.Drawing.Point(415, 27)
        Me.lstGraph1.Name = "lstGraph1"
        Me.lstGraph1.Size = New System.Drawing.Size(66, 472)
        Me.lstGraph1.TabIndex = 6
        Me.lstGraph1.Visible = False
        '
        'lstGraph2
        '
        Me.lstGraph2.FormattingEnabled = True
        Me.lstGraph2.Location = New System.Drawing.Point(487, 27)
        Me.lstGraph2.Name = "lstGraph2"
        Me.lstGraph2.Size = New System.Drawing.Size(66, 472)
        Me.lstGraph2.TabIndex = 7
        Me.lstGraph2.Visible = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 511)
        Me.Controls.Add(Me.lstGraph2)
        Me.Controls.Add(Me.lstGraph1)
        Me.Controls.Add(Me.pnInfo)
        Me.Controls.Add(Me.pnGame)
        Me.Controls.Add(Me.mnuMain)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Line"
        Me.pnInfo.ResumeLayout(False)
        CType(Me.picBgrSel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBgr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mnuGame As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGame_New As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnInfo As System.Windows.Forms.Panel
    Friend WithEvents lblScore As System.Windows.Forms.Label
    Friend WithEvents pnGame As System.Windows.Forms.Panel
    Friend WithEvents mnuInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInfo_Author As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents imgSmall As System.Windows.Forms.ImageList
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuFile_Exit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents imgGo As System.Windows.Forms.ImageList
    Friend WithEvents TimerGo As System.Windows.Forms.Timer
    Friend WithEvents picBgrSel As System.Windows.Forms.PictureBox
    Friend WithEvents picBgr As System.Windows.Forms.PictureBox
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dlgOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents dlgSave As System.Windows.Forms.SaveFileDialog
    Friend WithEvents lstGraph1 As System.Windows.Forms.ListBox
    Friend WithEvents lstGraph2 As System.Windows.Forms.ListBox

End Class
