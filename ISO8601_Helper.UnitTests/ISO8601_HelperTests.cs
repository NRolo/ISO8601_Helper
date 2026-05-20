using System.Globalization;

namespace ISO8601_Helper.UnitTests;

public class ISO8601_HelperTests
{
    private readonly ISO8601_Helper _sut = new();

    #region ISO8601_FormatDateTime

    [Theory]
    [InlineData(2024,  1, 15, 10, 30,  0, "2024-01-15T10:30:00Z")]
    [InlineData(2000, 12, 31, 23, 59, 59, "2000-12-31T23:59:59Z")]
    [InlineData(1990,  1,  1,  0,  0,  0, "1990-01-01T00:00:00Z")]
    [InlineData(2024,  2, 29, 12,  0,  0, "2024-02-29T12:00:00Z")]
    public void ISO8601_FormatDateTime_UtcInput_ReturnsCorrectFormat(
        int year, int month, int day, int hour, int minute, int second, string expected)
    {
        var input = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);

        var result = _sut.ISO8601_FormatDateTime(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ISO8601_FormatDateTime_OutputEndsWithZ()
    {
        var input = new DateTime(2023, 6, 15, 8, 0, 0, DateTimeKind.Utc);

        var result = _sut.ISO8601_FormatDateTime(input);

        Assert.EndsWith("Z", result);
    }

    [Fact]
    public void ISO8601_FormatDateTime_OutputMatchesISO8601Pattern()
    {
        var input = new DateTime(2023, 6, 15, 8, 0, 0, DateTimeKind.Utc);

        var result = _sut.ISO8601_FormatDateTime(input);

        Assert.Matches(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z$", result);
    }

    #endregion

    #region ISO8601_GetWeekNumber

    [Theory]
    [InlineData(2020,  1,  1,  1)]
    [InlineData(2020,  1,  6,  2)]
    [InlineData(2020, 12, 28, 53)]
    [InlineData(2020, 12, 31, 53)]
    [InlineData(2021,  1,  1, 53)]
    [InlineData(2021,  1,  4,  1)]
    [InlineData(2021, 12, 31, 52)]
    [InlineData(2015, 12, 31, 53)]
    [InlineData(2016,  1,  1, 53)]
    [InlineData(2016,  1,  4,  1)]
    [InlineData(2018,  1,  1,  1)]
    public void ISO8601_GetWeekNumber_KnownDates_ReturnCorrectWeek(
        int year, int month, int day, int expectedWeek)
    {
        var result = _sut.ISO8601_GetWeekNumber(new DateTime(year, month, day));

        Assert.Equal(expectedWeek, result);
    }

    [Fact]
    public void ISO8601_GetWeekNumber_Jan4_AlwaysWeek1()
    {
        foreach (int year in new[] { 2019, 2020, 2021, 2022, 2023, 2024 })
        {
            var result = _sut.ISO8601_GetWeekNumber(new DateTime(year, 1, 4));
            Assert.Equal(1, result);
        }
    }

    [Fact]
    public void ISO8601_GetWeekNumber_Dec28_AlwaysLastWeek()
    {
        foreach (int year in new[] { 2019, 2020, 2021, 2022, 2023, 2024 })
        {
            var lastWeek = _sut.ISO8601_GetLastWeekOfYear(year);
            var result = _sut.ISO8601_GetWeekNumber(new DateTime(year, 12, 28));
            Assert.Equal(lastWeek, result);
        }
    }

    #endregion

    #region ISO8601_GetLastWeekOfYear

    [Theory]
    [InlineData(2020, 53)]
    [InlineData(2021, 52)]
    [InlineData(2015, 53)]
    [InlineData(2018, 52)]
    [InlineData(2019, 52)]
    [InlineData(2026, 53)]
    public void ISO8601_GetLastWeekOfYear_KnownYears_ReturnCorrectLastWeek(int year, int expectedLastWeek)
    {
        var result = _sut.ISO8601_GetLastWeekOfYear(year);

        Assert.Equal(expectedLastWeek, result);
    }

    [Fact]
    public void ISO8601_GetLastWeekOfYear_ResultIs52Or53()
    {
        foreach (int year in Enumerable.Range(2000, 30))
        {
            var result = _sut.ISO8601_GetLastWeekOfYear(year);
            Assert.InRange(result, 52, 53);
        }
    }

    #endregion

    #region ISO8601_GetStartOfWeek

    [Theory]
    [InlineData(2021,  1, 2021,  1,  4)]
    [InlineData(2021, 52, 2021, 12, 27)]
    [InlineData(2020,  1, 2019, 12, 30)]
    [InlineData(2020,  2, 2020,  1,  6)]
    [InlineData(2020, 53, 2020, 12, 28)]
    [InlineData(2018,  1, 2018,  1,  1)]
    public void ISO8601_GetStartOfWeek_KnownWeeks_ReturnCorrectMonday(
        int year, int week, int expectedYear, int expectedMonth, int expectedDay)
    {
        var result = _sut.ISO8601_GetStartOfWeek(year, week);

        Assert.Equal(new DateTime(expectedYear, expectedMonth, expectedDay), result);
    }

    [Fact]
    public void ISO8601_GetStartOfWeek_ResultIsAlwaysMonday()
    {
        int[] yearsToTest = { 2018, 2019, 2020, 2021, 2022, 2023 };
        foreach (int year in yearsToTest)
        {
            int lastWeek = _sut.ISO8601_GetLastWeekOfYear(year);
            for (int week = 1; week <= lastWeek; week++)
            {
                var start = _sut.ISO8601_GetStartOfWeek(year, week);
                Assert.Equal(DayOfWeek.Monday, start.DayOfWeek);
            }
        }
    }

    [Fact]
    public void ISO8601_GetStartOfWeek_StartOfWeekHasSameWeekNumber()
    {
        int[] yearsToTest = { 2019, 2020, 2021, 2022 };
        foreach (int year in yearsToTest)
        {
            int lastWeek = _sut.ISO8601_GetLastWeekOfYear(year);
            for (int week = 1; week <= lastWeek; week++)
            {
                var start = _sut.ISO8601_GetStartOfWeek(year, week);
                var weekNumber = _sut.ISO8601_GetWeekNumber(start);
                Assert.Equal(week, weekNumber);
            }
        }
    }

    #endregion
}