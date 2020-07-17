
Public Class LoginToken
    Property login As String
    Property access_token As String
    Property expire_token As DateTime
    Property refresh_token As String
End Class

Public Class TradeModel
    Property id As Long
    Property price As Double
    Property qty As Double
    Property quoteQty As Double
    <Newtonsoft.Json.JsonConverter(GetType(JavascriptTimestampMicrosecondConverter))>
    Property time As DateTime
    Property isBuyerMaker As Boolean
    Property isBestMatch As Boolean
End Class

Public Class BinanceExchangeSymbol
    Property symbol As String
    Property baseAsset As String
    Property quoteAsset As String
    Property status As String
End Class

Public Class BinanceAveragePrice
    Property symbol As String
    Property priceChange As Double
    Property priceChangePercent As Double
    Property weightedAvgPrice As Double
    Property prevClosePrice As Double
    Property lastPrice As Double
    Property lastQty As Double
    Property bidPrice As Double
    Property bidQty As Double
    Property askPrice As Double
    Property askQty As Double
    Property openPrice As Double
    Property highPrice As Double
    Property lowPrice As Double
    Property volume As Double
    Property quoteVolume As Double
    <Newtonsoft.Json.JsonConverter(GetType(JavascriptTimestampMicrosecondConverter))>
    Property openTime As DateTime
    <Newtonsoft.Json.JsonConverter(GetType(JavascriptTimestampMicrosecondConverter))>
    Property closeTime As DateTime
    Property firstId As Long
    Property lastId As Long
    Property count As Long
End Class
