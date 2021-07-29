
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
Imports System.Configuration


'TODO: 建立包含應用程式邏輯的方法。
<EnableClientAccess()> _
Public Class DVRData
    Inherits DomainService
    Private _DVRDataBLL As CableSoft.SO.BLL.Facility.DVRData.DVRData
    Private result As New RIAResult()
    ''' <summary>
    ''' 檢核號碼是否正確
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <param name="SeqNo">設備流水號</param>
    ''' <param name="PhoneNumber">行動電話</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks>True Or False</remarks>
    Public Function ChkPhoneNumberOk(ByVal LoginInfo As LoginInfo,
                                       ByVal SeqNo As String,
                                       ByVal PhoneNumber As String) As RIAResult
        InitClass(LoginInfo)

        Return RIAResult.Convert(_DVRDataBLL.ChkPhoneNumberOk(SeqNo, PhoneNumber, CableSoft.Utility.Connection.Config.GetConnStrDict))
    End Function
    ''' <summary>
    ''' 取得行動電話資料
    ''' </summary>
    ''' <param name="FaciSeqNo">設備流水號</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function QueryPhoneNumber(ByVal LoginInfo As LoginInfo,
                                     ByVal FaciSeqNo As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _DVRDataBLL.QueryPhoneNumber(FaciSeqNo)
            result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 存檔(Save)
    ''' </summary>
    ''' <param name="LoginInfo">使用者資訊</param>
    ''' <param name="EditMode">狀態</param>
    ''' <param name="dsDVRData">SO004H DataSet</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal LoginInfo As LoginInfo,
                         ByVal EditMode As CableSoft.BLL.Utility.EditMode, ByVal dsDVRData As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim obj As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsDVRData)
            result = RIAResult.Convert(_DVRDataBLL.Save(EditMode,
                                                           obj))
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        End Try
        Return result
    End Function
    Private Sub InitClass(ByVal aLoginInfo As LoginInfo)
        _DVRDataBLL = New CableSoft.SO.BLL.Facility.DVRData.DVRData(LoginInfo.ConvertTo(aLoginInfo))
    End Sub

    'Public Shared Function GetConnectionString() As DataTable
    '    Dim appReader As New AppSettingsReader()
    '    Dim ConnInfoDEC As String = appReader.GetValue("ConnInfo", GetType(String))
    '    Dim ConnInfo As String = CableSoft.Utility.Cryptography.DES.Decrypt(ConnInfoDEC)
    '    Return ConvertXMLToDataTable(ConnInfo)
    'End Function
End Class

