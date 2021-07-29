
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
Public Class CopyOrder
    'Inherits DomainService
    Implements IDisposable

    Private _CopyOrder As CableSoft.SO.BLL.Order.CopyOrder.CopyOrder
    Private result As New RIAResult()
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _CopyOrder = New CableSoft.SO.BLL.Order.CopyOrder.CopyOrder(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Public Function CanEdit(ByVal LoginInfo As LoginInfo, ByVal OrderNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Return _CopyOrder.CanEdit(OrderNo)
            'Return RIAResult.Convert(_CopyOrder.CanEdit(OrderNo))
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _CopyOrder.Dispose()
        End Try
    End Function
    Public Function ChkAllSNOCitemCount(ByVal LoginInfo As LoginInfo, ByVal CustId As Integer, ByVal AllSNO As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result.ResultBoolean = True
            result = _CopyOrder.ChkAllSNOCitemCount(CustId, AllSNO)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _CopyOrder.Dispose()
        End Try
        Return result
    End Function
    Public Function GetCloseWipData(ByVal LoginInfo As LoginInfo, ByVal OrderNo As String, ByVal CustId As Integer, _
                                    ByVal IncludeOrder As Boolean, ByVal WorkType As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            result.ResultBoolean = True
            result = _CopyOrder.GetCloseWipData(OrderNo, CustId, IncludeOrder, WorkType)
            If result.ResultBoolean Then
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(result.ResultDataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            End If

            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _CopyOrder.Dispose()
        End Try
        Return result
    End Function
    Public Function ChkReturnCode(ByVal LoginInfo As LoginInfo, ByVal OrderNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result.ResultBoolean = True
            Dim dt As DataTable = _CopyOrder.ChkReturnCode(OrderNo)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _CopyOrder.Dispose()
        End Try
        Return result
    End Function
    Public Function GetCustId(ByVal LoginInfo As LoginInfo, ByVal OrderNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _CopyOrder.GetCustId(OrderNo)
            result.ResultBoolean = True
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            'Return RIAResult.Convert(_CopyOrder.Execute(OrderNo))
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _CopyOrder.Dispose()
        End Try
        Return result
    End Function
    Public Function ExecuteHtml(ByVal LoginInfo As LoginInfo, _
                           ByVal OrderNo As String, ByVal AllSNO As String, ByVal WorkType As Integer, _
                          ByVal ExecTab As String, ByVal ShouldRegPriv As Boolean, _
                         ByVal CustId As Integer, ByVal IsOrderTurnSend As Boolean, ByVal OtherTable As String) As RIAResult
        Dim dsExecTab As DataSet = Nothing
        Dim dtOther As DataTable = Nothing
        Dim dtExecTab As DataTable = Nothing
        Try
            InitClass(LoginInfo)
            If Not String.IsNullOrEmpty(ExecTab) Then
                dsExecTab = Silverlight.DataSetConnector.Connector.FromXml(ExecTab)
                dtExecTab = dsExecTab.Tables(0)
            End If


            If Not String.IsNullOrEmpty(OtherTable) Then
                dtOther = Silverlight.DataSetConnector.Connector.FromXml(OtherTable).Tables(0)
            End If
            result = _CopyOrder.Execute(OrderNo, AllSNO, WorkType, dtExecTab,
                                        ShouldRegPriv, CustId, IsOrderTurnSend, dtOther)
            If result.ResultBoolean Then
                result.ResultBoolean = True
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(result.ResultDataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            End If

            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _CopyOrder.Dispose()
            If dsExecTab IsNot Nothing Then
                dsExecTab.Dispose()
                dsExecTab = Nothing
            End If
            If dtOther IsNot Nothing Then
                dtOther.Dispose()
                dtOther = Nothing
            End If
        End Try
        Return result
    End Function

    Public Function Execute(ByVal LoginInfo As LoginInfo, ByVal OrderNo As String) As RIAResult

        Try
            InitClass(LoginInfo)
            result = _CopyOrder.Execute(OrderNo)
            If result.ResultBoolean Then
                result.ResultBoolean = True
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(result.ResultDataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            End If

            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _CopyOrder.Dispose()
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

