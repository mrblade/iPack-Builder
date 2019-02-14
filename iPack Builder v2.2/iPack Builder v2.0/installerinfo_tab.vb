Public Class installerinfo_tab
    Public Shared align As String
    Public Shared lang As String

    Private Sub installerinfo_tab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Left = Form1.PictureBox13.Right
        Label1.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label2.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label9.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label4.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label15.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label5.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label6.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label7.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label3.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        Label8.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        TextBox0.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        TextBox1.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        TextBox2.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        TextBox4.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        TextBox5.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        TextBox6.Font = CustomFont.GetInstance(9, FontStyle.Regular)
        ComboBox1.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0
        Try

     
        If Form1.bottompart.modification = True Then
            TextBox0.Text = Form1.bottompart.author
            TextBox5.Text = Form1.bottompart.mainheading
                TextBox6.Text = Form1.bottompart.lisc
                TextBox1.Text = Form1.bottompart.fv
                TextBox2.Text = Form1.bottompart.cpr
                TextBox4.Text = Form1.bottompart.pv
            If Form1.bottompart.lalign = 2 Then
                ComboBox1.SelectedIndex = 1
            ElseIf Form1.bottompart.lalign = 3 Then
                ComboBox1.SelectedIndex = 2
            Else
                ComboBox1.SelectedIndex = 0
            End If


                If Form1.bottompart.language = 1 Then
                    ComboBox2.SelectedIndex = 1
                ElseIf Form1.bottompart.language = 2 Then
                    ComboBox2.SelectedIndex = 2
                ElseIf Form1.bottompart.language = 3 Then
                    ComboBox2.SelectedIndex = 3
                Else
                    ComboBox2.SelectedIndex = 0
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        align = ComboBox1.Text

        If ComboBox1.SelectedIndex = 0 Then
            TextBox6.TextAlign = HorizontalAlignment.Left
        ElseIf ComboBox1.SelectedIndex = 1 Then
            TextBox6.TextAlign = HorizontalAlignment.Center
        Else
            TextBox6.TextAlign = HorizontalAlignment.Right
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = 0 Then
            lang = "0"
        ElseIf ComboBox2.SelectedIndex = 1 Then
            lang = "1"
        ElseIf ComboBox2.SelectedIndex = 2 Then
            lang = "2"
        ElseIf ComboBox2.SelectedIndex = 3 Then
            lang = "3"
        End If
    End Sub
End Class
