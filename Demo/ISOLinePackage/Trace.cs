using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Demo.ISOLinePackage
{
    class Trace
    {
        private List<Edges> edges = new List<Edges>();//c存储所有边的相邻三角形
        private int layers = 2;//等值线的分层等级
        private double valuemax = 0;//所有值中最大值
        private double valuemin = 0;//最小值
        private double EPSILON = 0;//一个极小的正数，根据最小值而定
        private double MyEPSILON = 0.002;//用在值上
        private List<Tin_Point> tpoint = new List<Tin_Point>();
        //如果等值点与TIN中的等值点相同，则得到阈值范围
        public List<Edges> GetEdges()
        {
            return this.edges;
        }
        public List<Tin_Point> GetSpecialPoint(double value)//找到特殊的等值点，在三角形顶点与等值线点相同的点
        {
            List<Tin_Point> sps = new List<Tin_Point>();
            foreach (var sp in this.tpoint)
            {
                if (sp.Value == value)
                {
                    sps.Add(sp);
                }
            }
            return sps;
        }
        //
        public double GetThreshold(double value)//此函数必须在edges初始化完成后才能调用
        {
            double theEpsilon = 0.02;
            if (edges.Count == 0)
            {
                return 0;
            }
            double threshold = 0;
            foreach (var e in edges)
            {
                if (e.p1.Value == value && e.p2.Value > value)
                {
                    Tin_Point p = GetValuePoint(e, value + theEpsilon, 0);
                    double distance = Math.Sqrt((e.p1.X - p.X) * (e.p1.X - p.X) + (e.p1.Y - p.Y) * (e.p1.Y - p.Y));
                    if (distance > threshold)
                    {
                        threshold = distance;
                    }
                    //Console.WriteLine("p1阈值为：" + threshold);
                }
                else if (e.p2.Value == value && e.p1.Value > value)
                {
                    Tin_Point p = GetValuePoint(e, value + theEpsilon, 0);
                    double distance = Math.Sqrt((e.p2.X - p.X) * (e.p2.X - p.X) + (e.p2.Y - p.Y) * (e.p2.Y - p.Y));
                    if (distance > threshold)
                    {
                        threshold = distance;
                    }
                    //Console.WriteLine("p1阈值为：" + threshold);
                }
            }
            return threshold;
        }
        //
        public List<Edges> Distinct(List<Edges> edges)//查重去重
        {
            for (int i = 0; i < edges.Count; i++)
            {
                for (int j = i + 1; j < edges.Count; j++)
                {
                    if (edges[i].Equals(edges[j]))
                    {
                        if (edges[i].SignT1 == -1 || edges[j].SignT1 == -1)
                        {
                            Console.WriteLine("异常（不可能事件）！！！");
                            Console.ReadKey();
                        }
                        else if (edges[i].SignT2 != -1 || edges[j].SignT2 != -1)
                        {
                            Console.WriteLine("异常（重复事件）！！！");
                            Console.ReadKey();
                        }
                        else
                        {
                            edges[i].SignT2 = edges[j].SignT1;
                            edges.RemoveRange(j, 1);
                            j--;
                        }
                    }
                }
            }
            return edges;
        }
        public Edges Index(Edges edge)
        {
            int i = 0;
            foreach (Edges e in this.edges)
            {
                if (e.Equals(edge))
                {
                    //Console.WriteLine("边序号：" + i);
                    return e;
                }
                i++;
            }
            return null;
        }
        public void TriangleForEdges(List<Triangle> triangles)//找到所有三角形中去重过的边，保存到edges，调用了Distinct函数
        {
            for (int i = 0; i < triangles.Count; i++)
            {
                Edges e1 = new Edges(triangles[i].p1, triangles[i].p2);
                Edges e2 = new Edges(triangles[i].p1, triangles[i].p3);
                Edges e3 = new Edges(triangles[i].p2, triangles[i].p3);
                e1.SetTriangleSign(i);
                e2.SetTriangleSign(i);
                e3.SetTriangleSign(i);
                /*e1.SetTriangle(triangles[i]);
                e2.SetTriangle(triangles[i]);
                e3.SetTriangle(triangles[i]);*/
                edges.Add(e1);
                edges.Add(e2);
                edges.Add(e3);
            }
            this.edges = Distinct(edges);
            /*for(int i = 0; i < this.edges.Count; i++)
            {
                if(ContainPoint(edges[i],18.4))
                    Console.WriteLine(i + ": " + edges[i].SignT1 + "," + edges[i].SignT2);
            }
            Console.ReadKey();*/
        }
        public void Extremum(List<Tin_Point> tin_Points)//得到最大最小值
        {
            for (int i = 0; i < tin_Points.Count; i++)
            {
                if (i == 0)
                {
                    this.valuemax = tin_Points[i].Value;
                    this.valuemin = tin_Points[i].Value;
                }
                else
                {
                    if (tin_Points[i].Value > this.valuemax)
                    {
                        this.valuemax = tin_Points[i].Value;
                    }
                    else if (tin_Points[i].Value < this.valuemin)
                    {
                        this.valuemin = tin_Points[i].Value;
                    }
                }
            }
            EPSILON = Math.Abs(this.valuemin / 1048576.0);
        }
        public bool ContainPoint(Tin_Point t1, Tin_Point t2, double value)//边上是否包括等值点
        {
            if ((t1.Value - value) * (t2.Value - value) <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ContainPoint(Edges edges, double value)//重载
        {
            if ((edges.p1.Value - value) * (edges.p2.Value - value) <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Edges[] EdgesForTriangle(Edges edges, Triangle triangle)//找到一个三角形的另外两条边
        {
            Edges[] ed = new Edges[2];
            Edges e1 = new Edges(triangle.p1, triangle.p2);
            Edges e2 = new Edges(triangle.p1, triangle.p3);
            Edges e3 = new Edges(triangle.p2, triangle.p3);
            if (edges.Equals(e1))
            {
                ed[0] = e2;
                ed[1] = e3;
            }
            else if (edges.Equals(e2))
            {
                ed[0] = e1;
                ed[1] = e3;
            }
            else
            {
                ed[0] = e1;
                ed[1] = e2;
            }
            return ed;
        }
        public Tin_Point GetValuePoint(Edges edges, double value, int type)//得到一条边上的等值点
        {
            double factor = 0;
            double thex = 0;
            double they = 0;
            if (edges.p1.Value == value && edges.p2.Value > value)
            {
                factor = (value - edges.p1.Value - EPSILON) / (edges.p2.Value - edges.p1.Value - EPSILON);
                thex = edges.p1.X + factor * (edges.p2.X - edges.p1.X);
                they = edges.p1.Y + factor * (edges.p2.Y - edges.p1.Y);
            }
            else if (edges.p1.Value == value && edges.p2.Value < value)
            {
                factor = (value - edges.p1.Value + EPSILON) / (edges.p2.Value - edges.p1.Value + EPSILON);
                thex = edges.p1.X + factor * (edges.p2.X - edges.p1.X);
                they = edges.p1.Y + factor * (edges.p2.Y - edges.p1.Y);
            }
            else if (edges.p2.Value == value && edges.p1.Value < value)
            {
                factor = (value - edges.p1.Value + EPSILON) / (edges.p2.Value - edges.p1.Value + EPSILON);
                thex = edges.p1.X + factor * (edges.p2.X - edges.p1.X);
                they = edges.p1.Y + factor * (edges.p2.Y - edges.p1.Y);
            }
            else if (edges.p2.Value == value && edges.p1.Value > value)
            {
                factor = (value - edges.p1.Value - EPSILON) / (edges.p2.Value - edges.p1.Value - EPSILON);
                thex = edges.p1.X + factor * (edges.p2.X - edges.p1.X);
                they = edges.p1.Y + factor * (edges.p2.Y - edges.p1.Y);
            }
            else
            {
                factor = (value - edges.p1.Value) / (edges.p2.Value - edges.p1.Value);
                thex = edges.p1.X + factor * (edges.p2.X - edges.p1.X);
                they = edges.p1.Y + factor * (edges.p2.Y - edges.p1.Y);
            }
            Tin_Point thepoint = new Tin_Point(thex, they, value);
            //if(thepoint.Equals(edges.p1))
            thepoint.Type = type;
            return thepoint;
        }
        /*public Triangle GetOtherTriangle(Edges edges,Triangle triangle)//得到一条边上的另一个三角形
        {
            if (edges.t1.Equals(triangle))
            {
                return edges.t2;
            }
            else if (edges.t2.Equals(triangle))
            {
                return edges.t1;
            }
            else
                return null;
        }*/
        public int GetOtherTriangle(Edges edges, int trianglesign)//得到一条边上的另一个三角形
        {
            if (edges.SignT1 == trianglesign)
            {
                return edges.SignT2;
            }
            else if (edges.SignT2 == trianglesign)
            {
                return edges.SignT1;
            }
            else
                return -1;
        }
        public List<Triangle> RecoverTriangle(List<Triangle> triangles, bool b)//将所有三角形的标识（用过true）更新（没用过false）
        {
            for (int i = 0; i < triangles.Count; i++)
            {
                triangles[i].flag = b;
            }
            return triangles;
        }
        public Edges GetValidEdges(Triangle triangle, double value)//得到等值点所在的边
        {

            Edges e1 = new Edges(triangle.p1, triangle.p2);
            Edges e2 = new Edges(triangle.p1, triangle.p3);
            Edges e3 = new Edges(triangle.p2, triangle.p3);
            if (ContainPoint(e1, value))
            {
                return e1;
            }
            else if (ContainPoint(e2, value))
            {
                return e2;
            }
            else if (ContainPoint(e3, value))
            {
                return e3;
            }
            else
                return null;
        }
        public bool CheckTriangle(List<Triangle> triangle)//检查是否还有三角形没用过
        {
            for (int i = 0; i < triangle.Count; i++)
            {
                if (triangle[i].flag == false)
                    return true;
                /*else if (i + 1 == triangle.Count && triangle[i].flag == true)
                    return true;*/
            }
            return false;
        }
        /*public void SignTriangle(List<Triangle> triangles,Triangle triangle)
        {
            for(int i = 0; i < triangles.Count; i++)
            {
                if (triangles[i].Equals(triangle))
                {
                    triangles[i].flag = true;
                }
            }
        }*/
        public int NumOfEdges(Edges e)
        {
            for (int i = 0; i < this.edges.Count; i++)
            {
                if (this.edges[i].Equals(e))
                    return i;
            }
            return -1;
        }
        public List<Tin_Point> GetValueLine(List<Triangle> triangles, double value)//type == 0;
        {
            List<Tin_Point> theline = new List<Tin_Point>();//返回值
            Edges firstedges = null;
            Triangle theTriangle = null;
            int type = 0;
            int sign = 0;
            if (triangles.Count <= 0)
            {
                return null;
            }
            for (int i = 0; i < triangles.Count; i++)
            {
                if (GetValidEdges(triangles[i], value) != null && !triangles[i].flag)
                {
                    firstedges = GetValidEdges(triangles[i], value);
                    firstedges = Index(firstedges);
                    //Console.WriteLine(firstedges.ToString());
                    theTriangle = triangles[i];
                    //Console.WriteLine(theTriangle.ToString());
                    sign = i;
                    //Console.WriteLine("三角形序号为：" + sign + "被使用");
                    break;
                }
                else
                {
                    triangles[i].flag = true;
                    //Console.WriteLine("三角形序号为：" + i + "没有该等值点");
                    //Console.WriteLine("三角形" + i + ": " + triangles[i].ToString());

                }
            }
            if (firstedges == null || theTriangle == null)
            {
                return theline;
            }
            var theedge = firstedges;
            var thePoint = GetValuePoint(firstedges, value, type);
            thePoint.Num = NumOfEdges(theedge);
            theline.Add(thePoint);
            while (true)
            {
                /*for(int i = 0; i < triangles.Count; i++)
                {
                    if (triangles[i].flag == false)
                        break;
                    else if (i - 1 == triangles.Count && triangles[i].flag == true)
                    {

                    }
                }//检测三角形数组中还有没有没有搜索到的三角形*/
                triangles[sign].flag = true;
                var edgess = EdgesForTriangle(theedge, theTriangle);

                int useEdge;
                if (ContainPoint(edgess[0], value))
                    useEdge = 0;
                else
                    useEdge = 1;
                /*if(thePoint == null)
                {
                    thePoint = GetValuePoint(edgess[1], value,type);
                    useEdge = 1;
                
                }*/
                thePoint = GetValuePoint(edgess[useEdge], value, type);
                thePoint.Num = NumOfEdges(edgess[useEdge]);
                theline.Add(thePoint);
                theedge = edgess[useEdge];
                theedge = Index(theedge);

                sign = GetOtherTriangle(theedge, sign);
                //Console.WriteLine(sign + ": " + theedge.SignT1 + "," + theedge.SignT2);
                //Console.WriteLine("re三角形序号为：" + sign + "被使用");
                //theTriangle = GetOtherTriangle(theedge, theTriangle);
                if (sign == -1)
                {
                    if (CheckTriangle(triangles))
                    {
                        type++;
                        //Console.WriteLine("进入重载函数进行递归循环" + type);
                        return GetValueLine(triangles, value, theline, type);
                        //未完待续
                    }
                    else
                    {
                        //break;
                        return theline;
                    }
                }
                else
                    theTriangle = triangles[sign];
                if (theTriangle.flag)
                {
                    if (CheckTriangle(triangles))
                    {
                        type++;
                        //Console.WriteLine("进入重载函数进行递归循环" + type);
                        return GetValueLine(triangles, value, theline, type);
                        //未完待续
                    }
                    else
                    {
                        //break;
                        return theline;
                    }
                }
            }
            //return theline;
        }
        public List<Tin_Point> GetValueLine(List<Triangle> triangles, double value, List<Tin_Point> theline, int type)//递归调用
        {
            Edges firstedges = null;
            Triangle theTriangle = null;
            int sign = -1;
            if (triangles.Count <= 0)
            {
                return null;
            }
            for (int i = 0; i < triangles.Count; i++)
            {
                if (GetValidEdges(triangles[i], value) != null && !triangles[i].flag)
                {
                    firstedges = GetValidEdges(triangles[i], value);
                    firstedges = Index(firstedges);
                    //Console.WriteLine(firstedges.ToString());
                    theTriangle = triangles[i];
                    sign = i;
                    //Console.WriteLine("三角形序号为：" + sign + "被使用");
                    //Console.WriteLine(theTriangle.ToString());
                    break;
                }
                else
                {
                    triangles[i].flag = true;
                    //Console.WriteLine("三角形序号为：" + i + "已被使用或没有该等值点");
                    //Console.WriteLine("三角形" + i + ": " + triangles[i].ToString());
                }
            }
            if (firstedges == null || theTriangle == null)
            {
                return theline;
            }
            var theedge = firstedges;
            var thePoint = GetValuePoint(firstedges, value, type);
            thePoint.Num = NumOfEdges(theedge);
            //Console.WriteLine("点的层数（type）为:" + thePoint.ToString() +";现在的层数（type）为："+type);
            theline.Add(thePoint);
            while (true)
            {
                /*for(int i = 0; i < triangles.Count; i++)
                {
                    if (triangles[i].flag == false)
                        break;
                    else if (i - 1 == triangles.Count && triangles[i].flag == true)
                    {

                    }
                }//检测三角形数组中还有没有没有搜索到的三角形*/
                triangles[sign].flag = true;
                var edgess = EdgesForTriangle(theedge, theTriangle);
                int useEdge;
                if (ContainPoint(edgess[0], value))
                    useEdge = 0;
                else
                    useEdge = 1;
                thePoint = GetValuePoint(edgess[useEdge], value, type);

                /*if (thePoint == null)
                {
                    thePoint = GetValuePoint(edgess[1], value, type);
                    useEdge = 1;

                }
                Console.WriteLine(edgess[0].ToString());
                Console.WriteLine(edgess[1].ToString());
                Console.ReadKey();*/
                thePoint.Num = NumOfEdges(edgess[useEdge]);
                theline.Add(thePoint);
                theedge = edgess[useEdge];
                theedge = Index(theedge);//变成了序号为4的边？应该是序号为8的边

                sign = GetOtherTriangle(theedge, sign);
                //Console.WriteLine(sign + ": " + theedge.SignT1 + "," + theedge.SignT2);
                //Console.WriteLine("re三角形序号为：" + sign + "被使用");
                //theTriangle = GetOtherTriangle(theedge, theTriangle);
                if (sign == -1)
                {
                    if (CheckTriangle(triangles))
                    {
                        type++;
                        Console.WriteLine("进入重载函数进行递归循环" + type);
                        return GetValueLine(triangles, value, theline, type);
                        //未完待续
                    }
                    else
                    {
                        //break;
                        return theline;
                    }
                }
                else
                    theTriangle = triangles[sign];
                if (theTriangle.flag)
                {
                    if (CheckTriangle(triangles))
                    {
                        type++;
                        Console.WriteLine("进入重载函数进行递归循环" + type);
                        return GetValueLine(triangles, value, theline, type);
                        //未完待续
                    }
                    else
                    {
                        //break;
                        return theline;
                    }
                }
            }
        }
        public List<List<Tin_Point>> MyTrace(List<Triangle> triangles, List<Tin_Point> tin_Points, double interval, double start)//调用轨迹查找的总函数
        {
            this.tpoint = tin_Points;
            List<double> linevalue = new List<double>();//每个等值线的值
            this.Extremum(tin_Points);//得到最值
            Console.WriteLine("最大值：" + this.valuemax + ",最小值：" + this.valuemin + ",Epsilon：" + this.MyEPSILON);
            this.TriangleForEdges(triangles);//更新边
            Console.WriteLine("边已更新完毕");
            //for(int i = 1; i < this.layers; i++)
            //{
            //    linevalue.Add(this.valuemin + (this.valuemax - this.valuemin) / this.layers * i);
            //}
            //interval代表着等值线之间的间距。
            if (start >= this.valuemax)
            {
                start = this.valuemin;
            }
            double thevalue = start - interval;
            while (true)
            {
                thevalue = thevalue + interval;
                if (thevalue < this.valuemin)
                {
                    continue;
                }
                else if (thevalue > this.valuemax)
                {
                    break;
                }
                foreach (var p in tin_Points)
                {
                    if (p.Value == thevalue)
                    {
                        thevalue += this.MyEPSILON;
                        break;
                    }
                }
                linevalue.Add(thevalue);
            }

            //linevalue.Add(this.valuemin + (this.valuemax - this.valuemin) / this.layers);

            //为验证先只尝试一条等值线
            //List<Tin_Point> line1 = new List<Tin_Point>();
            //line1 = GetValueLine(triangles, linevalue[0]);
            //return line1;

            //开启多条等值线的处理
            List<List<Tin_Point>> lines = new List<List<Tin_Point>>();
            foreach (var values in linevalue)
            {
                //Console.WriteLine("进入value循环！");
                lines.Add(new List<Tin_Point>());
                lines[lines.Count - 1] = GetValueLine(triangles, values);
                RecoverTriangle(triangles, false);
                //Console.WriteLine("Values: " + ls[0].Value);
                //Console.WriteLine("values: " + values);

            }

            //lines.Add(new List<Tin_Point>());
            //lines[0] = GetValueLine(triangles, 21.002);
            //RecoverTriangle(triangles, false);
            //lines.Add(new List<Tin_Point>());
            //lines[1] = GetValueLine(triangles, 18.002);
            //lines.Add(new List<Tin_Point>());
            //lines[1] = GetValueLine(triangles, 12);
            /*lines.Add(new List<Tin_Point>());
            lines[0] = GetValueLine(triangles, 14);
            //lines.Add(new List<Tin_Point>());
            RecoverTriangle(triangles, false);
            ls = GetValueLine(triangles, 17);
            Console.WriteLine(lines[0].Count);
            Console.WriteLine(ls.Count);*/
            //
            return lines;
            //List<Tin_Point> line2 = new List<Tin_Point>();
            //List<Tin_Point> line3 = new List<Tin_Point>();
            //List<Tin_Point> line4 = new List<Tin_Point>();
            //List<List<Tin_Point>> lists = new List<List<Tin_Point>>();

        }
    }
}