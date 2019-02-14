Imports System.IO
Imports System.Threading
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Text

Public Class Form1


    Public langthemed As New language_select
    Dim logwriter As System.IO.StreamWriter
    Private Declare Function Wow64RevertWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Private Declare Function Wow64DisableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Private lFsRedirectOldValue As Long = Nothing
    Public finishbtn1 As New btnfinish
    Public Shared appname As String
    Dim rng As Random = New Random
    Dim totalpatched As Integer
    Public Shared Uninstaller As New Uninstall
    Public Shared advert As New filesx
    Public Shared showfiles As New ad1
    Dim currentval, currentval2 As String
    Public Shared w, h, x, y, a As Integer
    Dim dirname As String
    Dim major As Integer = Environment.OSVersion.Version.Major
    Dim minor As Integer = Environment.OSVersion.Version.Minor
    Public Shared imgThumb As Image = Nothing
    Public Shared imgThumb2 As Image = Nothing
    Public Shared mainheading As String
    Dim windowsver As String
    Dim suportedwindows, rp As String
    Dim barmaxval As Integer
    Dim file0, file1, f, ff, ff2, ff3, path0() As String
    Dim pos As Integer
    Dim winfolder As String = Environment.GetEnvironmentVariable("windir")
    Public Shared author, pack, os, adv As String
    Dim totalfiles, totalfiles0, total_in_program_files As Integer
    Dim elapsed As TimeSpan
    Dim start_time, stop_time As DateTime
    Dim finaltime As Decimal
    Dim pfiles As String
    Public winbit, enable As String
    Dim rebootneed, rebootneed2 As String
    Dim SystemDir As String = Environment.GetEnvironmentVariable("SystemDrive")
    Dim winDir As String = Environment.GetEnvironmentVariable("windir")
    Dim files() As String
    Dim files2() As String
    Dim files3() As String
    Dim willpatch, willpatch2 As String
    Dim lalign As Integer
    Dim status() As Char
    Dim pval As Integer
    Public showselect As Integer
    Public button As New btn

    Public cbChecked, cbUnchecked, rChecked, rUnchecked, drop1, drop2, droptxtcolor As String

    Friend Enum MoveFileFlags
        MOVEFILE_REPLACE_EXISTING = 1
        MOVEFILE_COPY_ALLOWED = 2
        MOVEFILE_DELAY_UNTIL_REBOOT = &H4
        MOVEFILE_WRITE_THROUGH = 8
    End Enum
    <System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint:="MoveFileEx")> _
    Friend Shared Function MoveFileEx(lpExistingFileName As String, lpNewFileName As String, dwFlags As MoveFileFlags) As Boolean
    End Function
    Public Function GetSettingItem(ByVal File As String, ByVal Identifier As String) As String
        Dim S As New IO.StreamReader(File) : Dim Result As String = ""
        Do While (S.Peek <> -1)
            Dim Line As String = S.ReadLine
            If Line.ToLower.StartsWith(Identifier.ToLower & "=") Then
                Result = Line.Substring(Identifier.Length + 1)
            End If
        Loop
        Return Result
    End Function
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

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim i As Integer = 0
        Dim r As String


        If RichTextBox1.Visible = True Or Uninstaller.RichTextBox1.Visible = True Then
            If ComboBox1.SelectedIndex = 0 Then
                MessageBox.Show("Please wait for the process to complete.", mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            If ComboBox1.SelectedIndex = 1 Then
                MessageBox.Show("Por favor, espere a que el proceso se complete.", mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            If ComboBox1.SelectedIndex = 2 Then
                MessageBox.Show("Пожалуйста, подождите завершения процесса.", mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            If ComboBox1.SelectedIndex = 3 Then
                MessageBox.Show("Warten Sie, bis der Prozess abgeschlossen ist.", mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            e.Cancel = True

        Else

            Try
                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Patcher.exe")
                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Patcher.log")
                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Patcher.ini")

            Catch ex As Exception
            End Try
            Application.Exit()
        End If

        Try

        Catch ex As Exception

        End Try
        Return
    End Sub

    Public Function theme()
        Dim root As XElement = XDocument.Load(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\Theme.xml").Root
        Dim val As String

        Try
            val = (root.Element("main").Element("textcolor").Value)
            If Not val = Nothing Then
                Me.ForeColor = ColorTranslator.FromHtml(val)
                ComboBox1.ForeColor = ColorTranslator.FromHtml(val)
                Uninstaller.ComboBox1.ForeColor = ColorTranslator.FromHtml(val)

            End If
        Catch ex As Exception

        End Try
        Try
            val = (root.Element("main").Element("backcolor").Value)
            If Not val = Nothing Then
                Me.BackColor = ColorTranslator.FromHtml(val)
                ComboBox1.BackColor = ColorTranslator.FromHtml(val)
                Uninstaller.ComboBox1.BackColor = ColorTranslator.FromHtml(val)
                ProgressBar1.BackColor = ColorTranslator.FromHtml(val)
                ProgressBar2.BackColor = ColorTranslator.FromHtml(val)
            End If
        Catch ex As Exception

        End Try
        Try
            val = (root.Element("main").Element("backimage").Value)
            If Not val = Nothing Then
                Me.BackgroundImage = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val)
            End If
        Catch ex As Exception

        End Try
        Try
            val = (root.Element("main").Element("linedivider").Value)
            If Not val = Nothing Then
                If val = 0 Then
                    line1.Visible = False
                Else
                    line1.Visible = True
                End If
            End If

        Catch ex As Exception

        End Try

        Try
            val = (root.Element("main").Element("divider-img").Value)
            If Not val = Nothing And My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val) Then
                PictureBox2.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val)
                PictureBox2.Location = New Point(line1.Location.X, line1.Location.Y - 1)
                line1.Hide()
                PictureBox2.Width = Me.Width

                PictureBox2.Visible = True

            End If

        Catch ex As Exception

        End Try

        Try
            val = (root.Element("main").Element("license-backcolor").Value)
            If Not val = Nothing Then
                RichTextBox2.BorderStyle = BorderStyle.None
                RichTextBox2.BackColor = ColorTranslator.FromHtml(val)
                RichTextBox2.Height = imgThumb.Height
            End If
        Catch ex As Exception

        End Try

        Try
            val = (root.Element("main").Element("license-textcolor").Value)
            If Not val = Nothing Then
                RichTextBox2.ForeColor = ColorTranslator.FromHtml(val)
            End If
        Catch ex As Exception

        End Try
        '  Catch ex As Exception

        '   End Try

        Try
            val = (root.Element("patching-window").Element("window-backcolor").Value)
            If Not val = Nothing Then
                RichTextBox1.BackColor = ColorTranslator.FromHtml(val)
                Panel1.BackColor = RichTextBox1.BackColor
                Panel2.BackColor = RichTextBox1.BackColor



            End If
        Catch ex As Exception

        End Try

        Try
            val = (root.Element("patching-window").Element("window-textcolor").Value)
            If Not val = Nothing Then
                RichTextBox1.ForeColor = ColorTranslator.FromHtml(val)
            End If
        Catch ex As Exception

        End Try

        Try
            val = (root.Element("finish-window").Element("installed-tick-img").Value)
            If Not val = Nothing Then
                PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val)

            End If
        Catch ex As Exception

        End Try

        Try
            val = (root.Element("finish-window").Element("log-backcolor").Value)
            If Not val = Nothing Then
                TextBox2.BackColor = ColorTranslator.FromHtml(val)
                TextBox2.BorderStyle = BorderStyle.None
            End If
        Catch ex As Exception

        End Try
        Try
            val = (root.Element("finish-window").Element("log-textcolor").Value)
            If Not val = Nothing Then
                TextBox2.ForeColor = ColorTranslator.FromHtml(val)
            End If
        Catch ex As Exception

        End Try

        Try
            val = (root.Element("uninstall").Element("list-backcolor").Value)
            If Not val = Nothing Then
                Uninstaller.TextBox1.BackColor = ColorTranslator.FromHtml(val)
                Uninstaller.RichTextBox1.BackColor = ColorTranslator.FromHtml(val)
                Uninstaller.Panel2.BackColor = ColorTranslator.FromHtml(val)
                Uninstaller.Panel3.BackColor = ColorTranslator.FromHtml(val)
                Uninstaller.RichTextBox1.BorderStyle = BorderStyle.None
                Uninstaller.TextBox1.BorderStyle = BorderStyle.None
            End If
        Catch ex As Exception

        End Try

        Try
            val = (root.Element("uninstall").Element("list-textcolor").Value)
            If Not val = Nothing Then
                Uninstaller.TextBox1.ForeColor = ColorTranslator.FromHtml(val)
                Uninstaller.RichTextBox1.ForeColor = ColorTranslator.FromHtml(val)
            End If
        Catch ex As Exception

        End Try

        Try
            val = (root.Element("uninstall").Element("uninstall-img").Value)
            If Not val = Nothing Then
                Uninstaller.PictureBox1.Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & val)

            End If
        Catch ex As Exception

        End Try

        Try
            val = (root.Element("buttons").Element("checkbox-checked").Value)
            If Not val = Nothing Then
                cbChecked = val

            End If
        Catch ex As Exception

        End Try
        Try
            val = (root.Element("buttons").Element("radio-checked").Value)
            If Not val = Nothing Then
                rChecked = val

            End If
        Catch ex As Exception

        End Try

        Try
            val = (root.Element("buttons").Element("radio-unchecked").Value)
            If Not val = Nothing Then
                rUnchecked = val

            End If
        Catch ex As Exception

        End Try
        Try
            val = (root.Element("buttons").Element("checkbox-unchecked").Value)
            If Not val = Nothing Then
                cbUnchecked = val

            End If
        Catch ex As Exception

        End Try

        Try
            val = (root.Element("main").Element("drop-down").Value)
            If Not val = Nothing Then
                drop1 = val

            End If
        Catch ex As Exception

        End Try

        Try
            val = (root.Element("main").Element("drop-down-hover").Value)
            If Not val = Nothing Then
                drop2 = val

            End If
        Catch ex As Exception

        End Try

        Try
            val = (root.Element("main").Element("drop-down-textcolor").Value)
            If Not val = Nothing Then
                Try
                    droptxtcolor = val
                Catch ex As Exception

                End Try

            End If
        Catch ex As Exception

        End Try

        If Not drop2 = Nothing And Not drop1 = Nothing Then

            Me.Controls.Add(langthemed)
            Try
                langthemed.Label1.ForeColor = ColorTranslator.FromHtml(droptxtcolor)
            Catch ex As Exception

            End Try

        End If

    End Function


    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme") And My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\Theme.xml") Then
            theme()
            Me.Controls.Add(button)
            button.PictureBox1.Size = Button1.Size
            button.PictureBox2.Size = Button2.Size
            Try

                button.changenext(0)
            Catch ex As Exception

            End Try

        End If

        appname = Application.ExecutablePath.Replace(My.Computer.FileSystem.CurrentDirectory & "\", "")

        Try
            Me.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath)
            splash_screen.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
        Catch ex As Exception

        End Try
        line1.AutoSize = False
        line1.Height = 2
        showselect = 0
        rp = "No"
        adv = 1
        If splash_screen.lang > 3 Then
            ComboBox1.SelectedIndex = 0
        Else
            ComboBox1.SelectedIndex = splash_screen.lang
        End If

        'checking if x64 or x86
        ' Dim cpuID As String = _
        'My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\0", _
        '  "Identifier", "n/a")
        '
        'Get all chars from the beginning of the string until the first space is detected.
        ' cpuID = cpuID.Substring(0, cpuID.IndexOf(" "))
        '  If LCase(cpuID).Contains("amd64") OrElse LCase(cpuID).Contains("intel64") OrElse LCase(cpuID).Contains("64") Then
        If IntPtr.Size = 8 Then
            winbit = "x64"
            ' sysnative = "\Sysnative\"
        Else
            winbit = "x86"
            ' sysnative = "\System32\"

        End If



        Try
            For Each ff In Directory.GetFiles(winDir & "\", "*.iPtemp2", SearchOption.TopDirectoryOnly)
                File.Delete(ff)
            Next
            For Each ff In Directory.GetFiles(winDir & "\System32\", "*.iPtemp2", SearchOption.TopDirectoryOnly)
                File.Delete(ff)
            Next
            If winbit = "x64" Then
                For Each ff In Directory.GetFiles(winDir & "\SysWOW64\", "*.iPtemp2", SearchOption.TopDirectoryOnly)
                    File.Delete(ff)
                Next
            End If
        Catch ex As UnauthorizedAccessException
        End Try
        Try
            My.Computer.FileSystem.DeleteFile(winDir & "\Branding\ShellBrd\shellbrd.dll.iPtemp2")
            My.Computer.FileSystem.DeleteFile(winDir & "\Branding\Basebrd\basebrd.dll.iPtemp2")
        Catch ex As Exception
        End Try
        Try
            For Each ff In Directory.GetFiles(winDir & "\", "*.iPtemp", SearchOption.TopDirectoryOnly)
                File.Delete(ff)
            Next
            For Each ff In Directory.GetFiles(winDir & "\System32\", "*.iPtemp", SearchOption.TopDirectoryOnly)
                File.Delete(ff)
            Next
            If winbit = "x64" Then
                For Each ff In Directory.GetFiles(winDir & "\SysWOW64\", "*.iPtemp", SearchOption.TopDirectoryOnly)
                    File.Delete(ff)
                Next
            End If
        Catch ex As UnauthorizedAccessException
        End Try
        Try
            My.Computer.FileSystem.DeleteFile(winDir & "\Branding\ShellBrd\shellbrd.dll.iPtemp")
            My.Computer.FileSystem.DeleteFile(winDir & "\Branding\Basebrd\basebrd.dll.iPtemp")
        Catch ex As Exception
        End Try
        Try
            For Each ff In Directory.GetFiles(winDir & "\", "*.iPold2", SearchOption.TopDirectoryOnly)
                File.Delete(ff)
            Next
            For Each ff In Directory.GetFiles(winDir & "\System32\", "*.iPold2", SearchOption.TopDirectoryOnly)
                File.Delete(ff)
            Next
            If winbit = "x64" Then
                For Each ff In Directory.GetFiles(winDir & "\SysWOW64\", "*.iPold2", SearchOption.TopDirectoryOnly)
                    File.Delete(ff)

                Next
            End If
        Catch ex As UnauthorizedAccessException
        End Try
        Try
            My.Computer.FileSystem.DeleteFile(winDir & "\Branding\ShellBrd\shellbrd.dll.iPold2")
            My.Computer.FileSystem.DeleteFile(winDir & "\Branding\Basebrd\basebrd.dll.iPold2")
        Catch ex As Exception
        End Try
        Try
            For Each ff In Directory.GetFiles(winDir & "\", "*.iPold", SearchOption.TopDirectoryOnly)
                File.Delete(ff)
            Next
            For Each ff In Directory.GetFiles(winDir & "\System32\", "*.iPold", SearchOption.TopDirectoryOnly)
                File.Delete(ff)
            Next
            If winbit = "x64" Then
                For Each ff In Directory.GetFiles(winDir & "\SysWOW64\", "*.iPold", SearchOption.TopDirectoryOnly)
                    File.Delete(ff)
                Next
            End If
        Catch ex As UnauthorizedAccessException
        End Try
        Try
            My.Computer.FileSystem.DeleteFile(winDir & "\Branding\ShellBrd\shellbrd.dll.iPold")
            My.Computer.FileSystem.DeleteFile(winDir & "\Branding\Basebrd\basebrd.dll.iPold")
        Catch ex As Exception
        End Try

        splash_screen.Opacity = 0

        Try
            SaveToDisk("Patcher.exe", My.Computer.FileSystem.CurrentDirectory & "\Patcher.exe")
            My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\reload.bat")
        Catch ex As Exception

        End Try


        If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.CurrentDirectory & "\Resource Files") = True Or My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\Resource.iPack") = True And My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack") = True Then

        Else
            Me.Opacity = 0
            Application.Exit()

        End If
        Dim loc As String
        loc = My.Computer.FileSystem.CurrentDirectory
        If My.Computer.FileSystem.FileExists(loc & "\Setup files-iPack\Configuration.config") = True Then

        Else
            Me.Opacity = 0
            Application.Exit()
        End If

        If My.Computer.FileSystem.FileExists(loc & "\Setup files-iPack\Header.png") Then
        Else
            SaveToDisk("Header.png", loc & "\Setup files-iPack\Header.png")
        End If
        If My.Computer.FileSystem.FileExists(loc & "\Setup files-iPack\logo.png") Then
        Else
            SaveToDisk("logo.png", loc & "\Setup files-iPack\logo.png")
        End If
        If My.Computer.FileSystem.FileExists(loc & "\Setup files-iPack\License.txt") Then
        Else
            SaveToDisk("Liscense", loc & "\Setup files-iPack\License.txt")
        End If

        Try
            Label3.Text = GetSettingItem("Setup files-iPack\Configuration.config", "Pack Name")
            pack = String.Concat(pack, Label3.Text)
            '------->author name
            Label4.Text = GetSettingItem("Setup files-iPack\Configuration.config", "Author")
            author = String.Concat(author, Label4.Text)

        Catch ex As Exception
        End Try

        rebootneed = "No"
        pack = ""
        author = ""
        barmaxval = 0
        totalfiles = 0
        totalfiles0 = 0

        w = Me.Width
        h = TextBox1.Size.Height
        x = TextBox1.Location.X
        a = Label1.Location.Y
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle

        If major = 6 And minor = 1 Then
            windowsver = "Windows 7"
        ElseIf major = 6 And minor = 2 Then
            windowsver = "Windows 8"
        ElseIf major = 6 And minor = 3 Then
            windowsver = "Windows 8.1"
        ElseIf major = 6 And minor = 4 Then
            windowsver = "Windows 10"
        ElseIf major = 10 And minor = 0 Then
            windowsver = "Windows 10"
        End If
        Timer1.Enabled = False

        Try
            Dim image As Image = Nothing
            image = image.FromFile("Setup files-iPack\logo.png")
            imgThumb = image.GetThumbnailImage(x - 4, h + 1, Nothing, New IntPtr())
            Me.Refresh()
        Catch ex As Exception

        End Try

        Try

            '------->heading of the installer
            mainheading = GetSettingItem("Setup files-iPack\Configuration.config", "Heading")
            Me.Text = mainheading

            '------->Pack name
            Label3.Text = GetSettingItem("Setup files-iPack\Configuration.config", "Pack Name")
            pack = String.Concat(pack, Label3.Text)

            '------->author name
            Label4.Text = GetSettingItem("Setup files-iPack\Configuration.config", "Author")
            author = String.Concat(author, Label4.Text)

            '------->license text align
            lalign = GetSettingItem("Setup files-iPack\Configuration.config", "Lalign")

            '------->Pack name
            adv = GetSettingItem("Setup files-iPack\Configuration.config", "Ad")

            TextBox1.Text = My.Computer.FileSystem.ReadAllText(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\License.txt")
            RichTextBox2.Text = My.Computer.FileSystem.ReadAllText(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\License.txt")

            TextBox1.Select(0, 0)

            If lalign = 3 Then
                TextBox1.TextAlign = HorizontalAlignment.Right
                RichTextBox2.SelectAll()
                RichTextBox2.SelectionAlignment = HorizontalAlignment.Right
                RichTextBox2.Select(0, 0)
            ElseIf lalign = 2 Then
                TextBox1.TextAlign = HorizontalAlignment.Center
                RichTextBox2.SelectAll()
                RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
                RichTextBox2.Select(0, 0)
            Else
                TextBox1.TextAlign = HorizontalAlignment.Left
                RichTextBox2.SelectAll()
                RichTextBox2.SelectionAlignment = HorizontalAlignment.Left
                RichTextBox2.Select(0, 0)
            End If


        Catch ex As Exception

        End Try
        Dim packKey As Microsoft.Win32.RegistryKey
        packKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall\" & pack)

        If packKey Is Nothing Then
            If splash_screen.silent = 1 Then
                adv = 0
                Button1_Click(Nothing, Nothing)
            End If


        Else

            Me.Controls.Add(Uninstaller)

            Uninstaller.Location = New Point(0, 0)
            TextBox1.Visible = False
            RichTextBox2.Visible = False
            Button1.Visible = False
            Button2.Visible = False
            CheckBox1.Visible = False

        End If


        If willpatch = "no" And willpatch2 = "no" Then


        End If


    End Sub

    Public Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Dim r As String

        If ComboBox1.SelectedIndex = 0 Then
            r = MessageBox.Show("Are you sure you want to quit?", mainheading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        ElseIf ComboBox1.SelectedIndex = 1 Then
            r = MessageBox.Show("¿Está seguro que desea salir?", mainheading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        ElseIf ComboBox1.SelectedIndex = 2 Then
            r = MessageBox.Show("Вы уверены, что вы хотите бросить курить?", mainheading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        ElseIf ComboBox1.SelectedIndex = 3 Then
            r = MessageBox.Show("Sind Sie sicher, dass Sie aufhören möchten?", mainheading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        End If

        If r = DialogResult.No Then
        ElseIf r = DialogResult.Yes Then
            Try
                My.Computer.FileSystem.DeleteFile("Patcher.log")
                My.Computer.FileSystem.DeleteFile("Patcher.ini")
                My.Computer.FileSystem.DeleteDirectory("Resource Files\ACL", FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception

            End Try
            Dim objWriter As New System.IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & pack & ".bat")
            objWriter.Write("cd %windir%")
            objWriter.Write(vbNewLine)
            objWriter.Write("tasklist /FI " & Chr(34) & "IMAGENAME eq " & appname & "" & Chr(34) & " 2>NUL | find /I /N " & Chr(34) & appname & Chr(34) & ">NUL")
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
            launch.FileName = (My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & pack & ".bat")
            launch.WorkingDirectory = ""
            Process.Start(launch)
            Application.Exit()
        End If
    End Sub
    Public Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If splash_screen.silent = 1 Then
            showselect = 1
        End If
        If showselect = 0 Then
            Me.Controls.Add(showfiles)
            TextBox1.Hide()
            RichTextBox2.Hide()
            CheckBox1.Hide()
            Button1.Hide()
            Button2.Hide()
            ComboBox1.Hide()
            Try
                langthemed.Hide()
            Catch ex As Exception
            End Try
        Else
            advert.Hide()
            showfiles.Hide()
            button.Hide()
            ProgressBar1.Minimum = 0
            Label1.Visible = True
            Label2.Visible = True
            Label6.Visible = True
            Label7.Visible = True
            ComboBox1.Hide()
            Try
                langthemed.Hide()
            Catch ex As Exception
            End Try
            Label6.Text = windowsver & " " & winbit
            Label3.Show()
            Label4.Show()
            ProgressBar1.Visible = True
            RichTextBox1.Show()
            Panel2.Show()
            Panel1.Show()
            Label5.Show()
            TextBox1.Hide()
            RichTextBox2.Hide()
            Button1.Hide()
            Button2.Hide()
            CheckBox1.Hide()
            imgThumb = Nothing
            Me.Refresh()

            Try
                Dim image2 As Image = Nothing
                image2 = Image.FromFile("Setup files-iPack\header.png")
                imgThumb2 = image2.GetThumbnailImage(TextBox2.Width + 6, image2.Height, Nothing, New IntPtr())
                Me.Refresh()
            Catch ex As Exception

            End Try


            Try
                For Each File In My.Computer.FileSystem.GetFiles("Resource Files")
                    If File.Contains(".res") Then
                        totalfiles = totalfiles + 1
                    End If
                Next
                If My.Computer.FileSystem.DirectoryExists("Resource Files\Program Files") Then
                    For Each Dir As String In System.IO.Directory.GetDirectories("Resource Files\Program Files")
                        f = Dir.TrimStart
                        dirname = IO.Path.GetFileName(Dir)
                        For Each File In My.Computer.FileSystem.GetFiles("Resource Files\Program Files\" & dirname)
                            If File.Contains(".res") Then
                                total_in_program_files = total_in_program_files + 1
                            End If
                        Next
                    Next
                End If

            Catch ex As Exception

            End Try

            winfolder = Path.GetDirectoryName(Environment.SystemDirectory)
            Timer1.Interval = 10
            Timer1.Enabled = True

            If winbit = "x64" Then
                ProgressBar1.Maximum = (totalfiles + total_in_program_files) * 2
                ProgressBar1.Maximum = ProgressBar1.Maximum + 1
            Else
                ProgressBar1.Maximum = (totalfiles + total_in_program_files)
                ProgressBar1.Maximum = ProgressBar1.Maximum + 1
            End If



            If ComboBox1.SelectedIndex = 0 Then
                RichTextBox1.Text = "Initializing. . ."
                Dim a As String
                If splash_screen.silent = 0 Then
                    a = MessageBox.Show("Do you want to make a restore point?" & vbNewLine & "(It will take some extra time)", mainheading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If a = DialogResult.Yes Then
                        rp = "Yes"
                        ProgressBar1.Hide()
                        ProgressBar2.Visible = True

                    ElseIf a = DialogResult.No Then
                    End If
                End If


            ElseIf ComboBox1.SelectedIndex = 1 Then
                RichTextBox1.Text = "Inicializando. . ."
                Dim a As String
                If splash_screen.silent = 0 Then
                    a = MessageBox.Show("¿Quieres hacer un restore punto?" & vbNewLine & "(Tomará algo de tiempo extra)", mainheading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If a = DialogResult.Yes Then
                        rp = "Yes"
                        ProgressBar1.Hide()
                        ProgressBar2.Visible = True
                    ElseIf a = DialogResult.No Then
                    End If
                End If

            ElseIf ComboBox1.SelectedIndex = 2 Then
                RichTextBox1.Text = "Инициализация. . ."

                Dim a As String
                If splash_screen.silent = 0 Then
                    a = MessageBox.Show("Вы хотите сделать точку восстановления ?" & vbNewLine & "(Это займет некоторое дополнительное время)", mainheading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If a = DialogResult.Yes Then
                        rp = "Yes"
                        ProgressBar1.Hide()
                        ProgressBar2.Visible = True
                    ElseIf a = DialogResult.No Then
                    End If
                End If

            ElseIf ComboBox1.SelectedIndex = 3 Then
                RichTextBox1.Text = "Initialisierung. . ."

                Dim a As String
                If splash_screen.silent = 0 Then
                    a = MessageBox.Show("Machen einen Wiederherstellungspunkt ?" & vbNewLine & "(Es wird etwas mehr Zeit nehmen)", mainheading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If a = DialogResult.Yes Then
                        rp = "Yes"
                        ProgressBar1.Hide()
                        ProgressBar2.Visible = True
                    ElseIf a = DialogResult.No Then
                    End If
                End If

            End If
            total_in_program_files = 0
            Try
                My.Computer.FileSystem.CreateDirectory("Resource Files\Backup\Windows")
                My.Computer.FileSystem.CreateDirectory("Resource Files\Backup\System32")
                My.Computer.FileSystem.CreateDirectory("Resource Files\Backup\SysWOW64")
                My.Computer.FileSystem.CreateDirectory("Resource Files\Backup\Program Files")
                My.Computer.FileSystem.CreateDirectory("Resource Files\Backup\Program Files (x86)")
                My.Computer.FileSystem.CreateDirectory("Resource Files\Patch\SysWOW64")
                My.Computer.FileSystem.CreateDirectory("Resource Files\Patch\System32")
                My.Computer.FileSystem.CreateDirectory("Resource Files\ACL")
                My.Computer.FileSystem.CreateDirectory("Resource Files\ACL\System32")
                My.Computer.FileSystem.CreateDirectory("Resource Files\ACL\SysWOW64")
            Catch ex As Exception
            End Try

            BackgroundWorker1.RunWorkerAsync()
            start_time = Now


        End If


    End Sub



    Private Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            Button1.Enabled = True
            Try
                button.changenext(1)
            Catch ex As Exception

            End Try
        Else
            Button1.Enabled = False
            Try
                button.changenext(0)
            Catch ex As Exception

            End Try
        End If


    End Sub




    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint

        If TextBox1.Visible = True Then
            If Not imgThumb Is Nothing Then
                e.Graphics.DrawImage(imgThumb, 2, 2, imgThumb.Width, imgThumb.Height)
            End If
        Else
            If Not imgThumb2 Is Nothing Then
                e.Graphics.DrawImage(imgThumb2, 0, 0, imgThumb2.Width, imgThumb2.Height)
            End If

        End If

        
    End Sub

    Private Sub RichTextBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RichTextBox1.TextChanged
        RichTextBox1.SelectionStart = RichTextBox1.Text.Length
        RichTextBox1.ScrollToCaret()
        Try
            Thread.Sleep(100)
            If ComboBox1.SelectedIndex = 0 Then
                totalpatched = Regex.Matches(RichTextBox1.Text, "Patching").Count
                Label5.Text = "Overall progress:   " & "(" & totalpatched & "/" & (ProgressBar1.Maximum - 1) & ")"
            ElseIf ComboBox1.SelectedIndex = 1 Then
                totalpatched = Regex.Matches(RichTextBox1.Text, "Parches").Count
                Label5.Text = "Progreso en general:   " & "(" & totalpatched & "/" & (ProgressBar1.Maximum - 1) & ")"
            ElseIf ComboBox1.SelectedIndex = 2 Then
                totalpatched = Regex.Matches(RichTextBox1.Text, "Внесение исправлений").Count
                Label5.Text = "Общий прогресс:   " & "(" & totalpatched & "/" & (ProgressBar1.Maximum - 1) & ")"
            ElseIf ComboBox1.SelectedIndex = 3 Then
                totalpatched = Regex.Matches(RichTextBox1.Text, "Patchen").Count
                Label5.Text = "Gesamtfortschritt:   " & "(" & totalpatched & "/" & (ProgressBar1.Maximum - 1) & ")"
            End If
            ProgressBar1.Value = totalpatched
        Catch ex As Exception
        End Try

        If RichTextBox1.Text.Contains("Restore Point created successfully") Or RichTextBox1.Text.Contains("Punto de restauración creado con éxito") Or RichTextBox1.Text.Contains("Точка восстановления успешно создана") Or RichTextBox1.Text.Contains("Wiederherstellungspunkt erfolgreich erstellt") Or RichTextBox1.Text.Contains("Could not create restore point!") Or RichTextBox1.Text.Contains("No se pudo crear punto de restauración.") Or RichTextBox1.Text.Contains("Не удалось создать точку восстановления!") Or RichTextBox1.Text.Contains("Wiederherstellungspunkt konnte nicht erstellt werden!") Then
            ProgressBar2.Hide()
            ProgressBar1.Visible = True
        End If
        Try
            If RichTextBox1.Text.Contains("Backup") Or RichTextBox1.Text.Contains("Copia de seguridad") Or RichTextBox1.Text.Contains("Резервное копирование") Or RichTextBox1.Text.Contains("Sicherungs") Then
                ProgressBar2.Hide()
                ProgressBar1.Visible = True
            End If


        Catch ex As Exception

        End Try

        If RichTextBox1.Text.Contains("Done.") = True Or RichTextBox1.Text.Contains("¡Hecho!") = True Or RichTextBox1.Text.Contains("Готово!") = True Or RichTextBox1.Text.Contains("Fertig!") = True Then
            ProgressBar1.Value = ProgressBar1.Maximum
            Try
                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Patcher.exe")
                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Patcher.log")
                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Patcher.ini")
                ' My.Computer.FileSystem.DeleteDirectory(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\ACL", FileIO.DeleteDirectoryOption.DeleteAllContents)
                My.Computer.FileSystem.DeleteDirectory(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Patch", FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception
            End Try

            If RichTextBox1.Text.Contains("Scheduling") Or RichTextBox1.Text.Contains("Programación") Or RichTextBox1.Text.Contains("Планирование") Or RichTextBox1.Text.Contains("Planen") Then
                rebootneed = "Yes"
            End If

            Dim rKey As Microsoft.Win32.RegistryKey
            rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System\CurrentControlSet\Control\Session Manager")

            If rKey.GetValue("PendingFileRenameOperations") Is Nothing Then

            Else
                Dim str() As String
                str = rKey.GetValue("PendingFileRenameOperations")
                Dim i As Integer
                For i = 0 To str.Length - 1

                    If str(i).Contains("shell32.dll") Or str(i).Contains("themeui.dll") Or str(i).Contains("networkexplorer.dll") Or str(i).Contains("ntshrui.dll") Or str(i).Contains("comdlg32.dll") Or str(i).Contains(".iPtemp") Or str(i).Contains(".iPtemp2") Then
                        CheckBox2.Visible = True
                        rebootneed = "Yes"
                    End If
                Next
            End If
            Try
                SaveToDisk("Uninstall_config", My.Computer.FileSystem.CurrentDirectory & "\Uninstall iPack.exe.config")
                SaveToDisk("Uninstall iPack.exe", My.Computer.FileSystem.CurrentDirectory & "\Uninstall iPack.exe")
                Dim hide As New ProcessStartInfo
                hide.FileName = "CMD"
                hide.Arguments = "/c ATTRIB +H " & Chr(34) & My.Computer.FileSystem.CurrentDirectory & "\Uninstall iPack.exe.config" & Chr(34) & " /S /D && exit"
                hide.WindowStyle = ProcessWindowStyle.Hidden
                Dim h As New Process()
                h.StartInfo = hide
                h.Start()
                h.WaitForExit()
            Catch ex As Exception

            End Try
            My.Computer.Registry.LocalMachine.CreateSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall\" & pack)
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Uninstall\" & pack, "InstallLocation", My.Computer.FileSystem.CurrentDirectory)
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Uninstall\" & pack, "DisplayName", pack)
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Uninstall\" & pack, "DisplayName", pack)
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Uninstall\" & pack, "UninstallString", Chr(34) & My.Computer.FileSystem.CurrentDirectory & "\Uninstall iPack.exe" & Chr(34) & " " & Chr(34) & My.Computer.FileSystem.CurrentDirectory & Chr(34))
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Uninstall\" & pack, "Publisher", author)
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Uninstall\" & pack, "DisplayIcon", Application.ExecutablePath)
            Try
                For Each filename As String In IO.Directory.GetFiles(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\", "*.res")
                    IO.File.Delete(filename)
                Next
                My.Computer.FileSystem.DeleteDirectory(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Program Files", FileIO.DeleteDirectoryOption.DeleteAllContents)

            Catch ex As Exception

            End Try

            stop_time = Now
            elapsed = stop_time.Subtract(start_time)
            finaltime = elapsed.TotalSeconds.ToString("0.00")

            If rebootneed = "Yes" Then
                CheckBox2.Visible = True
                CheckBox2.Checked = True
            Else
                CheckBox2.Visible = False
                CheckBox2.CheckState = CheckState.Unchecked
                CheckBox2.Checked = False
            End If
            ProgressBar1.Hide()
            Panel1.Hide()
            Panel2.Hide()
            RichTextBox1.Hide()
            ProgressBar1.Hide()
            Label1.Hide()
            Label3.Hide()
            Label2.Hide()
            Label6.Hide()
            Label7.Hide()
            Label4.Hide()
            Label5.Hide()
            PictureBox1.Visible = True
            Label9.Visible = True
            TextBox2.Visible = True
            Dim rp2 As String

            If ComboBox1.SelectedIndex = 0 Then
                TextBox2.Text = ("[.. Logs ..]" & vbNewLine & vbNewLine & "Package Installed: " & pack & "" & vbNewLine & "Author: " & author & "" & vbNewLine & "OS: " & windowsver & " " & winbit & vbNewLine & "" & "Total Files Patched: " & totalpatched & "" & vbNewLine & "Backup Available: Yes" & vbNewLine & "Total Patching Time: ~" & finaltime & " sec" & vbNewLine & "Restore Point: " & rp & vbNewLine & "Reboot Required: " & rebootneed)
            ElseIf ComboBox1.SelectedIndex = 1 Then
                If rebootneed = "Yes" Then
                    rebootneed2 = "Sí"
                Else
                    rebootneed2 = "No"
                End If
                If rp = "Yes" Then
                    rp2 = "Sí"
                Else
                    rp2 = "No"
                End If
                TextBox2.Text = ("[.. Registros ..]" & vbNewLine & vbNewLine & "Paquete instalado: " & pack & "" & vbNewLine & "Autor: " & author & "" & vbNewLine & "OS: " & windowsver & " " & winbit & vbNewLine & "" & "Total Archivos parcheado: " & totalpatched & "" & vbNewLine & "Copia de seguridad: Sí" & vbNewLine & "Total de tiempo de aplicación: ~" & finaltime & " sec" & vbNewLine & "Punto de restauración: " & rp2 & vbNewLine & "Se requiere reiniciar: " & rebootneed2)
            ElseIf ComboBox1.SelectedIndex = 2 Then
                If rebootneed = "Yes" Then
                    rebootneed2 = "Да"
                Else
                    rebootneed2 = "Нет"
                End If
                If rp = "Yes" Then
                    rp2 = "Да"
                Else
                    rp2 = "Нет"
                End If
                TextBox2.Text = ("[.. Журналы ..]" & vbNewLine & vbNewLine & "Установлен пакет: " & pack & "" & vbNewLine & "Автор: " & author & "" & vbNewLine & "OS: " & windowsver & " " & winbit & vbNewLine & "" & "Всего файлов пропатчен: " & totalpatched & "" & vbNewLine & "Резервное копирование: Да" & vbNewLine & "Внесение исправлений Время: ~" & finaltime & " sec" & vbNewLine & "точку восстановления: " & rp2 & vbNewLine & "Требуется перезагрузка: " & rebootneed2)
            ElseIf ComboBox1.SelectedIndex = 3 Then
                If rebootneed = "Yes" Then
                    rebootneed2 = "Ja"
                Else
                    rebootneed2 = "Nicht"
                End If
                If rp = "Yes" Then
                    rp2 = "Ja"
                Else
                    rp2 = "Nicht"
                End If
                TextBox2.Text = ("[.. protokollieren ..]" & vbNewLine & vbNewLine & "Paket installiert: " & pack & "" & vbNewLine & "Autor: " & author & "" & vbNewLine & "OS: " & windowsver & " " & winbit & vbNewLine & "" & "Gesamtanzahl der Dateien gepatcht: " & totalpatched & "" & vbNewLine & "Sicherungskopie: Ja" & vbNewLine & "Gesamtzeit Patchen: ~" & finaltime & " sec" & vbNewLine & "Wiederherstellungspunkt: " & rp2 & vbNewLine & "Neustart erforderlich: " & rebootneed2)
            End If

            TextBox2.Select(0, 0)

            PictureBox1.Location = New Point(Label9.Location.X - 30, Label9.Location.Y + 3)

            If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme") And My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\Theme.xml") Then
                Me.Controls.Add(finishbtn1)
                Button3.SendToBack()
                finishbtn1.Location = New Point(Button3.Location.X, Button3.Location.Y)
                finishbtn1.PictureBox1.Size = Button3.Size

            Else
                Button3.Show()
            End If

            If splash_screen.silent = 1 Then
                CheckBox2.Checked = False

                If rebootneed = "Yes" Then
                    If ComboBox1.SelectedIndex = 0 Then
                        MessageBox.Show("A reboot is needed to complete installation.", mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf ComboBox1.SelectedIndex = 1 Then
                        MessageBox.Show("Se necesita un reinicio para terminar la instalación.", mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf ComboBox1.SelectedIndex = 2 Then
                        MessageBox.Show("Перезагрузка необходима для завершения установки.", mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf ComboBox1.SelectedIndex = 3 Then
                        MessageBox.Show("Ein Neustart ist erforderlich, um die Installation abzuschließen", mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
                Button3_Click_1(Nothing, Nothing)
            End If
        End If
    End Sub



    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub



    Public Sub Button3_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        Try
            My.Computer.FileSystem.DeleteFile("Patcher.log")
            My.Computer.FileSystem.DeleteFile("Patcher.ini")
            '  My.Computer.FileSystem.DeleteDirectory("Resource Files\ACL", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch ex As Exception

        End Try
        If CheckBox2.Checked = True Then

            If ComboBox1.SelectedIndex = 0 Then
                MessageBox.Show("The windows will restart now.", mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf ComboBox1.SelectedIndex = 1 Then
                MessageBox.Show("Windows reiniciará.", mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf ComboBox1.SelectedIndex = 2 Then
                MessageBox.Show("Окна перезагрузится.", mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf ComboBox1.SelectedIndex = 3 Then
                MessageBox.Show("Windows wird neu gestartet.", mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            Dim RESTART As New ProcessStartInfo()
            RESTART.FileName = ("CMD")
            RESTART.Arguments = ("/c shutdown /r /f && exit")
            RESTART.WindowStyle = ProcessWindowStyle.Hidden
            Process.Start(RESTART)
            Application.Exit()
        End If
        If CheckBox2.Checked = False Then
            Application.Exit()
        End If
        Application.Exit()
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property
    Public Function rand() As String
        Dim sb As New StringBuilder
        Dim pureNumbers = rng.Next(1, 11)
        If pureNumbers < 7 Then
            Dim number As Integer = rng.Next(1, 1000000)
            Dim digits As String = number.ToString("000000")
            For i As Integer = 1 To 18
                Dim idx As Integer = rng.Next(0, digits.Length)
                sb.Append(digits.Substring(idx, 1))
            Next
        Else
            Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
            For i As Integer = 1 To 18
                Dim idx As Integer = rng.Next(0, 36)
                sb.Append(s.Substring(idx, 1))
            Next
        End If
        Return sb.ToString()
    End Function

    Private Function saveACL(ByVal name As String, ByVal path As String)

        Dim ical As New ProcessStartInfo
        If path.Contains("System32") Then
            Try
                logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Saving ACLfile of " & path & "\" & name & " to " & My.Computer.FileSystem.CurrentDirectory & "\Resource Files\ACL\System32" & vbNewLine)
                ical.FileName = winDir & "\System32\icacls.exe"
                ical.Arguments = Chr(34) & path & "\" & name & Chr(34) & " /save " & Chr(34) & "Resource Files\ACL\System32\" & name & ".AclFile" & Chr(34)
                ical.WindowStyle = ProcessWindowStyle.Hidden
                Dim pp As New Process()
                pp.StartInfo = ical
                pp.Start()
                pp.WaitForExit()
            Catch ex As Exception
                logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Failed to save ACLfile of " & path & "\" & name & " to " & My.Computer.FileSystem.CurrentDirectory & "\Resource Files\ACL\System32" & vbNewLine)

            End Try

        ElseIf path.Contains("SysWOW64") Then
            Try
                logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Saving ACLfile of " & path & "\" & name & " to " & My.Computer.FileSystem.CurrentDirectory & "\Resource Files\ACL\SysWOW64" & vbNewLine)
                ical.FileName = winDir & "\System32\icacls.exe"
                ical.Arguments = Chr(34) & path & "\" & name & Chr(34) & " /save " & Chr(34) & "Resource Files\ACL\SysWOW64\" & name & ".AclFile" & Chr(34)
                ical.WindowStyle = ProcessWindowStyle.Hidden
                Dim pp As New Process()
                pp.StartInfo = ical
                pp.Start()
                pp.WaitForExit()
            Catch ex As Exception
                logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Failed to save ACLfile of " & path & "\" & name & " to " & My.Computer.FileSystem.CurrentDirectory & "\Resource Files\ACL\SysWOW64" & vbNewLine)

            End Try
        Else
            Try
                logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Saving ACLfile of " & path & "\" & name & " to " & My.Computer.FileSystem.CurrentDirectory & "\Resource Files\ACL" & vbNewLine)
                ical.FileName = winDir & "\System32\icacls.exe"
                ical.Arguments = Chr(34) & path & "\" & name & Chr(34) & " /save " & Chr(34) & "Resource Files\ACL\" & name & ".AclFile" & Chr(34)
                ical.WindowStyle = ProcessWindowStyle.Hidden
                Dim pp As New Process()
                pp.StartInfo = ical
                pp.Start()
                pp.WaitForExit()
            Catch ex As Exception
                logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Failed to save ACLfile of " & path & "\" & name & " to " & My.Computer.FileSystem.CurrentDirectory & "\Resource Files\ACL" & vbNewLine)

            End Try
        End If




    End Function

    Private Function backupFile(ByVal name As String, ByVal sourcePath As String, ByVal backupFolder As String, ByVal patchFolder As String)
        If backupFolder.Contains("SysWOW64") Then
            If ComboBox1.SelectedIndex = 0 Then
                status += "Backup " & name & " x86" & vbNewLine
                enable = "true"
            ElseIf ComboBox1.SelectedIndex = 1 Then
                status += "Copia de seguridad " & name & " x86" & vbNewLine
                enable = "true"
            ElseIf ComboBox1.SelectedIndex = 2 Then
                status += "Резервное копирование " & name & " x86" & vbNewLine
                enable = "true"
            ElseIf ComboBox1.SelectedIndex = 3 Then
                status += "Sicherungs " & name & " x86" & vbNewLine
                enable = "true"
            End If
        Else
            If ComboBox1.SelectedIndex = 0 Then
                status += "Backup " & name & vbNewLine
                enable = "true"
            ElseIf ComboBox1.SelectedIndex = 1 Then
                status += "Copia de seguridad " & name & vbNewLine
                enable = "true"
            ElseIf ComboBox1.SelectedIndex = 2 Then
                status += "Резервное копирование " & name & vbNewLine
                enable = "true"
            ElseIf ComboBox1.SelectedIndex = 3 Then
                status += "Sicherungs " & name & vbNewLine
                enable = "true"
            End If
        End If


        Try
            My.Computer.FileSystem.CopyFile(sourcePath & "\" & name, backupFolder & "\" & name, True)
            My.Computer.FileSystem.CopyFile(sourcePath & "\" & name, patchFolder & "\" & name, True)
            logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Backup " & sourcePath & "\" & name & " to " & My.Computer.FileSystem.CurrentDirectory & "\" & backupFolder & vbNewLine)
        Catch ex As Exception
            logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Failed to backup " & sourcePath & "\" & name & vbNewLine)

        End Try

    End Function

    Private Function takeOwnership(ByVal name As String, ByVal path As String)
        Dim takeown As New ProcessStartInfo
        Try
            logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Taking ownership of " & path & "\" & name & vbNewLine)
            takeown.FileName = "CMD"
            takeown.Arguments = "/c takeown /a /F " & Chr(34) & path & "\" & name & Chr(34) & " && icacls " & Chr(34) & path & "\" & name & Chr(34) & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F" & " && icacls " & Chr(34) & path & "\" & name & Chr(34) & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F" & " && exit"
            takeown.WindowStyle = ProcessWindowStyle.Hidden
            Dim pp1 As New Process()
            pp1.StartInfo = takeown
            pp1.Start()
            pp1.WaitForExit()

        Catch ex As Exception
            logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Failed to take ownership of " & path & "\" & name & vbNewLine)
        End Try


    End Function

    Private Function patchFile(ByVal name As String, ByVal resFolder As String, ByVal patchfolder As String)
        Dim ps As New ProcessStartInfo
        ps.FileName = "Patcher.exe"
        ps.Arguments = ("-addoverwrite " & Chr(34) & patchfolder & "\" & name & Chr(34) & ", " & Chr(34) & patchfolder & "\" & name & Chr(34) & ", " & Chr(34) & resFolder & "\" & name & ".res" & Chr(34) & " ,,,")
        Dim p As New Process()
        p.EnableRaisingEvents = True    'required if using events
        p.StartInfo = ps
        enable = "true"

        If patchfolder.Contains("SysWOW64") Then
            If ComboBox1.SelectedIndex = 0 Then
                status += "Patching " & name & " x86" & vbNewLine
                enable = "true"
            ElseIf ComboBox1.SelectedIndex = 1 Then
                status += "Parches " & name & " x86" & vbNewLine
                enable = "true"
            ElseIf ComboBox1.SelectedIndex = 2 Then
                status += "Внесение исправлений " & name & " x86" & vbNewLine
                enable = "true"
            ElseIf ComboBox1.SelectedIndex = 3 Then
                status += "Patchen " & name & " x86" & vbNewLine
                enable = "true"
            End If

        Else

            If ComboBox1.SelectedIndex = 0 Then
                status += "Patching " & name & vbNewLine
                enable = "true"
            ElseIf ComboBox1.SelectedIndex = 1 Then
                status += "Parches " & name & vbNewLine
                enable = "true"
            ElseIf ComboBox1.SelectedIndex = 2 Then
                status += "Внесение исправлений " & name & vbNewLine
                enable = "true"
            ElseIf ComboBox1.SelectedIndex = 3 Then
                status += "Patchen " & name & vbNewLine
                enable = "true"
            End If
        End If
        Try

            p.Start()
            p.WaitForExit()
            logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Patching " & name & vbNewLine)
        Catch ex As Exception
            logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Failed to patch " & name & vbNewLine)
        End Try


    End Function

    Private Function restorePermission(ByVal name As String, ByVal patchFolder As String, ByVal aclFolder As String)
        Dim ical3 As New ProcessStartInfo
        Try
            ical3.FileName = "CMD"
            ical3.Arguments = "/c icacls " & Chr(34) & patchFolder & "\" & name & Chr(34) & " /setowner " & Chr(34) & "NT Service\TrustedInstaller" & Chr(34) & " /T /C" & " && icacls " & Chr(34) & patchFolder & Chr(34) & " /restore " & Chr(34) & aclFolder & "\" & name & ".AclFile" & Chr(34) & " && exit"
            ical3.WindowStyle = ProcessWindowStyle.Hidden
            Dim pp2 As New Process()
            pp2.StartInfo = ical3
            pp2.Start()
            pp2.WaitForExit()
            logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Restoring permission of " & name & vbNewLine)
        Catch ex As Exception
            logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Failed to restore permission of " & name & vbNewLine)
        End Try

    End Function

    Private Function moveBack(ByVal name As String, ByVal sourceFolder As String, ByVal destFolder As String, ByVal mkdir As String)

        Try
            If My.Computer.FileSystem.FileExists(destFolder & "\" & name & ".iPtemp") Then
                My.Computer.FileSystem.RenameFile(destFolder & "\" & name & ".iPtemp", name & ".iPold")
                My.Computer.FileSystem.DeleteFile(destFolder & "\" & name & ".iPold")
                Threading.Thread.Sleep(300)
            End If

            My.Computer.FileSystem.RenameFile(destFolder & "\" & name, name & ".iPtemp")
            Try
                logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Moving back " & name & " to " & destFolder & vbNewLine)
                My.Computer.FileSystem.MoveFile(sourceFolder & "\" & name, destFolder & "\" & name, True)
            Catch ex As Exception
                logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Failed to move back " & name & " to " & destFolder & vbNewLine)
            End Try


            Try
                If mkdir = "System32" Then
                    restorePermission(name, destFolder, "Resource Files\ACL\System32")
                ElseIf mkdir = "SysWOW64" Then
                    restorePermission(name, destFolder, "Resource Files\ACL\SysWOW64")
                Else
                    restorePermission(name, destFolder, "Resource Files\ACL")
                End If

            Catch ex As Exception

            End Try

            Dim del As New ProcessStartInfo
            del.FileName = "CMD"
            del.Arguments = "/c del /f /q " & Chr(34) & destFolder & "\" & name & ".iPtemp" & Chr(34) & " && exit"
            del.WindowStyle = ProcessWindowStyle.Hidden
            Dim ppx As New Process()
            ppx.StartInfo = del
            ppx.Start()
            ppx.WaitForExit()

            If My.Computer.FileSystem.FileExists(sourceFolder & "\" & name) Then
                Try
                    My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x)
                    My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\" & mkdir)
                    My.Computer.FileSystem.MoveFile(sourceFolder & "\" & name, My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\" & mkdir & "\" & name, True)
                Catch exx As Exception
                End Try


              
                    MoveFileEx(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\" & mkdir & "\" & name, destFolder, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)


                rebootneed = "Yes"
            End If
        Catch ex As Exception



        End Try
        Try
            If My.Computer.FileSystem.FileExists(destFolder & "\" & name & ".iPtemp") Then
                ' My.Computer.FileSystem.DeleteFile(winDir & "\Branding\ShellBrd\" & file1 & ".iPtemp")
                For i As Integer = 1 To 3
                    Try
                        My.Computer.FileSystem.DeleteFile(destFolder & "\" & name & ".iPtemp")

                        Exit For
                    Catch ex As Exception
                        If i = 3 Then


                            If ComboBox1.SelectedIndex = 0 Then
                                status += "Scheduling change on reboot for " & name & vbNewLine
                                enable = "true"
                            ElseIf ComboBox1.SelectedIndex = 1 Then
                                status += "Programación de cambio en el reinicio para " & name & vbNewLine
                                enable = "true"
                            ElseIf ComboBox1.SelectedIndex = 2 Then
                                status += "Планирование изменений при перезагрузке для " & name & vbNewLine
                                enable = "true"
                            ElseIf ComboBox1.SelectedIndex = 3 Then
                                status += "Planen Änderung beim Neustart für " & name & vbNewLine
                                enable = "true"
                            End If
                            logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Scheduling change on reboot for " & name & vbNewLine)

                            MoveFileEx(destFolder & "\" & name & ".iPtemp", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)


                        Else
                            Thread.Sleep(1000)   ' same as Threading.Thread.Sleep(1000)
                        End If
                    End Try
                Next
            End If


        Catch ex As Exception


        End Try

    End Function

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Thread.Sleep(500)
        Try
            My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\" & pack & ".log")
        Catch ex As Exception

        End Try
        logwriter = My.Computer.FileSystem.OpenTextFileWriter(My.Computer.FileSystem.CurrentDirectory & "\" & pack & ".log", True)
        logwriter.Write("=====================================================" & vbNewLine)
        logwriter.Write("iPack log file" & vbNewLine)
        logwriter.Write("© Mr Blade Designs" & vbNewLine)
        logwriter.Write("=====================================================" & vbNewLine)
        logwriter.Write("Pack Name: " & pack & vbNewLine)
        logwriter.Write("OS: " & windowsver & " " & winbit & vbNewLine)
        logwriter.Write("=====================================================" & vbNewLine & vbNewLine)

        logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Log started." & vbNewLine)

        Dim x As String
        x = rand()
        If splash_screen.silent = 1 Then
            rp = "Yes"
        End If
        If rp = "Yes" Then


            logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Creating Restore Point please be patient." & vbNewLine)


            RichTextBox1.Text = ""
            If ComboBox1.SelectedIndex = 0 Then
                RichTextBox1.Text += "Creating Restore Point please be patient."
            ElseIf ComboBox1.SelectedIndex = 1 Then
                RichTextBox1.Text += "Crear punto de restauración por favor espera."
            ElseIf ComboBox1.SelectedIndex = 2 Then
                RichTextBox1.Text += "Создание точки восстановления пожалуйста, подождите."
            ElseIf ComboBox1.SelectedIndex = 3 Then
                RichTextBox1.Text += "Erstellen Wiederherstellungspunkt, bitte warten."
            End If

            Try
                Dim restPoint = GetObject("winmgmts:\\.\root\default:Systemrestore")
                If restPoint IsNot Nothing Then
                    If restPoint.CreateRestorePoint("iPack Restore point", 0, 100) = 0 Then
                        logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Restore Point created successfully" & vbNewLine)
                        If ComboBox1.SelectedIndex = 0 Then
                            RichTextBox1.Text += vbNewLine & "Restore Point created successfully"
                        ElseIf ComboBox1.SelectedIndex = 1 Then
                            RichTextBox1.Text += vbNewLine & "Punto de restauración creado con éxito"

                        ElseIf ComboBox1.SelectedIndex = 2 Then
                            RichTextBox1.Text += vbNewLine & "Точка восстановления успешно создана"

                        ElseIf ComboBox1.SelectedIndex = 3 Then
                            RichTextBox1.Text += vbNewLine & "Wiederherstellungspunkt erfolgreich erstellt"

                        End If
                    Else
                        Dim k As String
                        logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Could not create restore point." & vbNewLine)
                        If ComboBox1.SelectedIndex = 0 Then
                            RichTextBox1.Text += vbNewLine & "Could not create restore point!"


                        ElseIf ComboBox1.SelectedIndex = 1 Then
                            RichTextBox1.Text += vbNewLine & "No se pudo crear punto de restauración."


                        ElseIf ComboBox1.SelectedIndex = 2 Then
                            RichTextBox1.Text += vbNewLine & "Не удалось создать точку восстановления!"


                        ElseIf ComboBox1.SelectedIndex = 3 Then
                            RichTextBox1.Text += vbNewLine & "Wiederherstellungspunkt konnte nicht erstellt werden!"

                        End If
                    End If
                End If

            Catch ex As Exception

            End Try
        End If

       


        Dim iical3 As New ProcessStartInfo
        iical3.FileName = "taskkill.exe"
        iical3.Arguments = "/f /im explorer.exe"
        iical3.WindowStyle = ProcessWindowStyle.Hidden
        Dim ppi2 As New Process()
        ppi2.StartInfo = iical3
        ppi2.Start()
        ppi2.WaitForExit()
        logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Killed explorer.exe" & vbNewLine & vbNewLine)





        Try
            For Each File In My.Computer.FileSystem.GetFiles("Resource Files")
                If File.Contains(".res") Then
                    totalfiles = totalfiles + 1
                    file0 = My.Computer.FileSystem.GetName(File)
                    f = file0.TrimStart
                    pos = f.IndexOf(".res")
                    file1 = f.Remove(pos, 4)

                End If
            Next
        Catch ex As Exception

        End Try

        Dim curntdir As String
        curntdir = (My.Computer.FileSystem.CurrentDirectory)

        For Each File In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory & "\Resource Files")
            If File.Contains(".res") Then
                file0 = My.Computer.FileSystem.GetName(File)
                f = file0.TrimStart
                pos = f.IndexOf(".res")
                file1 = f.Remove(pos, 4)
                '  If file1 = "shellbrd.dll.res" Then
                If My.Computer.FileSystem.FileExists(winDir & "\Branding\ShellBrd\" & file1) Then
                    ' save AclFile

                    saveACL(file1, winDir & "\Branding\ShellBrd")

                    ' Backup

                    backupFile(file1, winDir & "\Branding\ShellBrd", "Resource Files\Backup\Windows", "Resource Files\Patch")

                    'takeownership of original

                    takeOwnership(file1, winDir & "\Branding\ShellBrd")

                    'patch file

                    patchFile(file1, "Resource Files", "Resource Files\Patch")

                    'move back

                    moveBack(file1, "Resource Files\Patch", winDir & "\Branding\ShellBrd", "Windows")
                    logwriter.Write(vbNewLine)
                    status += vbNewLine
                    enable = "true"

                End If

                '  If file1 = "basebrd.dll.res" Then
                If My.Computer.FileSystem.FileExists(winDir & "\Branding\Basebrd\" & file1) Then

                    ' save AclFile
                    saveACL(file1, winDir & "\Branding\Basebrd")

                    ' Backup
                    backupFile(file1, winDir & "\Branding\Basebrd", "Resource Files\Backup\Windows", "Resource Files\Patch")

                    'takeownership of original
                    takeOwnership(file1, winDir & "\Branding\Basebrd")

                    'Patching process
                    patchFile(file1, "Resource Files", "Resource Files\Patch")

                    'move back
                    moveBack(file1, "Resource Files\Patch", winDir & "\Branding\Basebrd", "Windows")

                    logwriter.Write(vbNewLine)
                    status += vbNewLine
                    enable = "true"
                End If

                If My.Computer.FileSystem.FileExists(winDir & "\" & file1) Then

                    ' save AclFile
                    saveACL(file1, winDir)

                    ' Backup
                    backupFile(file1, winDir, "Resource Files\Backup\Windows", "Resource Files\Patch")

                    'takeownership of original
                    takeOwnership(file1, winDir)

                    'Patching process
                    patchFile(file1, "Resource Files", "Resource Files\Patch")

                    'move back
                    moveBack(file1, "Resource Files\Patch", winDir, "Windows")

                    logwriter.Write(vbNewLine)
                    status += vbNewLine
                    enable = "true"


                End If

                If My.Computer.FileSystem.FileExists(winDir & "\System32\" & file1) Then
                    ' save AclFile
                    saveACL(file1, winDir & "\System32")

                    ' Backup
                    backupFile(file1, winDir & "\System32", "Resource Files\Backup\System32", "Resource Files\Patch\System32")

                    'takeownership of original
                    takeOwnership(file1, winDir & "\System32")

                    'Patching process
                    patchFile(file1, "Resource Files", "Resource Files\Patch\System32")

                    'move back
                    moveBack(file1, "Resource Files\Patch\System32", winDir & "\System32", "System32")

                    logwriter.Write(vbNewLine)
                    status += vbNewLine
                    enable = "true"

                End If


                If My.Computer.FileSystem.FileExists(winDir & "\SysWOW64\" & file1) Then

                    ' save AclFile
                    saveACL(file1, winDir & "\SysWOW64")

                    ' Backup
                    backupFile(file1, winDir & "\SysWOW64", "Resource Files\Backup\SysWOW64", "Resource Files\Patch\SysWOW64")

                    'takeownership of original
                    takeOwnership(file1, winDir & "\SysWOW64")

                    'Patching process
                    patchFile(file1, "Resource Files", "Resource Files\Patch\SysWOW64")

                    'move back
                    moveBack(file1, "Resource Files\Patch\SysWOW64", winDir & "\SysWOW64", "SysWOW64")
                    logwriter.Write(vbNewLine)
                    status += vbNewLine
                    enable = "true"
                End If
            End If
        Next
        If My.Computer.FileSystem.DirectoryExists("Resource Files\Program Files") Then
            For Each Dir As String In System.IO.Directory.GetDirectories("Resource Files\Program Files")
                f = Dir.TrimStart
                dirname = IO.Path.GetFileName(Dir)
                For Each File In My.Computer.FileSystem.GetFiles("Resource Files\Program Files\" & dirname)
                    If File.Contains(".res") Then
                        total_in_program_files = total_in_program_files + 1
                        file0 = My.Computer.FileSystem.GetName(File)
                        f = file0.TrimStart
                        pos = f.IndexOf(".res")
                        file1 = f.Remove(pos, 4)

                        '  Else

                        Try

                            path0 = Directory.GetFiles(SystemDir & "\Program Files\" & dirname & "\", file1, SearchOption.AllDirectories)

                            For Each f0 As String In path0
                                f0 = f0.Replace(file1, "")
                                f0 = f0.Replace(SystemDir & "\Program Files\" & dirname & "\", "")
                                'MessageBox.Show(f0)

                                Try
                                    My.Computer.FileSystem.CreateDirectory("Resource Files\Backup\Program Files\" & dirname)
                                Catch ex As Exception

                                End Try

                                ' Backup
                                If ComboBox1.SelectedIndex = 0 Then
                                    status += "Backup " & file1 & vbNewLine
                                    enable = "true"
                                ElseIf ComboBox1.SelectedIndex = 1 Then
                                    status += "Copia de seguridad " & file1 & vbNewLine
                                    enable = "true"
                                ElseIf ComboBox1.SelectedIndex = 2 Then
                                    status += "Резервное копирование " & file1 & vbNewLine
                                    enable = "true"
                                ElseIf ComboBox1.SelectedIndex = 3 Then
                                    status += "Sicherungs " & file1 & vbNewLine
                                    enable = "true"
                                End If
                                Try
                                    My.Computer.FileSystem.CopyFile(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1, "Resource Files\Backup\Program Files\" & dirname & "\" & file1, True)
                                    logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Backup " & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & " to " & My.Computer.FileSystem.CurrentDirectory & "\" & "Resource Files\Backup\Program Files\" & dirname & "\" & file1 & vbNewLine)
                                Catch ex As Exception
                                    logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Failed to backup " & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & vbNewLine)

                                End Try

                                'takeownership of original
                                logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Taking ownership of " & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & vbNewLine)
                                Dim takeown As New ProcessStartInfo
                                takeown.FileName = "CMD"
                                takeown.Arguments = "/c takeown /a /F " & Chr(34) & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & Chr(34) & " && icacls " & Chr(34) & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & Chr(34) & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F" & " && icacls " & Chr(34) & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & Chr(34) & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F" & " && exit"
                                takeown.WindowStyle = ProcessWindowStyle.Hidden
                                Dim pp1 As New Process()
                                pp1.StartInfo = takeown
                                pp1.Start()
                                pp1.WaitForExit()
                                Dim ps As New ProcessStartInfo

                                Dim kil As New ProcessStartInfo
                                kil.FileName = "TASKKILL"
                                kil.Arguments = "/f /im " & Chr(34) & file1 & Chr(34)
                                kil.WindowStyle = ProcessWindowStyle.Hidden
                                Dim pw As New Process()
                                pw.StartInfo = kil
                                pw.Start()
                                pw.WaitForExit()

                                Try
                                    If My.Computer.FileSystem.FileExists(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp") Then
                                        My.Computer.FileSystem.RenameFile(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp", file1 & ".iPold")
                                        My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPold")
                                        Threading.Thread.Sleep(300)
                                    End If

                                    My.Computer.FileSystem.RenameFile(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1, file1 & ".iPtemp")
                                Catch ex As Exception

                                End Try
                                'Patching process
                                logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Patching " & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & vbNewLine)
                                Dim pxs As New ProcessStartInfo
                                pxs.FileName = "Patcher.exe"
                                pxs.Arguments = ("-addoverwrite " & Chr(34) & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp" & Chr(34) & ", " & Chr(34) & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & Chr(34) & ", " & Chr(34) & "Resource Files\Program Files\" & dirname & "\" & file1 & ".res" & Chr(34) & " ,,,")
                                Dim p As New Process()
                                p.EnableRaisingEvents = True    'required if using events
                                p.StartInfo = pxs
                                enable = "true"
                                If ComboBox1.SelectedIndex = 0 Then
                                    status += "Patching " & file1 & vbNewLine
                                    enable = "true"
                                ElseIf ComboBox1.SelectedIndex = 1 Then
                                    status += "Parches " & file1 & vbNewLine
                                    enable = "true"
                                ElseIf ComboBox1.SelectedIndex = 2 Then
                                    status += "Внесение исправлений " & file1 & vbNewLine
                                    enable = "true"
                                ElseIf ComboBox1.SelectedIndex = 3 Then
                                    status += "Patchen " & file1 & vbNewLine
                                    enable = "true"
                                End If
                                p.Start()
                                p.WaitForExit()
                                Try

                                    Dim del As New ProcessStartInfo
                                    del.FileName = "CMD"
                                    del.Arguments = "/c del /f /q " & Chr(34) & "%systemdrive%\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp" & Chr(34) & " && exit"
                                    del.WindowStyle = ProcessWindowStyle.Hidden
                                    Dim ppx As New Process()
                                    ppx.StartInfo = del
                                    ppx.Start()
                                    ppx.WaitForExit()
                                Catch ex As Exception

                                End Try
                                Try
                                    ' Threading.Thread.Sleep(300)
                                    My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPold")
                                    '    My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp")
                                    If My.Computer.FileSystem.FileExists(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp") Then

                                        For i As Integer = 1 To 3
                                            Try
                                                My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp")

                                                Exit For
                                            Catch ex As Exception
                                                If i = 3 Then
                                                    '  AddToLog("Unable to delete file " & FileName & " after 10 tries over 10 seconds. Exception says " & ex.Message)
                                                    If ComboBox1.SelectedIndex = 0 Then
                                                        status += "Scheduling change on reboot for " & file1 & vbNewLine
                                                        enable = "true"
                                                    ElseIf ComboBox1.SelectedIndex = 1 Then
                                                        status += "Programación de cambio en el reinicio para " & file1 & vbNewLine
                                                        enable = "true"
                                                    ElseIf ComboBox1.SelectedIndex = 2 Then
                                                        status += "Планирование изменений при перезагрузке для " & file1 & vbNewLine
                                                        enable = "true"
                                                    ElseIf ComboBox1.SelectedIndex = 3 Then
                                                        status += "Planen Änderung beim Neustart für " & file1 & vbNewLine
                                                        enable = "true"
                                                    End If
                                                    logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Scheduling change on reboot for " & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & vbNewLine)
                                                    MoveFileEx(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)

                                                Else
                                                    Thread.Sleep(1000)   ' same as Threading.Thread.Sleep(1000)
                                                End If
                                            End Try
                                        Next

                                    End If



                                Catch ex As Exception

                                End Try
                                logwriter.Write(vbNewLine)
                                status += vbNewLine
                                enable = "true"


                            Next

                        Catch ex As Exception
                        End Try


                        If My.Computer.FileSystem.DirectoryExists(SystemDir & "\Program Files (x86)") Then

                            Try

                                path0 = Directory.GetFiles(SystemDir & "\Program Files (x86)\" & dirname & "\", file1, SearchOption.AllDirectories)
                                ' MessageBox.Show(path0.ToString)
                                For Each f0 As String In path0
                                    f0 = f0.Replace(file1, "")
                                    f0 = f0.Replace(SystemDir & "\Program Files (x86)\" & dirname & "\", "")
                                    Try
                                        My.Computer.FileSystem.CreateDirectory("Resource Files\Backup\Program Files (x86)\" & dirname)
                                    Catch ex As Exception

                                    End Try

                                    ' Backup
                                    If ComboBox1.SelectedIndex = 0 Then
                                        status += "Backup " & file1 & " x86" & vbNewLine
                                        enable = "true"
                                    ElseIf ComboBox1.SelectedIndex = 1 Then
                                        status += "Copia de seguridad " & file1 & " x86" & vbNewLine
                                        enable = "true"
                                    ElseIf ComboBox1.SelectedIndex = 2 Then
                                        status += "Резервное копирование " & file1 & " x86" & vbNewLine
                                        enable = "true"
                                    ElseIf ComboBox1.SelectedIndex = 3 Then
                                        status += "Sicherungs " & file1 & " x86" & vbNewLine
                                        enable = "true"
                                    End If
                                    Try
                                        My.Computer.FileSystem.CopyFile(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1, "Resource Files\Backup\Program Files (x86)\" & dirname & "\" & file1, True)
                                        logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Backup " & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & " to " & My.Computer.FileSystem.CurrentDirectory & "\" & "Resource Files\Backup\Program Files (x86)\" & dirname & "\" & file1 & vbNewLine)
                                    Catch ex As Exception
                                        logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Failed to backup " & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & vbNewLine)
                                    End Try


                                    'takeownership of original
                                    logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Taking ownership of " & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & vbNewLine)

                                    Dim takeown As New ProcessStartInfo
                                    takeown.FileName = "CMD"
                                    takeown.Arguments = "/c takeown /a /F " & Chr(34) & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & Chr(34) & " && icacls " & Chr(34) & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & Chr(34) & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F" & " && icacls " & Chr(34) & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & Chr(34) & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F" & " && exit"
                                    takeown.WindowStyle = ProcessWindowStyle.Hidden
                                    Dim pp1 As New Process()
                                    pp1.StartInfo = takeown
                                    pp1.Start()
                                    pp1.WaitForExit()
                                    Dim ps As New ProcessStartInfo

                                    Dim kil As New ProcessStartInfo
                                    kil.FileName = "TASKKILL"
                                    kil.Arguments = "/f /im " & Chr(34) & file1 & Chr(34)
                                    kil.WindowStyle = ProcessWindowStyle.Hidden
                                    Dim pw As New Process()
                                    pw.StartInfo = kil
                                    pw.Start()
                                    pw.WaitForExit()

                                    Try
                                        If My.Computer.FileSystem.FileExists(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp") Then
                                            My.Computer.FileSystem.RenameFile(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp", file1 & ".iPold")
                                            My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPold")
                                            '  Threading.Thread.Sleep(300)
                                        End If

                                        My.Computer.FileSystem.RenameFile(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1, file1 & ".iPtemp")
                                    Catch ex As Exception

                                    End Try
                                    'Patching process
                                    logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Patching " & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & vbNewLine)
                                    Dim pas As New ProcessStartInfo
                                    pas.FileName = "Patcher.exe"
                                    pas.Arguments = ("-addoverwrite " & Chr(34) & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp" & Chr(34) & ", " & Chr(34) & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & Chr(34) & ", " & Chr(34) & "Resource Files\Program Files\" & dirname & "\" & file1 & ".res" & Chr(34) & " ,,,")
                                    Dim p As New Process()
                                    p.EnableRaisingEvents = True    'required if using events
                                    p.StartInfo = pas

                                    enable = "true"
                                    If ComboBox1.SelectedIndex = 0 Then
                                        status += "Patching " & file1 & " x86" & vbNewLine
                                        enable = "true"
                                    ElseIf ComboBox1.SelectedIndex = 1 Then
                                        status += "Parches " & file1 & " x86" & vbNewLine
                                        enable = "true"
                                    ElseIf ComboBox1.SelectedIndex = 2 Then
                                        status += "Внесение исправлений " & file1 & " x86" & vbNewLine
                                        enable = "true"
                                    ElseIf ComboBox1.SelectedIndex = 3 Then
                                        status += "Patchen " & file1 & " x86" & vbNewLine
                                        enable = "true"
                                    End If
                                    p.Start()
                                    p.WaitForExit()
                                    Try


                                        Dim del As New ProcessStartInfo
                                        del.FileName = "CMD"
                                        del.Arguments = "/c del /f /q " & Chr(34) & "%systemdrive%\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp" & Chr(34) & " && exit"
                                        del.WindowStyle = ProcessWindowStyle.Hidden
                                        Dim ppx As New Process()
                                        ppx.StartInfo = del
                                        ppx.Start()
                                        ppx.WaitForExit()
                                    Catch ex As Exception

                                    End Try
                                    Try
                                        'Threading.Thread.Sleep(300)
                                        My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPold")
                                        '  My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp")
                                        If My.Computer.FileSystem.FileExists(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp") Then
                                            For i As Integer = 1 To 3
                                                Try
                                                    My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp")

                                                    Exit For
                                                Catch ex As Exception
                                                    If i = 3 Then
                                                        '  AddToLog("Unable to delete file " & FileName & " after 10 tries over 10 seconds. Exception says " & ex.Message)
                                                        If ComboBox1.SelectedIndex = 0 Then
                                                            status += "Scheduling change on reboot for " & file1 & " x86" & vbNewLine
                                                            enable = "true"
                                                        ElseIf ComboBox1.SelectedIndex = 1 Then
                                                            status += "Programación de cambio en el reinicio para " & file1 & " x86" & vbNewLine
                                                            enable = "true"
                                                        ElseIf ComboBox1.SelectedIndex = 2 Then
                                                            status += "Планирование изменений при перезагрузке для " & file1 & " x86" & vbNewLine
                                                            enable = "true"
                                                        ElseIf ComboBox1.SelectedIndex = 3 Then
                                                            status += "Planen Änderung beim Neustart für " & file1 & " x86" & vbNewLine
                                                            enable = "true"
                                                        End If

                                                        logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Scheduling change on reboot for " & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & vbNewLine)
                                                        MoveFileEx(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                                                    Else
                                                        Thread.Sleep(1000)   ' same as Threading.Thread.Sleep(1000)
                                                    End If
                                                End Try
                                            Next
                                        End If

                                    Catch ex As Exception


                                    End Try
                                    logwriter.Write(vbNewLine)
                                    status += vbNewLine
                                    enable = "true"
                                Next

                            Catch ex As Exception

                            End Try

                        End If
                        '  End If
                    End If
                Next
            Next
        End If

        Dim commandWriter As New System.IO.StreamWriter(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\reload.bat")
        commandWriter.Write("set iconcache=%localappdata%\IconCache.db")
        commandWriter.Write(vbNewLine)
        commandWriter.Write("TAKEOWN /f " & Chr(34) & "%iconcache%" & Chr(34) & " && ICACLS " & Chr(34) & "%iconcache%" & Chr(34) & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F /T" & " && ICACLS " & Chr(34) & "%iconcache%" & Chr(34) & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F /T")
        commandWriter.Write(vbNewLine)
        commandWriter.Write("del " & Chr(34) & "%iconcache%" & Chr(34) & "/A")
        commandWriter.Write(vbNewLine)
        commandWriter.Write("set thumbcache=" & SystemDir & "\Users\%username%\AppData\Local\Microsoft\Windows\Explorer\*.db*")
        commandWriter.Write(vbNewLine)
        commandWriter.Write("TAKEOWN /f " & Chr(34) & SystemDir & "\Users\%username%\AppData\Local\Microsoft\Windows\Explorer" & Chr(34) & " /r /d y && ICACLS " & Chr(34) & SystemDir & "\Users\%username%\AppData\Local\Microsoft\Windows\Explorer" & Chr(34) & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F /T && ICACLS " & Chr(34) & SystemDir & "\Users\%username%\AppData\Local\Microsoft\Windows\Explorer" & Chr(34) & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F /T")
        commandWriter.Write(vbNewLine)
        commandWriter.Write("del " & Chr(34) & "%thumbcache%" & Chr(34) & "/A")
        commandWriter.Write(vbNewLine)
        commandWriter.Write("start %windir%\explorer.exe")
        commandWriter.Write(vbNewLine)
        commandWriter.Write("DEL " & Chr(34) & "%~f0" & Chr(34))
        commandWriter.Close()
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        If ComboBox1.SelectedIndex = 0 Then
            status += "Done." & vbNewLine
            enable = "true"
        ElseIf ComboBox1.SelectedIndex = 1 Then
            status += "¡Hecho!" & vbNewLine
            enable = "true"
        ElseIf ComboBox1.SelectedIndex = 2 Then
            status += "Готово!" & vbNewLine
            enable = "true"
        ElseIf ComboBox1.SelectedIndex = 3 Then
            status += "Fertig!" & vbNewLine
            enable = "true"
        End If
        logwriter.Write("[" & DateTime.Now.ToString() & "] " & "Done." & vbNewLine)
        logwriter.Write("=====================================================" & vbNewLine)
        logwriter.Close()
       





        Try
            Dim launch As New ProcessStartInfo()
            launch.WindowStyle = ProcessWindowStyle.Hidden
            launch.UseShellExecute = True
            launch.FileName = ("Resource Files\reload.bat")
            launch.WorkingDirectory = ""
            Process.Start(launch)
            If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x) Then
                If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\Windows") Then

                    MoveFileEx(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\Windows", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)

                End If
                If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\System32") Then

                    MoveFileEx(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\System32", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)

                End If
                If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\SysWOW64") Then


                    MoveFileEx(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\SysWOW64", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                End If
            End If

            Dim schedule As New ProcessStartInfo

            If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x) Then
                MoveFileEx(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x, Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
            End If



        Catch ex As System.ComponentModel.Win32Exception
            Return


        End Try

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        RichTextBox1.Text = status
        enable = "false"
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If enable = "true" Then
            Timer1.Start()
        Else
            Timer1.Stop()

        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        If ComboBox1.SelectedIndex = 0 Then
            CheckBox1.Text = "I accept the agreement."
            Button1.Text = "Next >"
            Button2.Text = "Cancel"
            Label1.Text = "Package Name:"
            Label2.Text = "Author :"
            Label5.Text = "Overall progress:"
            Button3.Text = "Finish"
            Label9.Text = "Installed"
            CheckBox2.Text = "Reboot Now"
            Label3.Location = New Point(Label1.Width + 15, Label3.Location.Y)
            Label4.Location = New Point(Label2.Width + 15, Label4.Location.Y)
            showfiles.RadioButton1.Text = "Patch all files."
            showfiles.RadioButton2.Text = "Patch only selected files."
            showfiles.Label1.Text = "Please select an option above and press Next."


        End If
        If ComboBox1.SelectedIndex = 1 Then
            CheckBox1.Text = "Estoy de acuerdo."
            Button1.Text = "Siguiente >"
            Button2.Text = "Cancelar"
            Label1.Text = "Nombre del paquete:"
            Label2.Text = "Autor:"
            Label5.Text = "Progreso en general:"
            Button3.Text = "Acabado"
            Label9.Text = "Instalado"
            CheckBox2.Text = "Reiniciar ahora"
            Label3.Location = New Point(Label1.Width + 15, Label3.Location.Y)
            Label4.Location = New Point(Label2.Width + 15, Label4.Location.Y)
            showfiles.RadioButton1.Text = "Parche todos "
            showfiles.RadioButton2.Text = "Patch seleccionado"
            showfiles.Label1.Text = "Por favor, seleccione una opción más arriba y pulse Siguiente."
        End If
        If ComboBox1.SelectedIndex = 2 Then
            CheckBox1.Text = "Я согласен с условиями."
            Button1.Text = "Следующая >"
            Button2.Text = "Отменить"
            Label1.Text = "Название пакета:"
            Label2.Text = "Автор:"
            Label5.Text = "Общий прогресс:"
            Button3.Text = "Отделка"
            Label9.Text = "Установленная"
            CheckBox2.Text = "Перезагрузка Теперь"
            Label3.Location = New Point(Label1.Width + 15, Label3.Location.Y)
            Label4.Location = New Point(Label2.Width + 15, Label4.Location.Y)
            showfiles.RadioButton1.Text = "Патч все файлы"
            showfiles.RadioButton2.Text = "Патч выбран"
            showfiles.Label1.Text = "Пожалуйста, выберите опцию выше и нажмите Далее."
        End If
        If ComboBox1.SelectedIndex = 3 Then
            Label9.Text = "Installiert"
            CheckBox1.Text = "Ich stimme zu."
            Button1.Text = "Nächste >"
            Button2.Text = "Stornieren"
            Label1.Text = "Paketname:"
            Label2.Text = "Autor:"
            Label5.Text = "Gesamtfortschritt:"
            Button3.Text = "Fertig stellen"
            CheckBox2.Text = "Neu starten"
            Label3.Location = New Point(Label1.Width + 15, Label3.Location.Y)
            Label4.Location = New Point(Label2.Width + 15, Label4.Location.Y)
            showfiles.RadioButton1.Text = "Patch alle Dateien"
            showfiles.RadioButton2.Text = "Patch ausgewählt"
            showfiles.Label1.Text = "Bitte wählen Sie eine Option über und drücken Sie auf Weiter."
        End If
        Try
            langthemed.Label1.Text = ComboBox1.SelectedItem.ToString

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RichTextBox2_LinkClicked(sender As Object, e As LinkClickedEventArgs)
        System.Diagnostics.Process.Start(e.LinkText)
    End Sub



    Private Sub RichTextBox2_LinkClicked1(sender As Object, e As LinkClickedEventArgs) Handles RichTextBox2.LinkClicked
        System.Diagnostics.Process.Start(e.LinkText)
    End Sub




    Private Sub CheckBox1_Paint(sender As Object, e As PaintEventArgs) Handles CheckBox1.Paint

        Try

            If Not cbChecked = Nothing And CheckBox1.Checked = True Then
                e.Graphics.DrawImageUnscaledAndClipped((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbChecked)), New Rectangle(0, 2, (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbChecked).Width), (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbChecked).Height)))
            ElseIf Not cbUnchecked = Nothing And CheckBox1.Checked = False Then
                ' e.Graphics.DrawImageUnscaled((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbUnchecked)), 0, 0)
                e.Graphics.DrawImageUnscaledAndClipped((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbUnchecked)), New Rectangle(0, 2, (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbUnchecked).Width), (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbUnchecked).Height)))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckBox2_Paint(sender As Object, e As PaintEventArgs) Handles CheckBox2.Paint
        Try

            If Not cbChecked = Nothing And CheckBox2.Checked = True Then
                e.Graphics.DrawImageUnscaledAndClipped((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbChecked)), New Rectangle(0, 2, (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbChecked).Width), (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbChecked).Height)))
            ElseIf Not cbUnchecked = Nothing And CheckBox2.Checked = False Then
                e.Graphics.DrawImageUnscaledAndClipped((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbUnchecked)), New Rectangle(0, 2, (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbUnchecked).Width), (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & cbUnchecked).Height)))
            End If
        Catch ex As Exception

        End Try
    End Sub

   
End Class
