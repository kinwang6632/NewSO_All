
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

Public Class Maintain
    'Inherits DomainService
    Implements IDisposable

    Private _Maintain As CableSoft.SO.BLL.Wip.Maintain.Maintain
    Private _ChangeFaci As CableSoft.SO.BLL.Facility.ChangeFaci.ChangeFaci
    Private result As New RIAResult()
    Private Const fMaintain_Wip As String = "Wip"
    Private Const fMaintain_Facility As String = "Facility"
    Private Const fMaintain_PRFacility As String = "PRFacility"
    Private Const fMaintain_Charge As String = "Charge"
    Private Const fMaintain_ChangeFacility As String = "ChangeFacility"
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _Maintain = New CableSoft.SO.BLL.Wip.Maintain.Maintain(LoginInfo.ConvertTo(LoginInfo))

    End Sub
    ''' <summary>
    ''' 判斷該服務別是否可選擇
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <param name="CustId"></param>
    ''' <param name="ServiceType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CanServiceType(ByVal LoginInfo As LoginInfo,
                                  ByVal CustId As Int32, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            'Dim objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsMaintain)
            Return _Maintain.CanServiceType(CustId, ServiceType)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
    End Function
    Public Function AutoSetMaintain(ByVal LoginInfo As LoginInfo, ByVal SNo As String, ByVal FaciSeqNo As String) As RIAResult
        _ChangeFaci = New CableSoft.SO.BLL.Facility.ChangeFaci.ChangeFaci(LoginInfo.ConvertTo(LoginInfo))
        Try
            InitClass(LoginInfo)
            'Dim dsSource As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsSourceWip)
            Dim ds As DataSet = _ChangeFaci.GetMaintainFaci(SNo, FaciSeqNo, False).DataSet

            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ChangeFaci IsNot Nothing Then
                _ChangeFaci.Dispose()
                _ChangeFaci = Nothing
            End If

        End Try
        Return result
    End Function
    Public Function GetSysDate(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            result.ResultXML = _Maintain.GetSysDate
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    Public Function GetNullServiceType(ByVal LoginInfo As LoginInfo,
                                                 ByVal SNo As String, ByVal CustId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(_Maintain.GetNullServiceType(SNo, CustId), _
                                                                         JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(_Maintain.GetNullServiceType(SNo, CustId))
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function



    Public Function GetCommonData(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(_Maintain.GetCommonData, _
                                                                       JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(_Maintain.GetCommonData)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    Public Function GetServiceTypeChangeData(ByVal LoginInfo As LoginInfo,
                                             ByVal SNo As String,
                                             ByVal CustId As Integer,
                                             ByVal ServiceType As String,
                                             ByVal ServiceCode As String,
                                             ByVal MFCode As String,
                                             ByVal IsGetFalseSNo As Boolean) As RIAResult
        Try
            InitClass(LoginInfo)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(
            '                _Maintain.GetServiceTypeChangeData(SNo, CustId, ServiceType,
            '                                                   ServiceCode, MFCode, IsGetFalseSNo))
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(
                            _Maintain.GetServiceTypeChangeData(SNo, CustId, ServiceType,
                                                               ServiceCode, MFCode, IsGetFalseSNo), _
                                                           JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result

    End Function
    Public Function DelResvPoint(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            result.ResultBoolean = _Maintain.DelResvPoint
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取出可撰擇的服務別
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServiceType(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetServiceType
            'result.ResultCode = RIAResult.ConvertToCodeField(ds.Tables(0))
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, _
                                                                       True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    Public Function GetMaintainData(ByVal LoginInfo As LoginInfo, _
                                    ByVal SNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _Maintain.GetMaintainData(SNo)
            'result.ResultCode = RIAResult.ConvertToCodeField(ds.Tables(0))
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, _
                                                                       True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function

    ''' <summary>
    ''' 取得客戶基本資料
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="CustId">客編</param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns>TABLE</returns>
    ''' <remarks></remarks>
    Public Function GetCustomer(ByVal LoginInfo As LoginInfo, _
                                ByVal CustId As Int32, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetCustomer(CustId, ServiceType)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, _
                                                                       True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得客戶基本資料(SO001)
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="CustId">客編</param>
    ''' <returns>Table</returns>
    ''' <remarks></remarks>
    Public Function GetSO001(ByVal LoginInfo As LoginInfo, _
                                ByVal CustId As Int32) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetSO001(CustId)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, _
                                                                       True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得可選維修類別
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns>TABLE</returns>
    ''' <remarks></remarks>
    Public Function GetMaitainCode(ByVal LoginInfo As LoginInfo, _
                                   ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetMaitainCode(ServiceType)
            'result.ResultCode = RIAResult.ConvertToCodeField(dt)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, _
                                                                       True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得可選工程組別
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="ServCode">服務區</param>
    ''' <returns>TABLE</returns>
    ''' <remarks></remarks>
    Public Function GetGroupCode(ByVal LoginInfo As LoginInfo, _
                                ByVal ServCode As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetGroupCode(ServCode)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, _
                                                                       True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得可選工作人員
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="Type">工程人員種類</param>
    ''' <returns>TABLE</returns>
    ''' <remarks>TYPE = ( 0: 工程人員 , 1: 工程人員2 )</remarks>
    Public Function GetWorkerEn(ByVal LoginInfo As LoginInfo, _
                               ByVal Type As Int32) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetWorkerEn(Type)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, _
                                                                       True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result

    End Function
    ''' <summary>
    ''' 取得可選退單原因
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns>TABLE</returns>
    ''' <remarks></remarks>
    Public Function GetReturnCode(ByVal LoginInfo As LoginInfo, _
                                  ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetReturnCode(ServiceType)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, _
                                                                       True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得可選退單原因分類
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns>Table</returns>
    ''' <remarks></remarks>
    Public Function GetReturnDescCode(ByVal LoginInfo As LoginInfo, _
                                     ByVal ServiceType As String) As RIAResult

        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetReturnDescCode(ServiceType)
            result.ResultCode = RIAResult.ConvertToCodeField(dt)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function

    ''' <summary>
    ''' 取得可選服務滿意度
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns>Table</returns>
    ''' <remarks></remarks>
    Public Function GetSatiCode(ByVal LoginInfo As LoginInfo, _
                               ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetSatiCode(ServiceType)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, _
                                                                       True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得可選簽收人員
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <returns>Table</returns>
    ''' <remarks></remarks>
    Public Function GetSignEn(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetSignEn()
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得可選故障代號1
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns>Table</returns>
    ''' <remarks></remarks>
    Public Function GetMFCode1(ByVal LoginInfo As LoginInfo, _
                              ByVal ServiceType As String) As RIAResult

        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetMFCode1(ServiceType)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    '''取得可選故障代號2
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="MFCode">故障代號1</param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns>Table</returns>
    ''' <remarks></remarks>
    Public Function GetMFCode2(ByVal LoginInfo As LoginInfo, _
                              ByVal MFCode As Int32, _
                               ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetMFCode2(MFCode, ServiceType)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result

    End Function
    Public Function IsFixingArea(ByVal LoginInfo As LoginInfo, ByVal CustId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            'Return RIAResult.Convert(_Maintain.CanAppend(CustId, ServiceType))
            Return _Maintain.IsFixingArea(CustId)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
    End Function
    ''' <summary>
    ''' 可新增
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="CustId">客戶編號</param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function CanAppend(ByVal LoginInfo As LoginInfo,
                              ByVal CustId As Int32,
                              ByVal ServiceType As String, ByVal IsContact As Boolean) As RIAResult
        Try
            InitClass(LoginInfo)
            'Return RIAResult.Convert(_Maintain.CanAppend(CustId, ServiceType))
            Return _Maintain.CanAppend(CustId, ServiceType, IsContact)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try

    End Function
    ''' <summary>
    ''' 可修改
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="dsMaintain">Maintain Table 維修單資料</param>    
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function CanEdit(ByVal LoginInfo As LoginInfo,
                           ByVal SNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            'Dim objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsMaintain)
            'Return RIAResult.Convert(_Maintain.CanEdit(SNo))
            Return _Maintain.CanEdit(SNo)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
    End Function
    ''' <summary>
    ''' 作廢維修單
    ''' </summary>
    ''' <param name="SNo">維修單號</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function VoidData(ByVal LoginInfo As LoginInfo,
                             ByVal SNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            'Dim objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsMaintain)
            'Return RIAResult.Convert(_Maintain.VoidData(SNo))
            Return _Maintain.VoidData(SNo)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
    End Function
    ''' <summary>
    ''' 可作廢
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="SNo">維修單號</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function CanDelete(ByVal LoginInfo As LoginInfo,
                             ByVal SNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            'Dim objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsMaintain)
            'Return RIAResult.Convert(_Maintain.CanDelete(SNo))
            Return _Maintain.CanDelete(SNo)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
    End Function
    ''' <summary>
    ''' 可列印
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function CanPrint(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            'Return RIAResult.Convert(_Maintain.CanPrint())
            Return _Maintain.CanPrint()
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
    End Function
    Public Function CanView(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Return _Maintain.CanView()
            'Return RIAResult.Convert(_Maintain.CanView())
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
    End Function

    Public Function GetSO1132Priv(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            'Return RIAResult.Convert(_Maintain.GetSO1132Priv())
            Return _Maintain.GetSO1132Priv()
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
    End Function
    ''' <summary>
    ''' 取得所有權限
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function GetPriv(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetPriv
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得一般工單資訊
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="CustId">客編</param>
    ''' <param name="ServiceType">服務別</param>
    ''' <param name="ResvTime">預約時間</param>
    ''' <param name="InstCode">派工類別</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>Facility,Charge,ChangeFacility</remarks>
    Public Function GetNormalWip(ByVal LoginInfo As LoginInfo,
                                ByVal CustId As Integer,
                                 ByVal ServiceType As String,
                                 ByVal ResvTime As Date,
                                 ByVal InstCode As Integer,
                                 ByVal dsContact As String,
                                 ByVal dsOldWipData As String, ByVal isRefreshAll As Boolean) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dtContact As DataTable = Nothing
            Dim dsWipData As DataSet = Nothing
            If Not String.IsNullOrEmpty(dsContact) Then
                dtContact = Silverlight.DataSetConnector.Connector.FromXml(dsContact).Tables(0)
            End If
            If Not String.IsNullOrEmpty(dsOldWipData) Then
                dsWipData = Silverlight.DataSetConnector.Connector.FromXml(dsOldWipData)
            End If
            Dim ds As DataSet = _Maintain.GetNormalWip(CustId, ServiceType, ResvTime, InstCode, dtContact, dsWipData, isRefreshAll)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    Public Function GetReInstChangeFaci(ByVal LoginInfo As LoginInfo,
                                        ByVal SNo As String, ByVal FaciSeqNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _Maintain.GetReInstChangeFaci(SNo, FaciSeqNo)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function

    Public Function GetMaintainChangeFaci(ByVal LoginInfo As LoginInfo,
                                        ByVal CustId As Integer,
                                          ByVal ServiceType As String,
                                          ByVal ResvTime As Date,
                                          ByVal WorkCodeValue As Integer,
                                          ByVal dsContact As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dtContact As DataTable = Nothing
            If Not String.IsNullOrEmpty(dsContact) Then
                dtContact = Silverlight.DataSetConnector.Connector.FromXml(dsContact).Tables(0)
            End If
            Dim ds As DataSet = _Maintain.GetMaintainChangeFaci(CustId, ServiceType, ResvTime, WorkCodeValue, dtContact).DataSet
            If ds Is Nothing Then
                result.ResultBoolean = False
                result.ErrorMessage = "DS"
            End If
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
            'result.ErrorMessage = ex.ToString
            'result.ResultBoolean = False
            'ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _Maintain.Dispose()
        End Try
        Return result

    End Function


    Public Function GetSO014(ByVal LoginInfo As LoginInfo, ByVal AddrNo As Int32) As RIAResult

        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetSO014(AddrNo)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    Public Function ChooseFaciUpdData(ByVal LoginInfo As LoginInfo,
                                      FaciSeqNo As String, FaciSNo As String,
                                      WipRefNo As Integer, ReInstAcrossFlag As Boolean,
                                      dsWipData As String) As RIAResult
        Dim aRet As New RIAResult() With {.ErrorCode = 0, .ResultBoolean = False}
        Try
            InitClass(LoginInfo)
            Dim objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipData)

            aRet.ResultBoolean = _Maintain.ChooseFaciUpdData(FaciSeqNo, FaciSNo, WipRefNo, ReInstAcrossFlag, objDS)
            'aRet.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(objDS)
            aRet.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(objDS, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return aRet
    End Function
    Public Function Save(ByVal LoginInfo As CableSoft.BLL.Utility.LoginInfo,
                     ByVal EditMode As CableSoft.BLL.Utility.EditMode,
                     ByVal ShouldReg As Boolean,
                     ByVal dsWipData As String, ByVal ReturnFlag As Boolean) As RIAResult
        Dim _MaintainSave As New CableSoft.SO.BLL.Wip.Maintain.SaveData(LoginInfo.ConvertTo(LoginInfo))
        result.ErrorCode = -1
        result.ErrorMessage = "存檔錯誤！"
        Try
            'InitClass(LoginInfo)
            Dim objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipData)

            'If _MaintainSave.Save(EditMode, ShouldReg, ServCode, OldSNo, objDS) Then
            '    result.ResultBoolean = True
            '    result.ErrorMessage = Nothing
            '    result.ErrorCode = 0
            '    result.ResultXML = objDS.Tables(0).Rows(0).Item("SNO").ToString
            'End If
            Return _MaintainSave.Save(EditMode, ShouldReg, objDS, ReturnFlag)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _MaintainSave.Dispose()
        End Try
        Return result
    End Function
    'Public Function Save(ByVal LoginInfo As LoginInfo,
    '                     ByVal EditMode As CableSoft.BLL.Utility.EditMode,
    '                     ByVal ShouldReg As Boolean,
    '                     ByVal ServCode As String,
    '                     ByVal OldSNo As String,
    '                     ByVal dsWipData As String) As RIAResult
    '    Dim _MaintainSave As New CableSoft.SO.BLL.Wip.Maintain.SaveData(LoginInfo.ConvertTo(LoginInfo))
    '    result.ErrorCode = -1
    '    result.ErrorMessage = "存檔錯誤！"
    '    Try
    '        'InitClass(LoginInfo)
    '        Dim objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipData)

    '        If _MaintainSave.Save(EditMode, ShouldReg, ServCode, OldSNo, objDS) Then
    '            result.ResultBoolean = True
    '            result.ErrorMessage = Nothing
    '            result.ErrorCode = 0
    '            result.ResultXML = objDS.Tables(0).Rows(0).Item("SNO").ToString
    '        End If
    '        'Return RIAResult.Convert(_MaintainSave.Save(EditMode, ShouldReg, ServCode, OldSNo, objDS))
    '    Catch ex As Exception
    '        ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
    '        result.ResultBoolean = False
    '        Return result
    '    Finally
    '        _MaintainSave.Dispose()
    '    End Try
    '    Return result
    'End Function
    ''' <summary>
    ''' 取得預設預約時間
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="ServiceType">服務別</param>
    ''' <param name="MaintainCode">維修類別</param>
    ''' <param name="AcceptTime">受理時間</param>
    ''' <returns>Date(result.ResultXML)</returns>
    ''' <remarks>受理時間可為Nothing</remarks>
    Public Function GetDefaultResvTime(ByVal LoginInfo As LoginInfo,
                                      ByVal ServiceType As String,
                                       ByVal MaintainCode As Int32,
                                       ByVal AcceptTime As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Return _Maintain.GetDefaultResvTime(ServiceType, MaintainCode, AcceptTime)
            'Return RIAResult.Convert(_Maintain.GetDefaultResvTime(ServiceType, MaintainCode, AcceptTime))
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
    End Function
    Public Function GetInvoiceNo(ByVal LoginInfo As LoginInfo,
                                ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            'Return RIAResult.Convert(_Maintain.GetInvoiceNo(ServiceType))
            Return _Maintain.GetInvoiceNo(ServiceType)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
    End Function
    Public Function GetFalseSNO(ByVal LoginInfo As LoginInfo,
                                ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            'Return RIAResult.Convert(_Maintain.GetFalseSNO(ServiceType))
            Return _Maintain.GetFalseSNO(ServiceType)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
    End Function
    Public Function ChkCanResv(ByVal LoginInfo As LoginInfo,
                               ByVal ServCode As String, ByVal WipCode As Int32,
                            ByVal MCode As String, ByVal ServiceType As String,
                            ByVal ResvTime As Date,
                            ByVal AcceptTime As Date, ByVal OldResvTime As Date,
                            ByVal Resvdatebefore As Int32, ByVal WorkUnit As Decimal,
                            ByVal IsBooking As Boolean, ByVal oldServCode As String) As RIAResult
        Dim _MaintainValidate As New CableSoft.SO.BLL.Wip.Maintain.Validate(LoginInfo.ConvertTo(LoginInfo))
        Try

            'InitClass(LoginInfo)
            'Return RIAResult.Convert(_MaintainValidate.ChkCanResv(ServCode, WipCode,
            '                                                      MCode, ServiceType,
            '                                                      ResvTime, AcceptTime, OldResvTime,
            '                                                      Resvdatebefore, WorkUnit, IsBooking))
            Return _MaintainValidate.ChkCanResv(ServCode, WipCode,
                                                                 MCode, ServiceType,
                                                                 ResvTime, AcceptTime, OldResvTime,
                                                                 Resvdatebefore, WorkUnit, IsBooking, oldServCode)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _MaintainValidate.Dispose()
        End Try

    End Function
    Public Function InsSmartCardNo(ByVal LoginInfo As LoginInfo,
                                  ByVal dsFacility As String, ByVal SeqNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsFacility)
            Dim dt As DataTable = _Maintain.InsSmartCardNo(ds.Tables(0), SeqNo)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function

    Public Function ChkSaveDataOK(ByVal LoginInfo As LoginInfo,
                                  ByVal EditMode As CableSoft.BLL.Utility.EditMode,
                                  ByVal CloseDate As String, _
        ServiceType As String, ByVal dsWipData As String, ByVal ShouldReg As Boolean) As RIAResult
        Dim _MaintainValidate As New CableSoft.SO.BLL.Wip.Maintain.Validate(LoginInfo.ConvertTo(LoginInfo))
        Try
            'InitClass(LoginInfo)

            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipData)
            'Dim aRet As RIAResult = RIAResult.Convert(_MaintainValidate.ChkDataOk(EditMode, ds))
            'aRet = RIAResult.Convert(_MaintainValidate.ChkCloseDate(CloseDate, ServiceType))
            Dim aRet As RIAResult = _MaintainValidate.ChkDataOk(EditMode, ds, ShouldReg)
            aRet = _MaintainValidate.ChkCloseDate(CloseDate, ServiceType)
            Return aRet
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _MaintainValidate.Dispose()
        End Try
    End Function
    ''' <summary>
    ''' 檢查派工單是否正確
    ''' </summary>
    '''<param name="LoginInfo">LoginInfo</param>
    ''' <param name="dsWipData">派工資料</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function ChkDataOk(ByVal LoginInfo As LoginInfo,
                              ByVal EditMode As CableSoft.BLL.Utility.EditMode,
                              ByVal dsWipData As String, ByVal ShouldReg As Boolean) As RIAResult
        Dim _MaintainValidate As New CableSoft.SO.BLL.Wip.Maintain.Validate(LoginInfo.ConvertTo(LoginInfo))
        Try
            'InitClass(LoginInfo)

            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipData)
            'Dim aRet As RIAResult = RIAResult.Convert(_MaintainValidate.ChkDataOk(EditMode, ds))
            Dim aRet As RIAResult = _MaintainValidate.ChkDataOk(EditMode, ds, ShouldReg)
            Return aRet
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _MaintainValidate.Dispose()
        End Try
    End Function
    Public Function ChkCloseDate(ByVal LoginInfo As LoginInfo, ByVal CloseDate As String, _
        ByVal ServiceType As String) As RIAResult
        Dim _MaintainValidate As New CableSoft.SO.BLL.Wip.Maintain.Validate(LoginInfo.ConvertTo(LoginInfo))
        Try
            InitClass(LoginInfo)
            'Return RIAResult.Convert(_MaintainValidate.ChkCloseDate(CloseDate, ServiceType))
            Return _MaintainValidate.ChkCloseDate(CloseDate, ServiceType)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _MaintainValidate.Dispose()
        End Try
    End Function
    Public Function ChkHaveCM(ByVal LoginInfo As LoginInfo,
                             ByVal dsWipData As String,
                              ByVal Is004D As Boolean,
                              ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipData)
            'Return RIAResult.Convert(_Maintain.ChkHaveCM(ds, Is004D, ServiceType))
            Return _Maintain.ChkHaveCM(ds, Is004D, ServiceType)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try

    End Function
    Public Function ChkFaciFinishPrivFlag(ByVal LoginInfo As LoginInfo, ByVal dsWipData As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipData)
            Dim dsRet As New DataSet()
            Dim dt As DataTable = _Maintain.ChkFaciFinishPrivFlag(ds)
            dsRet.Tables.Add(dt)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dsRet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsRet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function
    Public Function GetReInstAddrNo(ByVal LoginInfo As LoginInfo,
                                ByVal CustId As Int32, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _Maintain.GetReInstAddrNo(CustId, ServiceType)
            Dim RetDt As New DataTable()
            Dim RetDs As New DataSet()
            Try
                RetDt.Columns.Add("blnLogChangeAddr", GetType(Int32))
                RetDt.Columns.Add("ReInstAddrNo", GetType(Int32))
                Dim rw As DataRow = RetDt.NewRow
                rw.Item("blnLogChangeAddr") = 0
                rw.Item("ReInstAddrNo") = -1
                If dt.Rows.Count > 0 Then
                    rw.Item("blnLogChangeAddr") = 1
                    rw.Item("ReInstAddrNo") = Int32.Parse(dt.Rows(0).Item("ReInstAddrNo").ToString)
                End If

                RetDt.Rows.Add(rw)
                RetDt.AcceptChanges()
                RetDs.Tables.Add(RetDt)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(RetDs)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(RetDs, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            Finally
                dt.Dispose()
                RetDt.Dispose()
                RetDs.Dispose()
            End Try

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Maintain.Dispose()

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

