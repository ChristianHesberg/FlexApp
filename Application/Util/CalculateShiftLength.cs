namespace Application.Util;

public static class CalculateShiftLength
{
    public static double CalculateLength(DateTime start, DateTime end)
    {
        double hours = 0;
        hours += (end.Day - start.Day) * 24;
        hours += end.Hour - start.Hour;
        hours += (end.Minute - start.Minute) / 60.0;
        return hours;
    }
}