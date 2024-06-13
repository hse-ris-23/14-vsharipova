using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public class HeavyCar : Car
    {
        private double capacity;

        public HeavyCar() { }

        public HeavyCar(string brand, int year, string color, int cost, int clearance, double capacity) : base(brand, year, color, cost, clearance)
        {
            Capacity = capacity;
        }

        public HeavyCar(IdNumber id, string brand, int year, string color, int cost, int clearance, double capacity) : base(id, brand, year, color, cost, clearance)
        {
            Capacity = capacity;
        }

        public double Capacity
        {
            get { return capacity; }
            set 
            {
                if (value < 0)
                    throw new ArgumentException();
                capacity = value;
            }
        }

        public override void Show()
        {
            base.Show();
            Console.Write($", Грузоподъемность = {capacity}");
        }

        public new void Show2()
        {
            base.Show2();
            Console.Write($", Грузоподъемность = {capacity}");
        }

        public override string ToString()
        {
            return base.ToString() + $", Грузоподъемность = {capacity}";
        }

        public override void RandomInit()
        {
            base.RandomInit();
            Capacity = random.Next(1, 10);
        }

        public override void Init()
        {
            base.Init();
            Console.WriteLine("Введите грузоподъемность");
            Capacity = double.Parse(Console.ReadLine());
        }

        public override bool Equals(object? obj)
        {
            return obj is HeavyCar car &&
                   base.Equals(obj) &&
                   capacity == car.capacity;
        }

        public override object Clone()
        {
            return new HeavyCar(new IdNumber(IdNumber.Number), Brand, Year, Color, Cost, Clearance, Capacity);
        }
    }
}
