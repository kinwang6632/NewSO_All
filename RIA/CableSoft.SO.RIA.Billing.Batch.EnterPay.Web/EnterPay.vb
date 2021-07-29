
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
Public Class EnterPay
    'Inherits DomainService
    Implements IDisposable
    Private _EnterPay As CableSoft.SO.BLL.Billing.Batch.EnterPay.EnterPay
    Private result As New RIAResult()
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _EnterPay = New CableSoft.SO.BLL.Billing.Batch.EnterPay.EnterPay(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Public Function ChkAuthority(ByVal LoginInfo As LoginInfo, ByVal Mid As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _EnterPay.ChkAuthority(Mid)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If _EnterPay IsNot Nothing Then
                _EnterPay.Dispose()
                _EnterPay = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function WriteMsgForDownload(ByVal LoginInfo As LoginInfo, ByVal Msg As String) As RIAResult
        Try
            InitClass(LoginInfo)

            Return _EnterPay.WriteMsgForDownload(Msg)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If _EnterPay IsNot Nothing Then
                _EnterPay.Dispose()
            End If
        End Try
        Return result
    End Function
    Public Function ChkCloseDate(ByVal LoginInfo As LoginInfo,
                                 ByVal CloseDate As String, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Return _EnterPay.ChkCloseDate(CloseDate, ServiceType)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If _EnterPay IsNot Nothing Then
                _EnterPay.Dispose()
            End If
        End Try
        Return result

    End Function
    Public Function GetAllData(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _EnterPay.GetAllData
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds.Copy(), JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If _EnterPay IsNot Nothing Then
                _EnterPay.Dispose()
            End If
        End Try
        Return result
    End Function
    Public Function Test(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)

            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _EnterPay.Dispose()
        End Try
        Return result
    End Function
    Public Function GetCompCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _EnterPay.GetCompCode.DataSet
            'result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds)
            result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _EnterPay.Dispose()
        End Try
        Return result
    End Function
    Public Function GetCMCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _EnterPay.GetCMCode.DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _EnterPay.Dispose()
        End Try
        Return result
    End Function
    Public Function GetPTCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _EnterPay.GetPTCode.DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _EnterPay.Dispose()
        End Try
        Return result
    End Function
    Public Function GetClctEn(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _EnterPay.GetClctEn.DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _EnterPay.Dispose()
        End Try
        Return result
    End Function
    Public Function GetSTCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _EnterPay.GetSTCode.DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _EnterPay.Dispose()
        End Try
        Return result
    End Function
    Public Function GetTempAllInfo(ByVal LoginInfo As LoginInfo, ByVal EntryType As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _EnterPay.GetTempAllInfo(EntryType)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _EnterPay.Dispose()
        End Try
        Return result
    End Function
    Public Function GetTempInfo(ByVal LoginInfo As LoginInfo, ByVal EntryType As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _EnterPay.GetTempInfo(EntryType).DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _EnterPay.Dispose()
        End Try
        Return result
    End Function
    Public Function ChkCanCancel(ByVal LoginInfo As LoginInfo, ByVal CitemCode As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result.ResultBoolean = True
            result.ErrorMessage = _EnterPay.ChkCanCancel(CitemCode)
            If Not String.IsNullOrEmpty(result.ErrorMessage) Then
                result.ErrorCode = -1
                result.ResultBoolean = False
            End If
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            'dsResult.Dispose()
            _EnterPay.Dispose()
        End Try
        Return result
    End Function
    Public Function ImportExcel(ByVal LoginInfo As LoginInfo, ByVal FileName As String,
                                ByVal dsCitemPara As String, ByVal UCRefNo As Integer) As RIAResult
        Dim dsReturn As DataSet = Nothing
        Try
            InitClass(LoginInfo)
            Dim dsPara As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsCitemPara)
            dsReturn = _EnterPay.ImportExcel(FileName, dsPara.Tables(0), UCRefNo)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsReturn, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If dsReturn IsNot Nothing Then
                dsReturn.Dispose()
            End If
            If _EnterPay IsNot Nothing Then
                _EnterPay.Dispose()
            End If
        End Try
        Return result
    End Function
    Public Function Execute(ByVal LoginInfo As LoginInfo, ByVal EntryType As Integer, ByVal dsCitemPara As String) As RIAResult
        Dim dsReturn As DataSet = Nothing
        Dim dsSource As DataSet = Nothing
        Try
            InitClass(LoginInfo)
            dsSource = Silverlight.DataSetConnector.Connector.FromXml(dsCitemPara)
            dsReturn = _EnterPay.Execute(EntryType, dsSource.Tables(0))
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsReturn, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If dsReturn IsNot Nothing Then
                dsReturn.Dispose()
                dsReturn = Nothing
            End If
            If dsSource IsNot Nothing Then
                dsSource.Dispose()
                dsSource = Nothing
            End If
            If _EnterPay IsNot Nothing Then
                _EnterPay.Dispose()
                _EnterPay = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function CancelBillData(ByVal LoginInfo As LoginInfo, ByVal EntryType As Integer,
                                   ByVal BillNo As String, ByVal Item As Integer) As RIAResult
        Dim dsReturn As DataSet = Nothing
        Try
            InitClass(LoginInfo)
            dsReturn = _EnterPay.CancelBillData(EntryType, BillNo, Item)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsReturn, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If dsReturn IsNot Nothing Then
                dsReturn.Dispose()
            End If
            If _EnterPay IsNot Nothing Then
                _EnterPay.Dispose()
            End If
        End Try
        Return result
    End Function

    Public Function CancelAllBillData(ByVal LoginInfo As LoginInfo,
                                   ByVal EntryType As Integer) As RIAResult
        Dim dsReturn As DataSet = Nothing
        Try
            InitClass(LoginInfo)
            dsReturn = _EnterPay.CancelAllBillData(EntryType)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsReturn, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If dsReturn IsNot Nothing Then
                dsReturn.Dispose()
            End If
            If _EnterPay IsNot Nothing Then
                _EnterPay.Dispose()
            End If
        End Try
        Return result
    End Function
    Public Function CancelTempData(ByVal LoginInfo As LoginInfo,
                                  ByVal EntryType As Integer,
                                   ByVal BillNo As String,
                                   ByVal Item As String,
                                   ByVal CancelDate As String,
                                   ByVal CancelCode As Integer, ByVal CancelName As String) As RIAResult

        Try
            InitClass(LoginInfo)
            result.ResultBoolean = True
            result.ErrorCode = 0
            result.ErrorMessage = Nothing
            result.ResultBoolean = _EnterPay.CancelTempData(EntryType, BillNo, Item,
                                                            CancelDate, CancelCode, CancelName)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally

            _EnterPay.Dispose()
            _EnterPay = Nothing
        End Try
        Return result
    End Function

    Public Function QueryCancelReason(ByVal LoginInfo As LoginInfo) As RIAResult
        Dim dsReturn As DataSet = Nothing
        Try
            InitClass(LoginInfo)
            dsReturn = _EnterPay.QueryCancelReason
            result.ResultBoolean = True
            result.ErrorMessage = Nothing
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsReturn, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If dsReturn IsNot Nothing Then
                dsReturn.Dispose()
                dsReturn = Nothing
            End If
            _EnterPay.Dispose()
            _EnterPay = Nothing
        End Try
        Return result
    End Function
    Public Function QueryEnterData(ByVal LoginInfo As LoginInfo,
                                   ByVal EntryType As Integer) As RIAResult


        Dim dsReturn As DataSet = Nothing
        Try
            InitClass(LoginInfo)
            dsReturn = _EnterPay.QueryEnterData(EntryType)
            result.ResultBoolean = True
            result.ErrorMessage = Nothing
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsReturn, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If dsReturn IsNot Nothing Then
                dsReturn.Dispose()
                dsReturn = Nothing
            End If
            _EnterPay.Dispose()
        End Try
        Return result
    End Function
    Public Function EntryBillData(ByVal LoginInfo As LoginInfo,
                                  ByVal EntryType As Integer,
                                  ByVal BillNo As String, ByVal dsCitemPara As String,
                                  ByVal UCRefNo As Integer) As RIAResult
        'Dim dsResult As New DataSet()
        Dim dsSource As DataSet = Nothing
        Dim dsReturn As DataSet = Nothing
        Try
            InitClass(LoginInfo)
            dsSource = Silverlight.DataSetConnector.Connector.FromXml(dsCitemPara)
            dsReturn = _EnterPay.EntryBillData(EntryType, BillNo, dsSource.Tables(0), UCRefNo)

            If dsReturn.Tables("Error").Rows.Count > 0 Then
                result.ResultBoolean = False
                result.ErrorMessage = dsReturn.Tables("Error").Rows(0).Item("ErrorName").ToString
            Else
                'dsResult.Tables.Add(dsReturn.Tables("OK").Copy)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsReturn, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End If
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If dsSource IsNot Nothing Then
                dsSource.Dispose()
                dsSource = Nothing
            End If
            If dsReturn IsNot Nothing Then
                dsReturn.Dispose()
                dsReturn = Nothing
            End If
            'If dsResult IsNot Nothing Then
            '    dsResult.Dispose()
            '    dsResult = Nothing
            'End If
            'dsResult.Dispose()
            _EnterPay.Dispose()
            _EnterPay = Nothing
        End Try
        Return result
    End Function
    Public Function GetParameters(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _EnterPay.GetParameters.DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _EnterPay.Dispose()
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

