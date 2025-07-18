using System.Net;

namespace Lab_2.items
{
    public class Organization: IComparable<Organization>, IEquatable<Organization>
    {
        public string Name { get; set; }
        public int CountOfWorkers { get; set; }
        public string DirectorName { get; set; }
        public List<House> Houses { get; set; }

        public Organization(string name, string directorName)
        {
            Name = name;
            DirectorName = directorName;
            CountOfWorkers = 100;
            Houses = new List<House>();
        }

        public Organization() { }

        public Organization(Organization other)
        {
            Name = other.Name;
            DirectorName = other.DirectorName;
            CountOfWorkers = other.CountOfWorkers;
            Houses = other.Houses;
        }

        public string GoBankrupt()
        {
            CountOfWorkers = 0;
            Houses.Clear();
            return "Компания обанкротилась...";
        }

        public override string ToString()
        {
            return $"Строительная компания:\n" +
                $"Название: {Name}\n" +
                $"Директор: {DirectorName}\n" +
                $"Число сотрудников: {CountOfWorkers}\n" +
                $"Дома: {string.Join(", ", Houses.Select(h => h.Address))}\n";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 10;
                hash = hash * 25 + (Name?.GetHashCode() ?? 0);
                hash = hash * 25 + (DirectorName?.GetHashCode() ?? 0);
                return hash;
            }
        }

        public int CompareTo(Organization? other)
        {
            if(this !=null && other!=null)
                return Houses.Count.CompareTo(other.Houses.Count);
            throw new NotImplementedException();
        }

        public bool Equals(Organization? other)
        {
            return this == other;
        }
        public static bool operator ==(Organization org1, Organization org2)
        {
            if (ReferenceEquals(org1, org2))
                return true;
            if (org1 is null || org2 is null)
                return false;
            return (org1.Name == org2.Name) && (org1.DirectorName == org2.DirectorName);
        }

        public static bool operator >(Organization? left, Organization? right)
        {
            return left.Houses.Count > right.Houses.Count;
        }
        public static bool operator <(Organization? left, Organization? right)
        {
            return left.Houses.Count < right.Houses.Count;
        }

        public static bool operator !=(Organization org1, Organization org2)
        {
            return !(org1 == org2);
        }
    }
}
