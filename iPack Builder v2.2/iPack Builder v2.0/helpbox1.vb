Public Class helpbox1

    Private Sub helpbox1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Form1.Enabled = True
    End Sub

    Private Sub helpbox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Form1.Enabled = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New Point(Form1.Location.X + (Form1.Width - Me.Width) / 2, Form1.Location.Y + (Form1.Height - Me.Height) / 2)
        Me.BringToFront()
        Owner = Form1
       
        ' Form1.Enabled = False

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim open As New ProcessStartInfo
        open.FileName = "http://mrbladedesigns.com/ipack-builder/"
        Process.Start(open)
        Me.Close()

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Dim donate As New ProcessStartInfo
        donate.FileName = ("http://mrbladedesigns.com/downloads/mrblade-designs-donation/")
        Process.Start(donate)
        Me.Close()
    End Sub
End Class