namespace Lab_2.items
{
    public class ApartmentHouse : House
    {
        public static readonly int floorMax = 15;
        public static readonly int floorMin = 2;
        public readonly int flatOnFloor = 6;
        private int floorCount;

        public int FloorCount
        {
            get { return floorCount; }
            set 
            {
                if(Conditions.CheckBelongToInterval(value, floorMin, floorMax))
                    floorCount = value;
            }
        }
        public int FlatCount { 
            get { return flatOnFloor * floorCount; }
            private set { }
        }

        public ApartmentHouse() : base() { }

        public ApartmentHouse( string address, int floor, int yearOfBuild, int square, Organization buildingCompany)
            : base(address,yearOfBuild, square, buildingCompany)
        {
            costPerMetr = 50000;
            FloorCount = floor;
        }

        public override string Buy()
        {
            if (FlatCount > 0)
            {
                FlatCount--;
                return $"Поздравляю с покупкой квартиры! Осталось квартир: {FloorCount}";
            }
            return "Квартир больше нет";
        }

        public override string Sell()
        {
            if (FlatCount <= floorCount*flatOnFloor)
            {
                FlatCount++;
                return $"Вы продали квартиру! Свободных квартир: {FloorCount}";
            }
            return "В этом доме нет занятых квартир!";
        }

        public override string Repair()
        {
            if (DateTime.Now.Year - YearOfBuild > 3)
                return "Проведен капитальный ремонт";
            return "Прошло меньше 3-ёх лет, пока ещё рано";
        }

        public override string ToString()
        {
            return $"Многоквартирный дом:\n" +
                $"Адрес: {Address}\n" +
                $"Год постройки: {YearOfBuild} год\n" +
                $"Площадь: {Square} кв.м.\n" +
                $"Кол-во этажей: {FloorCount}\n" +
                $"Свободных квартир: {FlatCount}\n" +
                $"Стоимость квартиры: {Cost} руб.\n"+
                $"Компания обсуживания: {BuildingCompany.Name}\n"+
                $"Директор компании: {BuildingCompany.DirectorName}\n";
        }
    }
}
