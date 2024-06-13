namespace CarLibrary
{
    public abstract class Car : IInit, IComparable, ICloneable
    {
        private string brand;
        private int year;
        private string color;
        private int cost;
        private int clearance;
        private IdNumber id;
        protected static Random random = new Random();

        public Car() { }

        public Car(string brand, int year, string color, int cost, int clearance)
        {
            Brand = brand;
            Year = year;
            Color = color;
            Cost = cost;
            Clearance = clearance;
        }

        public Car(IdNumber id, string brand, int year, string color, int cost, int clearance)
        {
            Brand = brand;
            Year = year;
            Color = color;
            Cost = cost;
            this.id = id;
        }

        public IdNumber IdNumber { get { return id; } }

        public string Brand
        {
            get { return brand; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Brand");
                brand = value; 
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                if (value > DateTime.Now.Year || value < 1800)
                    throw new ArgumentException();
                year = value;
            }
        }

        public string Color
        {
            get { return color; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Color");
                color = value;
            }
        }

        public int Cost
        {
            get { return cost; }
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                cost = value;
            }
        }

        public int Clearance
        {
            get { return clearance; }
            set
            {
                if (value < 0 || value > 1000)
                    throw new ArgumentException();
                clearance = value;
            }
        }

        public virtual void Show()
        {
            Console.Write($"Модель = {Brand}, Год выпуска = {Year}, Цвет = {Color}, Стоимость = {Cost}, Дорожный просвет = {Clearance}");
        }

        public void Show2()
        {
            Console.Write($"Модель = {Brand}, Год выпуска = {Year}, Цвет = {Color}, Стоимость = {Cost}, Дорожный просвет = {Clearance}");
        }

        public override string ToString()
        {
            return $"Модель = {Brand}, Год выпуска = {Year}, Цвет = {Color}, Стоимость = {Cost}, Дорожный просвет = {Clearance}";
        }

        public virtual void Init()
        {
            Console.WriteLine("Введите модель");
            Brand = Console.ReadLine();
            Console.WriteLine("Введите год выпуска");
            Year = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите цвет");
            Color = Console.ReadLine();
            Console.WriteLine("Введите стоимость");
            Cost = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите дорожный просвет");
            Clearance = int.Parse(Console.ReadLine());
        }

        public virtual void RandomInit()
        {
            string[] models = { "Toyota", "Lamborghini", "Chevrolet", "Hyundai", "Honda", "Renault", "Ford" };
            string[] colors = { "red", "green", "blue", "white", "black", "yellow", "orange" };

            brand = models[random.Next(models.Length)];
            color = colors[random.Next(colors.Length)];
            Year = random.Next(2000, 2024);
            Cost = random.Next(500000, 6000000);
            Clearance = random.Next(100, 250);
            id = new IdNumber(random.Next(1000));
        }

        public override bool Equals(object? obj)
        {
            return obj is Car car &&
                   brand == car.brand &&
                   year == car.year &&
                   color == car.color &&
                   cost == car.cost &&
                   clearance == car.clearance;
        }

        public int CompareTo(Car? other)
        {
            return Brand.CompareTo(other.brand);
        }

        public int CompareTo(object? obj)
        {
            if (obj is Car other)
                return Brand.CompareTo(other.brand);
            return 0;
        }

        public abstract object Clone();

        public override int GetHashCode()
        {
            return HashCode.Combine(Brand, Year, Color, Cost, Clearance);
        }
    }
}