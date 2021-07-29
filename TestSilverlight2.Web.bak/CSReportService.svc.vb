Imports System
Imports System.ServiceModel
Imports System.ServiceModel.Activation
Imports DevExpress.Data.Utils.ServiceModel
Imports DevExpress.Xpf.Printing.Service
Imports DevExpress.XtraReports.Service
Imports DevExpress.XtraReports.UI
Imports DevExpress.Xpf.Printing.Service.Native.Services
Imports DevExpress.XtraReports.Service.Native.Services
Imports CableSoft.BLL.Utility
Imports System.Reflection
Imports CableSoft.Utility.Connection
Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports System.Runtime.Serialization.Json
Imports DevExpress.XtraPrinting
Imports System.Xml.Serialization

' NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CSReportService" 
' in code, svc and config file together.
<SilverLightFaultBehavior()>
Public Class CSReportService
    Inherits DevExpress.XtraReports.Service.ReportService
    ' Uncomment this method if you need to extend the base functionality.
    ' Protected Overrides Sub CreateReportByName(reportName As String) As XtraReport
    '     Return MyBase.CreateReportByName(reportName)
    ' End Sub
    Private Const _reportPath As String = "~/Report/{0}"
    Private Const _exportPath As String = "~/Export/{0}"
    Private Const _pdfPath As String = "~/pdf/{0}"
    Private Const _xpsPath As String = "~/xps/{0}"
    Private Const _prnxPath As String = "~/prnx/{0}"
    Private Const _previousPath As String = "~/Previous/{0}"
    Private Const _reportCachePath As String = "~/ReportCache/{0}"

    Private WithEvents _xRpt As New XtraReport
    Private _reportConditions As String = ""
    Private _init As Boolean = False

    Private Shared ReadOnly _currContxt As HttpContext = HttpContext.Current
    Private Shared ReadOnly classLevel As String = "報表服務拋出訊息:"

    Private Shared Function GetPath(ByVal p As String) As String
        Return _currContxt.Server.MapPath(p)
    End Function

    Protected Overrides Function CreateReportByName(reportName As String) As XtraReport        
        Try
            If _init Then
                Return _xRpt ' New XtraReport
            Else
                Return ReportProcessing(reportName, _xRpt)
                '_reportConditions = reportName
                'Return _xRpt
            End If
        Catch ex As Exception
            Throw
        End Try
        'Return MyBase.CreateReportByName(reportName)
        'Dim reportName As String =
        '    "C:\Gird\NewSO\Hammer\Silverlight\CableSoft.SL.Printing\Test_CSPrinting.Web\Report\MultiWork.repx"
        'Return MyBase.CreateReportByName(reportName)
        'CreateReportByName = MyBase.CreateReportByName(reportName)
        'Try
        '    'rpt.PrintingSystem.LoadDocument(rptCache)
        '    ''rpt.PrintingSystem.PageSettings.PaperKind = Paperind.A3
        '    ''rpt.PrintingSystem.PageSettings.Landscape = True
        '    ''rpt.PrintingSystem.PageSettings.PrinterName = ""
        '    'Dim rptCache As String =
        '    '    "C:\Gird\NewSO\Hammer\Silverlight\CableSoft.SL.Printing\CableSoft.SL.Printing.Web\Previous\SO029.prnx"
        '    'Dim rpt As New XtraReport
        '    'Return rpt
        '    'Dim msgHdr As MessageHeaders = OperationContext.Current.IncomingMessageHeaders
        '    'Dim hdr As String = msgHdr.GetHeader(Of String)("Inspector", String.Empty)
        '    'Trace.WriteLine(hdr)
        '    'Dim hdr2 As String = msgHdr.GetHeader(Of String)("Outgoing", String.Empty)
        '    'Trace.WriteLine(hdr2)
        '    Dim rpt As XtraReport = ReportProcessing(reportConditions)
        '    'rpt.PrintingSystem.SaveDocument("C:\Gird\NewSO\Hammer\Silverlight\CableSoft.SL.Printing\CableSoft.SL.Printing.Web\Previous\SO029.prnx")
        '    Return rpt
        'Catch ex As Exception
        '    Throw
        'End Try
    End Function

    Private Shared Function GetLoginInfo(dsConditions As DataSet) As LoginInfo
        GetLoginInfo = Nothing
        Dim dtLi As DataTable = Nothing
        Try
            dtLi = dsConditions.Tables("LoginInfo")
            GetLoginInfo = New LoginInfo()
            'GetLoginInfo = New CableSoft.BLL.Utility.LoginInfo
            Dim tp As Type = GetLoginInfo.GetType
            Dim dr As DataRow = dtLi.Rows(0)
            For Each col As DataColumn In dtLi.Columns
                Dim pi As PropertyInfo = tp.GetProperty(col.ColumnName)
                If pi IsNot Nothing Then
                    If col.ColumnName.Equals("Guid") OrElse pi.PropertyType.Name.Equals("Guid") Then
                        pi.SetValue(GetLoginInfo, New Guid(dr(col.ColumnName).ToString), Nothing)
                    Else
                        If Not dr.IsNull(col.ColumnName) Then
                            Dim val As Object = Convert.ChangeType(dr(col.ColumnName), pi.PropertyType)
                            'pi.SetValue(GetLoginInfo, dr(col.ColumnName), Nothing)
                            pi.SetValue(GetLoginInfo, val, Nothing)
                        Else
                            'Dim colName As String = col.ColumnName
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            Throw
        Finally
            If dtLi IsNot Nothing Then dtLi.Dispose()
        End Try
    End Function

    Public Shared Function EscSeq(ByVal value As String) As String
        EscSeq = value
        Try
            If Not String.IsNullOrEmpty(value) Then
                Return Regex.Replace(value, "(?<!\\)\\(?!\\)", "\\", RegexOptions.IgnorePatternWhitespace)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function Instead(ByVal value As String,
                                   find As String,
                                   replacement As String) As String
        Instead = value
        Try
            If Not String.IsNullOrEmpty(value) Then
                Return Regex.Replace(value, find, replacement, RegexOptions.IgnoreCase)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Overrides Function GetReportParameters(instanceIdentity As DevExpress.DocumentServices.ServiceModel.DataContracts.InstanceIdentity) As DevExpress.DocumentServices.ServiceModel.DataContracts.ReportParameterContainer
        _init = True
        Return MyBase.GetReportParameters(instanceIdentity)
    End Function

    Private Shared Function ReportProcessing(reportConditions As String, ByRef rpt As XtraReport) As XtraReport
        'Private Function ReportProcessing(reportConditions As String, ByRef rpt As XtraReport) As XtraReport
        ReportProcessing = Nothing
        Dim bll As Object = Nothing
        Dim resultSet As DataSet = Nothing
        Dim dsConditions As DataSet = Nothing
        Dim dt As DataTable = Nothing
        Dim li As LoginInfo = Nothing
        Dim sql As String = String.Empty
        Try
            If String.IsNullOrEmpty(reportConditions) Then Return Nothing
            If _currContxt.Application("Trace") Then
                'Global_asax.Log2Txt(_currContxt.Application("LogPath"), reportConditions)
            End If
            'reportConditions = EscSeq(reportConditions)
            'If _currContxt.Application("Trace") Then
            '    Global_asax.Log2Txt(_currContxt.Application("LogPath"), reportConditions)
            'End If
            Try
                'Using dsConditions As DataSet = Connector.FromXml(reportConditions)
                dsConditions = CableSoft.BLL.Utility.JsonServer.FromJson(reportConditions)
            Catch jsonEx As Exception
                If _currContxt.Application("Trace") Then
                    'Global_asax.Log2Txt(_currContxt.Application("LogPath"), jsonEx.ToString)
                End If
                Throw New Exception(jsonEx.Message)
                Return Nothing
            End Try
            If dsConditions Is Nothing Then
                'Throw New Exception(classLevel.Add("<資料集>無法反序列化傳入之<報表相關參數資訊>!"))
                Return Nothing
            End If
            dsConditions.DataSetName = "ReportConditions"
            If dsConditions.Tables.Count <= 0 Then
                'Throw New Exception(classLevel.Add("報表相關資訊<資料表為空>!"))
                Return Nothing
            End If
            If dsConditions.Tables.IndexOf("LoginInfo") = -1 Then
                'Throw New Exception(classLevel.Add("報表相關資訊中<無系統登入資訊資料表>!"))
                Return Nothing
            End If
            li = GetLoginInfo(dsConditions)
            If li.CompCode = 0 Then
                'Throw New Exception(classLevel.Add("報表相關資訊中<無公司不合法>!"))
                Return Nothing
            End If
            Dim comp As String = li.CompCode.ToString
            If _currContxt.Application("Trace") Then
                'Global_asax.Log2Txt(_currContxt.Application("LogPath"), String.Format("RptSvc公司別:{0}", comp))
            End If
            If String.IsNullOrEmpty(li.ConnectionString) Then
                If _currContxt.Application("ConnInfo") IsNot Nothing AndAlso
                    TypeOf _currContxt.Application("ConnInfo") Is Dictionary(Of String, String) Then
                    Dim conDic As Dictionary(Of String, String) = _currContxt.Application("ConnInfo")
                    If conDic.ContainsKey(comp) Then
                        If Not String.IsNullOrEmpty(conDic(comp)) Then
                            li.ConnectionString = conDic(comp)
                            If _currContxt.Application("Trace") Then
                                'Global_asax.Log2Txt(_currContxt.Application("LogPath"), li.ConnectionString)
                                If String.IsNullOrEmpty(li.ConnectionString) Then
                                    'Global_asax.Log2Txt(_currContxt.Application("LogPath"),
                                    '                    "RptSvc_連線字串NG!")
                                Else
                                    'Global_asax.Log2Txt(_currContxt.Application("LogPath"),
                                    '                    "RptSvc_連線字串OK!")
                                End If
                            End If
                        End If
                    Else
                        If _currContxt.Application("Trace") Then
                            'Global_asax.Log2Txt(_currContxt.Application("LogPath"),
                            '                    String.Format("RptSvc_ConDic不含公司別{0}!", comp))
                        End If
                    End If
                Else
                    If _currContxt.Application("Trace") Then
                        'Global_asax.Log2Txt(_currContxt.Application("LogPath"), "RptSvc_空連線字串_空AppConInfo!")
                        'Global_asax.Log2Txt(_currContxt.Application("LogPath"),
                        '                    String.Concat("本來就有:", ControlChars.CrLf, li.ConnectionString))
                    End If
                    Dim conDic As Dictionary(Of String, String) = Config.GetConnStrDict()
                    If conDic.ContainsKey(comp) Then
                        If Not String.IsNullOrEmpty(conDic(comp)) Then
                            li.ConnectionString = conDic(comp)
                        End If
                        If String.IsNullOrEmpty(li.ConnectionString) Then
                            'Global_asax.Log2Txt(_currContxt.Application("LogPath"),
                            '                    "RptSvc_Config連線字串NG!")
                        Else
                            'Global_asax.Log2Txt(_currContxt.Application("LogPath"),
                            '                    "RptSvc_Config連線字串OK!")
                        End If
                    Else
                        If _currContxt.Application("Trace") Then
                            'Global_asax.Log2Txt(_currContxt.Application("LogPath"),
                            '                    String.Format("RptSvc_Config不含公司別{0}!", comp))
                        End If
                    End If
                End If
            End If
            If String.IsNullOrEmpty(li.ConnectionString) Then
                'Throw New Exception(classLevel.Add("無法取得公司別<{0}>的資料庫連線字串!".Fmt(comp)))
                Return Nothing
            End If
            'li.ConnectionString = "Server=rdknet;User Id=tbcsh;Password=tbcsh;Persist Security Info=True;Unicode=True" &
            '                      ";Pooling=True;Min Pool Size=2;Max Pool Size=100;Connection Lifetime=60"
            If dsConditions.Tables.IndexOf("Parameters") = -1 Then
                'Throw New Exception(classLevel.Add("報表相關資訊中<無參數資資料表訊>!"))
                Return Nothing
            End If
            If dsConditions.Tables("Parameters").Rows.Count <= 0 Then
                'Throw New Exception(classLevel.Add("報表<參數資訊資料表為空>!"))
                Return Nothing
            End If
            Dim dtr As DataRow = dsConditions.Tables("Parameters").Rows(0)
            If dtr Is Nothing Then
                'Throw New Exception(classLevel.Add("無法取得報表<參數資訊資料列>!"))
                Return Nothing
            End If
            'Throw New Exception(dsConditions.Tables("Parameters").Columns.Contains("PreviousReport"))
            'Throw New Exception(dtr("PreviousReport").ToString)

            If _currContxt.Application("Trace") Then
                ' Global_asax.Log2Txt(_currContxt.Application("LogPath"), "Parameters Row 0 OK!")
                If dtr("PreviousReport") Is Nothing Then
                    'Global_asax.Log2Txt(_currContxt.Application("LogPath"), "PreviousReport Flag Is Nothing")
                End If
            End If

            If dtr("PreviousReport") Then
                Dim rpts() As String = dtr("ReportFile").ToString.Split(";")
                If _currContxt.Application("Trace") Then
                    ' Global_asax.Log2Txt(_currContxt.Application("LogPath"), "Split Report File OK!")
                End If
                If rpts.Length = 0 Then
                    '  Throw New Exception(classLevel.Add("<參數資訊資料列>無<報表名稱>!"))
                    Return Nothing
                End If
                If _currContxt.Application("Trace") Then
                    '      Global_asax.Log2Txt(_currContxt.Application("LogPath"), "Report File Name OK!")
                End If
                Dim rptFileName As String = rpts(0)
                If Not rptFileName.EndsWith(".repx", StringComparison.CurrentCultureIgnoreCase) Then
                    rptFileName &= ".repx"
                End If
                If _currContxt.Application("Trace") Then
                    '   Global_asax.Log2Txt(_currContxt.Application("LogPath"), rptFileName)
                    'Global_asax.Log2Txt(_currContxt.Application("LogPath"), li.CompCode)
                    'Global_asax.Log2Txt(_currContxt.Application("LogPath"), li.EntryId)
                End If
                ReadPreviousResult(li.CompCode, li.EntryId, rptFileName, dt, rpt)
                If _currContxt.Application("Trace") Then
                    '   Global_asax.Log2Txt(_currContxt.Application("LogPath"), "ReadPreviousResult OK!")
                End If
                Return rpt
                Exit Function
            End If

            If _currContxt.Application("Trace") Then
                'Global_asax.Log2Txt(_currContxt.Application("LogPath"), "PreviousReport Flag OK!")
            End If

            If dtr.IsNull("DllName") Then
                ' Throw New Exception(classLevel.Add("報表參數資訊<資料列組件名稱為空>!"))
                Return Nothing
            End If
            If dtr.IsNull("ClassName") Then
                ' Throw New Exception(classLevel.Add("報表參數資訊<資料列類別名稱為空>!"))
                Return Nothing
            End If
            'If li.HttpPath Is Nothing OrElse String.IsNullOrEmpty(li.HttpPath) Then
            '    Throw New Exception(classLevel.Add("報表參數資訊<HttpPath為空>!"))
            '    Return Nothing
            'End If
            If _currContxt.Application("Trace") Then
                'Global_asax.Log2Txt(_currContxt.Application("LogPath"), dtr("DllName").ToString)
                'Global_asax.Log2Txt(_currContxt.Application("LogPath"), dtr("ClassName").ToString)
                'Global_asax.Log2Txt(_currContxt.Application("LogPath"), li.HttpPath)
            End If
            If dtr("DllName").ToString.Equals("CableSoft.BLL.Dynamic.Report") Then
                li.HttpPath = ""
            End If
            li.HttpPath = "C:\NewSO\Bin\Server\"
            bll = DynaLoadAsm(dtr("DllName").ToString,
                              dtr("ClassName").ToString,
                              li.HttpPath,
                              New Object() {li})
            'CableSoft.SL.Utility.Utility.ConvertTo(_LoginInfo, New CableSoft.SO.RIA.Wip.Maintain.Web.LoginInfo)

            If bll Is Nothing Then
                'Throw New Exception(classLevel.Add("無法建立報表對應之<商業邏輯組件>!"))
                Return Nothing
            End If
            resultSet = bll.GetReportParams(dsConditions)

            If resultSet Is Nothing Then
                'Throw New Exception(classLevel.Add("<商業邏輯組件>回傳<報表處理結果為空>!"))
                Return Nothing
            End If
            If resultSet.Tables.Count <= 0 Then
                'Throw New Exception(classLevel.Add("報表處理結果<資料集中無資料表>!"))
                Return Nothing
            End If
            If resultSet.Tables.IndexOf("Parameters") = -1 Then
                'Throw New Exception(classLevel.Add("報表處理結果資料表中<無參數資訊資料表>!"))
                Return Nothing
            End If
            Using dtParams As DataTable = resultSet.Tables("Parameters")
                If dtParams Is Nothing Then
                    'Throw New Exception(classLevel.Add("無法取得報表處理結果<參數資訊資料表>!"))
                    Return Nothing
                End If
                If dtParams.Rows.Count <= 0 Then
                    'Throw New Exception(classLevel.Add("報表處理結果<參數資訊資料表為空>!"))
                    Return Nothing
                End If
                Dim drParams As DataRow = dtParams.Rows(0)
                If drParams Is Nothing Then
                    'Throw New Exception(classLevel.Add("無法取得報表處理結果<參數資訊資料列>!"))
                    Return Nothing
                End If
                If drParams.IsNull("ReportFile") Then
                    'Throw New Exception(classLevel.Add("報表處理結果<參數資訊資料列中報表名稱為空>!"))
                    Return Nothing
                End If
                Dim rptFiles() As String = drParams("ReportFile").ToString.Split(";")
                If rptFiles.Length = 0 Then
                    'Throw New Exception(classLevel.Add("報表處理結果<參數資訊資料列中無報表檔案名稱>!"))
                    Return Nothing
                End If
                Dim rptFileName As String = rptFiles(0)
                If Not rptFileName.EndsWith(".repx", StringComparison.CurrentCultureIgnoreCase) Then
                    rptFileName &= ".repx"
                End If
                'Dim rpt As XtraReport = LoadReport(rptFileName)
                'rpt = LoadReport(rptFileName, rpt)
                'rpt.PrintingSystem.PageSettings.PaperKind = PaperKind.A3
                'rpt.PrintingSystem.PageSettings.Landscape = True
                'rpt.PrintingSystem.PageSettings.PrinterName = "EPSON LQ-675KT ESC/P 2"
                'CachingReport(rpt, rptFileName)
                If resultSet.Tables.Count <= 0 Then
                    'Throw New Exception(classLevel.Add("報表商業邏輯無回傳資料!"))
                    Return Nothing
                End If
                If resultSet.Tables.IndexOf("Rpt") = -1 Then
                    'Throw New Exception(classLevel.Add("報表商業邏輯無回傳主報表資料!"))
                    Return Nothing
                End If
                dt = resultSet.Tables("Rpt")
                If dt.Rows.Count = 0 Then
                    'SetReturnInfo(rpt, "查無資料!")
                    'Throw New Exception(classLevel.Add("@@查無資料!"))
                    Return New XtraReport() 'Nothing
                End If
                LoadReport(rptFileName, rpt)
                If rpt Is Nothing Then
                    'Throw New Exception(String.Format(classLevel.Add("報表[{0}]載入失敗!"), rptFileName))
                    Return Nothing
                End If
                SetDataSource(rpt, dt)
                'rpt.CreateDocument(True)
                'rpt.Parameters.Add(New Parameter() With {.Name = "TESTx", .Value = "TEST33"})
                If drParams.IsNull("ReportTitle") Then
                    'Throw New Exception(classLevel.Add("報表處理結果<參數資訊資料列中無報表抬頭>!"))
                    Return Nothing
                End If
                Dim rptTitle As String = drParams("ReportTitle").ToString
                'Dim rptName As String = rptFileName.Replace(".repx", "")
                Dim rptName As String = Instead(rptFileName, ".repx", "")
                If drParams.IsNull("CompanyName") Then
                    'Throw New Exception(classLevel.Add("報表處理結果<參數資訊資料列中無公司別名稱>!"))
                    Return Nothing
                End If
                Dim rptCompany As String = drParams("CompanyName").ToString
                Dim rptUserName As String = li.EntryName
                Dim rptCondition As String = String.Empty
                Dim saveResult As Boolean = drParams("SaveResult")
                SetReportInfo(rpt, "ReportTitle", rptTitle)
                SetReportInfo(rpt, "ReportName", rptName)
                SetReportInfo(rpt, "Company", rptCompany)
                SetReportInfo(rpt, "UserName", rptUserName)
                Using dtCondis As DataTable = resultSet.Tables("Conditions")
                    If dtCondis IsNot Nothing AndAlso dtCondis.Rows.Count > 0 Then
                        'FieldName , FieldDesc , Field1Value , Field2Value , DisplayText
                        Dim strBdr As New StringBuilder()
                        Dim dispTxt As String
                        For Each dr As DataRow In dtCondis.Rows
                            If Not dr.IsNull("DisplayText") Then
                                dispTxt = dr("DisplayText").ToString
                                If dispTxt <> String.Empty Then strBdr.AppendLine(dispTxt)
                            End If
                        Next
                        rptCondition = strBdr.ToString
                        'AddReportCondition(rpt, strBdr.ToString)
                    Else
                        If Not drParams.IsNull("ReportCondition") Then
                            'Dim rptCdt As String = drParams("ReportCondition")
                            rptCondition = drParams("ReportCondition")
                            'If Not String.IsNullOrEmpty(rptCdt) Then AddReportCondition(rpt, rptCdt)
                        End If
                    End If
                    SetReportInfo(rpt, "ReportCondition", rptCondition)
                End Using
                'SetReturnInfo(rpt, "Return Information")
                Dim xrSubRpts As Dictionary(Of String, XRSubreport) = GetSubReport(rpt)
                If xrSubRpts.Count > 0 Then
                    Dim subRptName As String
                    For lp = 1 To xrSubRpts.Count
                        subRptName = String.Format("SubRpt{0}", lp)
                        If xrSubRpts.ContainsKey(subRptName) Then
                            Dim subRptFileName As String = String.Empty
                            Try
                                subRptFileName = rptFiles(lp)
                                If Not subRptFileName.EndsWith(".repx", StringComparison.CurrentCultureIgnoreCase) Then
                                    subRptFileName &= ".repx"
                                End If
                            Catch ex As Exception
                                Throw
                                Return Nothing
                            End Try
                            Dim subRpt As XtraReport = LoadReport(subRptFileName)
                            If subRpt Is Nothing Then
                                'Throw New Exception(String.Format(classLevel.Add("子報表[{0}]載入失敗!"), subRptFileName))
                                Return Nothing
                            End If
                            'CachingReport(subRpt, subRptFileName)
                            If resultSet.Tables.IndexOf(subRptName) = -1 Then
                                'Throw New Exception(classLevel.Add("報表商業邏輯無回傳子報表資料!"))
                                'Return Nothing
                                SetDataSource(subRpt, dt)
                            Else
                                Dim dtSubRpt As DataTable = resultSet.Tables(subRptName)
                                SetDataSource(subRpt, dtSubRpt)
                            End If
                            'subRpt.CreateDocument(True)
                            xrSubRpts(subRptName).ReportSource = subRpt
                        End If
                    Next
                End If
                'Dim xx = rpt.FindControl("SubRpt1", True)
                'System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString)
                'If saveResult Then SavePrevious(rpt, rptFileName, rptTitle, li.EntryId)

                If saveResult Then
                    SaveReportResult(New DataTable() {dt},
                                     li.CompCode, li.EntryId,
                                     rptName, rptTitle, rptCompany, rptUserName, rptCondition)
                    'Dim sw As New Stopwatch
                    'sw.Start()
                    'SavePrevious(rpt, rptName, rptTitle, li.CompCode.ToString, li.EntryId)
                    'SaveResultToPDF(rpt, rptName, rptTitle, li.CompCode.ToString, li.EntryId)
                    'sw.Stop()
                    'Throw New Exception(String.Format("({0}秒)", sw.Elapsed.Seconds.ToString))
                End If
                'Dim nfo As New NativeFormatOptions
                'nfo.ShowOptionsBeforeSave = True
                Return rpt
            End Using
        Catch ex As Exception
            If bll IsNot Nothing AndAlso bll.GetType.GetProperty("SQL") IsNot Nothing Then
                sql = bll.SQL
            End If
            'Dim errMsg As String = String.Concat(ex.Message, Environment.NewLine, "'", sql, "'")
            'BuildMessage(New Exception(errMsg, ex), li)
            'BuildMessage(New Exception(sql), li)
            BuildMessage(ex, li)
            'Throw New ReportException(ex.Message, ex.GetBaseException) With {.SQL = sql}
            Throw New Exception(String.Format("{0}{1}{2}", ex.Message, Convert.ToChar(255), sql))
        Finally
            If dt IsNot Nothing Then dt.Dispose()
            If dsConditions IsNot Nothing Then dsConditions.Dispose()
            If resultSet IsNot Nothing Then resultSet.Dispose()
            If bll IsNot Nothing Then bll.Dispose()
        End Try
    End Function

    Private Shared Sub SetReportInfo(rpt As XtraReport, ctlName As String, ctlValue As String)
        Try
            Dim xrc As XRControl = rpt.FindControl(ctlName, True)
            If xrc IsNot Nothing Then xrc.Text = ctlValue
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Shared Function LoadReport(rptFileName As String,
                                       Optional ByRef report As XtraReport = Nothing) As XtraReport
        LoadReport = Nothing
        Try
            If report Is Nothing Then
                LoadReport = New XtraReport
            Else
                LoadReport = report
            End If
            Dim rptFile As String = GetPath(String.Format(_reportPath, rptFileName))
            If File.Exists(rptFile) Then
                LoadReport.LoadLayout(rptFile)
            Else
                'Throw New Exception(classLevel.Add("報表(格式檔)檔案不存在!{0}[{1}]".Fmt(Environment.NewLine, rptFile)))
                'Throw New Exception("報表(格式檔)檔案不存在!{0}[{1}]".Fmt(Environment.NewLine, rptFile))
            End If
            'Dim rptCacheFile As String = GetPath(String.Format(_reportCachePath, rptFileName))
            'rptCacheFile = rptCacheFile.Replace(".repx", ".prnx")
            'If File.Exists(rptCacheFile) Then
            '    LoadReport.PrintingSystem.LoadDocument(rptCacheFile)
            'Else
            '    LoadReport.LoadLayout(GetPath(String.Format(_reportPath, rptFileName)))
            'End If
            'rpt.LoadLayoutFromXml(rptFileName)
            'rpt = ReportFormFile(rptFileName)
            'rpt.ReportSourceUrl = rptFileName
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Shared Sub SetDataSource(xRpt As XtraReport, data As DataTable)
        Try
            xRpt.DataSource = data
            xRpt.DataMember = "Table"
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Private Shared Sub AddReportCondition(xRpt As XtraReport, rptCondition As String)
    '    Try
    '        If xRpt.Bands(BandKind.ReportHeader) Is Nothing Then xRpt.Bands.Add(New ReportHeaderBand)
    '        Dim fontName As String = "新細明體"
    '        Dim fontSize As Single = 9
    '        Dim rptTitle As XRControl = xRpt.FindControl("ReportTitle", True)
    '        If rptTitle IsNot Nothing Then fontName = rptTitle.Font.Name
    '        Dim dtlBand As Band = xRpt.Bands(BandKind.Detail)
    '        If dtlBand IsNot Nothing Then fontSize = dtlBand.Controls(0).Font.Size
    '        Dim lblCondition As New XRLabel() With
    '            {
    '                .Name = "rptCondition",
    '                .Text = rptCondition,
    '                .WordWrap = True,
    '                .AutoWidth = False,
    '                .CanShrink = False,
    '                .Multiline = True,
    '                .Font = New Font(fontName, fontSize),
    '                .Width = xRpt.PageWidth - (xRpt.Margins.Left + xRpt.Margins.Right)
    '            }
    '        xRpt.Bands(BandKind.ReportHeader).Controls.Add(lblCondition)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    Private Shared Sub SetReturnInfo(xRpt As XtraReport, returnInfo As String)
        Try
            Dim rtnInfo As New XRLabel() With
                {
                    .Name = "returnInfo",
                    .Visible = False,
                    .Text = returnInfo
                }
            xRpt.Bands(BandKind.ReportHeader).Controls.Add(rtnInfo)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Shared Sub SaveReportResult(dataAry() As DataTable,
                                        compCode As String,
                                        userID As String,
                                        rptName As String,
                                        rptTitle As String,
                                        rptCompany As String,
                                        rptUserName As String,
                                        rptCondition As String)
        Try

            'Dim prevRepxInfo As New PreviousReportInformation With
            '    {
            '        .ReportTitle = rptTitle,
            '        .ReportName = rptName,
            '        .Company = rptCompany,
            '        .UserName = rptUserName,
            '        .ReportCondition = rptCondition
            '    }

            Dim fileName As String =
                String.Format(_previousPath,
                              String.Concat(Instead(rptName, ".repx", ""), "-", compCode, "-", userID))

            Dim xmlFile As String = String.Concat(fileName, ".xml")
            dataAry(0).WriteXml(GetPath(xmlFile), XmlWriteMode.WriteSchema)

            Dim dicResult As New Dictionary(Of String, String)() From
                {
                    {"ReportTitle", rptTitle},
                    {"ReportName", rptName},
                    {"Company", rptCompany},
                    {"UserName", rptUserName},
                    {"ReportCondition", rptCondition}
                }

            Dim javaSer As New JavaScriptSerializer() With {.MaxJsonLength = Int32.MaxValue}
            Dim jsonResult As String = javaSer.Serialize(dicResult)

            Dim jsonFile As String = String.Concat(fileName, ".json")

            Using sw As New StreamWriter(GetPath(jsonFile), False)
                sw.WriteLine(jsonResult)
                sw.Close()
            End Using

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Shared Sub ReadPreviousResult(compCode As String,
                                          userID As String,
                                          rptName As String,
                                          ByRef dt As DataTable,
                                          ByRef rpt As XtraReport)
        Try
            Dim fileName As String =
                String.Format(_previousPath,
                              String.Concat(Instead(rptName, ".repx", ""), "-", compCode, "-", userID))
            Dim xmlFile As String = String.Concat(fileName, ".xml")
            If _currContxt.Application("Trace") Then
                'Global_asax.Log2Txt(_currContxt.Application("LogPath"), fileName)
                'Global_asax.Log2Txt(_currContxt.Application("LogPath"), xmlFile)
                'Global_asax.Log2Txt(_currContxt.Application("LogPath"), GetPath(xmlFile))
            End If
            If File.Exists(GetPath(xmlFile)) Then
                LoadReport(rptName, rpt)
                If rpt Is Nothing Then
                    'Throw New Exception(String.Format(classLevel.Add("報表[{0}]載入失敗!"), rptName))
                    rpt = Nothing
                    Exit Sub
                End If
                If _currContxt.Application("Trace") Then
                    'Global_asax.Log2Txt(_currContxt.Application("LogPath"), "報表載入成功!")
                End If
                If dt Is Nothing Then dt = New DataTable
                dt.ReadXml(GetPath(xmlFile))
                SetDataSource(rpt, dt)
                If _currContxt.Application("Trace") Then
                    ' Global_asax.Log2Txt(_currContxt.Application("LogPath"), "報表資料來源設定完成!")
                End If
                Dim jsonFile As String = String.Concat(fileName, ".json")
                If File.Exists(GetPath(jsonFile)) Then
                    Dim json As String = ""
                    Using sr As New StreamReader(GetPath(jsonFile))
                        json = sr.ReadToEnd
                        sr.Close()
                    End Using
                    If _currContxt.Application("Trace") Then
                        'Global_asax.Log2Txt(_currContxt.Application("LogPath"), "報表條件載入成功!")
                        'Global_asax.Log2Txt(_currContxt.Application("LogPath"), json)
                    End If
                    If Not String.IsNullOrEmpty(json) Then
                        Dim dicResult As New Dictionary(Of String, String)()
                        Dim javaSer As New JavaScriptSerializer() With {.MaxJsonLength = Int32.MaxValue}
                        If _currContxt.Application("Trace") Then
                            'Global_asax.Log2Txt(_currContxt.Application("LogPath"), "準備反序列化json")
                        End If
                        dicResult = javaSer.Deserialize(json, dicResult.GetType)
                        If _currContxt.Application("Trace") Then
                            ' Global_asax.Log2Txt(_currContxt.Application("LogPath"), "反序列化json成功!")
                        End If
                        SetReportInfo(rpt, "ReportTitle", dicResult("ReportTitle"))
                        SetReportInfo(rpt, "ReportName", dicResult("ReportName"))
                        SetReportInfo(rpt, "Company", dicResult("Company"))
                        SetReportInfo(rpt, "UserName", dicResult("UserName"))
                        SetReportInfo(rpt, "ReportCondition", dicResult("ReportCondition"))
                        If _currContxt.Application("Trace") Then
                            'Global_asax.Log2Txt(_currContxt.Application("LogPath"), "Set Report Info OK!")
                        End If
                    End If
                End If
            Else
                rpt = New XtraReport()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Shared Sub CachingReport(xRpt As XtraReport, rptFileName As String)
        Try
            Dim rptCacheFile As String = GetPath(String.Format(_reportCachePath, rptFileName))
            'rptCacheFile = rptCacheFile.Replace(".repx", ".prnx")
            rptCacheFile = Instead(rptCacheFile, ".repx", ".prnx")
            xRpt.PrintingSystem.SaveDocument(rptCacheFile)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Shared Sub SavePrevious(xRpt As XtraReport,
                                    rptFile As String,
                                    rptTitle As String,
                                    compCode As String,
                                    userID As String)
        Try
            Dim prnXfile As String =
                String.Format(_previousPath,
                              String.Concat(Instead(rptFile, ".repx", ""), "-", compCode, "-", userID, ".prnx"))
            'xRpt.PrintingSystem.Document.CanChangePageSettings = True
            'Dim opts As New NativeFormatOptions() With {.Compressed = True, .ShowOptionsBeforeSave = True}
            xRpt.CreateDocument()
            'xRpt.PrintingSystem.SaveDocument(GetPath(prnXfile), opts)
            xRpt.PrintingSystem.SaveDocument(GetPath(prnXfile))
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Shared Sub SaveResultToPRNX(xRpt As XtraReport,
                                        rptFile As String,
                                        rptTitle As String,
                                        userID As String)
        Try
            'Dim prnxFile As String =
            '    String.Format(_exportPath,
            '                  String.Concat(rptFile.Replace(".repx", ""), "-", userID, ".prnx"))
            Dim prnxFile As String =
                String.Format(_exportPath,
                              String.Concat(Instead(rptFile, ".repx", ""), "-", userID, ".prnx"))
            'Dim opts As New NativeFormatOptions() With {.Compressed = True, .ShowOptionsBeforeSave = True}
            'xRpt.PrintingSystem.SaveDocument(prnxFile, opts)
            xRpt.PrintingSystem.SaveDocument(prnxFile)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Shared Sub SaveResultToPDF(xRpt As XtraReport,
                                       rptFile As String,
                                       rptTitle As String,
                                       compCode As String,
                                       userID As String)
        Try
            Dim expOpt As New PdfExportOptions() With
                {
                    .Compressed = True,
                    .ConvertImagesToJpeg = True,
                    .ImageQuality = PdfJpegImageQuality.High
                }
            expOpt.DocumentOptions.Application = "CC&B"
            expOpt.DocumentOptions.Author = "CableSoft"
            expOpt.DocumentOptions.Keywords = rptTitle
            expOpt.DocumentOptions.Subject = rptTitle
            expOpt.DocumentOptions.Title = rptTitle
            'Dim pdfFile As String =
            '    String.Format(_exportPath,
            '                  String.Concat(rptFile.Replace(".repx", ""), "-", userID, ".pdf"))
            'Dim pdfFile As String =
            '    String.Format(_exportPath,
            '                  String.Concat(Instead(rptFile, ".repx", ""), "-", userID, ".pdf"))
            Dim pdfFile As String =
                String.Format(_exportPath,
                              String.Concat(Instead(rptFile, ".repx", ""), "-", compCode, "-", userID, ".pdf"))
            'Throw New Exception(GetPath(pdfFile))
            xRpt.ExportToPdf(GetPath(pdfFile), expOpt)
            '    'rpt.ExportOptions.SaveToStream
            '    'rpt.ExportToCsv()
            '    'rpt.ExportToHtml()
            '    'rpt.ExportToImage()
            '    'rpt.ExportToMht()
            '    'rpt.ExportToPdf()
            '    'rpt.ExportToRtf()
            '    'rpt.ExportToText()
            '    'rpt.ExportToXls()
            '    'rpt.ExportToXlsx()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Shared Function ReportFormFile(rptFileName As String) As XtraReport
        ReportFormFile = Nothing
        Try
            Return XtraReport.FromStream(New MemoryStream(File.ReadAllBytes(rptFileName)), True)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Shared Function GetSubReport(rpt As XtraReport) As Dictionary(Of String, XRSubreport)
        Dim subReports As New Dictionary(Of String, XRSubreport)
        Try
            For Each b As Band In rpt.Bands
                'Dim subRpts = b.Controls.OfType(Of XRControl).Where(Function(x) TypeOf x Is XRSubreport)
                Dim subRpts = b.Controls.OfType(Of XRSubreport)()
                'Dim dic = subRpts.ToDictionary(Function(x) x)
                If subRpts IsNot Nothing AndAlso subRpts.Count > 0 Then
                    For Each subRpt As XRSubreport In subRpts
                        subReports(subRpt.Name) = subRpt
                    Next
                End If
            Next
            Return subReports
        Catch ex As Exception
            Throw
        End Try
    End Function

    '    Using fs As New FileStream(Dst, FileMode.Open, FileAccess.Read)
    '        Dim PDFdata As Byte() = New Byte(fs.Length - 1) {}
    '        Dim br As New BinaryReader(fs)
    '        br.Read(PDFdata, 0, CInt(fs.Length))
    '        br.Close()
    '        Context.Response.Clear()
    '        Context.Response.ClearHeaders()
    '        Context.Response.ContentType = "application/pdf"
    '        Context.Response.AppendHeader("Content-Length", PDFdata.Length.ToString())
    '        Context.Response.BinaryWrite(PDFdata)
    '        Context.Response.Flush()
    '        Context.Response.Close()
    '        Context.Response.[End]()
    '    End Using

    Private Shared Function DynaLoadAsm(ByVal asmName As String,
                                        ByVal className As String,
                                        ByVal httpPath As String,
                                        ByRef arguments() As Object) As Object
        DynaLoadAsm = Nothing
        Try
            Dim asm As Assembly = Nothing
            If httpPath = String.Empty Then httpPath = AppDomain.CurrentDomain.BaseDirectory
            className = String.Format("{0}.{1}", asmName, className)
            asmName = String.Format("{0}.dll", asmName)
            Dim asmFile As String = GetAsmFile(asmName, httpPath)
            If Not File.Exists(asmFile) Then asmFile = GetAsmFile(asmName, AppDomain.CurrentDomain.BaseDirectory)

            If File.Exists(asmFile) Then
                'asm = Assembly.LoadFile(asmFile)
                'Return asm.CreateInstance(className,
                '                          True,
                '                          BindingFlags.CreateInstance,
                '                          Nothing,
                '                          arguments,
                '                          Nothing,
                '                          Nothing)
                Return New CableSoft.SO.BLL.Facility.VOD.Calculate.Calculate(arguments(0))
            Else
                'Throw New Exception(classLevel.Add("[商業邏輯]組件檔案不存在!{0}[{1}]".Fmt(Environment.NewLine, asmFile)))
                Return Nothing
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Shared Function GetAsmFile(asmName As String, httpPath As String) As String
        Try
            Dim asmFullpath As String = Path.Combine(httpPath, asmName)
            If Not File.Exists(asmFullpath) Then asmFullpath = Path.Combine(httpPath, "Bin", asmName)
            If Not File.Exists(asmFullpath) Then asmFullpath = Path.Combine(httpPath, "Bin\Debug", asmName)
            If Not File.Exists(asmFullpath) Then asmFullpath = Path.Combine(httpPath, "Bin\Release", asmName)
            If Not File.Exists(asmFullpath) Then asmFullpath = Path.Combine("C:\NewSO\Bin\Server", asmName)
            Return asmFullpath
        Catch ex As Exception
            Throw
        End Try
    End Function

    Protected Overrides Sub FillDataSources(ByVal report As XtraReport, ByVal reportName As String, ByVal isDesignActive As Boolean)
        Try
            'MyBase.FillDataSources(report, reportName, isDesignActive)
            'Dim msgHdr As MessageHeaders = OperationContext.Current.IncomingMessageHeaders
            'Dim reportConditions As String = msgHdr.GetHeader(Of String)("ReportConditions", String.Empty)
            'report = ReportProcessing(reportConditions, report)
            'ReportProcessing(reportConditions, report)
            'ReportProcessing(_reportConditions, report)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Overrides Function LoadReportLayout(reportName As String) As Byte()
        Return Nothing
        'Return MyBase.LoadReportLayout(reportName)
        'Return CType(HttpContext.Current.Session(reportName), Byte())
    End Function

    Protected Overrides Function IsWizardGeneratedReportName(reportName As String) As Boolean
        Return False
        'Return MyBase.IsWizardGeneratedReportName(reportName)
    End Function

    Protected Overrides Sub SaveReportLayout(ByVal reportName As String, ByVal layoutData As Byte())
        Throw New FaultException("This method is not implemented. Implement the SaveReportLayout method on the server-side, in the report service code-behind.")
        'HttpContext.Current.Session(reportName) = layoutData
    End Sub

    'Protected Overrides Sub AssignReportDataSerializer(report As XtraReport)
    '    'MyBase.AssignReportDataSerializer(report)
    'End Sub

    Protected Overrides Function CreateNewWizardGeneratedReportName() As String
        Return MyBase.CreateNewWizardGeneratedReportName()
    End Function

    Public Overrides Function GetDataSources() As IEnumerable(Of DevExpress.Data.XtraReports.DataProviders.DataSourceInfo)
        Return Nothing
        'Return MyBase.GetDataSources()
    End Function

    'Public Overrides Function GetDataSourcesForFieldList(reportId As DevExpress.XtraReports.ServiceModel.DataContracts.ReportSessionId) As DevExpress.XtraReports.ServiceModel.DataContracts.DataSourceInformation()
    '    Return MyBase.GetDataSourcesForFieldList(reportId)
    'End Function

    'Public Overrides Function GetDocumentData(documentId As DevExpress.Xpf.Printing.ServiceModel.DataContracts.DocumentId) As DevExpress.XtraReports.ServiceModel.DataContracts.DocumentData
    '    Return MyBase.GetDocumentData(documentId)
    'End Function

    'Protected Overrides Function TryCustomProcessInstanceIdentity(identity As DevExpress.Xpf.Printing.ServiceModel.DataContracts.InstanceIdentity, ByRef result As XtraReport) As Boolean
    '    Return False
    '    'Return MyBase.TryCustomProcessInstanceIdentity(identity, result)
    'End Function

    'Public Overrides Function GetReportInformation(identity As DevExpress.Xpf.Printing.ServiceModel.DataContracts.InstanceIdentity) As DevExpress.XtraReports.ServiceModel.DataContracts.ReportInformation
    '    Return MyBase.GetReportInformation(identity)
    'End Function

    Protected Overrides Sub RegisterDataSources(report As XtraReport, reportName As String)
        'MyBase.RegisterDataSources(report, reportName)
    End Sub

    'Public Overrides Sub ClearDocument(documentId As DevExpress.Xpf.Printing.ServiceModel.DataContracts.DocumentId)
    '    MyBase.ClearDocument(documentId)
    'End Sub

    Public Overrides Function AddNewReport(model As DevExpress.Data.XtraReports.Wizard.ReportModel) As String
        Return MyBase.AddNewReport(model)
    End Function

    'Public Overrides Sub ClearReport(reportId As DevExpress.XtraReports.ServiceModel.DataContracts.ReportSessionId)
    '    MyBase.ClearReport(reportId)
    'End Sub

    Protected Overrides Sub ClearReport(report As XtraReport, reportName As String)
        MyBase.ClearReport(report, reportName)
    End Sub

    'Protected Overrides Sub FillCustomProperties(reportName As String, control As XRControl, properties As Dictionary(Of String, Object))
    '    MyBase.FillCustomProperties(reportName, control, properties)
    'End Sub

    'Public Overrides Function GetBuildStatus(documentId As DevExpress.Xpf.Printing.ServiceModel.DataContracts.DocumentId) As DevExpress.XtraReports.ServiceModel.DataContracts.BuildStatus
    '    Return MyBase.GetBuildStatus(documentId)
    'End Function

    'Public Overrides Function GetColumns(dataSourceName As String, dataMemberName As DevExpress.Data.XtraReports.DataProviders.TableInfoName) As IEnumerable(Of DevExpress.Data.XtraReports.DataProviders.ColumnInfo)
    '    Return MyBase.GetColumns(dataSourceName, dataMemberName)
    'End Function

    'Public Overrides Function GetDataSourceDisplayNameForFieldList(dataSourceId As DevExpress.XtraReports.ServiceModel.DataContracts.DataSourceId, dataMember As String) As String
    '    Return MyBase.GetDataSourceDisplayNameForFieldList(dataSourceId, dataMember)
    'End Function

    'Public Overrides Function GetExportedDocument(exportId As DevExpress.Xpf.Printing.ServiceModel.DataContracts.ExportId) As Stream
    '    Return MyBase.GetExportedDocument(exportId)
    'End Function

    'Public Overrides Function GetExportStatus(exportId As DevExpress.Xpf.Printing.ServiceModel.DataContracts.ExportId) As DevExpress.Xpf.Printing.ServiceModel.DataContracts.ExportStatus
    '    Return MyBase.GetExportStatus(exportId)
    'End Function

    'Public Overrides Function GetItemPropertiesForFieldList(dataSourceId As DevExpress.XtraReports.ServiceModel.DataContracts.DataSourceId, dataMember As String) As DevExpress.Data.XtraReports.ServiceModel.DataContracts.PropertyDescriptorProxy()
    '    Return MyBase.GetItemPropertiesForFieldList(dataSourceId, dataMember)
    'End Function

    'Public Overrides Function GetListItemPropertiesForFieldList(dataSourceId As DevExpress.XtraReports.ServiceModel.DataContracts.DataSourceId, dataMember As String) As DevExpress.Data.XtraReports.ServiceModel.DataContracts.PropertyDescriptorProxy()
    '    Return MyBase.GetListItemPropertiesForFieldList(dataSourceId, dataMember)
    'End Function

    'Public Overrides Function GetPages(documentId As DevExpress.Xpf.Printing.ServiceModel.DataContracts.DocumentId, pageIndexes() As Integer, compatibility As DevExpress.XtraReports.ServiceModel.DataContracts.PageCompatibility) As Byte()
    '    Return MyBase.GetPages(documentId, pageIndexes, compatibility)
    'End Function

    'Public Overrides Function GetPrintDocument(printId As DevExpress.XtraReports.ServiceModel.DataContracts.PrintId) As Stream
    '    Return MyBase.GetPrintDocument(printId)
    'End Function

    'Public Overrides Function GetPrintStatus(printId As DevExpress.XtraReports.ServiceModel.DataContracts.PrintId) As DevExpress.XtraReports.ServiceModel.DataContracts.PrintStatus
    '    Return MyBase.GetPrintStatus(printId)
    'End Function

    Public Overrides Function GetTables(dataSourceName As String) As IEnumerable(Of DevExpress.Data.XtraReports.DataProviders.TableInfo)
        Return MyBase.GetTables(dataSourceName)
    End Function

    Public Overrides Function GetViews(dataSourceName As String) As IEnumerable(Of DevExpress.Data.XtraReports.DataProviders.TableInfo)
        Return MyBase.GetViews(dataSourceName)
    End Function

    'Public Overrides Function InvokeReportEditing(reportId As DevExpress.XtraReports.ServiceModel.DataContracts.ReportSessionId, editActions() As DevExpress.XtraReports.ServiceModel.DataContracts.EditActionBase) As String
    '    Return MyBase.InvokeReportEditing(reportId, editActions)
    'End Function

    'Protected Overrides Sub MakeSessionUntransacted()
    '    MyBase.MakeSessionUntransacted()
    'End Sub

    'Public Overrides Function StartBuild(instanceIdentity As DevExpress.Xpf.Printing.ServiceModel.DataContracts.InstanceIdentity, buildArgs As DevExpress.XtraReports.ServiceModel.DataContracts.ReportBuildArgs) As DevExpress.Xpf.Printing.ServiceModel.DataContracts.DocumentId
    '    Return MyBase.StartBuild(instanceIdentity, buildArgs)
    'End Function

    'Public Overrides Function StartDesign(identity As DevExpress.Xpf.Printing.ServiceModel.DataContracts.InstanceIdentity, compatibility As DevExpress.Xpf.Printing.XamlExport.XamlCompatibility) As DevExpress.XtraReports.ServiceModel.DataContracts.ReportDesignerPage
    '    Return MyBase.StartDesign(identity, compatibility)
    'End Function

    'Public Overrides Function StartExport(documentId As DevExpress.Xpf.Printing.ServiceModel.DataContracts.DocumentId, exportArgs As DevExpress.XtraReports.ServiceModel.DataContracts.DocumentExportArgs) As DevExpress.Xpf.Printing.ServiceModel.DataContracts.ExportId
    '    Return MyBase.StartExport(documentId, exportArgs)
    'End Function

    'Public Overrides Function StartPrint(documentId As DevExpress.Xpf.Printing.ServiceModel.DataContracts.DocumentId, compatibility As DevExpress.XtraReports.ServiceModel.DataContracts.PageCompatibility) As DevExpress.XtraReports.ServiceModel.DataContracts.PrintId
    '    Return MyBase.StartPrint(documentId, compatibility)
    'End Function

    'Public Overrides Function StartUpload() As DevExpress.Xpf.Printing.ServiceModel.DataContracts.UploadingResourceId
    '    Return MyBase.StartUpload()
    'End Function

    'Public Overrides Sub StopBuild(documentId As DevExpress.Xpf.Printing.ServiceModel.DataContracts.DocumentId)
    '    MyBase.StopBuild(documentId)
    'End Sub

    'Public Overrides Sub StopPrint(printId As DevExpress.XtraReports.ServiceModel.DataContracts.PrintId)
    '    MyBase.StopPrint(printId)
    'End Sub

    'Public Overrides Sub UploadResourceChunk(id As DevExpress.Xpf.Printing.ServiceModel.DataContracts.UploadingResourceId, data() As Byte)
    '    MyBase.UploadResourceChunk(id, data)
    'End Sub

    Public Overrides Function WebGetExportedDocument(exportIdValue As String) As Stream
        Return MyBase.WebGetExportedDocument(exportIdValue)
    End Function

    Public Overrides Function WebGetResource(imageId As String) As Stream
        Return MyBase.WebGetResource(imageId)
    End Function

    Private Shared Sub BuildMessage(ByRef exObj As Exception, ByVal LoginInfo As LoginInfo)
        Try
            Dim li As New CableSoft.Utility.ErrorHandler.LoginInfo() With
                {
                    .CompCode = LoginInfo.CompCode,
                    .ConnectionString = LoginInfo.ConnectionString,
                    .DebugMode = LoginInfo.DebugMode,
                    .EntryId = LoginInfo.EntryId,
                    .EntryName = LoginInfo.EntryName,
                    .Provider = LoginInfo.Provider,
                    .GroupId = LoginInfo.GroupId,
                    .HttpPath = LoginInfo.HttpPath
                }

            Dim currContxt As HttpContext = HttpContext.Current

            If currContxt IsNot Nothing Then

                Dim requ As HttpRequest = currContxt.Request
                Dim ip As String = requ.UserHostAddress 'requ.ServerVariables("REMOTE_ADDR")
                Dim agnt As String = requ.UserAgent 'requ.ServerVariables("HTTP_USER_AGENT")
                Dim webPath As String = requ.MapPath("~/")

                Dim bws As String =
                    String.Format("{0} {1} ; CodePage: {2} ; UserLanguages: {3} ; MSDomVersion: {4} ; W3CDomVersion: {5}",
                                  requ.Browser.Browser,
                                  requ.Browser.Version,
                                  currContxt.Session.CodePage.ToString,
                                  requ.UserLanguages(0).ToString,
                                  requ.Browser.MSDomVersion,
                                  requ.Browser.W3CDomVersion)

                Dim ClrVer As String = requ.Browser.ClrVersion.ToString
                Dim pltfom As String = requ.Browser.Platform
                Dim url As String = requ.Url.ToString

                Dim resp As HttpResponse = currContxt.Response
                'resp.Output.WriteLine(requ.ServerVariables("REMOTE_HOST"))

                Dim hostEntry As String = Dns.GetHostEntry(requ.UserHostAddress).HostName
                hostEntry = hostEntry.Split(".")(0)

                Dim mobile As String =
                    String.Format("IsMobile: {0} ; Manufacturer: {1} ; Model: {2}",
                      requ.Browser.IsMobileDevice.ToString,
                      requ.Browser.MobileDeviceManufacturer,
                      requ.Browser.MobileDeviceModel)

                If li.ClientComputerName = String.Empty Then
                    li.ClientComputerName = IIf(hostEntry.Length > 0, hostEntry, currContxt.Server.MachineName)
                End If

                If li.MobileInfo = String.Empty Then li.MobileInfo = mobile
                If li.ClientIP = String.Empty Then li.ClientIP = ip
                If li.UserAgent = String.Empty Then li.UserAgent = agnt
                If li.HttpPath = String.Empty Then li.HttpPath = webPath
                If li.Browser = String.Empty Then li.Browser = bws
                If li.ClrVersion = String.Empty Then li.ClrVersion = ClrVer
                If li.Platform = String.Empty Then li.Platform = pltfom
            End If

            CableSoft.Utility.ErrorHandler.Helper.Log(exObj, -1024, "報表服務執行發生錯誤!", li)

        Catch innerEx As Exception
            Throw
        End Try

    End Sub

    Private Shared Function SerializeXML(ByVal objType As Type, ByRef obj As Object) As String
        SerializeXML = String.Empty
        Dim xmlSeri As XmlSerializer = Nothing
        Dim writ As StringWriter = Nothing
        Try
            xmlSeri = New XmlSerializer(objType)
            writ = New StringWriter
            xmlSeri.Serialize(writ, obj)
            Return writ.ToString
        Catch ex As Exception
            Throw
        Finally
            If writ IsNot Nothing Then
                writ.Close()
                writ.Dispose()
            End If
        End Try
    End Function

    Private Shared Function DeserializeXML(ByVal objType As Type, ByVal xml As String) As Object
        DeserializeXML = Nothing
        Dim xmlSeri As XmlSerializer = Nothing
        Dim read As StringReader = Nothing
        Try
            xmlSeri = New XmlSerializer(objType)
            read = New IO.StringReader(xml)
            Return xmlSeri.Deserialize(read)
        Catch ex As Exception
            Throw
        Finally
            If read IsNot Nothing Then
                read.Close()
                read.Dispose()
            End If
        End Try
    End Function

    Private Shared Function OBJ2JSON(objType As Type, obj As Object) As String
        Dim ms As MemoryStream = Nothing
        Try
            ms = New MemoryStream
            Dim jsSer As New DataContractJsonSerializer(objType)
            jsSer.WriteObject(ms, obj)
            Dim bytAry() As Byte = ms.ToArray()
            OBJ2JSON = Encoding.UTF8.GetString(bytAry, 0, bytAry.Length)
        Catch ex As Exception
            Throw
        Finally
            If ms IsNot Nothing Then
                ms.Close()
                ms.Dispose()
            End If
        End Try
    End Function

    Private Shared Function JSON2OBJ(objType As Type, json As String) As Object
        Dim ms As MemoryStream = Nothing
        Try
            ms = New MemoryStream(Encoding.UTF8.GetBytes(json))
            Dim jsSer As New DataContractJsonSerializer(objType)
            JSON2OBJ = jsSer.ReadObject(ms)
            ms.Close()
        Catch ex As Exception
            Throw
        Finally
            If ms IsNot Nothing Then
                ms.Close()
                ms.Dispose()
            End If
        End Try
    End Function

    Private Shared Function ObjToJsonByJavaScriptSerializer(obj As Object) As String
        Try
            Dim javaSer As New JavaScriptSerializer() With {.MaxJsonLength = Int32.MaxValue}
            Return javaSer.Serialize(obj)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Shared Function ObjToJsonByJsonNet(obj As Object) As String
        Try
            Return JsonConvert.SerializeObject(obj)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Shared Function ObjToJsonByJsonNet(obj As Object, [formatting] As Newtonsoft.Json.Formatting) As String
        Try
            Return JsonConvert.SerializeObject(obj, [formatting])
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Shared Function JsonToObjByJsonNet(json As String) As Object
        Try
            Return JsonConvert.DeserializeObject(json)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Shared Function JsonToObjByJsonNet(json As String, objType As Type) As Object
        Try
            Return JsonConvert.DeserializeObject(json, objType)
            'Dim csd As CSData = JsonConvert.DeserializeObject(Of CSData)(json)
            'Dim dtJA As JArray = JsonConvert.DeserializeObject(Of JArray)(json)
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class

