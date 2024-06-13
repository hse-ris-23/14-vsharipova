using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public class Student : IInit
    {
        private string name;
        private int age;
        private double gpa;
        private static int countInstance = 0;
        private static Random random = new Random();

        public Student()
        {
            name = "No Name";
            age = 0;
            gpa = 0;
            countInstance++;
        }

        public Student(string name, int age, double gpa)
        {
            Name = name;
            Age = age;
            Gpa = gpa;
            countInstance++;
        }

        public Student(Student other)
        {
            Name = other.name;
            Age = other.age;
            Gpa = other.gpa;
            countInstance++;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public double Gpa
        {
            get { return gpa; }
            set { gpa = value; }
        }

        public string BrandCar { get; set; }

        public string Compare(Student other)
        {
            string s = Name + " ";
            if (Age > other.Age)
                s += "старше ";
            else if (Age < other.Age)
                s += "младше ";
            else
                s += "ровесник ";
            s += other.name + ". ";

            s += $"GPA {Name} ";
            if (Gpa > other.Gpa)
                s += "больше ";
            else if (Gpa < other.Gpa)
                s += "меньше ";
            else
                s += "равен ";

            s += $" GPA {other.Name}";

            return s;
        }

        public static string Compare(Student a, Student b)
        {
            return a.Compare(b);
        }

        public static int GetCountInstance()
        {
            return countInstance;
        }

        public void Init()
        {
            Console.WriteLine("Введите имя");
            name = Console.ReadLine();

            Console.WriteLine("Введите возраст");
            age = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите оценку");
            gpa = double.Parse(Console.ReadLine());
        }

        public void RandomInit()
        {
            string[] names = { "Иван", "Олег", "Михаил", "Игорь", "Анна", "Мария" };
            string[] models = { "Toyota", "Lamborghini", "Chevrolet", "Hyundai", "Honda", "Renault", "Ford" };

            name = names[random.Next(names.Length)];
            age = random.Next(10, 40);
            gpa = Math.Round(random.NextDouble() * random.Next(1,10), 1);
            BrandCar = models[random.Next(models.Length)];
        }

        public static Student operator ~(Student student)
        {
            string newName = char.ToUpper(student.name[0]) + student.name.Substring(1);
            return new Student(newName, student.age, student.gpa);
        }

        public static Student operator ++(Student student)
        {
            student.age++;
            return student;
        }

        public static explicit operator int(Student student)
        {
            int course = -1;
            if (student.age == 18)
                course = 1;
            else if (student.age == 19)
                course = 2;
            else if (student.age == 20)
                course = 3;
            else if (student.age == 21)
                course = 4;
            else if (student.age == 22)
                course = 5;
            return course;
        }

        public static implicit operator bool(Student student)
        {
            if (student.gpa < 6)
                return true;
            return false;
        }

        public static Student operator %(Student student, string newName)
        {
            Student other = new Student(student);
            other.name = newName;
            return other;
        }

        public static Student operator -(Student student, double d)
        {
            double newGpa = student.gpa - d;
            if (newGpa < 0)
                newGpa = 0;

            Student newStudent = new Student(student);
            newStudent.gpa = newGpa;
            return newStudent;
        }

        public static bool operator >(Student a, Student b)
        {
            return a.Age > b.Age;
        }

        public static bool operator <(Student a, Student b)
        {
            return a.Age < b.Age;
        }

        public static bool operator >(Student a, double gpa)
        {
            return a.Gpa > gpa;
        }

        public static bool operator <(Student a, double gpa)
        {
            return a.Gpa < gpa;
        }

        public override string ToString()
        {
            return $"Имя: {Name}, Возраст: {Age}, Оценка: {Gpa}";
        }
    }
}
