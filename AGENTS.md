Codex Agent Hints

Project: Yoti .NET SDK (IDV Web SDK parts inside /src)
Language: C#, .NET

Scope & Guardrails (CRITICAL)
- NEVER execute shell commands: rg, grep, find, ls, cat, sed, awk, etc. are FORBIDDEN.
- No process spawning (no scripts, no external tools).
- File discovery and read/write is limited strictly to the current workspace, using recursive globbing and the ### FILE INDEX provided in the prompt.
- This repository is a .NET/C# SDK. Only work with C#. Ignore Java/JS/TS/Python/Go.

File access rules
- Only open and edit files that are explicitly listed under ### FILE INDEX.
- Do not guess or invent file paths. If a required file is not in the index, stop and report.
- Apply changes in-place inside the repository. Do not write to /tmp or other external paths.

Where to look
- C# models & builders: /src/**/**.cs
- Tests (xUnit): /tests/**/**.cs
- Config classes typically contain "Config" in their name (e.g., SdkConfig).
- Policy/request builders are under policy-related namespaces.
- Note: The ** glob is recursive with unlimited depth; it includes nested folders at any level (2–3–4–5+).

Build & test (for reference only)
- Build: dotnet build
- Test:  dotnet test
Note: Shell commands are FORBIDDEN. These commands are only informational, do not execute them.

Conventions
- Properties use PascalCase; follow the existing style with nullable reference types.
- JSON serialization uses Newtonsoft.Json with [JsonProperty(PropertyName = "...")].
- Prefer minimal diffs that match the existing code style.

Safety
- Do not make network calls.
- Maintain backward compatibility in public API changes.

Notes for Codex (IMPORTANT)
- Do not attempt to use shell commands (e.g., rg -A 20). This causes errors. Files must be located through recursive globbing and file content search only.
- Apply patches file by file, with focused and minimal changes. Keep style and formatting consistent with existing code.
