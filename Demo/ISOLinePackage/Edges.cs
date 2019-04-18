using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    class Edges
    {
        public Tin_Point p1;
        public Tin_Point p2;
        //public Triangle t1 = null;
        //public Triangle t2 = null;
        public int SignT1 = -1;
        public int SignT2 = -1;
        public Edges(Tin_Point mp1, Tin_Point mp2)
        {
            this.p1 = mp1;
            this.p2 = mp2;
        }
        public void SetTriangleSign(int sign)
        {
            if (this.SignT1 == -1)
                this.SignT1 = sign;
            else
                this.SignT2 = sign;
        }
        /*public void SetTriangle(Triangle t)
        {
            if (this.t1 == null)  
                this.t1 = t;
            else
                this.t2 = t;
        }*/
        public override bool Equals(Object e)
        {
            if (e is Edges)
            {
                var tmp = e as Edges;
                if ((this.p1.Equals(tmp.p1) && this.p2.Equals(tmp.p2)) || (this.p1.Equals(tmp.p2) && this.p2.Equals(tmp.p1)))
                    return true;
            }
            return false;
        }
        public override string ToString()
        {
            String s1 = p1.ToString();
            String s2 = p2.ToString();
            return s1 + "\n" + s2;
        }
    }
}
