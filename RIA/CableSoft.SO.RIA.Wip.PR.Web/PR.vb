
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
Public Class PR
    'Inherits DomainService 2018.06.19 會議決定要取消
    Implements IDisposable '2018.07.05 因為取消DomainService後，有用到BLL呼叫RIA時沒有Disposable，BLL端用Using的方式會出錯，或是直接Dispos也會出錯。
    Private _Maintain As CableSoft.SO.BLL.Wip.Maintain.Maintain
    Private _PR As CableSoft.SO.BLL.Wip.PR.PR
    Private _PRSave As CableSoft.SO.BLL.Wip.PR.SaveData
    Private _PRValidate As CableSoft.SO.BLL.Wip.PR.Validate
    Private _PRVoidData As CableSoft.SO.BLL.Wip.PR.PRVoidData
    Private _PRReInst As CableSoft.SO.BLL.Wip.PR.ReInst
    Private result As New RIAResult()
    Private Const fMaintain_Wip As String = "Wip"
    Private Const fMaintain_Facility As String = "Facility"
    Private Const fMaintain_PRFacility As String = "PRFacility"
    Private Const fMaintain_Charge As String = "Charge"
    Private Const fMaintain_ChangeFacility As String = "ChangeFacility"
    Public Property isHTML As Boolean = False

