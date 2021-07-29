
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
<EnableClientAccess()>  _
Public Class WriteText
    Inherits DomainService
    Private Result As RIAResult = Nothing
    Private _ConnectInfo As ConnectInfo = Nothing
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        '_Facility = New CableSoft.SO.BLL.Order.Facility.Facility(LoginInfo.ConvertTo(LoginInfo))
        LoginInfo.ConvertTo(LoginInfo)
        Result = New RIAResult()
    End Sub
    Private Sub WriteText(ByVal LoginInfo As LoginInfo)
        InitClass(LoginInfo)

        'Using obj As New CableSoft.BLL.Dynamic.TextFileOut.DynamicText
        '    obj.WriteText()
        'End Using

    End Sub
End Class

