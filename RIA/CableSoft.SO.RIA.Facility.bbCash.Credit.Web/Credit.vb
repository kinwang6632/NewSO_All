
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
Public Class Credit
    Inherits DomainService
    Private _Credit As CableSoft.SO.BLL.Facility.bbCash.Credit.Credit
    Private result As New RIAResult()
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _Credit = New CableSoft.SO.BLL.Facility.bbCash.Credit.Credit(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Public Function ChangePoint(ByVal LoginInfo As LoginInfo,
                               ByVal transStoreCode As Integer, ByVal bbAccountId As String,
                               ByVal FaciSeqNo As String, ByVal FaciSNo As String, ByVal MinusPoint As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _Credit.ChangePoint(transStoreCode, bbAccountId, FaciSeqNo, FaciSNo, MinusPoint)


        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Credit.Dispose()
        End Try
        Return result
    End Function
End Class

