
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
<EnableClientAccess()> _
Public Class PRReasonCode
    Inherits DomainService
    Private _PRReasonCode As CableSoft.SO.BLL.CodeSet.PRReasonCode.PRReasonCode
    Private result As New RIAResult
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _PRReasonCode = New CableSoft.SO.BLL.CodeSet.PRReasonCode.PRReasonCode(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Public Function Test(ByVal LoginInfo As LoginInfo) As RIAResult
        InitClass(LoginInfo)
        result.ResultBoolean = True
        Return result
    End Function
    Public Function CopyToOtherDB(ByVal LoginInfo As LoginInfo,
                                  ByVal IsCoverData As Boolean, ByVal dsSource As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsSource)
                result = _PRReasonCode.CopyToOtherDB(IsCoverData, ds)
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _PRReasonCode.Dispose()
        End Try
        Return result
    End Function
    Public Function Execute(ByVal LoginInfo As LoginInfo,
                            ByVal EditMode As EditMode, ByVal dsMaster As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dsSource As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsMaster)
                result = _PRReasonCode.Execute(EditMode, dsSource)
            End Using
            If result.ResultDataSet IsNot Nothing Then
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(result.ResultDataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)

            End If
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _PRReasonCode.Dispose()
        End Try
        Return result
    End Function
    Public Function GetAllData(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _PRReasonCode.GetAllData
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _PRReasonCode.Dispose()
        End Try
        Return result
    End Function
    Public Function GetMaxCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)

            result.ResultXML = _PRReasonCode.GetMaxCode
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _PRReasonCode.Dispose()
        End Try
        Return result
    End Function
    Public Function QueryCD014A(ByVal LoginInfo As LoginInfo, ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _PRReasonCode.QueryCD014A(ServiceType)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _PRReasonCode.Dispose()
        End Try
        Return result
    End Function
End Class

