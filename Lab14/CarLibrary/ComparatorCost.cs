using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public class ComparatorCost : IComparer
    {
        public int Compare(object? x, object? y)
        {
            Car car1 = (Car)x;
            Car car2 = (Car)y;

            if (car1.Cost > car2.Cost)
                return 1;
            else if (car1.Cost < car2.Cost)
                return -1;
            else
                return 0;
        }
    }
}
