Imports System
Imports System.Collections.Generic
Imports System.Management
Imports System.Threading
Imports DialUpEmulator.My.Resources
Module Module_networking
Public Class NetworkAdapter 


#Region "Private Properties"


        ''' <summary> 
        ''' Network Adapter DeviceId 
        ''' </summary> 
        ''' <remarks></remarks> 
        Private _intDeviceId As Integer
        Property DeviceId As Integer
            Get
                Return _intDeviceId
            End Get
            Set(ByVal value As Integer)
                _intDeviceId = value
            End Set


        End Property


        ''' <summary> 
        ''' Network Adapter ProductName 
        ''' </summary> 
        ''' <remarks></remarks> 
        Private _strName As String
        Property Name() As String
            Get
                Return _strName
            End Get
            Set(ByVal value As String)
                _strName = value
            End Set
        End Property


        ''' <summary> 
        ''' Network Adapter Connection Status 
        ''' </summary> 
        ''' <remarks></remarks> 
        Private _intNetConnectionStatus As Integer
        Property NetConnectionStatus As Integer
            Get
                Return _intNetConnectionStatus
            End Get
            Set(ByVal value As Integer)
                _intNetConnectionStatus = value
            End Set
        End Property


        ''' <summary> 
        ''' Network Adapter NetEnabled 
        ''' </summary> 
        ''' <remarks></remarks> 
        Private _intNetEnabled As Integer
        Property NetEnabled As Integer
            Get
                Return _intNetEnabled
            End Get
            Set(ByVal value As Integer)
                _intNetEnabled = value
            End Set
        End Property


        ''' <summary> 
        ''' Network Adapter Connection Status Descriptions 
        ''' </summary> 
        ''' <remarks></remarks> 
        Public Shared SaNetConnectionStatus As String() = New String() _
        {
            NetConnectionStatus0,
            NetConnectionStatus1,
            NetConnectionStatus2,
            NetConnectionStatus3,
            NetConnectionStatus4,
            NetConnectionStatus5,
            NetConnectionStatus6,
            NetConnectionStatus7,
            NetConnectionStatus8,
            NetConnectionStatus9,
            NetConnectionStatus10,
            NetConnectionStatus11,
            NetConnectionStatus12
        }


        ''' <summary> 
        ''' Enum The Result Of Enable Or Disable Network Adapter 
        ''' </summary> 
        ''' <remarks></remarks> 
        Private Enum EnumEnableDisableResult
            Fail = -1
            Success = 1
            Unknow = 0
        End Enum


        ''' <summary> 
        ''' Enum The Network Adapter NetEnabled Status Values 
        ''' </summary> 
        ''' <remarks></remarks> 
        Private Enum EnumNetEnabledStatus
            Disabled = -1
            Enabled = 1
            Unknow = 0
        End Enum


#End Region


#Region "Construct NetworkAdapter"


        Public Sub New(ByVal deviceId As Integer,
                       ByVal name As String,
                       ByVal netEnabled As Integer,
                       ByVal netConnectionStatus As Integer)
            Me.DeviceId = deviceId
            Me.Name = name
            Me.NetEnabled = netEnabled
            Me.NetConnectionStatus = netConnectionStatus
        End Sub


#End Region

        Public Function getnet()

            Dim allNetworkAdapter As New List(Of NetworkAdapter)


            Dim oQuery As New ObjectQuery(
                "SELECT DeviceID,ProductName,Description,NetEnabled,NetConnectionStatus " _
                & "FROM Win32_NetworkAdapter WHERE Manufacturer <> 'Microsoft' ")
            Dim oSearcher As New ManagementObjectSearcher(oQuery)
            Dim oReturnCollection As ManagementObjectCollection = oSearcher.Get


            Dim mo As ManagementObject
            For Each mo In oReturnCollection
                NetEnabled = IIf(Convert.ToBoolean(mo.Item("NetEnabled").ToString), 1, -1)
                allNetworkAdapter.Add(
                    New NetworkAdapter(
                        Convert.ToInt32(mo.Item("DeviceID").ToString),
                        mo.Item("ProductName").ToString,
                        NetEnabled,
                        Convert.ToInt32(mo.Item("NetConnectionStatus").ToString)))
            Next


            oReturnCollection.Dispose()
            oSearcher.Dispose()


            Return allNetworkAdapter
        End Function
    End Class

End Module
