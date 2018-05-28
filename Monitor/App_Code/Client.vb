Imports Microsoft.VisualBasic
Imports System.Net.Sockets
Imports System.Text

Public Class Client
    Public Event Connected(ByVal Sender As Client)
    Public Event Disconnected(ByVal Sender As Client)
    Public Event ValueReceived(ByVal Sender As Client, ByVal Data As String)
    Public Event NameReceived(ByVal Sender As Client)

    Private ReceivedText As New StringBuilder()
    Private MarData(1024) As Byte
    Private TCPClient As TcpClient
    Private MGuid As Guid = Guid.NewGuid()

    Public Property Name As String
    Public Property LastValue As String

    Public ReadOnly Property ID() As String
        Get
            Return MGuid.ToString
        End Get
    End Property

    Public ReadOnly Property Client() As TcpClient
        Get
            Return TCPClient
        End Get
    End Property

    Public Sub New(ByVal Client As TcpClient)
        TCPClient = Client
        RaiseEvent Connected(Me)
        TCPClient.GetStream.BeginRead(MarData, 0, 1024, AddressOf DoReceive, Nothing)
    End Sub

    Private Sub ReceiveName(ByVal Ar As IAsyncResult)

    End Sub

    Private Sub DoReceive(ByVal Ar As IAsyncResult)
        Dim IntCount As Integer

        Try
            SyncLock TCPClient.GetStream
                IntCount = TCPClient.GetStream.EndRead(Ar)
            End SyncLock

            If IntCount < 1 Then
                RaiseEvent Disconnected(Me)
                Exit Sub
            End If

            BuildString(MarData, 0, IntCount)

            SyncLock TCPClient.GetStream
                TCPClient.GetStream.BeginRead(MarData, 0, 1024, AddressOf DoReceive, Nothing)
            End SyncLock

        Catch e As Exception
            RaiseEvent Disconnected(Me)
        End Try

    End Sub

    Private Sub BuildString(ByVal Bytes() As Byte, ByVal offset As Integer, ByVal count As Integer)
        Dim intIndex As Integer

        For intIndex = offset To offset + count - 1
            If Bytes(intIndex) = 13 Then

                If Name Is Nothing Then
                    Name = ReceivedText.ToString()
                    RaiseEvent NameReceived(Me)
                Else
                    LastValue = ReceivedText.ToString()
                End If

                ReceivedText = New StringBuilder()

            Else
                ReceivedText.Append(ChrW(Bytes(intIndex)))
            End If
        Next
    End Sub

End Class