#Region "InitClass All"
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        Try
            _PR = New CableSoft.SO.BLL.Wip.PR.PR(LoginInfo.ConvertTo(LoginInfo))
        Catch ex As Exception

        End Try
    End Sub
    Private Sub InitClassSave(ByVal LoginInfo As LoginInfo)
        _PRSave = New CableSoft.SO.BLL.Wip.PR.SaveData(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Private Sub InitClassValidate(ByVal LoginInfo As LoginInfo)
        _PRValidate = New CableSoft.SO.BLL.Wip.PR.Validate(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Private Sub InitClassVoidData(ByVal LoginInfo As LoginInfo)
        _PRVoidData = New CableSoft.SO.BLL.Wip.PR.PRVoidData(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Private Sub InitClassReInst(ByVal LoginInfo As LoginInfo)
        _PRReInst = New CableSoft.SO.BLL.Wip.PR.ReInst(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Private Sub InitMaintainClass(ByVal LoginInfo As LoginInfo)
        _Maintain = New CableSoft.SO.BLL.Wip.Maintain.Maintain(LoginInfo.ConvertTo(LoginInfo))
    End Sub
#End Region

#Region "Can 系列功能"
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
                              ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _PR.CanAppend(CustId, ServiceType)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 可修改
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="SNO">拆機單號</param>    
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function CanEdit(ByVal LoginInfo As LoginInfo,
                           ByVal SNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _PR.CanEdit(SNo)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 可作廢
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="SNO">拆機工單資料</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function CanDelete(ByVal LoginInfo As LoginInfo,
                             ByVal SNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _PR.CanEdit(SNo)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 可顯示
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="SNO">拆機工單資料</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function CanView(ByVal LoginInfo As LoginInfo,
                             ByVal SNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _PR.CanView(SNo)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
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
            result = _PR.CanPrint()
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
#End Region

    Public Function GetInitData2(ByVal LoginInfo As LoginInfo, ByVal CustId As Integer,
                                 ByVal SNO As String, ByVal ServiceType As String,
                                 ByVal ServCode As String, ByVal WipRefNo As Integer,
                                 ByVal WipCodeValueStr As String, ByVal EditMode As EditMode) As RIAResult()
        Dim results() As RIAResult = Nothing
        Try
            InitClass(LoginInfo)
            Dim strRetMeg As String = String.Empty
            Dim dsInitData As New DataSet
            Using ds As DataSet = _PR.GetInitData2(CustId, SNO, ServiceType, ServCode, WipRefNo, WipCodeValueStr, dsInitData, strRetMeg, EditMode)
                ReDim Preserve results(1)
                '基本資料 EX: initList...
                results(0) = New RIAResult() With {.ResultBoolean = True, .ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsInitData, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML), .ErrorMessage = strRetMeg}
                '工單資料
                results(1) = New RIAResult() With {.ResultBoolean = True, .ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)}
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(results(0), ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return results
    End Function

    Public Function GetFromDefaultLoad(ByVal LoginInfo As LoginInfo, ByVal CustId As Integer,
                                       ByVal ServiceType As String, ByVal ServCode As String,
                                       ByVal CanUseRefNo As String, ByVal WipCodeValueStr As String,
                                       ByVal blnReInstFilter As Boolean, ByVal ReInstAcrossFlag As Boolean,
                                       ByVal SNO As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _PR.GetFromDefaultLoad(CustId, ServiceType, ServCode, CanUseRefNo, WipCodeValueStr, blnReInstFilter, ReInstAcrossFlag, SNO)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function

#Region "整合相關的RIA"
    ''' <summary>
    ''' 取得可選停拆機類別(GetPRCode)
    ''' </summary>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns>Collection</returns>
    ''' <remarks></remarks>
    Public Function GetPRCode(ByVal LoginInfo As LoginInfo, ByVal ServiceType As String,
                              ByVal CanUseRefNo As String, ByVal WipCodeValueStr As String,
                              ByVal blnReInstFilter As Boolean, ByVal ReInstAcrossFlag As Boolean) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetPRCode(ServiceType, CanUseRefNo, WipCodeValueStr, blnReInstFilter, ReInstAcrossFlag)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultCode = RIAResult.ConvertToCodeField(dt)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得停拆移機原因
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPRReasonCode(ByVal LoginInfo As LoginInfo, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetPRReasonCode(ServiceType)
                result.ResultCode = RIAResult.ConvertToCodeField(dt)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得停拆移機細項
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <param name="ServiceType">服務別</param>
    ''' <param name="PRReasonCode">停拆移機原因</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPRReasonDescCode(ByVal LoginInfo As LoginInfo, ByVal ServiceType As String, ByVal PRReasonCode As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetPRReasonDescCode(ServiceType, PRReasonCode)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultCode = RIAResult.ConvertToCodeField(dt)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得可選退單原因(GetReturnCode)
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReturnCode(ByVal LoginInfo As LoginInfo, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetReturnCode(ServiceType)
                result.ResultCode = RIAResult.ConvertToCodeField(dt)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得可選退單原因分類(GetReturnDescCode)
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReturnDescCode(ByVal LoginInfo As LoginInfo, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetReturnDescCode(ServiceType)
                result.ResultCode = RIAResult.ConvertToCodeField(dt)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得可選簽收人員(GetSignEn)
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSignEn(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetSignEn
                result.ResultCode = RIAResult.ConvertToCodeField(dt)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得服務滿意度
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSatiCode(ByVal LoginInfo As LoginInfo, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetSatiCode(ServiceType)
                result.ResultCode = RIAResult.ConvertToCodeField(dt)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得工作人員
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <param name="Type">工程人員種類 (0:工程人員1,1:工程人員2)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWorkerEn(ByVal LoginInfo As LoginInfo, ByVal Type As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetWorkerEn(Type)
                result.ResultCode = RIAResult.ConvertToCodeField(dt)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得客戶流向(GetCustRtnCode)
    ''' </summary>
    ''' <returns>Collection</returns>
    ''' <remarks></remarks>
    Public Function GetCustRtnCode(ByVal LoginInfo As LoginInfo, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetCustRtnCode(ServiceType)
                result.ResultCode = RIAResult.ConvertToCodeField(dt)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    Public Function GetSO042(ByVal LoginInfo As LoginInfo, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetSO042(ServiceType)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
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
            Dim dt As DataTable = _PR.GetPriv
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    Public Function GetUserPriv(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetUserPriv()
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    Public Function GetAddressData(ByVal LoginInfo As LoginInfo, ByVal CustID As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetAddressData(CustID)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
#End Region

    Public Function GetAppendCanUseServiceType(ByVal LoginInfo As LoginInfo, ByVal CustId As Integer, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _PR.GetAppendCanUseServiceType(EditMode.Append, CustId, ServiceType)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function

    Public Function GetSO1132Priv(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _PR.GetSO1132Priv()
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得服務別資料(GetServiceType)
    ''' </summary>
    ''' <returns>Collection</returns>
    ''' <remarks></remarks>
    Public Function GetServiceType(ByVal LoginInfo As LoginInfo, ByVal CanUseServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetServiceType(CanUseServiceType)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultCode = RIAResult.ConvertToCodeField(dt)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    Public Function GetServiceType2(ByVal LoginInfo As LoginInfo, ByVal CanUseServiceType As String, ByVal lngCustID As Integer, ByVal strFaciSEQNO As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetServiceType(CanUseServiceType, lngCustID, strFaciSEQNO)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultCode = RIAResult.ConvertToCodeField(dt)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
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
                                ByVal CustId As Int32,
                                 ByVal ServiceType As String,
                                 ByVal ResvTime As Date,
                                 ByVal InstCode As Int32) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _PR.GetNormalWip(CustId, ServiceType, ResvTime, InstCode)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, False, True, isHTML)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    Public Function GetPRData(ByVal LoginInfo As LoginInfo, _
                                    ByVal SNo As String, ByVal Custid As Integer, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _PR.GetPRData(SNo, Custid, ServiceType)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得工作組別
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <param name="ServCode">服務區</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGroupCode(ByVal LoginInfo As LoginInfo, ByVal ServCode As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _PR.GetGroupCode(ServCode)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultCode = RIAResult.ConvertToCodeField(dt)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得SO001 + SO002 資料
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomer(ByVal LoginInfo As LoginInfo, ByVal ServiceType As String, ByVal Custid As Int32) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _PR.GetCustOmer(ServiceType, Custid)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function

    '檢核是否存檔資料
    Public Function ChkDataOk(ByVal LoginInfo As LoginInfo, ByVal EditMode As CableSoft.BLL.Utility.EditMode, _
                            ByVal dsWipAll As String, ByVal ShouldRegPriv As Boolean) As RIAResult
        Try
            InitClassValidate(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipAll)
            result = _PRValidate.ChkDataOk(EditMode, ds, ShouldRegPriv)
            ds.Dispose()
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PRValidate.Dispose()
        End Try
        Return result
    End Function

    '檢核是否可派工(HTML5)
    Public Function CheckCanPR2(ByVal LoginInfo As LoginInfo, ByVal PRCode As Int32,
                               ByVal Custid As Int32, ByVal ServiceType As String) As RIAResult
        Try
            InitClassValidate(LoginInfo)
            result = _PRValidate.CheckCanPR(PRCode, Custid, ServiceType)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PRValidate.Dispose()
        End Try
        Return result
    End Function

    '檢核是否可派工(sl)
    Public Function CheckCanPR(ByVal LoginInfo As LoginInfo, ByVal PRCode As Int32,
                               ByVal PRRefNo As Int32, ByVal Interdepend As Int32,
                               ByVal CustStatusCode As Int32, ByVal WipCode3 As String,
                               ByVal Custid As Int32, ByVal ServiceType As String,
                               ByVal InstAddrNo As Int64, ByVal dsWipAll As String) As RIAResult
        Try
            InitClassValidate(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipAll)
            result = _PRValidate.CheckCanPR(PRCode, PRRefNo, Interdepend, CustStatusCode, WipCode3, Custid, ServiceType, InstAddrNo, ds.Tables("Wip"))
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PRValidate.Dispose()
        End Try
        Return result
    End Function

    '檢核預約時間是不否正常
    Public Function ChkCanResv(ByVal LoginInfo As LoginInfo, ByVal WipCode As Integer, ByVal ServCode As String, ByVal MCode As Integer,
                               ByVal ServiceType As String, ByVal ResvTime As DateTime,
                               ByVal AcceptTime As DateTime, ByVal OldResvTime As DateTime,
                               ByVal Resvdatebefore As Integer, ByVal WorkUnit As Decimal,
                               ByVal IsBookIng As Boolean, ByVal oldServCode As String) As RIAResult
        Try
            InitClassValidate(LoginInfo)
            result = _PRValidate.ChkCanResv(ServCode, WipCode, MCode,
                                            ServiceType, ResvTime, AcceptTime,
                                            OldResvTime, Resvdatebefore, WorkUnit, IsBookIng, oldServCode)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PRValidate.Dispose()
        End Try
        Return result
    End Function

    '取得一般工單相關資料
    Public Function GetNormalCalculateData(ByVal LoginInfo As LoginInfo, ByVal CustId As Integer,
                                           ByVal ServiceType As String,
                                           ByVal WorkCodeValue As Integer,
                                           ByVal ResvTime As DateTime,
                                           ByVal SNo As String,
                                           ByVal OtherData As String,
                                           ByVal OldWipData As String) As RIAResult
        Try
            Dim dsOtherData As DataSet = Nothing
            Dim dsOldWipData As DataSet = Nothing
            Dim ds As DataSet
            InitClass(LoginInfo)

            If Not String.IsNullOrEmpty(OtherData) Then
                dsOtherData = Silverlight.DataSetConnector.Connector.FromXml(OtherData)
            End If
            If dsOtherData IsNot Nothing Then
                If dsOtherData.Tables.Count = 0 Then dsOtherData = Nothing
            End If

            If Not String.IsNullOrEmpty(OldWipData) Then
                dsOldWipData = Silverlight.DataSetConnector.Connector.FromXml(OldWipData)
            End If
            If dsOldWipData.Tables.Count = 0 Then dsOldWipData = Nothing

            ds = _PR.GetNormalCalculateData(CustId, ServiceType, WorkCodeValue, ResvTime, SNo, True, dsOtherData, dsOldWipData)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function

    '存檔
    Public Function Save(ByVal LoginInfo As LoginInfo, ByVal EditMode As EditMode,
                         ByVal ShouldReg As Boolean, ByVal dsWipData As String,
                         ByVal dsWipInstData As String, ByVal dsMoveFaciData As String) As RIAResult
        Try
            InitClassSave(LoginInfo)
            Using WipData As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipData)
                '移機單 需要裝機的相關資料
                Dim WipInstData As DataSet = Nothing
                If dsWipInstData IsNot Nothing Then
                    If Not String.IsNullOrEmpty(dsWipInstData) Then
                        WipInstData = Silverlight.DataSetConnector.Connector.FromXml(dsWipInstData)
                    End If
                End If
                '移機功能 其它服務別的工單資料
                Dim MoveFaciData As DataSet = Nothing
                If dsMoveFaciData IsNot Nothing Then
                    If Not String.IsNullOrEmpty(dsMoveFaciData) Then
                        MoveFaciData = Silverlight.DataSetConnector.Connector.FromXml(dsMoveFaciData)
                    End If
                End If
                result = _PRSave.Save(EditMode, ShouldReg, WipData, WipInstData, False, MoveFaciData)
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PRSave.Dispose()
        End Try
        Return result
    End Function

    '存檔2 (HTNL5版)
    Public Function Save2(ByVal LoginInfo As LoginInfo, ByVal EditMode As EditMode,
                         ByVal ShouldReg As Boolean, ByVal dsWipData As String,
                         ByVal dsWipInstData As String, ByVal dsMoveFaciData As String) As RIAResult
        Try
            InitClassSave(LoginInfo)
            InitClassValidate(LoginInfo)
            Using WipData As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipData)
                '移機單 需要裝機的相關資料
                Dim WipInstData As DataSet = Nothing
                If dsWipInstData IsNot Nothing Then
                    If Not String.IsNullOrEmpty(dsWipInstData) Then
                        WipInstData = Silverlight.DataSetConnector.Connector.FromXml(dsWipInstData)
                    End If
                End If
                '移機功能 其它服務別的工單資料
                Dim MoveFaciData As DataSet = Nothing
                If dsMoveFaciData IsNot Nothing Then
                    If Not String.IsNullOrEmpty(dsMoveFaciData) Then
                        MoveFaciData = Silverlight.DataSetConnector.Connector.FromXml(dsMoveFaciData)
                    End If
                End If
                result = _PRValidate.ChkDataOk(EditMode, WipData)
                If result.ResultBoolean = False Then
                    Return result
                End If
                result = _PRSave.Save(EditMode, ShouldReg, WipData, WipInstData, False, MoveFaciData)
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PRValidate.Dispose()
            _PRSave.Dispose()
        End Try
        Return result
    End Function

    ''' <summary>
    ''' 作廢維修單
    ''' </summary>
    ''' <param name="SNo">維修單號</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function VoidData(ByVal LoginInfo As LoginInfo, ByVal SNo As String,
                             ByVal Custid As Integer, ByVal ServiceType As String) As RIAResult
        Try
            InitClassVoidData(LoginInfo)
            Return _PRVoidData.VoidData(SNo, Custid, ServiceType)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            _PRVoidData.Dispose()
        End Try
    End Function

    Public Function InsSmartCardNo(ByVal LoginInfo As LoginInfo, FaciData As String, ByVal SeqNo As String) As RIAResult
        Try
            InitMaintainClass(LoginInfo)
            Using dsFaciData As DataTable = Silverlight.DataSetConnector.Connector.FromXml(FaciData).Tables("Facility")
                Using dt As DataTable = _Maintain.InsSmartCardNo(dsFaciData, SeqNo)
                    result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                    result.ResultBoolean = True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _Maintain.Dispose()
        End Try
        Return result
    End Function

    '互動進入，如過有指定設備的話需要將設備填入ChangeFacility
    Public Function GetDefChangeFaciData(ByVal LoginInfo As LoginInfo, ByVal dsWipData As String,
                                         ByVal WipType As Int32, ByVal WipRefNo As Int32,
                                         ByVal ReInstAcrossFlag As Boolean) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipData)
                Dim retDS As DataSet = Nothing
                retDS = _PR.GetDefChangeFaciData(ds, WipType, WipRefNo, ReInstAcrossFlag)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(retDS, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function

    Public Function GetCanMoveServiceType(ByVal LoginInfo As LoginInfo, ByVal CustId As Integer,
                                          ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(_PR.GetCanMoveServiceType(CustId, ServiceType), JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function

    Public Function GetChangeFacilityPinCode(ByVal LoginInfo As LoginInfo, ByVal CustId As Integer,
                                             ByVal InSeqNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(_PR.GetChangeFacilityPinCode(CustId, InSeqNo).DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function

#Region "同區移機"
    Public Function GetReInstAddr(ByVal LoginInfo As LoginInfo, ByVal ID As String, ByVal AddrNo As String, ByVal AddrSort As String) As RIAResult
        Try
            InitClassReInst(LoginInfo)
            Using dt As DataTable = _PRReInst.GetNewAddressData(ID, AddrNo, AddrSort)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PRReInst.Dispose()
        End Try
        Return result
    End Function

    Public Function GetCD046(ByVal LoginInfo As LoginInfo, CodeNo As String) As RIAResult
        Try
            InitClassReInst(LoginInfo)
            Using dt As DataTable = _PRReInst.GetCD046(CodeNo)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PRReInst.Dispose()
        End Try
        Return result
    End Function

    Public Function GetSO002(ByVal LoginInfo As LoginInfo, AddrNo As Int32) As RIAResult
        Try
            InitClassReInst(LoginInfo)
            Using dt As DataTable = _PRReInst.GetSO002(AddrNo)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PRReInst.Dispose()
        End Try
        Return result
    End Function

    Public Function GetCD005(ByVal LoginInfo As LoginInfo, ServiceType As String) As RIAResult
        Try
            InitClassReInst(LoginInfo)
            result.ResultXML = _PRReInst.GetCD005(ServiceType)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        Finally
            _PRReInst.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 刪除暫存點數
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DelResvPoint(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            result.ResultBoolean = _PR.DelResvPoint()
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        Finally
            _PR.Dispose()
        End Try
        Return result
    End Function

#End Region

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

