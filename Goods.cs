using System.Collections.Generic;

namespace CourseTask
{
    public class Goods
    {
        string label;
        double price;
        public void AddProduct(string line, List<Goods> g)
        {
            Goods temp = new Goods();
            string[] words = line.Split();
            temp.label = words[0];
            temp.price = double.Parse(words[1]);
            g.Add(temp);
        }
        public string GetLabel
        {
            get
            {
               return label;
            }
        }
        public double GetPrice
        {
            get
            {
                return price;
            }
        }
    }
}
