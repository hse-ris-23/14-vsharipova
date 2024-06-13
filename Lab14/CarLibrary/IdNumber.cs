using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public class IdNumber
    {
        private int number;

        public IdNumber(int number)
        {
            Number = number;
        }

        public int Number 
        { 
            get { return number; } 
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Number");
                number = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is IdNumber number &&
                   this.number == number.number;
        }

        public override string ToString()
        {
            return number.ToString();
        }

    }
}
