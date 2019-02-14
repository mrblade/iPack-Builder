Public Class btnfinish
    Public val, val2, val3 As String
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim c As Integer = 0
        If c = 0 And Form1.TextBox2.Visible = True Then
            Form1.Button3_Click_1(Nothing, Nothing)
            c = c + 1
        End If
    End Sub

    Private Sub btnfinish_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim root As XElement = XDocument.Load(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\Theme.xml").Root
        Form1.Button3.Visible = False
        Dim cd As String = My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme"

        


        Try
            val = (root.Element("buttons").Element("finish-normal").Value)
            val2 = (root.Element("buttons").Element("finish-hover").Value)
            val3 = (root.Element("buttons").Element("finish-pressed").Value)
            If Not val = Nothing Then
                PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val)

            End If
        Catch ex As Exception
        End Try
        If val = Nothing Or val2 = Nothing Or val3 = Nothing Or Not My.Computer.FileSystem.FileExists(cd & "\" & val) Or Not My.Computer.FileSystem.FileExists(cd & "\" & val2) Or Not My.Computer.FileSystem.FileExists(cd & "\" & val3) Then
            PictureBox1.Visible = False
            Form1.Button3.Visible = True
            '  Form1.Uninstaller.Button1.Visible = True
        End If
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        Try
            If Not val3 = Nothing Then
                PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val3)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        If Not val2 = Nothing Then
            PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val2)

        End If
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        If Not val = Nothing Then
            PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val)

        End If
    End Sub
End Class