'Dim subRpts As List(Of XRSubreport) = FindAllSubReport(rpt)

'Private Sub _xRpt_AfterPrint(sender As Object, e As EventArgs) Handles _xRpt.AfterPrint
'End Sub

'Private Sub _xRpt_BeforePrint(sender As Object, e As PrintEventArgs) Handles _xRpt.BeforePrint
'End Sub

'Private Sub _xRpt_PrintProgress(sender As Object, e As PrintProgressEventArgs) Handles _xRpt.PrintProgress
'End Sub

'Private Sub _xRpt_SaveComponents(sender As Object, e As SaveComponentsEventArgs) Handles _xRpt.SaveComponents
'End Sub

'CreateDocumentProgressEventArgs 
'ReportCondition
'ReportName
'CompanyName
'ReportTitle
'UserName
'Signature(Autograph)
'ReportPaperSize
'ReportOrientation

'ChangePrinter
'ChangeDefaultPrinter
'DefaultPrinter
'PreviewReport
'PrintReport
'SaveReport
'ProcessSubReport
'PreviousReport
'ChkPrinterExist
'GetPrinterInfo
'SendSQL
'SetCompCode

'PrintFunction(Optional ByRef ObjConnection As ADODB.Connection, _
'                Optional ByVal LoginEntry As String, Optional ByVal PrinterName As String, _
'                Optional ByVal ReportFileName As String, Optional ByVal MIDvalue As String, _
'                Optional ByVal Title As String, Optional ByVal Query As String, _
'                Optional ByVal ChooseCondition As String, Optional ByVal SortCondition As String, _
'                Optional SaveResult As Boolean, Optional ByVal JetDatabaseName As String, _
'                Optional BasicCode As Boolean, Optional ByVal FormulaString As String, _
'                Optional ByVal SubReport1SQLQuery As String, Optional ByVal SubReport2SQLQuery As String, _
'                Optional ByVal SubReport3SQLQuery As String, Optional ByVal SubReport4SQLQuery As String, _
'                Optional ByVal SubReport5SQLQuery As String, _
'                Optional ByVal AlfaValue As Boolean, Optional ByVal INI1path As String, Optional ByVal INI2path As String, _
'                Optional ByVal AppPath As String, Optional ByVal ToPrinter As Boolean, _
'                Optional ByVal ExtraCondition As String, Optional ByVal Exception As Boolean, _
'                Optional ByVal ShowProgressDialog As Boolean = True, _
'                Optional ByVal StartTime As Single, Optional ByRef ProcessTime As Single, _
'                Optional ByVal SubReport6SQLQuery As String, Optional ByVal SubReport7SQLQuery As String, Optional ByVal SubReport8SQLQuery As String, Optional ByVal SubReport9SQLQuery As String, _
'                Optional ByVal SubReport10SQLQuery As String, Optional ByVal SubReport11SQLQuery As String, Optional ByVal SubReport12SQLQuery As String, Optional ByVal SubReport13SQLQuery As String, _
'                Optional ByVal SubReport14SQLQuery As String, Optional ByVal SubReport15SQLQuery As String, Optional ByVal SubReport16SQLQuery As String, Optional ByVal SubReport17SQLQuery As String, _
'                Optional ByVal SubReport18SQLQuery As String, Optional ByVal SubReport19SQLQuery As String, Optional ByVal SubReport20SQLQuery As String, _
'                Optional ByVal SortFields1 As String = "", Optional ByVal SortFields2 As String = "", Optional ByVal SortFields3 As String = "") _
'                As Collection

