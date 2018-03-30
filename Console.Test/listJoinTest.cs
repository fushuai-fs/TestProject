using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest
{
    public class listJoinTest
    {
        /*宠物主人*/
        class Person
        {
            public string Name { get; set; }
        }
        /*宠物*/
        class Pet
        {
            public string Name { get; set; }
            public Person Owner { get; set; }
        }
        static void Main1111111111(string[] args)
        {
            /*宠物主人*/
            Person magnus = new Person { Name = "Hedlund, Magnus" };
            Person terry = new Person { Name = "Adams, Terry" };
            Person charlotte = new Person { Name = "Weiss, Charlotte" };

            /*宠物*/
            Pet barley = new Pet { Name = "Barley", Owner = terry };
            Pet boots = new Pet { Name = "Boots", Owner = terry };
            Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
            Pet daisy = new Pet { Name = "Daisy", Owner = magnus };

            /*宠物主人列表集合*/
            List<Person> people = new List<Person> { magnus, terry, charlotte };
            /*宠物列表集合*/
            List<Pet> pets = new List<Pet> { barley, boots, whiskers, daisy };

            /*          
             * Create a list of Person-Pet pairs where 
             * each element is an anonymous type that contains a
             * Pet's name and the name of the Person that owns the Pet.
             * 创建一个包含 "主人-宠物" 这样对应对象的列表
             * ，其中每个对象元素都是包含宠物名字和宠物主人名字的匿名类型
            */
            var query = people.Join(pets, person => person, pet => pet.Owner
            , (person, pet) => new { OwnerName = person.Name, Pet = pet.Name });

            /*循环输出最终结果 格式：宠物主人名-宠物名*/
            foreach (var obj in query)
            {
                Console.WriteLine("{0} - {1}", obj.OwnerName, obj.Pet);
            }

            Console.ReadKey();
        }


        public static void Main()
        {
            #region MyRegion
            List<Customer> custList = new List<Customer>();
            Customer customer = new Customer();
            customer.custID = 1; customer.custName = "张三"; customer.custAge = 20;
            custList.Add(customer);
            customer = new Customer();
            customer.custID = 2; customer.custName = "李四"; customer.custAge = 22;
            custList.Add(customer);
            customer = new Customer();
            customer.custID = 3; customer.custName = "王五"; customer.custAge = 21;
            custList.Add(customer);

            List<Car> carList = new List<Car>();
            Car car = new Car();
            car.custID = 1; car.carID = 1; car.carCode = "京12345";
            carList.Add(car);
            car = new Car();
            car.custID = 2; car.carID = 2; car.carCode = "冀12343";
            carList.Add(car);
            car = new Car();
            car.custID = 2; car.carID = 3; car.carCode = "沪89080";
            carList.Add(car);
            car = new Car();
            car.custID = 3; car.carID = 4; car.carCode = "福45678";
            carList.Add(car);
            car = new Car();
            car.custID = 1; car.carID = 5; car.carCode = "京54321";
            carList.Add(car);
            #endregion


            var query = custList.Join(carList, cust => cust.custID, c => c.carID, (cust, c) => new
              {

                  custID = cust.custID,
                  custName = cust.custName,
                  custAge = cust.custAge,
                  carID = c.carID,
                  carCode = c.carCode
              });
            foreach (var obj in query)
            {
                Console.WriteLine("{0} - {1} - {2} - {3} - {4} ", obj.custID, obj.custName, obj.custAge, obj.carID, obj.carCode);
            }

            Console.WriteLine();
            var query1 = carList.Join(custList, cust => cust.custID, c => c.custID, (c, cust) => new
            {

                custID = cust.custID,
                custName = cust.custName,
                custAge = cust.custAge,
                carID = c.carID,
                carCode = c.carCode
            });
            foreach (var obj in query1)
            {
                Console.WriteLine("{0} - {1} - {2} - {3} - {4} ", obj.custID, obj.custName, obj.custAge, obj.carID, obj.carCode);
            }
            
            Console.ReadKey();

        }

    }

    public class Customer
    {
        public int custID { get; set; }
        public string custName { get; set; }
        public int custAge { get; set; }
    }
    public class Car
    {
        public int custID { get; set; }

        public int carID { get; set; }
        public string carCode { get; set; }
    }


}
