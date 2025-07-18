using Lab_2.items;
using System.Text.Json;
using System;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab_2
{
    public class Program
    {
        private static District district;
        private static readonly string fileName = "houses.json";
        static void Main(string[] args)
        {
            Console.WriteLine("Вариант 8");
            district = new District();
            char key;
            do
            {
                Console.WriteLine("f - функции репозитория");
                Console.WriteLine("L - взять данные из файла");
                Console.WriteLine("s - сохранить данные в файл");
                Console.WriteLine("Esc - выход");
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case 'f':
                        ShowFunctionsForRepository();
                        break;
                    case 'L':
                        LoadDataFromFile(fileName);
                        break;
                    case 's':
                        SaveDataToFile(fileName);
                        break;
                    default:
                        break;
                }
            } while (key != ((char)ConsoleKey.Escape));

        }

        public static void ShowFunctionsForRepository()
        {
            char key;
            do
            {
                Console.Clear();
                Console.WriteLine("0 - добавить дом");
                Console.WriteLine("1 - удалить дом");
                Console.WriteLine("2 - сортировать");
                Console.WriteLine("3 - подсчёт количества объектов каждого типа");
                Console.WriteLine("4 - поиск");
                Console.WriteLine("5 - фильтрация");
                Console.WriteLine("6 - расчёт статистических характеристик");
                Console.WriteLine("7 - показать все дома");
                Console.WriteLine("Esc - выход");
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '0':
                        AddHouseInDistrict();
                        break;
                    case '1':
                        RemoveHouseFromDistrict();
                        break;
                    case '2':
                        SortHouseInDistrict();
                        break;
                    case '3':
                        CountTypeOfHousesInDistrict();
                        break;
                    case '4':
                        FindHousesByCharacteristics();
                        break;
                    case '5':
                        FilterHousesInDistrict();
                        break;
                    case '6':
                        CountStatistics();
                        break;
                    case '7':
                        ShowAllHouses();
                        break;
                }
            } while (key != ((char)ConsoleKey.Escape));
        }

        public static void LoadDataFromFile(string fileName)
        {
            Console.Clear();
            Console.WriteLine("Чтение из файла");
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не найден!");
                district.Houses = new List<House>();
            }
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            List<House> loadedHouses = JsonConvert.DeserializeObject<List<House>>(File.ReadAllText(fileName), settings);
            district.Houses = loadedHouses.Distinct().ToList();
            List<Organization> companies = loadedHouses
            .Select(h => h.BuildingCompany)
            .Where(c => c != null)
            .Distinct()
            .ToList();
            District.BuildingCompanies = companies;
        }

        public static void SaveDataToFile(string fileName)
        {
            Console.Clear();
            Console.WriteLine("Запись в файл");
            try
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.All
                };
                string json = JsonConvert.SerializeObject(district.Houses, settings);
                File.WriteAllText(fileName, json);
                Console.WriteLine($"Данные успешно сохранены в {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи JSON: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nДля выхода нажмите любую клавишу...");
                Console.ReadKey(false);
            }
        }

        public static void ShowAllHouses()
        {
            Console.Clear();
            Console.WriteLine("Дома\n");
            foreach (var house in district.Houses)
            {
                Console.WriteLine(house.ToString());
            }
            Console.WriteLine("\nДля выхода нажмите любую клавишу...");
            Console.ReadKey(false);
        }

        public static void AddHouseInDistrict()
        {
            char key;
            Console.Clear();
            Console.WriteLine("Выберете тип дом:");
            Console.WriteLine("\t1 - Многоквартирный дом");
            Console.WriteLine("\t2 - Коттедж");
            Console.WriteLine("\t3 - Офис");
            key = Console.ReadKey(true).KeyChar;
            switch (key)
            {
                case '1':
                    ApartmentHouse apartment = CreateApartmentHouse();
                    if (!district.Houses.Contains(apartment))
                        district.CreateHouse(apartment);
                    else
                        Console.WriteLine("Такой дом уже существует");
                    break;
                case '2':
                    Cottage cottage = CreateCottage();
                    if (!district.Houses.Contains(cottage))
                        district.CreateHouse(cottage);
                    else
                        Console.WriteLine("Такой дом уже существует");
                    break;
                case '3':
                    Office office = CreateOffice();
                    if (!district.Houses.Contains(office))
                        district.CreateHouse(office);
                    else
                        Console.WriteLine("Такой дом уже существует");
                    break;
            }
        }
        public static void RemoveHouseFromDistrict()
        {
            Console.Clear();
            Console.WriteLine("Выберете дом:");
            for (int i = 0; i < district.Houses.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {district.Houses[i].Address}");
            }
            if (int.TryParse(Console.ReadLine(), out int index) && Conditions.CheckBelongToInterval(index, 0, district.Houses.Count+1))
            {
                var removeHouse = district.Houses[index-1];
                district.DestroyHouse(removeHouse);
                Console.WriteLine("Дом удален");
            }
            else
                Console.WriteLine("Нет такого дома!");
        }
        public static void SortHouseInDistrict()
        {
            char key;
            List<House> sortedHouses = new();
            Console.Clear();
            Console.WriteLine("Выберете тип сортировки:");
            Console.WriteLine("\t1 - адрес");
            Console.WriteLine("\t2 - год");
            Console.WriteLine("\t3 - наименование строительной компании");
            key = Console.ReadKey(true).KeyChar;
            switch (key)
            {
                case '1':
                    sortedHouses = district.SortByAddress();
                    break;
                case '2':
                    sortedHouses = district.SortByYearOfBuild();
                    break;
                case '3':
                    sortedHouses = district.SortByBuildingCompany();
                    break;
            }
            foreach (House house in sortedHouses)
            {
                Console.WriteLine(house.ToString());
            }
            Console.WriteLine("\nДля выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
        public static void CountTypeOfHousesInDistrict()
        {
            Console.Clear();
            Console.WriteLine("Подсчёт количества объектов каждого типа:");
            var countOfTypes = district.CountItemsOfEachType();
            foreach (var pair in countOfTypes)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
            }
            Console.WriteLine("\nДля выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
        public static void FindHousesByCharacteristics()
        {
            Console.Clear();
            Console.Write("Введите адрес (или Enter, чтобы пропустить): ");
            string address = Console.ReadLine();
            string filterAddress = string.IsNullOrWhiteSpace(address) ? null : address;

            Console.Write("Введите год постройки (или Enter, чтобы пропустить): ");
            string year = Console.ReadLine();
            int? filterYear = int.TryParse(year, out int y) ? y : (int?)null;

            Console.Write("Введите площадь (или Enter, чтобы пропустить): ");
            string square = Console.ReadLine();
            int? filterSquare = int.TryParse(square, out int s) ? s : (int?)null;

            Console.Write("Введите стоимость (или Enter, чтобы пропустить): ");
            string cost = Console.ReadLine();
            int? filterCost = int.TryParse(cost, out int c) ? c : (int?)null;

            Console.Write("Введите название компании-застройщика (или Enter, чтобы пропустить): ");
            string company = Console.ReadLine();
            string filterCompany = string.IsNullOrWhiteSpace(company) ? null : company;

            var suitableHouses = district.FindHouse(filterAddress,filterYear,filterSquare, filterCost,filterCompany);
            if(suitableHouses!=null)
                Console.WriteLine(suitableHouses.ToString());
            else
                Console.WriteLine("Нет подходящих домов");
            Console.WriteLine("\nДля выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
        public static void FilterHousesInDistrict()
        {
            List<House> filteredHouses = null;
            char key;
            Console.Clear();
            Console.WriteLine("Выберете тип фильтрации:");
            Console.WriteLine("\t1 - год");
            Console.WriteLine("\t2 - стоимость");
            Console.WriteLine("\t3 - площадь");
            key = Console.ReadKey(true).KeyChar;
            switch (key)
            {
                case '1':
                    int year = GetItemIntoInterval("Год постройки (от 1920): ",1919, DateTime.Now.Year);
                    filteredHouses = district.FilterItemsYoungerYearOfBuild(year);
                    break;
                case '2':
                    int cost = GetPositiveField("Введите минимальную стоимость");
                    filteredHouses = district.FilterItemsBiggerThenCost(cost);
                    break;
                case '3':
                    int square = GetPositiveField("Введите минимальную площадь");
                    filteredHouses = district.FilterItemsBiggerThenSquare(square);
                    break;
            }
            if (filteredHouses.Count == 0)
                Console.WriteLine("Нет подходящих домов!");
            foreach (House house in filteredHouses)
            {
                Console.WriteLine(house.ToString());
            }
            Console.WriteLine("\nДля выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
        public static void CountStatistics()
        {
            Console.Clear();
            Console.WriteLine("Pасчёт статистических характеристик:");
            var countOfTypes = district.CountStatistics();
            foreach (var pair in countOfTypes)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value.ToString("F2")}");
            }
            Console.WriteLine("\nДля выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
        public static ApartmentHouse CreateApartmentHouse()
        {
            ApartmentHouse apartment;
            Console.Clear();
            Console.WriteLine("Создание многоквартирного дома");
            string address = GetNonEmptyInput("Адрес: ");
            int year = GetItemIntoInterval("Год постройки (от 1920): ", 1919, DateTime.Now.Year);
            int square = GetPositiveField("Площадь");
            Organization buildingCompany = GetBuildingCompany();
            int floor = GetItemIntoInterval("Кол-во этажей (от 3 до 15)", 2, ApartmentHouse.floorMax + 1);
            apartment = new ApartmentHouse(address, floor, year, square, buildingCompany);
            return apartment;
        }
        public static Cottage CreateCottage()
        {
            Cottage cottage;
            Console.Clear();
            Console.WriteLine("Создание коттедж");
            string address = GetNonEmptyInput("Адрес: ");
            int year = GetItemIntoInterval("Год постройки (от 1920): ", 1919, DateTime.Now.Year);
            int square = GetPositiveField("Площадь");
            int floors = GetItemIntoInterval("Кол-во этажей (от 1 до 4)", 0, Cottage.floorConstraint + 1);
            int rooms = GetPositiveField("Кол-во комнат");
            bool swimmingPool = GetBoolField("Наличие бассейна");
            Organization buildingCompany = GetBuildingCompany();
            cottage = new Cottage(address, rooms, floors, year, square, swimmingPool, buildingCompany);
            return cottage;
        }
        public static Office CreateOffice()
        {
            Office office;
            Console.Clear();
            Console.WriteLine("Создание офиса");
            string address = GetNonEmptyInput("Адрес: ");
            Console.WriteLine("Название: ");
            string name = Console.ReadLine();
            int year = GetItemIntoInterval("Год постройки (от 1920)", 1919, DateTime.Now.Year);
            int square = GetPositiveField("Площадь");
            bool parking = GetBoolField("Наличие парковки");
            Organization buildingCompany = GetBuildingCompany();
            office = new Office(name, address, square, year, parking, buildingCompany);
            return office;
        }
        public static Organization GetBuildingCompany()
        {
            Organization buildingCompany = null;
            char key;
            if (district.Houses.Count > 0)
                Console.WriteLine("1 - Взять существующую компанию");
            Console.WriteLine("Любая клавиша - Создать строительную компанию");
            key = Console.ReadKey(true).KeyChar;
            if (key == '1' && district.Houses.Count > 0)
            {
                for (int i = 0; i < District.BuildingCompanies.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {District.BuildingCompanies[i].Name}");
                }
                if (int.TryParse(Console.ReadLine(), out int index) && Conditions.CheckBelongToInterval(index, 0, District.BuildingCompanies.Count + 1))
                    buildingCompany = District.BuildingCompanies[index - 1];
            }
            else
            {
                Console.WriteLine("Создание строительной компании");
                string name = GetNonEmptyInput("Название: ");
                string director = GetNonEmptyInput("Директор: ");
                buildingCompany = new Organization(name, director);
                District.BuildingCompanies.Add(buildingCompany);
            }
            return buildingCompany;
        }
        public static int GetPositiveField(string field)
        {
            int value;
            while (true)
            {
                Console.WriteLine(field + ": ");
                if (int.TryParse(Console.ReadLine(), out value) && Conditions.CheckPositive(value))
                    break;
                else
                    Console.WriteLine($"{field} введено неверно!");
            }
            return value;
        }
        public static int GetItemIntoInterval(string field,int minValue, int maxValue)
        {
            int value;
            while (true)
            {
                Console.WriteLine(field + ": ");
                if (int.TryParse(Console.ReadLine(), out value) && Conditions.CheckBelongToInterval(value, minValue, maxValue))
                    break;
                else
                    Console.WriteLine($"{field} введено неверно!");
            }
            return value;
        }
        public static bool GetBoolField(string field)
        {
            char key;
            Console.WriteLine(field + ": ");
            Console.WriteLine("Введите Y есть да");
            key = Console.ReadKey(true).KeyChar;
            if (key == ((char)ConsoleKey.Y))
                return true;
            return false;
        }
        static string GetNonEmptyInput(string message)
        {
            string input;
            
            while(true)
            {
                Console.Write(message);
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    break;
            }

            return input;
        }

    }
}
