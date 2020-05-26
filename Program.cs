using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CourseTask
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Goods> goods = new List<Goods>(); //list for converting goods from file into objects of class Goods
            List<Goods> orderList = new List<Goods>(); //list that contains customers order
            Goods g = new Goods(); //object for using methods of class Goods
            Customer customer = new Customer(); //customer info

            Dictionary<int, Goods> goodsList = new Dictionary<int, Goods>(); //dictionary of goods to choose from when making order
            FileInfo goodsFile = new FileInfo("goods.txt");
            FileInfo customerFile = new FileInfo("customer.txt");
            string line;
            int i = 0;
            using (StreamReader sr = new StreamReader(goodsFile.FullName, System.Text.Encoding.Default))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    g.AddProduct(line, goods);
                    goodsList.Add(i + 1, goods.ElementAt(i));
                    i++;
                }
            }
            using (StreamWriter sw = new StreamWriter(customerFile.FullName, false, System.Text.Encoding.Default))
            {
                Console.WriteLine("Welcome to our store!");
                customer.AddCustomer(sw);
                Store st = new Store();
                st.MakingOrder(sw, goods, goodsList, orderList, customer);
            }

            Console.ReadKey();
        }
    }
}
