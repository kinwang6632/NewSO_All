
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
Public Class ManualNo
    'Inherits DomainService
    Implements IDisposable
    Private _ManualNo As CableSoft.SO.BLL.Billing.ManualNo.ManualNo
    Private result As New RIAResult()
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _ManualNo = New CableSoft.SO.BLL.Billing.ManualNo.ManualNo(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Public Function QueryData(ByVal LoginInfo As LoginInfo, dsWhere As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsWhere)
            Dim ds As DataSet = _ManualNo.QueryData(objDS)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function DeleteData(ByVal LoginInfo As LoginInfo, ByVal ds As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(ds)
                result = _ManualNo.DeleteData(objDS)
                objDS.Dispose()
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function CanDelete(ByVal LoginInfo As LoginInfo, ByVal ds As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(ds)
                result = _ManualNo.CanDelete(objDS)
                objDS.Dispose()
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function ReUseSave(ByVal LoginInfo As LoginInfo, ByVal ds As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(ds)
                result = _ManualNo.ReUseSave(objDS)
                objDS.Dispose()
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function

    Public Function SaveData(ByVal LoginInfo As LoginInfo, ByVal editMode As EditMode, ByVal ds As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(ds)
                result = _ManualNo.SaveData(editMode, objDS)
                objDS.Dispose()
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function UpdNewManualNo(ByVal LoginInfo As LoginInfo, ByVal dsData As String) As RIAResult
        Try

            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsData)
                result = _ManualNo.UpdNewManualNo(objDS)
                objDS.Dispose()
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function ChkNewManualNo(ByVal LoginInfo As LoginInfo, ByVal dsData As String) As RIAResult
        Try

            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsData)
                result = _ManualNo.ChkNewManualNo(objDS)
                objDS.Dispose()
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function QueryBillData(ByVal LoginInfo As LoginInfo, ByVal dsData As String) As RIAResult
        Try

            InitClass(LoginInfo)
            result.ErrorCode = 0
            result.ErrorMessage = Nothing
            result.ResultBoolean = True
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsData)
                Using dsResult As DataSet = _ManualNo.QueryBillData(objDS)
                    result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsResult, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                    dsResult.Tables(0).Dispose()
                    dsResult.Dispose()
                End Using
                objDS.Dispose()
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function abandonPaper(ByVal LoginInfo As LoginInfo, ByVal dsData As String) As RIAResult
        Try

            InitClass(LoginInfo)

            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsData)
                result = _ManualNo.abandonPaper(objDS)
                objDS.Dispose()
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function


    Public Function QueryPaperNum(ByVal LoginInfo As LoginInfo, ByVal dsData As String) As RIAResult
        Try

            InitClass(LoginInfo)
            result.ErrorCode = 0
            result.ErrorMessage = Nothing
            result.ResultBoolean = True
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsData)
                Using dsResult As DataSet = _ManualNo.QueryPaperNum(objDS)
                    result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsResult, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                    dsResult.Tables(0).Dispose()
                    dsResult.Dispose()
                End Using
                objDS.Dispose()
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function QueryCompCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = New DataSet
                ds.Tables.Add(_ManualNo.QueryCompCode.Copy)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
                ds.Tables(0).Dispose()
                ds.Dispose()
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function QueryEmployee(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = New DataSet
                ds.Tables.Add(_ManualNo.QueryEmployee.Copy)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
                ds.Tables(0).Dispose()
                ds.Dispose()
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function QueryAllData(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _ManualNo.QueryAllData
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
    End Function

    Public Function ChkAuthority(ByVal LoginInfo As LoginInfo, ByVal Mid As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _ManualNo.ChkAuthority(Mid)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If _ManualNo IsNot Nothing Then
                _ManualNo.Dispose()
                _ManualNo = Nothing
            End If
        End Try
        Return result
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

