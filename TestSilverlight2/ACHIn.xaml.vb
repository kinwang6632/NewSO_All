Imports CableSoft.SL.Utility
Imports CableSoft.SL.Window
Imports DevExpress.Xpf.Core
Imports Silverlight.DataSet

Partial Public Class ACHIn
    Inherits Page
    Private ds As Silverlight.DataSet = Nothing
    Private tb As Silverlight.DataTable = Nothing
    Private tbContact As Silverlight.DataTable = Nothing
    'Dim objControl As Global.CableSoft.SO.SL.Wip.Maintain.SO1112A = Nothing
    Public Sub New()
        InitializeComponent()
    End Sub

    '使用者巡覽至這個頁面時執行。


    Private Sub btbGC_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btbGC.Click
        If ds IsNot Nothing Then
            ds.Dispose()
            ds = Nothing
        End If
        GC.Collect()
        GC.WaitForPendingFinalizers()
        GC.Collect()
    End Sub

    Private Sub btnShow_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnShow.Click
        'objControl = New Global.CableSoft.SO.SL.Wip.Maintain.SO1112A()
        'Dim FLoginInfo As Object = App.LoginInfoWeb
        'ds = New Silverlight.DataSet
        'tb = New Silverlight.DataTable("Maintain")
        'tbContact = New Silverlight.DataTable("Contact")
        'tbContact.Columns.Add(New Silverlight.DataColumn("FaciSeqNo", GetType(String)))
        'tbContact.Columns.Add(New Silverlight.DataColumn("ServiceType", GetType(String)))
        'Dim rwContact As Silverlight.DataRow = tbContact.NewRow
        ''rwContact.Item("FaciSeqNo") = "201201160121640"
        'rwContact.Item("FaciSeqNo") = "201201160121640"
        'rwContact.Item("ServiceType") = "D"
        'tbContact.Rows.Add(rwContact)
        'tb.Columns.Add(New Silverlight.DataColumn("SNO"))
        'tb.Columns.Add(New Silverlight.DataColumn("CustId"))
        ''tb.Columns.Add(New Silverlight.DataColumn("ServiceType"))
        ''tb.Columns.Add(New Silverlight.DataColumn("FACISEQNO"))
        ''tb.Columns.Add(New Silverlight.DataColumn("ServiceType"))
        'Dim dr As Silverlight.DataRow = tb.NewRow
        ' ''159, "200609190189257"
        ''dr.Item("SNO") = "200401MD0270558"
        ''dr.Item("SNO") = "201203MD0319654"
        ''dr.Item("SNO") = "201203MD0319658"
        ''dr.Item("CustId") = 385713
        'dr.Item("SNO") = "201410MD0006273"
        'dr.Item("CustId") = 693320
        ''dr.Item("SNO") = "201205MI0045445"
        ''dr.Item("CustId") = 649610
        ''dr.Item("CustId") = 1050
        ''dr.Item("ServiceType") = "D"
        ' ''dr.Item("FACISEQNO") = "200609190189257"
        ''dr.Item("ServiceType") = "I"
        'tb.Rows.Add(dr)
        'ds.Tables.Add(tbContact)
        'ds.Tables.Add(tb)
        ''ds.Tables.Add(tbContact)
        ''Dim tbContact As New Silverlight.DataTable("Contact")
        ''tbContact.Columns.Add(New Silverlight.DataColumn("FaciSeqNo"))
        ''Dim rwContact As Silverlight.DataRow = tbContact.NewRow
        ''rwContact.Item("FaciSeqNo") = "XXXXX"
        ''tbContact.Rows.Add(rwContact)
        ''ds.Tables.Add(tbContact)

        'Dim aCollect As New System.Collections.Generic.Dictionary(Of String, Object)
        'aCollect.Add("Data".ToUpper, ds)
        ' ''Dim dtx As Silverlight.DataTable = CType(Parameters("DATA"), Silverlight.DataSet).Tables("ThiedDiscount")
        ' ''Dim CustId As Integer = dtx.Rows(0).Item("CustId")
        'objControl.LoginInfo = FLoginInfo
        'objControl.Parameters = aCollect
        'objControl.BatchFin = True
        'objControl.FinMode = True
        'objControl.IsAutoClosed = False

        'objControl.EditMode = IParams.EditModes.AddNew

        ''ShowChildWindow(objControl, New Size(900, 500))
        'objControl.CanAppend(ds, Sub(ariaResult As Boolean, aRiaXml As String)
        '                             If ariaResult Then
        '                                 'MessageBox.Show("yes")

        '                                 ShowChildWindow(objControl, New Size(900, 500))
        '                             Else
        '                                 MessageBox.Show(aRiaXml)
        '                             End If
        '                         End Sub)

    End Sub
    Private Sub ShowChildWindow(ByVal aChild As Object, ByVal aSize As Size)
        'Dim objShowWindow As New CSWindow()
        'AddHandler objShowWindow.Closed, Sub(s, arg)
        '                                     If TypeOf aChild Is Global.CableSoft.SO.SL.Wip.Maintain.SO1112A Then
        '                                         CType(aChild, Global.CableSoft.SO.SL.Wip.Maintain.SO1112A).Parameters.Clear()
        '                                         CType(aChild, Global.CableSoft.SO.SL.Wip.Maintain.SO1112A).Dispose()
        '                                         aChild = Nothing
        '                                         tb.Dispose()
        '                                         tb = Nothing
        '                                         tbContact.Dispose()
        '                                         tbContact = Nothing
        '                                         ds.Dispose()
        '                                         ds = Nothing
        '                                         objControl.Parameters.Clear()
        '                                         objControl.Dispose()
        '                                         objControl = Nothing
        '                                     End If


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
   
    Private Sub ACHIn_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        ThemeManager.ApplicationThemeName = Theme.Office2007BlueName
    End Sub
End Class
