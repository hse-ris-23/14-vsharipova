using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public class MiddleCar : Car
    {
        public bool FourWheelDrive { get; set; }
        public string OffRoadType { get; set; }

        public MiddleCar() { }

        public MiddleCar(string brand, int year, string color, int cost, int clearance, bool fourWheelDrive, string offRoadType) : base(brand, year, color, cost, clearance)
        {
            FourWheelDrive = fourWheelDrive;
            OffRoadType = offRoadType;
        }

        public override void Show()
        {
            base.Show();
            Console.Write($", Полный привод = {FourWheelDrive}, Тип бездорожья = {OffRoadType}");
        }

        public new void Show2()
        {
            base.Show2();
            Console.Write($", Полный привод = {FourWheelDrive}, Тип бездорожья = {OffRoadType}");
        }

        public override string ToString()
        {
            return base.ToString() + $", Полный привод = {FourWheelDrive}, Тип бездорожья = {OffRoadType}";
        }

        public override void RandomInit()
        {
            base.RandomInit();
            FourWheelDrive = random.Next() % 2 == 0;

            string[] roadTypes = { "лес", "снег", "болото", "гравий", "песок" };
            OffRoadType = roadTypes[random.Next(roadTypes.Length)];
        }

        public override void Init()
        {
            base.Init();
            Console.WriteLine("Введите тип бездорожья");
            OffRoadType = Console.ReadLine();

            Console.WriteLine("Полный привод? 1.Да 2.Нет");
            int choice = int.Parse(Console.ReadLine());

            FourWheelDrive = choice == 1;
        }

        public override bool Equals(object? obj)
        {
            return obj is MiddleCar car &&
                   base.Equals(obj) &&
                   FourWheelDrive == car.FourWheelDrive &&
                   OffRoadType == car.OffRoadType;
        }

        public override object Clone()
        {
            return new MiddleCar(Brand, Year, Color, Cost, Clearance, FourWheelDrive, OffRoadType);
        }
    }
}
