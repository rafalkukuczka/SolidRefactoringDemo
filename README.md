# Solid Refactoring Demo - Runnable Console + Tests

This solution contains:

- `BadCode` - bad monolithic console example.
- `SolidCode` - SOLID version as a runnable console application.
- `SolidCode.Tests` - xUnit tests using Moq and FluentAssertions.

## Run SOLID console project

```bash
dotnet run --project SolidCode
```

Expected output:

```text
Order 1001 saved. Total: 1170.00
Confirmation email sent to customer@example.com. Total: 1170.00

SOLID order processing finished.
```

## Run tests

```bash
dotnet test
```

The tests verify:

- calculation logic
- discount rules
- service orchestration
- validation failures
- that repository and email sender are not called when validation fails

## Suggested article section

You can use this project as a downloadable companion for an article titled:

**Refactoring bad C# code with solid principles:**
https://pkey.info/knowledge-base/refactoring-bad-csharp-code-solid-principles/
