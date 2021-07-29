
Option Compare Binary
Option Infer On
'Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.ServiceModel.DomainServices.Hosting
Imports System.ServiceModel.DomainServices.Server
Imports CableSoft.BLL.Utility
Imports System.Configuration

'TODO: 建立包含應用程式邏輯的方法。
'<EnableClientAccess()>  _
Public Class CounterPay
    'Inherits DomainService
    Implements IDisposable
    Private _LoginInfo As Object
    Private _result As New RIAResult
    Private _BLL As CableSoft.SO.BLL.Billing.CounterPay.CounterPay
    Public Property isHTML As Boolean = False

    Private Sub InitClass(ByVal LoginInfo As LoginInfo, Optional ByVal blnChangeConn As Boolean = False)
        Try
            _LoginInfo = LoginInfo.ConvertTo(LoginInfo)
            If blnChangeConn Then
                _LoginInfo = CableSoft.BLL.Utility.Utility.GetRealLoginInfo(_LoginInfo, LoginInfo.CompCode.ToString)
            End If
            _BLL = New CableSoft.SO.BLL.Billing.CounterPay.CounterPay(_LoginInfo)
        Catch ex As Exception
            Throw New Exception("Error Create DLL!!")
        End Try
    End Sub

    'Private Sub InitClass(ByVal LoginInfo As LoginInfo)
    '    Try
    '        _LoginInfo = LoginInfo.ConvertTo(LoginInfo)
    '        _BLL = New CableSoft.SO.BLL.Billing.CounterPay.CounterPay(_LoginInfo)

    '    Catch ex As Exception
    '        Throw New Exception("Error Create DLL!!")
    '    End Try
    'End Sub

    '一次取得畫面所需資料
    Public Function OpenAllData(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dsAllData As DataSet = _BLL.OpenAllData()
                '_result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dsAllData.Copy)
                _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsAllData.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            End Using
            _result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function
    '重取Grid收費資料
    Public Function GetChargeTmp(ByVal LoginInfo As LoginInfo, ByVal strPara As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dsPara As DataSet = Silverlight.DataSetConnector.Connector.FromXml(strPara)
                Using dsRtn As DataSet = _BLL.GetChargeTmp(dsPara.Tables("Para").Copy)
                    '_result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsRtn.Copy)
                    _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsRtn.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                End Using
            End Using
            _result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function
    '新增前取回檢核資料
    Public Function ChkDataOK(ByVal LoginInfo As LoginInfo, ByVal strBillNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            _result = _BLL.ChkDataOK(strBillNo)
            If _result.ResultDataSet IsNot Nothing Then
                Dim ds As DataSet = _result.ResultDataSet
                '_result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds.Copy)
                _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            End If
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function
    '新增費用
    Public Function AddNewCharge(ByVal LoginInfo As LoginInfo, ByVal strBillNo As String, ByVal strPara As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dsPara As DataSet = Silverlight.DataSetConnector.Connector.FromXml(strPara)
                _result = _BLL.AddNewCharge(strBillNo, dsPara.Tables("Para").Copy)
                If _result.ResultDataSet IsNot Nothing Then
                    Dim ds As DataSet = _result.ResultDataSet
                    '_result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds.Copy)
                    _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                End If
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function
    '修改費用
    Public Function EditChargeTmp(ByVal LoginInfo As LoginInfo, ByVal strCharge As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dsCharge As DataSet = Silverlight.DataSetConnector.Connector.FromXml(strCharge)
                _result = _BLL.UpdChargeTmp(dsCharge)
                If _result.ResultDataSet IsNot Nothing Then
                    Dim dsData As DataSet = _result.ResultDataSet
                    '_result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsData.Copy)
                    _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsData.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                End If
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function
    '取得收費資料SO033
    Public Function GetSimple(ByVal LoginInfo As LoginInfo, ByVal BillNo As String, ByVal Item As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As New DataSet
                Dim dt As DataTable = _BLL.GetSimple(BillNo, Item)
                ds.Tables.Add(dt.Copy)
                '_result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds.Copy)
                ''_result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds.Copy)
                _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            End Using
            _result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function
    '刪除登錄資料
    Public Function DeleteChargeTmp(ByVal LoginInfo As LoginInfo, ByVal strPara As String, ByVal EntryNo As Integer, ByVal RealDate As String, ByVal EntryEn As String, ByVal ClctEn As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dsCharge As DataSet = Silverlight.DataSetConnector.Connector.FromXml(strPara)
                _result = _BLL.DeleteChargeTmp(dsCharge.Tables("Para"), EntryNo, RealDate, EntryEn, ClctEn)
                If _result.ResultDataSet IsNot Nothing Then
                    Dim dsData As DataSet = _result.ResultDataSet
                    '_result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsData.Copy)
                    _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsData.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                End If
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function
    '結轉費用
    Public Function ChargeCutDate(ByVal LoginInfo As LoginInfo, ByVal strCharge As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dsCharge As DataSet = Silverlight.DataSetConnector.Connector.FromXml(strCharge)
                _result = _BLL.ChargeCutDate(dsCharge)
                If _result.ResultDataSet IsNot Nothing Then
                    Dim dsData As DataSet = _result.ResultDataSet
                    '_result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsData.Copy)
                    _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsData.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                    _result.ResultDataSet = Nothing
                End If
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function
    '修改付款種類
    Public Function EditPTData(ByVal LoginInfo As LoginInfo, ByVal PTCode As Integer, ByVal PTName As String, ByVal BillNo As String, ByVal Item As Integer, ByVal Para As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dsCharge As DataSet = Silverlight.DataSetConnector.Connector.FromXml(Para)
                _result = _BLL.EditPTData(dsCharge.Tables("Para"), PTCode, PTName, BillNo, Item)
                If _result.ResultDataSet IsNot Nothing Then
                    Dim dsData As DataSet = _result.ResultDataSet
                    '_result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsData.Copy)
                    _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsData.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                End If
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function
    '客戶資訊
    Public Function SaveCustData(ByVal LoginInfo As LoginInfo, ByVal CarrierTypeCode As String, ByVal CarrierTypeName As String, ByVal CarrierId1 As String,
                                 ByVal LoveNum As String, ByVal CardLastNo As String, ByVal BillNo As String, ByVal MediaBillNo As String,
                                 ByVal Para As String, ByVal InitData As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dsPara As DataSet = Silverlight.DataSetConnector.Connector.FromXml(Para)
                Using dsInitData As DataSet = Silverlight.DataSetConnector.Connector.FromXml(InitData)
                    _result = _BLL.SaveCustData(dsPara.Tables("Para"), CarrierTypeCode, CarrierTypeName, CarrierId1, LoveNum, CardLastNo, BillNo, MediaBillNo, dsInitData)
                    If _result.ResultDataSet IsNot Nothing Then
                        Dim dsData As DataSet = _result.ResultDataSet
                        '_result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsData.Copy)
                        _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsData.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                    End If
                End Using
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function
    '信用卡刷卡
    Public Function PaymentCharge(ByVal LoginInfo As LoginInfo, ByVal Para As String, ByVal InitData As String, ByVal Credit As Boolean) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dsPara As DataSet = Silverlight.DataSetConnector.Connector.FromXml(Para)
                Using dsInitData As DataSet = Silverlight.DataSetConnector.Connector.FromXml(InitData)
                    _result = _BLL.PaymentCharge(dsPara.Copy, dsInitData.Copy, Credit)
                    If _result.ResultDataSet IsNot Nothing Then
                        Dim ds As DataSet = _result.ResultDataSet
                        _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                    End If
                End Using
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function
    '檢核收費是否可以刪除登錄
    Public Function ChkChargeDel(ByVal LoginInfo As LoginInfo, ByVal chkCharge As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dsCharge As DataSet = Silverlight.DataSetConnector.Connector.FromXml(chkCharge)
                _result = _BLL.ChkChargeDel(dsCharge.Tables(0))
                If _result.ResultDataSet IsNot Nothing Then
                    Dim dsData As DataSet = _result.ResultDataSet
                    _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsData.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                End If
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function
    '信用卡退刷
    Public Function PaymentChargeDel(ByVal LoginInfo As LoginInfo, ByVal InitData As String, ByVal Credit As Boolean) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dsInitData As DataSet = Silverlight.DataSetConnector.Connector.FromXml(InitData)
                _result = _BLL.PaymentChargeDel(dsInitData.Copy, Credit)
                If _result.ResultDataSet IsNot Nothing Then
                    Dim ds As DataSet = _result.ResultDataSet
                    _result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds.Copy, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                End If
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex, LoginInfo)
        Finally
            ClassFinalize()
        End Try
        Return _result
    End Function


    '取得ReportServicePath
    Public Function GetReportServicePath() As String
        If _BLL IsNot Nothing Then _BLL.Dispose()
        Return GetKeyValue("ReportServicePath")
    End Function
    '取得Web.config裡面的Tag的Value
    Private Function GetKeyValue(Key As String) As String
        If _BLL IsNot Nothing Then _BLL.Dispose()
        Return CStr(New AppSettingsReader().GetValue(Key, GetType(String)))
    End Function

    Public Function CanView(ByVal LoginInfo As LoginInfo, Optional ByVal blnChangeConn As Boolean = False) As RIAResult
        CanView = Nothing
        Try
            InitClass(LoginInfo, blnChangeConn)
            _result = _BLL.CanView
            If _BLL IsNot Nothing Then _BLL.Dispose()
        Catch ex As Exception
            ErrorHandle.BuildMessage(_result, ex)
        End Try
        Return _result
    End Function

    Private Sub ClassFinalize()
        Try
            If _LoginInfo.LocalDAO IsNot Nothing Then
                _LoginInfo.LocalDAO.dispose()
            End If
            If _BLL IsNot Nothing Then
                _BLL.Dispose()
            End If
        Catch ex As Exception
        End Try
    End Sub



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

