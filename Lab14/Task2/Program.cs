using CarLibrary;
using MyCollectionLibrary;

namespace Task2
{
    internal class Program
    {
        static Car CreateCar()
        {
            Random random = new Random();
            int choice = random.Next(1, 4);
            Car car;
            if (choice == 1)
                car = new HeavyCar();
            else if (choice == 2)
                car = new LightCar();
            else
                car = new MiddleCar();
            car.RandomInit();
            return car;
        }
        
        static void ShowTable(IEnumerable<Car> table)
        {
            foreach (Car car in table)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine();
        }

        static List<Car> FindCarsByPriceAndBrandExt(HTable<Car> table, string brand, int price)
        {
            return table.Where(t=>t.Brand == brand && t.Cost > price).ToList();
        }

        static List<Car> FindCarsByPriceAndBrandLinq(HTable<Car> table, string brand, int price)
        {
            return (from t in table where t.Brand == brand && t.Cost > price select t).ToList();
        }

        static int CountLightCarsByPlacesExt(HTable<Car> table, int places) 
        {
            return table.Count(t => t is LightCar p && p.CountPlaces == places);
        }

        static int CountLightCarsByPlacesLinq(HTable<Car> table, int places)
        {
            return (from t in table where t is LightCar p && p.CountPlaces == places select t).Count();
        }

        static double AverageCostByModelExt(HTable<Car> table, string brand)
        {
            return table.Where(t => t.Brand == brand).Average(t => t.Cost);
        }

        static double AverageCostByModelLinq(HTable<Car> table, string brand)
        {
            return (from t in table where t.Brand == brand select t.Cost).Average();
        }

        static void ShowDict(Dictionary<int, Car> dict)
        {
            foreach (var item in dict)
            {
                Console.WriteLine($"Год - {item.Key}");
                item.Value.Show();
                Console.WriteLine();
            }
        }

        static Dictionary<int, Car> FindExpenseCarGroupByYearExt(HTable<Car> table)
        {
            var res = table.GroupBy(t => t.Year);
            Dictionary<int, Car> dict = new Dictionary<int, Car>();
            foreach (var item in res)
            {
                var sortCars = item.OrderBy(t => t.Cost);
                var last = sortCars.Last();

                dict[item.Key] = last;
            }
            return dict;
        }

        static Dictionary<int, Car> FindExpenseCarGroupByYearLinq(HTable<Car> table)
        {
            var res = from t in table group t by t.Year;
            Dictionary<int, Car> dict = new Dictionary<int, Car>();
            foreach (var item in res)
            {
                var last = (from t in item orderby t.Cost select t).Last();
                dict[item.Key] = last;
            }
            return dict;
        }

        static void Main(string[] args)
        {
            HTable<Car> cars = new HTable<Car>(50);

            int action;
            do
            {
                Console.WriteLine("1.Добавить объекты");
                Console.WriteLine("2.Вывести объекты");
                Console.WriteLine("3.Получить авто заданной модели со стоимостью больше заданной");
                Console.WriteLine("4.Посчитать кол-во легковых автомобилей с заданным кол-вом мест");
                Console.WriteLine("5.Получить среднюю стоимость по заданной модели");
                Console.WriteLine("6.Найти самый дорогой авто по годам");

                action = int.Parse(Console.ReadLine());

                if (action == 1)
                {
                    Console.WriteLine("Введите кол-во");
                    int n = int.Parse(Console.ReadLine());

                    for (int i = 0; i < n; i++)
                    {
                        cars.Add(CreateCar());
                    }
                }
                else if (action == 2)
                {
                    ShowTable(cars);
                }
                else if (action == 3)
                {
                    Console.WriteLine("Введите модель");
                    string brand = Console.ReadLine();

                    Console.WriteLine("Введите стоимость");
                    int price = int.Parse(Console.ReadLine());

                    var res1 = FindCarsByPriceAndBrandExt(cars, brand, price);
                    ShowTable(res1);

                    var res2 = FindCarsByPriceAndBrandLinq(cars, brand, price);
                    ShowTable(res2);
                }
                else if (action == 4)
                {
                    Console.WriteLine("Введите кол-во мест");
                    int countPlaces = int.Parse(Console.ReadLine());

                    int count1 = CountLightCarsByPlacesExt(cars, countPlaces);
                    Console.WriteLine("Кол-во = " + count1);

                    int count2 = CountLightCarsByPlacesLinq(cars, countPlaces);
                    Console.WriteLine("Кол-во = " + count1);
                }
                else if (action == 5)
                {
                    Console.WriteLine("Введите модель");
                    string brand = Console.ReadLine();

                    double avg1 = AverageCostByModelExt(cars, brand);
                    Console.WriteLine("Результат = " + avg1);

                    double avg2 = AverageCostByModelLinq(cars, brand);
                    Console.WriteLine("Результат = " + avg2);
                }
                else if (action == 6)
                {
                    var res1 = FindExpenseCarGroupByYearExt(cars);
                    ShowDict(res1);

                    Console.WriteLine();

                    var res2 = FindExpenseCarGroupByYearLinq(cars);
                    ShowDict(res2);
                }

            } while (true);
        }
    }
}
