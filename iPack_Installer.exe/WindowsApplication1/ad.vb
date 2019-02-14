Imports System.Net

Public Class filesx
    Dim count As Integer
    Dim showad As Integer
    Public a As Integer = 0

    Public Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim r As String

        If Form1.ComboBox1.SelectedIndex = 0 Then
            r = MessageBox.Show("Are you sure you want to quit?", Form1.mainheading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        ElseIf Form1.ComboBox1.SelectedIndex = 1 Then
            r = MessageBox.Show("¿Está seguro que desea salir?", Form1.mainheading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        ElseIf Form1.ComboBox1.SelectedIndex = 2 Then
            r = MessageBox.Show("Вы уверены, что вы хотите бросить курить?", Form1.mainheading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        ElseIf Form1.ComboBox1.SelectedIndex = 3 Then
            r = MessageBox.Show("Sind Sie sicher, dass Sie aufhören möchten?", Form1.mainheading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        End If

        If r = DialogResult.No Then
        ElseIf r = DialogResult.Yes Then
            Try

                My.Computer.FileSystem.DeleteFile("Patcher.log")
                My.Computer.FileSystem.DeleteFile("Patcher.ini")
                My.Computer.FileSystem.DeleteDirectory("Resource Files\ACL", FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception

            End Try
            Dim objWriter As New System.IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & Form1.pack & ".bat")
            objWriter.Write("cd %windir%")
            objWriter.Write(vbNewLine)
            objWriter.Write("tasklist /FI " & Chr(34) & "IMAGENAME eq iPack_Installer.exe" & Chr(34) & " 2>NUL | find /I /N " & Chr(34) & "iPack_Installer.exe" & Chr(34) & ">NUL")
            objWriter.Write(vbNewLine)
            objWriter.Write("if " & Chr(34) & "%ERRORLEVEL%" & Chr(34) & "==" & Chr(34) & "0" & Chr(34) & " (")
            objWriter.Write(vbNewLine)
            objWriter.Write("timeout 3")
            objWriter.Write(vbNewLine)
            objWriter.Write("RD /s /q " & Chr(34) & My.Computer.FileSystem.CurrentDirectory & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write(" ) else (")
            objWriter.Write(vbNewLine)
            objWriter.Write("timeout 2")
            objWriter.Write(vbNewLine)
            objWriter.Write("RD /s /q " & Chr(34) & My.Computer.FileSystem.CurrentDirectory & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write(" )")
            objWriter.Write(vbNewLine)
            objWriter.Write("DEL " & Chr(34) & "%~f0" & Chr(34))
            objWriter.Close()
            Dim launch As New ProcessStartInfo()
            launch.WindowStyle = ProcessWindowStyle.Hidden
            launch.UseShellExecute = True
            launch.FileName = (My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & Form1.pack & ".bat")
            launch.WorkingDirectory = ""
            Process.Start(launch)
            Application.Exit()
        End If
    End Sub
    Public Function IsConnectionAvailable() As Boolean
        Dim objUrl As New System.Uri("https://www.google.com")
        Dim objWebReq As System.Net.WebRequest
        objWebReq = System.Net.WebRequest.Create(objUrl)
        Dim objresp As System.Net.WebResponse
        Try
            objresp = objWebReq.GetResponse
            objresp.Close()
            objresp = Nothing
            Return True
        Catch ex As Exception
            objresp = Nothing
            objWebReq = Nothing
            Return False
        End Try
    End Function
    Public Sub changelocation()

        Try
            Button1.Location = New Point(Form1.Button1.Location.X, Form1.Button1.Location.Y)
            Button2.Location = New Point(Form1.Button2.Location.X, Form1.Button2.Location.Y)
            Button1.Size = Form1.Button1.Size
            Button2.Size = Form1.Button2.Size
            Label1.Left = 3
            Label1.Top = Button1.Top + 6
            Label2.Top = Label1.Top
            Label2.Left = Label1.Right
            WebBrowser1.Width = Form1.Width
            WebBrowser1.Height = Form1.line1.Top

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '  Form1.button.PictureBox1.Cursor = Cursors.No
        Try
            a = 1
            Form1.button.PictureBox1.Image = Image.FromFile(Form1.button.cd & "\" & Form1.button.val10)
            Form1.button.changenext(0)


        Catch ex As Exception

        End Try




        showad = 0
        changelocation()
        ad1.imgThumb = Nothing
        '  Form1.showfiles.Hide()
        ' Form1.Controls.Remove(Form1.showfiles)
        Timer1.Start()
        count = 10
        Label3.Visible = False
        WebBrowser1.Hide()

        If Form1.ComboBox1.SelectedIndex = 0 Then

            Button1.Text = "Next >"
            Button2.Text = "Cancel"
            Label1.Text = "Please wait :"
            Label2.Location = New Point(Label1.Width + 9, Label1.Location.Y)
            Label3.Text = "No internet connection."

        End If
        If Form1.ComboBox1.SelectedIndex = 1 Then

            Button1.Text = "Siguiente >"
            Button2.Text = "Cancelar"
            Label1.Text = "Por favor espera :"
            Label3.Text = "No hay conexión a internet."
            Label2.Location = New Point(Label1.Width + 9, Label1.Location.Y)
        End If
        If Form1.ComboBox1.SelectedIndex = 2 Then

            Button1.Text = "Следующая >"
            Button2.Text = "Отменить"
            Label1.Text = "Пожалуйста, подождите :"
            Label3.Text = "Нет подключение к интернету."
            Label2.Location = New Point(Label1.Width + 9, Label1.Location.Y)
        End If
        If Form1.ComboBox1.SelectedIndex = 3 Then

            Button1.Text = "Nächste >"
            Button2.Text = "Stornieren"
            Label1.Text = "Bitte warten :"
            Label3.Text = "Keine Internetverbindung."
            Label2.Location = New Point(Label1.Width + 9, Label1.Location.Y)
        End If
        BackgroundWorker1.RunWorkerAsync()


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        count = count - 1
        Label2.Text = count & " s"
        If count = 0 Then
            Timer1.Stop()
            Button1.Enabled = True
            Label1.Hide()
            Label2.Hide()
            WebBrowser1.Stop()
            BackgroundWorker1.WorkerSupportsCancellation = True
            BackgroundWorker1.CancelAsync()
            Try
                a = 0
                Form1.button.changenext(1)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        WebBrowser1.Hide()
        Label1.Hide()
        Label2.Hide()
        Label3.Hide()
        Button1.Hide()
        Button2.Hide()

        Form1.adv = 0
        Form1.showselect = 1
        Form1.Button1_Click(Nothing, Nothing)

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If IsConnectionAvailable() = True Then
            showad = 1
        Else
            showad = 0
        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If showad = 1 Then
            '  WebBrowser1.Url = New Uri("http://mrbladedesigns.com/")
            WebBrowser1.Url = New Uri("http://mrbladedesigns.com/app-ads/")
            WebBrowser1.Visible = True
        ElseIf showad = 0 Then
            Label3.Visible = True

        End If
    End Sub

   
    
End Class
