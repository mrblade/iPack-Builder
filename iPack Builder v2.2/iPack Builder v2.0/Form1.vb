Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Security.Cryptography


Public Class Form1

    Public folder As String
    Public bottompart As New bottom
    Public themetab1 As New themetab
    Dim file0, file1, f, ff As String
    Dim pos As Integer
    Dim winfolder As String
    Public Shared maintab As New main_tab
    Dim dirname, dirname2 As String
    Public Shared foldertab As New folder_tab
    Public interfacetab As New Interface_tab
    Public installertab As New installerinfo_tab
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Public pack, aa As String
    Dim os As Integer
    Public Shared resfiles As String
    Public Shared x, y As Integer
    Public check, i As Integer
    Dim ProcessFileStream As FileStream
    Dim ResultFileStream As FileStream
    Dim myCryptoStream As CryptoStream
    Public Sub SaveToDisk(ByVal resourceName As String, ByVal fileName As String)
        ' Get a reference to the running application.
        Dim assy As [Assembly] = [Assembly].GetExecutingAssembly()

        ' Loop through each resource, looking for the image name (case-insensitive).
        For Each resource As String In assy.GetManifestResourceNames()
            If resource.ToLower().IndexOf(resourceName.ToLower) <> -1 Then
                ' Get the embedded file from the assembly as a MemoryStream.
                Using resourceStream As System.IO.Stream = assy.GetManifestResourceStream(resource)
                    If resourceStream IsNot Nothing Then
                        Using reader As New BinaryReader(resourceStream)
                            ' Read the bytes from the input stream.
                            Dim buffer() As Byte = reader.ReadBytes(CInt(resourceStream.Length))
                            Using outputStream As New FileStream(fileName, FileMode.Create)
                                Using writer As New BinaryWriter(outputStream)
                                    ' Write the bytes to the output stream.
                                    writer.Write(buffer)
                                End Using
                            End Using
                        End Using
                    End If
                End Using
                Exit For
            End If
        Next resource
    End Sub

    Public Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            helpbox1.BringToFront()
            upgrade.BringToFront()

        Catch ex As Exception

        End Try
        PictureBox25.Image = My.Resources._001
        bottompart.PictureBox1.Image = My.Resources.new1
        bottompart.PictureBox2.Image = My.Resources.modi
        bottompart.PictureBox3.Image = My.Resources.upg1
        bottompart.PictureBox4.Image = My.Resources.sub1
    End Sub

    Public Sub Form1_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        PictureBox25.Image = My.Resources._002
        bottompart.PictureBox1.Image = My.Resources.new1d
        bottompart.PictureBox2.Image = My.Resources.modid
        bottompart.PictureBox3.Image = My.Resources.upg1d
        bottompart.PictureBox4.Image = My.Resources.sub1d
    End Sub

    Public Function showbottom()

        Me.Controls.Add(bottompart)
        bottompart.Location = New Point(0, Me.Height - bottompart.Height)
        TextBox1.Hide()
        PictureBox4.Hide()
        RadioButton1.Hide()
        RadioButton2.Hide()
        RadioButton3.Hide()
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox20.Hide()
        showbottom()
        i = 0
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        x = Me.Location.X
        y = Me.Location.Y
        Try
            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects")
        Catch ex As Exception

        End Try
        winfolder = Path.GetDirectoryName(Environment.SystemDirectory)
        TextBox1.Font = CustomFont.GetInstance(11, FontStyle.Regular)
        RadioButton1.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        RadioButton2.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        RadioButton3.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        RichTextBox1.Font = CustomFont.GetInstance(9, FontStyle.Regular)

        resfiles = main_tab.hasfiles
        Panel2.Visible = False
        os = "1"

        FileSystemWatcher1.Path = My.Computer.FileSystem.SpecialDirectories.Temp
        FileSystemWatcher1.Filter = "build_output.txt"
    End Sub

    Public Sub Form1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

    Public Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Public Sub Form1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        drag = False
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If bottompart.PictureBox6.Visible = True Then
            MessageBox.Show("Please wait while iPack is unpacking.", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If TextBox1.Visible = True Or bottompart.Visible = True Then
            For FadeOut = 90 To 10 Step -10
                Me.Opacity = FadeOut / 100
                'Me.Refresh()
                Threading.Thread.Sleep(50)
            Next
            Try
                My.Computer.FileSystem.DeleteDirectory(bottompart.path, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception

            End Try
            Application.Exit()
        ElseIf RichTextBox1.Text.Contains("Done") Then
            For FadeOut = 90 To 10 Step -10
                Me.Opacity = FadeOut / 100
                ' Me.Refresh()
                Threading.Thread.Sleep(50)
            Next
            Application.Exit()
        ElseIf bottompart.PictureBox6.Visible = True Then
            MessageBox.Show("Please wait while iPack is unpacking.", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        Else

            Dim result = MessageBox.Show("Cancelling will delete created project folder. Want to quit?", "iPack Builder", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.No Then
            ElseIf result = DialogResult.Yes Then
                Try
                    If My.Computer.FileSystem.DirectoryExists(folder) Then
                        System.IO.Directory.Delete(folder, True)
                    Else

                    End If

                Catch ex As Exception

                End Try
                For FadeOut = 90 To 10 Step -10
                    Me.Opacity = FadeOut / 100
                    ' Me.Refresh()
                    Threading.Thread.Sleep(50)
                Next
                Application.Exit()
            End If
        End If


    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.Image = My.Resources.c2

    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Image = My.Resources.c1

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub PictureBox2_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox2.MouseEnter
        PictureBox2.Image = My.Resources.m2

    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.Image = My.Resources.m1

    End Sub



    Private Sub PictureBox3_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox3.MouseEnter
        PictureBox3.Image = My.Resources.h2
    End Sub

    Private Sub PictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox3.MouseLeave
        PictureBox3.Image = My.Resources.h1
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        pack = TextBox1.Text


        If TextBox1.Text = "" Then
            MessageBox.Show("Please enter a pack name", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        ElseIf TextBox1.Text.Contains("/") Or TextBox1.Text.Contains("\") Or TextBox1.Text.Contains("*") Or TextBox1.Text.Contains(":") Or TextBox1.Text.Contains("?") Or TextBox1.Text.Contains(Chr(34)) Or TextBox1.Text.Contains("<") Or TextBox1.Text.Contains(">") Or TextBox1.Text.Contains("|") Then
            MessageBox.Show("Invalid charachter(s) in Pack name" & vbNewLine & "Name should not contain \ / * ? < > | : " & Chr(34), "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Clear()
        ElseIf Directory.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & "iPack Projects" & "\" & pack) And bottompart.modification = False Then
            MessageBox.Show("Project Folder already exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox1.Clear()

        ElseIf Directory.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & "iPack Projects" & "\" & pack) And bottompart.modification = True Then
            MessageBox.Show("Project Folder already exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '  TextBox1.Clear()

        ElseIf Not Directory.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & "iPack Projects" & "\" & pack) Then
            folder = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & "iPack Projects" & "\" & pack
            back_btn.Hide()
            Panel1.Show()
            Panel1.Controls.Add(maintab)
            maintab.Location = New Point(91, 0)
            TextBox1.Hide()
            RadioButton1.Hide()
            RadioButton2.Hide()
            RadioButton3.Hide()

            Panel2.Hide()
            PictureBox4.Hide()

            Directory.CreateDirectory(folder)
            SaveToDisk("iPack_Installer.exe", folder & "\iPack_Installer.exe")
            SaveToDisk("crypt.exe", folder & "\crypt.exe")
            SaveToDisk("ResHacker.exe", folder & "\ResHacker.exe")
            SaveToDisk("config.file", folder & "\config.file")
            SaveToDisk("GoRC.exe", folder & "\GoRC.exe")
            SaveToDisk("upx.exe", folder & "\upx.exe")
            SaveToDisk("7z.exe", folder & "\7z.exe")
            SaveToDisk("7zS.sfx", folder & "\7zS.sfx")
            SaveToDisk("manifest.txt", folder & "\manifest.txt")
            My.Computer.FileSystem.CreateDirectory(folder & "\Setup files-iPack")
            My.Computer.FileSystem.CreateDirectory(folder & "\Resource Files")
        End If


        pack = TextBox1.Text
        If RadioButton2.Checked = True Then
            os = "2"
        End If
        If RadioButton1.Checked = True Then
            os = "1"
        End If
        If RadioButton3.Checked = True Then
            os = "0"
        End If
    End Sub


    Private Sub PictureBox4_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox4.MouseEnter
        PictureBox4.Image = My.Resources.nxt2
    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox4.MouseLeave
        PictureBox4.Image = My.Resources.nxt1
    End Sub


    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Dim url As String = "http://mrbladedesigns.com/"
        Process.Start(url)
    End Sub

    Private Sub PictureBox9_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox9.MouseEnter
        PictureBox9.Image = My.Resources.ho
    End Sub

    Private Sub PictureBox9_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox9.MouseLeave
        PictureBox9.Image = My.Resources.ho1
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Dim url As String = "https://www.facebook.com/MrBladeDesigns"
        Process.Start(url)
    End Sub

    Private Sub PictureBox5_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox5.MouseEnter
        PictureBox5.Image = My.Resources.fb
    End Sub

    Private Sub PictureBox5_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox5.MouseLeave
        PictureBox5.Image = My.Resources.fb01
    End Sub


    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        Dim url As String = "https://plus.google.com/u/0/107756357152758897272"
        Process.Start(url)
    End Sub


    Private Sub PictureBox8_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox8.MouseEnter
        PictureBox8.Image = My.Resources.g_
    End Sub

    Private Sub PictureBox8_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox8.MouseLeave
        PictureBox8.Image = My.Resources.g1
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        Dim url As String = "https://twitter.com/iammrblade"
        Process.Start(url)
    End Sub

    Private Sub PictureBox7_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox7.MouseEnter
        PictureBox7.Image = My.Resources.twtr
    End Sub

    Private Sub PictureBox7_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox7.MouseLeave
        PictureBox7.Image = My.Resources.twtr1
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Dim url As String = "http://www.mr-blade.deviantart.com"
        Process.Start(url)
    End Sub

    Private Sub PictureBox6_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox6.MouseEnter
        PictureBox6.Image = My.Resources.da
    End Sub

    Private Sub PictureBox6_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox6.MouseLeave
        PictureBox6.Image = My.Resources.da01
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs)

        Panel1.Controls.Add(maintab)
        maintab.Location = New Point(91, 0)
    End Sub

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        Panel1.Controls.Remove(maintab)
        Panel1.Controls.Add(foldertab)
        Panel1.Controls.Remove(interfacetab)
        Panel1.Controls.Remove(installertab)
        ' Panel1.Controls.Remove(setuptab)
        Panel2.Visible = False
        Panel1.Controls.Remove(themetab1)
        PictureBox19.Image = My.Resources.t1
        ' foldertab.Location = New Point(91, 0)
        PictureBox11.Image = My.Resources.folder_btn
        PictureBox10.Image = My.Resources.res
        PictureBox13.Image = My.Resources.ii
        PictureBox12.Image = My.Resources.interface_btn
        PictureBox14.Image = My.Resources.setup
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
        Panel1.Controls.Remove(foldertab)
        Panel1.Controls.Remove(interfacetab)
        Panel1.Controls.Add(maintab)
        Panel1.Controls.Remove(installertab)
        Panel1.Controls.Remove(themetab1)
        ' Panel1.Controls.Remove(setuptab)
        Panel2.Visible = False
        PictureBox19.Image = My.Resources.t1
        ' maintab.Location = New Point(91, 0)
        PictureBox13.Image = My.Resources.ii
        PictureBox11.Image = My.Resources.folder
        PictureBox10.Image = My.Resources.res_btn
        PictureBox12.Image = My.Resources.interface_btn
        PictureBox14.Image = My.Resources.setup
    End Sub

    Private Sub PictureBox12_Click(sender As Object, e As EventArgs) Handles PictureBox12.Click
        Panel1.Controls.Remove(foldertab)
        '  Panel1.Controls.Remove(setuptab)
        Panel2.Visible = False
        Panel1.Controls.Add(interfacetab)
        Panel1.Controls.Remove(themetab1)
        Panel1.Controls.Remove(maintab)
        Panel1.Controls.Remove(installertab)
        ' interfacetab.Location = New Point(91, 0)
        PictureBox11.Image = My.Resources.folder
        PictureBox10.Image = My.Resources.res
        PictureBox12.Image = My.Resources._interface
        PictureBox13.Image = My.Resources.ii
        PictureBox14.Image = My.Resources.setup
        PictureBox19.Image = My.Resources.t1
    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click
        Panel1.Controls.Remove(foldertab)
        ' Panel1.Controls.Remove(setuptab)
        Panel2.Visible = False
        Panel1.Controls.Remove(interfacetab)
        Panel1.Controls.Remove(maintab)
        Panel1.Controls.Remove(themetab1)
        Panel1.Controls.Add(installertab)
        ' installertab.Location = New Point(91, 0)
        PictureBox11.Image = My.Resources.folder
        PictureBox19.Image = My.Resources.t1
        PictureBox10.Image = My.Resources.res
        PictureBox12.Image = My.Resources.interface_btn
        PictureBox13.Image = My.Resources.ii2
        PictureBox14.Image = My.Resources.setup
    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click
        Panel2.Visible = True
        Panel2.Show()

        Panel1.Controls.Remove(foldertab)
        Panel1.Controls.Remove(themetab1)
        Panel1.Controls.Remove(interfacetab)
        Panel1.Controls.Remove(maintab)
        Panel1.Controls.Remove(installertab)
        '  Panel1.Controls.Add(setuptab)
        '  setuptab.Location = New Point(91, 0)
        PictureBox11.Image = My.Resources.folder
        PictureBox10.Image = My.Resources.res
        PictureBox12.Image = My.Resources.interface_btn
        PictureBox13.Image = My.Resources.ii
        PictureBox14.Image = My.Resources.setup2
        PictureBox19.Image = My.Resources.t1
    End Sub

    Private Sub PictureBox16_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PictureBox16.Click


        If CheckBox1.CheckState = CheckState.Checked Then
            check = 1
        Else
            check = 0
        End If
        If installertab.TextBox0.Text = "" And installertab.TextBox1.Text = "" And installertab.TextBox2.Text = "" And installertab.TextBox4.Text = "" Then
            SaveToDisk("installerinfo.rc", folder & "\installerinfo.rc")
        Else

            'Installer info write
            Dim infowriter As New System.IO.StreamWriter(folder & "\installerinfo.rc")
            infowriter.Write(vbNewLine)
            infowriter.Write("1 VERSIONINFO" & vbNewLine & "FILEVERSION 1,0,0,0" & vbNewLine & "PRODUCTVERSION 1,0,0,0" & vbNewLine & "FILEOS 0x40004" & vbNewLine & "FILETYPE 0x1" & vbNewLine & "{" & vbNewLine)
            infowriter.Write("BLOCK " & Chr(34) & "StringFileInfo" & Chr(34))
            infowriter.Write(vbNewLine & "{" & vbNewLine)
            infowriter.Write("BLOCK " & Chr(34) & "000004B0" & Chr(34))
            infowriter.Write(vbNewLine & "{" & vbNewLine)
            infowriter.Write("VALUE " & Chr(34) & "CompanyName" & Chr(34) & ", " & Chr(34) & installertab.TextBox0.Text & Chr(34) & vbNewLine)
            infowriter.Write("VALUE " & Chr(34) & "FileDescription" & Chr(34) & ", " & Chr(34) & "iPack" & Chr(34) & vbNewLine)
            infowriter.Write("VALUE " & Chr(34) & "FileVersion" & Chr(34) & ", " & Chr(34) & installertab.TextBox1.Text & Chr(34) & vbNewLine)
            infowriter.Write("VALUE " & Chr(34) & "InternalName" & Chr(34) & ", " & Chr(34) & "iPack" & Chr(34) & vbNewLine)
            infowriter.Write("VALUE " & Chr(34) & "LegalCopyright" & Chr(34) & ", " & Chr(34) & installertab.TextBox2.Text & Chr(34) & vbNewLine)
            infowriter.Write("VALUE " & Chr(34) & "OriginalFilename" & Chr(34) & ", " & Chr(34) & pack & ".exe" & Chr(34) & vbNewLine)
            infowriter.Write("VALUE " & Chr(34) & "ProductName" & Chr(34) & ", " & Chr(34) & pack & Chr(34) & vbNewLine)
            infowriter.Write("VALUE " & Chr(34) & "ProductVersion" & Chr(34) & ", " & Chr(34) & installertab.TextBox4.Text & Chr(34) & vbNewLine)
            infowriter.Write("}" & vbNewLine)
            infowriter.Write("}" & vbNewLine)
            infowriter.Write(vbNewLine)
            infowriter.Write("BLOCK " & Chr(34) & "VarFileInfo" & Chr(34))
            infowriter.Write(vbNewLine & "{" & vbNewLine)
            infowriter.Write("VALUE " & Chr(34) & "Translation" & Chr(34) & ", " & "0x0000 0x04B0" & vbNewLine)
            infowriter.Write("}" & vbNewLine)
            infowriter.Write("}" & vbNewLine)
            infowriter.Close()
        End If

        PictureBox3.Enabled = False
        PictureBox10.Enabled = False
        PictureBox12.Enabled = False
        PictureBox11.Enabled = False
        PictureBox13.Enabled = False
        PictureBox19.Enabled = False
        PictureBox1.Enabled = False
        PictureBox17.Visible = True
        PictureBox16.Visible = False
        PictureBox18.Visible = False
        CheckBox1.Visible = False
        CheckBox2.Visible = False
        CheckBox3.Visible = False

        Dim itemCount As Integer = maintab.ListBox1.Items.Count
        Dim foldrcount As Integer = foldertab.ListBox1.Items.Count
        Try
            If Not themetab1.themepath = "" Then
                My.Computer.FileSystem.CreateDirectory(folder & "\Setup files-iPack\Theme")
                For Each f In Directory.GetFiles(themetab1.themepath)
                    Dim f2 As String
                    f2 = f.Replace(themetab1.themepath & "\", "")
                    My.Computer.FileSystem.CopyFile(f, folder & "\Setup files-iPack\Theme\" & f2)
                Next

            End If
        Catch ex As Exception

        End Try
        If installertab.TextBox6.Text = "" Then

            SaveToDisk("Liscense", folder & "\Setup files-iPack\License.txt")

        Else

            My.Computer.FileSystem.WriteAllText(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Setup files-iPack\License.txt", installertab.TextBox6.Text, False)
        End If
        If installertab.TextBox5.Text = "" Then
        Else

        End If


        If interfacetab.TextBox1.Text = "" Then
            SaveToDisk("header.png", folder & "\Setup files-iPack\header.png")
        Else
            My.Computer.FileSystem.CopyFile(interfacetab.TextBox1.Text, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Setup files-iPack\header.png", overwrite:=True)
        End If
        If interfacetab.TextBox2.Text = "" Then
            SaveToDisk("Icon.ico", folder & "\Icon.ico")
        Else
            My.Computer.FileSystem.CopyFile(interfacetab.TextBox2.Text, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Icon.ico", overwrite:=True)
        End If
        If interfacetab.TextBox3.Text = "" Then
            SaveToDisk("logo.png", folder & "\Setup files-iPack\logo.png")
        Else
            My.Computer.FileSystem.CopyFile(interfacetab.TextBox3.Text, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Setup files-iPack\logo.png", overwrite:=True)
        End If
        If interfacetab.TextBox4.Text = "" Then
        Else
            My.Computer.FileSystem.CopyFile(interfacetab.TextBox4.Text, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Setup files-iPack\splash.png", overwrite:=True)
        End If

        If itemCount = 0 Then
            If foldrcount = 0 Then
                RichTextBox1.Text = "Building process terminated." & vbNewLine & vbNewLine & "No files or folders have been dropped." & vbNewLine & vbNewLine & "Please resolve these and build again."
                RichTextBox1.ForeColor = Color.Red
                PictureBox10.Enabled = True
                PictureBox3.Enabled = True
                PictureBox12.Enabled = True
                PictureBox11.Enabled = True
                PictureBox13.Enabled = True
                PictureBox17.Visible = False
                PictureBox16.Visible = True
                PictureBox1.Enabled = True
                PictureBox18.Visible = True
                CheckBox1.Visible = True
                CheckBox2.Visible = True
                CheckBox3.Visible = True
                PictureBox19.Enabled = True
                Try
                    If themetab1.themepath = "" Then
                        My.Computer.FileSystem.DeleteDirectory(folder & "\Setup files-iPack\Theme", FileIO.DeleteDirectoryOption.DeleteAllContents)
                    End If
                Catch ex As Exception

                End Try
            End If

        End If
        If itemCount > 0 Or foldrcount > 0 Then
            RichTextBox1.Clear()
            Try
                Dim Filenum As Integer = FreeFile()
                FileOpen(Filenum, My.Computer.FileSystem.SpecialDirectories.Temp & "\build_output.txt", OpenMode.Output)
                FileClose()
                FileSystemWatcher1.Path = My.Computer.FileSystem.SpecialDirectories.Temp
                FileSystemWatcher1.Filter = "build_output.txt"
                PictureBox15.Visible = True
                'write iPack configuration.config
                Dim configWriter As New System.IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Setup files-iPack\Configuration.config")
                configWriter.Write(vbNewLine)
                configWriter.Write("---------// iPack Configuration (Do not modify) \\---------" & vbNewLine)
                configWriter.Write(vbNewLine)
                configWriter.Write("Pack Name=")
                '  configWriter.Write(vbNewLine)
                configWriter.Write(pack)
                configWriter.Write(vbNewLine)
                configWriter.Write("Author=")
                If installertab.TextBox0.Text = "" Then
                    configWriter.Write("Mr Blade")
                Else
                    configWriter.Write(installertab.TextBox0.Text)
                End If
                configWriter.Write(vbNewLine)
                configWriter.Write("Heading=")
                If installertab.TextBox5.Text = "" Then
                    configWriter.Write("iPack")
                Else
                    configWriter.Write(installertab.TextBox5.Text)
                End If
                configWriter.Write(vbNewLine)
                configWriter.Write("OS=")
                configWriter.Write(os)
                configWriter.Write(vbNewLine)
                configWriter.Write("Lalign=")

                If installertab.ComboBox1.SelectedIndex = 0 Then
                    configWriter.Write("1")
                ElseIf installertab.ComboBox1.SelectedIndex = 1 Then
                    configWriter.Write("2")
                Else
                    configWriter.Write("3")
                End If

                configWriter.Write(vbNewLine)
                configWriter.Write("Language=")
                configWriter.Write(installerinfo_tab.lang)
                configWriter.Write(vbNewLine)
                configWriter.Write("Ad=")
                If CheckBox2.Checked = True Then
                    configWriter.Write("1")
                Else
                    configWriter.Write("0")
                End If
                configWriter.Write(vbNewLine)
                configWriter.Write("Silent=")
                If CheckBox3.Checked = True Then
                    configWriter.Write("1")
                Else
                    configWriter.Write("0")
                End If
                configWriter.Write(vbNewLine & vbNewLine)
                configWriter.Write("-------------// iPack Configuration END  \\-------------" & vbNewLine)
                configWriter.Close()


                'backgroudnworker to copt .res files
                BackgroundWorker1.RunWorkerAsync()

            Catch ex As Exception

            End Try


            RichTextBox1.Text = "° Build Started. . ." & vbNewLine & vbNewLine
            RichTextBox1.ForeColor = Color.DeepSkyBlue
            If interfacetab.TextBox1.Text = "" Or interfacetab.TextBox2.Text = "" Or interfacetab.TextBox3.Text = "" Then

            End If
            If installertab.TextBox1.Text = "" Or installertab.TextBox2.Text = "" Or installertab.TextBox4.Text = "" Or installertab.TextBox5.Text = "" Or installertab.TextBox6.Text = "" Then

            End If
            RichTextBox1.Text += "° Encrypting .res files" & vbNewLine

        End If




    End Sub

    Private Sub PictureBox16_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox16.MouseEnter
        PictureBox16.Image = My.Resources.build1
    End Sub

    Private Sub PictureBox16_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox16.MouseLeave
        PictureBox16.Image = My.Resources.build
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub



    Private Sub PictureBox15_Click(sender As Object, e As EventArgs) Handles PictureBox15.Click

    End Sub






    Private Sub FileSystemWatcher1_Changed(sender As Object, e As FileSystemEventArgs) Handles FileSystemWatcher1.Changed
        Try

            System.Threading.Thread.Sleep(100)
            RichTextBox1.Text = (My.Computer.FileSystem.ReadAllText(My.Computer.FileSystem.SpecialDirectories.Temp & "\build_output.txt"))

        Catch ex As Exception

        End Try
        If RichTextBox1.Text.Contains("Done") Then
            PictureBox15.Hide()
            PictureBox16.Visible = False
            PictureBox1.Enabled = True
            PictureBox17.Visible = False
            '  PictureBox20.Location = New Point(PictureBox17.Location.X, PictureBox17.Location.Y)
            PictureBox20.Visible = True
            PictureBox20.Enabled = True
            PictureBox22.Visible = True
            PictureBox23.Visible = True
            PictureBox22.Enabled = True

            Me.WindowState = FormWindowState.Normal
            Try


                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.Temp & "\build_output.txt")

                ' System.IO.Directory.Delete(folder & "\Resource Files", True)
                System.IO.Directory.Delete(folder & "\Setup files-iPack", True)
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        RichTextBox1.SelectionStart = RichTextBox1.Text.Length
        RichTextBox1.ScrollToCaret()
    End Sub

    Private Sub PictureBox17_Click(sender As Object, e As EventArgs) Handles PictureBox17.Click

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)








    End Sub

    Private Sub PictureBox19_Click(sender As Object, e As EventArgs)
        ' submit.Show()
        '  Me.Enabled = False
        Dim site As New ProcessStartInfo
        site.FileName = ("http://mrbladedesigns.com/ipack-submission/")
        Process.Start(site)





    End Sub





    Private Sub PictureBox20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox20.Click
        Dim site As New ProcessStartInfo
        site.FileName = ("http://mrbladedesigns.com/ipack-submission/")
        Process.Start(site)

    End Sub

    Private Sub PictureBox20_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox20.MouseEnter
        PictureBox20.Image = My.Resources.sub_b2
    End Sub

    Private Sub PictureBox20_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox20.MouseLeave
        PictureBox20.Image = My.Resources.sub_b1
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        If TextBox1.Visible = True Or bottompart.Visible = True Then
            helpbox1.Show()

        ElseIf Panel1.Controls.Contains(maintab) Then
            helpbox1.Show()
            helpbox1.BackgroundImage = My.Resources.reshelp
            helpbox1.PictureBox1.Visible = False
            helpbox1.LinkLabel1.Visible = False

        ElseIf Panel1.Controls.Contains(foldertab) Then
            helpbox1.Show()
            helpbox1.BackgroundImage = My.Resources.folderhelp
            helpbox1.PictureBox1.Visible = False
            helpbox1.LinkLabel1.Visible = False

        ElseIf Panel1.Controls.Contains(interfacetab) Then
            helpbox1.Show()
            helpbox1.BackgroundImage = My.Resources.interfacehelp
            helpbox1.PictureBox1.Visible = False
            helpbox1.LinkLabel1.Visible = False

        ElseIf Panel1.Controls.Contains(installertab) Then
            helpbox1.Show()
            helpbox1.BackgroundImage = My.Resources.iinfohelp
            helpbox1.PictureBox1.Visible = False
            helpbox1.LinkLabel1.Visible = False
        ElseIf Panel1.Controls.Contains(themetab1) Then
            helpbox1.Show()
            helpbox1.BackgroundImage = My.Resources.themehelp
            helpbox1.PictureBox1.Visible = False
            helpbox1.LinkLabel1.Visible = False

        Else
            helpbox1.Show()
            helpbox1.BackgroundImage = My.Resources.setuphelp
            helpbox1.PictureBox1.Visible = False
            helpbox1.LinkLabel1.Visible = False
        End If


    End Sub

    Private Sub PictureBox21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox21.Click
        Dim donate As New ProcessStartInfo
        donate.FileName = ("http://mrbladedesigns.com/downloads/mrblade-designs-donation/")
        Process.Start(donate)

    End Sub

    Private Sub PictureBox21_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox21.MouseEnter
        PictureBox21.Image = My.Resources.don2
    End Sub

    Private Sub PictureBox21_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox21.MouseLeave
        PictureBox21.Image = My.Resources.don1
    End Sub

    Private Sub PictureBox22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox22.Click
        PictureBox20.Hide()
        PictureBox20.Visible = False
        Panel1.Hide()
        TextBox1.Text = ""
        Me.Enabled = True
        RadioButton1.Visible = True
        CheckBox1.Visible = True
        CheckBox2.Visible = True
        CheckBox3.Visible = True

        RadioButton3.Visible = True
        RadioButton2.Visible = True
        TextBox1.Enabled = True
        PictureBox4.Visible = True
        PictureBox4.Enabled = True
        PictureBox3.Enabled = True
        TextBox1.Enabled = True
        TextBox1.Visible = True
        PictureBox10.Enabled = True
        PictureBox11.Enabled = True
        PictureBox19.Enabled = True
        PictureBox12.Enabled = True
        PictureBox13.Enabled = True
        PictureBox14.Enabled = True
        maintab.ListBox2.Items.Clear()
        maintab.ListBox1.Items.Clear()
        foldertab.ListBox1.Items.Clear()
        interfacetab.TextBox1.Text = ""
        interfacetab.TextBox2.Text = ""
        interfacetab.TextBox3.Text = ""
        interfacetab.TextBox4.Text = ""
        installertab.TextBox0.Text = ""
        installertab.TextBox1.Text = ""
        installertab.TextBox2.Text = ""
        installertab.TextBox4.Text = ""
        installertab.TextBox5.Text = ""
        installertab.TextBox6.Text = ""
        RichTextBox1.Text = ""
        ' PictureBox20.Visible = False
        PictureBox22.Visible = False
        PictureBox23.Visible = False
        PictureBox17.Visible = False
        PictureBox16.Visible = True
        PictureBox16.Visible = True

        PictureBox18.Visible = True
        PictureBox14.Image = My.Resources.setup
        PictureBox10.Image = My.Resources.res_btn
        Interface_tab.imgThumb = Nothing
        Interface_tab.icoThumb = Nothing
        Interface_tab.splashThumb = Nothing
        Interface_tab.logoThumb = Nothing
        interfacetab.Refresh()
        showbottom()
        bottompart.Visible = True
        bottompart.PictureBox1.Show()
        bottompart.PictureBox2.Show()
        bottompart.PictureBox3.Show()
        bottompart.PictureBox4.Show()
        bottompart.PictureBox5.Hide()
        bottompart.PictureBox6.Hide()
        bottompart.PictureBox7.Hide()
        themetab1.FolderBrowserDialog1.SelectedPath = ""
        themetab1.PictureBox3_Click(Nothing, Nothing)
    End Sub

    Private Sub PictureBox22_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox22.MouseEnter
        PictureBox22.Image = My.Resources.another1
    End Sub

    Private Sub PictureBox22_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox22.MouseLeave
        PictureBox22.Image = My.Resources.another
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            MessageBox.Show("A console window will be shown, do NOT close it manually.", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork_1(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            For Each item As String In maintab.ListBox2.Items
                If IO.File.Exists(item) Then
                    My.Computer.FileSystem.CopyFile(item, folder & "\Resource Files\" & IO.Path.GetFileName(item), overwrite:=True)
                End If
            Next
            For Each fitem As String In foldertab.ListBox1.Items
                If IO.Directory.Exists(fitem) Then
                    My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Resource Files\Program Files")
                    For Each f In Directory.GetFiles(fitem, "*.res")
                        If File.Exists(f) Then
                            My.Computer.FileSystem.CopyFile(f, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Resource Files\Program Files\" & IO.Path.GetFileName(fitem) & "\" & IO.Path.GetFileName(f), overwrite:=True)

                        End If
                    Next
                End If
            Next

            bottompart.modification = False


            Dim objWriter As New System.IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\make.bat")
            objWriter.Write(vbNewLine)
            objWriter.Write("@echo off")
            objWriter.Write(vbNewLine)
            objWriter.Write("cd " & Chr(34) & My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("TITLE Building iPack: " & pack)
            objWriter.Write(vbNewLine)
            objWriter.Write("@echo -----------------------------------")
            objWriter.Write(vbNewLine)
            objWriter.Write("@echo Do not close this window manually.")
            objWriter.Write(vbNewLine)
            objWriter.Write("@echo -----------------------------------")
            objWriter.Write(vbNewLine)
            objWriter.Write("ren " & Chr(34) & My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\config.file" & Chr(34) & " iPack_Installer.exe.config")
            'objWriter.Write("cd " & Chr(34) & My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("@echo ° Build started >> %temp%\build_output.txt")
            objWriter.Write(vbNewLine & "@echo. >> %temp%\build_output.txt" & vbNewLine)
            objWriter.Write("@echo ° Encrypting .res files >> %temp%\build_output.txt")

            objWriter.Write(vbNewLine & "@echo.  >> %temp%\build_output.txt" & vbNewLine)
            objWriter.Write("@echo off")
            objWriter.Write(vbNewLine)
            objWriter.Write("@echo ° Compressing iPack into 7-zip format. >> %temp%\build_output.txt")
            objWriter.Write(vbNewLine & "@echo.  >> %temp%\build_output.txt" & vbNewLine)
            objWriter.Write("cd " & Chr(34) & My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("ATTRIB +H " & Chr(34) & folder & "\Setup files-iPack" & Chr(34) & " /S /D")
            objWriter.Write(vbNewLine)
            objWriter.Write("ATTRIB +H " & Chr(34) & My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & "iPack Projects" & "\" & "iPack_Installer.exe.config" & Chr(34) & " /S /D")
            objWriter.Write(vbNewLine)
            ' objWriter.Write("ATTRIB +H +S " & Chr(34) & folder & "\iPack_Installer.exe.config" & Chr(34))
            '  objWriter.Write(vbNewLine)
            objWriter.Write(Chr(34) & folder & "\GoRC.exe" & Chr(34) & " " & Chr(34) & "installerinfo.rc" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "installerinfo.obj" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write(Chr(34) & folder & "\ResHacker.exe" & Chr(34) & " -delete " & "iPack_Installer.exe" & ", iPack_Installer.exe, " & "versioninfo, 1,")
            objWriter.Write(vbNewLine)
            objWriter.Write(Chr(34) & folder & "\ResHacker.exe" & Chr(34) & " -addoverwrite " & "iPack_Installer.exe" & ", iPack_Installer.exe, " & "installerinfo.res ,,,")
            objWriter.Write(vbNewLine)
            objWriter.Write(Chr(34) & folder & "\ResHacker.exe" & Chr(34) & " -addoverwrite " & "iPack_Installer.exe" & ", iPack_Installer.exe" & ", Icon.ico, ICONGROUP, 32512,")
            objWriter.Write(vbNewLine)
            '            objWriter.Write("7z a -mx=9 " & Chr(34) & "Resource.7z" & Chr(34) & " " & Chr(34) & "Resource Files" & Chr(34))
            objWriter.Write(vbNewLine)
            'objWriter.Write("crypt.exe -encrypt -key 5567D8DC6290EC7E78A13C4D6E6E6DF730D2B2048FC02120721BEC09EFD7A4DB -infile  " & Chr(34) & "Resource.7z" & Chr(34) & " -outfile " & Chr(34) & "Resource.iPack" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("7z a -mx=9 " & Chr(34) & pack & ".7z" & Chr(34) & " " & Chr(34) & "Resource.iPack" & Chr(34) & " " & Chr(34) & "Setup files-iPack" & Chr(34) & " " & Chr(34) & "iPack_Installer.exe" & Chr(34) & " " & Chr(34) & "iPack_Installer.exe.config" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("@echo ° Merging files and building exe. >> %temp%\build_output.txt")
            objWriter.Write(vbNewLine)
            objWriter.Write("copy /b 7zS.sfx + Config.txt + " & Chr(34) & pack & ".7z" & Chr(34) & " " & Chr(34) & pack & ".exe" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & pack & ".7z" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "Config.txt" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "7zS.sfx" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "iPack_Installer.exe" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "iPack_Installer.exe.config" & Chr(34) & " /A:H")
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "7z.exe" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "Resource.iPack" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "crypt.exe" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "Resource.7z" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("RD /s /q " & Chr(34) & "Resource Files" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("RD /s /q " & Chr(34) & "Setup-files iPack" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write(vbNewLine & "@echo.  >> %temp%\build_output.txt" & vbNewLine)
            objWriter.Write("@echo ° Build successful, trying to reduce size using upx. >> %temp%\build_output.txt")
            objWriter.Write(vbNewLine)
            objWriter.Write(Chr(34) & folder & "\ResHacker.exe" & Chr(34) & " -addoverwrite " & Chr(34) & pack & ".exe" & Chr(34) & ", " & Chr(34) & pack & ".exe" & Chr(34) & ", " & Chr(34) & "installerinfo.res" & Chr(34) & " ,,,")
            objWriter.Write(vbNewLine)
            objWriter.Write(Chr(34) & folder & "\ResHacker.exe" & Chr(34) & " -addoverwrite " & Chr(34) & pack & ".exe" & Chr(34) & ", " & Chr(34) & pack & ".exe" & Chr(34) & ", Icon.ico, ICONGROUP, 101,")
            objWriter.Write(vbNewLine)
            ' objWriter.Write(Chr(34) & folder & "\ResHacker.exe" & Chr(34) & " -addoverwrite " & Chr(34) & pack & ".exe" & Chr(34) & ", " & Chr(34) & pack & ".exe" & Chr(34) & ", manifest.txt, 24, 1,")
            ' objWriter.Write(vbNewLine)
            objWriter.Write(Chr(34) & folder & "\upx.exe" & Chr(34) & " " & Chr(34) & pack & ".exe" & Chr(34))
            objWriter.Write(vbNewLine & "@echo.  >> %temp%\build_output.txt" & vbNewLine)
            objWriter.Write("del " & Chr(34) & "upx.exe" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "ResHacker.exe" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "manifest.txt" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "GoRC.exe" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "ResHacker.log" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "ResHacker.ini" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "Icon.ico" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "installerinfo.res" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("del " & Chr(34) & "installerinfo.rc" & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("@echo ° Done. >> %temp%\build_output.txt")
            objWriter.Write(vbNewLine)
            '  objWriter.Write("cd " & Chr(34) & My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & Chr(34))
            objWriter.Write(vbNewLine)
            objWriter.Write("start.")
            objWriter.Write(vbNewLine)
            objWriter.Write("DEL " & Chr(34) & "%~f0" & Chr(34))
            objWriter.Close()
            Dim objWriter2 As New System.IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Config.txt")
            objWriter2.Write(";!@Install@!UTF-8!" & vbNewLine & "Title=" & Chr(34) & "Unpacking" & Chr(34) & vbNewLine & "GUIMode=" & Chr(34) & "1" & Chr(34) & vbNewLine & "GUIFlags=" & Chr(34) & "1+8" & Chr(34) & vbNewLine & "InstallPath=" & Chr(34) & "%PROGRAMFILES%\\" & pack & Chr(34) & vbNewLine & "OverwriteMode=" & Chr(34) & "2" & Chr(34) & vbNewLine & "RunProgram=" & Chr(34) & "iPack_Installer.exe" & Chr(34) & vbNewLine & "ExtractTitle=" & Chr(34) & "Unpacking" & Chr(34) & vbNewLine & "ExtractDialogText=" & Chr(34) & Chr(34) & vbNewLine & "ExtractCancelText=" & Chr(34) & "Cancel" & Chr(34))
            objWriter2.Write(vbNewLine & ";!@InstallEnd@!")
            objWriter2.Close()
            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Resource Files")
            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Setup files-iPack")

            Dim takeown As New ProcessStartInfo
            takeown.FileName = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\7z.exe"
            takeown.Arguments = "a -mx=9 " & Chr(34) & "Resource.7z" & Chr(34) & " " & Chr(34) & "Resource Files" & Chr(34)
            takeown.WindowStyle = ProcessWindowStyle.Hidden
            takeown.WorkingDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack
            Dim pp1 As New Process()
            pp1.StartInfo = takeown
            pp1.Start()
            pp1.WaitForExit()


            ' My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Resource.iPack")
            Dim myDESProvider As DESCryptoServiceProvider = New DESCryptoServiceProvider()
            myDESProvider.Key = ASCIIEncoding.ASCII.GetBytes("B3DM60P7")
            myDESProvider.IV = ASCIIEncoding.ASCII.GetBytes("B3DM60P7")
            '  Dim myICryptoTransform As ICryptoTransform = myDESProvider.CreateEncryptor(myDESProvider.Key, myDESProvider.IV)
            Dim Decr As ICryptoTransform = myDESProvider.CreateEncryptor(myDESProvider.Key, myDESProvider.IV)
            ProcessFileStream = New FileStream(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Resource.7z", FileMode.Open, FileAccess.Read)
            ResultFileStream = New FileStream(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\Resource.iPack", FileMode.Create, FileAccess.Write)
            myCryptoStream = New CryptoStream(ResultFileStream, Decr, CryptoStreamMode.Write)
            Dim bytearrayinput(ProcessFileStream.Length - 1) As Byte
            ProcessFileStream.Read(bytearrayinput, 0, bytearrayinput.Length)
            myCryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length)
            myCryptoStream.Close()
            ProcessFileStream.Close()
            ResultFileStream.Close()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Dim launch As New ProcessStartInfo(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\iPack Projects\" & pack & "\make.bat")
        launch.WorkingDirectory = folder

        If check = 1 Then
            launch.WindowStyle = ProcessWindowStyle.Normal
        Else
            launch.WindowStyle = ProcessWindowStyle.Hidden
        End If
        Process.Start(launch)
        Try
            My.Computer.FileSystem.DeleteDirectory(bottompart.path, FileIO.DeleteDirectoryOption.DeleteAllContents)
            bottompart.adv = Nothing
            bottompart.path = Nothing
            bottompart.modification = False
            bottompart.logo = Nothing
            bottompart.header = Nothing
            bottompart.lisc = Nothing
            bottompart.silent = Nothing
            bottompart.mainheading = Nothing
            bottompart.author = Nothing
            bottompart.ico = Nothing
            bottompart.language = Nothing
            bottompart.os = Nothing
            bottompart.pack = Nothing
            bottompart.lalign = Nothing
            bottompart.fv = Nothing
            bottompart.pv = Nothing
            bottompart.cpr = Nothing
            bottompart.TextBox1.Text = ""

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)


    End Sub

    Private Sub PictureBox24_Click(sender As Object, e As EventArgs)
        upgrade.Show()
    End Sub

    Private Sub PictureBox24_MouseEnter(sender As Object, e As EventArgs)


    End Sub

    Private Sub PictureBox24_MouseLeave(sender As Object, e As EventArgs)


    End Sub


    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            If i = 0 Then
                MessageBox.Show("Please do not add files like shell32.dll or explorer.exe etc which have digital signature on Win 8.1 in " & Chr(34) & "All OS" & Chr(34) & " as it will break it and windows may fail to boot.", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            i = i + 1
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged

    End Sub

    Private Sub PictureBox19_Click_1(sender As Object, e As EventArgs) Handles PictureBox19.Click
        PictureBox19.Image = My.Resources.t3
        Panel1.Controls.Remove(foldertab)
        ' Panel1.Controls.Remove(setuptab)
        Panel2.Visible = False
        Panel1.Controls.Remove(interfacetab)
        Panel1.Controls.Remove(maintab)
        Panel1.Controls.Remove(installertab)
        Panel1.Controls.Add(themetab1)
        ' themetab1.Location = New Point(91, 0)
        PictureBox11.Image = My.Resources.folder
        PictureBox10.Image = My.Resources.res
        PictureBox12.Image = My.Resources.interface_btn
        PictureBox13.Image = My.Resources.ii
        PictureBox14.Image = My.Resources.setup
    End Sub

    Private Sub PictureBox19_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox19.MouseEnter

    End Sub

    Private Sub PictureBox19_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox19.MouseLeave
        '   PictureBox19.Image = My.Resources.t1
    End Sub
    
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        DoubleBuffered = True

    End Sub

    Private Sub back_btn_Click(sender As Object, e As EventArgs) Handles back_btn.Click
        back_btn.Hide()
        bottompart.Show()
        TextBox1.Hide()
        PictureBox4.Hide()
        RadioButton1.Hide()
        RadioButton2.Hide()
        RadioButton3.Hide()
        TextBox1.Text = ""


    End Sub

    Private Sub back_btn_MouseHover(sender As Object, e As EventArgs) Handles back_btn.MouseHover
        back_btn.Image = My.Resources.gback1
    End Sub

    Private Sub back_btn_MouseLeave(sender As Object, e As EventArgs) Handles back_btn.MouseLeave
        back_btn.Image = My.Resources.gback
    End Sub
End Class
