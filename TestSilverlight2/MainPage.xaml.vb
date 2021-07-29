Imports DevExpress.Xpf.Core
Imports Silverlight.DataSet

Imports System.Reflection
'Imports CableSoft.SO.RIA.Facility.ChooseFaci

Imports CableSoft.SL.Utility
Imports CableSoft.SL.Window
'Imports CableSoft.SO.RIA.Facility.IDData.Web
'Imports CableSoft.SO.RIA.Facility.DVRData.Web
'Imports TestSilverlight.CableSoft.SO.RIA.Wip.Maintain

'Imports CableSoft.SO.RIA.Facility.DVRData
'Imports System.Windows.Forms
Imports System.Globalization
Imports System.IO.IsolatedStorage
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Printing.Native
Imports System.ServiceModel.DomainServices.Client
Imports TestSilverlight2.CableSoft.BLL.Utility

Partial Public Class MainPage
    Inherits UserControl
    Private FLoginInfo As Object
    Private tbTest As Silverlight.DataTable = Nothing
    Private dsTest As Silverlight.DataSet = Nothing
    Private ctlSO1112A As Global.CableSoft.SO.SL.Wip.Maintain.SO1112A = Nothing
    Public Sub New()
        FLoginInfo = App.LoginInfoWeb
        InitializeComponent()
    End Sub
    Private Function GetEditModeValue() As Int32
        Dim aRet As Int32 = 0
        If lstEditMode.SelectedIndex >= 0 Then
            aRet = Int32.Parse([Enum].Parse(GetType(IParams.EditModes), lstEditMode.SelectedValue, False))
        End If
        Return aRet
    End Function

    Private Sub btnSO1112A_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSO1112A.Click

        If ctlSO1112A Is Nothing Then
            ctlSO1112A = New Global.CableSoft.SO.SL.Wip.Maintain.SO1112A()
        End If
        Dim ds As New Silverlight.DataSet()
        Dim tb As New Silverlight.DataTable("Maintain")
        Dim tbContact As New Silverlight.DataTable("Contact1")
        tbContact.Columns.Add(New Silverlight.DataColumn("FaciSeqNo", GetType(String)))
        tbContact.Columns.Add(New Silverlight.DataColumn("ServiceType", GetType(String)))
        Dim rwContact As Silverlight.DataRow = tbContact.NewRow
        'rwContact.Item("FaciSeqNo") = "201201160121640"
        'rwContact.Item("FaciSeqNo") = "201201160121640"
        rwContact.Item("ServiceType") = "D"
        tbContact.Rows.Add(rwContact)
        tb.Columns.Add(New Silverlight.DataColumn("SNO"))
        tb.Columns.Add(New Silverlight.DataColumn("CustId"))
        'tb.Columns.Add(New Silverlight.DataColumn("ServiceType"))
        'tb.Columns.Add(New Silverlight.DataColumn("FACISEQNO"))
        'tb.Columns.Add(New Silverlight.DataColumn("ServiceType"))
        Dim dr As Silverlight.DataRow = tb.NewRow
        ''159, "200609190189257"
        'dr.Item("SNO") = "200401MD0270558"
        'dr.Item("SNO") = "201203MD0319654"
        'dr.Item("SNO") = "201203MD0319658"
        'dr.Item("CustId") = 385713
        dr.Item("SNO") = "201202MI0045407"
        dr.Item("CustId") = 693320
        'dr.Item("SNO") = "201205MI0045445"
        'dr.Item("CustId") = 649610
        'dr.Item("CustId") = 1050
        'dr.Item("ServiceType") = "D"
        ''dr.Item("FACISEQNO") = "200609190189257"
        'dr.Item("ServiceType") = "I"
        tb.Rows.Add(dr)

        ds.Tables.Add(tb)
        ds.Tables.Add(tbContact)
        'Dim tbContact As New Silverlight.DataTable("Contact")
        'tbContact.Columns.Add(New Silverlight.DataColumn("FaciSeqNo"))
        'Dim rwContact As Silverlight.DataRow = tbContact.NewRow
        'rwContact.Item("FaciSeqNo") = "XXXXX"
        'tbContact.Rows.Add(rwContact)
        'ds.Tables.Add(tbContact)

        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        aCollect.Add("Data".ToUpper, ds)
        ''Dim dtx As Silverlight.DataTable = CType(Parameters("DATA"), Silverlight.DataSet).Tables("ThiedDiscount")
        ''Dim CustId As Integer = dtx.Rows(0).Item("CustId")
        ctlSO1112A.LoginInfo = FLoginInfo
        ctlSO1112A.Parameters = aCollect
        ctlSO1112A.BatchFin = True
        ctlSO1112A.FinMode = True
        ctlSO1112A.IsAutoClosed = False

        ctlSO1112A.EditMode = GetEditModeValue()
        ShowChildWindow(ctlSO1112A, New Size(900, 500))

        'ctlSO1112A.CanAppend(ds, Sub(ariaResult As Boolean, aRiaXml As String)
        '                             If ariaResult Then
        '                                 'MessageBox.Show("yes")
        '                                 ShowChildWindow(ctlSO1112A, New Size(900, 500))
        '                             Else
        '                                 MessageBox.Show(aRiaXml)
        '                             End If
        '                         End Sub)

    End Sub
    Private Sub ShowChildWindow(ByVal aChild As Object, ByVal aSize As Size)
        Dim objShowWindow As New CSWindow()
        AddHandler objShowWindow.Closed, Sub(s, arg)
                                             If TypeOf aChild Is Global.CableSoft.SO.SL.Wip.Maintain.SO1112A Then
                                                 CType(aChild, Global.CableSoft.SO.SL.Wip.Maintain.SO1112A).Parameters.Clear()
                                                 If CType(aChild, Global.CableSoft.SO.SL.Wip.Maintain.SO1112A).Parameters IsNot Nothing Then
                                                     CType(aChild, Global.CableSoft.SO.SL.Wip.Maintain.SO1112A).Parameters = Nothing
                                                 End If
                                                 aChild.Dispose()
                                                 If ctlSO1112A IsNot Nothing Then

                                                     ctlSO1112A.Dispose()
                                                     ctlSO1112A = Nothing
                                                 End If
                                             End If
                                             If TypeOf aChild Is Global.CableSoft.SL.Common.Dynamic.DynUpdateGrid.DynUpdateGrid Then
                                                 If CType(aChild, Global.CableSoft.SL.Common.Dynamic.DynUpdateGrid.DynUpdateGrid).Parameters IsNot Nothing Then
                                                     CType(aChild, Global.CableSoft.SL.Common.Dynamic.DynUpdateGrid.DynUpdateGrid).Parameters.Clear()
                                                 End If

                                             End If
                                             If TypeOf aChild Is Global.CableSoft.SO.SL.CodeSet.MFCode.MFCode Then
                                                 aChild.Dispose()
                                             End If
                                             'If TypeOf aChild Is Global.CableSoft.SO.SL.Facility.ChangeFaci.SO111XD Then
                                             '    'Dim ctl As Global.CableSoft.SO.SL.Facility.ChangeFaci.SO111XD = CType(aChild, Global.CableSoft.SO.SL.Facility.ChangeFaci.SO111XD)
                                             '    'For Each rw As Silverlight.DataRow In ctl.WipData.Tables("ChangeFacility").Rows
                                             '    '    MessageBox.Show("ChooseServiceID:" & rw.Item("ChooseServiceID"))
                                             '    '    MessageBox.Show("Delete003Citem:" & rw.Item("Delete003Citem"))
                                             '    'Next
                                             'End If
                                             'If TypeOf aChild Is Global.CableSoft.SO.SL.Customer.Account.SO1100D Then
                                             '    'Dim ctl As Global.CableSoft.SO.SL.Customer.Account.SO1100D = CType(aChild, Global.CableSoft.SO.SL.Customer.Account.SO1100D)
                                             '    'If ctl.IsSaved Then
                                             '    '    MessageBox.Show("Yes")
                                             '    'Else
                                             '    '    MessageBox.Show("NO")
                                             '    'End If

                                             'End If
                                             'If TypeOf aChild Is Global.CableSoft.SO.SL.Order.ChooseNextPeriod.SO1144N Then
                                             '    Dim dt As Silverlight.DataTable = CType(aChild, Global.CableSoft.SO.SL.Order.ChooseNextPeriod.SO1144N).Charge
                                             '    If dt IsNot Nothing Then
                                             '        'MessageBox.Show(dt.Rows.Count)
                                             '    End If
                                             '    'Dim ctl As Global.CableSoft.SO.SL.Customer.Account.SO1100D = CType(aChild, Global.CableSoft.SO.SL.Customer.Account.SO1100D)
                                             '    'If ctl.IsSaved Then
                                             '    '    MessageBox.Show("Yes")
                                             '    'Else
                                             '    '    MessageBox.Show("NO")
                                             '    'End If

                                             'End If
                                             'Dim obj As Silverlight.DataRow = CType(aChild, CableSoft.SO.SL.Facility.ChooseFaci.SO1131E).FaciData
                                             'MessageBox.Show(obj.Item("FaciSeqNo"))
                                             'MessageBox.Show(aChild.FaciSeqNo)
                                             'MessageBox.Show(aChild.FaciSNO)
                                         End Sub
        'Dim objShowWindow As New DXWindow()
        objShowWindow.FontSize = 12
        objShowWindow.HorizontalContentAlignment = Windows.HorizontalAlignment.Stretch
        objShowWindow.VerticalContentAlignment = Windows.VerticalAlignment.Stretch
        objShowWindow.Width = aSize.Width
        objShowWindow.Height = aSize.Height
        'objShowWindow.Width = 900
        objShowWindow.Title = aChild.Name
        'aChild.Refresh()
        'objShowWindow.Content = aChild
        'objShowWindow.Show()
        objShowWindow.Show(aChild, LayoutRoot)


    End Sub

    Private Sub MainPage_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        ThemeManager.ApplicationThemeName = Theme.Office2007BlueName
        'For Each Str As String In [Enum].GetNames(GetType(IParams.EditModes))
        '    lstEditMode.Items.Add(Str)
        'Next
        lstEditMode.ItemsSource = [Enum].GetNames(GetType(IParams.EditModes))
        lstEditMode.SelectedValue = [Enum].GetName(GetType(IParams.EditModes), IParams.EditModes.Edit)
    End Sub

    Private Sub btnSO1131C_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSO1131C.Click
        Dim objControl As New Global.CableSoft.SO.SL.Billing.LevelDiscount.ThirdDiscount.SO1131C
        Dim ds As New Silverlight.DataSet()
        Dim tb As New Silverlight.DataTable("ThirdDiscount")
        tb.Columns.Add(New Silverlight.DataColumn("CUSTID"))
        tb.Columns.Add(New Silverlight.DataColumn("FACISEQNO"))
        Dim dr As Silverlight.DataRow = tb.NewRow
        '159, "200609190189257"
        dr.Item("CUSTID") = 671239
        dr.Item("FACISEQNO") = "201001260064558"
        tb.Rows.Add(dr)
        ds.Tables.Add(tb)


        'Dim dv As New DataView(ds.Tables(0))
        'dv.RowStateFilter = DataViewRowState.ModifiedOriginaldataview
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        aCollect.Add("Data".ToUpper, ds)
        'Dim dtx As Silverlight.DataTable = CType(Parameters("DATA"), Silverlight.DataSet).Tables("ThiedDiscount")
        'Dim CustId As Integer = dtx.Rows(0).Item("CustId")
        objControl.LoginInfo = FLoginInfo
        objControl.Parameters = aCollect
        objControl.EditMode = GetEditModeValue()


        ShowChildWindow(objControl, New Size(900, 500))
    End Sub

    Private Sub btnChooseFaci_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnChooseFaci.Click
        Dim objControl As New Global.CableSoft.SO.SL.Facility.ChooseFaci.SO1131E
        Dim ds As New Silverlight.DataSet()
        Dim tb As New Silverlight.DataTable("ChooseFaci")

        'Dim WipData As New Silverlight.DataSet
        'WipData.FromXml(TextBox1.Text)
        'WipData.Tables(0).TableName = "ChooseFacility"
        'Dim rwWip As Silverlight.DataRow = WipData.Tables(0).NewRow
        'With rwWip
        '    .Item("CUSTID") = "600131"
        '    .Item("SNO") = "201207I10110480"
        '    .Item("FACICODE") = "234"

        '    .Item("FACINAME") = "VOIP門號"
        '    .Item("BUYCODE") = "4"
        '    .Item("BUYNAME") = "借"
        '    .Item("AMOUNT") = "0"
        '    '<QUANTITY>1</QUANTITY>
        '    '<UNITPRICE>0</UNITPRICE>
        '    '<UPDTIME>100/02/13 12:46:57</UPDTIME>
        '    '<UPDEN>CABLESOFT</UPDEN>
        '    '<SERVICETYPE>P</SERVICETYPE>
        '    '<COMPCODE>3</COMPCODE>
        '    '<SEQNO>201207160216202</SEQNO>
        '    '<ID>缺ID0600131</ID>
        '    '<CONTTEL>461067</CONTTEL>
        '    '<DOMICILEADDRESS>竹南鎮博愛街136號2樓</DOMICILEADDRESS>
        '    '<DECLARANTNAME>陳鳳琴</DECLARANTNAME>
        '    '<COMPANY>0</COMPANY>
        '    '<MARRIED>0</MARRIED>
        '    '<IDKINDCODE1>9</IDKINDCODE1>
        '    '<IDKINDNAME1>缺ID</IDKINDNAME1>
        '    '<SEXTUAL>1</SEXTUAL>    
        'End With
        'WipData.Tables(0).Rows.Add(rwWip)
        'objControl.WipData = WipData
        tb.Columns.Add(New Silverlight.DataColumn("CUSTID"))
        'tb.Columns.Add(New Silverlight.DataColumn("FACISEQNO"))
        tb.Columns.Add(New Silverlight.DataColumn("ServiceType"))
        Dim dr As Silverlight.DataRow = tb.NewRow
        '159, "200609190189257"
        dr.Item("CUSTID") = 617414
        'dr.Item("FACISEQNO") = "200609190189257"
        dr.Item("ServiceType") = "I"
        tb.Rows.Add(dr)
        ds.Tables.Add(tb)
        objControl.IncludeDVR = True
        objControl.IncludePR = True
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        aCollect.Add("Data".ToUpper, ds)
        'Dim dtx As Silverlight.DataTable = CType(Parameters("DATA"), Silverlight.DataSet).Tables("ThiedDiscount")
        'Dim CustId As Integer = dtx.Rows(0).Item("CustId")
        objControl.LoginInfo = FLoginInfo
        objControl.Parameters = aCollect
        'objControl.EditMode = 1

        ShowChildWindow(objControl, New Size(900, 500))
    End Sub

    Private Sub btnCPEMAC_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnCPEMAC.Click
        Dim objControl As New Global.CableSoft.SO.SL.Facility.CPEMAC.SO111xCEA()
        Dim aResult As InvokeOperation(Of Global.CableSoft.SO.RIA.Facility.CPEMAC.CableSoft.BLL.Utility.RIAResult) = Nothing
        Dim aRiaCPEMAC As New Global.CableSoft.SO.RIA.Facility.CPEMAC.Web.CPEMAC
        aResult = aRiaCPEMAC.GetCPEMAC(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, _
                                                                                     New Global.CableSoft.SO.RIA.Facility.CPEMAC.CableSoft.BLL.Utility.LoginInfo),
                                                       681433, "201003180066906")

        AddHandler aResult.Completed, Sub(s, arg)
                                          If s.Value IsNot Nothing Then
                                              If s.Value.ResultBoolean Then
                                                  Dim ds As Silverlight.DataSet = Global.CableSoft.SL.Utility.JsonClient.FromJson(s.value.ResultXML)
                                                  'ds.Tables.Clear()
                                                  'ds.FromXml(s.value.ResultXML)
                                                  objControl.LoginInfo = FLoginInfo
                                                  objControl.CPEMAC = ds
                                                  objControl.FixIPCount = 2
                                                  objControl.FaciSeqNo = "201003180066906"
                                                  objControl.EditMode = GetEditModeValue()
                                                  objControl.IsEdit = False
                                                  objControl.IsDelete = False
                                                  ShowChildWindow(objControl, New Size(500, 400))
                                                  'MessageBox.Show("Query OK")
                                              Else
                                                  MessageBox.Show(s.Value.ErrorMessage)
                                              End If
                                          End If
                                      End Sub
    End Sub

    Private Sub btnCommand_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnCommand.Click
        Dim WipData As New Silverlight.DataSet
        '------------------ExtraTable-----------------------------------
        Dim dtExtraTable As New Silverlight.DataTable("ExtraTable")
        dtExtraTable.Columns.Add(New Silverlight.DataColumn("UpdTime", GetType(String)))
        dtExtraTable.Columns.Add(New Silverlight.DataColumn("CMDID", GetType(String)))
        dtExtraTable.Columns.Add(New Silverlight.DataColumn("CMD_STATUS", GetType(String)))
        dtExtraTable.Columns.Add(New Silverlight.DataColumn("OPERATOR", GetType(String)))
        Dim rw As Silverlight.DataRow = dtExtraTable.NewRow
        rw.Item("UpdTime") = "101/08/21"
        rw.Item("CMDID") = "Z3"
        'rw.Item("CMD_STATUS") = "W"
        rw.Item("OPERATOR") = "TEST_INS"
        dtExtraTable.Rows.Add(rw)
        dtExtraTable.AcceptChanges()
        '--------------------------------------------------------------------------------
        '------------------Customer---------------------------------------------------
        Dim dtCustomer As New Silverlight.DataTable("Customer")
        dtCustomer.Columns.Add(New Silverlight.DataColumn("CustId", GetType(Int32)))
        dtCustomer.Columns.Add(New Silverlight.DataColumn("PINCODE", GetType(String)))
        Dim rwCustomer As Silverlight.DataRow = dtCustomer.NewRow
        rwCustomer("CustId") = 1234
        rwCustomer("PINCODE") = "0000"
        dtCustomer.Rows.Add(rwCustomer)
        dtCustomer.AcceptChanges()
        '---------------------------------------------------------------------------------
        '-----------------Facility--------------------------------------------------------
        Dim dtFacility As New Silverlight.DataTable("Facility")
        dtFacility.Columns.Add(New Silverlight.DataColumn("SmartCardNo", GetType(String)))
        dtFacility.Columns.Add(New Silverlight.DataColumn("STBSNo", GetType(String)))
        Dim rwFacility As Silverlight.DataRow = dtFacility.NewRow
        rwFacility("SmartCardNo") = "1057030146"
        rwFacility("STBSNo") = "65536"
        dtFacility.Rows.Add(rwFacility)
        dtFacility.AcceptChanges()
        '--------------------------------------------------------------------------------------
        '----------------Detail----------------------------------------------------------------
        Dim dtDetail As New Silverlight.DataTable("Detail")
        dtDetail.Columns.Add(New Silverlight.DataColumn("ProductId", GetType(String)))
        dtDetail.Columns.Add(New Silverlight.DataColumn("ProductName", GetType(String)))
        dtDetail.Columns.Add(New Silverlight.DataColumn("CitemCode", GetType(String)))
        Dim rwDetail As Silverlight.DataRow = dtDetail.NewRow
        rwDetail("ProductId") = "111"
        rwDetail("ProductName") = "222222"
        rwDetail("CitemCode") = "123"
        dtDetail.Rows.Add(rwDetail)
        dtDetail.AcceptChanges()

        '--------------------------------------------------------------------------------------

        WipData.Tables.Add(dtCustomer)
        WipData.Tables.Add(dtExtraTable)
        WipData.Tables.Add(dtFacility)
        WipData.Tables.Add(dtDetail)



        Dim aResult As InvokeOperation(Of Global.CableSoft.SO.RIA.Facility.Command.CableSoft.BLL.Utility.RIAResult) = Nothing
        Dim aRiaCommand As New Global.CableSoft.SO.RIA.Facility.Command.Web.Command
        Global.CableSoft.SL.Utility.Utility.SetTimeout(aRiaCommand, 600)    '設定TimeOut時間為10分鐘
        aResult = aRiaCommand.InsertCommand(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, _
                                                                                          New Global.CableSoft.SO.RIA.Facility.Command.CableSoft.BLL.Utility.LoginInfo),
                                            "SO555", "Z3", WipData.ToXml(False), False, True, True)

        AddHandler aResult.Completed, Sub(s, arg)
                                          If s.Value IsNot Nothing Then
                                              If s.Value.ResultBoolean Then
                                                  MessageBox.Show("Insert OK")
                                              Else
                                                  MessageBox.Show(String.Format("ErrorCode = {0},ErrorMessage={1}", s.Value.ErrorCode, s.Value.ErrorMessage))
                                              End If

                                              If Not String.IsNullOrEmpty(s.value.ResultXML) Then
                                                  Dim ds As New Silverlight.DataSet
                                                  ds.FromXml(s.value.ResultXML)
                                                  MessageBox.Show(ds.Tables("Master").Rows(0).Item("SeqNo").ToString)
                                                  If ds.Tables("Detail") IsNot Nothing AndAlso ds.Tables("Detail").Rows.Count > 0 Then
                                                      MessageBox.Show(ds.Tables("Detail").Rows(0).Item("CitemCode").ToString)
                                                  End If


                                              End If
                                          End If
                                      End Sub
    End Sub

    Private Sub btnSO111XD_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSO111XD.Click
        'Dim SNo As String = "'2012'"
        'Dim aMaintainResult As InvokeOperation(Of Global.CableSoft.SO.RIA.Wip.Maintain.Web.RIAResult) = Nothing

        'Dim aRiaMaintain As New Global.CableSoft.SO.RIA.Wip.Maintain.Web.Maintain()
        'Dim aQueryTableResult As InvokeOperation(Of TestSilverlight.Web.RIAResult) = Nothing
        'Dim aRiaQueryTable As New TestSilverlight.Web.QueryTable

        'aMaintainResult = aRiaMaintain.GetMaintainData(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.CableSoft.SO.RIA.Wip.Maintain.Web.LoginInfo),
        '                                               SNo)




        'AddHandler aMaintainResult.Completed, Sub(s, arg)
        '                                          If s.Value IsNot Nothing Then
        '                                              If s.Value.ResultBoolean Then
        '                                                  Dim objControl As New Global.CableSoft.SO.SL.Facility.ChangeFaci.SO111XD
        '                                                  Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)

        '                                                  'Dim ds As New Silverlight.DataSet
        '                                                  'ds.Tables.Clear()
        '                                                  'ds.FromXml(s.Value.ResultXML)


        '                                                  Dim ds As New Silverlight.DataSet
        '                                                  ds.Tables.Clear()
        '                                                  ds.FromXml(TextBox1.Text)



        '                                                  Dim rwNew As Silverlight.DataRow = ds.Tables("Wip").NewRow
        '                                                  rwNew.Item("CUSTID") = "681526"
        '                                                  rwNew.Item("SNO") = "201208P14501610"
        '                                                  With rwNew
        '                                                      .Item("OLDADDRNO") = "1089105"
        '                                                      .Item("OLDADDRESS") = "竹南鎮環市路2段38號"
        '                                                      .Item("PRCODE") = "215"
        '                                                      .Item("PRNAME") = "CM 關機"
        '                                                      .Item("RESVTIME") = Date.Now.ToString("yyyy/MM/dd HH:mm:ss")
        '                                                      .Item("SERVCODE") = "103"
        '                                                      .Item("STRTCODE") = "42181"
        '                                                      .Item("COMPCODE") = "3"
        '                                                      .Item("SERVICETYPE") = "I"
        '                                                      .Item("WORKSERVCODE") = "103"
        '                                                  End With
        '                                                  ds.Tables("Wip").Rows.Add(rwNew)

        '                                                  aCollect.Add("Data".ToUpper, ds)
        '                                                  objControl.LoginInfo = FLoginInfo
        '                                                  objControl.Parameters = aCollect
        '                                                  objControl.CustId = 681526
        '                                                  objControl.SNo = "201208P14501610"
        '                                                  objControl.ServiceType = "I"
        '                                                  objControl.WipData = ds
        '                                                  objControl.EditMode = GetEditModeValue()
        '                                                  objControl.CanDo = True
        '                                                  objControl.WipRefNo = 7
        '                                                  objControl.PrivCancelGetFaci = True
        '                                                  objControl.CanCancel = True
        '                                                  objControl.WipType = 3

        '                                                  ShowChildWindow(objControl, New Size(900, 500))
        '                                              Else
        '                                                  MessageBox.Show(s.Value.ErrorMessage)
        '                                              End If
        '                                          End If
        '                                      End Sub


    End Sub

    Private Sub btnEnterPay_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnEnterPay.Click
        Dim objControl As New Global.CableSoft.SO.SL.Billing.Batch.EnterPay.SO3311A
        objControl.LoginInfo = FLoginInfo
        'ShowChildWindow(objControl, New Size(500, 500))
        ShowChildWindow(objControl, New Size(900, 700))

    End Sub

    Private Sub btnCopyOrder_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnCopyOrder.Click
        Dim objControl As New Global.CableSoft.SO.SL.Order.CopyOrder.SO1144J
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        Dim ds As New Silverlight.DataSet
        Dim dtCopyOrder As New Silverlight.DataTable("CopyOrder")
        dtCopyOrder.Columns.Add(New Silverlight.DataColumn("OrderNo"))
        Dim rw As Silverlight.DataRow = dtCopyOrder.NewRow
        rw.Item("OrderNo") = "201307180048275"
        dtCopyOrder.Rows.Add(rw)

        ds.Tables.Add(dtCopyOrder)
        Dim col As New Dictionary(Of String, Object)
        col.Add("Data".ToUpper, ds)
        objControl.EditMode = GetEditModeValue()
        objControl.LoginInfo = FLoginInfo
        objControl.Parameters = col


        ShowChildWindow(objControl, New Size(900, 800))
        'aCollect.Add("Data".ToUpper, ds)
        'objControl.Parameters = aCollect
        'objControl.CanEdit(ds, Sub(bln As Boolean, str As String)
        '                           If bln Then
        '                               objControl.OrderNo = "201301080047479"

        '                               ShowChildWindow(objControl, New Size(500, 300))
        '                           Else
        '                               MessageBox.Show(str)
        '                           End If
        '                       End Sub)

    End Sub

    Private Sub btnChangePeriod_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnChangePeriod.Click
        Dim aResult As InvokeOperation(Of CableSoft.BLL.Utility.RIAResult) = Nothing
        Dim aRiaQuerySO105B As New Global.TestSilverlight2.Web.QueryTable()
        aResult = aRiaQuerySO105B.GetSO105B(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New LoginInfo))
        AddHandler aResult.Completed, Sub(s, arg)
                                          If s.Value IsNot Nothing Then
                                              Dim ds As New Silverlight.DataSet
                                              ds.Tables.Clear()
                                              ds.FromXml(s.value.ResultXML)
                                              ds.Tables(0).TableName = "Charge"
                                              ds.AcceptChanges()
                                              Dim dsCollect As New Silverlight.DataSet
                                              Dim dtProduct As New Silverlight.DataTable("Product")
                                              dtProduct.Columns.Add(New Silverlight.DataColumn("BPCode", GetType(String)))
                                              dtProduct.Columns.Add(New Silverlight.DataColumn("CitemCode", GetType(String)))
                                              Dim rwProduct As Silverlight.DataRow = dtProduct.NewRow
                                              rwProduct.Item("BpCode") = "5"
                                              rwProduct.Item("CitemCode") = "238"
                                              dtProduct.Rows.Add(rwProduct)
                                              dsCollect.Tables.Add(dtProduct)
                                              Dim objControl As New Global.CableSoft.SO.SL.Order.ChangePeriod.SO1144M
                                              Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
                                              aCollect.Add("Data".ToUpper, dsCollect)
                                              objControl.Parameters = aCollect
                                              objControl.EditMode = GetEditModeValue()
                                              objControl.LoginInfo = FLoginInfo
                                              objControl.Charge = ds.Tables("Charge")

                                              ShowChildWindow(objControl, New Size(500, 600))
                                          Else
                                              MessageBox.Show(s.Value.ErrorMessage)
                                          End If
                                      End Sub
    End Sub

    Private Sub btnChooseNextPeriod_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnChooseNextPeriod.Click
        Dim objControl As New Global.CableSoft.SO.SL.Order.ChooseNextPeriod.SO1144N
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        'aCollect.Add("Data".ToUpper, ds)
        'objControl.Parameters = aCollect
        Dim dtCharge As New Silverlight.DataTable("Charge")
        dtCharge.Columns.Add(New Silverlight.DataColumn("CITEMNAME", GetType(String)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("SERVICETYPE", GetType(String)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("PERIOD", GetType(Int32)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("AMOUNT", GetType(Int32)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("NEXTPERIOD", GetType(Int32)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("CITEMCODE", GetType(Int32)))
        'dtCharge.Columns.Add(New Silverlight.DataColumn("REALPERIOD", GetType(Int32)))
        'dtCharge.Columns.Add(New Silverlight.DataColumn("SHOULDAMT", GetType(Int32)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("NEXTAMT", GetType(Int32)))
        For i As Int32 = 0 To 9
            Dim rw As Silverlight.DataRow = dtCharge.NewRow
            rw.Item("CITEMNAME") = "測試用" & i.ToString
            rw.Item("SERVICETYPE") = "I"
            rw.Item("PERIOD") = i
            rw.Item("AMOUNT") = (i + 1) * 5
            rw.Item("NEXTPERIOD") = i
            rw.Item("CITEMCODE") = i
            'rw.Item("REALPERIOD") = i + 1
            'rw.Item("SHOULDAMT") = (i + 1) * 2
            dtCharge.Rows.Add(rw)
        Next
        dtCharge.AcceptChanges()
        Dim objColl As New Dictionary(Of String, Object)
        Dim ds As New Silverlight.DataSet
        ds.Tables.Add(dtCharge)
        objColl.Add("Data".ToUpper, ds)
        objControl.Parameters = objColl
        'objControl.Charge = dtCharge
        objControl.EditMode = GetEditModeValue()
        objControl.LoginInfo = FLoginInfo
        ShowChildWindow(objControl, New Size(500, 500))
    End Sub

    Private Sub btnDynamicUpdate_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnDynamicUpdate.Click
        Dim aResult As InvokeOperation(Of CableSoft.BLL.Utility.RIAResult) = Nothing
        Dim aRiaCodeNo As New Global.TestSilverlight2.Web.QueryTable()
        aResult = aRiaCodeNo.GetCD001(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New LoginInfo))
        AddHandler aResult.Completed, Sub(s, arg)
                                          If s.Value IsNot Nothing Then
                                              Dim ds As New Silverlight.DataSet
                                              ds.Tables.Clear()
                                              'ds.FromXml(s.value.ResultXML)
                                              Global.CableSoft.SL.Utility.JsonClient.FromJson(s.value.ResultXML)
                                              ds.AcceptChanges()

                                              Dim objControl As New Global.CableSoft.SL.Common.Dynamic.DynamicUpdate.DynamicUpdate
                                              Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
                                              aCollect.Add("Data".ToUpper, ds)

                                              objControl.EditMode = GetEditModeValue()
                                              If objControl.EditMode = IParams.EditModes.AddNew Then
                                                  For Each tb As Silverlight.DataTable In CType(aCollect("Data".ToUpper), Silverlight.DataSet).Tables
                                                      tb.Rows.Clear()
                                                  Next
                                              End If
                                              'objControl.ShowAllButton = False
                                              objControl.Parameters = aCollect
                                              objControl.LoginInfo = FLoginInfo
                                              objControl.SysProgramId = "CD001"
                                              objControl.ShowAllButton = True
                                              objControl.IsAutoClosed = False


                                              'objControl.Refresh()
                                              'objControl.Refresh2(Sub(bln As Boolean, Msg As String)
                                              '                        If bln Then
                                              '                            MessageBox.Show("完成")
                                              '                        Else
                                              '                            MessageBox.Show(Msg)
                                              '                        End If
                                              '                    End Sub)
                                              'objControl.IsAutoClosed = False
                                              ShowChildWindow(objControl, New Size(900, 500))
                                          Else
                                              'MessageBox.Show(s.Value.ErrorMessage)
                                          End If
                                      End Sub



        'objControl.Parameters = aCollect

        'objControl.CanEdit(Nothing, Sub(bln As Boolean, strMsg As String)
        '                                If bln Then
        '                                    MessageBox.Show("yes")
        '                                Else
        '                                    MessageBox.Show(strMsg)
        '                                End If
        '                            End Sub)
    End Sub

    Private Sub btnDynUpdateGrid_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnDynUpdateGrid.Click
        Dim aResult As InvokeOperation(Of CableSoft.BLL.Utility.RIAResult) = Nothing
        'Dim aRiaCodeNo As New Global.TestSilverlight2.Web.QueryTable()
        Dim objControl As New Global.CableSoft.SL.Common.Dynamic.DynUpdateGrid.DynUpdateGrid
        'Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        'Dim tbCD068 As New Silverlight.DataTable("CD002")
        'Dim ds As New Silverlight.DataSet
        'tbCD068.Columns.Add(New Silverlight.DataColumn("MDUID", GetType(String)))
        'tbCD068.Columns.Add(New Silverlight.DataColumn("CompCode", GetType(Integer)))
        'Dim rwNew As Silverlight.DataRow
        'rwNew = tbCD068.NewRow
        'rwNew.Item(0) = "D0030"
        'rwNew.Item(1) = 3
        'tbCD068.Rows.Add(rwNew)
        'tbCD068.AcceptChanges()

        'ds.Tables.Add(tbCD068.Copy(False))
        'tbCD068.Dispose()
        'tbCD068 = Nothing
        'aCollect.Add("DATA", ds)        
        objControl.EditMode = GetEditModeValue()
        objControl.LoginInfo = FLoginInfo
        objControl.SysProgramId = "CD001"
        'objControl.SysProgramId = "SO7400B"
        objControl.IsAutoClosed = False

        ShowChildWindow(objControl, New Size(900, 500))



    End Sub

    Private Sub btnTextOut_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnTextOut.Click
        Dim aResult As InvokeOperation(Of CableSoft.BLL.Utility.RIAResult) = Nothing
        Dim aRiaCodeNo As New Global.TestSilverlight2.Web.QueryTable()
        Dim objControl As New Global.CableSoft.SL.Common.Dynamic.TextFileOut.DynTextFileOut
        'Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        'Dim dsCollect As New Silverlight.DataSet("DATA")
        'Dim tbTemp As New Silverlight.DataTable("Temp")
        'Dim rwNew As Silverlight.DataRow = tbTemp.NewRow
        'tbTemp.Rows.Add(rwNew)
        'dsCollect.Tables.Add(tbTemp)
        'aCollect.Add("Data".ToUpper, dsCollect)
        objControl.EditMode = GetEditModeValue()
        objControl.LoginInfo = FLoginInfo
        objControl.SysProgramId = "SO3271A"
        'objControl.Parameters = aCollect
        ShowChildWindow(objControl, New Size(900, 500))
    End Sub

    Private Sub btnCutDay_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnCutDay.Click
        Dim objControl As New Global.CableSoft.SO.SL.Order.CutDate.SO1144P
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        Dim ds As New Silverlight.DataSet
        'aCollect.Add("Data".ToUpper, ds)
        'objControl.Parameters = aCollect
        Dim dtCharge As New Silverlight.DataTable("Charge")
        dtCharge.Columns.Add(New Silverlight.DataColumn("CITEMNAME", GetType(String)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("SERVICETYPE", GetType(String)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("PERIOD", GetType(Int32)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("AMOUNT", GetType(Int32)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("NEXTPERIOD", GetType(Int32)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("CITEMCODE", GetType(Int32)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("REALPERIOD", GetType(Int32)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("SHOULDAMT", GetType(Int32)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("NEXTAMT", GetType(Int32)))
        dtCharge.Columns.Add(New Silverlight.DataColumn("STOPDATE", GetType(Date)))
        For i As Int32 = 0 To 9
            Dim rw As Silverlight.DataRow = dtCharge.NewRow
            rw.Item("CITEMNAME") = "測試用" & i.ToString
            rw.Item("SERVICETYPE") = "I"
            rw.Item("PERIOD") = i + 1
            rw.Item("AMOUNT") = (i + 1) * 5
            rw.Item("NEXTPERIOD") = i + 2
            rw.Item("CITEMCODE") = i
            rw.Item("REALPERIOD") = i + 1
            rw.Item("SHOULDAMT") = (i + 1) * 2
            dtCharge.Rows.Add(rw)
        Next
        dtCharge.AcceptChanges()

        ds.Tables.Add(dtCharge)

        'Dim ds2 As New Silverlight.DataSet
        'Dim rwNew As Silverlight.DataRow = ds2.Tables("Charge").NewRow

        aCollect.Add("Data".ToUpper, ds)
        'objControl.Charge = dtCharge
        objControl.Parameters = aCollect

        objControl.CustId = 600007
        objControl.ResvTime = Now
        objControl.EditMode = GetEditModeValue()
        objControl.LoginInfo = FLoginInfo
        ShowChildWindow(objControl, New Size(500, 500))
    End Sub

    Private Sub btnOrderFacility_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnOrderFacility.Click
        Dim aResult As InvokeOperation(Of CableSoft.BLL.Utility.RIAResult) = Nothing
        Dim aRiaQuerySO105 As New Global.TestSilverlight2.Web.QueryTable()
        aResult = aRiaQuerySO105.GetSO105D(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New LoginInfo))
        AddHandler aResult.Completed, Sub(s, arg)
                                          If s.Value IsNot Nothing Then
                                              Dim ds As New Silverlight.DataSet
                                              ds.Tables.Clear()
                                              ds.FromXml(s.value.ResultXML)
                                              ds.Tables(0).TableName = "Facility"
                                              ds.AcceptChanges()
                                              Dim tbWip As New Silverlight.DataTable("Wip")
                                              tbWip.Columns.Add(New Silverlight.DataColumn("CodeNo", GetType(Integer)))
                                              Dim rwNew As Silverlight.DataRow = tbWip.NewRow
                                              rwNew("CodeNo") = 170
                                              tbWip.Rows.Add(rwNew)
                                              rwNew = tbWip.NewRow
                                              rwNew("CodeNo") = 213
                                              tbWip.Rows.Add(rwNew)
                                              ds.Tables.Add(tbWip)
                                              Dim objControl As New Global.CableSoft.SO.SL.Order.Facility.SO1144F()
                                              Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
                                              aCollect.Add("Data".ToUpper, ds)
                                              objControl.Parameters = aCollect
                                              objControl.EditMode = GetEditModeValue()

                                              objControl.LoginInfo = FLoginInfo
                                              ShowChildWindow(objControl, New Size(350, 200))

                                          Else
                                              MessageBox.Show(s.Value.ErrorMessage)
                                          End If
                                      End Sub
    End Sub

    Private Sub btnDeposit_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnDeposit.Click
        Dim aResult As InvokeOperation(Of CableSoft.BLL.Utility.RIAResult) = Nothing
        Dim aRiaDeposit As New Global.TestSilverlight2.Web.QueryTable()
        aResult = aRiaDeposit.GetDeposit(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New LoginInfo))
        AddHandler aResult.Completed, Sub(s, arg)
                                          If s.Value IsNot Nothing Then
                                              Dim ds As New Silverlight.DataSet
                                              ds.Tables.Clear()
                                              ds.FromXml(s.value.ResultXML)
                                              ds.AcceptChanges()
                                              Dim objControl As New Global.CableSoft.SO.SL.Order.Deposit.SO1144Q
                                              Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
                                              aCollect.Add("Data".ToUpper, ds)
                                              objControl.Parameters = aCollect
                                              objControl.EditMode = GetEditModeValue()
                                              objControl.LoginInfo = FLoginInfo

                                              'objControl.IsAutoClosed = False
                                              ShowChildWindow(objControl, New Size(350, 250))
                                          Else
                                              MessageBox.Show(s.Value.ErrorMessage)
                                          End If
                                      End Sub
    End Sub

    Private Sub btnchangeClctEn_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnchangeClctEn.Click
        Dim objControl As New Global.CableSoft.SO.SL.Billing.ChangeClctEn.ChangeClctEn
        'Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        'aCollect.Add("Data".ToUpper, ds)
        ''Dim dtx As Silverlight.DataTable = CType(Parameters("DATA"), Silverlight.DataSet).Tables("ThiedDiscount")
        ''Dim CustId As Integer = dtx.Rows(0).Item("CustId")
        'objControl.LoginInfo = FLoginInfo
        'objControl.Parameters = aCollect
        'objControl.EditMode = GetEditModeValue()
        FLoginInfo.GroupId = "0"
        objControl.LoginInfo = FLoginInfo
        objControl.RefNo = 1
        objControl.EditMode = IParams.EditModes.View
        'CType(FLoginInfo,CableSoft.RIA.Dynamic.DynamicUpdate.Web.LoginInfo).GroupId
        ShowChildWindow(objControl, New Size(650, 500))
    End Sub

    Private Sub btnSO1100G_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSO1100G.Click
        Dim objControl As New Global.CableSoft.SO.SL.Customer.Account.SO1100D

        Dim ds As New Silverlight.DataSet()
        Dim tb As New Silverlight.DataTable("Account")

        tb.Columns.Add(New Silverlight.DataColumn("MasterId"))
        tb.Columns.Add(New Silverlight.DataColumn("CustId"))
        'tb.Columns.Add(New Silverlight.DataColumn("FACISEQNO"))
        'tb.Columns.Add(New Silverlight.DataColumn("ServiceType"))
        Dim dr As Silverlight.DataRow = tb.NewRow
        ''159, "200609190189257"
        dr.Item("MasterId") = 56982
        'dr.Item("MasterId") = 56585
        dr.Item("CustId") = 600028
        ''dr.Item("FACISEQNO") = "200609190189257"
        'dr.Item("ServiceType") = "I"
        tb.Rows.Add(dr)
        ds.Tables.Add(tb)
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        aCollect.Add("Data".ToUpper, ds)
        ''Dim dtx As Silverlight.DataTable = CType(Parameters("DATA"), Silverlight.DataSet).Tables("ThiedDiscount")
        ''Dim CustId As Integer = dtx.Rows(0).Item("CustId")
        objControl.LoginInfo = FLoginInfo
        objControl.Parameters = aCollect


        'objControl.CanEdit(Nothing,  Sub(bln As Boolean, strMsg As String)
        '                                If bln Then
        '                                    MessageBox.Show("yes")
        '                                Else
        '                                    MessageBox.Show(strMsg)
        '                                End If
        '                            End Sub)




        objControl.EditMode = GetEditModeValue()

        ShowChildWindow(objControl, New Size(900, 500))

        'Dim lstStatus As List(Of UIElement) = LayoutRoot.Children.Where(Function(obj As UIElement)
        '                                                                    If TypeOf obj Is CSWindow Then
        '                                                                        Return True

        '                                                                    End If
        '                                                                    Return False
        '                                                                End Function).ToList()
    End Sub

    Private Sub btnUpload_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnUpload.Click
        'Try
        '    Dim _csUpldr As Global.CableSoft.SL.Utility.Transmission.Uploader
        '    _csUpldr = New Global.CableSoft.SL.Utility.Transmission.Uploader() With
        '               {
        '                   .UploadServerPath = "xls",
        '                   .ShowCancelButton = True,
        '                   .ShowProgress = True
        '               }

        '    If _csUpldr.ShowDialog(, , "All Files|*.*", 1) Then
        '        _csUpldr.UploadFileName = "A.xls"
        '        _csUpldr.Upload()
        '    End If
        'Catch ex As Exception
        '    Throw
        'End Try
    End Sub

    Private Sub btnVodClose_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnVodClose.Click
        Dim objControl As New Global.CableSoft.SO.SL.Facility.VOD.Calculate.SO1100U

        Dim ds As New Silverlight.DataSet()
        Dim tb As New Silverlight.DataTable("VODCalculate")


        tb.Columns.Add(New Silverlight.DataColumn("CustId"))
        Dim dr As Silverlight.DataRow = tb.NewRow

        dr.Item("CustId") = 600028

        tb.Rows.Add(dr)
        ds.Tables.Add(tb)
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        aCollect.Add("Data".ToUpper, ds)

        objControl.LoginInfo = FLoginInfo
        objControl.Parameters = aCollect
        objControl.EditMode = GetEditModeValue()
        ShowChildWindow(objControl, New Size(700, 500))
    End Sub

    Private Sub btnSO5620_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSO5620.Click
        Dim aResult As InvokeOperation(Of CableSoft.BLL.Utility.RIAResult)
        Dim objControl As New Global.CableSoft.SL.Common.Dynamic.Report.DynamicReport
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        objControl.EditMode = GetEditModeValue()

        objControl.LoginInfo = FLoginInfo
        objControl.SysProgramId = "SO3320A"
        'objControl.SourceTableName = "CD068"
        'objControl.IsAutoClosed = True
        'objControl.Refresh(Sub(bln As Boolean, msg As String)
        'If bln Then
        '    MessageBox.Show("完成")
        'Else
        '    MessageBox.Show("失敗")
        'End If
        '                     End Sub)
        ShowChildWindow(objControl, New Size(900, 500))
    End Sub

    Private Sub btnSO3316_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSO3316.Click
        Dim objControl As New Global.CableSoft.SO.SL.Billing.ACHAuthIn.SO3316A

        'Dim ds As New Silverlight.DataSet()
        'Dim tb As New Silverlight.DataTable("VODCalculate")


        'tb.Columns.Add(New Silverlight.DataColumn("CustId"))
        'Dim dr As Silverlight.DataRow = tb.NewRow

        'dr.Item("CustId") = 718117

        'tb.Rows.Add(dr)
        'ds.Tables.Add(tb)
        'Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        'aCollect.Add("Data".ToUpper, ds)

        objControl.LoginInfo = FLoginInfo
        'objControl.Parameters = aCollect
        objControl.EditMode = GetEditModeValue()
        ShowChildWindow(objControl, New Size(600, 250))
    End Sub

    Private Sub btnGC_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnGC.Click
        If tbTest IsNot Nothing Then
            tbTest.Dispose()
            tbTest = Nothing
        End If
        If dsTest IsNot Nothing Then
            dsTest.Dispose()
            dsTest = Nothing
        End If
        GC.Collect()
        GC.WaitForPendingFinalizers()
        GC.Collect()
    End Sub

    Private Sub btnTEST_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnTEST.Click
        tbTest = New Silverlight.DataTable
        dsTest = New Silverlight.DataSet
        tbTest.Columns.Add(New Silverlight.DataColumn("TEST1"))
        tbTest.Columns.Add(New Silverlight.DataColumn("TEST2"))
        tbTest.Columns.Add(New Silverlight.DataColumn("TEST3"))
        Dim rw As Silverlight.DataRow = tbTest.NewRow
        rw.Item("TEST1") = "1"
        rw.Item("TEST1") = "2"
        rw.Item("TEST1") = "3"
        tbTest.Rows.Add(rw)
        Dim rwSource As Silverlight.DataRow = tbTest.Rows(0)
        rwSource.Item("TEST1") = "A"
        dsTest.Tables.Add(tbTest)
        MessageBox.Show("完成")

    End Sub
    Private Sub TestTB()
        Dim str As String = dsTest.ToXml(False)

    End Sub

    Private Sub btnMFCode_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnMFCode.Click
        Dim objControl As New Global.CableSoft.SO.SL.CodeSet.MFCode.MFCode
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        objControl.EditMode = GetEditModeValue()

        objControl.LoginInfo = FLoginInfo

        'objControl.SourceTableName = "CD068"
        'objControl.IsAutoClosed = True
        'objControl.Refresh(Sub(bln As Boolean, msg As String)
        'If bln Then
        '    MessageBox.Show("完成")
        'Else
        '    MessageBox.Show("失敗")
        'End If
        '                     End Sub)

        ShowChildWindow(objControl, New Size(900, 500))
    End Sub

    Private Sub btnPRReason_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnPRReason.Click
        Dim objControl As New Global.CableSoft.SO.SL.CodeSet.PRReasonCode.PRReasonCode
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        objControl.EditMode = GetEditModeValue()

        objControl.LoginInfo = FLoginInfo

        'objControl.SourceTableName = "CD068"
        'objControl.IsAutoClosed = True
        'objControl.Refresh(Sub(bln As Boolean, msg As String)
        'If bln Then
        '    MessageBox.Show("完成")
        'Else
        '    MessageBox.Show("失敗")
        'End If
        '                     End Sub)

        ShowChildWindow(objControl, New Size(800, 500))
    End Sub
End Class
