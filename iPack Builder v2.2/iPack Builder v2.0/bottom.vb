Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Public Class bottom
    Public i, os, pack, lalign, author, mainheading, path, language, adv, header, logo, ico, splash, lisc, pv, fv, cpr, silent As String
    Public modification As Boolean = False
    Dim ProcessFileStream As FileStream
    Dim ResultFileStream As FileStream
    Dim myCryptoStream As CryptoStream
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Hide()
        Form1.TextBox1.Show()
        Form1.PictureBox4.Show()
        Form1.RadioButton1.Show()
        Form1.RadioButton2.Show()
        Form1.RadioButton3.Show()
        Form1.back_btn.Show()

    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.Image = My.Resources.new2
    End Sub

    Private Sub PictureBox1_MouseHover(sender As Object, e As EventArgs) Handles PictureBox1.MouseHover
        PictureBox9.Show()

    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Image = My.Resources.new1
        PictureBox9.Hide()

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        PictureBox7.Left = PictureBox5.Left
        PictureBox7.Show()
        PictureBox1.Hide()
        PictureBox2.Hide()
        PictureBox3.Hide()
        PictureBox4.Hide()
        'PictureBox5.Show()
        'PictureBox6.Show()
        TextBox1.Show()
        PictureBox8.Show()
        back_btn.Show()


    End Sub

    Private Sub PictureBox2_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox2.MouseEnter
        PictureBox2.Image = My.Resources.modi2
    End Sub

    Private Sub PictureBox2_MouseHover(sender As Object, e As EventArgs) Handles PictureBox2.MouseHover
        PictureBox10.Show()

    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.Image = My.Resources.modi
        PictureBox10.Hide()

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        upgrade.Show()
    End Sub

    Private Sub PictureBox3_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox3.MouseEnter
        PictureBox3.Image = My.Resources.upg2
        If upgrade.Visible = True Then
            PictureBox3.Image = My.Resources.upg1d
        End If
    End Sub

    Private Sub PictureBox3_MouseHover(sender As Object, e As EventArgs) Handles PictureBox3.MouseHover
        PictureBox11.Show()

    End Sub

    Private Sub PictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox3.MouseLeave
        PictureBox3.Image = My.Resources.upg1
        If upgrade.Visible = True Then
            PictureBox3.Image = My.Resources.upg1d
        End If
        PictureBox11.Hide()

    End Sub

    Private Sub bottom_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = Form1.Width
    End Sub

    Private Sub bottom_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Form1.Form1_MouseDown(Nothing, Nothing)

    End Sub

    Private Sub bottom_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        Form1.Form1_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub bottom_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        Form1.Form1_MouseUp(Nothing, Nothing)
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Dim site As New ProcessStartInfo
        site.FileName = ("http://mrbladedesigns.com/ipack-submission/")
        Process.Start(site)
    End Sub

    Private Sub PictureBox4_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox4.MouseEnter
        PictureBox4.Image = My.Resources.sub2
    End Sub

    Private Sub PictureBox4_MouseHover(sender As Object, e As EventArgs) Handles PictureBox4.MouseHover
        PictureBox12.Show()

    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox4.MouseLeave
        PictureBox4.Image = My.Resources.sub1
        PictureBox12.Hide()

    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        If Not OpenFileDialog1.FileName = "" Then
            TextBox1.Text = OpenFileDialog1.FileName
            PictureBox6.Show()
            TextBox1.Hide()
            back_btn.Hide()
            PictureBox7.Hide()
            PictureBox8.Hide()
            PictureBox6.Show()
            PictureBox5.Show()
            BackgroundWorker1.RunWorkerAsync()


        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        i = upgrade.rand()
        modification = True
        path = My.Computer.FileSystem.SpecialDirectories.Temp & "\" & i
       
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

        Catch ex As Exception

        End Try


        If My.Computer.FileSystem.FileExists(path & "\Resource.iPack") Then
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
                xtract7z()

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
                xtract7z()

            End Try

           

        End If

        Try
            Threading.Thread.Sleep(1000)
        Catch ex As Exception

        End Try
    End Sub

    Public Function xtract7z()
        Try
            Dim schedule As New ProcessStartInfo
            schedule.FileName = "7z.exe"
            schedule.WorkingDirectory = path
            schedule.Arguments = "x -y -bd " & Chr(34) & path & "\Resource.7z" & Chr(34)
            schedule.WindowStyle = ProcessWindowStyle.Hidden
            Dim sc As New Process()
            sc.StartInfo = schedule
            sc.Start()
            sc.WaitForExit()
        Catch ex As Exception

        End Try
    End Function
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        If Not My.Computer.FileSystem.FileExists(path & "\Setup files-iPack\Configuration.config") Then

            PictureBox6.Hide()
            PictureBox7.Hide()
            PictureBox8.Hide()
            PictureBox1.Show()
            PictureBox2.Show()
            PictureBox3.Show()
            PictureBox4.Show()
            PictureBox5.Hide()

            TextBox1.Text = ""
            MessageBox.Show("Not a valid iPack, cannot continue !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Try
                My.Computer.FileSystem.DeleteDirectory(path, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception

            End Try
            Return

        End If


        Me.Hide()
        Form1.TextBox1.Show()
        Form1.PictureBox4.Show()
        Form1.RadioButton1.Show()
        Form1.RadioButton2.Show()
        Form1.RadioButton3.Show()
        Try
            Form1.TextBox1.Text = upgrade.GetSettingItem(path & "\Setup files-iPack\Configuration.config", "Pack Name")
            os = upgrade.GetSettingItem(path & "\Setup files-iPack\Configuration.config", "OS")
            author = upgrade.GetSettingItem(path & "\Setup files-iPack\Configuration.config", "Author")
            mainheading = upgrade.GetSettingItem(path & "\Setup files-iPack\Configuration.config", "Heading")
            lalign = upgrade.GetSettingItem(path & "\Setup files-iPack\Configuration.config", "Lalign")
            silent = upgrade.GetSettingItem(path & "\Setup files-iPack\Configuration.config", "Silent")
            language = upgrade.GetSettingItem(path & "\Setup files-iPack\Configuration.config", "Language")
            adv = upgrade.GetSettingItem(path & "\Setup files-iPack\Configuration.config", "Ad")
            If os = 0 Then
                Form1.i = 1
                Form1.RadioButton3.Checked = True
            ElseIf os = 1 Then
                Form1.RadioButton1.Checked = True
            ElseIf os = 2 Then
                Form1.RadioButton2.Checked = True
            End If

            header = (path & "\Setup files-iPack\header.png")
            logo = (path & "\Setup files-iPack\logo.png")
            If My.Computer.FileSystem.FileExists(path & "\Setup files-iPack\splash.png") Then
                splash = (path & "\Setup files-iPack\splash.png")
            Else
                splash = Nothing
            End If

            ico = (path & "\Icon.ico")
            lisc = My.Computer.FileSystem.ReadAllText(path & "\Setup files-iPack\License.txt")
            pv = FileVersionInfo.GetVersionInfo(OpenFileDialog1.FileName).ProductVersion
            fv = FileVersionInfo.GetVersionInfo(OpenFileDialog1.FileName).FileVersion
            cpr = FileVersionInfo.GetVersionInfo(OpenFileDialog1.FileName).LegalCopyright

        Catch ex As Exception

        End Try

        Try
            ' Dim Files As String() = My.Computer.FileSystem.GetFiles(Form1.bottompart.path & "\Resource Files")
            Dim f0 As String
            For Each FileName As String In My.Computer.FileSystem.GetFiles(path & "\Resource Files")

                Form1.aa = FileName

                If FileName.Contains(".res") Then

                    f0 = IO.Path.GetFileName(FileName)
                    If Form1.maintab.ListBox1.Items.Contains(f0) Then
                        Form1.maintab.exist = "yes"
                    Else
                        Form1.maintab.ListBox1.Items.Add(f0)
                        Form1.maintab.ListBox2.Items.Add(FileName)
                        main_tab.hasfiles = "yes"

                    End If
                End If

            Next
        Catch ex As Exception

        End Try


        Try
            Try
                For Each Dirr In My.Computer.FileSystem.GetDirectories(path & "\Resource Files\Program Files", FileIO.SearchOption.SearchTopLevelOnly)
                    Form1.foldertab.ListBox1.Items.Add(Dirr)
                Next
            Catch ex As Exception

            End Try

            Form1.foldertab.ListBox1.Sorted = True
            Form1.foldertab.ListBox1.Refresh()
        Catch ex As Exception

        End Try


        Try
            Form1.interfacetab.TextBox1.Text = header


            Dim image As Image = Nothing
            ' Check if textbox has a value
            If header <> String.Empty Then
                image = image.FromFile(Form1.bottompart.header)
            End If
            ' Check if image exists
            If Not image Is Nothing Then
                Interface_tab.imgThumb = image.GetThumbnailImage(348, 54, Nothing, New IntPtr())
                Me.Refresh()
            End If

            Form1.interfacetab.TextBox3.Text = logo

            Dim image1 As Image = Nothing
            ' Check if textbox has a value
            If logo <> String.Empty Then
                image1 = image.FromFile(Form1.bottompart.logo)
            End If
            ' Check if image exists
            If Not image1 Is Nothing Then
                Interface_tab.logoThumb = image1.GetThumbnailImage(89, 218, Nothing, New IntPtr())
                Me.Refresh()
            End If

            If Not splash = Nothing Then
                Form1.interfacetab.RadioButton1.Checked = True
                Form1.interfacetab.TextBox4.Text = splash
                Try
                    Dim imagex As Image = Nothing
                    ' Check if textbox has a value
                    If Form1.interfacetab.TextBox4.Text <> String.Empty Then
                        imagex = image.FromFile(Form1.interfacetab.TextBox4.Text)
                    End If
                    ' Check if image exists
                    If Not imagex Is Nothing Then
                        Interface_tab.splashThumb = imagex.GetThumbnailImage(198, 98, Nothing, New IntPtr())
                        Me.Refresh()
                    End If
                Catch ex As Exception

                End Try

            End If

            Form1.interfacetab.TextBox2.Text = ico
            Dim image11 As Image = Nothing
            ' Check if textbox has a value
            If Form1.interfacetab.TextBox2.Text <> String.Empty Then
                image11 = image.FromFile(Form1.interfacetab.TextBox2.Text)
            End If
            ' Check if image exists
            If Not image11 Is Nothing Then
                Interface_tab.icoThumb = image11.GetThumbnailImage(25, 25, Nothing, New IntPtr())
                Me.Refresh()
            End If


        Catch ex As Exception

        End Try

        Try

            If modification = True Then
                Form1.installertab.TextBox0.Text = author
                Form1.installertab.TextBox5.Text = mainheading
                Form1.installertab.TextBox6.Text = lisc
                Form1.installertab.TextBox1.Text = fv
                Form1.installertab.TextBox2.Text = cpr
                Form1.installertab.TextBox4.Text = pv
                If lalign = 2 Then
                    Form1.installertab.ComboBox1.SelectedIndex = 1
                ElseIf lalign = 3 Then
                    Form1.installertab.ComboBox1.SelectedIndex = 2
                Else
                    Form1.installertab.ComboBox1.SelectedIndex = 0
                End If


                If language = 1 Then
                    Form1.installertab.ComboBox2.SelectedIndex = 1
                ElseIf language = 2 Then
                    Form1.installertab.ComboBox2.SelectedIndex = 2
                ElseIf language = 3 Then
                    Form1.installertab.ComboBox2.SelectedIndex = 3
                Else
                    Form1.installertab.ComboBox2.SelectedIndex = 0
                End If

            End If
        Catch ex As Exception

        End Try
        Try
            If Not silent = "" Or Not silent = Nothing Then
                If silent = 1 Then
                    Form1.CheckBox3.Checked = True
                Else
                    Form1.CheckBox3.Checked = False

                End If
            Else
                Form1.CheckBox3.Checked = False
            End If
        Catch ex As Exception

        End Try
        Try
            If Not adv = "" Or Not adv = Nothing Then
                If adv = 1 Then
                    Form1.CheckBox2.Checked = True
                Else
                    Form1.CheckBox2.Checked = False

                End If
            Else
                Form1.CheckBox2.Checked = True
            End If
        Catch ex As Exception

        End Try
        Try
            If My.Computer.FileSystem.FileExists(path & "\Setup files-iPack\Theme\Theme.xml") Then
                Form1.themetab1.TextBox1.Text = (path & "\Setup files-iPack\Theme")
                Form1.themetab1.themepath = (path & "\Setup files-iPack\Theme")
                Dim root As XElement = XDocument.Load(Form1.themetab1.themepath & "\Theme.xml").Root
                Dim val As String
                Try
                    val = (root.Element("name").Value)
                    If Not val = Nothing Then
                        Form1.themetab1.Label2.Text = val
                    Else
                        Form1.themetab1.Label2.Text = "None"
                    End If
                Catch ex As Exception

                End Try


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub back_btn_Click(sender As Object, e As EventArgs) Handles back_btn.Click
        back_btn.Hide()
        PictureBox1.Show()
        PictureBox2.Show()
        PictureBox3.Show()
        PictureBox4.Show()
        PictureBox8.Hide()
        PictureBox5.Hide()
        PictureBox6.Hide()
        PictureBox7.Hide()
        TextBox1.Text = ""
        TextBox1.Hide()

    End Sub

    Private Sub back_btn_MouseHover(sender As Object, e As EventArgs) Handles back_btn.MouseHover
        back_btn.Image = My.Resources.gback1
    End Sub

    Private Sub back_btn_MouseLeave(sender As Object, e As EventArgs) Handles back_btn.MouseLeave
        back_btn.Image = My.Resources.gback
    End Sub
End Class