'CstRptInitialize(Optional ByRef PrinterName As String, _
'                Optional ByRef ReportFileName As String, _
'                Optional ByRef MIDvalue As String, _
'                Optional ByRef Title As String, _
'                Optional ByRef Query As String, _
'                Optional ByRef Company As String, _
'                Optional ByRef UserName As String, _
'                Optional ByRef ChooseCondition As String, _
'                Optional ByRef SortCondition As String, _
'                Optional ByRef JetDatabaseName As String, _
'                Optional ByRef BasicCode As Boolean, _
'                Optional ByRef FormulaString As String, _
'                Optional ByRef SubReport1SQLQuery As String, Optional ByRef SubReport2SQLQuery As String, Optional ByRef SubReport3SQLQuery As String, Optional ByRef SubReport4SQLQuery As String, _
'                Optional ByRef SubReport5SQLQuery As String, Optional ByRef SubReport6SQLQuery As String, Optional ByRef SubReport7SQLQuery As String, Optional ByRef SubReport8SQLQuery As String, _
'                Optional ByRef SubReport9SQLQuery As String, Optional ByRef SubReport10SQLQuery As String, Optional ByRef SubReport11SQLQuery As String, Optional ByRef SubReport12SQLQuery As String, _
'                Optional ByRef SubReport13SQLQuery As String, Optional ByRef SubReport14SQLQuery As String, Optional ByRef SubReport15SQLQuery As String, Optional ByRef SubReport16SQLQuery As String, _
'                Optional ByRef SubReport17SQLQuery As String, Optional ByRef SubReport18SQLQuery As String, Optional ByRef SubReport19SQLQuery As String, Optional ByRef SubReport20SQLQuery As String, _
'                Optional ByRef ExtraCondition As String, _
'                Optional ByRef Exception As Boolean) As Boolean

