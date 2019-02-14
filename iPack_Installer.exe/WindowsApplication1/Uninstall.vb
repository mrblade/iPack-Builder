Imports Microsoft.Win32
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Threading
Imports System.Text

Public Class Uninstall

    Public i As Integer = 0
    Dim imgThumb2 As Image = Nothing
    Dim file0, file1, f, ff, dirname As String
    Dim SystemDir As String = Environment.GetEnvironmentVariable("SystemDrive")
    Dim total As Integer
    Dim loc = My.Computer.FileSystem.CurrentDirectory
    Dim list() As String
    Dim winDir As String = Environment.GetEnvironmentVariable("windir")
    Dim major As Integer = Environment.OSVersion.Version.Major
    Dim minor As Integer = Environment.OSVersion.Version.Minor
    Dim windowsver As String
    Dim path0(), f0 As String
    Dim enable, winbit As String
    Dim status() As Char
    Dim rebootneed As String
    Dim rng As Random = New Random

    Private Function restorePermission(ByVal name As String, ByVal patchFolder As String, ByVal aclFolder As String)
        Dim ical3 As New ProcessStartInfo
        ical3.FileName = "CMD"
        ical3.Arguments = "/c icacls " & Chr(34) & patchFolder & "\" & name & Chr(34) & " /setowner " & Chr(34) & "NT Service\TrustedInstaller" & Chr(34) & " /T /C" & " && icacls " & Chr(34) & patchFolder & Chr(34) & " /restore " & Chr(34) & aclFolder & "\" & name & ".AclFile" & Chr(34) & " && exit"
        ical3.WindowStyle = ProcessWindowStyle.Hidden
        Dim pp2 As New Process()
        pp2.StartInfo = ical3
        pp2.Start()
        pp2.WaitForExit()
    End Function
    Public Function rand() As String

        Dim sb As New StringBuilder

        ' Selection of pure numbers sequence or mixed one
        Dim pureNumbers = rng.Next(1, 11)
        If pureNumbers < 7 Then
            ' Generate a sequence of only digits
            Dim number As Integer = rng.Next(1, 1000000)
            Dim digits As String = number.ToString("000000")
            For i As Integer = 1 To 18
                Dim idx As Integer = rng.Next(0, digits.Length)
                sb.Append(digits.Substring(idx, 1))
            Next
        Else
            ' Generate a sequence of digits and letters 
            Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
            For i As Integer = 1 To 18
                Dim idx As Integer = rng.Next(0, 36)
                sb.Append(s.Substring(idx, 1))
            Next
        End If
        Return sb.ToString()
    End Function

    Public Sub changelocation()

        Try
            Button2.Location = New Point(Form1.Button1.Location.X, Form1.Button1.Location.Y)
            Button1.Location = New Point(Form1.Button2.Location.X, Form1.Button2.Location.Y)
            Button1.Size = Form1.Button1.Size
            Button2.Size = Form1.Button2.Size
            ComboBox1.Location = New Point(TextBox1.Left, Form1.ComboBox1.Location.Y)
            PictureBox1.Location = New Point(TextBox1.Left, 91)
            Label1.Location = New Point(PictureBox1.Right, 90)
            RadioButton1.Left = Form1.ProgressBar1.Left
            RadioButton2.Left = Form1.ProgressBar1.Left
            RadioButton1.Top = Label1.Bottom + 20
            RadioButton2.Top = RadioButton1.Bottom + 20
            Label6.Top = RadioButton2.Bottom + 20
            Label6.Left = Form1.ProgressBar1.Left
            TextBox1.Width = Form1.ProgressBar1.Width
            TextBox1.Top = Label6.Bottom
            TextBox1.Left = Form1.ProgressBar1.Left
            TextBox1.Height = (Form1.line1.Top - Label6.Bottom) - 3
            Panel2.Width = Form1.ProgressBar1.Width
            Panel3.Width = Panel2.Width - 2
            RichTextBox1.Width = Panel3.Width - 2
            Panel2.Left = Form1.ProgressBar1.Left
            Panel3.Left = 1
            RichTextBox1.Left = 1
            Panel2.Top = Label2.Bottom + 30

            Panel2.Height = (Form1.line1.Top - Label2.Bottom) - 55
            Panel3.Height = Panel2.Height - 2
            RichTextBox1.Height = Panel2.Height - 4


            ProgressBar1.Size = Form1.ProgressBar1.Size
            ProgressBar1.Left = Form1.ProgressBar1.Left
            ProgressBar1.Top = Form1.ProgressBar1.Top
            Label2.Left = ProgressBar1.Left
            Label2.Top = 120
            Label3.Top = 120
            Label3.Left = Label2.Right
            Panel1.Location = New Point(0, 0)
         
        Catch ex As Exception

        End Try

    End Sub

    Friend Enum MoveFileFlags
        MOVEFILE_REPLACE_EXISTING = 1
        MOVEFILE_COPY_ALLOWED = 2
        MOVEFILE_DELAY_UNTIL_REBOOT = &H4
        MOVEFILE_WRITE_THROUGH = 8
    End Enum
    <System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint:="MoveFileEx")> _
    Friend Shared Function MoveFileEx(lpExistingFileName As String, lpNewFileName As String, dwFlags As MoveFileFlags) As Boolean
    End Function

    Private Sub Uninstall_Enter(sender As Object, e As EventArgs) Handles Me.Enter
        i = 0
    End Sub

   
    Private Sub Uninstall_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        i = 1  'for activating form1 on unistaller
        Form1.Refresh()

        changelocation()
       
        If Not Form1.drop1 = Nothing And Not Form1.drop2 = Nothing Then
            '  Me.Controls.Add(langtheme)
            Try
                ' langtheme.Label1.ForeColor = ColorTranslator.FromHtml(Form1.droptxtcolor)
            Catch ex As Exception

            End Try

        End If

      

        ' Dim cpuID As String = _
        'My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\0", _
        '     "Identifier", "n/a")
        '
        'Get all chars from the beginning of the string until the first space is detected.
        ' cpuID = cpuID.Substring(0, cpuID.IndexOf(" "))
        ' If LCase(cpuID).Contains("amd64") OrElse LCase(cpuID).Contains("intel64") OrElse LCase(cpuID).Contains("64") Then
        If IntPtr.Size = 8 Then
            winbit = "x64"
            '   sysnative = "\Sysnative\"
        Else
            winbit = "x86"
            '  sysnative = "\System32\"

        End If
        RadioButton1_CheckedChanged(Nothing, Nothing)

        Form1.ComboBox1.Hide()

        Form1.RichTextBox2.Hide()
        Try

            Form1.button.changenext(1)

        Catch ex As Exception

        End Try
        rebootneed = "No"
        If splash_screen.lang > 3 Then
            ComboBox1.SelectedIndex = 0
        Else
            ComboBox1.SelectedIndex = splash_screen.lang
        End If







        Try
            For Each ff In Directory.GetFiles(winDir & "\", "*.iPtemp2", SearchOption.TopDirectoryOnly)
                File.Delete(ff)
            Next
            For Each ff In Directory.GetFiles(winDir & "\System32", "*.iPtemp2", SearchOption.TopDirectoryOnly)
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
            For Each ff In Directory.GetFiles(winDir & "\System32", "*.iPtemp", SearchOption.TopDirectoryOnly)
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
            For Each ff In Directory.GetFiles(winDir & "\System32", "*.iPold2", SearchOption.TopDirectoryOnly)
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
            For Each ff In Directory.GetFiles(winDir & "\System32", "*.iPold", SearchOption.TopDirectoryOnly)
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

        total = 0

        '  PictureBox1.Location = New Point(Label1.Location.X - 34, Label1.Location.Y - 3)
        Try
            Dim image2 As Image = Nothing
            image2 = Image.FromFile("Setup files-iPack\header.png")
            imgThumb2 = image2.GetThumbnailImage(Form1.TextBox2.Width + 6, image2.Height, Nothing, New IntPtr())
            Me.Refresh()
        Catch ex As Exception
        End Try
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


        Form1.Opacity = 100
        'Form1.i = 0
    End Sub

    Private Sub Uninstall_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter

    End Sub

   

    Private Sub Uninstall_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
        If Not imgThumb2 Is Nothing Then
            e.Graphics.DrawImage(imgThumb2, 0, 0, imgThumb2.Width, imgThumb2.Height)
        End If
        If i = 1 Then
            Form1.Activate()

        End If

    End Sub

    Public Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click


        Application.Exit()
    End Sub

    Public Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Form1.button.Hide()

        If ComboBox1.SelectedIndex = 0 Then
            RichTextBox1.Text = "Initializing. . ."
        ElseIf ComboBox1.SelectedIndex = 1 Then
            RichTextBox1.Text = "Inicializando. . ."
        ElseIf ComboBox1.SelectedIndex = 2 Then
            RichTextBox1.Text = "Инициализация. . ."
        ElseIf ComboBox1.SelectedIndex = 3 Then
            RichTextBox1.Text = "Initialisierung. . ."
        End If

        'Form1.ControlBox = False
        'uninstall code
        TextBox1.Visible = False
        Panel1.Visible = True
        Panel3.Visible = True
        Panel2.Visible = True
        '   Panel2.BackColor = Color.DimGray
        Label3.Text = Form1.pack
        RichTextBox1.Visible = True
        Label6.Hide()
        ComboBox1.Hide()
        Try
            ' langtheme.Hide()
            Form1.langthemed.Hide()

        Catch ex As Exception

        End Try

        If RadioButton2.Checked = True Then
            'sfc scannow
            Dim objWriter As New System.IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & Form1.pack & "_sfc.bat")
            objWriter.Write("@ echo off")
            objWriter.Write(vbNewLine)
            objWriter.Write("cd %temp%")
            objWriter.Write(vbNewLine)
            '   objWriter.Write("Title SFC /SCANNOW")
            objWriter.Write(vbNewLine)
            objWriter.Write("tasklist /FI " & Chr(34) & "IMAGENAME eq " & Form1.appname & "" & Chr(34) & " 2>NUL | find /I /N " & Chr(34) & Form1.appname & Chr(34) & ">NUL")
            objWriter.Write(vbNewLine)
            objWriter.Write("taskkill /f /im " & Form1.appname)
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
            objWriter.Write("cls")
            objWriter.Write(vbNewLine)
            ' objWriter.Write("sfc /scannow")
            objWriter.Write(vbNewLine)
            objWriter.Write("DEL " & Chr(34) & "%~f0" & Chr(34))
            objWriter.Close()
            Try
                Dim regKey As RegistryKey
                regKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall\", True)
                regKey.DeleteSubKey(Form1.pack, True)
                regKey.Close()
            Catch ex As Exception

            End Try


            Dim sfcc As New ProcessStartInfo()
            sfcc.FileName = (winDir & "\System32\" & "cmd.exe")
            sfcc.Arguments = ("/c sfc /scannow")
            sfcc.WindowStyle = ProcessWindowStyle.Normal
            Process.Start(sfcc)



            Dim laun As New ProcessStartInfo
            laun.FileName = (My.Computer.FileSystem.SpecialDirectories.Temp & "\" & Form1.pack & "_sfc.bat")
            laun.WindowStyle = ProcessWindowStyle.Hidden
            MoveFileEx(My.Computer.FileSystem.CurrentDirectory, Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
            Process.Start(laun)

            Application.Exit()
        Else

            BackgroundWorker1.RunWorkerAsync()

        End If




        Try
            For Each Dir As String In System.IO.Directory.GetDirectories("Resource Files\Backup\Program Files")
                f = Dir.TrimStart
                dirname = IO.Path.GetFileName(Dir)
                For Each File In My.Computer.FileSystem.GetFiles("Resource Files\Backup\Program Files\" & dirname)
                    total = total + 1
                Next
            Next
            For Each Dir As String In System.IO.Directory.GetDirectories("Resource Files\Backup\Program Files (x86)")
                f = Dir.TrimStart
                dirname = IO.Path.GetFileName(Dir)

                For Each File In My.Computer.FileSystem.GetFiles("Resource Files\Backup\Program Files (x86)\" & dirname)

                    total = total + 1
                Next
            Next

            For Each File In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\System32")

                total = total + 1
            Next
            For Each File In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\SysWOW64")

                total = total + 1
            Next
            For Each File In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\Windows")

                total = total + 1
            Next

        Catch ex As Exception

        End Try
        ProgressBar1.Maximum = total
        ProgressBar1.Minimum = 0

    End Sub




    Private Sub RichTextBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RichTextBox1.TextChanged
        RichTextBox1.SelectionStart = RichTextBox1.Text.Length
        RichTextBox1.ScrollToCaret()
        Try
            System.Threading.Thread.Sleep(100)


            If ComboBox1.SelectedIndex = 0 Then
                ProgressBar1.Value = Regex.Matches(RichTextBox1.Text, "Restoring").Count()
            ElseIf ComboBox1.SelectedIndex = 1 Then
                ProgressBar1.Value = Regex.Matches(RichTextBox1.Text, "Restaurar").Count()
            ElseIf ComboBox1.SelectedIndex = 2 Then
                ProgressBar1.Value = Regex.Matches(RichTextBox1.Text, "восстановить").Count()
            ElseIf ComboBox1.SelectedIndex = 3 Then
                ProgressBar1.Value = Regex.Matches(RichTextBox1.Text, "Wiederherstellen").Count()
            End If

        Catch ex As Exception
        End Try

        If RichTextBox1.Text.Contains("Done.") = True Or RichTextBox1.Text.Contains("¡Hecho!") = True Or RichTextBox1.Text.Contains("Готово!") = True Or RichTextBox1.Text.Contains("Fertig!") = True Then
            ProgressBar1.Value = ProgressBar1.Maximum
            Try
                'My.Computer.FileSystem.DeleteFile(winDir & sysnative & Form1.pack & "_Uninstall.bat")
                Dim regKey As RegistryKey
                regKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall\", True)
                regKey.DeleteSubKey(Form1.pack, True)
                regKey.Close()


            Catch ex As Exception

            End Try
            Dim objWriter As New System.IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & Form1.pack & ".bat")

            objWriter.Write("tasklist /FI " & Chr(34) & "IMAGENAME eq " & Form1.appname & Chr(34) & " 2>NUL | find /I /N " & Chr(34) & Form1.appname & Chr(34) & ">NUL")
            objWriter.Write(vbNewLine)
            objWriter.Write("taskkill /f /im " & Form1.appname)
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
            objWriter.Close()
            Form1.Opacity = 0
            'If RichTextBox1.Text.Contains("Scheduling") Then
            Dim r As String
            If RichTextBox1.Text.Contains("Scheduling") Or RichTextBox1.Text.Contains("Programación") Or RichTextBox1.Text.Contains("Планирование") Or RichTextBox1.Text.Contains("Planen") Or rebootneed = "Yes" Then

                If ComboBox1.SelectedIndex = 0 Then
                    r = MessageBox.Show("A reboot is required.Reboot now?", "Uninstall Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                ElseIf ComboBox1.SelectedIndex = 1 Then
                    r = MessageBox.Show("Un reinicio se required.Reboot ahora?", "Desinstalación completa", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                ElseIf ComboBox1.SelectedIndex = 2 Then
                    r = MessageBox.Show("Вы уверены, что вы хотите бросить курить?", "Удалить завершить", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                ElseIf ComboBox1.SelectedIndex = 3 Then
                    r = MessageBox.Show("Sind Sie sicher, dass Sie aufhören möchten?", "Deinstallation komplette", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

                End If

                If r = DialogResult.Yes Then
                    Dim reader As New IO.StreamReader(My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & Form1.pack & ".bat")
                    Dim contents As String = reader.ReadToEnd()
                    reader.Close()
                    contents += "shutdown /f /r" & vbNewLine & "DEL " & Chr(34) & "%~f0" & Chr(34)
                    Dim writer As New IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & Form1.pack & ".bat")
                    writer.Write(contents)
                    writer.Close()
                    MoveFileEx(My.Computer.FileSystem.CurrentDirectory, Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                    Threading.Thread.Sleep(500)
                    Dim launch As New ProcessStartInfo()
                    launch.WindowStyle = ProcessWindowStyle.Hidden
                    launch.UseShellExecute = True
                    launch.FileName = (My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & Form1.pack & ".bat")
                    launch.WorkingDirectory = ""
                    Process.Start(launch)
                    '    Application.Exit()
                Else
                    ' MessageBox.Show("without restart")
                    Dim reader As New IO.StreamReader(My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & Form1.pack & ".bat")
                    Dim contents As String = reader.ReadToEnd()
                    reader.Close()
                    contents += "DEL " & Chr(34) & "%~f0" & Chr(34)
                    Dim writer As New IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & Form1.pack & ".bat")
                    writer.Write(contents)
                    writer.Close()
                    MoveFileEx(My.Computer.FileSystem.CurrentDirectory, Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                    Threading.Thread.Sleep(500)
                    Dim launch As New ProcessStartInfo()
                    launch.WindowStyle = ProcessWindowStyle.Hidden
                    launch.UseShellExecute = True
                    launch.FileName = (My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & Form1.pack & ".bat")
                    launch.WorkingDirectory = ""
                    Process.Start(launch)
                    ' Application.Exit()
                End If
            Else

                If ComboBox1.SelectedIndex = 0 Then
                    MessageBox.Show(Form1.pack & " uninstall Complete.", "Uninstall Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ElseIf ComboBox1.SelectedIndex = 1 Then
                    MessageBox.Show(Form1.pack & " desinstalación completa.", "Desinstalación completa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ElseIf ComboBox1.SelectedIndex = 2 Then
                    MessageBox.Show(Form1.pack & " Удалить завершить.", "Удалить завершить", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ElseIf ComboBox1.SelectedIndex = 3 Then
                    MessageBox.Show(Form1.pack & " deinstallation komplette.", "Deinstallation komplette", MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If
                Dim reader As New IO.StreamReader(My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & Form1.pack & ".bat")
                Dim contents As String = reader.ReadToEnd()
                reader.Close()
                contents += "DEL " & Chr(34) & "%~f0" & Chr(34)
                Dim writer As New IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & Form1.pack & ".bat")
                writer.Write(contents)
                writer.Close()
                MoveFileEx(My.Computer.FileSystem.CurrentDirectory, Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                Threading.Thread.Sleep(500)
                Dim launch As New ProcessStartInfo()
                launch.WindowStyle = ProcessWindowStyle.Hidden
                launch.UseShellExecute = True
                launch.FileName = (My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & Form1.pack & ".bat")
                launch.WorkingDirectory = ""
                Process.Start(launch)
                '   Application.Exit()
            End If
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            TextBox1.Text = ""

            Try
                For Each Dir As String In System.IO.Directory.GetDirectories("Resource Files\Backup\Program Files")
                    f = Dir.TrimStart
                    dirname = IO.Path.GetFileName(Dir)

                    For Each File In My.Computer.FileSystem.GetFiles("Resource Files\Backup\Program Files\" & dirname)
                        file0 = My.Computer.FileSystem.GetName(File)
                        file1 = file0.TrimStart

                        ' If My.Computer.FileSystem.FileExists(SystemDir & "\Program Files\" & dirname & "\" & file1) Then
                        TextBox1.Text += file1 & vbNewLine
                        'End If
                    Next
                Next
                For Each Dir As String In System.IO.Directory.GetDirectories("Resource Files\Backup\Program Files (x86)")
                    f = Dir.TrimStart
                    dirname = IO.Path.GetFileName(Dir)

                    For Each File In My.Computer.FileSystem.GetFiles("Resource Files\Backup\Program Files (x86)\" & dirname)
                        file0 = My.Computer.FileSystem.GetName(File)
                        file1 = file0.TrimStart

                        ' If My.Computer.FileSystem.FileExists(SystemDir & "\Program Files (x86)\" & dirname & "\" & file1) Then
                        TextBox1.Text += file1 & " x86" & vbNewLine
                        ' End If
                    Next
                Next
                For Each file In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\System32")
                    file0 = My.Computer.FileSystem.GetName(file)
                    file1 = file0.TrimStart

                    If My.Computer.FileSystem.FileExists(winDir & "\System32\" & file1) Then
                        TextBox1.Text += file1 & vbNewLine
                    End If


                Next
                For Each file In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\SysWOW64")
                    file0 = My.Computer.FileSystem.GetName(file)
                    file1 = file0.TrimStart
                    If My.Computer.FileSystem.FileExists(winDir & "\SysWOW64\" & file1) Then
                        TextBox1.Text += file1 & " x86" & vbNewLine
                    End If
                Next
                For Each file In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\Windows")
                    file0 = My.Computer.FileSystem.GetName(file)
                    file1 = file0.TrimStart
                    If My.Computer.FileSystem.FileExists(winDir & "\" & file1) Then
                        TextBox1.Text += file1 & vbNewLine
                    End If
                    If file1 = "shellbrd.dll" Then
                        TextBox1.Text += file1 & vbNewLine
                    End If
                    If file1 = "basebrd.dll" Then
                        TextBox1.Text += file1 & vbNewLine
                    End If
                Next

                TextBox1.Update()
                TextBox1.Refresh()



            Catch ex As Exception

            End Try
        End If

    End Sub




    Private Sub RadioButton2_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButton2.CheckedChanged

        If RadioButton2.Checked = True Then


            If ComboBox1.SelectedIndex = 0 Then
                TextBox1.Text = "This will restore windows default icons (sfc /scannow)"
            End If
            If ComboBox1.SelectedIndex = 1 Then
                TextBox1.Text = "Esto restaura los iconos por defecto de windows (sfc/scannow)"
            End If
            If ComboBox1.SelectedIndex = 2 Then
                TextBox1.Text = "Это позволит восстановить windows по умолчанию иконки (sfc/scannow)"
            End If
            If ComboBox1.SelectedIndex = 3 Then
                TextBox1.Text = "Dies wird Windows-Standard-Icons (sfc/scannow) wiederherstellen."
            End If
            If ComboBox1.SelectedIndex = 0 Then
                MessageBox.Show("SFC /SCANNOW will not restore patched files in Program Files", Form1.mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            If ComboBox1.SelectedIndex = 1 Then
                MessageBox.Show("SFC /scannow no restaura parcheado los archivos en archivos de programa archivos", Form1.mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            If ComboBox1.SelectedIndex = 2 Then
                MessageBox.Show("SFC / SCANNOW не восстановит  файлы в Program Files", Form1.mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            If ComboBox1.SelectedIndex = 3 Then
                MessageBox.Show("SFC / scannow keine wiederherstellen Dateien in Program Files", Form1.mainheading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        End If

    End Sub

    Private Sub ProgressBar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProgressBar1.Click

    End Sub


    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim x As String
        x = rand()
        Dim iical3 As New ProcessStartInfo
        iical3.FileName = "taskkill.exe"
        iical3.Arguments = "/f /im explorer.exe"
        iical3.WindowStyle = ProcessWindowStyle.Hidden
        Dim ppi2 As New Process()
        ppi2.StartInfo = iical3
        ppi2.Start()
        ppi2.WaitForExit()



        Try

            For Each Dir As String In System.IO.Directory.GetDirectories("Resource Files\Backup\Program Files")
                f = Dir.TrimStart
                dirname = IO.Path.GetFileName(Dir)
                For Each File In My.Computer.FileSystem.GetFiles("Resource Files\Backup\Program Files\" & dirname)
                    file0 = My.Computer.FileSystem.GetName(File)
                    file1 = file0.TrimStart

                    path0 = Directory.GetFiles(SystemDir & "\Program Files\" & dirname & "\", file1, SearchOption.AllDirectories)
                    For Each f0 As String In path0
                        f0 = f0.Replace(file1, "")
                        '    MessageBox.Show(f0)
                        f0 = f0.Replace(SystemDir & "\Program Files\" & dirname & "\", "")
                        If ComboBox1.SelectedIndex = 0 Then
                            status += "Working on " & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 1 Then
                            status += "Trabajando sobre " & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 2 Then
                            status += "Обработка " & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 3 Then
                            status += "Arbeiten an " & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & vbNewLine
                            enable = "true"
                        End If

                        Dim kil As New ProcessStartInfo
                        kil.FileName = "TASKKILL"
                        kil.Arguments = "/f /im " & Chr(34) & file1 & Chr(34)
                        kil.WindowStyle = ProcessWindowStyle.Hidden
                        Dim pp2i As New Process()
                        pp2i.StartInfo = kil
                        pp2i.Start()
                        'takeownership of original
                        Dim takeown As New ProcessStartInfo
                        takeown.FileName = "CMD"
                        takeown.Arguments = "/c takeown /a /F " & Chr(34) & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & Chr(34) & " && icacls " & Chr(34) & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & Chr(34) & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F" & " && icacls " & Chr(34) & SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & Chr(34) & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F" & " && exit"
                        takeown.WindowStyle = ProcessWindowStyle.Hidden
                        Dim pp1 As New Process()
                        pp1.StartInfo = takeown
                        pp1.Start()
                        pp1.WaitForExit()

                        If ComboBox1.SelectedIndex = 0 Then
                            status += "Restoring " & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 1 Then
                            status += "Restaurar " & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 2 Then
                            status += "восстановить " & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 3 Then
                            status += "Wiederherstellen " & file1 & vbNewLine
                            enable = "true"
                        End If
                        Try
                            If My.Computer.FileSystem.FileExists(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp2") Then
                                My.Computer.FileSystem.RenameFile(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp2", file1 & ".iPold2")
                                My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPold2")
                                Threading.Thread.Sleep(300)
                            End If
                            My.Computer.FileSystem.RenameFile(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1, file1 & ".iPtemp2")
                            My.Computer.FileSystem.MoveFile("Resource Files\Backup\Program Files\" & dirname & "\" & file1, SystemDir & "\Program Files\" & dirname & "\" & f0 & file1, True)
                            Dim del As New ProcessStartInfo
                            del.FileName = "CMD"
                            del.Arguments = "/c del /f /q " & Chr(34) & "%systemdrive%\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp2" & Chr(34) & " && exit"
                            del.WindowStyle = ProcessWindowStyle.Hidden
                            Dim ppx As New Process()
                            ppx.StartInfo = del
                            ppx.Start()
                            ppx.WaitForExit()
                        Catch ex As Exception
                        End Try
                        '   Threading.Thread.Sleep(300)
                        Try
                            If My.Computer.FileSystem.FileExists(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp2") Then
                                For i As Integer = 1 To 3
                                    Try
                                        My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp2")
                                        Exit For
                                    Catch ex As Exception
                                        If i = 3 Then
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


                                            MoveFileEx(SystemDir & "\Program Files\" & dirname & "\" & f0 & file1 & ".iPtemp2", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                                        Else
                                            Thread.Sleep(1000)
                                        End If

                                    End Try
                                Next
                            End If

                        Catch ex As Exception

                        End Try
                        status += vbNewLine
                        enable = "true"

                    Next

                Next
            Next

            For Each Dir As String In System.IO.Directory.GetDirectories("Resource Files\Backup\Program Files (x86)")
                f = Dir.TrimStart
                dirname = IO.Path.GetFileName(Dir)

                For Each File In My.Computer.FileSystem.GetFiles("Resource Files\Backup\Program Files (x86)\" & dirname)

                    file0 = My.Computer.FileSystem.GetName(File)
                    file1 = file0.TrimStart
                    path0 = Directory.GetFiles(SystemDir & "\Program Files (x86)\" & dirname & "\", file1, SearchOption.AllDirectories)
                    For Each f0 As String In path0
                        f0 = f0.Replace(file1, "")
                        f0 = f0.Replace(SystemDir & "\Program Files (x86)\" & dirname & "\", "")
                        If ComboBox1.SelectedIndex = 0 Then
                            status += "Working on " & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 1 Then
                            status += "Trabajando sobre " & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 2 Then
                            status += "Обработка " & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 3 Then
                            status += "Arbeiten an " & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & vbNewLine
                            enable = "true"
                        End If

                        Dim kil As New ProcessStartInfo
                        kil.FileName = "TASKKILL"
                        kil.Arguments = "/f /im " & Chr(34) & file1 & Chr(34)
                        kil.WindowStyle = ProcessWindowStyle.Hidden
                        Dim pp2i As New Process()
                        pp2i.StartInfo = kil
                        pp2i.Start()
                        'takeownership of original

                        Dim takeown As New ProcessStartInfo
                        takeown.FileName = "CMD"
                        takeown.Arguments = "/c takeown /a /F " & Chr(34) & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & Chr(34) & " && icacls " & Chr(34) & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & Chr(34) & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F" & " && icacls " & Chr(34) & SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & Chr(34) & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F" & " && exit"
                        takeown.WindowStyle = ProcessWindowStyle.Hidden
                        Dim pp1 As New Process()
                        pp1.StartInfo = takeown
                        pp1.Start()
                        pp1.WaitForExit()
                        Dim ps As New ProcessStartInfo

                        If ComboBox1.SelectedIndex = 0 Then
                            status += "Restoring " & file1 & " x86" & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 1 Then
                            status += "Restaurar " & file1 & " x86" & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 2 Then
                            status += "восстановить " & file1 & " x86" & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 3 Then
                            status += "Wiederherstellen " & file1 & " x86" & vbNewLine
                            enable = "true"
                        End If

                        Try
                            If My.Computer.FileSystem.FileExists(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp2") Then
                                My.Computer.FileSystem.RenameFile(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp2", file1 & ".iPold2")
                                My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPold2")
                                Threading.Thread.Sleep(300)
                            End If
                            My.Computer.FileSystem.RenameFile(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1, file1 & ".iPtemp2")
                            My.Computer.FileSystem.MoveFile("Resource Files\Backup\Program Files (x86)\" & dirname & "\" & file1, SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1, True)
                            Dim del As New ProcessStartInfo
                            del.FileName = "CMD"
                            del.Arguments = "/c del /f /q " & Chr(34) & "%systemdrive%\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp2" & Chr(34) & " && exit"
                            del.WindowStyle = ProcessWindowStyle.Hidden
                            Dim ppx As New Process()
                            ppx.StartInfo = del
                            ppx.Start()
                            ppx.WaitForExit()
                        Catch ex As Exception
                        End Try
                        '  Threading.Thread.Sleep(300)
                        Try
                            My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPold")
                            If My.Computer.FileSystem.FileExists(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp2") Then
                                For i As Integer = 1 To 3
                                    Try
                                        My.Computer.FileSystem.DeleteFile(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp2")
                                        Exit For
                                    Catch ex As Exception
                                        If i = 3 Then
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

                                            MoveFileEx(SystemDir & "\Program Files (x86)\" & dirname & "\" & f0 & file1 & ".iPtemp2", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                                        Else
                                            Thread.Sleep(1000)
                                        End If
                                    End Try
                                Next
                            End If

                        Catch ex As Exception

                        End Try
                        status += vbNewLine
                        enable = "true"

                    Next

                Next

            Next
            For Each file In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\System32")
                file0 = My.Computer.FileSystem.GetName(file)
                file1 = file0.TrimStart
                Dim ver1, ver2 As String


                If ComboBox1.SelectedIndex = 0 Then
                    status += "Comparing Fileversion of " & file1 & " with original." & vbNewLine
                    enable = "true"
                ElseIf ComboBox1.SelectedIndex = 1 Then
                    status += "Comparando Fileversion de " & file1 & " con originales." & vbNewLine
                    enable = "true"
                ElseIf ComboBox1.SelectedIndex = 2 Then
                    status += "Сравнивая Fileversion из " & file1 & " с оригиналом." & vbNewLine
                    enable = "true"
                ElseIf ComboBox1.SelectedIndex = 3 Then
                    status += "Vergleich von Fileversion " & file1 & " mit Original-" & vbNewLine
                    enable = "true"
                End If


                Dim myFileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\System32\" & file1)
                ver1 = (myFileVersionInfo.FileMajorPart & "." & myFileVersionInfo.FileMinorPart & "." & myFileVersionInfo.FileBuildPart & "." & myFileVersionInfo.FilePrivatePart)
                Dim myFileVersionInfo1 As FileVersionInfo = FileVersionInfo.GetVersionInfo(winDir & "\System32\" & file1)
                ver2 = (myFileVersionInfo1.FileMajorPart & "." & myFileVersionInfo1.FileMinorPart & "." & myFileVersionInfo1.FileBuildPart & "." & myFileVersionInfo1.FilePrivatePart)
                If ver1 = ver2 Then

                    If ComboBox1.SelectedIndex = 0 Then
                        status += "Restoring " & file1 & " " & ver1 & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 1 Then
                        status += "Restaurar " & file1 & " " & ver1 & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 2 Then
                        status += "восстановить " & file1 & " " & ver1 & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 3 Then
                        status += "Wiederherstellen " & file1 & " " & ver1 & vbNewLine
                        enable = "true"
                    End If

                    'takeownership of original


                    Dim takeown As New ProcessStartInfo
                    takeown.FileName = "CMD"
                    takeown.Arguments = "/c takeown /a /F " & winDir & "\System32\" & file1 & " && icacls " & winDir & "\System32\" & file1 & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F" & " && icacls " & winDir & "\System32\" & file1 & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F" & " && exit"
                    takeown.WindowStyle = ProcessWindowStyle.Hidden
                    Dim pp1 As New Process()
                    pp1.StartInfo = takeown
                    pp1.Start()
                    pp1.WaitForExit()
                    Dim ps As New ProcessStartInfo

                    Try
                        If My.Computer.FileSystem.FileExists(winDir & "\System32\" & file1 & ".iPtemp2") Then
                            My.Computer.FileSystem.RenameFile(winDir & "\System32\" & file1, file1 & ".iPold2")
                            My.Computer.FileSystem.DeleteFile(winDir & "\System32\" & file1 & ".iPold2")
                            Threading.Thread.Sleep(300)
                        End If
                        My.Computer.FileSystem.RenameFile(winDir & "\System32\" & file1, file1 & ".iPtemp2")
                        My.Computer.FileSystem.MoveFile("Resource Files\Backup\System32\" & file1, winDir & "\System32\" & file1, True)
                        restorePermission(file1, winDir & "\System32", "Resource Files\ACL\System32")
                        Dim del As New ProcessStartInfo
                        del.FileName = "CMD"
                        del.Arguments = "/c del /f /q %windir%\System32\" & file1 & ".iPtemp2" & " && exit"
                        del.WindowStyle = ProcessWindowStyle.Hidden
                        Dim ppx As New Process()
                        ppx.StartInfo = del
                        ppx.Start()
                        ppx.WaitForExit()

                    Catch ex As Exception

                    End Try
                    ' Threading.Thread.Sleep(300)
                    Try
                        If My.Computer.FileSystem.FileExists(winDir & "\System32\" & file1 & ".iPtemp2") Then
                            For i As Integer = 1 To 3
                                Try
                                    My.Computer.FileSystem.DeleteFile(winDir & "\System32\" & file1 & ".iPtemp2")
                                    Exit For
                                Catch ex As Exception
                                    If i = 3 Then
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


                                        MoveFileEx(winDir & "\System32\" & file1 & ".iPtemp2", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                                    Else
                                        Thread.Sleep(1000)   ' same as Threading.Thread.Sleep(1000)
                                    End If
                                End Try
                            Next
                        End If

                    Catch ex As Exception

                    End Try
                    If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\System32\" & file1) Then
                        Try
                            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x)
                            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\System32")
                            My.Computer.FileSystem.MoveFile(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\System32\" & file1, My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\System32\" & file1, True)
                        Catch exx As Exception
                        End Try


                        MoveFileEx(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\System32\" & file1, winDir & "\System32", MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                        rebootneed = "Yes"
                    End If
                    status += vbNewLine
                    enable = "true"
                Else


                    If ComboBox1.SelectedIndex = 0 Then
                        status += "Fileversion mismatch, skipping " & file1 & vbNewLine & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 1 Then
                        status += "Desajuste Fileversion, saltar " & file1 & vbNewLine & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 2 Then
                        status += "Fileversion несоответствие, пропуская " & file1 & vbNewLine & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 3 Then
                        status += "Fileversion Missverhältnis, das Überspringen " & file1 & vbNewLine & vbNewLine
                        enable = "true"
                    End If

                End If

            Next

            For Each file In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\SysWOW64")
                file0 = My.Computer.FileSystem.GetName(file)
                file1 = file0.TrimStart

                If ComboBox1.SelectedIndex = 0 Then
                    status += "Comparing Fileversion of " & file1 & " x86 with original." & vbNewLine
                    enable = "true"
                ElseIf ComboBox1.SelectedIndex = 1 Then
                    status += "Comparando Fileversion de " & file1 & " x86 con originales." & vbNewLine
                    enable = "true"
                ElseIf ComboBox1.SelectedIndex = 2 Then
                    status += "Сравнивая Fileversion из " & file1 & " x86 с оригиналом." & vbNewLine
                    enable = "true"
                ElseIf ComboBox1.SelectedIndex = 3 Then
                    status += "Vergleich von Fileversion " & file1 & " x86 mit Original-" & vbNewLine
                    enable = "true"
                End If

                Dim ver1, ver2 As String
                Dim myFileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\SysWOW64\" & file1)
                ver1 = (myFileVersionInfo.FileMajorPart & "." & myFileVersionInfo.FileMinorPart & "." & myFileVersionInfo.FileBuildPart & "." & myFileVersionInfo.FilePrivatePart)
                Dim myFileVersionInfo1 As FileVersionInfo = FileVersionInfo.GetVersionInfo(winDir & "\SysWOW64\" & file1)
                ver2 = (myFileVersionInfo1.FileMajorPart & "." & myFileVersionInfo1.FileMinorPart & "." & myFileVersionInfo1.FileBuildPart & "." & myFileVersionInfo1.FilePrivatePart)


                If ver1 = ver2 Then

                    If ComboBox1.SelectedIndex = 0 Then
                        status += "Restoring " & file1 & " x86 " & ver1 & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 1 Then
                        status += "Restaurar " & file1 & " x86 " & ver1 & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 2 Then
                        status += "восстановить " & file1 & " x86 " & ver1 & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 3 Then
                        status += "Wiederherstellen " & file1 & " x86 " & ver1 & vbNewLine
                        enable = "true"
                    End If

                    'takeownership of original
                    Dim takeown As New ProcessStartInfo
                    takeown.FileName = "CMD"
                    takeown.Arguments = "/c takeown /a /F " & winDir & "\SysWOW64\" & file1 & " && icacls " & winDir & "\SysWOW64\" & file1 & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F" & " && icacls " & winDir & "\SysWOW64\" & file1 & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F" & " && exit"
                    takeown.WindowStyle = ProcessWindowStyle.Hidden
                    Dim pp1 As New Process()
                    pp1.StartInfo = takeown
                    pp1.Start()
                    pp1.WaitForExit()


                    Try
                        If My.Computer.FileSystem.FileExists(winDir & "\SysWOW64\" & file1 & ".iPtemp2") Then
                            My.Computer.FileSystem.RenameFile(winDir & "\SysWOW64\" & file1, file1 & ".iPold2")
                            My.Computer.FileSystem.DeleteFile(winDir & "\SysWOW64\" & file1 & ".iPold2")
                            Threading.Thread.Sleep(300)
                        End If
                        My.Computer.FileSystem.RenameFile(winDir & "\SysWOW64\" & file1, file1 & ".iPtemp2")
                        My.Computer.FileSystem.MoveFile("Resource Files\Backup\SysWOW64\" & file1, winDir & "\SysWOW64\" & file1, True)
                        restorePermission(file1, winDir & "\SysWOW64", "Resource Files\ACL\SysWOW64")
                        Dim del As New ProcessStartInfo
                        del.FileName = "CMD"
                        del.Arguments = "/c del /f /q %windir%\SysWOW64\" & file1 & ".iPtemp2" & " && exit"
                        del.WindowStyle = ProcessWindowStyle.Hidden
                        Dim ppx As New Process()
                        ppx.StartInfo = del
                        ppx.Start()
                        ppx.WaitForExit()
                    Catch ex As Exception

                    End Try
                    '  Threading.Thread.Sleep(300)
                    Try
                        If My.Computer.FileSystem.FileExists(winDir & "\SysWOW64\" & file1 & ".iPtemp2") Then
                            For i As Integer = 1 To 3

                                Try
                                    My.Computer.FileSystem.DeleteFile(winDir & "\SysWOW64\" & file1 & ".iPtemp2")
                                    Exit For
                                Catch ex As Exception
                                    If i = 3 Then
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

                                        MoveFileEx(winDir & "\SysWOW64\" & file1 & ".iPtemp2", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                                    Else
                                        Thread.Sleep(1000)   ' same as Threading.Thread.Sleep(1000)
                                    End If
                                End Try
                            Next
                        End If

                    Catch ex As Exception

                    End Try
                    If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\SysWOW64\" & file1) Then
                        Try
                            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x)
                            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\SysWOW64")
                            My.Computer.FileSystem.MoveFile(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\SysWOW64\" & file1, My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\SysWOW64\" & file1, True)
                        Catch exx As Exception
                        End Try

                        MoveFileEx(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\SysWOW64\" & file1, winDir & "\SysWOW64", MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                        rebootneed = "Yes"
                    End If
                    status += vbNewLine
                    enable = "true"



                Else

                    If ComboBox1.SelectedIndex = 0 Then
                        status += "Fileversion mismatch, skipping " & file1 & " x86" & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 1 Then
                        status += "Desajuste Fileversion, saltar " & file1 & " x86" & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 2 Then
                        status += "Fileversion несоответствие, пропуская " & file1 & " x86" & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 3 Then
                        status += "Fileversion Missverhältnis, das Überspringen " & file1 & " x86" & vbNewLine
                        enable = "true"
                    End If


                End If

            Next
            For Each file In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\Windows")
                file0 = My.Computer.FileSystem.GetName(file)
                file1 = file0.TrimStart

                If ComboBox1.SelectedIndex = 0 Then
                    status += "Comparing Fileversion of " & file1 & " with original." & vbNewLine
                    enable = "true"
                ElseIf ComboBox1.SelectedIndex = 1 Then
                    status += "Comparando Fileversion de " & file1 & " con originales." & vbNewLine
                    enable = "true"
                ElseIf ComboBox1.SelectedIndex = 2 Then
                    status += "Сравнивая Fileversion из " & file1 & " с оригиналом." & vbNewLine
                    enable = "true"
                ElseIf ComboBox1.SelectedIndex = 3 Then
                    status += "Vergleich von Fileversion " & file1 & " mit Original-" & vbNewLine
                    enable = "true"
                End If

                If file1 = "shellbrd.dll" Then
                    Dim ver01, ver02 As String
                    Dim myFileVersionInfo0 As FileVersionInfo = FileVersionInfo.GetVersionInfo(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\Windows\" & file1)
                    ver01 = (myFileVersionInfo0.FileMajorPart & "." & myFileVersionInfo0.FileMinorPart & "." & myFileVersionInfo0.FileBuildPart & "." & myFileVersionInfo0.FilePrivatePart)
                    Dim myFileVersionInfo10 As FileVersionInfo = FileVersionInfo.GetVersionInfo(winDir & "\Branding\ShellBrd\" & file1)
                    ver02 = (myFileVersionInfo10.FileMajorPart & "." & myFileVersionInfo10.FileMinorPart & "." & myFileVersionInfo10.FileBuildPart & "." & myFileVersionInfo10.FilePrivatePart)

                    If ver01 = ver02 Then

                        If ComboBox1.SelectedIndex = 0 Then
                            status += "Restoring " & file1 & " " & ver01 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 1 Then
                            status += "Restaurar " & file1 & " " & ver01 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 2 Then
                            status += "восстановить " & file1 & " " & ver01 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 3 Then
                            status += "Wiederherstellen " & file1 & " " & ver01 & vbNewLine
                            enable = "true"
                        End If

                        'takeownership of original


                        Dim takeown As New ProcessStartInfo
                        takeown.FileName = "CMD"
                        takeown.Arguments = "/c takeown /a /F " & winDir & "\Branding\ShellBrd\" & file1 & " && icacls " & winDir & "\Branding\ShellBrd\" & file1 & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F" & " && icacls " & winDir & "\Branding\ShellBrd\" & file1 & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F" & " && exit"
                        takeown.WindowStyle = ProcessWindowStyle.Hidden
                        Dim pp1 As New Process()
                        pp1.StartInfo = takeown
                        pp1.Start()
                        pp1.WaitForExit()




                        Try
                            If My.Computer.FileSystem.FileExists(winDir & "\Branding\ShellBrd\" & file1 & ".iPtemp2") Then
                                My.Computer.FileSystem.RenameFile(winDir & "\Branding\ShellBrd\" & file1, file1 & ".iPold2")
                                My.Computer.FileSystem.DeleteFile(winDir & "\Branding\ShellBrd\" & file1 & ".iPold2")
                                Threading.Thread.Sleep(300)
                            End If
                            My.Computer.FileSystem.RenameFile(winDir & "\Branding\ShellBrd\" & file1, file1 & ".iPtemp2")
                            My.Computer.FileSystem.MoveFile("Resource Files\Backup\Windows\" & file1, winDir & "\Branding\ShellBrd\" & file1, True)
                            Dim del As New ProcessStartInfo
                            del.FileName = "CMD"
                            del.Arguments = "/c del /f /q %windir%\Branding\ShellBrd\" & file1 & ".iPtemp2" & " && exit"
                            del.WindowStyle = ProcessWindowStyle.Hidden
                            Dim ppx As New Process()
                            ppx.StartInfo = del
                            ppx.Start()
                            ppx.WaitForExit()



                        Catch ex As Exception

                        End Try
                        '   Threading.Thread.Sleep(300)
                        Try
                            If My.Computer.FileSystem.FileExists(winDir & "\Branding\ShellBrd\" & file1 & ".iPtemp2") Then
                                For i As Integer = 1 To 3
                                    Try
                                        My.Computer.FileSystem.DeleteFile(winDir & "\Branding\ShellBrd\" & file1 & ".iPtemp2")
                                        Exit For
                                    Catch ex As Exception
                                        If i = 3 Then
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


                                            MoveFileEx(winDir & "\Branding\ShellBrd\" & file1 & ".iPtemp2", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                                        Else
                                            Thread.Sleep(1000)   ' same as Threading.Thread.Sleep(1000)
                                        End If
                                    End Try
                                Next
                            End If

                        Catch ex As Exception

                        End Try
                        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\Windows\" & file1) Then
                            Try
                                My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x)
                                My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\Windows")
                                My.Computer.FileSystem.MoveFile(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\Windows\" & file1, My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\Windows\" & file1, True)
                            Catch exx As Exception
                            End Try

                            MoveFileEx(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\Windows\" & file1, winDir & "\Branding\ShellBrd", MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                            rebootneed = "Yes"
                        End If
                        status += vbNewLine
                        enable = "true"


                    Else

                        If ComboBox1.SelectedIndex = 0 Then
                            status += "Fileversion mismatch, skipping " & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 1 Then
                            status += "Desajuste Fileversion, saltar " & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 2 Then
                            status += "Fileversion несоответствие, пропуская " & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 3 Then
                            status += "Fileversion Missverhältnis, das Überspringen " & file1 & vbNewLine
                            enable = "true"
                        End If

                    End If
                End If

                If file1 = "basebrd.dll" Then
                    Dim ver01, ver02 As String
                    Dim myFileVersionInfo0 As FileVersionInfo = FileVersionInfo.GetVersionInfo(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\Windows\" & file1)
                    ver01 = (myFileVersionInfo0.FileMajorPart & "." & myFileVersionInfo0.FileMinorPart & "." & myFileVersionInfo0.FileBuildPart & "." & myFileVersionInfo0.FilePrivatePart)
                    Dim myFileVersionInfo10 As FileVersionInfo = FileVersionInfo.GetVersionInfo(winDir & "\Branding\Basebrd\" & file1)
                    ver02 = (myFileVersionInfo10.FileMajorPart & "." & myFileVersionInfo10.FileMinorPart & "." & myFileVersionInfo10.FileBuildPart & "." & myFileVersionInfo10.FilePrivatePart)

                    If ver01 = ver02 Then
                        If ComboBox1.SelectedIndex = 0 Then
                            status += "Restoring " & file1 & " " & ver01 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 1 Then
                            status += "Restaurar " & file1 & " " & ver01 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 2 Then
                            status += "восстановить " & file1 & " " & ver01 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 3 Then
                            status += "Wiederherstellen " & file1 & " " & ver01 & vbNewLine
                            enable = "true"
                        End If

                        'takeownership of original


                        Dim takeown As New ProcessStartInfo
                        takeown.FileName = "CMD"
                        takeown.Arguments = "/c takeown /a /F " & winDir & "\Branding\Basebrd\" & file1 & " && icacls " & winDir & "\Branding\Basebrd\" & file1 & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F" & " && icacls " & winDir & "\Branding\Basebrd\" & file1 & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F" & " && exit"
                        takeown.WindowStyle = ProcessWindowStyle.Hidden
                        Dim pp1 As New Process()
                        pp1.StartInfo = takeown
                        pp1.Start()
                        pp1.WaitForExit()

                        Try
                            If My.Computer.FileSystem.FileExists(winDir & "\Branding\Basebrd\" & file1 & ".iPtemp2") Then
                                My.Computer.FileSystem.RenameFile(winDir & "\Branding\Basebrd\" & file1, file1 & ".iPold2")
                                My.Computer.FileSystem.DeleteFile(winDir & "\Branding\Basebrd\" & file1 & ".iPold2")
                                Threading.Thread.Sleep(300)
                            End If
                            My.Computer.FileSystem.RenameFile(winDir & "\Branding\Basebrd\" & file1, file1 & ".iPtemp2")
                            My.Computer.FileSystem.MoveFile("Resource Files\Backup\Windows\" & file1, winDir & "\Branding\Basebrd\" & file1, True)
                            Dim del As New ProcessStartInfo
                            del.FileName = "CMD"
                            del.Arguments = "/c del /f /q %windir%\Branding\Basebrd\" & file1 & ".iPtemp2" & " && exit"
                            del.WindowStyle = ProcessWindowStyle.Hidden
                            Dim ppx As New Process()
                            ppx.StartInfo = del
                            ppx.Start()
                            ppx.WaitForExit()
                        Catch ex As Exception

                        End Try
                        '  Threading.Thread.Sleep(300)
                        Try
                            If My.Computer.FileSystem.FileExists(winDir & "\Branding\Basebrd\" & file1 & ".iPtemp2") Then
                                For i As Integer = 1 To 3
                                    Try
                                        My.Computer.FileSystem.DeleteFile(winDir & "\Branding\Basebrd\" & file1 & ".iPtemp2")
                                        Exit For
                                    Catch ex As Exception
                                        If i = 3 Then
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

                                            MoveFileEx(winDir & "\Branding\Basebrd\" & file1 & ".iPtemp2", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                                        Else
                                            Thread.Sleep(1000)   ' same as Threading.Thread.Sleep(1000)
                                        End If
                                    End Try
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\Windows\" & file1) Then
                           

                            Try
                                My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x)
                                My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\Windows")
                                My.Computer.FileSystem.MoveFile(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\Windows\" & file1, My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\Windows\" & file1, True)
                            Catch exx As Exception
                            End Try

                            MoveFileEx(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\Windows\" & file1, winDir & "\Branding\Basebrd", MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                            rebootneed = "Yes"
                        End If
                        status += vbNewLine
                        enable = "true"


                    Else

                        If ComboBox1.SelectedIndex = 0 Then
                            status += "Fileversion mismatch, skipping " & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 1 Then
                            status += "Desajuste Fileversion, saltar " & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 2 Then
                            status += "Fileversion несоответствие, пропуская " & file1 & vbNewLine
                            enable = "true"
                        ElseIf ComboBox1.SelectedIndex = 3 Then
                            status += "Fileversion Missverhältnis, das Überspringen " & file1 & vbNewLine
                            enable = "true"
                        End If

                    End If
                End If

                Dim ver1, ver2 As String
                Dim myFileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\Windows\" & file1)
                ver1 = (myFileVersionInfo.FileMajorPart & "." & myFileVersionInfo.FileMinorPart & "." & myFileVersionInfo.FileBuildPart & "." & myFileVersionInfo.FilePrivatePart)
                Dim myFileVersionInfo1 As FileVersionInfo = FileVersionInfo.GetVersionInfo(winDir & "\" & file1)
                ver2 = (myFileVersionInfo1.FileMajorPart & "." & myFileVersionInfo1.FileMinorPart & "." & myFileVersionInfo1.FileBuildPart & "." & myFileVersionInfo1.FilePrivatePart)

                If ver1 = ver2 Then
                    If winbit = "x64" Then


                    End If
                    If ComboBox1.SelectedIndex = 0 Then
                        status += "Restoring " & file1 & " " & ver1 & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 1 Then
                        status += "Restaurar " & file1 & " " & ver1 & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 2 Then
                        status += "восстановить " & file1 & " " & ver1 & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 3 Then
                        status += "Wiederherstellen " & file1 & " " & ver1 & vbNewLine
                        enable = "true"
                    End If

                    'takeownership of original


                    Dim takeown As New ProcessStartInfo
                    takeown.FileName = "CMD"
                    takeown.Arguments = "/c takeown /a /F " & winDir & "\" & file1 & " && icacls " & winDir & "\" & file1 & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F" & " && icacls " & winDir & "\" & file1 & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F" & " && exit"
                    takeown.WindowStyle = ProcessWindowStyle.Hidden
                    Dim pp1 As New Process()
                    pp1.StartInfo = takeown
                    pp1.Start()
                    pp1.WaitForExit()
                    Dim ps As New ProcessStartInfo

                    Try
                        If My.Computer.FileSystem.FileExists(winDir & "\" & file1 & ".iPtemp2") Then
                            My.Computer.FileSystem.RenameFile(winDir & "\" & file1, file1 & ".iPold2")
                            My.Computer.FileSystem.DeleteFile(winDir & "\" & file1 & ".iPold2")
                            Threading.Thread.Sleep(300)
                        End If
                        My.Computer.FileSystem.RenameFile(winDir & "\" & file1, file1 & ".iPtemp2")
                        My.Computer.FileSystem.MoveFile("Resource Files\Backup\Windows\" & file1, winDir & "\" & file1, True)
                        restorePermission(file1, winDir, "Resource Files\ACL")
                        Dim del As New ProcessStartInfo
                        del.FileName = "CMD"
                        del.Arguments = "/c del /f /q %windir%\" & file1 & ".iPtemp2" & " && exit"
                        del.WindowStyle = ProcessWindowStyle.Hidden
                        Dim ppx As New Process()
                        ppx.StartInfo = del
                        ppx.Start()
                        ppx.WaitForExit()
                    Catch ex As Exception

                    End Try
                    '  Threading.Thread.Sleep(300)
                    Try
                        If My.Computer.FileSystem.FileExists(winDir & "\" & file1 & ".iPtemp2") Then
                            For i As Integer = 1 To 3

                                Try
                                    My.Computer.FileSystem.DeleteFile(winDir & "\" & file1 & ".iPtemp2")
                                    Exit For
                                Catch ex As Exception
                                    If i = 3 Then
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


                                        MoveFileEx(winDir & "\" & file1 & ".iPtemp2", Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                                    Else
                                        Thread.Sleep(1000)   ' same as Threading.Thread.Sleep(1000)
                                    End If
                                End Try
                            Next
                        End If

                    Catch ex As Exception

                    End Try
                    If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\Windows\" & file1) Then
                        Try
                            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x)
                            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\Windows")
                            My.Computer.FileSystem.MoveFile(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\Backup\Windows\" & file1, My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\Windows\" & file1, True)
                        Catch exx As Exception
                        End Try

                        MoveFileEx(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x & "\Windows\" & file1, winDir, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
                        rebootneed = "Yes"
                    End If

                    status += vbNewLine
                    enable = "true"


                Else


                    If ComboBox1.SelectedIndex = 0 Then
                        status += "Fileversion mismatch, skipping " & file1 & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 1 Then
                        status += "Desajuste Fileversion, saltar " & file1 & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 2 Then
                        status += "Fileversion несоответствие, пропуская " & file1 & vbNewLine
                        enable = "true"
                    ElseIf ComboBox1.SelectedIndex = 3 Then
                        status += "Fileversion Missverhältnis, das Überspringen " & file1 & vbNewLine
                        enable = "true"
                    End If

                End If

            Next


        Catch ex As Exception

        End Try
        Dim uniWriter As New System.IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.Temp & "\reload.bat")
        uniWriter.Write(vbNewLine)
        uniWriter.Write("set iconcache=%localappdata%\IconCache.db")
        uniWriter.Write(vbNewLine)
        uniWriter.Write("TAKEOWN /f " & Chr(34) & "%iconcache%" & Chr(34) & " && ICACLS " & Chr(34) & "%iconcache%" & Chr(34) & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F /t" & " && ICACLS " & Chr(34) & "%iconcache%" & Chr(34) & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F /t")
        uniWriter.Write(vbNewLine)
        uniWriter.Write("del " & Chr(34) & "%iconcache%" & Chr(34) & "/A")
        uniWriter.Write(vbNewLine)
        uniWriter.Write("set thumbcache=" & SystemDir & "\Users\%username%\AppData\Local\Microsoft\Windows\Explorer\*.db*")
        uniWriter.Write(vbNewLine)
        uniWriter.Write("TAKEOWN /f " & Chr(34) & SystemDir & "\Users\%username%\AppData\Local\Microsoft\Windows\Explorer" & Chr(34) & " /r /d y && ICACLS " & Chr(34) & SystemDir & "\Users\%username%\AppData\Local\Microsoft\Windows\Explorer" & Chr(34) & " /grant:r " & Chr(34) & "%username%" & Chr(34) & ":F /t && ICACLS " & Chr(34) & SystemDir & "\Users\%username%\AppData\Local\Microsoft\Windows\Explorer" & Chr(34) & " /grant:r " & Chr(34) & "administrators" & Chr(34) & ":F /t")
        uniWriter.Write(vbNewLine)
        uniWriter.Write("del " & Chr(34) & "%thumbcache%" & Chr(34) & "/A")
        uniWriter.Write(vbNewLine)
        uniWriter.Write("start %windir%\explorer.exe")
        uniWriter.Write(vbNewLine)
        ' uniWriter.Write("cd %temp%")
        ' uniWriter.Write(vbNewLine)
        'uniWriter.Write("rd /s /q " & Chr(34) & My.Computer.FileSystem.CurrentDirectory & Chr(34))
        ' uniWriter.Write(vbNewLine)
        uniWriter.Write("DEL " & Chr(34) & "%~f0" & Chr(34))
        uniWriter.Write(vbNewLine)
        uniWriter.Close()
        Try
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

            If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x) Then
                MoveFileEx(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & x, Nothing, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT)
            End If

        Catch ex As Exception

        End Try


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
       
        Dim launch As New ProcessStartInfo()
        launch.WindowStyle = ProcessWindowStyle.Hidden
        launch.UseShellExecute = True
        launch.FileName = (My.Computer.FileSystem.SpecialDirectories.Temp & "\reload.bat")
        launch.WorkingDirectory = ""
        Process.Start(launch)


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
            changelocation()
            '   TextBox1.Text = "[.. File(s) available for restore ..]" & vbNewLine & vbNewLine
            ' Label1.Font = New Font(Label1.Font.FontFamily, 12)
            ' Label1.Text = "iPack Uninstaller"
            RadioButton1.Text = "Restore backup made at the time of installation."
            RadioButton2.Text = "Restore windows default icons ( sfc /scannow )"
            Button2.Text = "Next >"
            Button1.Text = "Cancel"
            Label6.Text = "File(s) available for restore:"
            Label2.Text = "Uninstalling: "
            Label3.Location = New Point(Label2.Width + 17, Label2.Location.Y)

        End If
        If ComboBox1.SelectedIndex = 1 Then
            changelocation()
            'Label1.Font = New Font(Label1.Font.FontFamily, 12)
            'Button2.Text = "Siguiente >"
            Button1.Text = "Cancelar"
            Label1.Text = "iPack Uninstaller"
            RadioButton1.Text = "Restaurar copia de seguridad realizada por el instalador."
            RadioButton2.Text = "Restaurar ventanas iconos predeterminados (sfc / scannow)"
            Label6.Text = "Archivos disponibles para restaura:"
            Label2.Text = "La desinstalación: "
            Label3.Location = New Point(Label2.Width + 17, Label2.Location.Y)

        End If
        If ComboBox1.SelectedIndex = 2 Then
            changelocation()
            ' Label1.Font = New Font(Label1.Font.FontFamily, 11)
            ' Label1.Text = "iPack Uninstaller"
            RadioButton1.Text = "Восстановление резервной копии, сделанные во время установки."
            RadioButton2.Text = "Восстановление окна дефолтных иконок (SFC / SCANNOW)"
            Button2.Text = "Следующая >"
            Button1.Text = "Отменить"
            Label6.Text = "Файл (ы) для восстановления:"
            Label2.Text = "деинсталляции: "
            Label3.Location = New Point(Label2.Width + 17, Label2.Location.Y)

        End If
        If ComboBox1.SelectedIndex = 3 Then
            changelocation()
            ' Label1.Font = New Font(Label1.Font.FontFamily, 12)
            Button2.Text = "Nächste >"
            Button1.Text = "Stornieren"
            ' Label1.Text = "iPack Uninstaller"
            RadioButton1.Text = "Wiederherstellen Sicherung durch gemacht Installater."
            RadioButton2.Text = "Wiederherstellen von Windows Standard-Icons (sfc / scannow)"
            Label6.Text = "Datei(en) für die Wiederherstellung verfügbar:"
            Label2.Text = "Deinstallieren: "
            Label3.Location = New Point(Label2.Width + 17, Label2.Location.Y)

        End If

        If RadioButton2.Checked = True Then

            If ComboBox1.SelectedIndex = 0 Then
                TextBox1.Text = "This will restore windows default icons (sfc /scannow)"
            End If
            If ComboBox1.SelectedIndex = 1 Then
                TextBox1.Text = "Esto restaura los iconos por defecto de windows (sfc/scannow)"
            End If
            If ComboBox1.SelectedIndex = 2 Then
                TextBox1.Text = "Это позволит восстановить windows по умолчанию иконки (sfc/scannow)"
            End If
            If ComboBox1.SelectedIndex = 3 Then
                TextBox1.Text = "Dies wird Windows-Standard-Icons (sfc/scannow) wiederherstellen."
            End If
        End If
        Try
            Form1.langthemed.Label1.Text = Form1.Uninstaller.ComboBox1.SelectedItem.ToString

        Catch ex As Exception

        End Try

    End Sub


    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        If Not imgThumb2 Is Nothing Then
            e.Graphics.DrawImage(imgThumb2, 0, 0, imgThumb2.Width, imgThumb2.Height)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub RadioButton1_Paint(sender As Object, e As PaintEventArgs) Handles RadioButton1.Paint
        Try

            If Not Form1.rChecked = Nothing And RadioButton1.Checked = True Then

                ' e.Graphics.DrawImageUnscaled((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked)), 0, 0)
                e.Graphics.DrawImageUnscaledAndClipped((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked)), New Rectangle(0, 3, (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked).Width), (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked).Height)))

            ElseIf Not Form1.rUnchecked = Nothing And RadioButton1.Checked = False Then
                '  e.Graphics.DrawImageUnscaled((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked)), 0, 0)
                e.Graphics.DrawImageUnscaledAndClipped((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked)), New Rectangle(0, 3, (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked).Width), (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked).Height)))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadioButton2_Paint(sender As Object, e As PaintEventArgs) Handles RadioButton2.Paint

        Try

            If Not Form1.rChecked = Nothing And RadioButton2.Checked = True Then
                e.Graphics.DrawImageUnscaledAndClipped((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked)), New Rectangle(0, 3, (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked).Width), (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rChecked).Height)))
            ElseIf Not Form1.rUnchecked = Nothing And RadioButton2.Checked = False Then
                e.Graphics.DrawImageUnscaledAndClipped((Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked)), New Rectangle(0, 3, (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked).Width), (Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Theme\" & Form1.rUnchecked).Height)))
            End If
        Catch ex As Exception

        End Try
    End Sub


End Class
