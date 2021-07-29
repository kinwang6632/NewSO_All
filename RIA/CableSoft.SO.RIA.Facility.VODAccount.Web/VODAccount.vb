
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


'TODO: 建立包含應用程式邏輯的方法。
<EnableClientAccess()>  _
Public Class VODAccount
    Inherits DomainService
    Private _VODAccountBLL As CableSoft.SO.BLL.Facility.VODAccount.VODAccount
    Private result As New RIAResult
    ''' <summary>
    ''' 查詢VOD資訊
    ''' </summary>
    ''' <param name="VODAccountID">VODAccountID</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function QueryVODAccount(ByVal LoginInfo As LoginInfo, ByVal VODAccountID As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _VODAccountBLL.QueryVODAccount(VODAccountID)
            result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得可選點數行銷辦法
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetSalePointcode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _VODAccountBLL.GetSalePointcode
            result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 查詢可選修改人員
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetReqEmpNo(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _VODAccountBLL.GetReqEmpNo
            result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        End Try
        Return result
    End Function
    Private Sub InitClass(ByVal aLoginInfo As LoginInfo)
        _VODAccountBLL = New CableSoft.SO.BLL.Facility.VODAccount.VODAccount(LoginInfo.ConvertTo(aLoginInfo))
    End Sub
    ''' <summary>
    ''' 存檔(Save)
    ''' </summary>
    ''' <param name="LoginInfo">使用者資訊</param>
    ''' <param name="EditMode">狀態</param>
    ''' <param name="dsVODData">SO004G DataSet</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal LoginInfo As LoginInfo,
                         ByVal EditMode As CableSoft.BLL.Utility.EditMode, ByVal dsVODData As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim obj As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsVODData)
            result = RIAResult.Convert(_VODAccountBLL.Save(EditMode,
                                                           obj))
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        End Try
        Return result
    End Function
End Class

