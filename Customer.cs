using System;
using System.IO;

namespace CourseTask
{
    public class Customer
    {
        string sirname;
        string name;
        string middle_name;
        DateTime birth_date;
        DateTime order_time;
        public Customer()
        { }

        public void AddCustomer(StreamWriter sw)
        {
            Console.WriteLine("Please input your Sirname, Name, Middle name, Birth date (dd/MM/YY), each starting from a new line:");
            string s, n, m, b;
            try
            {
                s = Console.ReadLine();
                n = Console.ReadLine();
                m = Console.ReadLine();
                b = Console.ReadLine();

                this.sirname = s;
                this.name = n;
                this.middle_name = m;
                this.birth_date = DateTime.Parse(b);

                sw.Write(s);
                sw.Write(" ");
                sw.Write(n);
                sw.Write(" ");
                sw.Write(m);
                sw.Write(" ");
                sw.Write(b);

            }
            catch(FormatException)
            {
                Console.WriteLine("Wrong format!");
            }
        }
        public DateTime GetBirthday
        {
            get
            {
                return birth_date;
            }
        }
        public DateTime GetOrderTime
        {
            get
            {
                return this.order_time;
            }
            set
            {
                this.order_time = value;
            }
        }
    }
}
