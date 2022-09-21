using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPConsoleApp
{
    internal class Program
    {
        public static List<Order> OrdersList = new List<Order>() {
            new Order(1000.0,DateTime.Now.AddMonths(36),250.0,"Electronics") ,
            new Order(100.0,DateTime.Now.AddMonths(1),550.0,"Grocery") ,
            new Order(2000.0,DateTime.Now.AddMonths(60),150.0,"Mobiles") ,
            new Order(700.0,DateTime.Now.AddMonths(600),1250.0,"Home") ,
            new Order(400.0,DateTime.Now.AddMonths(24),300.0,"Fashion")
        };
        public static void Main(string[] args)
        {
            Program p = new Program();
            List<Order> ordersAfterDiscount = OrdersList.Select(r => p.ClaculateOrderDiscount(r, p.GetDiscountRules())).ToList();
            ordersAfterDiscount.ForEach(r => r.PrintOrderDetails());
            Console.ReadKey();
        }

        public Order ClaculateOrderDiscount(Order r, List<(Func<Order, bool> isQualified, Func<Order, double> DicountValue)> Rules)
        {
            List<double> qulaifiedDiscounts = Rules.Where(x => x.isQualified(r)).Select(x => x.DicountValue(r)).ToList();
            r.discount = qulaifiedDiscounts.Count > 0 ? qulaifiedDiscounts.OrderBy(d => d).Take(3).Average() : 0.0;
            return r;
        }


        public  List<(Func<Order, bool> isQualified, Func<Order, double> DicountValue)> GetDiscountRules()
        {
            return new List<(Func<Order, bool> isQualified, Func<Order, double> DicountValue)>()
            {
                (isPriceQualified,CalculatePriceDiscount),
                (isExpDateQualified,CalculateExpDateDiscount),
                (isPackageWeightQualified,CalculatePackageWeightDiscount),
                (isProductCategoryQualified,CalculateProductCategoryDiscount)
            };
        }


        public bool isPriceQualified(Order r)
        {
            return r.price > 1000;
        }
        public double CalculatePriceDiscount(Order r)
        {
            return r.price * 0.10;
        }

        public bool isExpDateQualified(Order r)
        {
            return r.ExpDate < DateTime.Now.AddMonths(2);
        }

        public double CalculateExpDateDiscount(Order r)
        {
            return r.price * 0.25;
        }

        public bool isPackageWeightQualified(Order r)
        {
            return r.PackageWeight < 200.0;
        }

        public double CalculatePackageWeightDiscount(Order r)
        {
            return r.price * 0.02;
        }

        public bool isProductCategoryQualified(Order r)
        {
            return r.ProductCategory == "Fashion";
        }

        public double CalculateProductCategoryDiscount(Order r)
        {
            return r.price * 0.20;
        }

    }
}
