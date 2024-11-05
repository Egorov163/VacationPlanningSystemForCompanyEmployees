using System;
using System.Collections.Generic;
using System.Linq;

namespace VacationPlanningSystemForCompanyEmployees
{
    public class VacationPlanner
    {
        // Список сотрудников
        private List<Employee> employees;
        // Количество дней отпуска
        private int vacationDays;
        // Текущий год
        private int currentYear;
        // Генератор случайных чисел
        private Random random;

        /// <summary>
        /// Создание VacationPlanner
        /// </summary>
        /// <param name="employeeNames">Спиок сотрудников</param>
        /// <param name="vacationDays">Количество дней отпуска</param>
        /// <param name="year">Год</param>
        public VacationPlanner(List<string> employeeNames, int vacationDays, int year)
        {
            // Преобразование списка имен сотрудников в список объектов Employee
            employees = employeeNames.Select(name => new Employee(name)).ToList();
            // Инициализация поля vacationDays
            this.vacationDays = vacationDays;
            // Инициализация поля currentYear
            this.currentYear = year;
            // Создание нового экземпляра класса Random
            this.random = new Random();
        }

        /// <summary>
        /// Планирование отпусков для всех сотрудников
        /// </summary>
        public void PlanVacations()
        {
            // Итерация по всем сотрудникам
            foreach (var employee in employees)
            {
                // Планирование отпуска для текущего сотрудника
                PlanEmployeeVacation(employee);
            }
        }

        /// <summary>
        /// Планирование отпуска для одного сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        private void PlanEmployeeVacation(Employee employee)
        {
            // Остаток дней отпуска
            int remainingDays = vacationDays;

            // Цикл, пока не будут распределены все дни отпуска
            while (remainingDays > 0)
            {
                // Выбор случайной продолжительности отпуска (7 или 14 дней)
                int vacationLength = Math.Min(remainingDays, random.Next(2) == 0 ? 7 : 14);

                // Генерация случайной даты начала отпуска
                DateTime startDate = GetRandomWorkday();
                // Вычисление даты окончания отпуска
                DateTime endDate = startDate.AddDays(vacationLength - 1);

                // Создание нового объекта DateRange для отпуска
                DateRange newVacation = new DateRange(startDate, endDate);

                // Проверка, является ли отпуск допустимым (не пересекается с другими отпусками)
                if (IsVacationValid(newVacation))
                {
                    // Добавление отпуска в список отпусков сотрудника
                    employee.Vacations.Add(newVacation);
                    // Обновление остатка дней отпуска
                    remainingDays -= vacationLength;
                }
            }
        }

        /// <summary>
        /// Генерация случайной даты начала отпуска, которая является рабочим днем
        /// </summary>
        /// <returns></returns>
        private DateTime GetRandomWorkday()
        {
            DateTime date;
            // Цикл, пока не будет сгенерирована дата, которая не является выходным днем
            do
            {
                // Генерация случайного дня в году (от 1 до 365)
                int dayOfYear = random.Next(1, 366);
                // Создание даты на основе дня в году
                date = new DateTime(currentYear, 1, 1).AddDays(dayOfYear - 1);
            }
            // Проверка, является ли дата выходным днем
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
            // Возврат даты
            return date;
        }

        /// <summary>
        /// Проверка, является ли отпуск допустимым (не пересекается с другими отпусками)
        /// </summary>
        private bool IsVacationValid(DateRange newVacation)
        {
            // Проверка, есть ли у сотрудников отпуска, которые пересекаются с новым отпуском
            return !employees.Any(e => e.Vacations.Any(v => v.Overlaps(newVacation)));
        }

        /// <summary>
        /// Вывод графика отпусков на консоль
        /// </summary>
        public void PrintVacationSchedule()
        {
            // Итерация по всем сотрудникам
            foreach (var employee in employees)
            {
                // Вывод имени сотрудника
                Console.WriteLine($"{employee.Name}:");
                // Итерация по отпускам сотрудника, отсортированных по дате начала
                foreach (var vacation in employee.Vacations.OrderBy(v => v.Start))
                {
                    // Вывод даты начала и окончания отпуска
                    Console.WriteLine($"  {vacation.Start.ToShortDateString()} - {vacation.End.ToShortDateString()}");
                }
                Console.WriteLine();
            }

            // Вычисление общего количества дней отпуска
            int totalVacationDays = employees.Sum(e => e.Vacations.Sum(v => v.WorkdaysCount()));

            // Вывод общего количества дней отпуска
            Console.WriteLine($"Общее количество дней отпуска: {totalVacationDays}");
        }
    }
}
