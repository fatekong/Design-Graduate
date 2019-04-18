using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    class Triangle
    {
        public Tin_Point p1;
        public Tin_Point p2;
        public Tin_Point p3;
        public Circle outcircle;
        public bool flag = false;
        public Triangle(Tin_Point mp1, Tin_Point mp2, Tin_Point mp3)
        {
            this.p1 = mp1;
            this.p2 = mp2;
            this.p3 = mp3;
        }
        public Triangle(Tin_Point mp1, Tin_Point mp2, Tin_Point mp3, Circle circle)
        {
            this.p1 = mp1;
            this.p2 = mp2;
            this.p3 = mp3;
            this.outcircle = circle;
        }
        public override bool Equals(object obj)
        {
            if (obj is Triangle)
            {
                var tmp = obj as Triangle;
                if (tmp.p1.Equals(this.p1) || tmp.p1.Equals(this.p2) || tmp.p1.Equals(this.p3))
                {
                    if (tmp.p2.Equals(this.p1) || tmp.p2.Equals(this.p2) || tmp.p2.Equals(this.p3))
                    {
                        if (tmp.p3.Equals(this.p1) || tmp.p3.Equals(this.p2) || tmp.p3.Equals(this.p1))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public override string ToString()
        {
            string s = "";
            s += "p1: (" + this.p1.X.ToString() + "," + this.p1.Y.ToString() + ") :num&value: " + this.p1.Num.ToString() + " & " + this.p1.Value.ToString() + "\n";
            s += "p2: (" + this.p2.X.ToString() + "," + this.p2.Y.ToString() + ") :num&value: " + this.p2.Num.ToString() + " & " + this.p2.Value.ToString() + "\n";
            s += "p3: (" + this.p3.X.ToString() + "," + this.p3.Y.ToString() + ") :num&value: " + this.p3.Num.ToString() + " & " + this.p3.Value.ToString() + "\n";
            return s;
        }
    }
}
