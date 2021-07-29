
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
Public Class Invoice
    'Inherits DomainService
    Implements IDisposable

    Private _Invoice As CableSoft.INV.BLL.CreateInvoice.Invoice
    Private _Batch As CableSoft.INV.BLL.CreateInvoice.BatchCreate
    Private result As New RIAResult()
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        _Invoice = New CableSoft.INV.BLL.CreateInvoice.Invoice(LoginInfo.ConvertTo(LoginInfo))
    End Sub
    Public Function CanEdit(ByVal LoginInfo As LoginInfo, ByVal invid As String) As RIAResult
        Try
            InitClass(LoginInfo)

            result = _Invoice.CanEdit(invid)


        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Invoice IsNot Nothing Then
                _Invoice.Dispose()
                _Invoice = Nothing
            End If

        End Try
        Return result
    End Function
    Public Function ChkAuthority(ByVal LoginInfo As LoginInfo, ByVal Mid As String) As RIAResult
        _Batch = New CableSoft.INV.BLL.CreateInvoice.BatchCreate(LoginInfo.ConvertTo(LoginInfo))
        Try

            result = _Batch.ChkAuthority(Mid)

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            Return result
        Finally
            If _Batch IsNot Nothing Then
                _Batch.Dispose()
                _Batch = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function QueryExceptInvDetail(ByVal LoginInfo As LoginInfo, ByVal SEQ As String, ByVal DataType As Integer) As RIAResult
        _Batch = New CableSoft.INV.BLL.CreateInvoice.BatchCreate(LoginInfo.ConvertTo(LoginInfo))
        Try


            Using dsResult As DataSet = _Batch.QueryExceptInvDetail(SEQ, DataType)

                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsResult, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using



        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Batch IsNot Nothing Then
                _Batch.Dispose()
                _Batch = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function Execute(ByVal LoginInfo As LoginInfo, ByVal ds As String) As RIAResult
        _Batch = New CableSoft.INV.BLL.CreateInvoice.BatchCreate(LoginInfo.ConvertTo(LoginInfo))
        Try

            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(ds)
                result = _Batch.Execute(objDS)
                If result.ResultDataSet IsNot Nothing Then
                    result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(result.ResultDataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                End If
                'result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsResult, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                'result.ResultBoolean = True
            End Using



        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Batch IsNot Nothing Then
                _Batch.Dispose()
                _Batch = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function QueryExceptInvInfo(ByVal LoginInfo As LoginInfo, ByVal ds As String, ByVal DataType As Integer) As RIAResult
        _Batch = New CableSoft.INV.BLL.CreateInvoice.BatchCreate(LoginInfo.ConvertTo(LoginInfo))
        Try

            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(ds)
                Using dsResult As DataSet = _Batch.QueryExceptInvInfo(objDS, DataType)

                    result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsResult, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                    'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                    result.ResultBoolean = True
                End Using

            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Batch IsNot Nothing Then
                _Batch.Dispose()
                _Batch = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function QueryCanCreateInv(ByVal LoginInfo As LoginInfo, ByVal ds As String) As RIAResult
        _Batch = New CableSoft.INV.BLL.CreateInvoice.BatchCreate(LoginInfo.ConvertTo(LoginInfo))
        Try

            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(ds)
                Using dsResult As DataSet = _Batch.QueryCanCreateInv(objDS)

                    result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsResult, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                    'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                    result.ResultBoolean = True
                End Using

            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Batch IsNot Nothing Then
                _Batch.Dispose()
                _Batch = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function QueryBatchAllData(ByVal LoginInfo As LoginInfo) As RIAResult
        _Batch = New CableSoft.INV.BLL.CreateInvoice.BatchCreate(LoginInfo.ConvertTo(LoginInfo))
        Try
            Using ds As DataSet = _Batch.QueryAllData
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Batch IsNot Nothing Then
                _Batch.Dispose()
                _Batch = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function Save(ByVal LoginInfo As LoginInfo, ByVal editMode As EditMode, ByVal ds As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(ds)
                result = _Invoice.Save(editMode, objDS)

            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Invoice IsNot Nothing Then
                _Invoice.Dispose()
                _Invoice = Nothing
            End If

        End Try
        Return result

    End Function
    Public Function QuerySOCustByQuery(ByVal LoginInfo As LoginInfo,
                                       ByVal QueryIndex As Integer, ByVal QueryText As String, ByVal RefNo As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _Invoice.QuerySOCustByQuery(QueryIndex, QueryText, RefNo)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Invoice IsNot Nothing Then
                _Invoice.Dispose()
                _Invoice = Nothing
            End If

        End Try
        Return result
    End Function
    Public Function QueryEditData(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _Invoice.QueryEditData
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Invoice IsNot Nothing Then
                _Invoice.Dispose()
                _Invoice = Nothing
            End If

        End Try
        Return result

    End Function
    Public Function QueryINV099(ByVal LoginInfo As LoginInfo,
                                ByVal InvDate As Date) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _Invoice.QueryINV099(InvDate)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Invoice IsNot Nothing Then
                _Invoice.Dispose()
                _Invoice = Nothing
            End If

        End Try
        Return result

    End Function
    Public Function QuerySOBill(ByVal LoginInfo As LoginInfo, ByVal existsBillNo As String,
                                ByVal custid As String, ByVal invseqno As String, ByVal guino As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _Invoice.QuerySOBill(existsBillNo, custid, invseqno, guino)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Invoice IsNot Nothing Then
                _Invoice.Dispose()
                _Invoice = Nothing
            End If

        End Try
        Return result
    End Function
    Public Function QuerySOCustId(ByVal LoginInfo As LoginInfo,
                                  ByVal CustId As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _Invoice.QuerySOCustId(CustId)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Invoice IsNot Nothing Then
                _Invoice.Dispose()
                _Invoice = Nothing
            End If

        End Try
        Return result
    End Function
    Public Function QuerySOCustInfo(ByVal LoginInfo As LoginInfo,
                                  ByVal CustId As String, ByVal existsBill As String) As RIAResult

        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _Invoice.QuerySOCustInfo(CustId, existsBill)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Invoice IsNot Nothing Then
                _Invoice.Dispose()
                _Invoice = Nothing
            End If

        End Try
        Return result



    End Function
    Public Function QueryData(ByVal LoginInfo As LoginInfo,
                                  ByVal EditMode As CableSoft.BLL.Utility.EditMode,
                                  ByVal InvId As String) As RIAResult

        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _Invoice.QueryAllData(EditMode, InvId)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Invoice IsNot Nothing Then
                _Invoice.Dispose()
                _Invoice = Nothing
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

