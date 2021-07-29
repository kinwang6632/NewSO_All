
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


'TODO: 建立包含應用程式邏輯的方法。
<EnableClientAccess()>  _
Public Class IDData
    Inherits DomainService
End Class

