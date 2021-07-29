
Option Compare Binary
Option Infer On
'Option Strict On
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
Public Class Account
    'Inherits DomainService
    Implements IDisposable
    'Private _Account As CableSoft.SO.BLL.Customer.Account.Account
    Private _Account As CableSoft.SO.BLL.Customer.Account.Account = Nothing
    Private result As New RIAResult()
    Public Property isHTML As Boolean = False
    Private Sub InitClass(ByVal LoginInfo As LoginInfo)
        '_BLL = Utility.DynamicCreateClass("CableSoft.SO.BLL.Order.dll", "CableSoft.SO.BLL.Order.Order", New Object() {LoginInfo.ConvertTo(LoginInfo)}, LoginInfo.HttpPath)

        _Account = New CableSoft.SO.BLL.Customer.Account.Account(LoginInfo.ConvertTo(LoginInfo))

        '_Account = Utility.DynamicCreateClass("CableSoft.SO.BLL.Customer.Account.dll", "CableSoft.SO.BLL.Customer.Account.Account", New Object() {LoginInfo.ConvertTo(LoginInfo)}, LoginInfo.HttpPath)
    End Sub
    Public Function QueryAccountDetail(ByVal LoginInfo As LoginInfo,
                                       ByVal MasterId As Int32) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.QueryAccountDetail(MasterId)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Account IsNot Nothing Then
                _Account.Dispose()
                _Account = Nothing
            End If

        End Try
        Return result
    End Function
    Public Function GetNewCanChooseProdutWithACH(ByVal LoginInfo As LoginInfo, ByVal SEQNO As Integer,
                                                 ByVal ACHTNO As String, ByVal ACHTDESC As String) As RIAResult
        Try

            InitClass(LoginInfo)
            Using ds As DataSet = _Account.GetNewCanChooseProdutWithACH(SEQNO, ACHTNO, ACHTDESC)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Account IsNot Nothing Then
                _Account.Dispose()
                _Account = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function GetCanChooseCharge(ByVal LoginInfo As LoginInfo, ByVal CustId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.GetCanChooseCharge(CustId)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function
    Public Function QueryReLoadData(ByVal LoginInfo As LoginInfo,
                                    ByVal MasterId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _Account.QueryReLoadData(MasterId)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Account IsNot Nothing Then
                _Account.Dispose()
                _Account = Nothing
            End If

        End Try
        Return result
    End Function
    Public Function QueryNewAllData(ByVal LoginInfo As LoginInfo,
                                ByVal MasterId As Integer, ByVal SEQNO As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _Account.QueryNewAllData(MasterId, SEQNO)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Account IsNot Nothing Then
                _Account.Dispose()
                _Account = Nothing
            End If

        End Try
        Return result
    End Function
    Public Function QueryAllData(ByVal LoginInfo As LoginInfo,
                                 ByVal MasterId As Integer, ByVal CustId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using ds As DataSet = _Account.QueryAllData(MasterId, CustId)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(ds, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(ds)
                result.ResultBoolean = True
            End Using
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            If _Account IsNot Nothing Then
                _Account.Dispose()
                _Account = Nothing
            End If

        End Try
        Return result
    End Function

    Public Function QueryAccount(ByVal LoginInfo As LoginInfo,
                                        ByVal MasterId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.QueryAccount(MasterId)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function
    Public Function QueryChooseProduct(ByVal LoginInfo As LoginInfo,
                                    ByVal CustId As Integer,
                                    ByVal MasterId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.QueryChooseProduct(CustId, MasterId)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function

    Public Function QueryChooseFaci(ByVal LoginInfo As LoginInfo,
                                    ByVal CustId As Integer,
                                    ByVal MasterId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.QueryChooseFaci(CustId, MasterId)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function
    Public Function GetBankCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.GetBankCode
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function
    'Public Function GetIntroId(ByVal LoginInfo As LoginInfo,
    '                           ByVal MediaRefNo As Integer, ByVal IntroId As String) As RIAResult
    '    Try
    '        InitClass(LoginInfo)
    '        Dim dt As DataTable = _Account.GetIntroId(MediaRefNo, IntroId)
    '        result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
    '        result.ResultBoolean = True
    '    Catch ex As Exception
    '        ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
    '    End Try
    '    Return result
    'End Function
    'Public Function GetIntroData(ByVal LoginInfo As LoginInfo,
    '                             ByVal MediaRefNo As Integer, ByVal aWhere As String) As RIAResult
    '    Try
    '        InitClass(LoginInfo)
    '        Dim dt As DataTable = _Account.GetIntroData(MediaRefNo, aWhere)
    '        result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
    '        result.ResultBoolean = True
    '    Catch ex As Exception
    '        ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
    '    End Try
    '    Return result
    'End Function

    Public Function GetACHTNo(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.GetACHTNo()
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function
    Public Function GetVirtualAccount(ByVal LoginInfo As LoginInfo,
                                      ByVal CustId As Integer,
                                      ByVal BankCode As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Return _Account.GetVirtualAccount(CustId, BankCode)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function

    Public Function GetMediaCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.GetMediaCode
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function
    Public Function GetCardCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.GetCardCode
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function
    Public Function GetAcceptName(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)

            Using dt As DataTable = _Account.GetAcceptName
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using


        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function
    Public Function GetPTCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.GetPTCode
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function
    Public Function GetCMCode(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.GetCMCode
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function
    Public Function GetCanChooseProduct(ByVal LoginInfo As LoginInfo, ByVal CustId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.GetCanChooseProduct(CustId)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result

    End Function
    Public Function GetCanChooseFaci(ByVal LoginInfo As LoginInfo, ByVal CustId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.GetCanChooseFaci(CustId)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function
    Public Function CanAppend(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Return _Account.CanAppend
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
    End Function
    Public Function CanDelete(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Return _Account.CanDelete
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
    End Function
    Public Function CanPrint(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Return _Account.CanPrint
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
    End Function
    Public Function CanView(ByVal LoginInfo As LoginInfo) As RIAResult
        Try
            InitClass(LoginInfo)
            Return _Account.CanView
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
    End Function


    Public Function CanEdit(ByVal LoginInfo As LoginInfo) As RIAResult

        Try
            InitClass(LoginInfo)
            Return _Account.CanEdit
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try

    End Function
    Public Function IsACHBank(ByVal LoginInfo As LoginInfo, ByVal BankCode As Int32) As RIAResult
        Try
            InitClass(LoginInfo)
            Return _Account.IsACHBank(BankCode.ToString)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
    End Function

    Public Function GetPriv(ByVal LoginInfo As LoginInfo) As RIAResult

        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.GetPriv
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using

            'Return RIAResult.Convert(_Account.GetPriv)
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        Return result
    End Function
    ''' <summary>
    ''' 作廢
    ''' </summary>
    ''' <param name="LoginInfo">使用者資訊</param>
    ''' <param name="dsAccount">Account: 帳號資訊、ChooseFaci: 指定的設備</param>
    ''' <returns>True or false</returns>
    ''' <remarks></remarks>
    Public Function VoidData(ByVal LoginInfo As LoginInfo,
                             ByVal dsAccount As String) As RIAResult
        Try

            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsAccount)
                Return _Account.VoidData(objDS)
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
    End Function
    Public Function SaveNewData(ByVal LoginInfo As LoginInfo,
                         ByVal EditMode As CableSoft.BLL.Utility.EditMode, ByVal dsAccount As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsAccount)
                Using dsResult As DataSet = _Account.SaveNewData(EditMode, objDS)
                    'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dsResult)
                    result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsResult, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                    result.ResultBoolean = True
                    result.ErrorCode = 0
                End Using
                '    Return RIAResult.Convert(_Account.Save(EditMode, objDS))
            End Using
            Return result
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
            _Account = Nothing
        End Try
    End Function
    Public Function SaveData(ByVal LoginInfo As LoginInfo,
                         ByVal EditMode As CableSoft.BLL.Utility.EditMode, ByVal dsAccount As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsAccount)
                Using dsResult As DataSet = _Account.SaveData(EditMode, objDS)
                    'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dsResult)
                    result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dsResult, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                    result.ResultBoolean = True
                    result.ErrorCode = 0
                End Using
                '    Return RIAResult.Convert(_Account.Save(EditMode, objDS))
            End Using
            Return result
        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
            _Account = Nothing
        End Try
    End Function
    Public Function SAVE(ByVal LoginInfo As LoginInfo,
                         ByVal EditMode As CableSoft.BLL.Utility.EditMode, ByVal dsAccount As String) As RIAResult
        Try

            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsAccount)
                Return _Account.Save(EditMode, objDS)
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
    End Function

    Public Function ChkDataOk(ByVal LoginInfo As LoginInfo,
                              ByVal EditMode As CableSoft.BLL.Utility.EditMode,
                                  ByVal dsAccount As String) As RIAResult
        Try

            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsAccount)
                Return _Account.ChkDataOk(EditMode, objDS)
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
            Return result
        Finally
            _Account.Dispose()
        End Try
        'Return result
    End Function

    Public Function ChkCMDataOk(ByVal LoginInfo As LoginInfo,
                                ByVal dsAccount As String) As RIAResult
        Try
            InitClass(LoginInfo)
            Using objDS As DataSet = Silverlight.DataSetConnector.Connector.FromXml(dsAccount)
                Return _Account.ChkCMDataOk(objDS)
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
        Finally
            _Account.Dispose()
        End Try
    End Function



    Public Function GetProposer(ByVal LoginInfo As LoginInfo, ByVal CustId As Integer) As RIAResult
        Try
            InitClass(LoginInfo)
            Using dt As DataTable = _Account.GetProposer(CustId)
                'result.ResultXML = Silverlight.DataSetConnector.Connector.ToXml(dt.DataSet)
                result.ResultXML = CableSoft.BLL.Utility.JsonServer.ToJson(dt.DataSet, JsonServer.JsonFormatting.None, JsonServer.NullValueHandling.Ignore, True, True, isHTML)
                result.ResultBoolean = True
            End Using

        Catch ex As Exception
            ErrorHandle.BuildMessage(result, ex, LoginInfo.DebugMode)
            result.ResultBoolean = False
        Finally
            _Account.Dispose()
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

