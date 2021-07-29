Imports System.ServiceModel.DomainServices.Client
Imports Silverlight.DataSet
Imports DevExpress.Xpf.Core
Imports System.Reflection
'Imports CableSoft.SO.RIA.Facility.ChooseFaci
Imports CableSoft.SO.SL.Billing.LevelDiscount.ThirdDiscount
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

Partial Public Class MainPage
    Inherits UserControl

    Private FLoginInfo As Object
    Private Master As Global.CableSoft.SL.Common.Dynamic.Grid.DynamicGrid = Nothing
    Public Sub New()
        InitializeComponent()
        FLoginInfo = App.LoginInfoWeb
        '((WebDomainClient<MyDomainContext.IMyDomainServiceContract>)this.DomainClient)
        '            .ChannelFactory.Endpoint.Binding.SendTimeout = new TimeSpan(0, 5, 0);


    End Sub

    Private Sub SO1131C_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles SO1131C.Click
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
    Private Sub ShowChildWindow(ByVal aChild As Object, ByVal aSize As Size)
        Dim objShowWindow As New CSWindow()
        AddHandler objShowWindow.Closed, Sub(s, arg)
                                             If TypeOf aChild Is Global.CableSoft.SO.SL.Facility.ChangeFaci.SO111XD Then
                                                 'Dim ctl As Global.CableSoft.SO.SL.Facility.ChangeFaci.SO111XD = CType(aChild, Global.CableSoft.SO.SL.Facility.ChangeFaci.SO111XD)
                                                 'For Each rw As Silverlight.DataRow In ctl.WipData.Tables("ChangeFacility").Rows
                                                 '    MessageBox.Show("ChooseServiceID:" & rw.Item("ChooseServiceID"))
                                                 '    MessageBox.Show("Delete003Citem:" & rw.Item("Delete003Citem"))
                                                 'Next
                                             End If
                                             If TypeOf aChild Is Global.CableSoft.SO.SL.Customer.Account.SO1100D Then
                                                 'Dim ctl As Global.CableSoft.SO.SL.Customer.Account.SO1100D = CType(aChild, Global.CableSoft.SO.SL.Customer.Account.SO1100D)
                                                 'If ctl.IsSaved Then
                                                 '    MessageBox.Show("Yes")
                                                 'Else
                                                 '    MessageBox.Show("NO")
                                                 'End If

                                             End If
                                             If TypeOf aChild Is Global.CableSoft.SO.SL.Order.ChooseNextPeriod.SO1144N Then
                                                 Dim dt As Silverlight.DataTable = CType(aChild, Global.CableSoft.SO.SL.Order.ChooseNextPeriod.SO1144N).Charge
                                                 If dt IsNot Nothing Then
                                                     'MessageBox.Show(dt.Rows.Count)
                                                 End If
                                                 'Dim ctl As Global.CableSoft.SO.SL.Customer.Account.SO1100D = CType(aChild, Global.CableSoft.SO.SL.Customer.Account.SO1100D)
                                                 'If ctl.IsSaved Then
                                                 '    MessageBox.Show("Yes")
                                                 'Else
                                                 '    MessageBox.Show("NO")
                                                 'End If

                                             End If
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
        dr.Item("CUSTID") = 600131
        'dr.Item("FACISEQNO") = "200609190189257"
        dr.Item("ServiceType") = "P"
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



    Private Sub btnDVRData_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)

        'Dim aResult As InvokeOperation(Of Global.CableSoft.SO.RIA.Facility.DVRData.Web.RIAResult) = Nothing


        'Dim aRiaDVR As New Global.CableSoft.SO.RIA.Facility.DVRData.Web.DVRData()
        'aResult = aRiaDVR.ChkPhoneNumberOk(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo,
        '                                                                          New Global.CableSoft.SO.RIA.Facility.DVRData.Web.LoginInfo),
        '                                                                          "201009120666421", "0926688238") '201009120666422


        'AddHandler aResult.Completed, Sub(s, arg)
        '                                  If s.value IsNot Nothing Then
        '                                      If s.Value.ResultBoolean Then
        '                                          MessageBox.Show("yes")
        '                                      Else
        '                                          MessageBox.Show(s.Value.ErrorMessage)
        '                                      End If
        '                                  End If
        '                              End Sub

    End Sub

    Private Sub btnQueryPhoneNumber_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        'Dim aResult As InvokeOperation(Of Global.CableSoft.SO.RIA.Facility.DVRData.Web.RIAResult) = Nothing
        'Dim aRiaDVR As New Global.CableSoft.SO.RIA.Facility.DVRData.Web.DVRData()

        'aResult = aRiaDVR.QueryPhoneNumber(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo,
        '                                                                          New Global.CableSoft.SO.RIA.Facility.DVRData.Web.LoginInfo), "201009120666422")
        'AddHandler aResult.Completed, Sub(s, arg)
        '                                  If s.value IsNot Nothing Then
        '                                      If s.Value.ResultBoolean Then
        '                                          Dim dsSO004H As New Silverlight.DataSet()

        '                                          dsSO004H.Tables.Clear()
        '                                          dsSO004H.FromXml(s.value.resultxml)
        '                                          dsSO004H.Tables(0).TableName = "SO004H"
        '                                          MessageBox.Show("Qurey OK")
        '                                      Else
        '                                          MessageBox.Show(s.Value.ErrorMessage)
        '                                      End If
        '                                  End If

        '                              End Sub
    End Sub

    Private Sub DVRSave_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        'Dim aResult As InvokeOperation(Of Global.CableSoft.SO.RIA.Facility.DVRData.Web.RIAResult) = Nothing
        'Dim aRiaDVR As New Global.CableSoft.SO.RIA.Facility.DVRData.Web.DVRData()
        'Dim dsSO004H As New Silverlight.DataSet()
        'aResult = aRiaDVR.QueryPhoneNumber(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo,
        '                                                                          New Global.CableSoft.SO.RIA.Facility.DVRData.Web.LoginInfo), "201009120666422")
        'AddHandler aResult.Completed, Sub(s, arg)
        '                                  dsSO004H.Tables.Clear()
        '                                  dsSO004H.FromXml(s.value.resultxml)
        '                                  dsSO004H.Tables(0).TableName = "DVRData"
        '                                  dsSO004H.Tables(0).Columns.Add(New Silverlight.DataColumn("RowId"))
        '                                  dsSO004H.Tables(0).Rows(0).Item("RowId") = "AAAh3TAAOAAG5y9ABN"

        '                                  Dim aResult2 As InvokeOperation(Of Global.CableSoft.SO.RIA.Facility.DVRData.Web.RIAResult) = Nothing
        '                                  Dim aRiaDVR2 As New Global.CableSoft.SO.RIA.Facility.DVRData.Web.DVRData
        '                                  aResult2 = aRiaDVR2.Save(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo,
        '                                                                           New Global.CableSoft.SO.RIA.Facility.DVRData.Web.LoginInfo),
        '                                                                        Global.CableSoft.SO.RIA.Facility.DVRData.CableSoft.BLL.Utility.EditMode.Append,
        '                                                                       dsSO004H.ToXml(False))


        '                                  AddHandler aResult2.Completed, Sub(s2, arg2)
        '                                                                     If s2.value IsNot Nothing Then
        '                                                                         If s2.Value.ResultBoolean Then
        '                                                                             MessageBox.Show("Save OK")
        '                                                                         Else
        '                                                                             MessageBox.Show(s2.Value.ErrorMessage)
        '                                                                         End If
        '                                                                     End If
        '                                                                 End Sub
        '                              End Sub
    End Sub

    Private Sub btnQueryIDPicture_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)

        'Dim aResult As InvokeOperation(Of Global.CableSoft.SO.RIA.Facility.IDData.Web.RIAResult) = Nothing
        'Dim aRiaDVR As New Global.CableSoft.SO.RIA.Facility.IDData.Web.IDData
        'Dim objLoginInfo As Object = Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.CableSoft.SO.RIA.Facility.IDData.Web.LoginInfo)
        'aResult = aRiaDVR.QueryIDPicture(objLoginInfo, "C:\Documents and Settings\Administrator\桌面\20110222 高雄辦公室聚餐吃麻辣小火鍋\20110222 高雄辦公室聚餐吃麻辣小火鍋", "IMG_0001.JPG")
    End Sub

    Private Sub btnSaveID_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)

        'Dim aQryResult As InvokeOperation(Of CableSoft.SO.RIA.Facility.IDData.Web.RIAResult) = Nothing
        'Dim aRiaQryIDData As New CableSoft.SO.RIA.Facility.IDData.Web.IDData()
        'Dim dsSO004E As New Silverlight.DataSet("IDData")

        'aQryResult = aRiaQryIDData.QueryIDData(CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New CableSoft.SO.RIA.Facility.IDData.Web.LoginInfo),
        '                                       "1685061924")
        'AddHandler aQryResult.Completed, Sub(s, arg)
        '                                     dsSO004E.Tables.Clear()
        '                                     dsSO004E.FromXml(s.value.resultxml)
        '                                     dsSO004E.Tables(0).TableName = "IDData"
        '                                     'dsSO004E.Tables(0).Rows(0).Item("CreateDate") = Date.Now
        '                                     'dsSO004E.Tables(0).Columns.Add(New Silverlight.DataColumn("RowId"))
        '                                     'dsSO004E.Tables(0).Rows(0).Item("RowId") = "AAAh3TAAOAAG5y9ABN"
        '                                     Dim aResult As InvokeOperation(Of CableSoft.SO.RIA.Facility.IDData.Web.RIAResult) = Nothing
        '                                     Dim aRiaIDData As New CableSoft.SO.RIA.Facility.IDData.Web.IDData
        '                                     aResult = aRiaIDData.Save(CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New CableSoft.SO.RIA.Facility.IDData.Web.LoginInfo()), CableSoft.SO.RIA.Facility.IDData.CableSoft.BLL.Utility.EditMode.Edit, dsSO004E.ToXml(False))

        '                                     AddHandler aResult.Completed, Sub(s2, arg2)
        '                                                                       If s2.value IsNot Nothing Then
        '                                                                           If s2.Value.ResultBoolean Then
        '                                                                               MessageBox.Show("Save OK")

        '                                                                           Else
        '                                                                               MessageBox.Show(s2.Value.ErrorMessage)
        '                                                                           End If
        '                                                                       End If

        '                                                                   End Sub

        '                                 End Sub


    End Sub

    Private Sub btnQueryIDData_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)

        'Dim aQryResult As InvokeOperation(Of Global.CableSoft.SO.RIA.Facility.IDData.Web.RIAResult) = Nothing
        'Dim aRiaQryIDData As New Global.CableSoft.SO.RIA.Facility.IDData.Web.IDData()
        'Dim dsSO004E As New Silverlight.DataSet("IDData")
        'aQryResult = aRiaQryIDData.QueryIDKind(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.CableSoft.SO.RIA.Facility.IDData.Web.LoginInfo))

        'AddHandler aQryResult.Completed, Sub(s, arg)
        '                                     If s.Value IsNot Nothing Then
        '                                         If s.Value.ResultBoolean Then
        '                                             Dim ds As New Silverlight.DataSet
        '                                             ds.Tables.Clear()
        '                                             ds.FromXml(s.value.ResultXML)
        '                                             MessageBox.Show("Query OK")
        '                                         Else
        '                                             MessageBox.Show(s.Value.ErrorMessage)
        '                                         End If
        '                                     End If
        '                                 End Sub
    End Sub

    Private Sub btnQueryVODAccount_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        'Dim aQryResult As InvokeOperation(Of Global.CableSoft.SO.RIA.Facility.VODAccount.Web.RIAResult) = Nothing
        'Dim aRiaQryIDData As New Global.CableSoft.SO.RIA.Facility.VODAccount.Web.VODAccount
        'Dim dsSO004E As New Silverlight.DataSet("VODAccount")
        'aQryResult = aRiaQryIDData.QueryVODAccount(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.CableSoft.SO.RIA.Facility.VODAccount.Web.LoginInfo), "1")

        'AddHandler aQryResult.Completed, Sub(s, arg)
        '                                     If s.Value IsNot Nothing Then
        '                                         If s.Value.ResultBoolean Then
        '                                             Dim ds As New Silverlight.DataSet
        '                                             ds.Tables.Clear()
        '                                             ds.FromXml(s.value.ResultXML)
        '                                             MessageBox.Show("Query OK")
        '                                         Else
        '                                             MessageBox.Show(s.Value.ErrorMessage)
        '                                         End If
        '                                     End If
        '                                 End Sub
    End Sub

    Private Sub btnSO1120B2_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        'Dim ds As New Silverlight.DataSet()
        'Dim tb As New Silverlight.DataTable("VODData")
        'Dim tb2 As New Silverlight.DataTable("ReqData")
        'tb.Columns.Add(New Silverlight.DataColumn("VODAccountId"))
        'tb2.Columns.Add(New Silverlight.DataColumn("ReqDep"))
        'tb2.Columns.Add(New Silverlight.DataColumn("ReqEn"))
        'tb2.Columns.Add(New Silverlight.DataColumn("ReqNotes"))
        'Dim dr As Silverlight.DataRow = tb.NewRow
        'dr.Item(0) = 1
        'tb.Rows.Add(dr)
        'ds.Tables.Add(tb)
        'Dim dr2 As Silverlight.DataRow = tb2.NewRow
        'dr2.Item("ReqDep") = "開博"
        'dr2.Item("ReqEn") = "Administrator"
        'dr2.Item("ReqNotes") = "ABCDEFG"
        'tb2.Rows.Add(dr2)
        'ds.Tables.Add(tb2)
        'Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        'aCollect.Add("Data".ToUpper, ds)
        'SO1120B21.LoginInfo = FLoginInfo
        'SO1120B21.Parameters = aCollect
        'SO1120B21.EditMode = IParams.EditModes.Edit
        'SO1120B21.Refresh()

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
        dr.Item("MasterId") = 5988
        dr.Item("CustId") = 644970
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
    End Sub

    Private Sub MainPage_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        'ThemeManager.ApplicationThemeName = "Office2007Blue" 'Theme.Office2007BlueFullName
        ThemeManager.ApplicationThemeName = Theme.Office2007BlueName
        'For Each Str As String In [Enum].GetNames(GetType(IParams.EditModes))
        '    lstEditMode.Items.Add(Str)
        'Next
        lstEditMode.ItemsSource = [Enum].GetNames(GetType(IParams.EditModes))
        lstEditMode.SelectedValue = [Enum].GetName(GetType(IParams.EditModes), IParams.EditModes.Edit)
        AddNote()


        'Dim Child As New Global.CableSoft.SL.Window.CSWindow With {.Width = 800, .Height = 500}
        'Dim grd As New Grid
        'grd.RowDefinitions.Add(New RowDefinition)
        'grd.RowDefinitions.Add(New RowDefinition)

        'Dim grd2 As New Grid
        'grd.Children.Add(grd2)
        'Dim grd3 As New Grid
        'grd.Children.Add(grd3)
        'grd3.SetValue(Grid.RowProperty, 1)
        'Dim x As New Global.CableSoft.SL.Common.Dynamic.Condition.DynamicCondition
        'grd2.Children.Add(x)
        ''CreateButton(grd3, x)
        'If x IsNot Nothing Then
        '    x.LoginInfo = FLoginInfo
        '    x.SysProgramId = "SO1100A"
        '    x.Refresh()
        '    'AddHandler x.TextChanged, AddressOf TextChanged
        '    Child.Show(grd, LayoutRoot)
        'End If

    End Sub


    Private Sub btnSO1112A_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSO1112A.Click
        Dim objControl As New Global.CableSoft.SO.SL.Wip.Maintain.SO1112A()

        Dim ds As New Silverlight.DataSet()
        Dim tb As New Silverlight.DataTable("Maintain")
        Dim tbContact As New Silverlight.DataTable("Contact")
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
        dr.Item("SNO") = "201205MD0006112"
        dr.Item("CustId") = 600057
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
        objControl.LoginInfo = FLoginInfo
        objControl.Parameters = aCollect
        objControl.BatchFin = True
        objControl.FinMode = True
        objControl.IsAutoClosed = False

        objControl.EditMode = GetEditModeValue()

        'ShowChildWindow(objControl, New Size(900, 500))
        objControl.CanAppend(ds, Sub(ariaResult As Boolean, aRiaXml As String)
                                     If ariaResult Then
                                         'MessageBox.Show("yes")
                                         ShowChildWindow(objControl, New Size(900, 500))
                                     Else
                                         MessageBox.Show(aRiaXml)
                                     End If
                                 End Sub)


       
    End Sub
    Private Function GetEditModeValue() As Int32
        Dim aRet As Int32 = 0
        If lstEditMode.SelectedIndex >= 0 Then
            aRet = Int32.Parse([Enum].Parse(GetType(IParams.EditModes), lstEditMode.SelectedValue, False))
        End If
        Return aRet
    End Function


    Private Sub btnSO1112A1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSO1112A1.Click
        Dim objControl As New Global.CableSoft.SO.SL.Wip.Maintain.SO1112A1

        Dim ds As New Silverlight.DataSet()
        Dim tb As New Silverlight.DataTable("Maintain")

        tb.Columns.Add(New Silverlight.DataColumn("SNO"))
        tb.Columns.Add(New Silverlight.DataColumn("CustId"))
        tb.Columns.Add(New Silverlight.DataColumn("ServiceType"))
        'tb.Columns.Add(New Silverlight.DataColumn("FACISEQNO"))
        'tb.Columns.Add(New Silverlight.DataColumn("ServiceType"))
        Dim dr As Silverlight.DataRow = tb.NewRow
        ''159, "200609190189257"
        'dr.Item("SNO") = "200401MD0270558"
        'dr.Item("SNO") = "200408MD0271931"
        'dr.Item("CustId") = 385713
        'dr.Item("ServiceType") = "D"

        dr.Item("CustId") = 600028
        dr.Item("ServiceType") = "D"
        tb.Rows.Add(dr)
        ds.Tables.Add(tb)
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        aCollect.Add("Data".ToUpper, ds)
        ''Dim dtx As Silverlight.DataTable = CType(Parameters("DATA"), Silverlight.DataSet).Tables("ThiedDiscount")
        ''Dim CustId As Integer = dtx.Rows(0).Item("CustId")
        objControl.LoginInfo = FLoginInfo
        objControl.Parameters = aCollect


        objControl.IsAutoClosed = True
        objControl.EditMode = GetEditModeValue()
        objControl.CanDelete(ds, Sub(ariaResult As Boolean, aRiaXml As String)
                                     If ariaResult Then
                                         objControl.VoidData(ds, Sub(bln As Boolean, strMsg As String)
                                                                     If bln Then
                                                                         MessageBox.Show("作廢完成！")
                                                                     Else
                                                                         MessageBox.Show(strMsg)
                                                                     End If
                                                                 End Sub)
                                         'ShowChildWindow(objControl, New Size(900, 500))
                                     Else
                                         MessageBox.Show(aRiaXml)
                                     End If
                                 End Sub)
    End Sub

    Private Sub btnSO111XD_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSO111XD.Click
        Dim SNo As String = "'2012'"
        Dim aMaintainResult As InvokeOperation(Of Global.CableSoft.SO.RIA.Wip.Maintain.Web.RIAResult) = Nothing

        Dim aRiaMaintain As New Global.CableSoft.SO.RIA.Wip.Maintain.Web.Maintain()
        Dim aQueryTableResult As InvokeOperation(Of TestSilverlight.Web.RIAResult) = Nothing
        Dim aRiaQueryTable As New TestSilverlight.Web.QueryTable

        aMaintainResult = aRiaMaintain.GetMaintainData(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.CableSoft.SO.RIA.Wip.Maintain.Web.LoginInfo),
                                                       SNo)

        'If String.IsNullOrEmpty(TextBox1.Text) Then
        '    MessageBox.Show("無任何匯入檔", "訊息", MessageBoxButton.OK)
        '    Exit Sub
        'End If

        'aRiaQueryTable.FromXMLToDataSet(TextBox1.Text)


        'AddHandler aQueryTableResult.Completed, Sub(s2, arg2)


        '                                        End Sub


        AddHandler aMaintainResult.Completed, Sub(s, arg)
                                                  If s.Value IsNot Nothing Then
                                                      If s.Value.ResultBoolean Then
                                                          Dim objControl As New Global.CableSoft.SO.SL.Facility.ChangeFaci.SO111XD
                                                          Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)

                                                          'Dim ds As New Silverlight.DataSet
                                                          'ds.Tables.Clear()
                                                          'ds.FromXml(s.Value.ResultXML)


                                                          Dim ds As New Silverlight.DataSet
                                                          ds.Tables.Clear()
                                                          ds.FromXml(TextBox1.Text)



                                                          Dim rwNew As Silverlight.DataRow = ds.Tables("Wip").NewRow
                                                          rwNew.Item("CUSTID") = "681526"
                                                          rwNew.Item("SNO") = "201208P14501610"
                                                          With rwNew
                                                              .Item("OLDADDRNO") = "1089105"
                                                              .Item("OLDADDRESS") = "竹南鎮環市路2段38號"
                                                              .Item("PRCODE") = "215"
                                                              .Item("PRNAME") = "CM 關機"
                                                              .Item("RESVTIME") = Date.Now.ToString("yyyy/MM/dd HH:mm:ss")
                                                              .Item("SERVCODE") = "103"
                                                              .Item("STRTCODE") = "42181"
                                                              .Item("COMPCODE") = "3"
                                                              .Item("SERVICETYPE") = "I"
                                                              .Item("WORKSERVCODE") = "103"
                                                          End With
                                                          ds.Tables("Wip").Rows.Add(rwNew)

                                                          aCollect.Add("Data".ToUpper, ds)
                                                          objControl.LoginInfo = FLoginInfo
                                                          objControl.Parameters = aCollect
                                                          objControl.CustId = 681526
                                                          objControl.SNo = "201208P14501610"
                                                          objControl.ServiceType = "I"
                                                          objControl.WipData = ds
                                                          objControl.EditMode = GetEditModeValue()
                                                          objControl.CanDo = True
                                                          objControl.WipRefNo = 7
                                                          objControl.PrivCancelGetFaci = True
                                                          objControl.CanCancel = True
                                                          objControl.WipType = 3

                                                          ShowChildWindow(objControl, New Size(900, 500))
                                                      Else
                                                          MessageBox.Show(s.Value.ErrorMessage)
                                                      End If
                                                  End If
                                              End Sub

        'aQryResult = aRiaQryIDData.QueryVODAccount(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.CableSoft.SO.RIA.Facility.VODAccount.Web.LoginInfo), "1")

        'AddHandler aQryResult.Completed, Sub(s, arg)
        '                                     If s.Value IsNot Nothing Then
        '                                         If s.Value.ResultBoolean Then
        '                                             Dim ds As New Silverlight.DataSet
        '                                             ds.Tables.Clear()
        '                                             ds.FromXml(s.value.ResultXML)
        '                                             MessageBox.Show("Query OK")
        '                                         Else
        '                                             MessageBox.Show(s.Value.ErrorMessage)
        '                                         End If
        '                                     End If
        '                                 End Sub


        'Dim objControl As New Global.CableSoft.SO.SL.Facility.ChangeFaci.SO111XD
        'Dim ds As New Silverlight.DataSet()
        'Dim tb As New Silverlight.DataTable("Wip")

        'tb.Columns.Add(New Silverlight.DataColumn("SNO"))
        'tb.Columns.Add(New Silverlight.DataColumn("CustId"))
        'tb.Columns.Add(New Silverlight.DataColumn("ServiceType"))
        ''tb.Columns.Add(New Silverlight.DataColumn("FACISEQNO"))
        ''tb.Columns.Add(New Silverlight.DataColumn("ServiceType"))
        'Dim dr As Silverlight.DataRow = tb.NewRow
        ' ''159, "200609190189257"
        ''dr.Item("SNO") = "200401MD0270558"
        'dr.Item("SNO") = "201203MD0319654"
        'dr.Item("CustId") = 385713
        'dr.Item("ServiceType") = "D"
        ' ''dr.Item("FACISEQNO") = "200609190189257"
        ''dr.Item("ServiceType") = "I"
        'tb.Rows.Add(dr)
        'ds.Tables.Add(tb)
        'Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        'aCollect.Add("Data".ToUpper, ds)
        ' ''Dim dtx As Silverlight.DataTable = CType(Parameters("DATA"), Silverlight.DataSet).Tables("ThiedDiscount")
        ' ''Dim CustId As Integer = dtx.Rows(0).Item("CustId")
        'objControl.LoginInfo = FLoginInfo
        'objControl.Parameters = aCollect
        ''objControl.BatchFin = True
        ''objControl.FinMode = True
        ''objControl.IsAutoClosed = True
        'objControl.EditMode = IParams.EditModes.Edit
        'ShowChildWindow(objControl, New Size(900, 500))
        ''objControl.CanEdit(ds, Sub(ariaResult As Boolean, aRiaXml As String)
        ''                           If ariaResult Then
        ''                               MessageBox.Show("yes")
        ''                               ShowChildWindow(objControl, New Size(900, 500))
        ''                           Else
        ''                               MessageBox.Show(aRiaXml)
        ''                           End If
        ''                       End Sub)

    End Sub



    Private Sub btnCPEMAC_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnCPEMAC.Click
        Dim objControl As New Global.CableSoft.SO.SL.Facility.CPEMAC.SO111xCEA()
        Dim aResult As InvokeOperation(Of CableSoft.SO.RIA.Facility.CPEMAC.Web.RIAResult) = Nothing
        Dim aRiaCPEMAC As New CableSoft.SO.RIA.Facility.CPEMAC.Web.CPEMAC
        aResult = aRiaCPEMAC.GetCPEMAC(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New CableSoft.SO.RIA.Facility.CPEMAC.Web.LoginInfo),
                                                       681433, "201003180066906")

        AddHandler aResult.Completed, Sub(s, arg)
                                          If s.Value IsNot Nothing Then
                                              If s.Value.ResultBoolean Then
                                                  Dim ds As New Silverlight.DataSet
                                                  ds.Tables.Clear()
                                                  ds.FromXml(s.value.ResultXML)
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

    Private Sub Command_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Command.Click
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

        
       
        Dim aResult As InvokeOperation(Of Global.CableSoft.SO.RIA.Facility.Command.Web.RIAResult) = Nothing
        Dim aRiaCommand As New Global.CableSoft.SO.RIA.Facility.Command.Web.Command
        Global.CableSoft.SL.Utility.Utility.SetTimeout(aRiaCommand, 600)    '設定TimeOut時間為10分鐘
        aResult = aRiaCommand.InsertCommand(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.CableSoft.SO.RIA.Facility.Command.Web.LoginInfo),
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

    Private Sub btnOrderFacility_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnOrderFacility.Click
        Dim aResult As InvokeOperation(Of Global.TestSilverlight.Web.RIAResult) = Nothing
        Dim aRiaQuerySO105 As New Global.TestSilverlight.Web.QueryTable()
        aResult = aRiaQuerySO105.GetSO105D(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.TestSilverlight.Web.LoginInfo))
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
        'aResult = aRiaQuerySO105.(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.CableSoft.SO.RIA.Facility.Command.Web.LoginInfo),
        '                                   "SO555", "Z3", WipData.ToXml(False), False)

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
        Dim aResult As InvokeOperation(Of Global.TestSilverlight.Web.RIAResult) = Nothing
        Dim aRiaQuerySO105B As New Global.TestSilverlight.Web.QueryTable()
        aResult = aRiaQuerySO105B.GetSO105B(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.TestSilverlight.Web.LoginInfo))
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
    Private Sub AddNote()
        Dim ds As New Silverlight.DataSet
        Dim dt As New Silverlight.DataTable
        dt.Columns.Add(New Silverlight.DataColumn("ParentId"))
        dt.Columns.Add(New Silverlight.DataColumn("Id"))
        dt.Columns.Add(New Silverlight.DataColumn("Test1"))
        dt.Columns.Add(New Silverlight.DataColumn("Test2"))
        dt.Columns.Add(New Silverlight.DataColumn("Test3"))
        Dim rw As Silverlight.DataRow = dt.NewRow()        
        rw.Item("ParentId") = "1"
        rw.Item("Id") = "1"
        rw.Item("Test1") = "ABC"
        rw.Item("Test2") = "DEF"
        rw.Item("Test3") = "GHI"
        dt.Rows.Add(rw)

        Dim rw2 As Silverlight.DataRow = dt.NewRow
        rw2.Item("ParentId") = "1"
        rw2.Item("Id") = "2"
        rw2.Item("Test1") = "AAA"
        rw2.Item("Test2") = "BBB"
        rw2.Item("Test3") = "CCC"
        dt.Rows.Add(rw2)

        Dim rw3 As Silverlight.DataRow = dt.NewRow
        rw3.Item("ParentId") = "2"
        rw3.Item("Id") = "3"
        rw3.Item("Test1") = "AAA"
        rw3.Item("Test2") = "BBB"
        rw3.Item("Test3") = "CCC"
        dt.Rows.Add(rw3)


        ds.Tables.Add(dt)
        'TableView1.Nodes

        'GridControl1.ItemsSource = ds.GetBindingData(0)

        'Dim aResult As InvokeOperation(Of Global.TestSilverlight.Web.RIAResult) = Nothing
        'Dim aRiaCodeNo As New Global.TestSilverlight.Web.QueryTable()

        'aResult = aRiaCodeNo.GetCD001(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.TestSilverlight.Web.LoginInfo))
        'AddHandler aResult.Completed, Sub(s, arg)
        '                                  If s.Value IsNot Nothing Then
        '                                      Dim dsTest As New Silverlight.DataSet
        '                                      dsTest.Tables.Clear()
        '                                      dsTest.FromXml(s.value.ResultXML)
        '                                      dsTest.AcceptChanges()

        '                                      Master = New Global.CableSoft.SL.Common.Dynamic.Grid.DynamicGrid With
        '                                       {.SystemProgramId = "CD001", .ParentDataSource = dsTest,
        '                                           .LoginInfo = App.LoginInfoWeb,
        '                                               .ShowStatusBar = False, .Container = LayoutRoot}
        '                                      Master.Refresh()
        '                                      AddHandler Master.FocusedRowChanged, AddressOf RefreshUpdate
        '                                      'LayoutRoot.Children.Add(Master)
        '                                      DynamicGridTest.Children.Add(Master)




        '                                  Else
        '                                      MessageBox.Show(s.Value.ErrorMessage)
        '                                  End If
        '                              End Sub



        
       


        'MasterGrid.Children.Add(Master)

        'AddHandler Master.ButtonClick, AddressOf ShowDetailGrid

        'AddHandler Master.FocusedRowChanged, AddressOf RefreshDetailGrid

        'Master.Refresh()

        'Dim note As New TreeListNode()
        'note.Content = "ABC"
        'TreeListView1.Nodes.Add(note)

    End Sub
    Private Sub btnDeposit_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnDeposit.Click
        Dim aResult As InvokeOperation(Of Global.TestSilverlight.Web.RIAResult) = Nothing
        Dim aRiaDeposit As New Global.TestSilverlight.Web.QueryTable()
        aResult = aRiaDeposit.GetDeposit(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.TestSilverlight.Web.LoginInfo))
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
    Private Sub ShowDynamicUpdate()
        Dim aResult As InvokeOperation(Of Global.TestSilverlight.Web.RIAResult) = Nothing
        Dim aRiaCD001 As New Global.TestSilverlight.Web.QueryTable()
        aResult = aRiaCD001.GetCD001(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.TestSilverlight.Web.LoginInfo))
        AddHandler aResult.Completed, Sub(s, arg)
                                          If s.Value IsNot Nothing Then
                                              Dim ds As New Silverlight.DataSet
                                              ds.Tables.Clear()
                                              ds.FromXml(s.value.ResultXML)
                                              ds.AcceptChanges()
                                              Dim objControl As New Global.CableSoft.SL.Common.Dynamic.DynamicUpdate.DynamicUpdate
                                              Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
                                              aCollect.Add("Data".ToUpper, ds)
                                              objControl.Parameters = aCollect
                                              objControl.EditMode = GetEditModeValue()
                                              objControl.LoginInfo = FLoginInfo
                                              objControl.SysProgramId = "CD001"
                                              objControl.IsAutoClosed = True
                                              objControl.Refresh()

                                              'objControl.IsAutoClosed = False
                                              'ShowChildWindow(objControl, New Size(900, 500))
                                          Else
                                              MessageBox.Show(s.Value.ErrorMessage)
                                          End If
                                      End Sub
    End Sub

    Private Sub btnDynamicUpdate_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnDynamicUpdate.Click
        Dim aResult As InvokeOperation(Of Global.TestSilverlight.Web.RIAResult) = Nothing
        Dim aRiaCodeNo As New Global.TestSilverlight.Web.QueryTable()
        aResult = aRiaCodeNo.GetCD001(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.TestSilverlight.Web.LoginInfo))
        AddHandler aResult.Completed, Sub(s, arg)
                                          If s.Value IsNot Nothing Then
                                              Dim ds As New Silverlight.DataSet
                                              ds.Tables.Clear()
                                              ds.FromXml(s.value.ResultXML)
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
                                              objControl.SysProgramId = "SO1400B"
                                              objControl.SysProgramId = "SO6110A"
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
                                              MessageBox.Show(s.Value.ErrorMessage)
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

    Private Sub RefreshUpdate(sender As Object, e As FocusedRowChangedEventArgs)
        If Master.GetFocusDataTable IsNot Nothing Then

            MessageBox.Show(Master.GetFocusDataTable.Rows(0).Item(0))
        End If
    End Sub

    Private Sub btnDynUpdateGrid_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnDynUpdateGrid.Click
        Dim aResult As InvokeOperation(Of Global.TestSilverlight.Web.RIAResult) = Nothing
        Dim aRiaCodeNo As New Global.TestSilverlight.Web.QueryTable()
        Dim objControl As New Global.CableSoft.SL.Common.Dynamic.DynUpdateGrid.DynUpdateGrid
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        'Dim tbCD068 As New Silverlight.DataTable("TCD0681")
        'Dim ds As New Silverlight.DataSet
        'tbCD068.Columns.Add(New Silverlight.DataColumn("PostAccountNo", GetType(String)))
        'Dim rwNew As Silverlight.DataRow
        'rwNew = tbCD068.NewRow
        'rwNew.Item(0) = "456"
        'tbCD068.Rows.Add(rwNew)
        'tbCD068.AcceptChanges()

        Dim tbCD068 As New Silverlight.DataTable("CD001")
        Dim ds As New Silverlight.DataSet
        tbCD068.Columns.Add(New Silverlight.DataColumn("MDUID", GetType(String)))
        tbCD068.Columns.Add(New Silverlight.DataColumn("CompCode", GetType(Integer)))
        Dim rwNew As Silverlight.DataRow
        rwNew = tbCD068.NewRow
        rwNew.Item(0) = "D0030"
        rwNew.Item(1) = 3
        tbCD068.Rows.Add(rwNew)
        tbCD068.AcceptChanges()

        ds.Tables.Add(tbCD068)
        aCollect.Add("DATA", ds)
        objControl.EditMode = GetEditModeValue()


        objControl.EditMode = GetEditModeValue()
        'objControl.Parameters = aCollect
        objControl.LoginInfo = FLoginInfo
        'objControl.SysProgramId = "SO1300D"
        objControl.SysProgramId = "SO6110A"
        objControl.IsAutoClosed = False
        'objControl.Refresh(Sub(bln As Boolean, msg As String)
        '                       If bln Then
        '                           MessageBox.Show("完成")
        '                       Else
        '                           MessageBox.Show("失敗")
        '                       End If
        '                   End Sub)
        ShowChildWindow(objControl, New Size(900, 500))

        'aResult = aRiaCodeNo.GetCD068(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.TestSilverlight.Web.LoginInfo))
        'AddHandler aResult.Completed, Sub(s, arg)
        '                                  If s.Value IsNot Nothing Then
        '                                      Dim ds As New Silverlight.DataSet
        '                                      ds.Tables.Clear()
        '                                      ds.FromXml(s.value.ResultXML)
        '                                      ds.AcceptChanges()



        '                                      'Dim objControl As New Global.CableSoft.SL.Common.Dynamic.DynUpdateGrid.DynUpdateGrid
        '                                      'Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        '                                      aCollect.Add("Data".ToUpper, ds)

        '                                      objControl.EditMode = GetEditModeValue()

        '                                      If objControl.EditMode = IParams.EditModes.AddNew Then
        '                                          For Each tb As Silverlight.DataTable In CType(aCollect("Data".ToUpper), Silverlight.DataSet).Tables
        '                                              tb.Rows.Clear()
        '                                          Next
        '                                      End If
        '                                      objControl.EditMode = GetEditModeValue()
        '                                      objControl.Parameters = aCollect
        '                                      objControl.LoginInfo = FLoginInfo
        '                                      objControl.SysProgramId = "CD068"
        '                                      objControl.IsAutoClosed = False
        '                                      objControl.Refresh(Sub(bln As Boolean, msg As String)
        '                                                             'If bln Then
        '                                                             '    MessageBox.Show("完成")
        '                                                             'Else
        '                                                             '    MessageBox.Show("失敗")
        '                                                             'End If
        '                                                         End Sub)
        '                                      'objControl.Refresh()
        '                                      'objControl.Refresh2(Sub(bln As Boolean, Msg As String)
        '                                      '                        If bln Then
        '                                      '                            MessageBox.Show("完成")
        '                                      '                        Else
        '                                      '                            MessageBox.Show(Msg)
        '                                      '                        End If
        '                                      '                    End Sub)
        '                                      'objControl.IsAutoClosed = False
        '                                      ShowChildWindow(objControl, New Size(900, 500))
        '                                  Else
        '                                      MessageBox.Show(s.Value.ErrorMessage)
        '                                  End If
        '                              End Sub


    End Sub

    Private Sub btnGetCustomer_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnGetCustomer.Click
        Dim riaOrder As New Global.CableSoft.SO.RIA.Order.Edit.Web.OrderEdit

        Dim riaData As InvokeOperation(Of Global.CableSoft.SO.RIA.Order.Edit.Web.RIAResult) = riaOrder.QueryCustomer(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.CableSoft.SO.RIA.Order.Edit.Web.LoginInfo),
                                                                                                                     600028)
        'AddHandler riaData.Completed, Sub(s, args)
        '                                  If s.Value IsNot Nothing Then
        '                                      If Not String.IsNullOrEmpty(s.Value.ErrorMessage) Then
        '                                          MessageBox.Show(s.Value.ErrorMessage)
        '                                          Return
        '                                      End If
        '                                      Try
        '                                          Dim ds As New Silverlight.DataSet
        '                                          ds.FromXml(s.value.ResultXML)

        '                                          '客戶基本資料
        '                                          'If dsOrderData.Tables(CustomerTableName) IsNot Nothing Then
        '                                          '    dsOrderData.Tables.Remove(dsOrderData.Tables(CustomerTableName))
        '                                          'End If
        '                                          'dsOrderData.Tables.Add(ds.Tables(0).Copy(False))
        '                                          'If dsUnderData.Tables(ParameterTableName) Is Nothing Then
        '                                          '    dsUnderData.Tables.Add(ds.Tables(ParameterTableName).Copy(False))
        '                                          '    If dsUnderData.Tables(ParameterTableName).Rows(0).Item("PaynowFlag") = 0 Then
        '                                          '        CType(chkPaynow.Parent, Grid).ColumnDefinitions(Grid.GetColumn(chkPaynow) - 1).Width = New GridLength(CType(chkPaynow.Parent, Grid).ColumnDefinitions(Grid.GetColumn(chkPaynow) - 1).Width.Value + CType(chkPaynow.Parent, Grid).ColumnDefinitions(Grid.GetColumn(chkPaynow)).Width.Value, GridUnitType.Star)
        '                                          '        CType(chkPaynow.Parent, Grid).ColumnDefinitions(Grid.GetColumn(chkPaynow)).Width = New GridLength(0)
        '                                          '    End If
        '                                          'End If

        '                                      Catch ex As Exception
        '                                          'CableSoft.SL.Utility.Utility.ShowErrMsg(ex.Message, True, ex, LoginInfo)
        '                                      End Try
        '                                  End If
        '                                  'completed(True)
        '                              End Sub

        Dim aControl As New Global.CableSoft.SO.SL.Order.Edit.SO1144B
        Dim ds As New Silverlight.DataSet()
        Dim dt As New Silverlight.DataTable("Order")
        Dim aCollect As New Dictionary(Of String, Object)

        dt.Columns.Add(New Silverlight.DataColumn("CustId", GetType(Integer)))
        dt.Columns.Add(New Silverlight.DataColumn("OrderNo", GetType(String)))
        Dim rwNew As Silverlight.DataRow = dt.NewRow
        rwNew.Item("CustId") = 601005
        rwNew.Item("OrderNo") = "201301080047479"
        dt.Rows.Add(rwNew)
        ds.Tables.Add(dt)
        aCollect.Add("Data".ToUpper, ds) '需有有一個DataTable: Order ,Field: CustId,OrderNo
        aControl.Parameters = aCollect
        aControl.EditMode = IParams.EditModes.Edit
        aControl.LoginInfo = FLoginInfo ' Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.CableSoft.SO.RIA.Order.Edit.Web.LoginInfo)
        ShowChildWindow(aControl, New Size(Me.Width, Me.Height))
    End Sub

    Private Sub btnSaveText_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSaveText.Click
        Dim _SaveText As New SaveFileDialog()
        _SaveText.DefaultExt = "txt"
        '_SaveText.DefaultFileName = "Test"
        _SaveText.DefaultFileName = "Test"
        If _SaveText.ShowDialog Then
            '_SaveText.DefaultFileName = "Test"
            Using stm As System.IO.Stream = _SaveText.OpenFile
                Using wtr As New System.IO.StreamWriter(stm, System.Text.UnicodeEncoding.Unicode)
                    wtr.WriteLine("ABCDE")
                    wtr.Flush()
                    wtr.Close()
                End Using
            End Using
        End If
    End Sub

    Private Sub btnWriteText_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnWriteText.Click
        Dim aResult As InvokeOperation(Of Global.TestSilverlight.Web.RIAResult) = Nothing
        Dim aRiaCodeNo As New Global.TestSilverlight.Web.QueryTable()


        aResult = aRiaCodeNo.WriteText(Global.CableSoft.SL.Utility.Utility.ConvertTo(FLoginInfo, New Global.TestSilverlight.Web.LoginInfo))
        'Dim aResult As InvokeOperation(Of Global.TestSilverlight.Web.RIAResult) = Nothing
        'Dim aRia As New Global.TestSilverlight.Web.QueryTable()

        'aResult = aRia.WriteText(FLoginInfo)
        'AddHandler aResult.Completed, Sub(s, arg)

        '                              End Sub

        Dim obj As New System.Net.WebClient()
        AddHandler obj.OpenReadCompleted, AddressOf TestComplete


    End Sub
    Private Sub TestComplete(sender As Object, e As System.Net.OpenReadCompletedEventArgs)
        If e.Error Is Nothing Then
            Using stm As System.IO.Stream = e.Result
                Dim bty() As Byte = System.IO.File.ReadAllBytes("C:\Test.xml")
                Using mem As New System.IO.MemoryStream(bty)
                    Using stm2 As New System.IO.FileStream("C:\TEST.xml", IO.FileMode.OpenOrCreate)
                        mem.WriteTo(stm2)
                        stm2.Flush()
                    End Using
                End Using
            End Using
        Else

        End If
    End Sub

    Private Sub btnTextOut_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnTextOut.Click
        Dim aResult As InvokeOperation(Of Global.TestSilverlight.Web.RIAResult) = Nothing
        Dim aRiaCodeNo As New Global.TestSilverlight.Web.QueryTable()
        Dim objControl As New Global.CableSoft.SL.Common.Dynamic.TextFileOut.DynTextFileOut
        Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        objControl.EditMode = GetEditModeValue()
        'objControl.Parameters = aCollect
        objControl.LoginInfo = FLoginInfo
        objControl.SysProgramId = "SO3271A"
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

    Private Sub btnEnterPay_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnEnterPay.Click
        Dim objControl As New Global.CableSoft.SO.SL.Billing.Batch.EnterPay.SO3311A
        objControl.LoginInfo = FLoginInfo
        'ShowChildWindow(objControl, New Size(500, 500))
        ShowChildWindow(objControl, New Size(900, 700))


      
    End Sub
End Class