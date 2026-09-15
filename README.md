# ISO8601 Helper

An **OutSystems Developer Cloud (ODC)** external library that exposes ISO 8601 date/time utilities as server actions.

## Overview

The library wraps common ISO 8601 operations — week number calculation, year boundary detection, and date formatting — making them available natively inside ODC apps and libraries.

## Server Actions

| Action | Description | Input | Output |
|--------|-------------|-------|--------|
| `ISO8601_FormatDateTime` | Converts a DateTime to ISO 8601 string (`yyyy-MM-ddTHH:mm:ssZ`) | `DateTime value` | `Text FormatedDateTime` |
| `ISO8601_GetWeekNumber` | Returns the ISO 8601 week number for a given date | `DateTime value` | `Integer WeekNumber` |
| `ISO8601_GetLastWeekOfYear` | Returns the last ISO 8601 week number of a given year (52 or 53) | `Integer year` | `Integer LastWeekNumber` |
| `ISO8601_GetStartOfWeek` | Returns the Monday that starts the given ISO 8601 year/week | `Integer year`, `Integer week_number` | `DateTime StartDateTime` |

## Project Structure

```
ISO8601_Helper/
├── ISO8601_Helper/               # Library source
│   ├── IISO8601_Helper.cs        # ODC interface (exposed actions)
│   ├── ISO8601_Helper.cs         # Implementation
│   ├── ISO8601_Helper.csproj
│   ├── ISO8601_Helper.sln
│   └── resources/
│       └── ISO8601.png
└── ISO8601_Helper.UnitTests/     # xUnit test project
    ├── ISO8601_HelperTests.cs
    └── ISO8601_Helper.UnitTests.csproj
```

## Requirements

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [OutSystems.ExternalLibraries.SDK](https://www.nuget.org/packages/OutSystems.ExternalLibraries.SDK) 1.3.2

## Build

```bash
dotnet build ISO8601_Helper/ISO8601_Helper.sln
```

## Run Unit Tests

```bash
dotnet test ISO8601_Helper.UnitTests/ISO8601_Helper.UnitTests.csproj
```

All **34 tests** cover:
- Correct ISO 8601 output format and UTC suffix
- Week number for known boundary dates (year transitions, long years)
- Correct identification of 52- vs 53-week years
- Week start date always being a Monday and consistent with `GetWeekNumber`

## ISO 8601 Week Rules

- **Week 1** is the week containing the first Thursday of the year (equivalently, the week containing January 4th).
- **Weeks start on Monday.**
- A year has **53 weeks** when January 1st is a Thursday, or when it is a leap year and January 1st is a Wednesday or Thursday.

## Deploy to ODC

Build the project in **Release** mode and upload the resulting `.zip` to the ODC Portal under **External Libraries**:

```bash
dotnet publish ISO8601_Helper/ISO8601_Helper.csproj -c Release
```

## License

This project is provided as-is for use within OutSystems ODC environments.
