
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
Imports System.Web.SessionState
Imports System.Web.Configuration

Imports CableSoft.Utility.Connection
Imports CableSoft.BLL.Utility

'TODO: 建立包含應用程式邏輯的方法。
<EnableClientAccess()>  _
Public Class Connect
    Inherits DomainService
    Private _ConnectInfo As ConnectInfo = Nothing
    Public Shared appReader As New AppSettingsReader
    Public Function GetLoginInfo() As LoginInfo
        Return New LoginInfo()
    End Function
    Public Function GetConfigInfo(ByVal aCompCode As Integer) As ConnectInfo
        'Return New ConfigInfo With {.ConntionString = Global_asax.GetConnString(CompCode.ToString), .Provider = Global_asax.GetProvider(), _
        '                           .DebugMode = Global_asax.GetDebugMode(), .HttpPath = Global_asax.GetPath("~/")}
        _ConnectInfo = New ConnectInfo()
        _ConnectInfo.ConntionString = Config.GetConnString(aCompCode.ToString)
        _ConnectInfo.ReoprtServiceUrl = Config.GetAppSet("ReportServicePath")
        _ConnectInfo.Provider = Config.GetProvider

        _ConnectInfo.DebugMode = True
        _ConnectInfo.HttpPath = HttpContext.Current.Server.MapPath("~/")

        '_ConnectInfo.ConntionString = "Data Source=RDKNET;User ID=TBCSH;Password=TBCSH;" & _
        '                                            "Persist Security Info=True;Unicode=True;Min Pool Size=0;Max Pool Size=25;" & _
        '                                            "Connection Lifetime=60"
        '_ConnectInfo.ReoprtServiceUrl = "http://192.168.10.80:7493/CSReportService.svc"
        '_ConnectInfo.Provider = "System.Data.OracleClient"

        '_ConnectInfo.DebugMode = True
        '_ConnectInfo.HttpPath = "D:\Gird\NewSO\Kin\TestSilverlight2.Web\"

        Return _ConnectInfo



    End Function

    Public Sub New()
        '((WebDomainClient<MyDomainContext.IMyDomainServiceContract>)this.DomainClient)
        '            .ChannelFactory.Endpoint.Binding.SendTimeout = new TimeSpan(0, 5, 0);

    End Sub
End Class
Public Class ConnectInfo
    Public Property ConntionString As String
    Public Property Provider As String
    Public Property DebugMode As Boolean
    Public Property HttpPath As String
    Public Property ReoprtServiceUrl As String
End Class
