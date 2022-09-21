using System;

namespace FPConsoleApp
{
    public class Order
    {
        public double price { get; }
        public double discount { get; set; }
        public DateTime ExpDate { get; }
        public double PackageWeight { get; }
        public string ProductCategory { get; }

        public Order(double price, DateTime ExpDate, double PackageWeight, string ProductCategory)
        {
            this.price = price;
            this.ExpDate = ExpDate;
            this.PackageWeight = PackageWeight;
            this.ProductCategory = ProductCategory;
            this.discount = 0.0;
        }

        public void PrintOrderDetails()
        {
            Console.WriteLine($"Order Price: {price} \t ExpDate: {ExpDate} \t " +
                $"PackageWeight: {PackageWeight} Category: {ProductCategory} Discount: {discount}");
        }
    }
}
