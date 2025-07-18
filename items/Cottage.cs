namespace Lab_2.items
{
    public sealed class Cottage : House
    {
        private int floorCount;
        private int roomCount;
        public static readonly int floorConstraint = 4;
        public int RoomsCount {
            get { return roomCount; }
            set
            {
                if(Conditions.CheckPositive(value))
                    roomCount = value;
            }
        }
        public bool IsHavingPool { get; set; }
        public bool IsSelled { get; private set; }
        public int FloorCount
        {
            get { return floorCount; }
            set
            {
                if (Conditions.CheckBelongToInterval(value,0, floorConstraint))
                    floorCount = value;
            }
        }
        public override int Cost { get { return (int)(costPerMetr * Square * 1.5); } }
        public Cottage(string address, int roomsCount, int floorCount, int yearOfBuild, int square, bool isHavingPool,
            Organization buildingCompany)
            :base(address, yearOfBuild, square, buildingCompany)
        {
            costPerMetr = 70000;
            RoomsCount = roomsCount;
            FloorCount = floorCount;
            IsHavingPool = isHavingPool;
        }

        public Cottage() : base() { }

        public override string Buy()
        {
            if (IsSelled)
                return "Этот дом уже куплен";
            return $"Поздавляю! Вы купили дом по адресу {Address}";
        }

        public override string Sell()
        {
            if (!IsSelled)
                return $"Вы продали дом по адресу {Address}";
            return "Этот дом свободен";
        }

        public override string ToString() 
        {
            return $"Коттедж:\n" +
                $"Адрес: {Address}\n" +
                $"Год постройки: {YearOfBuild} год\n" +
                $"Кол-во этажей: {FloorCount}\n" +
                $"Кол-во комнат: {RoomsCount}\n" +
                $"Площадь: {Square} кв.м.\n" +
                $"Статус: {(IsSelled ? "Занят" : "Свободен")}\n" +
                $"Наличие бассейна: {(IsHavingPool ? "Есть" : "Нет")}\n" +
                $"Цена: {Cost} руб.\n"+
                $"Компания обслуживания: {BuildingCompany.Name}\n"+
                $"Директор компании: {BuildingCompany.DirectorName}\n";
        }
    }
}
