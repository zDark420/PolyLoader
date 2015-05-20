<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.ReactorTheme1 = New ReactorTheme()
        Me.ReactorButton2 = New ReactorButton()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ReactorButton1 = New ReactorButton()
        Me.ReactorTheme1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ReactorTheme1
        '
        Me.ReactorTheme1.Controls.Add(Me.ReactorButton2)
        Me.ReactorTheme1.Controls.Add(Me.TextBox1)
        Me.ReactorTheme1.Controls.Add(Me.ReactorButton1)
        Me.ReactorTheme1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReactorTheme1.Font = New System.Drawing.Font("Verdana", 6.75!)
        Me.ReactorTheme1.Location = New System.Drawing.Point(0, 0)
        Me.ReactorTheme1.Name = "ReactorTheme1"
        Me.ReactorTheme1.Size = New System.Drawing.Size(284, 262)
        Me.ReactorTheme1.TabIndex = 0
        Me.ReactorTheme1.Text = "PollyPockets v0.1"
        '
        'ReactorButton2
        '
        Me.ReactorButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.ReactorButton2.Font = New System.Drawing.Font("Verdana", 6.75!)
        Me.ReactorButton2.Location = New System.Drawing.Point(93, 41)
        Me.ReactorButton2.Name = "ReactorButton2"
        Me.ReactorButton2.Size = New System.Drawing.Size(75, 23)
        Me.ReactorButton2.TabIndex = 6
        Me.ReactorButton2.Text = "Clear"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 78)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(260, 172)
        Me.TextBox1.TabIndex = 5
        '
        'ReactorButton1
        '
        Me.ReactorButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.ReactorButton1.Font = New System.Drawing.Font("Verdana", 6.75!)
        Me.ReactorButton1.Location = New System.Drawing.Point(12, 41)
        Me.ReactorButton1.Name = "ReactorButton1"
        Me.ReactorButton1.Size = New System.Drawing.Size(75, 23)
        Me.ReactorButton1.TabIndex = 4
        Me.ReactorButton1.Text = "Junk Generator"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.ReactorTheme1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PolyPockets"
        Me.ReactorTheme1.ResumeLayout(False)
        Me.ReactorTheme1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ReactorTheme1 As ReactorTheme
    Friend WithEvents ReactorButton1 As ReactorButton
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ReactorButton2 As ReactorButton
End Class