'Protected Overrides Function CreateReportByName(ByVal reportTypeName As String) As XtraReport
'    Try
'        Dim rpt As New XtraReport1
'        'rpt.XrLabel1.Text = "Hammer"
'        Return rpt
'    Catch exception As Exception
'        Throw New FaultException(exception.Message)
'    End Try
'End Function

'Protected Overrides Function CreateReport(reportTypeName As String,
'                                          reportInformation As RootGenericReportParameterContainer) As XtraReport
'    Return (Nothing)
'End Function

'Protected Overrides Function StartBuild(report As XtraReport,
'                                        pageData As PageData) As DocumentId

'    'report.XrLabel1()
'    report.ShowDesignerDialog()
'    Return MyBase.StartBuild(report, pageData)
'    'Return (Nothing)
'End Function

'Public Function StartBuild(reportTypeName As String, reportInformation As RootGenericReportParameterContainer, serializedPageData() As Byte) As DocumentId Implements IReportService.StartBuild

'End Function


'Public Sub ClearDocument(documentId As DocumentId) Implements IExportService.ClearDocument

'End Sub

'Public Function GetExportedDocument(exportId As ExportId) As Stream Implements IExportService.GetExportedDocument

'End Function

'Public Function GetExportStatus(exportId As ExportId) As ExportStatus Implements IExportService.GetExportStatus

