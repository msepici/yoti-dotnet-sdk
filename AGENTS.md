# Codex Agent Hints

Project: Yoti .NET SDK (IDV Web SDK parts under /src)  
Language: C#, .NET

## Scope & Guardrails
- Shell komutları YASAK: rg/grep/find/ls/cat vb. çalıştırma; süreç başlatma yok.
- Dosyaları yalnızca bu çalışma alanında aç/kaydet; in-place düzenle (kesinlikle /tmp patch yok).
- Bu repo .NET/C#; yalnızca C# dosyalarına odaklan (Java/JS/TS/Python/Go yok).

## File Discovery (Very Important)
- Dosya keşfi için prompt içinde sağlanan ### FILE INDEX’i kullan.
- Sadece FILE INDEX içinde listelenen dosyaları aç/düzenle.
- Gerekli dosya FILE INDEX’te yoksa tahmin etme; bunun yerine raporla.

## Where to look
- C# models & builders: /src/**/*.cs
- Tests (xUnit): /test/**/*.cs
- Konfig sınıfları genelde adında Config geçer (örn. SdkConfig).
- Policy/request builder’lar ilgili namespace klasörleri altında bulunur (örn. ...Session/Create/...).

## Build & Test (bilgi amaçlı)
- Build: dotnet build
- Test:  dotnet test
> Not: Sandbox’ta bu komutları çalıştırma; sadece proje stiline uygun kod ve test yaz.

## Conventions
- PascalCase özellikler, mevcut nullability örüntüsünü takip et.
- JSON: System.Text.Json + [JsonPropertyName]; gerekli ise  
  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)].
- Mevcut tarz/formatter ile minimal diff; public API’de breaking chang
