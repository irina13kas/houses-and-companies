using Lab_2.items;

namespace UnitTest1
{
    public class Tests
    {

        [Test]
        public void Test_AddHouseIntoRepository()
        {
            House house = new ApartmentHouse();
            District repo = new District();

            repo.CreateHouse(house);
            var houses = repo.Houses;

            Assert.That(houses.Count == 1);
            Assert.Contains(house, houses);
        }

        [Test]
        public void Test_RemoveHouseIntoRepository()
        {
            Organization org = new Organization("СтройГрупп", "Быков В.В.");
            House house = new Office("Алмаз", "ул.Руднева 15", 250, 2016, false, org);
            District repo = new District();

            repo.CreateHouse(house);
            repo.DestroyHouse(house);
            var houses = repo.Houses;

            Assert.That(houses.Count == 0);
            Assert.False(houses.Contains(house));
        }

        [Test]
        public void Test_SortByAddress()
        {
            Organization org = new Organization("СтройГрупп", "Быков В.В.");
            House house_1 = new Office("Алмаз", "ул.Руднева 15", 250, 2016, false, org);
            House house_2 = new ApartmentHouse("ул.Вавиловых 10 к3", 10, 1980, 455, org);
            House house_3 = new Cottage("ул.Соколова 7", 5, 3, 2007, 130, true, org);
            District repo = new District();

            repo.CreateHouse(house_1);
            repo.CreateHouse(house_2);
            repo.CreateHouse(house_3);
            var sortedHouses = repo.SortByAddress();

            Assert.That(sortedHouses.Count == 3);
            Assert.That(sortedHouses[0], Is.EqualTo(house_2));
            Assert.That(sortedHouses[1], Is.EqualTo(house_1));
            Assert.That(sortedHouses[2], Is.EqualTo(house_3));
        }

        [Test]
        public void Test_SortByYear()
        {
            Organization org = new Organization("СтройГрупп", "Быков В.В.");
            House house_1 = new Office("Алмаз", "ул.Руднева 15", 250, 2016, false, org);
            House house_2 = new ApartmentHouse("ул.Вавиловых 10 к3", 10, 1980, 455, org);
            House house_3 = new Cottage("ул.Соколова 7", 5, 3, 2007, 130, true, org);
            District repo = new District();

            repo.CreateHouse(house_1);
            repo.CreateHouse(house_2);
            repo.CreateHouse(house_3);
            var sortedHouses = repo.SortByYearOfBuild();

            Assert.That(sortedHouses.Count == 3);
            Assert.That(sortedHouses[0], Is.EqualTo(house_2));
            Assert.That(sortedHouses[1], Is.EqualTo(house_3));
            Assert.That(sortedHouses[2], Is.EqualTo(house_1));
        }

        [Test]
        public void Test_SortByBuildingCompany()
        {
            Organization org_1 = new Organization("СтройГрупп", "Быков В.В.");
            Organization org_2 = new Organization("BuildCompany", "Ушаков Г.С.");
            House house_1 = new Office("Алмаз", "ул.Руднева 15", 250, 2016, false, org_1);
            House house_2 = new ApartmentHouse("ул.Вавиловых 10 к3", 10, 1980, 455, org_2);
            House house_3 = new Cottage("ул.Соколова 7", 5, 3, 2007, 130, true, org_1);
            District repo = new District();

            repo.CreateHouse(house_1);
            repo.CreateHouse(house_2);
            repo.CreateHouse(house_3);
            var sortedHouses = repo.SortByBuildingCompany();

            Assert.That(sortedHouses.Count == 3);
            Assert.That(sortedHouses[0], Is.EqualTo(house_2));
            Assert.That(sortedHouses[1], Is.EqualTo(house_1));
            Assert.That(sortedHouses[2], Is.EqualTo(house_3));
        }

        [Test]
        public void Test_CountItemsOfEachType()
        {
            Organization org = new Organization("СтройГрупп", "Быков В.В.");
            House house_1 = new Office("Алмаз", "ул.Руднева 15", 250, 2016, false, org);
            House house_2 = new Office("Плойка", "ул.Баренца 77", 150, 2010, false, org);
            House house_3 = new ApartmentHouse("ул.Вавиловых 10 к3", 10, 1980, 455, org);
            District repo = new District();

            repo.CreateHouse(house_1);
            repo.CreateHouse(house_2);
            repo.CreateHouse(house_3);
            var typesOfHouses = repo.CountItemsOfEachType();

            Assert.That(typesOfHouses.Count, Is.EqualTo(3));
            Assert.That(typesOfHouses["Office"] == 2);
            Assert.That(typesOfHouses["ApartmentHouse"] == 1);
            Assert.That(typesOfHouses["Organization"] == 1);
        }

