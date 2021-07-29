
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
            Result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
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
            Result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
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
    Public Function API252(ByVal LoginInfo As LoginInfo) As RIAResult
        InitClass(LoginInfo)
        Dim _API As New CableSoft.SO.BLL.Wip.Maintain.BillingAPI252(LoginInfo)
        Dim InData As New DataSet
        Dim dtMain As New DataTable("Main")
        Dim dtSNO As New DataTable("SNO")
        dtMain.Columns.Add(New DataColumn("APIID", GetType(String)))
        dtMain.Columns.Add(New DataColumn("Compcode", GetType(Integer)))
        dtMain.Columns.Add(New DataColumn("Caller", GetType(String)))
        dtMain.Columns.Add(New DataColumn("Seqno", GetType(String)))

        dtSNO.Columns.Add(New DataColumn("CustId", GetType(Integer)))
        dtSNO.Columns.Add(New DataColumn("ServiceCode", GetType(String)))
        dtSNO.Columns.Add(New DataColumn("Priority", GetType(Integer)))
        dtSNO.Columns.Add(New DataColumn("ResvTime", GetType(String)))
        dtSNO.Columns.Add(New DataColumn("AcceptEn", GetType(String)))
        dtSNO.Columns.Add(New DataColumn("Note", GetType(String)))
        dtSNO.Columns.Add(New DataColumn("PrintBillFlag", GetType(Integer)))
        dtSNO.Columns.Add(New DataColumn("ResvFlagTime", GetType(String)))
        dtSNO.Columns.Add(New DataColumn("Faciseqno", GetType(String)))
        dtSNO.Columns.Add(New DataColumn("ServiceType", GetType(String)))
        dtSNO.Columns.Add(New DataColumn("Kind", GetType(Integer)))
        dtSNO.Columns.Add(New DataColumn("CallSeqNo", GetType(String)))


        Dim rwNew As DataRow = dtMain.NewRow
        rwNew.Item("APIID") = "252"
        rwNew.Item("Compcode") = 3
        rwNew.Item("Caller") = "CSR"
        rwNew.Item("Seqno") = "201401150628114539"
        dtMain.Rows.Add(rwNew)

        Dim rwSNo As DataRow = dtSNO.NewRow
        'rwSNo.Item("CustId") = 600028
        'rwSNo.Item("ServiceCode") = "999"
        'rwSNo.Item("Priority") = 1
        'rwSNo.Item("ResvTime") = "2015/03/01 12:00:00"
        'rwSNo.Item("AcceptEn") = "TEST"
        'rwSNo.Item("Note") = "施工前請先電聯"
        'rwSNo.Item("PrintBillFlag") = 1
        'rwSNo.Item("ResvFlagTime") = ""
        ''rwSNo.Item("Faciseqno") = "199902030169828"
        'rwSNo.Item("Faciseqno") = ""
        'rwSNo.Item("ServiceType") = "C"
        'rwSNo.Item("Kind") = 0
        'rwSNo.Item("CallSeqNo") = "200403190158998"
        rwSNo.Item("CustId") = 650056
        rwSNo.Item("ServiceCode") = "273"
        rwSNo.Item("Priority") = 1
        rwSNo.Item("ResvTime") = "2015/05/14 12:00:00"
        rwSNo.Item("AcceptEn") = "1610890404"
        rwSNo.Item("Note") = "施工前請先電聯"
        rwSNo.Item("PrintBillFlag") = 1
        rwSNo.Item("ResvFlagTime") = ""
        rwSNo.Item("Faciseqno") = "201108150105637"
        'rwSNo.Item("Faciseqno") = ""
        rwSNo.Item("ServiceType") = "D"
        rwSNo.Item("Kind") = 0
        rwSNo.Item("CallSeqNo") = "201505131560370"
        dtSNO.Rows.Add(rwSNo)
        Try
            InData.Tables.Add(dtMain)
            InData.Tables.Add(dtSNO)
            InData.AcceptChanges()
            Return _API.Execute(1234, InData)
        Catch ex As Exception
            Throw
        Finally
            _API.Dispose()
        End Try
    End Function
    Public Function API235(ByVal LoginInfo As LoginInfo, ByVal SNo As String) As RIAResult
        InitClass(LoginInfo)
        Dim _API As New CableSoft.SO.BLL.Wip.Maintain.BillingAPI235(LoginInfo)
        Dim InData As New DataSet
        Dim dtMain As New DataTable("Main")
        dtMain.Columns.Add(New DataColumn("APIID", GetType(String)))
        dtMain.Columns.Add(New DataColumn("Compcode", GetType(Integer)))
        dtMain.Columns.Add(New DataColumn("Caller", GetType(String)))
        dtMain.Columns.Add(New DataColumn("Seqno", GetType(String)))
        dtMain.Columns.Add(New DataColumn("SNO", GetType(String)))
        dtMain.Columns.Add(New DataColumn("ResvTime", GetType(String)))
        Dim rwNew As DataRow = dtMain.NewRow
        rwNew.Item("APIID") = "235"
        rwNew.Item("Compcode") = 3
        rwNew.Item("Caller") = "IVR"
        rwNew.Item("Seqno") = "20140115062810451"
        rwNew.Item("SNO") = SNo
        'rwNew.Item("ResvTime") = Now.ToString("yyyy/MM/dd HH:mm:ss")
        rwNew.Item("ResvTime") = "2015/03/01 12:00:00"
        dtMain.Rows.Add(rwNew)
        Try
            InData.Tables.Add(dtMain)
            Return _API.Execute(1234, InData)
        Catch ex As Exception
            Throw
        Finally
            _API.Dispose()
        End Try

    End Function
    Public Function bbCredit(ByVal LoginInfo As LoginInfo, ByVal transStoreCode As Integer,
                              ByVal bbAccountId As String, ByVal FaciSeqNo As String,
                              ByVal FaciSNo As String, ByVal MinusPoint As Integer) As RIAResult
        InitClass(LoginInfo)
        Dim _Credit As New CableSoft.SO.BLL.Facility.bbCash.Credit.Credit(LoginInfo)
        Try
            Return _Credit.ChangePoint(transStoreCode, bbAccountId, FaciSeqNo, FaciSNo, MinusPoint)
        Catch ex As Exception
            Throw
        Finally
            _Credit.Dispose()
        End Try


    End Function

    Public Function GetSO105B(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim aSQL As String = "select * from so105b where orderno='200403010003465'"
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

