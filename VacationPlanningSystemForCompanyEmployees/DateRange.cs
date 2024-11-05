using System;
using System.Linq;

namespace VacationPlanningSystemForCompanyEmployees
{
    public class DateRange
    {
        //Дата начала диапазона
        public DateTime Start { get; set; }
        //Дата окончания диапазона
        public DateTime End { get; set; }

        /// <summary>
        /// Создание диапазона
        /// </summary>
        /// <param name="start">начало диапазона</param>
        /// <param name="end">окончание диапазона</param>
        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Проверка, пересекается ли текущий диапазон с другим диапазоном
        /// </summary>
        /// <param name="other">Другой диапазон</param>
        /// <returns></returns>
        public bool Overlaps(DateRange other)
        {
            // Проверка, находятся ли даты начала и окончания одного диапазона внутри другого диапазона
            return this.Start <= other.End && other.Start <= this.End;
        }

        /// <summary>
        /// Подсчет количества рабочих дней в диапазоне
        /// </summary>
        public int WorkdaysCount()
        {
            // Создание последовательности целых чисел от 0 до количества дней в диапазоне
            return Enumerable.Range(0, (int)(End - Start).TotalDays + 1)               
                .Select(offset => Start.AddDays(offset)) // Преобразование каждого целого числа в дату в диапазоне
                .Count(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday); // Подсчет дат, которые не являются выходными днями
                
        }
    }
}
