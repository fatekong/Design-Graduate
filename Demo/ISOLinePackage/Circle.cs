using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    class Circle
    {
        public double X;
        public double Y;
        public double R_pow;
        public Circle()
        {
            this.X = this.Y = this.R_pow = 0;
        }
        public Circle(double mx, double my, double mr_pow)
        {
            this.X = mx;
            this.Y = my;
            this.R_pow = mr_pow;
        }
    }
}
