using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.MyKDTree
{
    class KDTreeNodeCollection
    {
        public double max = 0;
        private int location = 0;
        KD_Point thepoint;
        List<KD_Point> collection;
        public List<KD_Point> GetPoints()
        {
            return this.collection;
        }
        public int k = 0;//k临近值
        //
        public KDTreeNodeCollection(int num, KD_Point point)
        {
            this.k = num;
            this.thepoint = point;
            collection = new List<KD_Point>();
        }
        public void Add(KDTreeNode currentnode, double d)
        {
            KD_Point current = currentnode.DivisionPoint;
            if (collection.Count == 0)
            {
                collection.Add(current);
                this.max = d;
            }
            else if (collection.Count < this.k)
            {
                if (max < d)
                {
                    this.max = d;
                    location = collection.Count;
                    collection.Add(current);

                }
                else
                {
                    collection.Add(current);
                }
            }
            else
            {
                if (d <= max)
                {
                    collection.RemoveAt(location);
                    collection.Add(current);
                    for (int i = 0; i < k; i++)
                    {
                        if (i == 0)
                        {
                            max = Math.Sqrt(Math.Pow(thepoint.X - collection[i].X, 2) + Math.Pow(thepoint.Y - collection[i].Y, 2));
                            location = i;
                        }
                        else
                        {
                            double distance = Math.Sqrt(Math.Pow(thepoint.X - collection[i].X, 2) + Math.Pow(thepoint.Y - collection[i].Y, 2));
                            if (distance > max)
                            {
                                max = distance;
                                location = i;
                            }
                        }
                    }
                }
            }
        }
    }
}
