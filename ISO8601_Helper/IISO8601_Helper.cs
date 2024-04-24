using OutSystems.ExternalLibraries.SDK;

namespace ISO8601_Helper
{
    /// <summary>
    /// The IISO8601_Helper interface defines the methods (exposed as server actions)
    /// for the ISO8601 international standard for date time functionality
    /// </summary>
    [OSInterface(Description = "Enables features for ISO8601 in OutSystems Developer Cloud (ODC) apps.", IconResourceName = "ISO8601_Helper.resources.ISO8601.png")]
    public interface IISO8601_Helper
    {
        /// <summary>
        /// Converts a DateTime value to the ISO 8601 format
        /// This method is exposed as a server action to your ODC apps and libraries.
        /// </summary>
        /// <param name="value">The DateTime to be formated to ISO 8601</param>
        /// <returns>ISO 8601 text output of the input Datetime</returns>
        [OSAction(Description = "Converts a DateTime value to the ISO 8601 format", IconResourceName = "ISO8601_Helper.resources.ISO8601.png", ReturnName = "FormatedDateTime", ReturnType = OSDataType.Text)]
        public string ISO8601_FormatDateTime([OSParameter(DataType = OSDataType.DateTime, Description = "The DateTime to format to ISO8601")] DateTime value);

        /// <summary>
        /// Returns the ISO 8601 last week number of the given year
        /// This method is exposed as a server action to your ODC apps and libraries.
        /// </summary>
        /// <param name="year">The input year</param>
        /// <returns>The ISO 8601 last week number of the input year</returns>
        [OSAction(Description = "Returns the ISO 8601 last week number of the given year", IconResourceName = "ISO8601_Helper.resources.ISO8601.png", ReturnName = "LastWeekNumber", ReturnType = OSDataType.Integer)]
        public int ISO8601_GetLastWeekOfYear([OSParameter(DataType = OSDataType.Integer, Description = "The year to get the last week number")] int year);

        /// <summary>
        /// Returns the DateTime that according to ISO 8601 represents the start of the week of the given year and week number
        /// This method is exposed as a server action to your ODC apps and libraries.
        /// </summary>
        /// <param name="year">The input year</param>
        /// <param name="week_number">The input week number</param>
        /// <returns>The DateTime that according to ISO 8601 represents the start of the week of the given year and week number</returns>
        [OSAction(Description = "Returns the DateTime that according to ISO 8601 represents the start of the week of the given year and week number", IconResourceName = "ISO8601_Helper.resources.ISO8601.png", ReturnName = "StartDateTime", ReturnType = OSDataType.DateTime)]
        public DateTime ISO8601_GetStartOfWeek([OSParameter(DataType = OSDataType.Integer, Description = "The year to get the date time")] int year, [OSParameter(DataType = OSDataType.Integer, Description = "The week number to get the date time")] int week_number);

        /// <summary>
        /// Returns the ISO 8601 week number from a DateTime
        /// This method is exposed as a server action to your ODC apps and libraries.
        /// </summary>
        /// <param name="value">The input DateTime</param>
        /// <returns>The ISO 8601 week number of the input DateTime</returns>
        [OSAction(Description = "Returns the ISO 8601 week number from a DateTime", IconResourceName = "ISO8601_Helper.resources.ISO8601.png", ReturnName = "WeekNumber", ReturnType = OSDataType.Integer)]
        public int ISO8601_GetWeekNumber([OSParameter(DataType = OSDataType.DateTime, Description = "The DateTime to get the week number")] DateTime value);
    }
}