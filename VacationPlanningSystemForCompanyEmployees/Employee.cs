using System.Collections.Generic;

namespace VacationPlanningSystemForCompanyEmployees
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    class Employee
    {
        //Имя сотрудника
        public string Name { get; set; }
        //Список отпусков сотрудника
        public List<DateRange> Vacations { get; set; } = new List<DateRange>();

        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        public Employee(string name)
        {
            Name = name;
        }
    }
}
