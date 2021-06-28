using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.Helpers
{


    public enum DateTimeRangeType
    {
        Today = 0,
        Yesterday = 1,
        ThisWeek = 2,
        LastWeek = 3,
        In7Days = 4,
        ThisMonth = 5,
        LastMonth = 6,
        In30Days = 7,
        ThisYear = 8,
        LastYear = 9,
    }

    public static partial class DateTimeHelper
    {

        /// <summary>
        /// 获取时间范围
        /// </summary>
        /// <param name="type"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime[] GetDateTimeRange(DateTimeRangeType type, DateTime? now = null)
        {
            var dates = new DateTime[2];
            switch (type)
            {
                case DateTimeRangeType.Today:
                    dates[0] = GetToday(now);
                    dates[1] = dates[0].AddDays(1);
                    break;
                case DateTimeRangeType.Yesterday:
                    dates[0] = GetYesterday(now);
                    dates[1] = dates[0].AddDays(1);
                    break;
                case DateTimeRangeType.ThisWeek:
                    dates[0] = GetThisWeekFirstDay(now);
                    dates[1] = dates[0].AddDays(7);
                    break;
                case DateTimeRangeType.LastWeek:
                    dates[0] = GetLastWeekFirstDay(now);
                    dates[1] = dates[0].AddDays(7);
                    break;
                case DateTimeRangeType.In7Days:
                    dates[0] = Get6DaysAgo(now);
                    dates[1] = dates[0].AddDays(7);
                    break;
                case DateTimeRangeType.ThisMonth:
                    dates[0] = GetThisMonthFirstDay(now);
                    dates[1] = dates[0].AddMonths(1);
                    break;
                case DateTimeRangeType.LastMonth:
                    dates[0] = GetLastMonthFirstDay(now);
                    dates[1] = dates[0].AddMonths(1);
                    break;
                case DateTimeRangeType.In30Days:
                    dates[0] = Get29DaysAgo(now);
                    dates[1] = dates[0].AddDays(30);
                    break;
                case DateTimeRangeType.ThisYear:
                    dates[0] = GetThisYearFirstDay(now);
                    dates[1] = dates[0].AddYears(1);
                    break;
                case DateTimeRangeType.LastYear:
                    dates[0] = GetLastYearFirstDay(now);
                    dates[1] = dates[0].AddYears(1);
                    break;
            }
            return dates;
        }

        public static DateTime GetToday(DateTime? now = null)
        {
            if (now == null)
            {
                now = DateTime.Now;
            }
            return now.Value.Date;
        }

        public static DateTime GetYesterday(DateTime? now = null)
        {
            return GetToday(now).AddDays(-1);
        }

        /// <summary>
        /// 获取本周星期一的日期 （约定星期一为一周的第一天）
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime GetThisWeekFirstDay(DateTime? now = null)
        {
            if (now == null)
            {
                now = DateTime.Now;
            }
            var days = 0;
            switch (now.Value.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    days = 0;
                    break;
                case DayOfWeek.Tuesday:
                    days = 1;
                    break;
                case DayOfWeek.Wednesday:
                    days = 2;
                    break;
                case DayOfWeek.Thursday:
                    days = 3;
                    break;
                case DayOfWeek.Friday:
                    days = 4;
                    break;
                case DayOfWeek.Saturday:
                    days = 5;
                    break;
                case DayOfWeek.Sunday:
                    days = 6;
                    break;
            }
            return now.Value.Date.AddDays(-days);
        }

        /// <summary>
        /// 获取上周星期一的日期 （约定星期一为一周的第一天）
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime GetLastWeekFirstDay(DateTime? now = null)
        {
            return GetThisWeekFirstDay(now).AddDays(-7);
        }

        /// <summary>
        /// 获取6天前日期 （7天内的第1天日期）
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime Get6DaysAgo(DateTime? now = null)
        {
            if (now == null)
            {
                now = DateTime.Now;
            }
            return now.Value.Date.AddDays(-6);
        }

        /// <summary>
        /// 获取当月第1天日期
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime GetThisMonthFirstDay(DateTime? now = null)
        {
            if (now == null)
            {
                now = DateTime.Now;
            }
            return new DateTime(now.Value.Year, now.Value.Month, 1);
        }

        /// <summary>
        /// 获取上月第1天日期
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime GetLastMonthFirstDay(DateTime? now = null)
        {
            return GetThisMonthFirstDay(now).AddMonths(-1);
        }

        /// <summary>
        /// 获取29天前的日期 （30天内的第1天日期）
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime Get29DaysAgo(DateTime? now = null)
        {
            if (now == null)
            {
                now = DateTime.Now;
            }
            return now.Value.Date.AddDays(-29);
        }

        /// <summary>
        /// 获取元旦日期
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime GetThisYearFirstDay(DateTime? now = null)
        {
            if (now == null)
            {
                now = DateTime.Now;
            }
            return new DateTime(now.Value.Year, 1, 1);
        }


        /// <summary>
        /// 获取去年元旦日期
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime GetLastYearFirstDay(DateTime? now = null)
        {
            return GetThisYearFirstDay().AddYears(-1);
        }
    }
}
