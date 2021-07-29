
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
Public Class TextFileOut
    'Inherits DomainService
    Implements IDisposable
    Private _TextFileOut As CableSoft.BLL.Dynamic.TextFileOut.DynamicText

    Private result As New RIAResult()
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        '    _TextFileOut = New CableSoft.BLL.Dynamic.DynUpdateGrid.DynUpdateGrid(LoginInfo.ConvertTo(LoginInfo))
        _TextFileOut = New CableSoft.BLL.Dynamic.TextFileOut.DynamicText(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    '2017/01/09 Jacky 增加一個一次取回設定檔的method
    Public Function GetSettingData(ByVal LoginInfo As LoginInfo, ByVal SysProgramId As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _TextFileOut.GetSettingData(SysProgramId)
            If result.ResultBoolean AndAlso result.ResultDataSet IsNot Nothing Then
                '_result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(result.ResultDataSet)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(result.ResultDataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            End If
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _TextFileOut.Dispose()
        End Try
        Return result
    End Function
    Public Function GetCompCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _TextFileOut.GetCompCode().DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _TextFileOut.Dispose()
        End Try
        Return result
    End Function
    Public Function QueryDynCondition(ByVal LoginInfo As LoginInfo, SysProgramId As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _TextFileOut.QueryDynCondition(SysProgramId)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _TextFileOut.Dispose()
        End Try
        Return result
    End Function
    Public Function QueryDynTextOut(ByVal LoginInfo As LoginInfo, ByVal SysProgramId As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _TextFileOut.QueryDynTextOut(SysProgramId)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _TextFileOut.Dispose()
        End Try
        Return result
    End Function
    Public Function ExecuteResvTime(ByVal LoginInfo As LoginInfo,
                            ByVal SysProgramId As String, ByVal AutoSerialNo As Integer, ByVal dsConditions As String, ByVal SEQNO As Long) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsConditions)

            result.ResultXML = _TextFileOut.Execute(SysProgramId, AutoSerialNo, True, SEQNO, objDS)
            result.ResultBoolean = True
            If result.ResultXML = "-3" Then
                result.DownloadFileName = Nothing
                result.ResultBoolean = False
                result.ErrorCode = -3
                result.ErrorMessage = "無任何資料！"
            Else
                '2017/01/09 Jacky 加傳下載檔案位置
                result.DownloadFileName = result.ResultXML.Split(";"c)(0)
            End If
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _TextFileOut.Dispose()
        End Try
        Return result
    End Function
    Public Function Execute(ByVal LoginInfo As LoginInfo,
                            ByVal SysProgramId As String, ByVal AutoSerialNo As Integer, ByVal dsConditions As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsConditions)

            result.ResultXML = _TextFileOut.Execute(SysProgramId, AutoSerialNo, objDS)
            result.ResultBoolean = True
            If result.ResultXML = "-3" Then
                result.DownloadFileName = Nothing
                result.ResultBoolean = False
                result.ErrorCode = -3
                result.ErrorMessage = "無任何資料！"
            Else
                '2017/01/09 Jacky 加傳下載檔案位置
                result.DownloadFileName = result.ResultXML.Split(";"c)(0)
            End If
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _TextFileOut.Dispose()
        End Try
        Return result
    End Function
    Public Function ChkAuthority(ByVal LoginInfo As LoginInfo, ByVal SysProgramId As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _TextFileOut.ChkAuthority(SysProgramId)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _TextFileOut.Dispose()
        End Try
        Return result
    End Function
    Public Function Test(ByVal LoginInfo As LoginInfo) As RIAResult
        InitClass(LoginInfo)
        Try
            result.ResultBoolean = True
        Catch ex As Exception

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

