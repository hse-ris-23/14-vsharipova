using CarLibrary;
using MyCollectionLibrary;
namespace Tests
{
    [TestClass]
    public class HTableTests
    {
        [TestMethod]
        public void ArrayWithCapacity10()
        {
            HTable<Car> table = new HTable<Car>();

            Assert.AreEqual(10, table.points.Length);
        }

        [TestMethod]
        public void ArrayWithGivenCapacity()
        {
            HTable<Car> table = new HTable<Car>(20);

            Assert.AreEqual(20, table.points.Length);
        }

        [TestMethod]
        public void Count()
        {
            Car car1 = new LightCar();
            Car car2 = new LightCar();
            HTable<Car> table = new HTable<Car>();
            table.Add(car1);
            table.Add(car2);

            int count = table.Count;

            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void IsReadOnly()
        {
            HTable<Car> table = new HTable<Car>();

            bool isReadOnly = table.IsReadOnly;

            Assert.IsFalse(isReadOnly);
        }

        [TestMethod]
        public void AddsItemsToTable()
        {
            Car car1 = new LightCar();
            Car car2 = new LightCar();
            HTable<Car> table = new HTable<Car>(5);
            table.Add(car1);
            table.Add(car2);
            List<Car> items = new List<Car> { car1, car2};

            table.AddRange(items);

            Assert.IsTrue(table.Contains(car1));
        }

        [TestMethod]
        public void RemovesItemFromTable()
        {
            Car car1 = new LightCar();
            Car car2 = new LightCar();
            HTable<Car> table = new HTable<Car>(2);
            table.Add(car1);
            table.Add(car2);

            bool removed1 = table.Remove(car1);
            bool removed2 = table.Remove(car2);

            Assert.IsTrue(removed1);
            Assert.AreEqual(0, table.Count);
        }

        [TestMethod]
        public void CopyTo()
        {
            HTable<Car> table = new HTable<Car>(5);
            Car car1 = new LightCar();
            table.Add(car1);
            table.Add(new HeavyCar());
            table.Remove(new HeavyCar()); 
            Car[] targetArray = new Car[2];

            table.CopyTo(targetArray, 0);

            Assert.IsNull(targetArray[1]);
        }


        [TestMethod]
        public void ShallowClone()
        {
            HTable<Car> original = new HTable<Car>(5);
            original.Add(new LightCar());
            original.Add(new MiddleCar());

            HTable<Car> clone = original.ShallowClone();

            Assert.AreNotSame(original, clone);
            Assert.AreNotSame(original.points, clone.points);
        }
    }
}