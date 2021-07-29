
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
Public Class ChangeFaci
    'Inherits DomainService
    Implements IDisposable
    Private _ChangFaci As CableSoft.SO.BLL.Facility.ChangeFaci.ChangeFaci
    Private result As New RIAResult()
    Private Const fMaintain_Wip As String = "Wip"
    Private Const fMaintain_Facility As String = "Facility"
    Private Const fMaintain_PRFacility As String = "PRFacility"
    Private Const fMaintain_Charge As String = "Charge"
    Private Const fMaintain_ChangeFacility As String = "ChangeFacility"
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _ChangFaci = New CableSoft.SO.BLL.Facility.ChangeFaci.ChangeFaci(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Public Function GetCanChangeKind(ByVal LoginInfo As LoginInfo,
                                     ByVal WipType As Int32, WipRefNo As Int32, ReInstAcrossFlag As Boolean) As RIAResult

        Try
            InitClass(LoginInfo)
            Using ds As New DataSet
                ds.Tables.Add(_ChangFaci.GetCanChangeKind(WipType, WipRefNo, ReInstAcrossFlag))
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using



        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function
    Public Function GetChooseServiceIDs(ByVal LoginInfo As LoginInfo,
                                        ByVal CustId As Integer, ByVal FaciSeqNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim str As String = _ChangFaci.GetChooseServiceIDs(CustId, FaciSeqNo)

            result.ResultXML = str
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function
    Public Function GetServiceIdAndCitemCode(ByVal LoginInfo As LoginInfo,
                                             ByVal CustId As Integer, ByVal FaciSeqNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangFaci.GetServiceIdAndCitemCode(CustId, FaciSeqNo)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function
    Public Function GetChangeFacility(ByVal LoginInfo As LoginInfo,
                                ByVal Kind As Int32, dtWipRow As String,
                                  dtFaciRow As String, dtInChangeDataRow As String,
                                  DeleteCitems As String,
                                  ChooseServiceIDs As String,
                                  dtChangeFacility As String) As RIAResult


        Try
            InitClass(LoginInfo)
            Dim dtWipData As DataTable = Silverlight.DataSetConnector.Connector.FromXml(dtWipRow).Tables(0)
            Dim dtInChangeData = Silverlight.DataSetConnector.Connector.FromXml(dtInChangeDataRow).Tables(0)
            Dim dtChangeFacilityData = Silverlight.DataSetConnector.Connector.FromXml(dtChangeFacility).Tables(0)
            Dim dtFacilityData As DataTable = Nothing
            Dim rwFacilityData As DataRow = Nothing
            If Not String.IsNullOrEmpty(dtFaciRow) Then
                dtFacilityData = Silverlight.DataSetConnector.Connector.FromXml(dtFaciRow).Tables(0)
                rwFacilityData = dtFacilityData.Rows(0)
            End If
            If dtWipData.Rows.Count = 0 Then
                Dim rw As DataRow = dtWipData.NewRow
                dtWipData.Rows.Add(rw)
                dtWipData.AcceptChanges()
            End If


            Using ds As New DataSet
                _ChangFaci.GetChangeFacility(Kind, dtWipData.Rows(0), rwFacilityData,
                                                             dtInChangeData.Rows(0),
                                                                 DeleteCitems, ChooseServiceIDs, dtChangeFacilityData)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dtChangeFacilityData.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dtChangeFacilityData.DataSet)
                result.ResultBoolean = True
            End Using


            'Dim ds As DataSet = _ChangFaci.GetChangeFacility(Kind, WipRow, FaciRow,
            '                                                 InChangeDataRow,
            '                                                     DeleteCitems, ChangeFacility).DataSet


        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result

    End Function
    Public Function GetFaciCode(ByVal LoginInfo As LoginInfo,
                                ByVal SeqNos As String, ByVal CustId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangFaci.GetFaciCode(SeqNos, CustId).DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function
    Public Function ChkDataOK(ByVal LoginInfo As LoginInfo,
                             ByVal SeqNo As String, ByVal SNO As String) As RIAResult
        Try
            InitClass(LoginInfo)

            'Return RIAResult.Convert(_ChangFaci.ChkDataOK(SeqNo, SNO))
            Return _ChangFaci.ChkDataOK(SeqNo, SNO)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得可指定變更設備
    ''' </summary>
    ''' <param name="CustId">客編</param>
    ''' <param name="ServiceType">服務別</param>
    ''' <param name="IncludePR">包含拆除</param>
    ''' <param name="IncludeDVR">包否DVR</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetCanChangeFaci(ByVal LoginInfo As LoginInfo,
                                    ByVal CustId As Integer,
                                     ByVal ServiceType As String,
                                     ByVal IncludePR As Boolean,
                                     ByVal IncludeDVR As Boolean,
                                     ByVal IncludFilter As Boolean,
                                     ByVal WipType As Integer,
                                     ByVal wipData As String) As RIAResult


        Try
            InitClass(LoginInfo)
            Dim dtSource As DataSet = Silverlight.DataSetConnector.Connector.FromXml(wipData)
            Dim ds As DataSet = _ChangFaci.GetCanChangeFaci(CustId, ServiceType,
                                                            IncludePR, IncludeDVR, IncludFilter, WipType, dtSource)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function
    Public Function GetCMRateCode(ByVal LoginInfo As LoginInfo,
                                  ByVal Type As Integer,
                                  ByVal CMRateCode As Integer) As RIAResult

        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangFaci.GetCMRateCode(Type, CMRateCode).DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result

    End Function
    Public Function GetCPEMAC(ByVal LoginInfo As LoginInfo,
                          ByVal CustId As Integer, ByVal FaciSeqNo As String) As RIAResult

        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangFaci.GetCPEMAC(CustId, FaciSeqNo)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result

    End Function
    Public Function GetIPCount(ByVal LoginInfo As LoginInfo,
                               ByVal Type As Integer, ByVal IPCount As Integer, ByVal ZeroIPCount As Boolean) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangFaci.GetIPCount(Type, IPCount, ZeroIPCount).DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function

    Public Function GetDVRSizeCode(ByVal LoginInfo As LoginInfo,
                                   ByVal Type As Integer, ByVal DVRSizeCode As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangFaci.GetDVRSizeCode(Type, DVRSizeCode).DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得週期性收費資料
    ''' </summary>
    ''' <param name="CustId">客編</param>
    ''' <param name="FaciSeqNo">設備流水號</param>
    ''' <param name="ServiceType">服務別</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPeriodCharge(ByVal LoginInfo As LoginInfo,
                                    ByVal CustId As Integer, ByVal FaciSeqNo As String,
                                    ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ChangFaci.GetPeriodCharge(CustId, FaciSeqNo, ServiceType).DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function
    Public Function GetPRFaci(ByVal LoginInfo As LoginInfo,
                                 ByVal SNo As String,
                                  ByVal FaciSeqNo As String, ByVal dsSourceWip As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dsSource As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsSourceWip)
            Dim ds As DataSet = _ChangFaci.GetPRFaci(SNo, FaciSeqNo, dsSource)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function

    Public Function GetMovePRFaci(ByVal LoginInfo As LoginInfo,
                                 ByVal SNo As String,
                                  ByVal FaciSeqNo As String, ByVal dsSourceWip As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dsSource As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsSourceWip)
            Dim ds As DataSet = _ChangFaci.GetMovePRFaci(SNo, FaciSeqNo, dsSource)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function

    ''' <summary>
    ''' 取得指定更換設備資訊
    ''' </summary>
    ''' <param name="SNo">工單單號</param>
    ''' <param name="FaciSeqNo">設備流水號</param>    
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Function GetReInstFaci(ByVal LoginInfo As LoginInfo,
                                 ByVal SNo As String,
                                  ByVal FaciSeqNo As String,
                                  ByVal dsSourceWip As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dsSource As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsSourceWip)
            Dim ds As DataSet = _ChangFaci.GetReInstFaci(SNo, FaciSeqNo, dsSource, False)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得指定變更設備資訊
    ''' </summary>
    ''' <param name= "dtWip">工單資料</param>
    ''' <param name="dtFacility">設備資料</param>
    ''' <returns>DataTable</returns>
    ''' <remarks>ChangeFacility</remarks>
    Public Function GetChangeFacility2(ByVal LoginInfo As LoginInfo,
                                     ByVal dtWip As String,
                                      ByVal dtFacility As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dtWipData As DataTable = Silverlight.DataSetConnector.Connector.FromXml(dtWip).Tables(0)
            Dim dtFacilityData As DataTable = Silverlight.DataSetConnector.Connector.FromXml(dtFacility).Tables(0)
            Dim ds As DataSet = _ChangFaci.GetChangeFacility(dtWipData, dtFacilityData).DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得所有變更設備
    ''' </summary>
    ''' <param name="LoginInfo">LoginInfo</param>
    ''' <param name="dsChangeFacility">ChangeFacility</param>
    ''' <returns>DataTable</returns>
    ''' <remarks>Facility</remarks>
    Public Function GetAllChangeData(ByVal LoginInfo As LoginInfo,
                                    ByVal dsChangeFacility As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dtSource As DataTable = Silverlight.DataSetConnector.Connector.FromXml(dsChangeFacility).Tables(0)
            Dim dt As DataTable = _ChangFaci.GetAllChangeData(dtSource)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得指定維修設備資訊
    ''' </summary>
    ''' <param name="SNo">工單單號</param>
    ''' <param name="FaciSeqNo">設備流水號</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMaintainFaci(ByVal LoginInfo As LoginInfo,
                                    ByVal SNo As String, ByVal FaciSeqNo As String, ByVal dsSourceWip As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dsSource As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsSourceWip)
            Dim ds As DataSet = _ChangFaci.GetMaintainFaci(SNo, FaciSeqNo, dsSource).DataSet
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChangFaci.Dispose()
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

