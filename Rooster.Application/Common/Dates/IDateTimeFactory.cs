namespace Rooster.Application.Common.Common.Dates;

public interface IDateTimeFactory
{
    DateTimeOffset NowWithOffset();
    DateTimeOffset UtcNowWithOffset();
    DateTime Now();
    DateTime UtcNow();
    DateTime MaxValue();
    DateTime MinValue();
    DateTime GetDateForDayOfWeek(DateTime dt, DayOfWeek startOfWeek);
    DateTimeOffset GetUtcForPacificMidnightByDate(DateTime dt);
}

public class DateTimeFactory : IDateTimeFactory
{
    DateTimeOffset IDateTimeFactory.NowWithOffset() => DateTimeOffset.Now;
    public DateTimeOffset UtcNowWithOffset() => DateTimeOffset.UtcNow;
    DateTime IDateTimeFactory.Now() => DateTime.Now;
    public DateTime UtcNow() => DateTime.UtcNow;
    public DateTime MaxValue() => DateTime.MaxValue;
    public DateTime MinValue() => DateTime.MinValue;

    public DateTime GetDateForDayOfWeek(DateTime dt, DayOfWeek startOfWeek)
    {
        var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }

    public DateTimeOffset GetUtcForPacificMidnightByDate(DateTime inputDate)
    {
        var pacificTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

        var dateTime = TimeZoneInfo.ConvertTime(new DateTime(inputDate.Year, inputDate.Month, inputDate.Day, 0, 0, 0),
            pacificTimeZone, TimeZoneInfo.Utc);
        return new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute,
            dateTime.Second, dateTime.Millisecond, TimeSpan.Zero);
    }
}