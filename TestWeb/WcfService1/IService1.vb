' 注意: 您可以使用操作功能表上的 [重新命名] 命令同時變更程式碼和組態檔中的介面名稱 "IService1"。
<ServiceContract()>
Public Interface IService1

    <OperationContract()>
    Function GetData(ByVal value As Integer) As String

    <OperationContract()>
    Function GetDataUsingDataContract(ByVal composite As CompositeType) As CompositeType

    ' TODO: 在此新增您的服務作業

End Interface

'使用下列範例中所示的資料合約，新增複合型別至服務作業。

<DataContract()>
Public Class CompositeType

    <DataMember()>
    Public Property BoolValue() As Boolean

    <DataMember()>
    Public Property StringValue() As String

End Class
