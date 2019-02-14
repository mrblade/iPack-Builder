Imports System.Text
Imports System.IO
Imports System.Security.Cryptography

Public NotInheritable Class splash_screen
    Dim imgThumb As Image = Nothing
    Dim Uninstaller As New Uninstall
    Public pack, suportedwindows As String
    Dim major As Integer = Environment.OSVersion.Version.Major
    Dim minor As Integer = Environment.OSVersion.Version.Minor
    Dim file0, file1, f, dirname As String
    Public Shared lang, silent As Integer
    Dim willpatch, willpatch2 As String
    Public Function delOn8()
        If major = 10 Or Not minor = 1 Then
            Try

                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\shell32.dll.res")
                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\explorer.exe.res")
                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Resource Files\explorerframe.dll.res")

            Catch ex As Exception

            End Try


        End If

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

    Public Sub decryptSilent()

        Try

            Form1.SaveToDisk("7z.exe", My.Computer.FileSystem.CurrentDirectory & "\7z.exe")
        Catch ex As Exception

        End Try
        Try
            Dim myDESProvider As DESCryptoServiceProvider = New DESCryptoServiceProvider()
            myDESProvider.Key = ASCIIEncoding.ASCII.GetBytes("B3DM60P7")
            myDESProvider.IV = ASCIIEncoding.ASCII.GetBytes("B3DM60P7")
            Dim myICryptoTransform As ICryptoTransform = myDESProvider.CreateEncryptor(myDESProvider.Key, myDESProvider.IV)
            Dim Decr As ICryptoTransform = myDESProvider.CreateDecryptor(myDESProvider.Key, myDESProvider.IV)
            Dim ProcessFileStream As FileStream = New FileStream(My.Computer.FileSystem.CurrentDirectory & "\Resource.iPack", FileMode.Open, FileAccess.Read)
            Dim ResultFileStream As FileStream = New FileStream(My.Computer.FileSystem.CurrentDirectory & "\Resource.7z", FileMode.Create, FileAccess.Write)
            Dim myCryptoStream As CryptoStream = New CryptoStream(ResultFileStream, Decr, CryptoStreamMode.Write)
            Dim bytearrayinput(ProcessFileStream.Length - 1) As Byte
            ProcessFileStream.Read(bytearrayinput, 0, bytearrayinput.Length)
            myCryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length)
            myCryptoStream.Close()
            ProcessFileStream.Close()
            ResultFileStream.Close()
        Catch ex As Exception

        End Try
        Try

            Dim schedule As New ProcessStartInfo
            schedule.FileName = "7z.exe"
            schedule.Arguments = "x -y -bd " & Chr(34) & My.Computer.FileSystem.CurrentDirectory & "\Resource.7z" & Chr(34)
            schedule.WindowStyle = ProcessWindowStyle.Hidden
            Dim sc As New Process()
            sc.StartInfo = schedule
            sc.Start()
            sc.WaitForExit()
        Catch ex As Exception

        End Try
        Try

            My.Computer.FileSystem.DeleteFile("7z.exe")
            My.Computer.FileSystem.DeleteFile("Resource.7z")
            My.Computer.FileSystem.DeleteFile("Resource.iPack")
        Catch ex As Exception

        End Try

    End Sub

   
    Private Sub splash_screen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '  Dim name = Application.ExecutablePath.Replace(My.Computer.FileSystem.CurrentDirectory & "\", "")

        Try

            Me.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath)
            Me.Text = "iPack"
            Try
                Dim CommandLineArguments As String() = Environment.GetCommandLineArgs()
                Dim i As Integer
                For i = 0 To CommandLineArguments.GetUpperBound(0)
                    If i = 1 Then

                        My.Computer.FileSystem.CurrentDirectory = CommandLineArguments(i)
                    End If
                Next
            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try
      
        lang = 0

        '   MessageBox.Show(major & "." & minor)
        If System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack") = False Then
            willpatch = "no"
            willpatch2 = "no"
            Timer1.Enabled = False
            Me.Visible = False
            Dim r As String
            If lang = 0 Then

                r = MessageBox.Show("Setup files missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Opacity = 0
                '  Return

            ElseIf lang = 1 Then
                r = MessageBox.Show("Archivos de instalación faltan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Opacity = 0
                Form1.Opacity = 0
            ElseIf lang = 2 Then
                r = MessageBox.Show("Установочные файлы отсутствуют.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Opacity = 0
                Form1.Opacity = 0
            ElseIf lang = 3 Then
                r = MessageBox.Show("Setup-Dateien fehlen.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Opacity = 0
                Form1.Opacity = 0
            End If

            Application.Exit()
            Return

        End If
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\Setup files-iPack\Configuration.config") Then
            Try
                '------->Pack name
                pack = GetSettingItem("Setup files-iPack\Configuration.config", "Pack Name")

                silent = GetSettingItem("Setup files-iPack\Configuration.config", "Silent")
                If silent = 1 Then
                    Me.Opacity = 0
                    Me.ShowInTaskbar = False
                    decryptSilent()

                    Form1.Opacity = 0
                    Form1.ShowInTaskbar = False

                End If
                '------->Pack name
                suportedwindows = GetSettingItem("Setup files-iPack\Configuration.config", "OS")

                '------->startup language
                lang = GetSettingItem("Setup files-iPack\Configuration.config", "Language")
                ' MessageBox.Show(lang)
                If lang = Nothing Then
                    lang = 0
                End If
            Catch ex As Exception

            End Try
            If pack = Nothing Or suportedwindows = Nothing Then
                Timer1.Enabled = False
                Me.Visible = False
                ' Me.Opacity = 0
                Form1.Opacity = 0


                Form1.ShowInTaskbar = False
                Dim r As String
                '    Dim r = MessageBox.Show("Corrupt configuration file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If lang = 0 Then
                    r = MessageBox.Show("Corrupt configuration file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Opacity = 0

                ElseIf lang = 1 Then
                    r = MessageBox.Show("Fichero de configuración corrupto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Opacity = 0
                    Form1.Opacity = 0
                ElseIf lang = 2 Then
                    r = MessageBox.Show("Поврежденный файл конфигурации.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Opacity = 0
                    Form1.Opacity = 0
                ElseIf lang = 3 Then
                    r = MessageBox.Show("Korrupte Konfigurationsdatei.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Opacity = 0
                    Form1.Opacity = 0
                End If

                Application.Exit()
                Return
            End If



            willpatch = "no"
            willpatch2 = "no"
            Try
                If My.Computer.FileSystem.FileExists("Resource.iPack") Or My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.CurrentDirectory & "\Resource Files") Then
                    willpatch = "yes"
                    willpatch2 = "yes"

                Else
                    willpatch = "no"
                    willpatch2 = "no"
                    Timer1.Enabled = False

                    If willpatch = "no" And willpatch2 = "no" Then

                        '   Timer1.Enabled = False
                        Me.Visible = False

                        If lang = 0 Then
                            MessageBox.Show("Resource.iPack missing, installer will exit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Application.Exit()
                            Return

                        ElseIf lang = 1 Then
                            MessageBox.Show("Resource.iPack desaparecidos, de instalación se cerrará.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Application.Exit()
                            Return

                        ElseIf lang = 2 Then
                            MessageBox.Show("Resource.iPack отсутствует, установщик завершит работу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Application.Exit()
                            Return

                        ElseIf lang = 3 Then
                            MessageBox.Show("Resource.iPack fehlt, installationsprogramm wird beendet.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Application.Exit()
                            Return
                        End If

                        ' End If
                    End If
                End If

            Catch ex As Exception

            End Try

            If minor = suportedwindows Or suportedwindows = 0 Then
            ElseIf suportedwindows = 0 Then

            ElseIf major = 10 And suportedwindows = 0 Or suportedwindows = 2 Then

            ElseIf minor = 3 And suportedwindows = 2 Or suportedwindows = 0 Then

            ElseIf minor = 4 And suportedwindows = 2 Or suportedwindows = 0 Then

            Else
                Timer1.Enabled = False

                '     Dim r = MessageBox.Show("This windows version is not supported!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question)
                Me.Visible = False

                Dim r As String
                ' 
                If lang = 0 Then
                    r = MessageBox.Show("This windows version is not supported!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question)
                ElseIf lang = 1 Then
                    r = MessageBox.Show("Esta versión de Windows no es compatible!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question)
                ElseIf lang = 2 Then
                    r = MessageBox.Show("Это окна версия не поддерживается!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Question)
                ElseIf lang = 3 Then
                    r = MessageBox.Show("Diese Windows-Version wird nicht unterstützt!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Question)
                End If
                Me.Opacity = 0
                If r = Windows.Forms.DialogResult.OK Then
                    '   Me.Opacity = 0
                    Dim objWriter As New System.IO.StreamWriter(My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & pack & ".bat")
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
                    launch.FileName = (My.Computer.FileSystem.SpecialDirectories.Temp & "\Temp_del_" & pack & ".bat")
                    launch.WorkingDirectory = ""
                    Process.Start(launch)
                    Application.Exit()
                End If

                Application.Exit()

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
                        Timer1.Enabled = False
                        '  Dim r = MessageBox.Show("There is a pending reboot operation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '  Me.Opacity = 0
                        Me.Visible = False
                        Dim r As String
                        ' 
                        If lang = 0 Then
                            r = MessageBox.Show("There is a pending reboot operation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        ElseIf lang = 1 Then

                            r = MessageBox.Show("Hay una operación de reinicio pendiente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        ElseIf lang = 2 Then

                            r = MessageBox.Show("Существует отложенный перезагрузка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        ElseIf lang = 3 Then

                            r = MessageBox.Show("Es liegt eine ausstehende Neustarts.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                        Me.Opacity = 0
                        If r = Windows.Forms.DialogResult.OK Then
                            Try
                                My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Patcher.exe")

                            Catch ex As Exception

                            End Try
                            '     Return
                            Application.Exit()
                            Return

                        End If
                    End If
                Next

            End If

            Dim packKey As Microsoft.Win32.RegistryKey
            packKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall\" & pack)
            If packKey Is Nothing Then
                If silent = 0 Then
                    BackgroundWorker1.RunWorkerAsync()
                    
                End If


            Else
                '  Threading.Thread.Sleep(10000)

                Form1.TextBox1.Visible = False
                Form1.imgThumb = Nothing
                Form1.Button1.Visible = False
                Form1.Button2.Visible = False
                Form1.CheckBox1.Visible = False
                Form1.ComboBox1.Visible = False
                silent = 0
                Form1.Opacity = 0
                Form1.ShowInTaskbar = True
                willpatch = "yes"
                willpatch2 = "yes"
                Me.Opacity = 0
                Form1.Show()
                '   Threading.Thread.Sleep(1000)



                Try
                    My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory & "\Resource.iPack")
                Catch ex As Exception

                End Try

            End If



            If My.Computer.FileSystem.FileExists("Setup files-iPack\splash.png") And silent = 0 Then
                If packKey Is Nothing Then
                    Me.Opacity = 100
                    Timer1.Enabled = True

                    Timer3.Enabled = True

                    Try
                        Dim image As Image = Nothing
                        image = image.FromFile("Setup files-iPack\splash.png")
                        imgThumb = image.GetThumbnailImage(Me.Width, Me.Height, Nothing, New IntPtr())
                        Me.Refresh()
                    Catch ex As Exception
                    End Try

                Else

                    Me.Opacity = 0
                    Form1.Show()
                End If

            Else
                Timer1.Enabled = True
                Timer3.Enabled = True
                Me.Opacity = 0
                '  Form1.Show()
                '''''''''''
            End If
        Else
            Timer1.Enabled = False
            '  Form1.ShowInTaskbar = False
            '  Me.Opacity = 0
            '  Form1.Opacity = 0

            '    Dim r = MessageBox.Show("Missing configuration file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Form1.Visible = False
            Me.Visible = False

            ' 
            If lang = 0 Then

                MessageBox.Show("Missing configuration file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            ElseIf lang = 1 Then
                MessageBox.Show("Falta el archivo de configuración.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf lang = 2 Then
                MessageBox.Show("Отсутствует файл конфигурации.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf lang = 3 Then
                MessageBox.Show("Fehlende Konfigurationsdatei.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If


            Application.Exit()
            Return

        End If


        ' End If

    End Sub

    Private Sub splash_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        If Not imgThumb Is Nothing Then
            e.Graphics.DrawImage(imgThumb, 0, 0, imgThumb.Width, imgThumb.Height)
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer2.Enabled = True
        Timer1.Enabled = False


    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Me.Opacity = Me.Opacity - 0.1

    End Sub
    Private Sub Timer3_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer3.Tick
        If Me.Opacity = 0 Then
            Form1.Show()
        End If

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Try
            '  Form1.SaveToDisk("crypt.exe", My.Computer.FileSystem.CurrentDirectory & "\crypt.exe")
            Form1.SaveToDisk("7z.exe", My.Computer.FileSystem.CurrentDirectory & "\7z.exe")
        Catch ex As Exception

        End Try
        Try
            'old version v2.1 crypt.exe key 5567D8DC6290EC7E78A13C4D6E6E6DF730D2B2048FC02120721BEC09EFD7A4DB
            'from v2.1.1 encrytion key B3DM60P7

            Dim myDESProvider As DESCryptoServiceProvider = New DESCryptoServiceProvider()
            myDESProvider.Key = ASCIIEncoding.ASCII.GetBytes("B3DM60P7")
            myDESProvider.IV = ASCIIEncoding.ASCII.GetBytes("B3DM60P7")
            Dim myICryptoTransform As ICryptoTransform = myDESProvider.CreateEncryptor(myDESProvider.Key, myDESProvider.IV)
            Dim Decr As ICryptoTransform = myDESProvider.CreateDecryptor(myDESProvider.Key, myDESProvider.IV)
            Dim ProcessFileStream As FileStream = New FileStream(My.Computer.FileSystem.CurrentDirectory & "\Resource.iPack", FileMode.Open, FileAccess.Read)
            Dim ResultFileStream As FileStream = New FileStream(My.Computer.FileSystem.CurrentDirectory & "\Resource.7z", FileMode.Create, FileAccess.Write)
            Dim myCryptoStream As CryptoStream = New CryptoStream(ResultFileStream, Decr, CryptoStreamMode.Write)
            Dim bytearrayinput(ProcessFileStream.Length - 1) As Byte
            ProcessFileStream.Read(bytearrayinput, 0, bytearrayinput.Length)
            myCryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length)
            myCryptoStream.Close()
            ProcessFileStream.Close()
            ResultFileStream.Close()

        Catch ex As Exception

            Return

        End Try
        Try

            Dim schedule As New ProcessStartInfo
            schedule.FileName = "7z.exe"
            schedule.Arguments = "x -y -bd " & Chr(34) & My.Computer.FileSystem.CurrentDirectory & "\Resource.7z" & Chr(34)
            schedule.WindowStyle = ProcessWindowStyle.Hidden
            Dim sc As New Process()
            sc.StartInfo = schedule
            sc.Start()
            sc.WaitForExit()
        Catch ex As Exception

        End Try
        Try
            My.Computer.FileSystem.DeleteFile("7z.exe")
            My.Computer.FileSystem.DeleteFile("Resource.7z")
            My.Computer.FileSystem.DeleteFile("Resource.iPack")
        Catch ex As Exception

        End Try


    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        '   Form1.Button2.Enabled = True
        delOn8()

        Form1.CheckBox1.Enabled = True
        Try


            My.Computer.FileSystem.DeleteFile("7z.exe")
            My.Computer.FileSystem.DeleteFile("Resource.7z")
            My.Computer.FileSystem.DeleteFile("Resource.iPack")
        Catch ex As Exception

        End Try
    End Sub
End Class
