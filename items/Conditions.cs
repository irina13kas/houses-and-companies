using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2.items
{
    public class Conditions
    {
        public static bool CheckBelongToInterval(int value, int minValue, int maxValue)
        {
            if (value < maxValue && value > minValue)
                return true;
            return false;
        }
        public static bool CheckPositive(int value)
        {
            if (value > 0)
                return true;
            return false;
        }
    }
}
