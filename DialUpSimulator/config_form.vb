Public Class config_form
#Region "Disable X"
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            Const CS_DBLCLKS As Int32 = &H8
            Const CS_NOCLOSE As Int32 = &H200
            cp.ClassStyle = CS_DBLCLKS Or CS_NOCLOSE
            Return cp
        End Get
    End Property
#End Region

    Private Sub config_form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'make list
        For Each i In netAdapters
            ComboBox3.Items.Add(i)
        Next
        If main.Label1.Text.Contains("first start") = False Then
            For Each l In ComboBox3.Items
                If l.ToString.Contains(My.Settings.myNetAdapterName) Then
                    ComboBox3.SelectedItem = l
                End If
            Next
        End If
        ComboBox1.SelectedIndex = My.Settings.myMaxBaud
        'check registry
        Try
            Dim key As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run")
            If key.GetValue(My.Application.Info.ProductName).ToString.Contains("-disable") = True Then
                CheckBox5.Checked = True
                If key.GetValue(My.Application.Info.ProductName).ToString.Contains("-disablesilent") = True Then
                    CheckBox6.Checked = True
                Else
                    CheckBox6.Checked = False
                End If
            Else
                CheckBox5.Checked = False
                CheckBox6.Checked = False
            End If
        Catch ex As Exception
            Debug.Print("error registry")
            CheckBox5.Checked = False
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        My.Settings.myMaxBaud = ComboBox1.SelectedIndex
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox3.SelectedIndex <= -1 Then
            MsgBox("You must select a Network Adapter to work with!", MsgBoxStyle.OkOnly, "Warning")
        Else
            Dim b As String = ComboBox3.SelectedItem.ToString.Substring(0, ComboBox3.SelectedItem.ToString.LastIndexOf(" ("))
            If b.EndsWith(" ") Then
                b = b.Length - 1
            End If
            Dim s As String = ComboBox3.SelectedItem.ToString.Substring(ComboBox3.SelectedItem.ToString.LastIndexOf(" ("))
            Dim s1 As String = s.Replace(" ", "")
            Dim s2 As String = s1.Replace("(", "")
            Dim s3 As String = s2.Replace(")", "")
            My.Settings.myNetAdapterDeviceId = CInt(s3)
            My.Settings.myNetAdapterName = b
            My.Settings.Save()
            main.Label1.Text = My.Settings.myNetAdapterName
            If checkNetState(CSng(s3)) >= 0 Then
                main.Button2.Text = "Disc&onnect"
                mainForm_disconnect()
            Else
                main.Button2.Text = "C&onnect"
                mainForm_connect()
            End If
            'Autostart stuff
            Dim key As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.CreateSubKey("Software\Microsoft\Windows\CurrentVersion\Run")
            Dim key1 As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.CreateSubKey("Software\Microsoft\Windows\CurrentVersion\Run")

            If CheckBox5.Checked = True Then
                If CheckBox6.Checked = False Then
                    key.SetValue(My.Application.Info.ProductName, """" & System.Reflection.Assembly.GetEntryAssembly.Location & """ -disable")
                Else
                    key.SetValue(My.Application.Info.ProductName, """" & System.Reflection.Assembly.GetEntryAssembly.Location & """ -disablesilent")
                End If
            Else
                Try
                    key1.DeleteValue(My.Application.Info.ProductName, False)
                Catch ex As Exception
                End Try
            End If
            'END
            Me.Dispose()
            Me.Close()
        End If

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = 0 Then
            ComboBox2.SelectedIndex = -1
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        Select Case CheckBox5.Checked
            Case True
                CheckBox6.Enabled = True
            Case False
                CheckBox6.Enabled = False
        End Select
    End Sub
End Class