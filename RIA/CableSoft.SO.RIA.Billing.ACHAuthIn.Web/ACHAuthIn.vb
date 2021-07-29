
Option Compare Binary
Option Infer On
Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.ServiceModel.DomainServices.Hosting
Imports System.ServiceModel.DomainServices.Server
Imports CableSoft.BLL.Utility


'TODO: 建立包含應用程式邏輯的方法。
Public Class ACHAuthIn
    'Inherits DomainService
    Implements IDisposable
    Private _ACHAuthIn As CableSoft.SO.BLL.Billing.ACHAuthIn.ACHAuthIn
    Private result As New RIAResult()
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _ACHAuthIn = New CableSoft.SO.BLL.Billing.ACHAuthIn.ACHAuthIn(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Public Function ChkAuthority(ByVal LoginInfo As LoginInfo, ByVal Mid As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _ACHAuthIn.ChkAuthority(Mid)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If _ACHAuthIn IsNot Nothing Then
                _ACHAuthIn.Dispose()
                _ACHAuthIn = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function Launch(ByVal LoginInfo As LoginInfo,
                            ByVal dsInputData As String,
                            ByVal fileName As String,
                            ByVal StopDate1 As String,
                            ByVal StopDate2 As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsInputData)
            result = _ACHAuthIn.Launch(ds, fileName, StopDate1, StopDate2)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ACHAuthIn IsNot Nothing Then
                _ACHAuthIn.Dispose()
            End If
        End Try
        Return result
    End Function
    Public Function Execute(ByVal LoginInfo As LoginInfo,
                            ByVal dsInputData As String,
                            ByVal ContextString As String,
                            ByVal StopDate1 As String,
                            ByVal StopDate2 As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsInputData)
            result = _ACHAuthIn.Execute(ds, ContextString, StopDate1, StopDate2)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ACHAuthIn IsNot Nothing Then
                _ACHAuthIn.Dispose()
            End If
        End Try
        Return result
    End Function
    Public Function DeleteLogFile(ByVal LoginInfo As LoginInfo, ByVal FileName As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result.ResultBoolean = _ACHAuthIn.DeleteLogFile(FileName)
        Catch ex As Exception
            'ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = True
            Return result
        Finally
            If _ACHAuthIn IsNot Nothing Then
                _ACHAuthIn.Dispose()
            End If
        End Try
        Return result
    End Function
    Public Function QueryAllData(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ACHAuthIn.QueryAllData
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ACHAuthIn IsNot Nothing Then
                _ACHAuthIn.Dispose()
            End If
        End Try
        Return result
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: 處置 Managed 狀態 (Managed 物件)。
            End If

            ' TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下面的 Finalize()。
            ' TODO: 將大型欄位設定為 null。
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: 只有當上面的 Dispose(ByVal disposing As Boolean) 有可釋放 Unmanaged 資源的程式碼時，才覆寫 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 請勿變更此程式碼。在上面的 Dispose(ByVal disposing As Boolean) 中輸入清除程式碼。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' 由 Visual Basic 新增此程式碼以正確實作可處置的模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 請勿變更此程式碼。在以上的 Dispose 置入清除程式碼 (ByVal 視為布林值處置)。
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

