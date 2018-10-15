Option Explicit On
Module Module1
    Public myappname As String = "Dial Up Emulator"
    Public myappversnum As Integer = 200
    Public myappvers As String = "v" & (myappversnum / 1000).ToString.Replace(",", ".")
    Public myappfullname As String = myappname & " " & myappvers
    '
    Public netAdapters As New ArrayList

    'Copyright Stuff
    Public Function current_copyright(ByVal fromyear As String, Optional ByVal Copyright_Icon As Boolean = True, Optional ByVal After_Copyright_Text As String = "") As String
        Dim s As String = Date.Today.Year
        Dim p As String = ""
        Dim c As String = ""
        Select Case Copyright_Icon
            Case True
                c = " © "
            Case False
                c = " (c) "
        End Select
        p = fromyear & "-" & s & c & After_Copyright_Text
        Return p
    End Function

    Public Sub resetMySettings()
        My.Settings.myUsername = "user@world.com"
        My.Settings.myPassword = "myBeitenPassword"
        My.Settings.myMaxBaud = 10
        My.Settings.myNetAdapterName = "None"
        My.Settings.myNetAdapterDeviceId = 0

        My.Settings.Save()
    End Sub

    Public Function checkNetState(ByVal netId As Single)
        For Each i In NetworkAdapter.GetAllNetworkAdapter
            If i.DeviceId = netId Then
                Return i.NetEnabled
            End If
        Next
        Return Nothing
    End Function

    Public Sub mainForm_disconnect()
        main.TextBox1.Enabled = False
        main.TextBox2.Enabled = False
        main.TextBox3.Enabled = False
        main.CheckBox1.Enabled = False
        main.ComboBox1.Enabled = False
        main.Button2.Text = "Disc&onnect"
        main.Button3.Text = "&Cancel"
    End Sub
    Public Sub mainForm_connect()
        main.TextBox1.Enabled = True
        main.TextBox2.Enabled = True
        main.TextBox3.Enabled = True
        main.CheckBox1.Enabled = True
        main.ComboBox1.Enabled = True
        main.Button2.Text = "C&onnect"
        main.Button3.Text = "&Cancel"
    End Sub
End Module