'End Function

'Public Function StartExport(documentId As DocumentId, format As DevExpress.XtraPrinting.ExportFormat, serializedExportOptions() As Byte) As ExportId Implements IExportService.StartExport

'End Function

'Public Function UploadDocument(stream As Stream) As DocumentId Implements IExportService.UploadDocument

'End Function

'Public Function GetBuildStatus(documentId As DocumentId) As BuildStatus Implements IReportService.GetBuildStatus

'End Function

'Public Function GetDocumentData(documentId As DocumentId) As DocumentData Implements IReportService.GetDocumentData

'End Function

'Public Function GetPage(documentId As DocumentId, pageIndex As Integer) As String Implements IReportService.GetPage

'End Function

'Public Function GetPrintDocument(printId As PrintId) As Stream Implements IReportService.GetPrintDocument

'End Function

'Public Function GetPrintStatus(printId As PrintId) As PrintStatus Implements IReportService.GetPrintStatus

'End Function

'Public Function GetReportInformation(reportTypeName As String) As RootReportParameterContainer Implements IReportService.GetReportInformation

'End Function

'Public Function StartBuild(reportTypeName As String, reportInformation As RootGenericReportParameterContainer, serializedPageData() As Byte) As DocumentId Implements IReportService.StartBuild

'End Function

