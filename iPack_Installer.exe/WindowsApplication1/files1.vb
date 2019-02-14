Imports System.Net
Imports System.Drawing.Drawing2D

Public Class ad1
    Dim count, ncount As Integer
    Dim showad As Integer
    Public Shared imgThumb As Image = Nothing
    Dim dirname, f, file0, pos, file1 As String
    Dim nodename As String
    Dim stop_recursion As Boolean = True
    Public Shared a As Integer = 0
    Public Function theme()

        Dim root As XElement = XDocument.Load(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\Theme.xml").Root
        Dim val As String

        Try
            val = (root.Element("files-window").Element("list-backcolor").Value)
            If Not val = Nothing Then
                TreeView1.BackColor = ColorTranslator.FromHtml(val)
                TreeView1.BorderStyle = Windows.Forms.BorderStyle.None
            End If
        Catch ex As Exception

        End Try
        Try
            val = (root.Element("files-window").Element("list-textcolor").Value)
            If Not val = Nothing Then
                TreeView1.ForeColor = ColorTranslator.FromHtml(val)

            End If
        Catch ex As Exception

        End Try
        Try
            val = (root.Element("files-window").Element("node-linecolor").Value)
            If Not val = Nothing Then

                TreeView1.LineColor = ColorTranslator.FromHtml(val)
            End If
        Catch ex As Exception

        End Try
    End Function
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


    Public Sub changelocation()

        Try


            Button1.Location = New Point(Form1.Button1.Location.X, Form1.Button1.Location.Y)
            Button2.Location = New Point(Form1.Button2.Location.X, Form1.Button2.Location.Y)
            Button1.Size = Form1.Button1.Size
            Button2.Size = Form1.Button2.Size
            TreeView1.Width = Form1.ProgressBar1.Width / 1.35
            TreeView1.Left = (Form1.ProgressBar1.Width - TreeView1.Width) / 1.9
            RadioButton1.Left = TreeView1.Left
            RadioButton2.Left = TreeView1.Left
            Label1.Left = TreeView1.Left - 2
            RadioButton1.Top = Form1.Label1.Top - 10
            RadioButton2.Top = RadioButton1.Top + 25
            TreeView1.Top = RadioButton2.Top + 25
            Label1.Top = Form1.line1.Top - 20
            TreeView1.Height = Form1.TextBox2.Height - (Label1.Height * 1.2)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ad_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        changelocation()
        Form1.imgThumb = Nothing
        If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme") And My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\Theme.xml") Then
            theme()
        End If

        Dim x As Integer = Form1.TextBox1.Location.X
        Dim h As Integer = Form1.TextBox1.Size.Height
        ncount = 0
        Try
            Dim image2 As Image = Nothing
            image2 = Image.FromFile("Setup files-iPack\header.png")
            imgThumb = image2.GetThumbnailImage(Form1.TextBox2.Width + 6, image2.Height, Nothing, New IntPtr())
            Me.Refresh()
        Catch ex As Exception

        End Try


        If Form1.ComboBox1.SelectedIndex = 0 Then

            Button1.Text = "Next >"
            Button2.Text = "Cancel"


        End If
        If Form1.ComboBox1.SelectedIndex = 1 Then

            Button1.Text = "Siguiente >"
            Button2.Text = "Cancelar"

        End If
        If Form1.ComboBox1.SelectedIndex = 2 Then

            Button1.Text = "Следующая >"
            Button2.Text = "Отменить"

        End If
        If Form1.ComboBox1.SelectedIndex = 3 Then

            Button1.Text = "Nächste >"
            Button2.Text = "Stornieren"

        End If



        Try
            For Each File In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory & "\Resource Files")
                If File.Contains(".res") Then
                    Dim fx, fz, pos As String
                    fx = My.Computer.FileSystem.GetName(File).TrimStart
                    pos = fx.IndexOf(".res")
                    fz = fx.Remove(pos, 4)
                    Dim t As New TreeNode
                    t.Text = fz
                    t.Checked = True
                    TreeView1.BeginUpdate()
                    TreeView1.Nodes.Add(t)
                    TreeView1.EndUpdate()
                Else

                End If
            Next
            If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Program Files") Then

                For Each Dir As String In System.IO.Directory.GetDirectories("Resource Files\Program Files")
                    f = Dir.TrimStart
                    dirname = IO.Path.GetFileName(Dir)
                    Dim t2 As New TreeNode

                    t2.Text = dirname
                    t2.Checked = True

                    TreeView1.BeginUpdate()
                    TreeView1.Nodes.Add(t2)
                    TreeView1.EndUpdate()
                    For Each File In My.Computer.FileSystem.GetFiles("Resource Files\Program Files\" & dirname)

                        If File.Contains(".res") Then
                            file0 = My.Computer.FileSystem.GetName(File)
                            f = file0.TrimStart
                            pos = f.IndexOf(".res")
                            file1 = f.Remove(pos, 4)
                            Dim t As New TreeNode
                            ' t.Name = dirname

                            '   t.Text = file1 & " " & "(" & dirname & ")"
                            t.Text = file1


                            t2.Nodes.Add(t)
                            't2.Expand()

                            If t2.Checked = True Then
                                t.Checked = True

                            Else
                                '  t.Checked = False

                            End If
                        Else


                        End If
                    Next
                Next
            End If
            If TreeView1.Nodes.Count = 0 Then
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


            End If
        Catch ex As Exception
        End Try

    End Sub



    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim n As String
        If RadioButton2.Checked = True Then
            Try
                For Each node As TreeNode In TreeView1.Nodes
                    If node.Checked = True And My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Program Files\" & node.FullPath.ToString) Then
                        For Each child As TreeNode In node.Nodes
                            If child.Checked = False Then
                                Try
                                    My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Program Files\" & child.FullPath.ToString & ".res")
                                Catch ex As Exception

                                End Try

                            End If
                        Next


                    End If

                    If node.Checked = False Then
                        nodename = node.ToString.Replace("TreeNode: ", "")

                        Try
                            If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Program Files\" & nodename) Then

                                My.Computer.FileSystem.DeleteDirectory(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Program Files\" & nodename, FileIO.DeleteDirectoryOption.DeleteAllContents)
                            End If
                            My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\" & nodename & ".res")

                        Catch ex As Exception
                            '   MessageBox.Show(ex.ToString)
                        End Try
                    End If


                Next
            Catch ex As Exception

            End Try

        End If

        If Form1.adv > 0 Then
            TreeView1.Hide()
            RadioButton1.Hide()
            RadioButton2.Hide()
            Button1.Hide()
            Button2.Hide()
            Me.Controls.Add(Form1.advert)
            a = 1
            Form1.advert.Location = New Point(0, 0)
        Else
            TreeView1.Hide()
            RadioButton1.Hide()
            RadioButton2.Hide()
            Button1.Hide()
            Button2.Hide()
            Form1.showselect = 1
            Form1.Button1_Click(Nothing, Nothing)

        End If
        Label1.Hide()

    End Sub




    Private Sub ad_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

        If Not imgThumb Is Nothing Then
            e.Graphics.DrawImage(imgThumb, 0, 0, imgThumb.Width, imgThumb.Height)
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

        If RadioButton1.Checked = True Then
            '    TreeView1.Enabled = False
            TreeView1.CheckBoxes = False
            Button1.Enabled = True

        End If


    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            TreeView1.Enabled = True
            TreeView1.CheckBoxes = True
            For Each nod2 As TreeNode In TreeView1.Nodes
                CheckChildren(nod2)
                nod2.Checked = True

            Next
            '  TreeView1.ExpandAll()

        End If
    End Sub

    Private Sub TreeView1_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterCheck
        Dim check As Boolean = True
        Dim t As Integer = 0
        For Each node As TreeNode In TreeView1.Nodes
            If node.Checked = True Then
                t = t + 1
            End If
        Next
        If t = 0 Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
        End If
        If stop_recursion = True Then
            stop_recursion = False
            CheckChildren(e.Node)
            If Not e.Node.Parent Is Nothing Then
                For Each child As TreeNode In e.Node.Parent.Nodes
                    If child.Checked = False Then
                        check = False
                    End If
                Next
                If check = True Then
                    CheckParent(e.Node)

                Else
                    UncheckParent(e.Node)
                End If
            End If
            stop_recursion = True
        End If
    End Sub

    Public Sub CheckChildren(ByVal currNode As TreeNode)
        Dim checkStatus As Boolean = currNode.Checked
        For Each node As TreeNode In currNode.Nodes
            node.Checked = checkStatus
            CheckChildren(node)
        Next
    End Sub
    Public Sub CheckParent(ByVal currNode As TreeNode)
        Dim check As Boolean = True
        While (Not currNode.Parent Is Nothing)
            For Each child As TreeNode In currNode.Parent.Nodes
                If child.Checked = False Then
                    check = False
                End If
            Next
            If check = True Then
                currNode.Parent.Checked = True
            End If
            currNode = currNode.Parent
            CheckParent(currNode)
        End While
    End Sub


    Public Sub UncheckParent(ByVal currNode As TreeNode)
        Dim i As Integer = 0
        If Not currNode.Parent Is Nothing Then
            For Each node As TreeNode In currNode.Parent.Nodes
                If node.Checked = True Then
                    i = i + 1
                End If
            Next
            ' If currNode.Checked = False Then
            '  currNode.Parent.Checked = False
            'currNode = currNode.Parent
            ' UncheckParent(currNode)
            ' End If
            If i = 0 Then
                currNode.Parent.Checked = False
            Else
                currNode.Parent.Checked = True

            End If
        End If
    End Sub


    Private Sub TreeView1_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        Try
            If e.Node.Checked = False Then
                e.Node.Checked = True
            ElseIf e.Node.Checked = True Then
                e.Node.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub




    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect

    End Sub

    Private Sub RadioButton1_Paint(sender As Object, e As PaintEventArgs) Handles RadioButton1.Paint
        Try

            If Not Form1.rChecked = Nothing And RadioButton1.Checked = True Then

                ' e.Graphics.DrawImageUnscaled((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked)), 0, 0)
                e.Graphics.DrawImageUnscaledAndClipped((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked)), New Rectangle(0, 2, (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked).Width), (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked).Height)))

            ElseIf Not Form1.rUnchecked = Nothing And RadioButton1.Checked = False Then
                '  e.Graphics.DrawImageUnscaled((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked)), 0, 0)
                e.Graphics.DrawImageUnscaledAndClipped((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked)), New Rectangle(0, 2, (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked).Width), (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked).Height)))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadioButton2_Paint(sender As Object, e As PaintEventArgs) Handles RadioButton2.Paint
        Try

            If Not Form1.rChecked = Nothing And RadioButton2.Checked = True Then
                e.Graphics.DrawImageUnscaledAndClipped((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked)), New Rectangle(0, 2, (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked).Width), (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked).Height)))
            ElseIf Not Form1.rUnchecked = Nothing And RadioButton2.Checked = False Then
                e.Graphics.DrawImageUnscaledAndClipped((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked)), New Rectangle(0, 2, (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked).Width), (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked).Height)))
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
