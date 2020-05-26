using System;
using System.IO;
using System.Collections.Generic;

namespace CourseTask
{
    class Store
    {
        public void StoreInfo()
        {
            Console.WriteLine("\n\tKoshik");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Contact info:\t+380567891011");
            Console.WriteLine("\t\t+380997885335");
            Console.WriteLine("Schedule:\tMonday-Friday - 09:00-18:00\n\t\tSaturday - 09:00-15:00\n\t\tSunday - day off\n");
        }
        public void TimeOfOrder(StreamWriter sw, Customer c) // checks if the time of order matches with store schedule
        {
            c.GetOrderTime = DateTime.Now;
            sw.Write(" Time of order: " + c.GetOrderTime);
            Console.WriteLine("Time of order confirmation: {0}", c.GetOrderTime);
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                Console.WriteLine("Sorry! Today is a day off. Your order will be processed tomorrow");
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday && DateTime.Now.Hour >= 9 && DateTime.Now.Hour < 15)
                Console.WriteLine("Your order is being processed");
            else if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour < 18)
                Console.WriteLine("Your order is being processed");
            else
                Console.WriteLine("Our store does not work right now. Your order will be processed during the workhour\nYou can check our schedule in Store info");
        }
        public void SortingByPrice(List<Goods> list) // sorts a list of objects by price
        {
            list.Sort(new SortByPrice());
        }
        public void SearchProduct(List<Goods> list, string name) // searches product by name
        {
            bool productFound = true;
            foreach (Goods item in list)
            {
                if (item.GetLabel == name)
                {
                    Console.WriteLine("{0} - {1} grn", item.GetLabel, item.GetPrice);
                    productFound = true;
                    break;
                }
                else
                {
                    productFound = false;
                }
            }

            if(productFound == false)
                Console.WriteLine("Product not found!");
        }
        public double Discount(double orderPrice, Customer c) // makes a discount
        {
            double orderWithDiscount = orderPrice;
            if (DateTime.Now.DayOfYear - c.GetBirthday.DayOfYear <= 0 && (DateTime.Now.DayOfYear - c.GetBirthday.DayOfYear) >= -3)
                orderWithDiscount -= (orderPrice / 100) * 10;

            if (orderPrice >= 200 && orderPrice < 500)
                return orderWithDiscount - (orderPrice / 100) * 5;
            else if (orderPrice >= 500)
                return orderWithDiscount - (orderPrice / 100) * 7;
            else
                return orderWithDiscount;
        }
        public void GetList(List<Goods> list)
        {
            int i = 1;
            foreach (Goods item in list)
            {
                Console.WriteLine("{0}.{1} - {2} grn", i, item.GetLabel, item.GetPrice);
                i++;
            }
        }
        public void MakingOrder(StreamWriter sw, List<Goods> goods, Dictionary<int, Goods> goodsList, List<Goods> orderList, Customer customer)
        {
            bool flag = true;
            int totalGoods = 0; //total number of goods in order
            double totalPrice = 0; //total price of the order
            
            if (DateTime.Now.DayOfYear - customer.GetBirthday.DayOfYear <= 0 && (DateTime.Now.DayOfYear - customer.GetBirthday.DayOfYear) >= -3)
                Console.WriteLine("For your birthday we give you 10% discount!\n");

            Console.WriteLine("\tList of products");
            Console.WriteLine("--------------------------------");
            GetList(goods);
            Console.WriteLine("");

            while (flag)
            {
                Console.WriteLine("1. Add product to the list");
                Console.WriteLine("2. Remove product from the list");
                Console.WriteLine("3. Order info");
                Console.WriteLine("4. Confirm order");
                Console.WriteLine("5. Store info");
                Console.WriteLine("6. Sort your order list by price");
                Console.WriteLine("7. Search product by name");
                char c = char.Parse(Console.ReadLine());
                switch (c)
                {
                    case '1':
                        try
                        {
                            Console.WriteLine("Select product to add to your list:");
                            int index = int.Parse(Console.ReadLine());
                            orderList.Add(goodsList[index]);
                            Console.WriteLine("{0} was added to your list.\n", goodsList[index].GetLabel);
                        }
                        catch(KeyNotFoundException)
                        {
                            Console.WriteLine("\nWrong index! No product under this index found!\n");
                        }
                        break;

                    case '2':
                        try
                        {
                            Console.WriteLine("Select product to remove from your list:");
                            Console.WriteLine("\n Your list");
                            GetList(orderList);

                            int index = int.Parse(Console.ReadLine());
                            orderList.RemoveAt(index - 1);
                            Console.WriteLine("Product was removed from your list.\n");
                            GetList(orderList);
                            Console.WriteLine("");
                        }
                        catch(KeyNotFoundException)
                        {
                            Console.WriteLine("\nWrong index! No product under this index found!\n");
                        }
                        break;

                    case '3':
                        foreach (Goods item in orderList)
                        {
                            totalPrice += item.GetPrice;
                            totalGoods++;
                        }
                        Console.WriteLine("Ordered {0} goods at total price of {1} grn.\nPrice with discount: {2} grn", totalGoods, totalPrice, Discount(totalPrice, customer));
                        Console.WriteLine("\n Your list");
                        GetList(orderList);
                        Console.WriteLine("");
                        totalGoods = 0;
                        break;

                    case '4':
                        flag = false;
                        break;

                    case '5':
                        StoreInfo();
                        break;

                    case '6':
                        SortingByPrice(orderList);
                        Console.WriteLine("\n Your list");
                        GetList(orderList);
                        Console.WriteLine("");
                        break;

                    case '7':
                        Console.WriteLine("Type the name of the product you want to find (first letter uppercase): ");
                        string name = Console.ReadLine();
                        SearchProduct(goods, name);
                        Console.WriteLine("");
                        break;

                    default:
                        Console.WriteLine("Entered wrong command!");
                        break;
                }
            }

            totalPrice = 0;
            foreach (Goods item in orderList)
            {
                totalPrice += item.GetPrice;
                totalGoods++;
            }
            totalPrice = Discount(totalPrice, customer);
            Console.WriteLine("\nThank you for making an order!");
            Console.WriteLine("Ordered {0} product(s) at total price of {1} grn.\nPrice with discount: {2}", totalGoods, totalPrice, Discount(totalPrice, customer));
            TimeOfOrder(sw, customer);
        }
    }
}
