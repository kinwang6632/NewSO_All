
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
Public Class ChangeClctEn
    'Inherits DomainService
    Implements IDisposable
    Private Shared _ChangeClct As CableSoft.SO.BLL.Billing.ChangeClctEn.ChangeClctEn

    Private result As New RIAResult()
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)

        _ChangeClct = New CableSoft.SO.BLL.Billing.ChangeClctEn.ChangeClctEn(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Public Function GetCompCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangeClct.GetCompCode().DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangeClct.Dispose()
        End Try
        Return result

    End Function
    Public Function GetAllData(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangeClct.GetAllData
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangeClct.Dispose()
        End Try
        Return result
    End Function
    Public Function GetClctEn(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangeClct.GetClctEn.DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangeClct.Dispose()
        End Try
        Return result
    End Function
    Public Function GetStrtCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangeClct.GetStrtCode.DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangeClct.Dispose()
        End Try
        Return result
    End Function
    Public Function GetServiceType(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangeClct.GetServiceType.DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangeClct.Dispose()
        End Try
        Return result
    End Function
    Public Function GetMduId(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangeClct.GetMduId.DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangeClct.Dispose()
        End Try
        Return result
    End Function
    Public Function GetGroupData(ByVal LoginInfo As LoginInfo,
                                 ByVal RefNo As Integer, ByVal dsWhere As String) As RIAResult
        Dim ds As DataSet = Nothing
        Dim dsResult As DataSet = Nothing
        Try
            InitClass(LoginInfo)
            ds = Silverlight.DataSetConnector.Connector.FromXml(dsWhere)
            dsResult = _ChangeClct.GetGroupData(RefNo, ds.Tables(0)).DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsResult, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dsResult)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
            End If
            If dsResult IsNot Nothing Then
                dsResult.Dispose()
            End If
            _ChangeClct.Dispose()
        End Try
        Return result
    End Function
    Public Function ChkAuthority(ByVal LoginInfo As LoginInfo, ByVal RefNo As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _ChangeClct.ChkAuthority(RefNo)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangeClct.Dispose()
            _ChangeClct = Nothing
        End Try
        Return result
    End Function
    Public Function GetClctStrtGroupData(ByVal LoginInfo As LoginInfo,
                                         ByVal ClctEnStr As String, ByVal StrtCodeStr As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangeClct.GetClctStrtGroupData(ClctEnStr, StrtCodeStr).DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangeClct.Dispose()
        End Try
        Return result
    End Function
    Public Function Execute(ByVal LoginInfo As LoginInfo,
                             ByVal RefNo As Integer,
                            ByVal GroupByStr As Integer,
                            ByVal ModifyType As Integer,
                            ByVal dtPara As String,
                            ByVal ModifyData As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(ModifyData)
            Dim dsPara As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dtPara)
            result.ResultBoolean = (_ChangeClct.Execute(ds.Tables(0), dsPara.Tables(0), RefNo, GroupByStr, ModifyType) = 0)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangeClct.Dispose()
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

