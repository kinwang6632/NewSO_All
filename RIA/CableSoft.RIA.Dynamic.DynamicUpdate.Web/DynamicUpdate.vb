
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
Public Class DynamicUpdate
    'Inherits DomainService
    Implements IDisposable
    Private _DynamicUpdate As CableSoft.BLL.Dynamic.DynamicUpdate.DynamicUpdate
    Private result As New RIAResult()
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _DynamicUpdate = New CableSoft.BLL.Dynamic.DynamicUpdate.DynamicUpdate(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Public Function Save(ByVal LoginInfo As LoginInfo,
                         ByVal EditMode As EditMode,
                         ByVal SysProgramId As String, ByVal dsSource As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsSource)
            result.ResultBoolean = True
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(
            '        _DynamicUpdate.Execute(EditMode, SysProgramId, ds))
            'result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(
            '        _DynamicUpdate.Execute(EditMode, SysProgramId, ds))
            result = _DynamicUpdate.Execute(EditMode, SysProgramId, ds)
            If result.ResultDataSet IsNot Nothing Then
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(result.ResultDataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)

            End If
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _DynamicUpdate.Dispose()
        End Try
        Return result
    End Function
    Public Function GetCompCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _DynamicUpdate.GetCompCode().DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _DynamicUpdate.Dispose()
        End Try
        Return result

    End Function
    Public Function CopyToOtherDB(ByVal LoginInfo As LoginInfo, ByVal sysProgramId As String, ByVal IsCover As Boolean,
                                  ByVal dsSource As String, ByVal dsCopyComps As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsSource)
            Dim dsComp As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsCopyComps)
            result = _DynamicUpdate.CopyToOtherDB(sysProgramId, IsCover, ds, dsComp)
        Catch ex As Exception
            result.ErrorCode = -999
            result.ErrorMessage = ex.ToString
        Finally
            _DynamicUpdate.Dispose()
            _DynamicUpdate = Nothing
        End Try
        Return result
    End Function
    Public Function Execute(ByVal LoginInfo As LoginInfo,
                            ByVal EditMode As EditMode,
                            ByVal SysProgramId As String,
                            ByVal dsSource As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsSource)

            result.ResultBoolean = True
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(
            '        _DynamicUpdate.Execute(EditMode, SysProgramId, ds))
            'result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(
            '            _DynamicUpdate.Execute(EditMode, SysProgramId, ds))
            result = _DynamicUpdate.Execute(EditMode, SysProgramId, ds)
            If result.ResultDataSet IsNot Nothing Then
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(result.ResultDataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            End If
        Catch ex As Exception
            If TypeOf ex Is OracleClient.OracleException Then
                If CType(ex, OracleClient.OracleException).Code = 1 Then
                    result.ErrorCode = -1
                    result.ErrorMessage = "PK值重複!"
                    result.ResultBoolean = False
                Else
                    ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
                    result.ResultBoolean = False
                    Return result
                End If
            Else
                ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
                result.ResultBoolean = False
                Return result
            End If

        Finally
            _DynamicUpdate.Dispose()
        End Try
        Return result
    End Function
    Public Function QueryEnvironment(ByVal LoginInfo As LoginInfo,
                                     ByVal EditMode As EditMode, ByVal SysProgramId As String) As RIAResult

        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _DynamicUpdate.QueryEnvironment(SysProgramId)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _DynamicUpdate.Dispose()
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

