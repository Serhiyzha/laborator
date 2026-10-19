using System;
using System.Collections.Generic;
using System.Linq;
using StudentApp.DataAccess.Models;
using StudentApp.DataAccess.Repositories;

namespace StudentApp.ConsoleUI
{
    public class ConsoleMenu
    {
        private readonly IRepository<IPerson> _personRepository;
        private readonly string _filePath = "people.json";

        public ConsoleMenu(IRepository<IPerson> personRepository)
        {
            _personRepository = personRepository;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЮ КЕРУВАННЯ ===");
                Console.WriteLine("1. Згенерувати та зберегти тестові дані у файл");
                Console.WriteLine("2. Показати всіх з файлу");
                Console.WriteLine("3. Знайти студенток 5-го курсу з Києва");
                Console.WriteLine("0. Вихід");
                Console.Write("\nОберіть пункт: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        GenerateAndSaveData();
                        break;
                    case "2":
                        ShowAllPeople();
                        break;
                    case "3":
                        FilterKievFemaleFifthCourseStudents();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Натисніть Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void GenerateAndSaveData()
        {
            var people = new List<IPerson>
            {
                new Student { FirstName = "Олена", LastName = "Коваль", Course = 5, StudentCard = "KB123456", Gender = "Жіноча", City = "Київ", RecordBook = "101-А" },
                new Student { FirstName = "Анна", LastName = "Шевченко", Course = 5, StudentCard = "KB123457", Gender = "Жіноча", City = "Київ", RecordBook = "102-А" },
                new Student { FirstName = "Марія", LastName = "Бондаренко", Course = 3, StudentCard = "KB123458", Gender = "Жіноча", City = "Київ", RecordBook = "103-Б" },
                new Student { FirstName = "Ірина", LastName = "Лисенко", Course = 5, StudentCard = "KB123459", Gender = "Жіноча", City = "Львів", RecordBook = "104-А" },
                new Student { FirstName = "Олег", LastName = "Петренко", Course = 5, StudentCard = "KB123460", Gender = "Чоловіча", City = "Київ", RecordBook = "105-В" },
                new Joiner { FirstName = "Катерина", LastName = "Гончар", WorkshopNumber = 5, Gender = "Жіноча", City = "Київ" },
                new Photographer { FirstName = "Олег", LastName = "Сидоренко", CameraModel = "Canon EOS R5", Gender = "Чоловіча", City = "Київ" }
            };

            _personRepository.SaveToFile(_filePath, people);
            Console.WriteLine("\nТестові дані успішно збережено у файл!");
            Console.WriteLine("Натисніть Enter для продовження...");
            Console.ReadLine();
        }

        private void ShowAllPeople()
        {
            var people = _personRepository.LoadFromFile(_filePath);
            Console.WriteLine("\n--- Список усіх осіб у файлі ---");
            foreach (IPerson p in people)
            {
                if (p is Student s)
                {
                    Console.WriteLine($"[Студент] {s.LastName} {s.FirstName} | Курс: {s.Course} | Стать: {s.Gender} | Місто: {s.City} | Квиток: {s.StudentCard}");
                }
                else if (p is Joiner j)
                {
                    Console.WriteLine($"[Столяр] {j.LastName} {j.FirstName} | Майстерня №: {j.WorkshopNumber} | Стать: {j.Gender} | Місто: {j.City}");
                }
                else if (p is Photographer ph)
                {
                    Console.WriteLine($"[Фотограф] {ph.LastName} {ph.FirstName} | Камера: {ph.CameraModel} | Стать: {ph.Gender} | Місто: {ph.City}");
                }
            }
            Console.WriteLine("\nНатисніть Enter для продовження...");
            Console.ReadLine();
        }

        private void FilterKievFemaleFifthCourseStudents()
        {
            var people = _personRepository.LoadFromFile(_filePath);

            var filtered = people.Where(p =>
                p is Student s &&
                s.Course == 5 &&
                s.Gender.Equals("Жіноча", StringComparison.OrdinalIgnoreCase) &&
                s.City.Equals("Київ", StringComparison.OrdinalIgnoreCase)
            ).ToList();

            Console.WriteLine($"\n--- Результати пошуку (Знайдено: {filtered.Count}) ---");
            PrintPeople(filtered);

            Console.WriteLine("\nНатисніть Enter для продовження...");
            Console.ReadLine();
        }

        private void PrintPeople(List<IPerson> people)
        {
            if (people.Count == 0)
            {
                Console.WriteLine("Дані відсутні або файл порожній.");
                return;
            }

            foreach (var p in people)
            {
                if (p is Student s)
                {
                    Console.WriteLine($"[Студент] {s.LastName} {s.FirstName} | Курс: {s.Course} | Стать: {s.Gender} | Місто: {s.City} | Квиток: {s.StudentCard}");
                }
                else if (p is Joiner j)
                {
                    Console.WriteLine($"[Столяр] {j.LastName} {j.FirstName} | Майстерня №: {j.WorkshopNumber} | Стать: {j.Gender} | Місто: {j.City}");
                }
                else if (p is Photographer ph)
                {
                    Console.WriteLine($"[Фотограф] {ph.LastName} {ph.FirstName} | Камера: {ph.CameraModel} | Стать: {ph.Gender} | Місто: {ph.City}");
                }
            }
        }
    }
}