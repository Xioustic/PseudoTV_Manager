Imports System
Imports System.IO

Public Class Form6

    Public User As String = Environment.UserName
    Private VideoDBFile As String
    Private AddonDBFile As String
    Private Version As String

    Private VideoDBFileDialog As New OpenFileDialog()
    Private SettingsFileDialog As New OpenFileDialog()
    Private AddonDBFileDialog As New OpenFileDialog()

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If XbmcVersion.SelectedIndex = 0 Then
            Version = "XBMC"
        Else
            Version = "Kodi"
        End If



        VideoDBFileDialog.InitialDirectory = "C:\Users\" & User & "\AppData\Roaming\" & Version & "\userdata\Database"

        VideoDBFileDialog.DefaultExt = ""
        VideoDBFileDialog.Filter = "SqliteDB files (*.db)|*MyVideos*.db"

        VideoDBFileDialog.ShowDialog()

        Dim Filename = VideoDBFileDialog.FileName
        If VideoDBFileDialog.FileName <> "OpenFileDialog1" Then
            TextBox1.Text = Filename
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SettingsFileDialog.InitialDirectory = "C:\Users\" & User & "\AppData\Roaming\" & Version & "\userdata\addon_data\script.pseudotv.live"

        SettingsFileDialog.DefaultExt = ""
        SettingsFileDialog.Filter = "Settings2 files (*.xml)|**.xml"

        SettingsFileDialog.ShowDialog()

        Dim Filename = SettingsFileDialog.FileName

        If SettingsFileDialog.FileName <> "OpenFileDialog1" Then
            TextBox2.Text = Filename
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        AddonDBFileDialog.InitialDirectory = "C:\Users\" & User & "\AppData\Roaming\" & Version & "\userdata\Database"

        AddonDBFileDialog.DefaultExt = ""
        AddonDBFileDialog.Filter = "SqliteDB files (*.db)|*Addons*.db"

        AddonDBFileDialog.ShowDialog()

        Dim Filename = AddonDBFileDialog.FileName

        If AddonDBFileDialog.FileName <> "OpenFileDialog1" Then
            TextBox8.Text = Filename
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim SettingsFile As String = Application.StartupPath() & "\" & "Settings.txt"

        'See if there's already a text file in place, if not then create one.

        If System.IO.File.Exists(SettingsFile) = False Then
            System.IO.File.Create(SettingsFile)
        End If

        'Verify that all 3 files indeed exist at least
        If System.IO.File.Exists(TextBox1.Text) = True And System.IO.File.Exists(TextBox2.Text) = True And System.IO.File.Exists(TextBox8.Text) = True Then

            If TestMYSQLite(TextBox1.Text) = True Then

                'Save them to the settings file
                Dim FilePaths As String = "0" & " | " & TextBox1.Text & " | " & TextBox2.Text & " | " & TextBox8.Text
                SaveFile(SettingsFile, FilePaths)

                'Now, update the variables in the Main form with the proper paths
                Form1.DatabaseType = 0
                Form1.VideoDatabaseLocation = TextBox1.Text
                Form1.PseudoTvSettingsLocation = TextBox2.Text
                Form1.AddonDatabaseLocation = TextBox8.Text

                'Refresh everything
                Form1.RefreshALL()
                Form1.RefreshTVGuide()

                Me.Visible = False
                Form1.Focus()
            End If
        ElseIf TextBox3.Text <> "" And TextBox4.Text <> "" And TextBox6.Text <> "" And System.IO.File.Exists(TextBox2.Text) = True Then

            'server=localhost; user id=mike; password=12345; database=in_out

            Dim ConnectionString = "server=" & TextBox3.Text & "; user id=" & TextBox4.Text & "; password=" & TextBox5.Text & "; database=" & TextBox6.Text & "; port=" & TextBox7.Text

            If TestMYSQL(ConnectionString) = True Then

                Dim FilePaths As String = "1" & " | " & ConnectionString & " | " & TextBox2.Text & " | " & TextBox8.Text
                SaveFile(SettingsFile, FilePaths)

                'Now, update the variables in the Main form with the proper paths
                Form1.DatabaseType = 1
                Form1.MySQLConnectionString = ConnectionString
                Form1.PseudoTvSettingsLocation = TextBox2.Text
                Form1.AddonDatabaseLocation = TextBox8.Text

                'Refresh everything
                Form1.RefreshALL()
                Form1.RefreshTVGuide()

                Me.Visible = False
                Form1.Focus()
            End If
        End If



    End Sub

    Private Sub Form6_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Form1.VideoDatabaseLocation <> "" Then
            TextBox1.Text = Form1.VideoDatabaseLocation
            TextBox2.Text = Form1.PseudoTvSettingsLocation
            TextBox8.Text = Form1.AddonDatabaseLocation
        End If

        If Form1.MySQLConnectionString <> "" Then
            Dim SplitString() = Split(Form1.MySQLConnectionString, ";")

            TextBox2.Text = Form1.PseudoTvSettingsLocation
            TextBox3.Text = Split(SplitString(0), "server=")(1)
            TextBox4.Text = Split(SplitString(1), "user id=")(1)
            TextBox5.Text = Split(SplitString(2), "password=")(1)
            TextBox6.Text = Split(SplitString(3), "database=")(1)
            TextBox7.Text = Split(SplitString(4), "port=")(1)
        End If

    End Sub

End Class