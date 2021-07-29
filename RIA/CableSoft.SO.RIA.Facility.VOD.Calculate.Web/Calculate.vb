
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
Public Class Calculate
    'Inherits DomainService
    Implements IDisposable

    Private _Calculate As CableSoft.SO.BLL.Facility.VOD.Calculate.Calculate
    Private result As New RIAResult()
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _Calculate = New CableSoft.SO.BLL.Facility.VOD.Calculate.Calculate(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Public Function ChkAuthority(ByVal LoginInfo As LoginInfo, ByVal Mid As String) As RIAResult
        Try
            InitClass(LoginInfo)
            result = _Calculate.ChkAuthority(Mid)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If _Calculate IsNot Nothing Then
                _Calculate.Dispose()
                _Calculate = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function CreateBillNo(ByVal LoginInfo As LoginInfo,
                                 ByVal dsSource As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsSource)
            Dim dsResult As DataSet = _Calculate.CreateBillNo(ds)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsResult)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Calculate.Dispose()
        End Try
        Return result
    End Function
    Public Function DeleteView(ByVal LoginInfo As LoginInfo, ByVal ViewName As String) As RIAResult
        Try
            InitClass(LoginInfo)
            'Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsSource)
            Return _Calculate.DeleteView(ViewName)
            'Dim dsResult As DataSet = _Calculate.CalculateBill(ds)
            'result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsResult)
            'result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Calculate.Dispose()
            _Calculate = Nothing
        End Try
        Return result
    End Function
    Public Function CalculateBill(ByVal LoginInfo As LoginInfo, ByVal dsSource As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsSource)
            Return _Calculate.CalculateBill(ds)
            'Dim dsResult As DataSet = _Calculate.CalculateBill(ds)
            'result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsResult)
            'result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Calculate.Dispose()
        End Try
        Return result
    End Function
    Public Function ImportExcel(ByVal LoginInfo As LoginInfo, ByVal xlsFileName As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _Calculate.ImportExcel(xlsFileName)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Calculate.Dispose()
        End Try
        Return result
    End Function
    Public Function QueryDefaultValue(ByVal LoginInfo As LoginInfo,
                                ByVal ServiceType As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Dim ds As DataSet = _Calculate.QueryDefaultValue(ServiceType)
            result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds)
            result.ResultBoolean = True
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Calculate.Dispose()
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

