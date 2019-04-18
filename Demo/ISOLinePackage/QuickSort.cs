using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    class QuickSort
    {
        public int Division(List<Tin_Point> list, int left, int right)
        {
            while (left < right)
            {
                double num = list[left].X;
                Tin_Point tnum = list[left];
                if (num > list[left + 1].X)
                {
                    list[left] = list[left + 1];
                    list[left + 1] = tnum;
                    left++;
                }
                else
                {
                    double temp = list[right].X;
                    Tin_Point ttemp = list[right];
                    list[right] = list[left + 1];
                    list[left + 1] = ttemp;
                    right--;
                }
                //Console.WriteLine(string.Join(",", list));
            }
            //Console.WriteLine("---------------\n");
            return left;
        }
        public void Mysort(List<Tin_Point> list, int left, int right)
        {
            if (left < right)
            {
                int i = Division(list, left, right);
                Mysort(list, i + 1, right);
                Mysort(list, left, i - 1);
            }
        }
    }
}
