Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports Newtonsoft.Json

Public Class WebApi2Result
    Property IsSuccess As Boolean
    Property Result As String
    Property Headers As HttpResponseHeaders
    Property Status As HttpStatusCode
    Public Sub New(Response As HttpResponseMessage)
        If Response.Content IsNot Nothing Then
            Result = Response.Content.ReadAsStringAsync().Result 'sync, Await without .Result
        End If
        Headers = Response.Headers
        Status = Response.StatusCode
        If Response.IsSuccessStatusCode Then
            IsSuccess = True
        Else
            IsSuccess = False
        End If
    End Sub

    Public Function HeaderToString() As String
        Dim Str1 As New StringBuilder
        If Headers IsNot Nothing Then
            For Each One In Headers
                Str1.AppendLine()
                Str1.Append(One.Key & " : ")
                For Each Val As String In One.Value
                    Str1.Append(Val & ",")
                Next
                Str1.Length = Str1.Length - 1
            Next
            Return Str1.ToString
        Else
            Return ""
        End If

    End Function

End Class

Public Class WebApi2

    Dim BaseApiURL As String
    Dim Client As HttpClient
    Public Shared Property DoubleNumericFormaterSerializerSettings = New JsonSerializerSettings With {.Converters = {New FormatNumbersAsTextConverter()}}
    Public Shared Property JavascriptTimestampMicrosecondSerializerSettings = New JsonSerializerSettings With {.Converters = {New JavascriptTimestampMicrosecondConverter()}}

    Public Sub New(BaseURL As String)
        BaseApiURL = BaseURL
        Client = New HttpClient()
        Client.BaseAddress = New Uri(BaseApiURL)
        Client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
    End Sub

    Function Err1(Ex As Exception, Response As HttpResponseMessage) As WebApi2Result
        Dim Ret1 As New WebApi2Result(New HttpResponseMessage(HttpStatusCode.SeeOther))
        Ret1.IsSuccess = False
        If Response IsNot Nothing Then
            Ret1.Status = Response.StatusCode
            Ret1.Headers = Response.Headers
        End If
        Ret1.Result = Ex.Message
        If Ex.InnerException IsNot Nothing Then
            Ret1.Result = Ex.Message & vbCrLf & Ex.InnerException.ToString
        End If
        Return Ret1
    End Function

    Public Function GetWithoutAU(ApiPoint As String) As WebApi2Result
        Dim Response As HttpResponseMessage
        Try
            Response = Client.GetAsync(ApiPoint).Result           'sync, Await without .Result
            Return New WebApi2Result(Response)
        Catch ex As Exception
            Return Err1(ex, Response)
        End Try
    End Function

    Public Function Post(ApiPoint As String, InputJsonObject As Object) As WebApi2Result
        Dim Response As HttpResponseMessage
        Try
            'Dim JsonString As String = Serializer.Serialize(InputJsonObject)
            Dim JsonString As String = JsonConvert.SerializeObject(InputJsonObject, DoubleNumericFormaterSerializerSettings)
            Dim Content = New StringContent(JsonString, Encoding.UTF8, "application/json")
            Response = Client.PostAsync(ApiPoint, Content).Result 'sync, Await without .Result
            Return New WebApi2Result(Response)
        Catch ex As Exception
            Return Err1(ex, Response)
        End Try
    End Function

    Public Function GetWithBearerHeader(ApiPoint As String, BearerToken As String) As WebApi2Result
        Dim Response As HttpResponseMessage
        Try
            Client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", BearerToken)
            Response = Client.GetAsync(ApiPoint).Result           'sync, Await without .Result
            Return New WebApi2Result(Response)
        Catch ex As Exception
            Return Err1(ex, Response)
        End Try
    End Function

    Public Function PostWithBearerHeader(ApiPoint As String, BearerToken As String) As WebApi2Result
        Dim Response As HttpResponseMessage
        Try
            Client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", BearerToken)
            Response = Client.PostAsync(ApiPoint, New StringContent("")).Result 'sync, Await without .Result
            Return New WebApi2Result(Response)
        Catch ex As Exception
            Return Err1(ex, Response)
        End Try
    End Function

    Public Function PostWithBearerHeader(ApiPoint As String, BearerToken As String, InputJsonObject As Object) As WebApi2Result
        Dim Response As HttpResponseMessage
        Try
            Client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", BearerToken)
            Dim JsonString As String = JsonConvert.SerializeObject(InputJsonObject, DoubleNumericFormaterSerializerSettings)
            'Dim JsonString As String = Serializer.Serialize(InputJsonObject)
            Dim Content = New StringContent(JsonString, Encoding.UTF8, "application/json")
            Response = Client.PostAsync(ApiPoint, Content).Result 'sync, Await without .Result
            Return New WebApi2Result(Response)
        Catch ex As Exception
            Return Err1(ex, Response)
        End Try
    End Function

    Public Function PutWithBearerHeader(ApiPoint As String, BearerToken As String, InputJsonObject As Object) As WebApi2Result
        Dim Response As HttpResponseMessage
        Try
            Client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", BearerToken)
            Dim JsonString As String = JsonConvert.SerializeObject(InputJsonObject, DoubleNumericFormaterSerializerSettings)
            'Dim JsonString As String = Serializer.Serialize(InputJsonObject)
            Dim Content = New StringContent(JsonString, Encoding.UTF8, "application/json")
            Response = Client.PutAsync(ApiPoint, Content).Result 'sync, Await without .Result
            Return New WebApi2Result(Response)
        Catch ex As Exception
            Return Err1(ex, Response)
        End Try
    End Function

    Public Function DeleteWithBearerHeader(ApiPoint As String, BearerToken As String, InputJsonObject As Object) As WebApi2Result
        Dim Response As HttpResponseMessage
        Try
            Client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", BearerToken)
            Dim JsonString As String = JsonConvert.SerializeObject(InputJsonObject, DoubleNumericFormaterSerializerSettings)
            'Dim JsonString As String = Serializer.Serialize(InputJsonObject)
            Dim Content = New StringContent(JsonString, Encoding.UTF8, "application/json")
            Response = Client.SendAsync(New HttpRequestMessage(HttpMethod.Delete, ApiPoint) With {.Content = Content}).Result
            Return New WebApi2Result(Response)
        Catch ex As Exception
            Return Err1(ex, Response)
        End Try
    End Function

    Public Function GetWithAPIKeyHeader(ApiPoint As String, XAPIKeyToken As String) As WebApi2Result
        Dim Response As HttpResponseMessage
        Try
            Client.DefaultRequestHeaders.Clear()
            Client.DefaultRequestHeaders.Add("X-API-Key", XAPIKeyToken)
            Response = Client.GetAsync(ApiPoint).Result           'sync, Await without .Result
            Return New WebApi2Result(Response)
        Catch ex As Exception
            Return Err1(ex, Response)
        End Try
    End Function

    Public Function PutWithAPIKeyHeader(ApiPoint As String, XAPIKeyToken As String, InputJsonObject As Object) As WebApi2Result
        Dim Response As HttpResponseMessage
        Try
            Client.DefaultRequestHeaders.Clear()
            Client.DefaultRequestHeaders.Add("X-API-Key", XAPIKeyToken)
            Dim JsonString As String = JsonConvert.SerializeObject(InputJsonObject, DoubleNumericFormaterSerializerSettings)
            Dim Content = New StringContent(JsonString, Encoding.UTF8, "application/json")
            Response = Client.PutAsync(ApiPoint, Content).Result 'sync, Await without .Result
            Return New WebApi2Result(Response)
        Catch ex As Exception
            Return Err1(ex, Response)
        End Try
    End Function

    Public Function PostWithAPIKeyHeader(ApiPoint As String, XAPIKeyToken As String, InputJsonObject As Object) As WebApi2Result
        Dim Response As HttpResponseMessage
        Try
            Client.DefaultRequestHeaders.Clear()
            Client.DefaultRequestHeaders.Add("X-API-Key", XAPIKeyToken)
            Dim JsonString As String = JsonConvert.SerializeObject(InputJsonObject, DoubleNumericFormaterSerializerSettings)
            Dim Content = New StringContent(JsonString, Encoding.UTF8, "application/json")
            Response = Client.PostAsync(ApiPoint, Content).Result 'sync, Await without .Result
            Return New WebApi2Result(Response)
        Catch ex As Exception
            Return Err1(ex, Response)
        End Try
    End Function

    Public Function DeleteWithAPIKeyHeader(ApiPoint As String, XAPIKeyToken As String, InputJsonObject As Object) As WebApi2Result
        Dim Response As HttpResponseMessage
        Try
            Client.DefaultRequestHeaders.Clear()
            Client.DefaultRequestHeaders.Add("X-API-Key", XAPIKeyToken)
            Dim JsonString As String = JsonConvert.SerializeObject(InputJsonObject, DoubleNumericFormaterSerializerSettings)
            Dim Content = New StringContent(JsonString, Encoding.UTF8, "application/json")
            Response = Client.SendAsync(New HttpRequestMessage(HttpMethod.Delete, ApiPoint) With {.Content = Content}).Result
            Return New WebApi2Result(Response)
        Catch ex As Exception
            Return Err1(ex, Response)
        End Try
    End Function


End Class


Friend NotInheritable Class FormatNumbersAsTextConverter
    Inherits JsonConverter

    Public Overrides ReadOnly Property CanRead As Boolean = False
    Public Overrides ReadOnly Property CanWrite As Boolean = True

    ' <NullableAttribute(2)>
    Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
        writer.WriteValue(String.Format("{0:#######0.########}", value))
    End Sub

    ' <NullableAttribute(2)>
    Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
        Throw New NotImplementedException()
    End Function

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        Return objectType Is GetType(Double)
    End Function
End Class


Public Class JavascriptTimestampMicrosecondConverter
    Inherits JsonConverter

    Public Overrides ReadOnly Property CanRead As Boolean = True
    Public Overrides ReadOnly Property CanWrite As Boolean = True

    Private Shared ReadOnly _epoch As DateTime = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)

    Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
        writer.WriteRawValue((CType(value, DateTime) - _epoch).TotalMilliseconds)
    End Sub

    Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
        If reader.Value Is Nothing Then
            Return Nothing
        Else
            Return _epoch.AddMilliseconds(CLng(reader.Value)) 'double precizion float
        End If
    End Function

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        Return objectType Is GetType(DateTime)
    End Function
End Class