        [Test]
        public void Test_FindHouse()
        {
            Organization org = new Organization("СтройГрупп", "Быков В.В.");
            House house_1 = new Office("Алмаз", "ул.Руднева 15", 250, 2016, false, org);
            House house_2 = new ApartmentHouse("ул.Вавиловых 10 к3", 10, 1980, 455, org);
            House house_3 = new Cottage("ул.Соколова 7", 5, 3, 2007, 130, true, org);
            District repo = new District();
            string address = null;
            int year = 2007;
            int? square = (int?)null;
            int? cost = (int?)null;
            string companyName = "СтройГрупп";

            repo.CreateHouse(house_1);
            repo.CreateHouse(house_2);
            repo.CreateHouse(house_3);
            var suitableHouse = repo.FindHouse(address, year, square, cost, companyName);

            Assert.False(suitableHouse == null);
            Assert.That(suitableHouse, Is.EqualTo(house_3));
        }

        [Test]
        public void Test_FilterItemsOlderYearOfBuild()
        {
            Organization org = new Organization("СтройГрупп", "Быков В.В.");
            House house_1 = new Office("Алмаз", "ул.Руднева 15", 250, 2016, false, org);
            House house_2 = new ApartmentHouse("ул.Вавиловых 10 к3", 10, 1980, 455, org);
            House house_3 = new Cottage("ул.Соколова 7", 5, 3, 2007, 130, true, org);
            District repo = new District();
            int year = 2006;

            repo.CreateHouse(house_1);
            repo.CreateHouse(house_2);
            repo.CreateHouse(house_3);
            var suitableHouse = repo.FilterItemsYoungerYearOfBuild(year);

            Assert.That(suitableHouse.Count, Is.EqualTo(2));
            Assert.Contains(house_1, suitableHouse);
            Assert.Contains(house_3, suitableHouse);
        }

        [Test]
        public void Test_FilterItemsBiggerThenSquare()
        {
            Organization org = new Organization("СтройГрупп", "Быков В.В.");
            House house_1 = new Office("Алмаз", "ул.Руднева 15", 250, 2016, false, org);
            House house_2 = new ApartmentHouse("ул.Вавиловых 10 к3", 10, 1980, 455, org);
            House house_3 = new Cottage("ул.Соколова 7", 5, 3, 2007, 130, true, org);
            District repo = new District();
            int square = 300;

            repo.CreateHouse(house_1);
            repo.CreateHouse(house_2);
            repo.CreateHouse(house_3);
            var suitableHouse = repo.FilterItemsBiggerThenSquare(square);

            Assert.That(suitableHouse.Count, Is.EqualTo(1));
            Assert.Contains(house_2, suitableHouse);
        }

        [Test]
        public void Test_FilterItemsBiggerThenCost()
        {
            Organization org = new Organization("СтройГрупп", "Быков В.В.");
            House house_1 = new Office("Алмаз", "ул.Руднева 15", 250, 2016, false, org);
            House house_2 = new ApartmentHouse("ул.Вавиловых 10 к3", 10, 1980, 455, org);
            House house_3 = new Cottage("ул.Соколова 7", 5, 3, 2007, 130, true, org);
            District repo = new District();
            int cost = 20000000;

            repo.CreateHouse(house_1);
            repo.CreateHouse(house_2);
            repo.CreateHouse(house_3);
            var suitableHouse = repo.FilterItemsBiggerThenCost(cost);

            Assert.That(suitableHouse.Count, Is.EqualTo(2));
            Assert.Contains(house_1, suitableHouse);
            Assert.Contains(house_2, suitableHouse);
        }

        [Test]
        public void Test_CountStatistics()
        {
            Organization org = new Organization("СтройГрупп", "Быков В.В.");
            House house_1 = new Office("Алмаз", "ул.Руднева 15", 250, 2016, false, org);
            House house_2 = new ApartmentHouse("ул.Вавиловых 10 к3", 10, 1980, 455, org);
            House house_3 = new Cottage("ул.Соколова 7", 5, 3, 2007, 135, true, org);
            District repo = new District();

            repo.CreateHouse(house_1);
            repo.CreateHouse(house_2);
            repo.CreateHouse(house_3);
            var statistics = repo.CountStatistics();

            Assert.That(statistics["Всего элементов"] == 3);
            Assert.That(statistics["Средняя стоимость"] == 22341666.666666668);
            Assert.That(statistics["Средняя площадь"] == 280);
        }

        [Test]
        public void Test_IndexStringOverride()
        {
            Organization org = new Organization("СтройГрупп", "Быков В.В.");
            House house_1 = new Office("Алмаз", "ул.Руднева 15", 250, 2016, false, org);
            District repo = new District();

            repo.CreateHouse(house_1);
            var index = repo["ул.Руднева 15"];

            Assert.That(index, Is.EqualTo(repo.Houses[0]));
        }


        [Test]
        public void Test_IndexIntOverride()
        {
            Organization org = new Organization("СтройГрупп", "Быков В.В.");
            House house_1 = new Office("Алмаз", "ул.Руднева 15", 250, 2016, false, org);
            District repo = new District();

            repo.CreateHouse(house_1);
            var index = repo[0];

            Assert.That(index, Is.EqualTo(repo.Houses[0]));
        }
    }
}