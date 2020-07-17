Imports System.Security.Cryptography
Imports Newtonsoft.Json

Public Class CommonExt
    Public Shared Function FormatLoginToken(CurrentLoginToken As LoginToken) As String
        Dim X As New System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler
        Dim AccessToken = X.ReadJwtToken(CurrentLoginToken.access_token)
        Dim RefreshToken = X.ReadJwtToken(CurrentLoginToken.refresh_token)
        Dim AccessTokenFormated = JsonConvert.SerializeObject(AccessToken.Payload, Formatting.Indented, WebApi2.DoubleNumericFormaterSerializerSettings)
        Dim RefreshTokenFormated = JsonConvert.SerializeObject(RefreshToken.Payload, Formatting.Indented, WebApi2.DoubleNumericFormaterSerializerSettings)
        Return "Expired:" & CurrentLoginToken.expire_token.ToString & vbCrLf & AccessTokenFormated & vbCrLf & RefreshTokenFormated
    End Function

    Public Shared Function FormatObjForPrint(Json As Object) As String
        Return JsonConvert.SerializeObject(Json, Formatting.Indented, WebApi2.DoubleNumericFormaterSerializerSettings)
    End Function

    Public Shared Function Create_HMACSHA256_Sign(ByVal SecretKey As String, ByVal Message As String) As String
        Dim Encoding = New Text.ASCIIEncoding()
        Dim KeyByte As Byte() = Encoding.GetBytes(SecretKey)
        Dim MessageBytes As Byte() = Encoding.GetBytes(Message)
        Using Hmacsha256 = New HMACSHA256(KeyByte)
            Dim HashBytes As Byte() = Hmacsha256.ComputeHash(MessageBytes)
            Return BitConverter.ToString(HashBytes).Replace("-", "").ToLower()
        End Using
    End Function

    Public Function FormatJson(Json As Object) As String
        Return JsonConvert.SerializeObject(Json.Payload, Formatting.Indented, WebApi2.DoubleNumericFormaterSerializerSettings)
    End Function
End Class
