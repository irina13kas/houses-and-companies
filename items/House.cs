using System.ComponentModel.DataAnnotations;

namespace Lab_2.items
{
    public abstract class House: IComparable<House>, IEquatable<House>
    {
        private int yearOfBuild;
        private int square;
        protected static int costPerMetr = 20000;
        public string Address { get; set; }
        public int YearOfBuild { 
            get { return yearOfBuild; }
            set 
            {
                if (Conditions.CheckBelongToInterval(value,1919, DateTime.Now.Year))
                    yearOfBuild = value;
            } 
        }
        public virtual int Cost { get { return costPerMetr * Square; } private set { } }
        public int Square {
            get { return square; }
            set 
            { 
                if(Conditions.CheckPositive(value))
                    square = value;
            }
        }
        public Organization BuildingCompany { get; set; }

        protected House() { }

        protected House(string address, int yearOfBuild, int square, Organization buildingCompany)
        {
            Address = address;
            YearOfBuild = yearOfBuild;
            Square = square;
            BuildingCompany = buildingCompany;
            buildingCompany.Houses.Add(this);
        }

        ~House()
        {
            BuildingCompany.Houses.Remove(this);
        }
        public abstract string Buy();
        public abstract string Sell();
        public virtual string Repair()
        {
            return "Вы отремонтировали дом";
        }

        public override string ToString()
        {
            return "Дом:" +
                $"Адрес: {Address}\n" +
                $"Год постройки: {YearOfBuild} год\n" +
                $"Площадь: {Square} кв.м.\n" +
                $"Стоимость: {Cost} руб.\n" +
                $"Компания обслуживания: {BuildingCompany.Name}\n";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (Address?.GetHashCode() ?? 0);
                hash = hash * 23 + YearOfBuild.GetHashCode();
                hash = hash * 23 + Square.GetHashCode();
                hash = hash * 23 + (BuildingCompany?.GetHashCode() ?? 0);
                return hash;
            }
        }

        public int CompareTo(House? other)
        {
            if (this is not null && other is not null)
                return Cost.CompareTo(other.Cost);
            throw new NotImplementedException();
        }

        public bool Equals(House? other)
        {
            if (this == other) return true;
            return false;
        }
        public static bool operator ==(House h1, House h2)
        {
            if (ReferenceEquals(h1, h2))
                return true;
            if (h1 is null || h2 is null)
                return false;
            return h1.Address == h2.Address && h1.BuildingCompany.Name == h2.BuildingCompany.Name
                && h1.YearOfBuild == h2.YearOfBuild && h1.Square == h2.Square;
        }
        public static bool operator !=(House h1, House h2)
        {
            return !(h1 == h2);
        }
        public static bool operator >(House? left, House? right)
        {
            return left.YearOfBuild < right.YearOfBuild;
        }
        public static bool operator <(House? left, House? right)
        {
            return left.YearOfBuild > right.YearOfBuild;
        }

    }
}
