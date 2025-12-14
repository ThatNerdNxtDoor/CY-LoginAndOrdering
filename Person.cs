using OrderNamespace;

namespace PeopleNamespace
{
    class Person
    {
        public string password;
        public string Name {get; set;}
        public int Age {get; set;}

        public Person(string name, int age, string password)
        {
            Name = name;
            Age = age;
            this.password = password;
        }
    }

    class Customer : Person
    {
        public List<Order> orders;
        public Customer(string name, int age, string password) : base(name, age, password)
        {
            orders = new List<Order>();
        }

        public void DisplayOrders()
        {
            if (orders.Count == 0)
            {
                Console.WriteLine("-No Orders Have Been Made-");
            } else {
                foreach (Order o in orders)
                {
                    Console.WriteLine($"* {o.Item} for [{o.Price}]");
                }
            }
        }

        public void AddOrder(string item, double price)
        {
            orders.Add(new Order(item, price));
        }
    }

    class Employee : Person
    {
        public Employee(string name, int age, string password) : base(name, age, password)
        {
            
        }
    }
}