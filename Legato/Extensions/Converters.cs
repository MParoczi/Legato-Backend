using System;

namespace Legato.Extensions
{
    /// <summary>
    ///     Contains methods to convert one type to another
    /// </summary>
    public static class Converters
    {
        /// <summary>
        ///     Converts a string (eg. "2020.05.26") into a DateTime object
        /// </summary>
        /// <param name="date">String representation of a date in yyyy/MM/dd format</param>
        /// <returns>A newly created DateTime object that is set to the given date</returns>
        public static DateTime DateTimeConverter(string date)
        {
            var dates = Array.ConvertAll(date.Split("."), int.Parse);
            return new DateTime(dates[0], dates[1], dates[2]);
        }
    }
}