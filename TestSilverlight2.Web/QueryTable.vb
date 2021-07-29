
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
Public Class QueryTable
    Inherits DomainService
    Private Result As RIAResult = Nothing
    Private _ConnectInfo As ConnectInfo = Nothing
    Private Sub InitClass(ByVal LoginInfo As CableSoft.BLL.Utility.LoginInfo)
        '_Facility = New CableSoft.SO.BLL.Order.Facility.Facility(LoginInfo.ConvertTo(LoginInfo))
        LoginInfo.ConvertTo(LoginInfo)
        If String.IsNullOrEmpty(LoginInfo.ConnectionString) Then
            LoginInfo.ConnectionString = "Data Source=RDKNET;User ID=TBCSH;Password=TBCSH;Persist Security Info=True;Unicode=True;Min Pool Size=0;Max Pool Size=25;Connection Lifetime=60"
        End If
        Result = New RIAResult()
    End Sub
    Public Function GetSO013(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim aSQL As String = "select * from SO013 where introid='00000001'"
            Dim ds As New DataSet()
            Dim obj As New CableSoft.Utility.DataAccess.DAO(LoginInfo.Provider, LoginInfo.ConnectionString)
            Dim dtSO013 As DataTable = obj.ExecQry(aSQL)
            dtSO013.TableName = "IntroId"
            ds.Tables.Add(dtSO013.Copy)
            Result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            Result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(Result, ex, LoginInfo.DebugMode)
        End Try
        Return Result
    End Function
    Public Function GetCD068(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim aSQL As String = "select * from CD068"
            Dim ds As New DataSet()
            Dim obj As New CableSoft.Utility.DataAccess.DAO(LoginInfo.Provider, LoginInfo.ConnectionString)
            Dim dtCD001 As DataTable = obj.ExecQry(aSQL)
            dtCD001.TableName = "CD068"
            ds.Tables.Add(dtCD001.Copy)
            Result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds)
            Result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(Result, ex, LoginInfo.DebugMode)
        End Try
        Return Result
    End Function
    Public Function WriteText(ByVal LoginInfo As LoginInfo) As RIAResult
        InitClass(LoginInfo)

        Result.ResultBoolean = True
        Return Result
    End Function
    Public Function GetCD001(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim aSQL As String = "select * from cd001"
            Dim ds As New DataSet()            
            Dim obj As New CableSoft.Utility.DataAccess.DAO(LoginInfo.Provider, LoginInfo.ConnectionString)
            Dim dtCD001 As DataTable = obj.ExecQry(aSQL)
            dtCD001.TableName = "CD001"
            ds.Tables.Add(dtCD001.Copy)
            'Result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            Result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds)
            Result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(Result, ex, LoginInfo.DebugMode)
        End Try
        Return Result
    End Function
    Public Function GetDeposit(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim aSQLCharge As String = "select * from so105b where OrderNo='200403010003465'"
            Dim aSQLFacility As String = String.Format("select * from so105d where faciseqno in ( {0} )",
                                                      "select faciseqno from so105b where OrderNo ='200403010003465'")
            Dim ds As New DataSet()
            Dim obj As New CableSoft.Utility.DataAccess.DAO(LoginInfo.Provider, LoginInfo.ConnectionString)
            Dim dtCharge As DataTable = obj.ExecQry(aSQLCharge)
            dtCharge.TableName = "Charge"
            Dim dtFacility As DataTable = obj.ExecQry(aSQLFacility)
            dtFacility.TableName = "Facility"
            ds.Tables.Add(dtCharge.Copy)
            ds.Tables.Add(dtFacility.Copy)
            Result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
            Result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(Result, ex, LoginInfo.DebugMode)
        End Try
        Return Result
    End Function
    Public Function FromXMLToDataSet(ByVal strXML As String) As RIAResult
        Dim Result As New RIAResult With {.ErrorCode = 0, .ResultBoolean = True}
        Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(strXML)

        Result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)

        Return Result


    End Function
    Public Function GetDepositFacility(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim aSQL As String = "select faciseqno from so105D where Faciseqno='0045799'"
            Dim obj As New CableSoft.Utility.DataAccess.DAO(LoginInfo.Provider, LoginInfo.ConnectionString)
            Dim dt As DataTable = obj.ExecQry(aSQL)

            Result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            Result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(Result, ex, LoginInfo.DebugMode)
        End Try
        Return Result
    End Function
    Public Function GetDepositCharge(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim aSQL As String = "select * from so105b where Rowid='AAASYYAAGAAGY8bAAN'"
            Dim obj As New CableSoft.Utility.DataAccess.DAO(LoginInfo.Provider, LoginInfo.ConnectionString)
            Dim dt As DataTable = obj.ExecQry(aSQL)

            Result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            Result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(Result, ex, LoginInfo.DebugMode)
        End Try
        Return Result
    End Function
    Public Function GetSO105B(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim aSQL As String = "select * from so105b where orderno='200503210014004'"
            Dim obj As New CableSoft.Utility.DataAccess.DAO(LoginInfo.Provider, LoginInfo.ConnectionString)

            Dim dt As DataTable = obj.ExecQry(aSQL)

            Result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            Result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(Result, ex, LoginInfo.DebugMode)
        End Try
        Return Result
    End Function
    Public Function GetSO105D(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim aSQL As String = "Select * From SO105D Where RowNum=1"
            Dim obj As New CableSoft.Utility.DataAccess.DAO(LoginInfo.Provider, LoginInfo.ConnectionString)

            Dim dt As DataTable = obj.ExecQry(aSQL)

            Result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
            Result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(Result, ex, LoginInfo.DebugMode)
        End Try
        Return Result
    End Function
End Class

