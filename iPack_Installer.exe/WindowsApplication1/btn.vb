Public Class btn
    Public val, val2, val3, val4, val5, val6, val7, val8, val9, val10, cd As String
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim c As Integer = 0


        If ad1.a = 1 And c = 0 And Form1.Controls.Contains(Form1.showfiles) Then
            Try
                Form1.advert.Button1_Click(Nothing, Nothing)
            Catch ex As Exception

            End Try
            c = c + 1
            Return

        End If

        If Form1.Controls.Contains(Form1.Uninstaller) = True And c = 0 Then
            Form1.Uninstaller.Button2_Click(Nothing, Nothing)
            c = c + 1
            Return
        End If

        If Form1.RichTextBox2.Visible = True And c = 0 And Form1.CheckBox1.Checked = True Then
            Form1.Button1_Click(Nothing, Nothing)
            c = c + 1
            Return
        End If

        If Form1.RichTextBox2.Visible = True And c = 0 And Form1.CheckBox1.Checked = False Then
            If Form1.ComboBox1.SelectedIndex = 0 Then
                MessageBox.Show("Please accept the agreement.", Form1.pack, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Form1.ComboBox1.SelectedIndex = 1 Then
                MessageBox.Show("Por favor acepte el acuerdo.", Form1.pack, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Form1.ComboBox1.SelectedIndex = 2 Then
                MessageBox.Show("Пожалуйста, примите соглашение.", Form1.pack, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Form1.ComboBox1.SelectedIndex = 3 Then
                MessageBox.Show("Bitte akzeptieren Sie die Vereinbarung.", Form1.pack, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            c = c + 1
            Return
        End If


        If Form1.showfiles.Visible = True And c = 0 Then
            If Form1.showfiles.Button1.Enabled = False Then
                If Form1.ComboBox1.SelectedIndex = 0 Then
                    MessageBox.Show("No file selected to patch.", Form1.pack, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf Form1.ComboBox1.SelectedIndex = 1 Then
                    MessageBox.Show("Ningún archivo seleccionado para parcheo.", Form1.pack, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf Form1.ComboBox1.SelectedIndex = 2 Then
                    MessageBox.Show("Файл не выбран патч.", Form1.pack, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf Form1.ComboBox1.SelectedIndex = 3 Then
                    MessageBox.Show("Keine Datei zum Patch ausgewählt.", Form1.pack, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                c = c + 1
                Return
            ElseIf Form1.showfiles.TreeView1.Nodes.Count = 0 Then
                Form1.Opacity = 0
                If Form1.ComboBox1.SelectedIndex = 0 Then
                    MessageBox.Show("No .res file present to patch, installer will exit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ElseIf Form1.ComboBox1.SelectedIndex = 1 Then
                    MessageBox.Show("No ningún archivo de res, de instalación se cerrará.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ElseIf Form1.ComboBox1.SelectedIndex = 2 Then
                    MessageBox.Show("Нет .res файл подать залатать, установщик выхода.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ElseIf Form1.ComboBox1.SelectedIndex = 3 Then
                    MessageBox.Show("Keine .res Datei anwesend, Installationsprogramm wird beendet.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                Application.Exit()

            Else
                Form1.showfiles.Button1_Click(Nothing, Nothing)
                c = c + 1
                Return
                End If
            End If

        If Form1.Controls.Contains(Form1.Uninstaller) = True And c = 0 Then
            Form1.Uninstaller.Button2_Click(Nothing, Nothing)
            c = c + 1
            Return
        End If
      
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim c As Integer = 0

        If ad1.a = 1 And c = 0 And Form1.Controls.Contains(Form1.showfiles) Then
            Form1.advert.Button2_Click(Nothing, Nothing)
            c = c + 1
            Return

        End If



        If Form1.RichTextBox2.Visible = True And c = 0 Then
            Form1.Button2_Click(Nothing, Nothing)
            c = c + 1
        End If

        If Form1.Controls.Contains(Form1.showfiles) = True And c = 0 Then

            Form1.showfiles.Button2_Click(Nothing, Nothing)
            c = c + 1
        End If

        If Form1.Controls.Contains(Form1.Uninstaller) = True And c = 0 Then
            Form1.Uninstaller.Button1_Click(Nothing, Nothing)
            c = c + 1
        End If

    End Sub


    Private Sub btn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim a As Integer = 0
        Dim root As XElement = XDocument.Load(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\Theme.xml").Root
        cd = My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme"
        Me.Location = New Point(Form1.Button1.Location.X, Form1.Button1.Location.Y)
        Form1.Button1.Visible = False
        Form1.Button2.Visible = False
        Me.Width = Form1.Button1.Width + Form1.Button2.Width + 4
        '   Form1.Uninstaller.Button1.Visible = False
        ' Form1.Uninstaller.Button2.Visible = False

        Try
            val = (root.Element("buttons").Element("next-normal").Value)
            val2 = (root.Element("buttons").Element("next-hover").Value)
            val3 = (root.Element("buttons").Element("next-pressed").Value)
            val4 = (root.Element("buttons").Element("cancel-normal").Value)
            val5 = (root.Element("buttons").Element("cancel-hover").Value)
            val6 = (root.Element("buttons").Element("cancel-pressed").Value)
            val7 = (root.Element("buttons").Element("finish-pressed").Value)
            val8 = (root.Element("buttons").Element("finish-pressed").Value)
            val9 = (root.Element("buttons").Element("finish-pressed").Value)
            val10 = (root.Element("buttons").Element("next-disabled").Value)

            If val = Nothing Or val2 = Nothing Or val3 = Nothing Or Not My.Computer.FileSystem.FileExists(cd & "\" & val) Or Not My.Computer.FileSystem.FileExists(cd & "\" & val2) Or Not My.Computer.FileSystem.FileExists(cd & "\" & val3) Then
                PictureBox1.Visible = False
                Form1.Button1.Visible = True
                Form1.Button1.BringToFront()
                '  Form1.Uninstaller.Button2.Visible = True
                PictureBox2.Location = PictureBox1.Location
                Me.Size = Form1.Button2.Size
                Me.Location = New Point(Form1.Button2.Location.X, Form1.Button2.Location.Y)
                a = a + 1
            Else

                PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val)


            End If

            If val4 = Nothing Or val5 = Nothing Or val6 = Nothing Or Not My.Computer.FileSystem.FileExists(cd & "\" & val4) Or Not My.Computer.FileSystem.FileExists(cd & "\" & val5) Or Not My.Computer.FileSystem.FileExists(cd & "\" & val6) Then
                PictureBox2.Visible = False
                Form1.Button2.Visible = True
                Form1.Button2.BringToFront()
                '    Form1.Uninstaller.Button1.Visible = True

                '  PictureBox2.SendToBack()
                Me.Size = Form1.Button1.Size
                ' Me.Location = New Point(Form1.Button2.Location.X, Form1.Button2.Location.Y - 30)
                a = a + 1
            Else

                PictureBox2.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val4)
                '    PictureBox2.Left = Form1.Button2.Left
            End If

            If a = 2 Then
                Me.Width = 0
                Me.Height = 0
            End If




        Catch ex As Exception

        End Try





    End Sub

    Public Function changenext(ByVal e As Integer)

        If e = 0 Then

            Try

                PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val10)
                PictureBox1.Refresh()

                PictureBox1.Enabled = False

            Catch ex As Exception

            End Try

            'Else
            Try
                ' PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val)
                '  PictureBox1.Enabled = False
            Catch ex As Exception

            End Try
            '  End If
        ElseIf e = 1 Then
        Try
            PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val)
            PictureBox1.Enabled = True
        Catch ex As Exception

        End Try
        End If

    End Function

    Public Sub PictureBox1_EnabledChanged(sender As Object, e As EventArgs) Handles PictureBox1.EnabledChanged
       
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        If Not val3 = Nothing Then
            PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val3)

        End If
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter

        If Form1.advert.a = 1 Then
            Try
                PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val10)
            Catch ex As Exception

            End Try
            Return
        End If

        If Not val2 = Nothing Then
            PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val2)

        End If
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        If Form1.advert.a = 1 Then
            Try
                PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val10)
            Catch ex As Exception

            End Try
            Return
        End If
        If Not val = Nothing Then
            PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val)

        End If
    End Sub

    Private Sub PictureBox2_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseDown
        PictureBox2.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val6)
    End Sub

    Private Sub PictureBox2_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox2.MouseEnter
        PictureBox2.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val5)
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val4)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs)
        Dim c As Integer = 0
        If c = 0 And Form1.TextBox2.Visible = True Then
            Form1.Button3_Click_1(Nothing, Nothing)
            c = c + 1
        End If
    End Sub

   
End Class
