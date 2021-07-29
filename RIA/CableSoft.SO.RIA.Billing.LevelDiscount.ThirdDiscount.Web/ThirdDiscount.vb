
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
Imports CableSoft.SO.BLL.Billing.LevelDiscount
'TODO: 建立包含應用程式邏輯的方法。
<EnableClientAccess()>  _
Public Class ThirdDiscount
    Inherits DomainService
    Private _ThirdDiscount As CableSoft.SO.BLL.Billing.LevelDiscount.ThirdDiscount
    Private result As New RIAResult()
    Public Property isHTML As Boolean = False
    Public Function Save(ByVal LoginInfo As LoginInfo,
                         ByVal EditMode As EditMode, ByVal dsThirdDiscount As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim obj As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsThirdDiscount)
            result = _ThirdDiscount.Save(EditMode,
                                                           obj)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo)
            result.ResultBoolean = False
            Return result
        Finally
            _ThirdDiscount.Dispose()
        End Try
        Return result
    End Function
    Public Function QueryThirdDiscount(ByVal LoginInfo As LoginInfo,
                                       ByVal aCustId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _ThirdDiscount.QueryThirdDiscount(aCustId)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ThirdDiscount.Dispose()
        End Try
        Return result
    End Function
    Public Function QueryCanChooseFaci(ByVal LoginInfo As LoginInfo,
                                        ByVal aCustId As Integer,
                                        ByVal aServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim dt As DataTable = _ThirdDiscount.QueryCanChooseFaci(aCustId, aServiceType)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ThirdDiscount.Dispose()
        End Try
        Return result
    End Function
    Public Function CanView(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Return _ThirdDiscount.CanView
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ThirdDiscount.Dispose()
        End Try
    End Function
    Public Function CanEdit(ByVal LoginInfo As LoginInfo) As RIAResult

        Try
            InitClass(LoginInfo)
            Return _ThirdDiscount.CanEdit
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _ThirdDiscount.Dispose()
        End Try

    End Function

    Private Sub InitClass(ByVal aLoginInfo As LoginInfo)
        _ThirdDiscount = New CableSoft.SO.BLL.Billing.LevelDiscount.ThirdDiscount(LoginInfo.ConvertTo(aLoginInfo))
    End Sub
End Class

