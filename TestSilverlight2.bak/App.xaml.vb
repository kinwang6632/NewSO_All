Imports System.ServiceModel.DomainServices.Client
Imports TestSilverlight2.Web

Partial Public Class App
    Inherits Application
    'Public Shared LoginInfoWeb As New Web.LoginInfo
    Public Shared LoginInfoWeb As New CableSoft.BLL.Utility.LoginInfo
    Private fComplete As Boolean = False
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Application_Startup(ByVal o As Object, ByVal e As StartupEventArgs) Handles Me.Startup
        Me.RootVisual = New MainPage
        Dim strCompCode As String = "3"
        Dim strEntryId As String = "A"
        Dim strEntryName As String = "TEST"
        Dim GroupId As String = "0"
        '((WebDomainClient<LibraryDomainContext.ILibraryDomainServiceContract>)this.DomainClient).ChannelFactory.Endpoint.Binding.SendTimeout = new TimeSpan(0, 5, 0)


        'If Not e.InitParams("userid") Is Nothing Then
        '    strEntryId = e.InitParams("userid")
        'End If
        'If Not e.InitParams("groupid") Is Nothing Then
        '    GroupId = e.InitParams("groupid")
        'End If
        'If Not e.InitParams("password") Is Nothing Then
        '    'strEntryId = e.InitParams("password")
        'End If
        'If Not e.InitParams("username") Is Nothing Then
        '    strEntryName = e.InitParams("username")
        'End If
        'If Not e.InitParams("compcode") Is Nothing Then
        '    strCompCode = e.InitParams("compcode")
        'End If
        fComplete = False
        LoginInfoWeb.CompCode = CInt(strCompCode)
        LoginInfoWeb.EntryId = strEntryId
        LoginInfoWeb.EntryName = strEntryName
        LoginInfoWeb.DebugMode = True
        LoginInfoWeb.GroupId = GroupId
        LoginInfoWeb.RIATimeout = 600
        Dim conn As Connect = New Connect
        'conn.GetConfigInfo(LoginInfoWeb.CompCode)
        Dim dtimer As Windows.Threading.DispatcherTimer = New Windows.Threading.DispatcherTimer()

        Dim result As InvokeOperation(Of ConnectInfo) = conn.GetConfigInfo(LoginInfoWeb.CompCode)
        AddHandler result.Completed, Sub(s, arg)
                                         LoginInfoWeb.Provider = result.Value.Provider
                                         LoginInfoWeb.ConnectionString = result.Value.ConntionString
                                         LoginInfoWeb.DebugMode = result.Value.DebugMode
                                         LoginInfoWeb.HttpPath = result.Value.HttpPath
                                         LoginInfoWeb.ReportServicePath = result.Value.ReoprtServiceUrl

                                         fComplete = True
                                     End Sub

        dtimer.Interval = New TimeSpan(0, 0, 0, 0, 100)
        AddHandler dtimer.Tick, Sub(s, args)
                                    If fComplete Then
                                        Me.RootVisual = New MainPage()
                                        dtimer.Stop()
                                    End If
                                End Sub
        dtimer.Start()
     
    End Sub

    Private Sub Application_UnhandledException(ByVal sender As Object, ByVal e As ApplicationUnhandledExceptionEventArgs) Handles Me.UnhandledException

        ' 如果應用程式是在偵錯工具外執行，則使用瀏覽器的例外機制
        ' 報告例外狀況。在 IE 中，這會顯示為狀態列中的黃色提醒圖示，
        ' 而 Firefox 則會顯示指令碼錯誤。
        If Not System.Diagnostics.Debugger.IsAttached Then

            ' 注意: 這樣可以讓應用程式在有例外狀況擲回但未處理的情況下
            ' 繼續執行。
            ' 對於生產應用程式，這個錯誤處理方式應該置換成會向網站
            ' 報告錯誤並停用應用程式的程序。
            e.Handled = True
            Dim errorWindow As ChildWindow = New ErrorWindow(e.ExceptionObject)
            errorWindow.Show()
        End If
    End Sub

End Class
