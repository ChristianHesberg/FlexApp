using Application.Util;

DateTime start = new DateTime(2000, 12, 13, 8, 12, 0);
DateTime end = new DateTime(2000, 12, 13, 10, 15, 0);


Console.WriteLine(CalculateShiftLength.CalculateLength(start, end));