'Public Function StartPrint(documentId As DocumentId) As PrintId Implements IReportService.StartPrint

'End Function

'Public Sub StopBuild(documentId As DocumentId) Implements IReportService.StopBuild

'End Sub

'Public Sub StopPrint(printId As PrintId) Implements IReportService.StopPrint

'End Sub

'Using rpt As XtraReport = MyBase.CreateReportByName(reportName)

'    'rpt = rpt.FromFile("", True)

'    'rpt.FromFile()
'    'rpt.BeginInit()
'    'rpt.EndInit()
'    'rpt.BeginUpdate()
'    'rpt.DataSource = ""
'    'rpt.PrinterName = ""
'    ''rpt.Parameters
'    'rpt.FillDataSource()
'    'rpt.EndUpdate()
'    'rpt.Print()

'    'rpt.Report

'    'rpt.CreateDocument
'    'rpt.ClosePreview
'    'rpt.CloseRibbonPreview
'    'rpt.DataSourceSchema
'    'rpt.FilterString
'    'rpt.DesignerOptions.ShowDesignerHints
'    'rpt.DetailPrintCount
'    'rpt.DetailPrintCountOnEmptyDataSource
'    'rpt.DisplayName
'    'rpt.GetCurrentColumnValue
'    'rpt.GetCurrentRow
'    'rpt.Landscape
'    'rpt.Pages.Count

