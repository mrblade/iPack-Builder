Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Public Class upgrade
    Dim rng As Random = New Random
    Dim packk, loc As String
    Dim adv, author, os, language, pack, mainheading, lalign, silent As String
    Dim path As String
    Dim ProcessFileStream As FileStream
    Dim ResultFileStream As FileStream
    Dim myCryptoStream As CryptoStream
    Private Sub upgrade_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Label4.Text.Contains("Working") Then
            MessageBox.Show("Please wait untill iPack Upgradation process is complete.", "iPack Upgrade", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            e.Cancel = True
        Else
            Form1.bottompart.Enabled = True
            Form1.Enabled = True

            ' Form1.Form1_Activated(Nothing, Nothing)
        End If
    End Sub

    Private Sub upgrade_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New Point(Form1.Location.X + (Form1.Width - Me.Width) / 2, Form1.Location.Y + (Form1.Height - Me.Height) / 2)
        Me.BringToFront()
        Owner = Form1
        Form1.bottompart.Enabled = False
        ' Form1.Form1_Deactivate(Nothing, Nothing)
        Form1.Enabled = False


    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        OpenFileDialog1.Filter = "Applications (*.exe)|*.exe"
        OpenFileDialog1.ShowDialog()

        If OpenFileDialog1.FileName = "OpenFileDialog1" Then
            TextBox1.Text = ""
        Else
            TextBox1.Text = OpenFileDialog1.FileName
            packk = IO.Path.GetFileName(OpenFileDialog1.FileName)
            loc = My.Computer.FileSystem.SpecialDirectories.Temp & "\" & packk
            '  Button2.Enabled = True
            '  MessageBox.Show(IO.Path.GetFileName(OpenFileDialog1.FileName))
        End If
    End Sub
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

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Please select an iPack.", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf TextBox2.Text = "" Then
            MessageBox.Show("Please select output path.", "iPack Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Else
            Dim i As String
            i = rand()
            path = My.Computer.FileSystem.SpecialDirectories.Temp & "\" & i

            PictureBox1.Visible = False
            PictureBox2.Enabled = False
            PictureBox4.Visible = True
            PictureBox3.Enabled = False

            Label4.Text = "Working."
            ' Me.Cursor = Cursors.No
            My.Computer.FileSystem.CreateDirectory(path)
            BackgroundWorker1.RunWorkerAsync()

        End If

    End Sub
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

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.Image = My.Resources.upg2
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Image = My.Resources.upg1
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        FolderBrowserDialog1.ShowDialog()
        TextBox2.Text = FolderBrowserDialog1.SelectedPath

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        ' MessageBox.Show(loc & vbNewLine & packk)
        Try
           

            My.Computer.FileSystem.CreateDirectory(path)
            Form1.SaveToDisk("7z.exe", path & "\7z.exe")
            Form1.SaveToDisk("crypt.exe", path & "\crypt.exe")
            Form1.SaveToDisk("7zS.sfx", path & "\7zS.sfx")
            Form1.SaveToDisk("upx.exe", path & "\upx.exe")
            Form1.SaveToDisk("ResHacker.exe", path & "\ResHacker.exe")
            Dim takeown As New ProcessStartInfo
            takeown.FileName = path & "\7z.exe"
            takeown.Arguments = "x -o" & Chr(34) & path & Chr(34) & " -y " & Chr(34) & OpenFileDialog1.FileName & Chr(34)
            takeown.WindowStyle = ProcessWindowStyle.Hidden
            Dim pp1 As New Process()
            pp1.StartInfo = takeown
            pp1.Start()
            pp1.WaitForExit()
            Form1.SaveToDisk("iPack_Installer.exe", path & "\iPack_Installer.exe")

            Dim xicon As New ProcessStartInfo
            xicon.FileName = path & "\ResHacker.exe"
            xicon.Arguments = "-extract " & Chr(34) & OpenFileDialog1.FileName & Chr(34) & ", " & Chr(34) & path & "\Icon.ico" & Chr(34) & ", ICONGROUP, 101,"
            xicon.WindowStyle = ProcessWindowStyle.Hidden
            Dim xtract As New Process()
            xtract.StartInfo = xicon
            xtract.Start()
            xtract.WaitForExit()

            Dim xver As New ProcessStartInfo
            xver.FileName = path & "\ResHacker.exe"
            xver.Arguments = "-extract " & Chr(34) & OpenFileDialog1.FileName & Chr(34) & ", " & Chr(34) & path & "\ver.res" & Chr(34) & ", VERSIONINFO ,,"
            xver.WindowStyle = ProcessWindowStyle.Hidden
            Dim xtract1 As New Process()
            xtract1.StartInfo = xver
            xtract1.Start()
            xtract1.WaitForExit()

            Dim xdel As New ProcessStartInfo
            xdel.FileName = path & "\ResHacker.exe"
            xdel.Arguments = "-delete " & Chr(34) & path & "\iPack_Installer.exe" & Chr(34) & ", " & Chr(34) & path & "\iPack_Installer.exe" & Chr(34) & ", VERSIONINFO , 1,"
            xdel.WindowStyle = ProcessWindowStyle.Hidden
            Dim xtract10 As New Process()
            xtract10.StartInfo = xdel
            xtract10.Start()
            xtract10.WaitForExit()

            Dim x0 As New ProcessStartInfo
            x0.FileName = path & "\ResHacker.exe"
            x0.Arguments = "-addoverwrite " & Chr(34) & path & "\iPack_Installer.exe" & Chr(34) & ", " & Chr(34) & path & "\iPack_Installer.exe" & Chr(34) & ", " & Chr(34) & path & "\Icon.ico" & Chr(34) & ", ICONGROUP , 32512,"
            x0.WindowStyle = ProcessWindowStyle.Hidden
            Dim xtract100 As New Process()
            xtract100.StartInfo = x0
            xtract100.Start()
            xtract100.WaitForExit()

            Dim x00 As New ProcessStartInfo
            x00.FileName = path & "\ResHacker.exe"
            x00.Arguments = "-addoverwrite " & Chr(34) & path & "\iPack_Installer.exe" & Chr(34) & ", " & Chr(34) & path & "\iPack_Installer.exe" & Chr(34) & ", " & Chr(34) & path & "\ver.res" & Chr(34) & " ,,,"
            x00.WindowStyle = ProcessWindowStyle.Hidden
            Dim xtract0100 As New Process()
            xtract0100.StartInfo = x00
            xtract0100.Start()
            xtract0100.WaitForExit()


        Catch ex As Exception

        End Try
      
        Try
            ' ----->read config file
            Dim lines() As String = System.IO.File.ReadAllLines(path & "\Setup files-iPack\Configuration.config")
            Dim i, j As Integer
            For i = 0 To lines.Length - 1
                If lines(i).Contains("Pack Name:") Then
                    '------->Pack name
                    pack = lines(i + 1)
                ElseIf lines(i).Contains("Author:") Then
                    '------->author name
                    author = lines(i + 1)
                ElseIf lines(i).Contains("Lalign:") Then
                    '------->license text align
                    lalign = lines(i + 1)
                ElseIf lines(i).Contains("OS:") Then
                    '------->license text align
                    os = lines(i + 1)
                ElseIf lines(i).Contains("Language:") Then
                    '------->license text align
                    language = lines(i + 1)
                ElseIf lines(i).Contains("Ad:") Then
                    '------->Pack name
                    adv = lines(i + 1)
                ElseIf lines(i).Contains("Heading:") Then
                    '------->heading of the installer
                    mainheading = lines(i + 1)

                End If

            Next
            My.Computer.FileSystem.MoveFile(path & "\Setup files-iPack\Configuration.config", path & "\old.config")

            If pack = "" And author = "" And os = "" Then

                pack = GetSettingItem(path & "\old.config", "Pack Name")
                author = GetSettingItem(path & "\old.config", "Author")
                os = GetSettingItem(path & "\old.config", "OS")

                If pack = "" Or author = "" Or os = "" Then
                    MessageBox.Show("Corrupt configuration file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Close()
                    Return
                End If
                mainheading = GetSettingItem(path & "\old.config", "Heading")
                If mainheading = "" Then
                    mainheading = pack
                End If
                lalign = GetSettingItem(path & "\old.config", "Lalign")
                If lalign = "" Then
                    lalign = 1
                End If
                language = GetSettingItem(path & "\old.config", "Language")
                If language = "" Then
                    language = 0
                End If
                adv = GetSettingItem(path & "\old.config", "Ad")
                If adv = "" Then
                    adv = 0
                End If

            End If


            Dim configWriter As New System.IO.StreamWriter(path & "\Setup files-iPack\Configuration.config")
            configWriter.Write(vbNewLine)
            configWriter.Write("---------// iPack Configuration (Do not modify) \\---------" & vbNewLine)
            configWriter.Write(vbNewLine)
            configWriter.Write("Pack Name=")
            configWriter.Write(pack)
            configWriter.Write(vbNewLine)
            configWriter.Write("Author=")
            configWriter.Write(author)
            configWriter.Write(vbNewLine)
            configWriter.Write("Heading=")
            configWriter.Write(mainheading)
            configWriter.Write(vbNewLine)
            configWriter.Write("OS=")
            If os = "" Then
                configWriter.Write("0")
            Else
                configWriter.Write(os)
            End If
            configWriter.Write(vbNewLine)
            configWriter.Write("Lalign=")
            If lalign = "" Then
                configWriter.Write("1")
            Else
                configWriter.Write(lalign)
            End If
            configWriter.Write(vbNewLine)
            configWriter.Write("Language=")
            If language = "" Then
                configWriter.Write("0")
            Else
                configWriter.Write(language)
            End If
            configWriter.Write(vbNewLine)
            configWriter.Write("Ad=")
            If adv = "" Then
                configWriter.Write("0")
            Else
                configWriter.Write(adv)
            End If
            configWriter.Write(vbNewLine)
            configWriter.Write("Silent=")
            If silent = "" Then
                configWriter.Write("0")
            Else
                configWriter.Write(silent)
            End If
            configWriter.Write(vbNewLine & vbNewLine)
            configWriter.Write("-------------// iPack Configuration END  \\-------------" & vbNewLine)
            configWriter.Close()
        Catch ex As Exception

        End Try

        If My.Computer.FileSystem.FileExists(path & "\Resource.iPack") Then
            '   My.Computer.FileSystem.RenameFile(path & "\Resource.iPack", "Resource.iPack2")
            Try
                Dim myDESProvider As DESCryptoServiceProvider = New DESCryptoServiceProvider()
                myDESProvider.Key = ASCIIEncoding.ASCII.GetBytes("B3DM60P7")
                myDESProvider.IV = ASCIIEncoding.ASCII.GetBytes("B3DM60P7")
                '  Dim myICryptoTransform As ICryptoTransform = myDESProvider.CreateEncryptor(myDESProvider.Key, myDESProvider.IV)
                Dim Decr As ICryptoTransform = myDESProvider.CreateDecryptor(myDESProvider.Key, myDESProvider.IV)
                ProcessFileStream = New FileStream(path & "\Resource.iPack", FileMode.Open, FileAccess.Read)
                ResultFileStream = New FileStream(path & "\Resource.7z", FileMode.Create, FileAccess.Write)
                myCryptoStream = New CryptoStream(ResultFileStream, Decr, CryptoStreamMode.Write)
                Dim bytearrayinput(ProcessFileStream.Length - 1) As Byte
                ProcessFileStream.Read(bytearrayinput, 0, bytearrayinput.Length)
                myCryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length)
                myCryptoStream.Close()
                ProcessFileStream.Close()
                ResultFileStream.Close()


            Catch ex As Exception
                ResultFileStream.Close()
                ResultFileStream.Dispose()
                ProcessFileStream.Close()
                ProcessFileStream.Dispose()

                Dim x0 As New ProcessStartInfo
                x0.FileName = "CMD"
                x0.WorkingDirectory = path
                x0.Arguments = "/c crypt.exe -decrypt -key 5567D8DC6290EC7E78A13C4D6E6E6DF730D2B2048FC02120721BEC09EFD7A4DB -infile " & Chr(34) & path & "\Resource.iPack" & Chr(34) & " -outfile " & Chr(34) & path & "\Resource.7z" & Chr(34)
                x0.WindowStyle = ProcessWindowStyle.Hidden
                Dim xtract100 As New Process()
                xtract100.StartInfo = x0
                xtract100.Start()
                xtract100.WaitForExit()

            End Try
        ElseIf My.Computer.FileSystem.DirectoryExists(path & "\Resource Files") Then
            Dim takeown As New ProcessStartInfo
            takeown.FileName = path & "\7z.exe"
            takeown.Arguments = "a -mx=9 " & Chr(34) & "Resource.7z" & Chr(34) & " " & Chr(34) & "Resource Files" & Chr(34)
            takeown.WindowStyle = ProcessWindowStyle.Hidden
            takeown.WorkingDirectory = path
            Dim pp1 As New Process()
            pp1.StartInfo = takeown
            pp1.Start()
            pp1.WaitForExit()

        End If
        Try
            My.Computer.FileSystem.RenameFile(path & "\Resource.iPack", "Resource2.iPack")
            My.Computer.FileSystem.DeleteFile(path & "\Resource2.iPack")
        Catch ex As Exception

        End Try
        Try
            '  My.Computer.FileSystem.DeleteFile(path & "\Resource.iPack")
            Dim myDESProvider As DESCryptoServiceProvider = New DESCryptoServiceProvider()
            myDESProvider.Key = ASCIIEncoding.ASCII.GetBytes("B3DM60P7")
            myDESProvider.IV = ASCIIEncoding.ASCII.GetBytes("B3DM60P7")
            '  Dim myICryptoTransform As ICryptoTransform = myDESProvider.CreateEncryptor(myDESProvider.Key, myDESProvider.IV)
            Dim Decr As ICryptoTransform = myDESProvider.CreateEncryptor(myDESProvider.Key, myDESProvider.IV)
            ProcessFileStream = New FileStream(path & "\Resource.7z", FileMode.Open, FileAccess.Read)
            ResultFileStream = New FileStream(path & "\Resource.iPack", FileMode.Create, FileAccess.Write)
            myCryptoStream = New CryptoStream(ResultFileStream, Decr, CryptoStreamMode.Write)
            Dim bytearrayinput(ProcessFileStream.Length - 1) As Byte
            ProcessFileStream.Read(bytearrayinput, 0, bytearrayinput.Length)
            myCryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length)
            myCryptoStream.Close()
            ProcessFileStream.Close()
            ResultFileStream.Close()
        Catch ex As Exception

        End Try
   
        Try
            Dim objWriter2 As New System.IO.StreamWriter(path & "\Config.txt")
            objWriter2.Write(";!@Install@!UTF-8!" & vbNewLine & "Title=" & Chr(34) & "Unpacking" & Chr(34) & vbNewLine & "GUIMode=" & Chr(34) & "1" & Chr(34) & vbNewLine & "GUIFlags=" & Chr(34) & "1+8" & Chr(34) & vbNewLine & "InstallPath=" & Chr(34) & "%PROGRAMFILES%\\" & pack & Chr(34) & vbNewLine & "OverwriteMode=" & Chr(34) & "2" & Chr(34) & vbNewLine & "RunProgram=" & Chr(34) & "iPack_Installer.exe" & Chr(34) & vbNewLine & "ExtractTitle=" & Chr(34) & "Unpacking" & Chr(34) & vbNewLine & "ExtractDialogText=" & Chr(34) & Chr(34) & vbNewLine & "ExtractCancelText=" & Chr(34) & "Cancel" & Chr(34))
            objWriter2.Write(vbNewLine & ";!@InstallEnd@!")
            objWriter2.Close()
            Dim upgradeWriter As New System.IO.StreamWriter(path & "\upgrade.bat")
            upgradeWriter.Write("cd " & Chr(34) & path & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("ATTRIB +H " & Chr(34) & loc & "\" & "iPack_Installer.exe.config" & Chr(34) & " /S /D")
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("7z a -mx=9 " & Chr(34) & "Resource.7z" & Chr(34) & " " & Chr(34) & "Resource Files" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            'upgradeWriter.Write("crypt.exe -encrypt -key 5567D8DC6290EC7E78A13C4D6E6E6DF730D2B2048FC02120721BEC09EFD7A4DB -infile  " & Chr(34) & "Resource.7z" & Chr(34) & " -outfile " & Chr(34) & "Resource.iPack" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("7z a -mx=9 " & Chr(34) & pack & ".7z" & Chr(34) & " " & Chr(34) & "Resource.iPack" & Chr(34) & " " & Chr(34) & "Setup files-iPack" & Chr(34) & " " & Chr(34) & "iPack_Installer.exe" & Chr(34) & " " & Chr(34) & "iPack_Installer.exe.config" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("copy /b 7zS.sfx + Config.txt + " & Chr(34) & pack & ".7z" & Chr(34) & " " & Chr(34) & pack & ".exe" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("del " & Chr(34) & pack & ".7z" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("del " & Chr(34) & "Config.txt" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("del " & Chr(34) & "7zS.sfx" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("del " & Chr(34) & "Resource.7z" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("del " & Chr(34) & "iPack_Installer.exe" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("del " & Chr(34) & "crypt.exe" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("del " & Chr(34) & "Resource.iPack2" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("del " & Chr(34) & "Resource.iPack" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("del " & Chr(34) & "iPack_Installer.exe.config" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("del " & Chr(34) & "7z.exe" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("RD /s /q " & Chr(34) & "Resource Files" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("RD /s /q " & Chr(34) & "Setup-files iPack" & Chr(34))
            upgradeWriter.Write(vbNewLine)
            '   upgradeWriter.Write("pause")
            upgradeWriter.Write(vbNewLine)
            upgradeWriter.Write("DEL " & Chr(34) & "%~f0" & Chr(34))
            upgradeWriter.Close()
            Threading.Thread.Sleep(500)

            Try
                My.Computer.FileSystem.DeleteFile(path & "\Setup files-iPack\old.config")
            Catch ex As Exception

            End Try
            Dim takeown As New ProcessStartInfo
            takeown.FileName = "upgrade.bat"
            takeown.WorkingDirectory = path
            takeown.WindowStyle = ProcessWindowStyle.Hidden
            Dim pp1 As New Process()
            pp1.StartInfo = takeown
            pp1.Start()
            pp1.WaitForExit()

            Threading.Thread.Sleep(1000)
            Dim x00 As New ProcessStartInfo
            x00.FileName = path & "\ResHacker.exe"
            x00.Arguments = "-addoverwrite " & Chr(34) & path & "\" & pack & ".exe" & Chr(34) & ", " & Chr(34) & path & "\" & pack & ".exe" & Chr(34) & ", " & Chr(34) & path & "\ver.res" & Chr(34) & " ,,,"
            x00.WindowStyle = ProcessWindowStyle.Hidden
            Dim xtract0100 As New Process()
            xtract0100.StartInfo = x00
            xtract0100.Start()
            xtract0100.WaitForExit()

            Threading.Thread.Sleep(1000)

            Dim xxx As New ProcessStartInfo
            xxx.FileName = path & "\ResHacker.exe"
            xxx.Arguments = "-addoverwrite " & Chr(34) & path & "\" & pack & ".exe" & Chr(34) & ", " & Chr(34) & path & "\" & pack & ".exe" & Chr(34) & ", " & Chr(34) & path & "\Icon.ico" & Chr(34) & ", ICONGROUP , 101,"
            xxx.WindowStyle = ProcessWindowStyle.Hidden
            Dim x45t100 As New Process()
            x45t100.StartInfo = xxx
            x45t100.Start()
            x45t100.WaitForExit()
            Threading.Thread.Sleep(1000)
            Dim x210 As New ProcessStartInfo
            x210.FileName = path & "\upx.exe"
            x210.Arguments = Chr(34) & Chr(34) & " " & Chr(34) & path & "\" & pack & ".exe" & Chr(34)
            x210.WindowStyle = ProcessWindowStyle.Hidden
            Dim x2tract10 As New Process()
            x2tract10.StartInfo = x210
            x2tract10.Start()
            x2tract10.WaitForExit()
            My.Computer.FileSystem.DeleteDirectory(path & "\Setup files-iPack", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch ex As Exception

        End Try


    End Sub
    
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        PictureBox4.Hide()
        Label4.Text = "-"
        Try
            My.Computer.FileSystem.DeleteFile(path & "\ResHacker.exe")
            My.Computer.FileSystem.DeleteFile(path & "\upx.exe")
            My.Computer.FileSystem.DeleteFile(path & "\ResHacker.ini")
            My.Computer.FileSystem.DeleteFile(path & "\ResHacker.log")
            My.Computer.FileSystem.DeleteFile(path & "\ver.res")
            My.Computer.FileSystem.DeleteFile(path & "\Icon.ico")
            My.Computer.FileSystem.DeleteFile(path & "\old.config")
            My.Computer.FileSystem.DeleteFile(path & "\iPack_Installer.exe.config")
        Catch ex As Exception

        End Try
        Try
            My.Computer.FileSystem.RenameFile(path & "\" & pack & ".exe", pack & "_upgraded.exe")
            My.Computer.FileSystem.MoveFile(path & "\" & pack & "_upgraded.exe", TextBox2.Text & "\" & pack & "_upgraded.exe", True)
            MessageBox.Show("Upgrade complete.", "iPack Upgrade", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TextBox1.Text = ""
            TextBox2.Text = ""
            PictureBox1.Enabled = True
            PictureBox2.Enabled = True
            PictureBox3.Enabled = True
            PictureBox4.Hide()
            Label4.Text = "-"
            Try
                My.Computer.FileSystem.DeleteDirectory(path, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception

            End Try

            Me.Close()

        Catch ex As Exception
            Try

                Dim pi As New ProcessStartInfo
                pi.FileName = "CMD"
                pi.WorkingDirectory = path
                pi.WindowStyle = ProcessWindowStyle.Hidden
                pi.Arguments = "/c move /y " & Chr(34) & path & "\" & pack & "_upgraded.exe" & Chr(34) & " " & Chr(34) & TextBox2.Text & Chr(34)
                pi.Verb = "runas"
                Process.Start(pi)
                MessageBox.Show("Upgrade complete.", "iPack Upgrade", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TextBox1.Text = ""
                TextBox2.Text = ""
                PictureBox1.Enabled = True
                PictureBox2.Enabled = True
                PictureBox3.Enabled = True
                PictureBox4.Hide()
                Label4.Text = "-"
                My.Computer.FileSystem.DeleteDirectory(path, FileIO.DeleteDirectoryOption.DeleteAllContents)
                Me.Close()
            Catch exx As Exception
                MessageBox.Show("Couldn't move upgraded iPack to " & TextBox2.Text, "iPack Upgrade", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim p As New ProcessStartInfo
                p.FileName = "CMD"
                p.WorkingDirectory = path
                p.Arguments = "/c cd " & Chr(34) & path & Chr(34) & " && start."
                p.WindowStyle = ProcessWindowStyle.Hidden
                '  My.Computer.FileSystem.RenameFile(TextBox2.Text & "\" & pack & ".exe", pack & "_upgraded.exe")
                Process.Start(p)
                ' MessageBox.Show("Upgrade complete.", "iPack Upgrade", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TextBox1.Text = ""
                TextBox2.Text = ""
                PictureBox1.Enabled = True
                PictureBox2.Enabled = True
                PictureBox3.Enabled = True
                PictureBox4.Hide()
                Label4.Text = "-"
                Me.Close()
            End Try

            '  MessageBox.Show(ex.ToString)
        End Try

    End Sub
End Class