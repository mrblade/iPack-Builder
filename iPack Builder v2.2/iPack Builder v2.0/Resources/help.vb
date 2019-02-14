Public Class helpbox

    Private Sub help_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Form1.Enabled = True
    End Sub

    Private Sub help_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New Point(Form1.Location.X + 65, Form1.Location.Y + 80)
        Form1.Enabled = False
        Me.TopMost = True
        If Form1.TextBox1.Visible = True Then
            Me.BackgroundImage = My.Resources.helpbox
            LinkLabel1.Visible = True
            PictureBox1.Visible = True
            Me.Refresh()

        End If
        If Form1.maintab.ListBox1.Enabled = True Then
            Me.BackgroundImage = My.Resources.reshelp
            Me.Refresh()

        End If
        If Form1.foldertab.ListBox1.Enabled = True Then
            Me.BackgroundImage = My.Resources.folderhelp
            Me.Refresh()
        End If

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim open As New ProcessStartInfo
        open.FileName = ("http://mrblade.info/ipack-builder")
        Process.Start(open)
        Me.Close()


    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Dim donate As New ProcessStartInfo
        donate.FileName = ("http://mrblade.info/downloads/mrblade-designs-donation/")
        Process.Start(donate)
        Me.Close()
    End Sub
End Class