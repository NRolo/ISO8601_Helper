using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ISO8601_Helper
{
    public class ISO8601_Helper : IISO8601_Helper
    {
        /// <summary>
        /// Converts a DateTime value to the ISO 8601 format
        /// This method is exposed as a server action to your ODC apps and libraries
        /// </summary>
        /// <param name="value">The DateTime to be formated to ISO 8601</param>
        /// <returns>ISO 8601 text output of the input Datetime</returns>
        public string ISO8601_FormatDateTime(DateTime value)
        {
            return value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns the ISO 8601 last week number of the given year
        /// This method is exposed as a server action to your ODC apps and libraries.
        /// </summary>
        /// <param name="year">The input year</param>
        /// <returns>The ISO 8601 last week number of the input year</returns>
        public int ISO8601_GetLastWeekOfYear(int year)
        {
            return ISO8601_GetWeekNumber(new DateTime(year, 12, 28));
        }

        /// <summary>
        /// Returns the DateTime that according to ISO 8601 represents the start of the week of the given year and week number
        /// This method is exposed as a server action to your ODC apps and libraries.
        /// </summary>
        /// <param name="year">The input year</param>
        /// <param name="week_number">The input week number</param>
        /// <returns>The DateTime that according to ISO 8601 represents the start of the week of the given year and week number</returns>
        public DateTime ISO8601_GetStartOfWeek(int year, int week_number)
        {
            CultureInfo ci = CultureInfo.InvariantCulture;

            DateTime jan1 = new DateTime(year, 1, 1);

            int daysOffset = (int)DayOfWeek.Monday - (int)jan1.DayOfWeek;

            DateTime firstWeekDay = jan1.AddDays(daysOffset);

            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                week_number -= 1;
            }

            return firstWeekDay.AddDays(week_number * 7);
        }

        /// <summary>
        /// Returns the ISO 8601 week number from a DateTime
        /// This method is exposed as a server action to your ODC apps and libraries.
        /// </summary>
        /// <param name="value">The input DateTime</param>
        /// <returns>The ISO 8601 week number of the input DateTime</returns>
        public int ISO8601_GetWeekNumber(DateTime value)
        {
            CultureInfo ciCurr = CultureInfo.InvariantCulture;

            DayOfWeek day = ciCurr.Calendar.GetDayOfWeek(value);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                value = value.AddDays(3);
            }

            // return the week of our adjusted day
            return ciCurr.Calendar.GetWeekOfYear(value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
