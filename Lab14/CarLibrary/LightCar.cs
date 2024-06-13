using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public class LightCar : Car
    {
        private int countPlaces;
        private int maxSpeed;

        public LightCar() { }
        public LightCar(string brand, int year, string color, int cost, int clearance, int countPlaces, int maxSpeed) : base(brand, year, color, cost, clearance)
        { 
            CountPlaces = countPlaces;
            MaxSpeed = maxSpeed;
        }

        public int CountPlaces
        {
            get { return countPlaces; }
            set 
            {
                if (value < 2 || value > 7)
                    throw new ArgumentException();
                countPlaces = value; 
            }
        }

        public int MaxSpeed
        {
            get { return maxSpeed; }
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                maxSpeed = value;
            }
        }

        public override void Show()
        {
            base.Show();
            Console.Write($", Кол-во мест = {CountPlaces}, Макс скорость = {MaxSpeed}");
        }

        public new void Show2()
        {
            base.Show2();
            Console.Write($", Кол-во мест = {CountPlaces}, Макс скорость = {MaxSpeed}");
        }

        public override string ToString()
        {
            return base.ToString() + $", Кол-во мест = {CountPlaces}, Макс скорость = {MaxSpeed}";
        }

        public override void RandomInit()
        {
            base.RandomInit();
            countPlaces = random.Next(2, 8);
            MaxSpeed = random.Next(100, 300);
        }

        public override void Init()
        {
            base.Init();
            Console.WriteLine("Введите кол-во мест");
            countPlaces = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите макс скорость");
            MaxSpeed = int.Parse(Console.ReadLine());
        }

        public override bool Equals(object? obj)
        {
            return obj is LightCar car &&
                   base.Equals(obj) &&
                   countPlaces == car.countPlaces &&
                   maxSpeed == car.maxSpeed;
        }

        public override object Clone()
        {
            return new LightCar(Brand, Year, Color, Cost, Clearance, CountPlaces, MaxSpeed);
        }
    }
}
