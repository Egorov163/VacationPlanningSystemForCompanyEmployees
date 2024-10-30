using System.Collections.Generic;

namespace VacationPlanningSystemForCompanyEmployees
{
    class Employee
    {
        public string Name { get; set; }
        public List<DateRange> Vacations { get; set; } = new List<DateRange>();

        public Employee(string name)
        {
            Name = name;
        }
    }
}
