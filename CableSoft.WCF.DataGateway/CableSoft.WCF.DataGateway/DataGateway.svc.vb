Imports CableSoft.BLL.Utility
Imports System.Reflection
Imports System.IO
Imports System.ServiceModel.Activation
Imports System.Drawing

<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)> _
Public Class DataGateway
    Implements IDataGateway
    Const getKey As String = "getkey"
    Const Pass As Boolean = True
    Const checkLogiInfo As String = "Check_LoginInfo"
    Public Sub New()

    End Sub
    Private Function checkCanGetKey(jObject As Newtonsoft.Json.Linq.JObject) As Boolean
        Dim dllname As String = Nothing
        If jObject.Property("dllname").Value IsNot Nothing Then
            dllname = jObject.Property("dllname").Value
        End If
        Dim methodname As String = Nothing
        If jObject.Property("methodname").Value IsNot Nothing Then
            methodname = jObject.Property("methodname").Value
        End If
        If (String.IsNullOrEmpty(dllname) = False AndAlso dllname.ToUpper() = getKey.ToUpper()) OrElse (String.IsNullOrEmpty(methodname) = False AndAlso methodname = "Check_LoginInfo") Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetDecodeValue(value As String) As String
        If value.IndexOf("dllname") >= 0 Then
            Return value
        End If
        Dim newBytes As Byte() = Convert.FromBase64String(value)
        Return System.Text.Encoding.UTF8.GetString(newBytes)
    End Function
    Public Function GetData(value As String) As String Implements IDataGateway.GetData
        Try
            Dim IsLogin As Boolean = False
            LogToText(value, Nothing)
            Dim jObject As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(GetDecodeValue(value))
            Dim result As RIAResult = ChkParameters(jObject)
            If result.ResultBoolean = False Then
                Return ReturnString(result, False)
            End If
            LogToText(1, Nothing, Pass)
            Dim dllname As String = Nothing
            If jObject.Property("dllname").Value IsNot Nothing Then
                dllname = jObject.Property("dllname").Value
            End If
            LogToText(2, Nothing, Pass)
            If String.IsNullOrEmpty(dllname) = False AndAlso dllname.ToUpper() = getKey.ToUpper() Then
                LogToText(21, Nothing, Pass)
                Dim ChkKeyOk As Boolean = ChkGetKeyDataOk(jObject.Property("classname").Value)
                Return (ReturnString(Nothing, ChkKeyOk))
            Else
                LogToText(22, Nothing, Pass)
                Dim classname As String = jObject.Property("classname").Value
                Dim methodname As String = jObject.Property("methodname").Value
                Dim encryptkey As String = jObject.Property("encryptkey").Value
                Dim parameters As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jObject.Property("parameters").Value)
                Dim RIAObj As Object = CableSoft.BLL.Utility.Utility.DynamicCreateClass(dllname, classname, Nothing, Nothing)
                Dim LoginInfoName As String = classname.Replace(classname.Split(".")(classname.Split(".").GetUpperBound(0)), "") & "logininfo"
                Dim LoginInfo As Object = Nothing
                LogToText(23, Nothing, Pass)

                Dim method As MethodInfo = RIAObj.GetType().GetMethod(methodname, Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
                If "CableSoft.BLL.Utility.LoginInfo".ToUpper() = method.GetParameters(0).ParameterType.FullName.ToUpper() Then
                    LoginInfo = New LoginInfo()
                Else
                    LoginInfo = CableSoft.BLL.Utility.Utility.DynamicCreateClass(dllname, LoginInfoName, Nothing, Nothing)
                End If
                LogToText(24, Nothing, Pass)
                'LogToText(String.Format("path is :{0},LoginInfo is nothing = {1}40", AppDomain.CurrentDomain.BaseDirectory, (LoginInfo Is Nothing)), Nothing)
                If RIAObj.GetType().GetProperty("isHTML") IsNot Nothing Then
                    RIAObj.isHTML = True
                End If
                LogToText(25, Nothing, Pass)
                Dim args(parameters.Properties.Count - 1) As Object
                If Not PushEUDCToApplication() Then
                    Throw New Exception("PushEUDCToApplication")
                End If
                For intLoop As Integer = 0 To parameters.Properties.Count - 1
                    Dim jp As Newtonsoft.Json.Linq.JProperty = parameters.Properties(intLoop)
                    Dim ErrorCode As String = Nothing
                    Dim ErrorMsg As String = Nothing
                    args(intLoop) = GetParameterValue(jp, LoginInfo, methodname.ToUpper().IndexOf(checkLogiInfo.ToUpper()) >= 0, ErrorCode, ErrorMsg)
                    If ErrorCode < 0 Then
                        Return ReturnString(New RIAResult With {.ResultBoolean = False, .ErrorCode = ErrorCode, .ErrorMessage = ErrorMsg}, False)
                    End If
                Next
                Try
                    LogToText(26, Nothing, Pass)
                    Dim resultObj As Object = method.Invoke(RIAObj, args)
                    If RIAObj.GetType().GetMethod("Dispose") IsNot Nothing Then
                        RIAObj.dispose()
                    End If
                    RIAObj = Nothing
                    LogToText(27, Nothing, Pass)
                    resultObj = ConvertDataSet(resultObj)
                    LogToText(28, Nothing, Pass)
                    Dim rString As String = ReturnString(resultObj, True)
                    LogToText(29, Nothing, Pass)
                    LogToText(rString, Nothing, Pass)
                    LogToText(30, Nothing, Pass)
                    Return rString
                Catch ex As Exception
                    LogToText(value, ex.ToString())
                    Return ReturnString(New RIAResult With {.ResultBoolean = False, .ErrorCode = -999, .ErrorMessage = ex.ToString()}, True)
                End Try
            End If
        Catch ex As Exception
            LogToText(value, ex.ToString())
            Return ReturnString(New RIAResult With {.ResultBoolean = False, .ErrorCode = -999, .ErrorMessage = ex.ToString()}, False)
        End Try
    End Function
    Private Shared Function UnicodeToEUDC(text As String) As String
        If HttpContext.Current.Application("EUDC") Is Nothing Then
            Return text
        End If
        UnicodeToEUDC = ""
        Try
            Dim dicEUDC As Dictionary(Of String, String) = HttpContext.Current.Application("EUDC")
            For Each c As Char In text.ToCharArray
                If Encoding.UTF8.GetBytes(c)(0) > 128 AndAlso dicEUDC.ContainsKey(c) Then text = text.Replace(c, dicEUDC(c))
            Next
            Return text
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function PushEUDCToApplication() As Boolean
        If HttpContext.Current.Application("EUDC") Is Nothing Then
            Dim mapFile As String = HttpContext.Current.Server.MapPath("~/EUDC_Mapping.xml")
            If IO.File.Exists(mapFile) Then
                HttpContext.Current.Application("EUDC") = InitEUDC(mapFile)
            End If
        End If
        PushEUDCToApplication = True
    End Function
    Private Function InitEUDC(mapFile As String) As Dictionary(Of String, String)
        InitEUDC = Nothing
        Dim dt As DataTable = Nothing
        Dim dicEUDC As Dictionary(Of String, String)
        Try
            dt = New DataTable("EUDC")
            dt.ReadXml(mapFile)
            dicEUDC = New Dictionary(Of String, String)
            For Each dr As DataRow In dt.Rows
                If Not dr.IsNull("Big5") AndAlso Not dr.IsNull("UTF8") Then
                    If Not dicEUDC.ContainsKey(dr("UTF8")) Then dicEUDC.Add(dr("UTF8"), dr("Big5"))
                End If
            Next
            Return dicEUDC
        Catch ex As Exception
            Throw ex
        Finally
            If dt IsNot Nothing Then dt.Dispose()
        End Try
    End Function
    Private Function ChkGetKeyDataOk(value As String) As Boolean
        Dim RealValue As String = GetDecryptKey(value)
        If String.IsNullOrEmpty(RealValue) = False Then
            If DateTime.Now > DateTime.Parse(RealValue).AddSeconds(60) Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function
    Private Function ConvertDataSet(resultObj As Object) As Object
        If TypeOf resultObj Is RIAResult Then
            Return ConvertRIAResult(resultObj)
        ElseIf TypeOf resultObj Is RIAResult() Then
            For Each obj In resultObj
                obj = ConvertRIAResult(obj)
            Next
            Return resultObj
        Else
            Return resultObj
        End If
    End Function
    Private Function ConvertRIAResult(resultObj As RIAResult) As RIAResult
        Dim result As RIAResult = resultObj
        If result.ResultBoolean = True AndAlso String.IsNullOrEmpty(result.ResultXML) = False AndAlso
            ChkIsJSON(result.ResultXML) AndAlso result.ResultXML.IndexOf("rows") < 0 AndAlso result.ResultXML.IndexOf("columns") < 0 Then
            Dim data As DataSet = CableSoft.BLL.Utility.JsonServer.FromJson(result.ResultXML)
            Dim d As New Dictionary(Of String, DataSet)
            Dim tableNames As String = Nothing
            For Each table As DataTable In data.Tables
                tableNames &= "," & table.TableName
            Next
            If data.Tables.Count = 0 OrElse String.IsNullOrEmpty(tableNames) Then
                Return result
            End If
            For Each tableName As String In tableNames.Substring(1).Split(",")
                Dim rows As DataTable = data.Tables(tableName)
                For Each rowCol As DataColumn In rows.Columns
                    If Char.IsLetter(rowCol.ColumnName.Substring(1, 1)) Then
                        rowCol.ColumnName = rowCol.ColumnName.ToUpper()
                    End If
                Next
                Dim columns As New DataTable() With {.TableName = "columns"}
                columns.Columns.Add(New DataColumn With {.ColumnName = "name"})
                columns.Columns.Add(New DataColumn With {.ColumnName = "type"})
                For Each col As DataColumn In data.Tables(tableName).Columns
                    Dim nRow As DataRow = columns.NewRow()
                    nRow.Item("name") = col.ColumnName.ToUpper()
                    nRow.Item("type") = col.DataType.ToString().ToLower().Replace("system.", "")
                    If nRow.Item("type").ToString().ToUpper() = "DATETIME" Then
                        nRow.Item("type") = "date"
                    End If
                    columns.Rows.Add(nRow)
                Next
                data.Tables.Remove(rows)
                Dim nData As New DataSet()
                nData.Tables.Add(rows)
                nData.Tables.Add(columns)
                rows.TableName = "rows"
                d.Add(tableName, nData)
            Next
            result.ResultXML = Newtonsoft.Json.JsonConvert.SerializeObject(d, getSettings())
        End If
        result.DownloadFileName = ChangeDownloadFile(result.DownloadFileName)
        Return result
    End Function
    Private Shared Function ChangeDownloadFile(ByVal DownloadFileName As String) As String
        If String.IsNullOrEmpty(DownloadFileName) = False Then
            'Dim fileServerPath As String = CableSoft.BLL.Utility.Utility.GetAppSettingValue("fileServerPath")
            'If String.IsNullOrEmpty(fileServerPath) = False Then
            '    DownloadFileName = DownloadFileName.Replace(CableSoft.BLL.Utility.Utility.GetRelativePath(), "")
            '    Dim tPath As String = IO.Path.GetFullPath(fileServerPath & "\") & DownloadFileName.Replace("/", "\")
            '    Dim sPath As String = CableSoft.BLL.Utility.Utility.GetCurrentDirectory & DownloadFileName.Replace("/", "\")
            '    If tPath <> sPath Then
            '        IO.File.Copy(sPath, tPath, True)
            '    End If
            '    DownloadFileName = "downloadfiles.ashx?filename=" & DownloadFileName
            'End If
            'CableSoft.BLL.Utility.Utility.GetFileNameFromFileServer(DownloadFileName, False)
            If String.IsNullOrEmpty(CableSoft.BLL.Utility.Utility.GetAppSettingValue("fileServerPath")) = False Then
                CableSoft.BLL.Utility.Utility.SaveFileToFileServer(DownloadFileName)
            End If
            DownloadFileName = DownloadFileName.Replace(CableSoft.BLL.Utility.Utility.GetRelativePath(), "")
            DownloadFileName = "downloadfiles.ashx?filename=" & DownloadFileName
        End If
        Return DownloadFileName
    End Function
    Private Function getSettings() As Newtonsoft.Json.JsonSerializerSettings
        Dim setting As New Newtonsoft.Json.JsonSerializerSettings()
        setting.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local
        setting.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include
        Return setting
    End Function
    Private Function ChkIsJSON(js As String) As Boolean
        If (js.StartsWith("{") AndAlso js.EndsWith("}")) OrElse
            (js.StartsWith("[") AndAlso js.EndsWith("]")) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function GetParameterValue(jp As Newtonsoft.Json.Linq.JProperty, LoginInfo As Object,
                                       IsLogiIn As Boolean, ByRef ErrorCode As Integer,
                                       ByRef ErrorMsg As String) As Object
        Dim typeName As String = jp.Value.Item("type")
        If typeName Is Nothing Then
            typeName = "string"
        End If
        If jp.Value.Item("value") Is Nothing OrElse jp.Value.Item("value").ToString().Length = 0 Then
            Return Nothing
        End If
        Select Case typeName.ToLower()
            Case "logininfo"
                Dim value As Newtonsoft.Json.Linq.JToken = jp.Value.Item("value")
                Dim row As Newtonsoft.Json.Linq.JToken = Nothing
                If value.Item("loginInfo") IsNot Nothing Then
                    row = value.Item("loginInfo").Item("rows").Item(0)
                Else
                    row = value.Item("rows").Item(0)
                End If
                With LoginInfo
                    .CompCode = row.Item("compcode")
                    .EntryId = row.Item("entryid")
                    .EntryName = row.Item("entryname")
                    .Provider = GetProvider()
                    .GroupId = row.Item("groupid")
                    If row.Item("riatimeout") IsNot Nothing Then
                        .RIATimeout = row.Item("riatimeout")
                    Else
                        .RIATimeout = 300
                    End If
                    '2018/02/27 Jacky 重取groupId
                    Using bll As New CableSoft.SO.BLL.Utility.Utility(CableSoft.BLL.Utility.Utility.GetRealLoginInfo(LoginInfo))
                        'Using groupId As DataTable = bll.GetGroupID()
                        '    If groupId IsNot Nothing AndAlso groupId.Rows.Count > 0 Then
                        '        .GroupId = groupId.Rows(0).Item("groupid")
                        '    End If
                        'End Using
                        If IsLogiIn = False Then
                            '2018/06/25 Jacky 增加檢核登入人員合不合法
                            If HttpContext.Current.Request.Url.Host.IndexOf("localhost") < 0 Then
                                If String.IsNullOrEmpty(row.Item("LoginCompCode")) = False AndAlso row.Item("LoginCompCode").ToString() <> .CompCode Then
                                    Dim tLoginInfo As New LoginInfo()
                                    CableSoft.BLL.Utility.Utility.CopyObjA2ObjB(LoginInfo, tLoginInfo)
                                    tLoginInfo.CompCode = row.Item("LoginCompCode").ToString()
                                    tLoginInfo.ConnectionString = Nothing
                                    Using nBLL As New CableSoft.SO.BLL.Utility.Utility(CableSoft.BLL.Utility.Utility.GetRealLoginInfo(tLoginInfo))
                                        Dim r As RIAResult = nBLL.ChkLoginIDVerify(row.Item("LoginEncryptkey"))
                                        If r.ResultBoolean = False Then
                                            ErrorCode = -998
                                            ErrorMsg = r.ErrorMessage
                                        End If
                                    End Using
                                Else
                                    Dim r As RIAResult = bll.ChkLoginIDVerify(row.Item("LoginEncryptkey"))
                                    If r.ResultBoolean = False Then
                                        ErrorCode = -998
                                        ErrorMsg = r.ErrorMessage
                                    End If
                                End If
                            End If
                        End If
                    End Using
                End With
                Return LoginInfo
            Case "integer", "int32"
                Return Integer.Parse(jp.Value.Item("value").ToString())
            Case "long", "int64"
                Return Long.Parse(jp.Value.Item("value").ToString())
            Case "int16", "short"
                Return Short.Parse(jp.Value.Item("value").ToString())
            Case "decimal"
                Return Decimal.Parse(jp.Value.Item("value").ToString())
            Case "datetime", "date"
                Return DateTime.Parse(jp.Value.Item("value").ToString())
            Case "double", "single"
                Return Double.Parse(jp.Value.Item("value").ToString())
            Case "boolean"
                Return Boolean.Parse(jp.Value.Item("value").ToString())
            Case Else
                Return UnicodeToEUDC(jp.Value.Item("value").ToString())
        End Select
    End Function
    Private Function ReturnString(result As Object, getKey As Boolean) As String
        Dim d As New Dictionary(Of String, Object)
        LogToText(281, Nothing, Pass)
        If getKey Then
            d.Add("encryptkey", GetEncryptKey())
        End If
        LogToText(282, Nothing, Pass)
        If result IsNot Nothing Then
            d.Add("parameters", result)
        End If
        LogToText(283, Nothing, Pass)
        Return Newtonsoft.Json.JsonConvert.SerializeObject(d, getSettings())
    End Function
    Private Function ChkParameters(jObj As Newtonsoft.Json.Linq.JObject) As RIAResult
        If jObj.Property("dllname") Is Nothing Then
            Return New RIAResult With {.ResultBoolean = False, .ErrorCode = -998, .ErrorMessage = "Parameter: dllname not exist"}
        End If
        If jObj.Property("dllname").Value.ToString().ToUpper() <> getKey.ToUpper() Then
            Dim pNames As String() = {"classname", "methodname", "parameters", "encryptkey"}
            For Each pname As String In pNames
                If jObj.Property(pname) Is Nothing Then
                    Return New RIAResult With {.ResultBoolean = False, .ErrorCode = -998, .ErrorMessage = "Parameter: " & pname & " not exist"}
                End If
            Next
            Dim firstTime As Boolean = False
            Dim EncryptKey As String = Nothing
            If firstTime = False Then
                Dim value As String = jObj.Property("encryptkey").Value.ToString()
                Dim aesKey As String = GetParaEncryptKey()
                Dim result As RIAResult = ChkEncryptKeyOK(value, Nothing, aesKey)
                If result.ResultBoolean = False Then
                    Return result
                End If
            End If
        End If
        Return New RIAResult With {.ResultBoolean = True}
    End Function
    Public Function ChkEncryptKeyOK(ValueStr As String, ClientData As String,
                                    aesKey As String) As RIAResult
        If String.IsNullOrEmpty(ValueStr) Then
            Return New RIAResult With {.ResultBoolean = False, .ErrorCode = -998, .ErrorMessage = "Must Have EncryptKey !!"}
        Else
            Dim aes As New CableSoft.Utility.Cryptography.AES(GetParaEncryptKey())
            Dim DeStr As String = Nothing
            Try
                DeStr = aes.Decrypt(ValueStr)
            Catch ex As Exception
                Return New RIAResult With {.ResultBoolean = False, .ErrorCode = -998, .ErrorMessage = "Illegal EncryptKey !!"}
            End Try
            If Not DateTime.TryParse(DeStr, Nothing) Then
                Return New RIAResult With {.ResultBoolean = False, .ErrorCode = -998, .ErrorMessage = "Illegal EncryptKey !!"}
            End If
            If DateTime.Parse(DeStr) < DateTime.Now Then
                Return New RIAResult With {.ResultBoolean = False, .ErrorCode = -998, .ErrorMessage = String.Format("Timeout!!{0}{1}", ControlChars.CrLf, DateTime.Parse(DeStr))}
            End If
        End If
        Return New RIAResult With {.ResultBoolean = True}
    End Function
    Private Shared ReadOnly objLock As New Object() ' 用於 SyncLock 陳述式
    Private Function LogToText(value As String, ErrorMsg As String) As Boolean
        Return LogToText(value, ErrorMsg, False)
    End Function
    Private Function LogToText(value As String, ErrorMsg As String, Pass As Boolean) As Boolean
        Try
            If Pass Then
                Return True
            End If
            Dim Path As String = String.Format("{0}\log\", CableSoft.BLL.Utility.Utility.GetCurrentDirectory)
            Dim fileName As String = String.Format("{0}\{1}_{2}.log", Path, "DataGateway", DateTime.Now.ToString("yyyyMMdd"))
            SyncLock objLock
                Dim LogMsg As String = String.Format("時間:{0:yyyy/MM/dd HH:mm:ss:ff},傳入字串:{1}", DateTime.Now, value)
                If String.IsNullOrEmpty(ErrorMsg) = False Then
                    LogMsg = String.Format("{0},錯誤訊息:{1}", LogMsg, ErrorMsg)
                End If
                Using sw As New StreamWriter(fileName, True)
                    sw.WriteLine(LogMsg)
                    sw.Close()
                End Using
                'Dim LogFile As IO.TextWriter = Nothing
                'If IO.File.Exists(fileName) Then
                '    LogFile = New StreamWriter(fileName, True)
                'Else
                '    LogFile = IO.File.CreateText(fileName)
                'End If
                'Dim LogMsg As String = String.Format("時間:{0:yyyy/MM/dd HH:mm:ss:ff},傳入字串:{1}", DateTime.Now, value)
                'If String.IsNullOrEmpty(ErrorMsg) = False Then
                '    LogMsg = String.Format("{0},錯誤訊息:{1}", LogMsg, ErrorMsg)
                'End If
                'LogFile.WriteLine(LogMsg)
                'LogFile.Flush()
                'LogFile.Close()
                'LogFile.Dispose()
            End SyncLock
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetEncryptKey() As String
        Dim aes As New CableSoft.Utility.Cryptography.AES(GetParaEncryptKey())
        Return aes.Encrypt(Date.Now.AddHours(2).ToString("yyyy/MM/dd HH:mm:ss"))
    End Function
    Private Function GetDecryptKey(value As String) As String
        Try
            Dim aes As New CableSoft.Utility.Cryptography.AES(GetParaEncryptKey())
            Dim realValue As String = aes.Decrypt(value)
            Return realValue
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private ReadOnly AppSetting As New System.Configuration.AppSettingsReader()
    Private Function GetProvider() As String
        Return AppSetting.GetValue("Provider", GetType(String))
    End Function
    Private Function GetParaEncryptKey() As String
        Return AppSetting.GetValue("EncryptKey", GetType(String))
    End Function
    Public Function UploadFile(f() As Byte, path As String, FileName As String) As String Implements IDataGateway.UploadFile
        Try
            Dim r As Boolean = CableSoft.BLL.Utility.Utility.WriteFileToFileServer(f, String.Format("{0}\{1}", path, FileName))
            'Dim ms As MemoryStream = New MemoryStream(f)
            'Dim fs As FileStream = New FileStream(String.Format("{0}\{1}\{2}", CableSoft.BLL.Utility.Utility.GetCurrentDirectory(), path, FileName), FileMode.Create)
            'ms.WriteTo(fs)
            'ms.Close()
            'ms.Dispose()
            'fs.Close()
            'fs.Dispose()
            Return ReturnString(New RIAResult With {.ResultBoolean = r}, False)
        Catch ex As Exception
            LogToText(Nothing, ex.ToString())
            Return ReturnString(New RIAResult With {.ResultBoolean = False, .ErrorCode = -999, .ErrorMessage = ex.ToString()}, False)
        End Try
    End Function

    Public Function DownloadFile(FileName As String) As Byte() Implements IDataGateway.DownloadFile
        Try
            Return CableSoft.BLL.Utility.Utility.ReadFileFromFileServer(FileName)
            'Dim fo As FileStream = File.Open(FileName, FileMode.Open)
            'Dim br As IO.BinaryReader = New IO.BinaryReader(fo)
            'Dim numBytes As Long = fo.Length
            '' convert the file to a byte array
            'Dim data As Byte() = br.ReadBytes(CType(numBytes, Integer))
            'br.Close()
            'br.Dispose()
            'fo.Close()
            'fo.Dispose()
            'Return data
        Catch ex As Exception
            LogToText(Nothing, ex.ToString())
            Return Nothing
        End Try
    End Function
End Class
