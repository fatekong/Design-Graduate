using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.MyKDTree
{
    class KD_Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public KD_Point(double x,double y,double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public KD_Point(double x,double y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0;
        }

    }
}
