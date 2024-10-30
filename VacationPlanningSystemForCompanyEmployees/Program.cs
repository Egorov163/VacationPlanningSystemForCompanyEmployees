using System.Collections.Generic;
using System;
using VacationPlanningSystemForCompanyEmployees;

class Program
{
    static void Main()
    {
        List<string> employeeNames = new List<string>
        {
            "Иванов Иван Иванович",
            "Петров Петр Петрович",
            "Сидоров Сидор Сидорович",
            "Александров Александр Александрович",
            "Николаев Николай Николаевич",
            "Дмитриев Дмитрий Дмитриевич"
        };

        VacationPlanner planner = new VacationPlanner(employeeNames, 28, DateTime.Now.Year);
        planner.PlanVacations();
        planner.PrintVacationSchedule();
    }
}

