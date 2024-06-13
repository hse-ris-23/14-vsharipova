using CarLibrary;
using System.Collections.Generic;

namespace Lab14
{
    internal class Program
    {
        static void FillQueue(Queue<List<Car>> queue)
        {
            Random random = new Random();
            int count = random.Next(5, 10);

            for (int i = 0; i < count; i++)
            {
                int countCars = random.Next(3, 10);
                List<Car> cars = new List<Car>();
                for (int j = 0; j < countCars; j++)
                {
                    int choice = random.Next(1, 4);
                    Car car;
                    if (choice == 1)
                        car = new LightCar();
                    else if (choice == 2)
                        car = new MiddleCar();
                    else
                        car = new HeavyCar();

                    car.RandomInit();
                    cars.Add(car);
                }
                queue.Enqueue(cars);
            }
        }

        static void Show(Queue<List<Car>> queue)
        {
            int i = 1;
            foreach (var item in queue)
            {
                Console.WriteLine("Цех №" + i);
                foreach (var car in item)
                {
                    car.Show();
                    Console.WriteLine();
                }
                i++;
                Console.WriteLine();
            }
        }

        static void ShowHeavyCarExt(Queue<List<Car>> queue)
        {
            Console.WriteLine("Метод расширения");
            var result = queue.SelectMany(t => t).Where(t => t is HeavyCar);
            foreach (var item in result)
            {
                item.Show();
                Console.WriteLine();
            }
        }

        static void ShowHeavyCarLinq(Queue<List<Car>> queue)
        {
            Console.WriteLine("LINQ");
            var result = from t in queue.SelectMany(t => t) where t is HeavyCar select t;
            foreach (var item in result)
            {
                item.Show();
                Console.WriteLine();
            }
        }

        static void FindQueueMaxCarsExt(Queue<List<Car>> queue)
        {
            int maxCount = queue.Max(t => t.Count);
            var maxQueue = queue.First(t => t.Count == maxCount);
            int index = queue.ToList().IndexOf(maxQueue);

            Console.WriteLine($"Максимальное кол-во авто = {maxCount}, номер цеха = {index + 1}");
        }

        static void FindQueueMaxCarsLinq(Queue<List<Car>> queue)
        {
            int maxCount = (from list in queue select list.Count).Max();
            var maxQueue = (from list in queue where list.Count == maxCount select list).First();
            int index = queue.ToList().IndexOf(maxQueue);

            Console.WriteLine($"Максимальное кол-во авто = {maxCount}, номер цеха = {index + 1}");
        }

        static void GroupByBrandExt(Queue<List<Car>> queue)
        {
            var result = queue.SelectMany(t => t).GroupBy(t => t.Brand);
            foreach (var item in result)
            {
                Console.WriteLine($"Модель - {item.Key}, Кол-во - {item.Count()}");
            }
        }

        static void GroupByBrandLinq(Queue<List<Car>> queue)
        {
            var result = (from car in queue.SelectMany(t => t) group car by car.Brand);
            foreach (var item in result)
            {
                Console.WriteLine($"Модель - {item.Key}, Кол-во - {item.Count()}");
            }
        }

        static void UnionFirstLastExt(Queue<List<Car>> queue)
        {
            var result = queue.First().Union(queue.Last());
            foreach (var item in result)
            {
                item.Show();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void UnionFirstLastLinq(Queue<List<Car>> queue)
        {
            var result = (from t in queue.First() select t).Union(from p in queue.Last() select p);
            foreach (var item in result)
            {
                item.Show();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void FillListStudents(List<Student> students)
        {
            Random random = new Random();
            int count = random.Next(5, 10);
            for (int i = 0; i < count; i++)
            {
                Student student = new Student();
                student.RandomInit();

                students.Add(student);
            }
        }

        static void ShowStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine(student.ToString());
            }
        }

        static void JoinStudentsAndCarExt(List<Student> students, Queue<List<Car>> queue)
        {
            var res = students.Join(
                queue.SelectMany(t => t), 
                p => p.BrandCar, 
                v => v.Brand, 
                (p, v) => new { Name = p.Name, Brand = p.BrandCar, Price = v.Cost }
            );

            foreach (var item in res)
            {
                Console.WriteLine($"Имя: {item.Name}, Модель: {item.Brand}, Цена: {item.Price}");
            }
            Console.WriteLine();
        }

        static void JoinStudentsAndCarLinq(List<Student> students, Queue<List<Car>> queue)
        {
            var res = from student in students
                      join c in queue.SelectMany(t => t) on student.BrandCar equals c.Brand
                      select new { Name = student.Name, Brand = student.BrandCar, Price = c.Cost };

            foreach (var item in res)
            {
                Console.WriteLine($"Имя: {item.Name}, Модель: {item.Brand}, Цена: {item.Price}");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Queue<List<Car>> queue = new Queue<List<Car>>();
            FillQueue(queue);

            do
            {
                Console.WriteLine("1.Вывести на экран");
                Console.WriteLine("2.Из всех цехов выбрать грузовые авто");
                Console.WriteLine("3.Найти цех с максимальным кол-вом авто");
                Console.WriteLine("4.Вывести сколько автомобилей какой марки");
                Console.WriteLine("5.Объединение первого и последнего цеха");
                Console.WriteLine("6.Соединение студентов с авто");

                int action = int.Parse(Console.ReadLine());
                if (action == 1)
                {
                    Show(queue);
                }
                else if (action == 2)
                {
                    ShowHeavyCarExt(queue);
                    ShowHeavyCarLinq(queue);
                }
                else if (action == 3)
                {
                    FindQueueMaxCarsExt(queue);
                    FindQueueMaxCarsLinq(queue);
                }
                else if (action == 4)
                {
                    GroupByBrandExt(queue);
                    GroupByBrandLinq(queue);
                }
                else if (action == 5)
                {
                    UnionFirstLastExt(queue);
                    UnionFirstLastLinq(queue);
                }
                else if (action == 6)
                {
                    List<Student> students = new List<Student>();
                    FillListStudents(students);
                    ShowStudents(students);

                    JoinStudentsAndCarExt(students, queue);
                    JoinStudentsAndCarLinq(students, queue);
                }

            } while (true);
        }
    }
}
