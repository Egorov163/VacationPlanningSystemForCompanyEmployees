using System;
using System.Linq;

namespace VacationPlanningSystemForCompanyEmployees
{
    public class DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public bool Overlaps(DateRange other)
        {
            return this.Start <= other.End && other.Start <= this.End;
        }

        public int WorkdaysCount()
        {
            return Enumerable.Range(0, (int)(End - Start).TotalDays + 1)
                .Select(offset => Start.AddDays(offset))
                .Count(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday);
        }
    }
}
