using System;
using System.Collections.Generic;
using System.Linq;
using VacationPlanningSystemForCompanyEmployees;

class Program
{

    // Константы для определения длительности отпусков
    const int MINIMUM_VACATION_DAYS = 7; // Минимальное количество отпускных дней
    const int MAXIMUM_VACATION_DAYS = 14; // Максимальное количество отпускных дней
    const int TOTAL_STANDARD_VACATION_DAYS = 28; // Стандартное количество отпускных дней в году
    const int DAYS_Of_VERIFICATION = 3; // Количество дней для проверки существующих отпусков

    static void Main(string[] args)
    {
        // Словарь для хранения сотрудников и списков их отпусков
        var vacationDictionary = new Dictionary<string, List<DateTime>>()
        {
            ["Иванов Иван Иванович"] = new List<DateTime>(),
            ["Петров Петр Петрович"] = new List<DateTime>(),
            ["Юлина Юлия Юлиановна"] = new List<DateTime>(),
            ["Сидоров Сидор Сидорович"] = new List<DateTime>(),
            ["Павлов Павел Павлович"] = new List<DateTime>(),
            ["Георгиев Георг Георгиевич"] = new List<DateTime>()
        };

        // Список доступных рабочих дней без выходных (перечисление)
        var availableWorkingDays = new List<DayOfWeekEnum>
        {
            DayOfWeekEnum.Monday,
            DayOfWeekEnum.Tuesday,
            DayOfWeekEnum.Wednesday,
            DayOfWeekEnum.Thursday,
            DayOfWeekEnum.Friday
        };

        // Список всех отпусков сотрудников
        List<DateTime> vacations = new List<DateTime>();
        // Словарь для хранения отпусков с указанием дат
        Dictionary<string, List<(DateTime Start, DateTime End)>> vacationRecords = new Dictionary<string, List<(DateTime, DateTime)>>();

        int allVacationCount = 0; // Переменная для хранения общего количества отпусков

        Random randomGen = new Random(); // Генератор случайных чисел

        // Цикл по каждому сотруднику, чтобы назначить отпуска
        foreach (var vacationList in vacationDictionary)
        {
            List<DateTime> dateList = vacationList.Value; // Получаем список дат отпусков для данного сотрудника
            int vacationCount = TOTAL_STANDARD_VACATION_DAYS; // Количество оставшихся отпускных дней

            DateTime start = new DateTime(DateTime.Now.Year, 1, 1); // Начало года
            DateTime end = new DateTime(DateTime.Today.Year, 12, 31); // Конец года

            // Цикл для назначения отпусков до тех пор, пока не исчерпаются отпуска
            while (vacationCount > 0)
            {
                // Получаем случайную начальную дату отпуска
                int range = (end - start).Days;
                var startDate = start.AddDays(randomGen.Next(range));

                // Проверяем, является ли пулевое число рабочим днем
                if (availableWorkingDays.Contains((DayOfWeekEnum)startDate.DayOfWeek))
                {
                    // Получаем случайную длительность отпуска
                    int vacationDuration = randomGen.Next(MINIMUM_VACATION_DAYS, MAXIMUM_VACATION_DAYS + 1);
                    var endDate = startDate.AddDays(vacationDuration);

                    // Если осталось мало отпусков, назначаем минимальную длительность
                    if (vacationCount <= MINIMUM_VACATION_DAYS)
                    {
                        endDate = startDate.AddDays(MINIMUM_VACATION_DAYS);
                    }
                    // Проверка условий по отпуску
                    bool canCreateVacation = CanCreateVacation(startDate, endDate, vacations, dateList);
                    
                    // Если отпуск можно назначить
                    if (canCreateVacation)
                    {
                        // Добавляем все дни отпуска
                        for (DateTime dt = startDate; dt < endDate; dt = dt.AddDays(1))
                        {
                            vacations.Add(dt); // Добавляем в общий список отпусков
                            dateList.Add(dt); // Добавляем в список отпусков конкретного сотрудника
                        }
                        allVacationCount++; // Увеличиваем общий счетчик отпусков

                        // Записываем информацию об отпуске в словарь
                        if (!vacationRecords.ContainsKey(vacationList.Key))
                        {
                            vacationRecords[vacationList.Key] = new List<(DateTime, DateTime)>();
                        }
                        vacationRecords[vacationList.Key].Add((startDate, endDate));

                        // Уменьшаем количество оставшихся отпусков
                        vacationCount -= vacationDuration; 
                    }
                }
            }
        }

        // Вывод информации о назначенных отпусках в консоль
        foreach (var record in vacationRecords)
        {
            foreach (var vacation in record.Value)
            {
                Console.WriteLine($"{record.Key} берет отпуск с {vacation.Start.ToShortDateString()} по {vacation.End.ToShortDateString()}");
            }
            Console.WriteLine();
        }

        Console.WriteLine($"Всего отпусков: {allVacationCount}");
    }

    // Метод для проверки возможности назначения отпуска
    static bool CanCreateVacation(DateTime startDate, DateTime endDate, List<DateTime> vacations, List<DateTime> dateList)
    {
        // Проверяем, пересекается ли отпуск с уже назначенными
        if (vacations.Any(element => element >= startDate && element <= endDate))
            return false;
        
        // Проверяем, пересекается ли отпуск с добавлением 3 дней
        if (vacations.Any(element => element.AddDays(DAYS_Of_VERIFICATION) >= startDate && element.AddDays(DAYS_Of_VERIFICATION) <= endDate))
            return false;

        // Проверяем наличие уже назначенных отпусков до и после
        bool existStart = dateList.Any(element => element.AddMonths(1) >= startDate && element.AddMonths(1) >= endDate);
        bool existEnd = dateList.Any(element => element.AddMonths(-1) <= startDate && element.AddMonths(-1) <= endDate);
        
        return !existStart || !existEnd; // Возвращаем true, если нет пересечений
    }
}
