
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
Public Class IDData
    Inherits DomainService
    Private _IDDataBLL As CableSoft.SO.BLL.Facility.IDData.IDData
    Private result As New RIAResult()
    Private Sub InitClass(ByVal aLoginInfo As LoginInfo)
        _IDDataBLL = New CableSoft.SO.BLL.Facility.IDData.IDData(LoginInfo.ConvertTo(aLoginInfo))
    End Sub
    ''' <summary>
    ''' 取得證件圖檔
    ''' </summary>
    ''' <param name="LoginInfo"></param>
    ''' <param name="PicturePath">圖檔路徑</param>
    ''' <param name="PictureName">圖檔名稱</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks>True Or False</remarks>
    Public Function QueryIDPicture(ByVal LoginInfo As LoginInfo,
                                       ByVal PicturePath As String,
                                       ByVal PictureName As String) As RIAResult
        InitClass(LoginInfo)

        Return RIAResult.Convert(_IDDataBLL.QueryIDPicture(PicturePath, PictureName))
    End Function
    ''' <summary>
    ''' 存檔(Save)
    ''' </summary>
    ''' <param name="LoginInfo">使用者資訊</param>
    ''' <param name="EditMode">狀態</param>
    ''' <param name="dsIDData">SO004E DataSet</param>
    ''' <returns>RIAResult</returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal LoginInfo As LoginInfo,
                         ByVal EditMode As CableSoft.BLL.Utility.EditMode, ByVal dsIDData As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim obj As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsIDData)
            result = RIAResult.Convert(_IDDataBLL.Save(EditMode,
                                                           obj))
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得可選證件種類
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks>0:申請人第一證件</remarks>
    Public Function QueryIDKind(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _IDDataBLL.QueryIDKind
            result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)

        End Try
        Return result
    End Function
    ''' <summary>
    ''' 取得證件資料
    ''' </summary>
    ''' <param name="FaciSNO">設備序號</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function QueryIDData(ByVal LoginInfo As LoginInfo,
                                     ByVal FaciSNO As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _IDDataBLL.QueryIDData(FaciSNO)
            result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
        End Try
        Return result
    End Function
    Public Function CanEdit(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Return RIAResult.Convert(_IDDataBLL.CanEdit)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        End Try
        
    End Function
    Public Function CanAppend(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Return RIAResult.Convert(_IDDataBLL.CanAppend)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        End Try
        
    End Function
    Public Function CanDelete(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Return RIAResult.Convert(_IDDataBLL.CanDelete)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        End Try
        
    End Function
End Class

