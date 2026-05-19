# External Market Analysis

> A C# ASP.NET microservice that consumes public international broker APIs to perform detailed financial calculations on open-cap market assets, delivering both a human-readable summary and raw analytical data.

---

## 📌 Overview

External Market Analysis is a financial data microservice focused on the stock market. It connects to public APIs from international brokers and exchanges to pull real-time and historical market data, runs a set of detailed quantitative calculations and returns the results in two layers: a simplified, easy-to-read summary and a full structured dataset for programmatic consumption.

The service is designed as a standalone microservice — stateless, fast and ready to integrate with any frontend, dashboard or internal tooling via its REST API.

---

## ✨ Features

- **Multi-broker integration** — connects to public APIs from international brokers with open cap market access (stocks, ETFs, indices)
- **Real-time and historical data** — fetch current quotes and historical price series for any supported asset
- **Comprehensive financial calculations** — volatility, moving averages, RSI, momentum, price variation, volume analysis and more
- **Dual output format** — plain-language summary (easy to read) + full structured JSON (for dashboards and further processing)
- **Asset comparison** — compare multiple tickers side by side on any metric
- **Configurable analysis depth** — choose time window, indicators and output verbosity per request
- **Caching layer** — avoid redundant API calls with configurable TTL per data source

---

## 🖥️ Tech Stack

| Component | Technology |
|-----------|-----------|
| Framework | C# ASP.NET Core (.NET 8) |
| HTTP Client | HttpClient + Polly (retry/circuit breaker) |
| Serialization | System.Text.Json |
| Caching | IMemoryCache / Redis |
| API Docs | Swagger / OpenAPI |
| Containerization | Docker |

---

## 🗂️ Project Structure

```
external-market-analysis/
├── src/
│   ├── ExternalMarket.API/               # ASP.NET Core entry point
│   │   ├── Controllers/
│   │   │   ├── AssetController.cs         # Single asset analysis
│   │   │   ├── ComparisonController.cs    # Multi-asset comparison
│   │   │   └── MarketController.cs        # Market-wide indicators
│   │   └── Program.cs
│   │
│   ├── ExternalMarket.Application/        # Business logic layer
│   │   ├── Services/
│   │   │   ├── MarketDataService.cs       # Data fetching orchestration
│   │   │   ├── AnalysisService.cs         # Calculation pipeline
│   │   │   └── SummaryService.cs          # Human-readable output generator
│   │   ├── UseCases/
│   │   │   ├── AnalyzeAsset/
│   │   │   └── CompareAssets/
│   │   └── DTOs/
│   │
│   ├── ExternalMarket.Domain/             # Core models and calculation logic
│   │   ├── Models/
│   │   │   ├── Asset.cs
│   │   │   ├── Quote.cs
│   │   │   ├── HistoricalSeries.cs
│   │   │   └── AnalysisResult.cs
│   │   └── Calculators/
│   │       ├── MovingAverageCalculator.cs
│   │       ├── RSICalculator.cs
│   │       ├── VolatilityCalculator.cs
│   │       ├── MomentumCalculator.cs
│   │       └── VolumeAnalyzer.cs
│   │
│   └── ExternalMarket.Infrastructure/    # External API integrations
│       ├── Brokers/
│       │   ├── IBrokerClient.cs
│       │   ├── AlphaVantageClient.cs
│       │   ├── YahooFinanceClient.cs
│       │   └── BrApiClient.cs
│       └── Cache/
│           └── MarketDataCache.cs
│
├── tests/
│   ├── Unit/
│   └── Integration/
│
├── docker-compose.yml
└── README.md
```

---

## 📐 Calculations & Indicators

The analysis pipeline runs the following calculations on the fetched data:

### Price Analysis
| Indicator | Description |
|-----------|-------------|
| Current price | Latest quote with bid/ask spread |
| Daily variation | Absolute and percentage change vs previous close |
| 52-week high/low | Range extremes with dates |
| Distance from high/low | % distance from current price to 52w extremes |

### Moving Averages
| Indicator | Periods |
|-----------|---------|
| SMA (Simple) | 9, 20, 50, 200 days |
| EMA (Exponential) | 9, 21, 50 days |
| Golden/Death Cross detection | SMA50 vs SMA200 |
| Price position relative to MAs | Above / below with % distance |

### Momentum & Trend
| Indicator | Description |
|-----------|-------------|
| RSI (14) | Overbought / oversold classification |
| MACD | Line, signal, histogram and crossover detection |
| Momentum (10) | Rate of price change |
| Trend classification | Strong up / up / neutral / down / strong down |

### Volatility
| Indicator | Description |
|-----------|-------------|
| Historical volatility | Annualized std deviation of log returns |
| ATR (Average True Range) | 14-day true range average |
| Beta | Correlation vs market index (if available) |

