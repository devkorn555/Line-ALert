Imports System.IO
Imports System.Net
Imports System.Text

Public Class Line

    Private _Token As String
    Public Property Token() As String
        Get
            Return _Token
        End Get
        Set(ByVal value As String)
            _Token = value
        End Set
    End Property

    Private _Message As String
    Public Property Message() As String
        Get
            Return _Message
        End Get
        Set(ByVal value As String)
            _Message = value
        End Set
    End Property

    Public Function SenMessage()
        If _Token = Nothing Then
            Return False
        ElseIf _Message = Nothing Then
            Return False
        Else
            Dim request = DirectCast(WebRequest.Create("https://notify-api.line.me/api/notify"), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.Headers.Add("Authorization", "Bearer " & _Token)

            Dim postData = String.Format("message={0}", _Message)
            Dim data = Encoding.UTF8.GetBytes(postData)
            request.AllowWriteStreamBuffering = True
            request.KeepAlive = False
            request.ContentLength = data.Length
            request.Credentials = CredentialCache.DefaultCredentials

            Dim stream As Stream
            stream = request.GetRequestStream()
            stream.Write(data, 0, data.Length)
            Return True
        End If
    End Function

End Class
