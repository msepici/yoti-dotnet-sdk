# Codex Agent Hints

Project: Yoti .NET SDK (IDV Web SDK parts inside /src)
Language: C#, .NET

Where to look:
- C# models & builders live under: /src/**/**.cs
- Config classes likely contain "Config" in the class name, e.g., SdkConfig
- Tests are under: /tests/**/**.cs (xUnit)

Build & test:
- Build: dotnet build
- Test:  dotnet test

Conventions:
- Properties use PascalCase and nullable reference types as in existing code.
- JSON serialization uses System.Text.Json with [JsonPropertyName] when needed.

Safety:
- Don’t run network calls. Use local build/test commands only.
- Prefer minimal diffs that match existing style.

