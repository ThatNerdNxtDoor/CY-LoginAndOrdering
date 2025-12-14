using PeopleNamespace;
using OrderNamespace;
using System.Linq;

namespace InheritencePrac {
    class Program {
        static void Main()  {
            List<Person> users = new List<Person>();
            Customer i = new Customer("Isaiah", 23, "IHt0202");
            i.orders.Add(new Order("Christmas Lights", 20.99));
            i.orders.Add(new Order("Pack of Trading Cards", 5.99));

            users.Add(i);
            users.Add(new Customer("Ricky", 20, "123oOopy"));
            users.Add(new Employee("Joseph", 42, "newYorker85"));

            bool runtime = true;
            do
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine($"--Order Placement System--");
                Console.WriteLine("1 - Login.");
                Console.WriteLine("2 - Create an Account.");
                Console.WriteLine("STOP - End Program.");
                Console.WriteLine("Select Action:");
                string input = Console.ReadLine();
                switch(input)
                {
                    case "1": //Login
                        Console.WriteLine("Enter a password:");
                        input = Console.ReadLine();
                        Person foundUser = users.Find(x => x.password.Contains(input));
                        if (foundUser != null)
                        {
                            Console.WriteLine("Login Successful!");
                            if (foundUser is Customer) //Customer Login
                            {
                                CustomerMenu((Customer)foundUser);
                            } else //Employee Login
                            {
                                List<Customer> customers = new List<Customer>();
                                foreach(Person user in users)
                                {
                                    if (user is Customer)
                                    {
                                        customers.Add((Customer)user);
                                    }
                                }
                                EmployeeMenu((Employee)foundUser, customers);
                            }
                        } else
                        {
                            Console.WriteLine("Invalid password, or that account does not exist. Try Again.");
                        }
                        break;
                    case "2": //Create account
                        Console.WriteLine("Enter a name for your account.");
                        input = Console.ReadLine();
                        string name = input;
                        bool success = false;
                        int age = 0;
                        do {
                            Console.WriteLine("Enter your age.");
                            input = Console.ReadLine();
                            try {
                                age = int.Parse(input);
                                if (age < 18)
                                {
                                    Console.WriteLine("You must be 18 years or older to create an account.");
                                } else
                                {
                                    success = true;
                                }
                            } catch
                            {
                                Console.WriteLine("That input is invalid, please try again.");
                            }
                        } while(!success);
                        Console.WriteLine("Give a password.");
                        success = false;
                        string password = "";
                        do {
                            Console.WriteLine("Passwords must be 7-12 characters, and have at least one of each: lowercase letter, uppercase letter, and number.");
                            password = Console.ReadLine();
                            if ((password.Length > 7 && password.Length < 12) && password.Any(Char.IsAsciiLetterLower) && password.Any(Char.IsAsciiLetterUpper) && password.Any(Char.IsDigit))
                            {
                                success = true;
                            } else {
                                Console.WriteLine("That input is invalid, please try again.");
                            }
                        } while(!success);
                        users.Add(new Customer(name, age, password));
                        break;
                    case "STOP": //End Program
                        runtime = false;
                        break;
                    default:
                        Console.WriteLine("That input is invalid, please try again.");
                        break;
                }
            } while (runtime);
        }

        public static void DisplayCustomers(List<Customer> customers)
        {
            foreach(Customer c in customers)
            {
                Console.WriteLine($"* {c.Name}");
            }
        }

        public static void CustomerMenu(Customer user)
        {
            bool login = true;
            do
            {
                Console.WriteLine("-------------------------------------");
                user.DisplayOrders();
                Console.WriteLine(" ");
                Console.WriteLine($"--Welcome, {user.Name}!--");
                Console.WriteLine("1 - Place an Order.");
                Console.WriteLine("2 - Logout.");
                Console.WriteLine("Select Action:");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": //Place New Order
                        Console.WriteLine("What is the object being ordered?");
                        input = Console.ReadLine();
                        string item = input;
                        bool success = false;
                        double price = 0.0;
                        do {
                            Console.WriteLine("What is the price of the order?");
                            input = Console.ReadLine();
                            try {
                                price = double.Parse(input);
                                if (price < 0.0)
                                {
                                    Console.WriteLine("The price cannot be negative.");
                                } else
                                {
                                    success = true;
                                }
                            } catch
                            {
                                Console.WriteLine("That input is invalid, please try again.");
                            }
                        } while(!success);
                        user.AddOrder(item, price);
                        break;
                    case "2": //Logout
                        login = false;
                        break;
                }
            } while(login);
        }

        public static void EmployeeMenu(Employee user, List<Customer> customers)
        {
            bool login = true;
            do
            {
                Console.WriteLine("------------------Current Customers------------------");
                DisplayCustomers(customers);
                Console.WriteLine($"--Welcome, {user.Name}!--");
                Console.WriteLine("1 - Examine a Customer's Orders");
                Console.WriteLine("2 - Remove a Customer's Order.");
                Console.WriteLine("3 - Logout.");
                Console.WriteLine("Select Action:");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": //Examine Customer's Oders
                        Console.WriteLine("Enter the name of the customer.");
                        input = Console.ReadLine();
                        Customer customer = customers.Find(x => x.Name.Contains(input));
                        if (customer != null)
                        {
                            customer.DisplayOrders();
                        } else
                        {
                            Console.WriteLine("User does not exist.");
                        }
                        break;
                    case "2": //Remove Order
                        Console.WriteLine("Enter the name of the customer.");
                        input = Console.ReadLine();
                        customer = customers.Find(x => x.Name.Contains(input));
                        if (customer != null)
                        {
                            customer.DisplayOrders();
                        } else
                        {
                            Console.WriteLine("User does not exist.");
                            break;
                        }
                        Console.WriteLine("Enter the item in the order.");
                        input = Console.ReadLine();
                        Order order = customer.orders.Find(x => x.Item.Contains(input));
                        if (customer != null)
                        {
                            customer.orders.Remove(order);
                        } else
                        {
                            Console.WriteLine("That order does not exist.");
                        }
                        break;
                    case "3": //Logout
                        login = false;
                        break;
                }
            } while(login);
        }
    }
}