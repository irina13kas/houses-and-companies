namespace Lab_2.items
{
    public class District
    {
        public List<House> Houses { get; set; }
        public static List<Organization> BuildingCompanies { get; set; }
        public District() 
        { 
            Houses = new List<House>();
            BuildingCompanies = new List<Organization>();
        }

        public void Do()
        {
            Console.WriteLine("FFFF");
        }
        public void CreateHouse(House house)
        {
            if (house != null)
            {
                Houses.Add(house);
                if(!BuildingCompanies.Contains(house.BuildingCompany))
                    BuildingCompanies.Add(house.BuildingCompany);
            }
        }

        public void DestroyHouse(House house)
        {
            if (house != null)
            {
                BuildingCompanies.Remove(house.BuildingCompany);
                Houses.Remove(house);
            }
        }

        public List<House> SortByAddress()
        {
            var resultHouses = Houses.OrderBy(h=>h.Address.ToLower()).ToList();
            return resultHouses;
        }

        public List<House> SortByYearOfBuild()
        {
            var resultHouses = Houses.OrderBy(h => h.YearOfBuild).ToList();
            return resultHouses;
        }

        public List<House> SortByBuildingCompany()
        {
            var resultHouses = Houses.OrderBy(h => h.BuildingCompany.Name.ToLower()).ToList();
            return resultHouses;
        }

        public Dictionary<string, int> CountItemsOfEachType()
        {
            Dictionary<string, int> types = new();
            foreach(House house in Houses)
            {
                if (!types.ContainsKey(house.GetType().Name)){
                    types.Add(house.GetType().Name, 1);
                }
                else
                {
                    types[house.GetType().Name]++;
                }
            }
            types.Add("Organization",BuildingCompanies.Count);
            return types;
        }
        public House FindHouse(string address, int? year, int? square, int? cost, string buildingCompanyName)
        {
            var suitableHouse = Houses
                .Where(h => (address == null || h.Address.Equals(address, StringComparison.OrdinalIgnoreCase)))
                .Where(h => (!year.HasValue || h.YearOfBuild == year.Value))
                .Where(h => (!square.HasValue || h.Square == square.Value))
                .Where(h => (!cost.HasValue || h.Cost == cost.Value))
                .Where(h => (buildingCompanyName == null || h.BuildingCompany.Name.Equals(buildingCompanyName, StringComparison.OrdinalIgnoreCase)))
                .ToList()
                .FirstOrDefault();
            return suitableHouse;
        }
        public List<House> FilterItemsYoungerYearOfBuild(int yearOfBuild)
        {
            List<House> houses = Houses.Where(h => h.YearOfBuild >= yearOfBuild).ToList();
            return houses;
        }
        public List<House> FilterItemsBiggerThenCost(int cost)
        {
            List<House> houses = Houses.Where(h => h.Cost >= cost).ToList();
            return houses;
        }
        public List<House> FilterItemsBiggerThenSquare(int square)
        {
            List<House> houses = Houses.Where(h => h.Square >= square).ToList();
            return houses;
        }
        public House this[int index]
        {
            get 
            {
                if(index>=0 && index<Houses.Count)
                    return Houses[index];
                return null;
            }
            set
            {
                if (index >= 0 && index < Houses.Count)
                    Houses[index] = value;
            }
        }
        public House this[string address]
        {
            get
            {
                return Houses.FirstOrDefault(h=>h.Address.ToLower()==address.ToLower());
            }
            set
            {
                var house = Houses.Find(h => h.Address.ToLower() == address.ToLower());
                house = value;
            }
        }

        public Dictionary<string,double> CountStatistics()
        {
            Dictionary<string, double> statistics = new();
            statistics.Add("Всего элементов",Houses.Count);
            statistics.Add("Средняя стоимость",Houses.Average(h=>h.Cost));
            statistics.Add("Средняя площадь", Houses.Average(h => h.Square));
            return statistics;
        }
    }
}