'    'rpt.ShowDesigner()
'    'rpt.ShowDesignerDialog()
'    'rpt.ShowPreview()
'    'rpt.ShowPreviewDialog()
'    'rpt.ShowRibbonDesigner()
'    'rpt.ShowRibbonDesignerDialog()
'    'rpt.ShowRibbonPreview()
'    'rpt.ShowRibbonPreviewDialog()

'End Using


'SideBySideProducts sideBySideReport = (SideBySideProducts)report;
'sideBySideReport.xrSubreport1.ReportSource.DataSource = GetProducts(entities, Convert.ToInt32(report.Parameters["leftCategoryId"].Value));
'sideBySideReport.xrSubreport2.ReportSource.DataSource = GetProducts(entities, Convert.ToInt32(report.Parameters["rightCategoryId"].Value));

'private void xrTableCell112_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
'{
'    int factor;

'    Graphics gr = Graphics.FromHwnd(IntPtr.Zero);

'    if (this.ReportUnit == ReportUnit.HundredthsOfAnInch)
'    {
'        gr.PageUnit = GraphicsUnit.Inch;
'        factor = 100;
'    }
'    else
'    {
'        gr.PageUnit = GraphicsUnit.Millimeter;
'        factor = 10;
'    }

'    var cell = sender as XRTableCell;
'    var size =  gr.MeasureString(cell.Text, cell.Font);
'    while (cell.Width < Convert.ToInt32(size.Width * factor))
'    {
'        cell.Font = new Font(cell.Font.FontFamily, cell.Font.Size - 1F);
'        size = gr.MeasureString(cell.Text, cell.Font);
'    }

