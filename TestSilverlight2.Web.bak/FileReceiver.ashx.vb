Imports System.Web
Imports System.Web.Services
Imports System
Imports System.IO

Imports System.Web.Hosting


Public Class FileReceiver
    Implements System.Web.IHttpHandler
    Private _httpContxt As HttpContext
    Private _filePath As String
    Private _fileName As String
    Private _pkgCnt As Int32
    Private _pkgNum As Int32
    Private _transType As String
    Private _1stChunk As Boolean
    Private _lstChunk As Boolean
    Private _cancel As Boolean
    Private _sttByt As Int64
    Private Const _tmpExt As String = "_tmp"
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        'context.Response.ContentType = "text/plain"
        'context.Response.Write("Hello World!")
        Try
            'context.Response.ContentType = "text/plain"
            'context.Response.Write("Hello World!")
            'If context.Request.InputStream.Length <= 0 Then Throw New ArgumentException("無上傳檔案!")
            _httpContxt = context
            ProcessQueryString()
            ProcessFile()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ProcessQueryString()
        Try
            Dim nvc As NameValueCollection = _httpContxt.Request.QueryString
            _filePath = Uri.UnescapeDataString(nvc("filePath"))
            _fileName = Uri.UnescapeDataString(nvc("fileName"))
            If nvc("tranX") Is Nothing OrElse nvc("tranX") = 0 Then
                _pkgCnt = Int32.Parse(nvc("packageCount"))
                _pkgNum = Int32.Parse(nvc("packageNumber"))
                _transType = 0
            Else
                _transType = nvc("tranX")
                '_transType => 1. HttpWebRequest , 2. WebClient
                _1stChunk = If(String.IsNullOrEmpty(nvc("first")), True, Boolean.Parse(nvc("first")))
                _lstChunk = If(String.IsNullOrEmpty(nvc("last")), True, Boolean.Parse(nvc("last")))
                _sttByt = If(String.IsNullOrEmpty(nvc("offset")), 0, Int64.Parse(nvc("offset")))
                _cancel = If(String.IsNullOrEmpty(nvc("cancel")), 0, Boolean.Parse(nvc("cancel")))
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ProcessFile()
        Dim binRdr As BinaryReader = Nothing
        Dim binWtr As BinaryWriter = Nothing
        Dim dstStm As FileStream = Nothing
        Try
            Dim svrFile As String = GetSvrFile()
            Select Case _transType
                Case 0
                    If _httpContxt.Request.InputStream.Length > 0 Then
                        Dim mode As FileMode =
                            If(File.Exists(svrFile) AndAlso _pkgNum > 0, FileMode.Append, FileMode.Create)
                        binRdr = New BinaryReader(_httpContxt.Request.InputStream)
                        binWtr = New BinaryWriter(File.Open(svrFile, mode))
                        Dim buffer(4095) As Byte
                        Dim byt2rd As Int32 = binRdr.Read(buffer, 0, buffer.Length)
                        Do While byt2rd <> 0
                            binWtr.Write(buffer, 0, byt2rd)
                            byt2rd = binRdr.Read(buffer, 0, buffer.Length)
                        Loop
                    End If
                Case 1
                    Dim tmpFile As String = String.Concat(svrFile, _tmpExt)
                    If _httpContxt.Request.InputStream.Length > 0 AndAlso Not _cancel Then
                        If _1stChunk AndAlso _sttByt = 0 Then
                            If File.Exists(tmpFile) Then File.Delete(tmpFile)
                            If File.Exists(svrFile) Then File.Delete(svrFile)
                            dstStm = File.Open(tmpFile, FileMode.Create)
                        Else
                            dstStm = File.Open(tmpFile, FileMode.Append)
                        End If
                        Dim srcStm As Stream = _httpContxt.Request.InputStream
                        Dim buffer(4095) As Byte
                        Dim byt2rd As Int32 = srcStm.Read(buffer, 0, buffer.Length)
                        While byt2rd <> 0
                            dstStm.Write(buffer, 0, byt2rd)
                            byt2rd = srcStm.Read(buffer, 0, buffer.Length)
                        End While
                        If dstStm IsNot Nothing Then
                            dstStm.Close()
                            dstStm.Dispose()
                        End If
                        If _lstChunk Then File.Move(tmpFile, svrFile)
                    Else
                        If File.Exists(tmpFile) Then
                            Try
                                File.Delete(tmpFile)
                            Catch ex As Exception
                                Diagnostics.Debug.WriteLine(ex.ToString)
                            End Try
                        End If
                    End If
                Case 2
                Case Else
            End Select
        Catch ex As Exception
            Throw
        Finally
            If binWtr IsNot Nothing Then
                binWtr.Close()
                binWtr.Dispose()
            End If
            If binRdr IsNot Nothing Then
                binRdr.Close()
                binRdr.Dispose()
            End If
        End Try
    End Sub
    Private Function GetSvrFile() As String
        'HostingEnvironment.ApplicationPhysicalPath
        GetSvrFile = ""
        Try
            Return _httpContxt.Server.MapPath(Path.Combine(_filePath, Path.GetFileName(_fileName)))
        Catch ex As Exception
            Throw
        End Try
    End Function
    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class