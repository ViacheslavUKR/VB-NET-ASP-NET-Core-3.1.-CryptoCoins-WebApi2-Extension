Imports System
Imports WebApi2Ext

Module Program

    Dim BinanceTrades2 As List(Of TradeModel)
    Dim BinanceExchangeSymbols As List(Of BinanceExchangeSymbol)
    Dim BTCCost As Double

    Dim Coingecko As WebApi2
    Dim Binance As WebApi2




    Sub Main(args As String())

        Binance = New WebApi2("https://www.binance.com")
        Coingecko = New WebApi2("https://api.coingecko.com")

        GetBinanceExchangeInfo()
        GetBTCCurrentAveragePrice()


    End Sub

#Region "Tick=0, Fixed or Dynamic ExcangeInfo from Binance"
    Sub GetBinanceExchangeInfo()
        If Not Binance.BinanceApiExchangeInfo(BinanceExchangeSymbols) Then
            ParseFixedExchangeInfo()
        End If
    End Sub

    Sub ParseFixedExchangeInfo()
        BinanceExchangeSymbols = New List(Of BinanceExchangeSymbol)
        Dim StrArr() As String = IO.File.ReadAllLines(IO.Path.Combine(IO.Directory.GetCurrentDirectory(), "BinanceExcangeInfo.txt"))
        For Each OneLine In StrArr
            Dim Arr1() As String = OneLine.Split("!")
            BinanceExchangeSymbols.Add(New BinanceExchangeSymbol With {.baseAsset = Arr1(0), .quoteAsset = Arr1(1), .status = Arr1(2), .symbol = Arr1(3)})
        Next
    End Sub
#End Region

#Region "Tick=1"

    Dim BTCUSDT As BinanceAveragePrice
    Sub GetBTCCurrentAveragePrice()
        BTCUSDT = New BinanceAveragePrice
        If Binance.BinanceApiTicker24hr("BTCUSDT", BTCUSDT) Then
            BTCCost = BTCUSDT.lastPrice
        End If
    End Sub
#End Region


End Module
