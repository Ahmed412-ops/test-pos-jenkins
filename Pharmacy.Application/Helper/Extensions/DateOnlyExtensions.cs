namespace Pharmacy.Application.Helper.Extensions;

public static class DateOnlyExtensions
{
    public static DateOnly WithDay(this DateOnly date, int day) => new DateOnly(date.Year, date.Month, day);
    public static string ToMonthYearString(this DateOnly date) => date.ToString("MM/yyyy");
}
