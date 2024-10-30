using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationPlanningSystemForCompanyEmployees
{
    public class VacationPlanner
    {
        private List<Employee> employees;
        private int vacationDays;
        private int currentYear;
        private Random random;

        public VacationPlanner(List<string> employeeNames, int vacationDays, int year)
        {
            employees = employeeNames.Select(name => new Employee(name)).ToList();
            this.vacationDays = vacationDays;
            this.currentYear = year;
            this.random = new Random();
        }

        public void PlanVacations()
        {
            foreach (var employee in employees)
            {
                PlanEmployeeVacation(employee);
            }
        }

        private void PlanEmployeeVacation(Employee employee)
        {
            int remainingDays = vacationDays;

            while (remainingDays > 0)
            {
                int vacationLength = Math.Min(remainingDays, random.Next(2) == 0 ? 7 : 14); // выбираем между 7 и 14 днями

                DateTime startDate = GetRandomWorkday();
                DateTime endDate = startDate.AddDays(vacationLength - 1);

                DateRange newVacation = new DateRange(startDate, endDate);

                if (IsVacationValid(newVacation))
                {
                    employee.Vacations.Add(newVacation);
                    remainingDays -= vacationLength;
                }
            }
        }

        private DateTime GetRandomWorkday()
        {
            DateTime date;
            do
            {
                int dayOfYear = random.Next(1, 366);
                date = new DateTime(currentYear, 1, 1).AddDays(dayOfYear - 1);
            }
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);

            return date;
        }

        private bool IsVacationValid(DateRange newVacation)
        {
            return !employees.Any(e => e.Vacations.Any(v => v.Overlaps(newVacation)));
        }

        public void PrintVacationSchedule()
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Name}:");
                foreach (var vacation in employee.Vacations.OrderBy(v => v.Start))
                {
                    Console.WriteLine($"  {vacation.Start.ToShortDateString()} - {vacation.End.ToShortDateString()}");
                }
                Console.WriteLine();
            }

            int totalVacationDays = employees.Sum(e => e.Vacations.Sum(v => v.WorkdaysCount()));
            Console.WriteLine($"Общее количество дней отпуска: {totalVacationDays}");
        }
    }
}
