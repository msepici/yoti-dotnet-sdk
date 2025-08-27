# Codex Agent Hints

Project: Yoti .NET SDK (IDV Web SDK parts inside /src)  
Language: C#, .NET

## Scope & Guardrails
- In the Codex sandbox, shell commands (rg, grep, find, ls, etc.) are NOT available.
- Do NOT execute any shell commands or spawn processes.
- Discover and open files by recursively listing and globbing within the current workspace ONLY.
- Read/write files directly and edit files **in-place** inside this repository (no /tmp patches).
- This repository is a .NET/C# SDK. Focus ONLY on C#; ignore Java/JS/TS/Python/Go.

## Where to look
- C# models & builders: `/src/**/**.cs`
- Tests (xUnit): `/tests/**/**.cs`
- Config classes likely contain `Config` in the class name (e.g., `SdkConfig`).
- Policy/request builders under policy-related namespaces.

## Build & test
- Build: `dotnet build`
- Test:  `dotnet test`

## Conventions
- Properties use PascalCase; nullable reference types as in existing code.
- JSON serialization: `System.Text.Json` with `[JsonPropertyName]` when needed.
- Prefer minimal diffs that match existing style.

## Safety
- Don’t run network calls.
- Keep public API changes backward compatible.
