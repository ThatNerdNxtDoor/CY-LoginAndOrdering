using System.Dynamic;

namespace OrderNamespace
{
    public class Order
    {
        public string Item {get; set;}
        public double Price {get; set;}

        public Order(string name, double price)
        {
            Item = name;
            Price = price;
        }
    }
}