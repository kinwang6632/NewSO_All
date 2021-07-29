
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
Public Class Command
    'Inherits DomainService
    Implements IDisposable
    Private _Command As CableSoft.SO.BLL.Facility.Command.Command
    Private result As New RIAResult()
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _Command = New CableSoft.SO.BLL.Facility.Command.Command(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Public Function InsertCommand(ByVal LoginInfo As LoginInfo,
                                ByVal TableName As String,
                                ByVal CMDID As String, ByVal dsWipData As String,
                                ByVal NoWait As Boolean,
                                ByVal WriteTimeOut As Boolean,
                                ByVal DeleteData As Boolean) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim objDs As DataSet
            objDs = Silverlight.DataSetConnector.Connector.FromXml(dsWipData)
            Dim CmdRIAResult As CableSoft.BLL.Utility.RIAResult = _Command.InsertCommand(TableName, CMDID,
                                                                                         objDs, NoWait,
                                                                                         WriteTimeOut, DeleteData)
            Dim strXML As String = Nothing
            If CmdRIAResult.ResultDataSet IsNot Nothing Then
                strXML = CableSoft.BLL.Utility.JsonServer.ToJson(CmdRIAResult.ResultDataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'strXML = Silverlight.DataSetConnector.Connector.ToXml(CmdRIAResult.ResultDataSet)
            End If
            'Dim aRet As RIAResult = RIAResult.Convert(CmdRIAResult)
            Dim aRet As RIAResult = CmdRIAResult
            aRet.ResultXML = strXML
            Return aRet
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Command.Dispose()
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: 處置 Managed 狀態 (Managed 物件)。
            End If

            ' TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下面的 Finalize()。
            ' TODO: 將大型欄位設定為 null。
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: 只有當上面的 Dispose(ByVal disposing As Boolean) 有可釋放 Unmanaged 資源的程式碼時，才覆寫 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 請勿變更此程式碼。在上面的 Dispose(ByVal disposing As Boolean) 中輸入清除程式碼。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' 由 Visual Basic 新增此程式碼以正確實作可處置的模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 請勿變更此程式碼。在以上的 Dispose 置入清除程式碼 (ByVal 視為布林值處置)。
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

