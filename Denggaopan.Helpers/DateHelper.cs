using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.Helpers
{
    public static partial class DateHelper
    {
        public static List<int> GetHours(DateTime? date)
        {
            var currentHour = DateTime.Now.Hour;
            if(date != null)
            {
                currentHour = date.Value.Hour;
            }
            var list = new List<int>();
            for (var i = 0; i <= currentHour; i++)
            {
                list.Add(i);
            }
            return list;
        }

        public static List<DateTime> GetRecentDays(int num)
        {
            var list = new List<DateTime>();
            var today = DateTime.Now.Date;
            for (var i = 1; i <= num; i++)
            {
                list.Add(today.AddDays(i - num));
            }
            return list;
        }

        public static List<DateTime> GetRangeDays(string startDate, string endDate)
        {
            var list = new List<DateTime>();
            var startDay = DateTime.Parse(startDate);
            var endDay = DateTime.Parse(endDate);
            var currentDay = startDay;
            while (currentDay <= endDay)
            {
                list.Add(currentDay);
                currentDay = currentDay.AddDays(1);
            }
            return list;
        }

        public static List<DateTime> GetRecentMonths(int num)
        {
            var list = new List<DateTime>();
            var now = DateTime.Now;
            var currentMonth1stDay = new DateTime(now.Year, now.Month, 1);
            for (var i = 1; i <= num; i++)
            {
                list.Add(currentMonth1stDay.AddMonths(i - num));
            }
            return list;
        }
    }
}
