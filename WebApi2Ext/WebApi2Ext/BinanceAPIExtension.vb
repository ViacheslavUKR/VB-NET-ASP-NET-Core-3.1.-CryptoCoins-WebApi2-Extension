Imports System.Runtime.CompilerServices
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

'https://github.com/binance-exchange/binance-official-api-docs/blob/master/rest-api.md
Public Module BinanceAPIExtension

    <Extension()>
    Public Function BinanceApiTrades(Binance As WebApi2, Symbol As String, ByRef BinanceTrades As List(Of TradeModel)) As Boolean
        Dim Trades = Binance.GetWithoutAU("/api/v3/trades?symbol=" & Symbol)
        If Trades.IsSuccess Then
            Dim TradesArr1 As JArray = JArray.Parse(Trades.Result)
            BinanceTrades = JsonConvert.DeserializeObject(Of List(Of TradeModel))(TradesArr1.ToString)
            Return True
        Else
            Console.WriteLine(Trades.Result & vbCrLf & Trades.Status.ToString & vbCrLf & CommonExt.FormatObjForPrint(Trades.Result) & vbCrLf & Trades.HeaderToString, MsgBoxStyle.Critical, "/api/v3/trades Error")
            Return False
        End If
    End Function

    <Extension()>
    Public Function BinanceApiExchangeInfo(Binance As WebApi2, ByRef BinanceExchangeSymbols As List(Of BinanceExchangeSymbol)) As Boolean
        Dim Info = Binance.GetWithoutAU("/api/v3/exchangeInfo")
        If Info.IsSuccess Then
            Dim TradesArr1 As JArray = JArray.Parse(Info.Result)
            BinanceExchangeSymbols = JsonConvert.DeserializeObject(Of List(Of BinanceExchangeSymbol))(TradesArr1.ToString)
            Return True
        Else
            Console.WriteLine(Info.Result & vbCrLf & Info.Status.ToString & vbCrLf & Info.HeaderToString, MsgBoxStyle.Critical, "/api/v3/exchangeInfo Error")
            Return False
        End If
    End Function

    <Extension()>
    Public Function BinanceApiTicker24hr(Binance As WebApi2, Symbol As String, ByRef AveragePrice As BinanceAveragePrice) As Boolean
        Dim Info = Binance.GetWithoutAU("/api/v3/ticker/24hr?symbol=" & Symbol)
        If Info.IsSuccess Then
            Dim TradesRes1 As JObject = JObject.Parse(Info.Result)
            AveragePrice = JsonConvert.DeserializeObject(Of BinanceAveragePrice)(TradesRes1.ToString)
            Return True
        Else
            Console.WriteLine(Info.Result & vbCrLf & Info.Status.ToString & vbCrLf & Info.HeaderToString, MsgBoxStyle.Critical, "/api/v3/exchangeInfo Error")
            Return False
        End If
    End Function

End Module
