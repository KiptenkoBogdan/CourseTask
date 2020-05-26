using System.Collections.Generic;

namespace CourseTask
{
    public class SortByPrice : IComparer<Goods>
    {
        public int Compare(Goods g1, Goods g2)
        {
            return g1.GetPrice.CompareTo(g2.GetPrice);
        }
    }
}
