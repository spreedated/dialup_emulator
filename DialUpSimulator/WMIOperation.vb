Imports System
Imports System.Management

Public Class WMIOperation

    Public Shared Function WMIQuery(ByVal strwQuery As String) _
        As ManagementObjectCollection

        Dim oQuery As New ObjectQuery(strwQuery)

        Dim oSearcher As New ManagementObjectSearcher(oQuery)
        Return oSearcher.Get
    End Function

End Class