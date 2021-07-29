' 注意: 您可以使用內容功能表上的 [重新命名] 命令同時變更程式碼和組態檔中的介面名稱 "IService1"。
<ServiceContract()>
Public Interface IDataGateway

    <OperationContract()>
    Function GetData(ByVal value As String) As String
    <OperationContract()>
    Function UploadFile(ByVal f As Byte(), ByVal path As String, ByVal FileName As String) As String
    <OperationContract()>
    Function DownloadFile(ByVal FileName As String) As Byte()

End Interface

