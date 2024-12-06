using ls6_practice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace ls6_practice
{
    public class Program
    {
        static List<Order> GetOrders()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + @"\Configs")
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new ShopContext(optionsBuilder.Options, config))
            {
                return context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderProducts)
                    .ThenInclude(od => od.Product)
                    .ToList();
            }
        }

        static List<Customer> GetCustomers()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + @"\Configs")
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new ShopContext(optionsBuilder.Options, config))
            {
                return context.Customers
                    .Include(c => c.Orders)
                    .ToList();
            }
        }
        static List<Product> GetProducts()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + @"\Configs")
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new ShopContext(optionsBuilder.Options, config))
            {
                return context.Products.ToList();
            }
        }
        static void Main(string[] args)
        {
            //•	Найдите всех клиентов, у которых в адресе электронной почты содержится домен "example.com".
            //var customerDomen = GetCustomers().Where(c => c.Email.Contains("example.com"));
            //foreach (var c in customerDomen)
            //{
            //    Console.WriteLine($"Id: {c.Id}, Name: {c.Name}, Email: {c.Email}");
            //}

            //•	Получите все продукты, цена которых меньше средней цены всех продуктов.
            //var avgPrice = GetProducts().Average(p => p.Price);
            //Console.WriteLine($"Avg price: {avgPrice}");
            //foreach (var product in GetProducts())
            //{
            //    if (product.Price < avgPrice)
            //    {
            //        Console.WriteLine($"Id: {product.Id}, Product name: {product.Name}, Price: {product.Price}");
            //    }
            //}

            //•	Найдите клиента с самым длинным именем.
            //var longestName = GetCustomers().OrderByDescending(c => c.Name.Length).FirstOrDefault();
            //Console.WriteLine($"Customer with the longest name: {longestName.Name}");

            //•	Получите все заказы, сделанные клиентом с самым коротким именем.
            //var customers = GetCustomers();
            //var shortestName = customers.OrderBy(c => c.Name.Length).FirstOrDefault();
            //if (shortestName != null)
            //{
            //    Console.WriteLine($"{shortestName.Name} orders:\n");
            //    var orders = GetOrders().Where(o => o.Customer.Name == shortestName.Name).ToList();
            //    if (orders.Any())
            //    {
            //        foreach (var order in orders)
            //        {
            //            Console.WriteLine($"\tOrder ID: {order.Id}, Order Date: {order.CreatedDate}");
            //            foreach (var product in order.OrderProducts)
            //            {
            //                Console.WriteLine($"\t\tProduct: {product.Product.Name}, Price: {product.Product.Price}, Quantity: {product.Quantity}");
            //            }
            //        }
            //    }
            //}

            //•	Найдите продукт с наименьшей ценой.
            //var cheapestPrice = GetProducts().OrderBy(p => p.Price).First();
            //Console.WriteLine($"Cheapest product name: {cheapestPrice.Name}, Price: {cheapestPrice.Price}");

            //•	Получите всех клиентов, у которых количество заказов превышает среднее количество заказов всех клиентов.
            //var customers = GetCustomers();
            //var avgOrdersCount = customers.Average(c => c.Orders.Count());

            //var result = customers.Where(c => c.Orders.Count() > avgOrdersCount).ToList();
            //if (result.Count == 0)
            //{
            //    Console.WriteLine("No customer with orders count bigger then avg");
            //}
            //else
            //{
            //    foreach (var customer in result)
            //    {
            //        Console.WriteLine(customer.Name);
            //    }
            //}

            //•	Найдите клиента, который сделал заказ с самым дорогим продуктом.
            //var orders = GetOrders();

            //var mostExpensiveProduct = orders
            //    .SelectMany(o => o.OrderProducts)
            //    .OrderByDescending(od => od.Product.Price)
            //    .FirstOrDefault();

            //if (mostExpensiveProduct != null)
            //{
            //    var customer = mostExpensiveProduct.Order.Customer;
            //    Console.WriteLine($"Customer with the most expensive product: {customer.Name}");
            //    Console.WriteLine($"Product: {mostExpensiveProduct.Product.Name}, Price: {mostExpensiveProduct.Product.Price}");
            //}

            //•	Получите среднюю цену заказа.
            //var orders = GetOrders();
            //var avgOrderPrice = orders.Select(o => o.OrderProducts.Sum(op => op.Product.Price * op.Quantity)).Average();
            //Console.WriteLine($"Average price for order is {avgOrderPrice}");

            //•	Найдите клиента, который сделал последний заказ в базе данных.
            //var orders = GetOrders();
            //var lastOrder = orders.OrderByDescending(o => o.CreatedDate).FirstOrDefault();
            //Console.WriteLine($"Last order was made by customer {lastOrder.Customer.Name} at {lastOrder.CreatedDate}");

            //•	Получите все заказы, содержащие продукты с ценой больше 800.
            var orders = GetOrders();
            var result = orders.Where(o => o.OrderProducts.Any(op => op.Product.Price > 800));
            foreach (var order in result)
            {
                Console.WriteLine($"Order id: {order.Id}, Products:\n");
                foreach (var product in order.OrderProducts) Console.WriteLine($"\t-{product.Product.Name} for {product.Product.Price}");
            }
        }
    }
}
