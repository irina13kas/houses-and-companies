namespace Lab_2.items
{
    public sealed class Office : House
    {
        public string Name { get; set; }
        public bool IsSelled{get;private set;}
        public bool IsParking { get; set; }
        public override int Cost{get{return (int)(costPerMetr*Square*1.2);}}
        public Office(string name, string address, int square, int yearOfBuild, bool isParking, Organization buildingCompany)
            :base(address,yearOfBuild, square, buildingCompany)
        {
            costPerMetr = 35000;
            Name = name;
            IsParking = isParking;
        }

        public Office() : base() { }

        public override string Buy()
        {
            if (IsSelled)
                return "Этот офис уже куплен";
            return $"Поздавляю! Вы купили офис по адресу {Address}";
        }

        public override string Sell()
        {
            if (!IsSelled)
                return $"Вы продали офис по адресу {Address}";
            return "Этот офис свободен";
        }

        public override string Repair()
        {
            if (DateTime.Now.Year - YearOfBuild > 5)
                return "Проведен капитальный ремонт";
            return "Прошло меньше 5-ти лет, пока ещё рано";
        }

        public override string? ToString()
        {
            return $"Офис:\n" +
                $"Адрес: {Address}\n" +
                $"Название: {Name}\n" +
                $"Год постройки: {YearOfBuild} год\n" +
                $"Площадь: {Square} кв.м.\n" +
                $"Наличие парковки: {(IsParking ? "Есть" : "Нет")}\n"+
                $"Статус: {(IsSelled ? "Занят" : "Свободен")}\n" +
                $"Цена: {Cost} руб.\n"+
                $"Компания обслуживания: {BuildingCompany.Name}\n" +
                $"Директор компании: {BuildingCompany.DirectorName}\n";
        }
    }
}
