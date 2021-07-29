
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
Public Class ChooseFaci
    'Inherits DomainService
    Implements IDisposable
    Private _ChooseFaci As CableSoft.SO.BLL.Facility.Facility
    Private result As New RIAResult()
    Public Property isHTML As Boolean = False
    ''' <summary>
    ''' 查詢可選取設備資料
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <param name="aCustId">客戶編號</param>
    ''' <param name="aServiceType">服務別</param>
    ''' <param name="IncludePR">含拆機設備</param>
    ''' <param name="IncludeDVR">含DVR設備</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function QueryCanChooseFaci(ByVal LoginInfo As LoginInfo,
                                    ByVal aCustId As Integer,
                                    ByVal aServiceType As String,
                                    ByVal IncludePR As Boolean,
                                    ByVal IncludeDVR As Boolean,
                                    ByVal IncludFilter As Boolean,
                                    ByVal noRefNo As String,
                                    ByVal AddRefNo As String,
                                    ByVal InFaciCode As String
                                    ) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _ChooseFaci.QueryCanChooseFaci(aCustId, aServiceType, _
                                                                 IncludePR, IncludeDVR, IncludFilter, noRefNo, AddRefNo, InFaciCode)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChooseFaci.Dispose()
        End Try
        Return result
    End Function
    Public Function QueryMvodId(ByVal LoginInfo As LoginInfo, ByVal VodAccountIdS As String) As RIAResult
        Try
            Using _ChooseMVodId As New CableSoft.SO.BLL.Facility.VOD.Calculate.Calculate(LoginInfo.ConvertTo(LoginInfo))
                result.ResultBoolean = True
                result.ResultXML = _ChooseMVodId.QueryMVodId(VodAccountIdS)
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally

        End Try
        Return result
    End Function
    Public Function QueryChooseFaci(ByVal LoginInfo As LoginInfo,
                                    ByVal ServiceType As String,
                                    ByVal dsWipData As String,
                                    ByVal IncludePR As Boolean,
                                    ByVal IncludeDVR As Boolean,
                                    ByVal IncludeFilter As Boolean) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWipData)
            Dim dt As DataTable = _ChooseFaci.QueryChooseFaci(ServiceType, objDS, IncludePR, IncludeDVR, IncludeFilter)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ChooseFaci.Dispose()
        End Try
        Return result
    End Function
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _ChooseFaci = New CableSoft.SO.BLL.Facility.Facility(LoginInfo.ConvertTo(LoginInfo))
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

