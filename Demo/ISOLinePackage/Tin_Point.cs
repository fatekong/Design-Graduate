using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    class Tin_Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Value { get; set; }
        public int Num { get; set; }
        public int Type { get; set; }
        public Tin_Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
            this.Value = 0;
            this.Num = 0;
        }
        public Tin_Point(double x, double y, double v)
        {
            this.X = x;
            this.Y = y;
            this.Value = v;
        }
        public Tin_Point(double x, double y, double v, int n)
        {
            this.X = x;
            this.Y = y;
            this.Value = v;
            this.Num = n;
        }
        public Tin_Point()
        {

        }
        public override bool Equals(object obj)
        {
            if (obj is Tin_Point)
            {
                var tmp = obj as Tin_Point;
                if (this.X == tmp.X && this.Y == tmp.Y)
                    return true;
            }
            return false;
        }
        public override string ToString()
        {
            string s = "x:" + this.X + ",y:" + this.Y + ",value:" + this.Value + ",num:" + this.Num + ",type:" + this.Type;
            return s;
        }
    }
}
