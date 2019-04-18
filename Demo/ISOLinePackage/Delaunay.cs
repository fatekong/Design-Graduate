using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    class Delaunay
    {
        private static double EPSILON = 1.0 / 1048576.0;

        public Delaunay()
        {
        }

        public Triangle SuperTriangle(List<Tin_Point> list)
        {
            double xmax, ymax, xmin, ymin, dx, dy, dmax, xmid, ymid;
            xmax = xmin = ymax = ymin = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    xmax = xmin = list[i].X;
                    ymax = ymin = list[i].Y;
                }
                else
                {
                    if (list[i].X < xmin)
                        xmin = list[i].X;
                    if (list[i].X > xmax)
                        xmax = list[i].X;
                    if (list[i].Y < ymin)
                        ymin = list[i].Y;
                    if (list[i].Y > ymax)
                        ymax = list[i].Y;
                }
            }
            dx = xmax - xmin;
            dy = ymax - ymin;
            dmax = Math.Max(dx, dy);
            xmid = xmin + dx / 2;
            ymid = ymin + dy / 2;
            Triangle super = new Triangle(new Tin_Point(xmid - 5 * dmax, ymid - dmax, 0, -1), new Tin_Point(xmid, ymid + 5 * dmax, 0, -2), new Tin_Point(xmid + 5 * dmax, ymid - dmax, 0, -3));
            Circle mycricle = Out_Circle(super);
            super.outcircle = mycricle;
            return super;
        }
        public Circle Out_Circle(Triangle t)
        {
            double px1 = t.p1.X;
            double py1 = t.p1.Y;
            double px2 = t.p2.X;
            double py2 = t.p2.Y;
            double px3 = t.p3.X;
            double py3 = t.p3.Y;
            double fabsy1y2 = Math.Abs(py1 - py2);
            double fabsy2y3 = Math.Abs(py2 - py3);
            double xc, yc, m1, m2, mx1, mx2, my1, my2, dx, dy;
            if (fabsy1y2 < EPSILON && fabsy2y3 < EPSILON)
                return new Circle();
            //三角形三边的垂直平分线交点为圆心
            if (fabsy1y2 < EPSILON)//p1p2与y平行，只需要p3与p2p3边的垂线的交点，且交点x轴坐标必为p3x坐标
            {
                m2 = -((px3 - px2) / (py3 - py2));
                mx2 = (px2 + px3) / 2.0;
                my2 = (py2 + py3) / 2.0;
                xc = (px2 + px1) / 2.0;
                yc = m2 * (xc - mx2) + my2;
            }
            else if (fabsy2y3 < EPSILON)//与上同理，p2p3平行于y轴
            {
                m1 = -((px2 - px1) / (py3 - py2));
                mx1 = (px1 + px2) / 2.0;
                my1 = (py1 + py2) / 2.0;
                xc = (px3 + px2) / 2.0;
                yc = m1 * (xc - mx1) + my1;
            }
            else
            {
                m1 = -((px2 - px1) / (py2 - py1));
                m2 = -((px3 - px2) / (py3 - py2));
                mx1 = (px1 + px2) / 2.0;
                mx2 = (px2 + px3) / 2.0;
                my1 = (py1 + py2) / 2.0;
                my2 = (py2 + py3) / 2.0;
                xc = (m1 * mx1 - m2 * mx2 + my2 - my1) / (m1 - m2);
                yc = (fabsy1y2 > fabsy2y3) ?
                  m1 * (xc - mx1) + my1 :
                  m2 * (xc - mx2) + my2;
            }
            dx = px2 - xc;
            dy = py2 - yc;
            return new Circle(xc, yc, dx * dx + dy * dy);
        }
        public List<Edges> Update(List<Edges> edges)
        {
            //List<Edges> update = edges.Distinct().ToList();
            //foreach (Edges i in edges)
            //{
            //    Console.WriteLine("(" + i.p1.X + "," + i.p1.Y + ")" + " - " + "(" + i.p2.X + "," + i.p2.Y + ")");
            //}
            bool flag = false;
            for (int i = 0; i < edges.Count; i++)
            {
                for (int j = i + 1; j < edges.Count; j++)
                {
                    if ((edges[i].p1.Equals(edges[j].p1) && edges[i].p2.Equals(edges[j].p2)) || (edges[i].p1.Equals(edges[j].p2) && edges[i].p2.Equals(edges[j].p1)))
                    {
                        //Console.WriteLine("移除: " + j);
                        edges.RemoveRange(j, 1);
                        flag = true;
                        j--;
                    }
                }
                if (flag == true)
                {
                    edges.RemoveRange(i, 1);
                    i--;
                    flag = false;
                }

            }
            foreach (Edges i in edges)
            {
                Console.WriteLine("(" + i.p1.Num + ")" + " - " + "(" + i.p2.Num + ")");
            }
            return edges;//edges去重
        }
        public bool RemoveSuper(Triangle super, Triangle other)
        {
            if (super.p1.Equals(other.p1))
                return true;
            if (super.p1.Equals(other.p2))
                return true;
            if (super.p1.Equals(other.p3))
                return true;
            if (super.p2.Equals(other.p1))
                return true;
            if (super.p2.Equals(other.p2))
                return true;
            if (super.p2.Equals(other.p3))
                return true;
            if (super.p3.Equals(other.p1))
                return true;
            if (super.p3.Equals(other.p2))
                return true;
            if (super.p3.Equals(other.p3))
                return true;
            return false;
        }
        public List<Triangle> ConstructionDelaunay(List<Tin_Point> vertices)
        {
            int num = vertices.Count;
            if (num < 3)
                return null;
            QuickSort qs = new QuickSort();
            qs.Mysort(vertices, 0, num - 1);//vertices已经按照x从小到大排序
            Console.WriteLine("排序完成！");
            Triangle super = SuperTriangle(vertices);
            Console.WriteLine("supertriangle" + super.ToString());
            Console.WriteLine("超级三角形构建完成！");
            List<Triangle> open = new List<Triangle>();
            List<Triangle> closed = new List<Triangle>();
            List<Edges> edges = new List<Edges>();
            open.Add(super);
            Console.WriteLine("超级三角形p1,p2,p3:" + super.p1.X + "," + super.p1.Y + " ; " + super.p2.X + "," + super.p2.Y + " ; " + super.p3.X + "," + super.p3.Y);
            Console.WriteLine("已将超级三角形加入未确定三角形列表");
            /*for(int i = 0; i < vertices.Count; i++)
            {
                Console.WriteLine(vertices[i].Num + ":" + vertices[i].X + "," + vertices[i].Y);
            }*/
            //Console.ReadKey();     
            for (int i = 0; i < num; i++)
            {
                edges.Clear();
                Console.WriteLine("i: " + i + "num: " + num);
                Tin_Point thepoint = vertices[i];
                for (int j = 0; j < open.Count; j++)
                {
                    Console.WriteLine("j: " + j + " , open.num: " + open.Count);
                    Console.WriteLine("p1: " + open[j].p1.Num + " , p2: " + open[j].p2.Num + " , p3: " + open[j].p3.Num);
                    //Console.WriteLine("thepointx:" + thepoint.X + " , " + "open[j].outcircle.X:" + open[j].outcircle.X);
                    //Console.ReadKey();
                    double dx = thepoint.X - open[j].outcircle.X;
                    if (dx > 0.0 && dx * dx > open[j].outcircle.R_pow)
                    {
                        Console.WriteLine("点" + thepoint.Num + "在圆的右侧，该三角形是Delaunay三角形，将此三角形从open加入到close！");
                        closed.Add(open[j]);
                        open.RemoveRange(j, 1);
                        j--;
                        continue;
                    }
                    double dy = thepoint.Y - open[j].outcircle.Y;
                    if (dx * dx + dy * dy - open[j].outcircle.R_pow > EPSILON)
                    {
                        Console.WriteLine("点" + thepoint.Num + "在圆的外且非右侧，该三角形不确定是Delaunay三角形，不做任何操作！");
                        continue;
                    }
                    Console.WriteLine("点" + thepoint.Num + "在圆的内侧，该三角形必不是delaunary三角形，移除三角形，并将三边加入边的集合！");
                    edges.Add(new Edges(open[j].p1, open[j].p2));
                    edges.Add(new Edges(open[j].p1, open[j].p3));
                    edges.Add(new Edges(open[j].p2, open[j].p3));
                    //问题在于查重和移除
                    open.RemoveRange(j, 1);
                    j--;
                }
                Update(edges);
                for (int j = 0; j < edges.Count; j++)
                {
                    Triangle newtriangle = new Triangle(edges[j].p1, edges[j].p2, thepoint);
                    newtriangle.outcircle = Out_Circle(newtriangle);
                    open.Add(newtriangle);
                }
            }
            Console.WriteLine("完成整体三角形选择！");
            //Console.ReadKey();
            Console.WriteLine("closed:");
            /*for (int k = 0; k < closed.Count; k++)
            {
                Console.WriteLine(closed[k].ToString());
            }
            Console.WriteLine("closed:");
            for (int k = 0; k < open.Count; k++)
            {
                Console.WriteLine(open[k].ToString());
            }*/
            for (int i = 0; i < open.Count; i++)
            {
                closed.Add(open[i]);
            }
            open.Clear();
            for (int i = 0; i < closed.Count; i++)
            {
                if (!RemoveSuper(closed[i], super))
                    open.Add(closed[i]);
            }
            return open;
        }

        public bool Equals(Edges x, Edges y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(Edges obj)
        {
            throw new NotImplementedException();
        }
    }
}
