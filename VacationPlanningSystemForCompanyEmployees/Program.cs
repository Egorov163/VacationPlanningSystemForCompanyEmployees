using System.Collections.Generic;
using System;
using VacationPlanningSystemForCompanyEmployees;

class Program
{
    static void Main()
    {
        // Создаем список сотрудников
        List<string> employeeNames = new List<string>
        {
            // Добавляем имена сотрудников в список
            "Иванов Иван Иванович",
            "Петров Петр Петрович",
            "Сидоров Сидор Сидорович",
            "Александров Александр Александрович",
            "Николаев Николай Николаевич",
            "Дмитриев Дмитрий Дмитриевич"
        };
        // Создаем экземпляр класса VacationPlanner
        VacationPlanner planner = new VacationPlanner(employeeNames, 28, DateTime.Now.Year);
        // Вызываем метод PlanVacations() для планирования отпусков
        planner.PlanVacations();
        // Вызываем метод PrintVacationSchedule() для вывода графика отпусков на консоль
        planner.PrintVacationSchedule();
    }
}