'    gr.Dispose();
'}

'Imports System
'Imports System.Drawing
'Imports System.Drawing.Printing
'Imports DevExpress.XtraReports.UI

'' ...

'Private Sub OnBeforePrint(ByVal sender As Object, ByVal e As PrintEventArgs) Handles XrLabel1.BeforePrint
'    Dim factor As Int32
'    Dim gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)
'    If Me.ReportUnit = ReportUnit.HundredthsOfAnInch Then
'        gr.PageUnit = GraphicsUnit.Inch
'        factor = 100
'    Else
'        gr.PageUnit = GraphicsUnit.Millimeter
'        factor = 10
'    End If
'    Dim size As SizeF = gr.MeasureString(CType(sender, XRLabel).Text, CType(sender, XRLabel).Font)
'    CType(sender, XRLabel).Width = size.Width * factor
'    gr.Dispose()
'End Sub

'Dim factor As Int32
'Dim gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)
'If rpt.ReportUnit = ReportUnit.HundredthsOfAnInch Then
'    gr.PageUnit = GraphicsUnit.Inch
'    factor = 100
'Else
'    gr.PageUnit = GraphicsUnit.Millimeter
'    factor = 10
'End If
'Dim size As SizeF = gr.MeasureString(lblCondition.Text, lblCondition.Font)
'lblCondition.Width = size.Width * factor
'gr.Dispose()
'lblCondition.Width = rpt.PrintingSystem.PageSettings.UsablePageSize.Width

'Protected Overrides Function CreateReportByName(reportName As String) As DevExpress.XtraReports.UI.XtraReport
'    Dim report = MyBase.CreateReportByName(reportName)
'    report.Parameters.Add(New DevExpress.XtraReports.Parameters.Parameter() With {.Name = "TESTx", .Value = "TEST33"})
'    Return report
'End Function

'Private Shared Function FindSubReport(ByVal rpt As XtraReport, ByVal bt As BandKind) As List(Of XRSubreport)
'    FindSubReport = Nothing
'    Using b As Band = rpt.Bands(bt)
'        If b IsNot Nothing Then
'            Return b.Controls.OfType(Of XRControl).Where(Function(x) TypeOf x Is XRSubreport).Cast(Of XRSubreport).ToList()
'            'Return b.Controls.OfType(Of XRControl).Where(Function(a) a.GetType() = GetType(XRSubreport)).Cast(Of XRSubreport).ToList()
'        End If
'    End Using
'End Function

'Public Function GetReportsByName(reportName As String) As DocumentId Implements IRptSvc.GetReportsByName
'    'Dim path As String = HttpContext.Current.Server.MapPath(String.Format("~/Reports/{0}.repx", reportName))
'    'Dim bytAry As Byte() = File.ReadAllBytes(path)
'    'Dim rpt As XtraReport = XtraReport.FromStream(New MemoryStream(bytAry), True)
'    ''report.DataSource = GetData();
'    'Dim doc As DocumentId = StartBuild(rpt)
'    'Return doc
'    Return Nothing
'End Function

'Private Shared Function FindAllSubReport(ByVal rpt As XtraReport) As List(Of XRSubreport)
'    Dim list As List(Of XRSubreport) = New List(Of XRSubreport)
'    'Dim r As List(Of XRSubreport) = FindSubReport(rpt, BandKind.BottomMargin)
'    'list.AddRange(r.ToArray)
'    'r = FindSubReport(rpt, BandKind.Detail)
'    For Each Item In [Enum].GetValues(GetType(BandKind))
'        Dim sr As List(Of XRSubreport) = FindSubReport(rpt, CType(Item, BandKind))
'        If sr IsNot Nothing AndAlso sr.Count > 0 Then list.AddRange(sr.ToArray)
'    Next
'    Return list
'End Function
'Public Overrides Function StartBuild(reportName As String,
'                                     parameters As IEnumerable(Of ParameterStub),
'                                     serializedPageData() As Byte) As DocumentId
'    Return MyBase.StartBuild(reportName, parameters, serializedPageData)
'End Function

'Public Overrides Function StartPrint(documentId As DocumentId) As PrintId
'    Return MyBase.StartPrint(documentId)
'End Function

'Public Overrides Function GetReportInformation(reportName As String) As ReportParameterContainer
'    Return MyBase.GetReportInformation(reportName)
'End Function

'Public Overrides Function GetDocumentData(documentId As DocumentId) As DocumentData
'    Return MyBase.GetDocumentData(documentId)
'End Function

'Public Overrides Function GetBuildStatus(documentId As DocumentId) As BuildStatus
'    Return MyBase.GetBuildStatus(documentId)
'End Function

'Protected Overrides Sub ConfigureReport(report As XtraReport)
'    MyBase.ConfigureReport(report)
'End Sub