### Volume
| Indicator | Description |
|-----------|-------------|
| Average volume | 10 and 30-day averages |
| Volume ratio | Current vs 30-day average |
| Volume trend | Increasing / decreasing classification |

---

## 📤 Output Format

The API returns two response sections for every analysis:

### Summary (plain-language)

```json
{
  "summary": {
    "ticker": "AAPL",
    "signal": "Bullish",
    "overview": "Apple is trading at $189.42, up 1.8% today. The asset is above its 50 and 200-day moving averages, suggesting a sustained uptrend. RSI at 61 indicates room before overbought territory. Volume is 23% above its 30-day average, confirming buyer interest.",
    "key_points": [
      "Price above SMA50 and SMA200 — bullish structure",
      "RSI at 61 — neutral to slightly overbought",
      "Volume spike of +23% today — above average demand",
      "22% below 52-week high — potential recovery room"
    ],
    "risk_level": "Moderate"
  }
}
```

### Full data

```json
{
  "data": {
    "ticker": "AAPL",
    "price": {
      "current": 189.42,
      "change_today": 3.34,
      "change_percent": 1.80,
      "high_52w": 232.15,
      "low_52w": 164.08,
      "distance_from_high_pct": -18.4,
      "distance_from_low_pct": 15.4
    },
    "moving_averages": {
      "sma_20": 182.10,
      "sma_50": 178.65,
      "sma_200": 171.30,
      "ema_9": 186.90,
      "price_vs_sma200_pct": 10.58,
      "golden_cross_active": true
    },
    "momentum": {
      "rsi_14": 61.2,
      "rsi_signal": "Neutral",
      "macd_line": 3.45,
      "macd_signal": 2.80,
      "macd_histogram": 0.65,
      "macd_crossover": "Bullish"
    },
    "volatility": {
      "historical_volatility_annualized": 0.2341,
      "atr_14": 4.12
    },
    "volume": {
      "current": 72400000,
      "avg_30d": 58900000,
      "ratio_vs_avg": 1.23,
      "trend": "Increasing"
    }
  }
}
```

---

## 🔌 API Endpoints

### Single asset analysis

```
GET /api/asset/{ticker}/analysis?window=90d&indicators=all
```

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `ticker` | string | required | Asset ticker symbol (e.g. AAPL, PETR4) |
| `window` | string | `90d` | Historical data window (7d, 30d, 90d, 1y, 5y) |
| `indicators` | string | `all` | Comma-separated list or `all` |

### Asset comparison

```
GET /api/comparison?tickers=AAPL,MSFT,GOOGL&metric=rsi,volatility,momentum
```

### Current quote

```
GET /api/asset/{ticker}/quote
```

### Market overview

```
GET /api/market/overview?index=SP500
```

---

## 🌐 Supported Data Sources

| Source | Data Type | Notes |
|--------|-----------|-------|
| Alpha Vantage | Real-time + historical | Free tier available |
| Yahoo Finance (unofficial) | Real-time + historical | No API key required |
| BrAPI | Brazilian market (B3) | Free tier available |
| Twelve Data | Real-time + technical | Free tier available |

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- API key for at least one data source (see `.env.example`)

### Installation

```bash
git clone https://github.com/augustovcs/external-market-analysis
cd external-market-analysis
cp .env.example .env
# Add your API keys to .env
dotnet restore
dotnet run --project src/ExternalMarket.API
```

### Running with Docker

```bash
docker-compose up -d
```

Access Swagger at `http://localhost:5000/swagger`.

### Configuration

```json
// appsettings.json
{
  "MarketData": {
    "PrimarySource": "AlphaVantage",
    "FallbackSource": "YahooFinance",
    "CacheTtlSeconds": 300
  },
  "ApiKeys": {
    "AlphaVantage": "",
    "TwelveData": "",
    "BrApi": ""
  }
}
```

---

## 🗺️ Roadmap

- [x] Real-time quote fetching
- [x] Historical price series
- [x] Moving averages (SMA, EMA)
- [x] RSI and MACD
- [x] Volatility analysis
- [x] Volume analysis
- [x] Plain-language summary output
- [ ] Portfolio analysis (multiple tickers as a group)
- [ ] Dividend history and yield analysis
- [ ] Sector and peer comparison
- [ ] Price target projection (linear regression)
- [ ] Webhook alerts on indicator thresholds
- [ ] Frontend dashboard (React + Recharts)

---

## ⚠️ Disclaimer

This project is for educational and informational purposes only. The data and calculations provided do not constitute financial advice. Always consult a licensed financial professional before making investment decisions.

---

## 📄 License

MIT © [augustovcs](https://github.com/augustovcs)
