namespace ShoppingSpree
{
    public class Product
    {
        public Product(decimal cost, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Name cannot be empty");
            if (cost < 0) throw new ArgumentException("Money cannot be negative");

            this.Cost = cost;
            this.Name = name;
        }

        public decimal Cost { get; }
        public string Name { get; }
    }
}