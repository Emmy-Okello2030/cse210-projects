using System;

class Program
{
    static void Main(string[] args)
    {
        // Create addresses
        Address usaAddress = new Address("123 Main St", "Rexburg", "ID", "USA");
        Address internationalAddress = new Address("456 High St", "London", "England", "UK");

        // Create customers
        Customer usaCustomer = new Customer("John Smith", usaAddress);
        Customer internationalCustomer = new Customer("Emma Johnson", internationalAddress);

        // Create products
        Product product1 = new Product("Laptop", "P100", 999.99, 1);
        Product product2 = new Product("Mouse", "P101", 19.99, 2);
        Product product3 = new Product("Keyboard", "P102", 49.99, 1);
        Product product4 = new Product("Monitor", "P103", 199.99, 2);

        // Create orders
        Order order1 = new Order(usaCustomer);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        Order order2 = new Order(internationalCustomer);
        order2.AddProduct(product3);
        order2.AddProduct(product4);

        // Display order information
        DisplayOrderDetails(order1);
        DisplayOrderDetails(order2);
    }

    static void DisplayOrderDetails(Order order)
    {
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order.GetTotalCost():F2}");
        Console.WriteLine("----------------------------------\n");
    }
}