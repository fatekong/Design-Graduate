using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Demo.ISOLinePackage
{
    class PolishLine
    {
        private Map map;
        private FeatureSet fs;
        private const int Clip = 10;
        private List<Tin_Point> line = new List<Tin_Point>();
        private Trace trace;
        private double threshold = 0;
        public double GetDistance(Tin_Point p1, Tin_Point p2)
        {
            double distance = Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
            return distance;
        }
        public PolishLine(Map map1, FeatureSet fs1, Trace trace1)
        {
            this.map = map1;
            this.fs = fs1;
            this.trace = trace1;
        }
        public void SelectLine(List<Tin_Point> lines)
        {
            if (lines[0].Type == -2)
                CloseLine(lines);
            else if (lines[0].Type == -1)
                FreeLine(lines);
            //return null;
        }
        public void FreeLine(List<Tin_Point> freeline)
        {

            if (freeline.Count < 4)
            {
                Console.WriteLine("不符合标准！");
                return;
                //return null;
            }
            else
            {

            }
            //label标识
            //
            List<Coordinate> lineArray = new List<Coordinate>(); ;
            ILineString lineGeometry = new LineString(lineArray);
            IFeature lineFeature = fs.AddFeature(lineGeometry);
            lineFeature.DataRow["Value"] = freeline[0].Value;
            //非封闭的等值线，需要在最前面加入p0，p0=p1,pn+1 = pn
            freeline.Add(freeline[freeline.Count - 1]);
            int i, j, n;
            n = freeline.Count;//等值点总数
            for (i = freeline.Count - 1; i > 0; i--)
            {
                freeline[i] = freeline[i - 1];
            }
            freeline.Add(freeline[freeline.Count - 1]);
            //
            double t1, t2, t3, t, a, b, c, d, x, y;//abcd为参数，xy为新插入点的坐标，
            //t1,t2,t3分别表示系数的开方，t1表示一次项系数，t2表示二次项系数，它表示3此项系数
            t = 0.5f / Clip;//系数0-0.5
                            //List<List<Tin_Point>> point_list = new List<List<Tin_Point>>();
            for (i = 0; i < n - 2; i++)
            {
                //
                Coordinate coordinate = new Coordinate(freeline[i + 1].X, freeline[i + 1].Y);
                lineArray.Add(coordinate);
                lineFeature.Coordinates.Add(coordinate);
                //
                //point_list.Add(new List<Tin_Point>());
                //List<Tin_Point> tp = new List<Tin_Point>();
                //point_list.Add(tp);
                for (j = 1; j < Clip; j++)
                {
                    t1 = j * t;
                    t2 = t1 * t1;
                    t3 = t2 * t1;
                    //下面是公式的表达
                    a = 4.0 * t2 - t1 - 4.0 * t3;
                    b = 1.0 - 10.0 * t2 + 12.0 * t3;
                    c = t1 + 8.0 * t2 - 12.0 * t3;
                    d = 4.0 * t3 - 2.0 * t2;
                    x = a * freeline[i].X + b * freeline[i + 1].X + c * freeline[i + 2].X + d * freeline[i + 3].X;
                    y = a * freeline[i].Y + b * freeline[i + 1].Y + c * freeline[i + 2].Y + d * freeline[i + 3].Y;
                    //Tin_Point new_point = new Tin_Point(x, y);
                    //
                    coordinate = new Coordinate(x, y);
                    lineArray.Add(coordinate);
                    LineString ls = new LineString(lineArray);
                    lineFeature.Coordinates.Add(coordinate);
                    fs.InitializeVertices();
                    //
                    //point_list[i].Add(new_point);
                }
                if (i == n - 3)
                {
                    coordinate = new Coordinate(freeline[i + 2].X, freeline[i + 2].Y);
                    lineArray.Add(coordinate);
                    LineString ls = new LineString(lineArray);
                    lineFeature.Coordinates.Add(coordinate);
                    fs.InitializeVertices();
                }
                //tp.Clear();
            }

            map.ResetBuffer();
            //return point_list;
        }
        public void CloseLine(List<Tin_Point> closeline)
        {
            if (closeline.Count < 4)
            {
                Console.WriteLine("不符合标准！");
                //return null;
            }
            else
            {
                List<Coordinate> lineArray = new List<Coordinate>(); ;
                ILineString lineGeometry = new LineString(lineArray);
                IFeature lineFeature = fs.AddFeature(lineGeometry);
                lineFeature.DataRow["Value"] = (int)closeline[0].Value;
                closeline.Add(closeline[closeline.Count - 1]);
                int i, j, n;
                double t1, t2, t3, t, a, b, c, d, x, y;//abcd为参数，xy为新插入点的坐标，
                                                       //t1,t2,t3分别表示系数的开方，t1表示一次项系数，t2表示二次项系数，它表示3此项系数
                for (i = closeline.Count - 1; i > 0; i--)
                {
                    closeline[i] = closeline[i - 1];
                }
                n = closeline.Count;
                closeline[0].X = closeline[n - 1].X;
                closeline[0].Y = closeline[n - 1].Y;
                closeline.Add(closeline[1]);
                closeline.Add(closeline[2]);
                t = 0.5f / Clip;
                List<List<Tin_Point>> point_list = new List<List<Tin_Point>>();
                for (i = 0; i < n - 1; i++)
                {
                    //
                    Coordinate coordinate = new Coordinate(closeline[i + 1].X, closeline[i + 1].Y);
                    lineArray.Add(coordinate);
                    lineFeature.Coordinates.Add(coordinate);
                    //
                    point_list.Add(new List<Tin_Point>());
                    //List<Tin_Point> tp = new List<Tin_Point>();
                    //point_list.Add(tp);
                    for (j = 1; j < Clip; j++)
                    {
                        t1 = j * t;
                        t2 = t1 * t1;
                        t3 = t1 * t2;
                        a = 4.0 * t2 - t1 - 4.0 * t3;
                        b = 1.0 - 10.0 * t2 + 12.0 * t3;
                        c = t1 + 8.0 * t2 - 12.0 * t3;
                        d = 4.0 * t3 - 2.0 * t2;
                        x = a * closeline[i].X + b * closeline[i + 1].X + c * closeline[i + 2].X + d * closeline[i + 3].X;
                        y = a * closeline[i].Y + b * closeline[i + 1].Y + c * closeline[i + 2].Y + d * closeline[i + 3].Y;
                        //Tin_Point new_point = new Tin_Point(x, y);
                        //
                        coordinate = new Coordinate(x, y);
                        lineArray.Add(coordinate);
                        LineString ls = new LineString(lineArray);
                        lineFeature.Coordinates.Add(coordinate);
                        fs.InitializeVertices();
                        //
                        //point_list[i].Add(new_point);
                    }
                    if (i == n - 3)
                    {
                        coordinate = new Coordinate(closeline[i + 2].X, closeline[i + 2].Y);
                        lineArray.Add(coordinate);
                        LineString ls = new LineString(lineArray);
                        lineFeature.Coordinates.Add(coordinate);
                        fs.InitializeVertices();
                    }
                    //tp.Clear();
                }
                map.ResetBuffer();
            }
            //return point_list;
        }
        public List<List<Tin_Point>> SetLinesType(List<List<Tin_Point>> tp)
        {
            foreach (var intp in tp)
            {
                if (intp[0].Equals(intp[intp.Count - 1]))
                {
                    for (int k = 0; k < intp.Count; k++)
                    {
                        intp[k].Type = -2;//闭合式
                    }
                }
                else
                {
                    for (int k = 0; k < intp.Count; k++)
                    {
                        intp[k].Type = -1;//暂定为开放式
                    }
                }
            }
            return tp;
        }
        //得到阈值
        public void GetThreshold(double original)//此函数必须在edges初始化完成后才能调用
        {
            double theEpsilon = 0.002;
            if (trace.GetEdges().Count == 0)
            {
                return;
            }
            //double threshold = 0;
            foreach (var e in trace.GetEdges())
            {
                if (e.p1.Value == original)
                {
                    Tin_Point p = trace.GetValuePoint(e, original + theEpsilon, 0);
                    double distance = Math.Abs((e.p1.X - p.X) * (e.p1.X - p.X) + (e.p1.Y - p.Y) * (e.p1.Y - p.Y));
                    if (distance > threshold)
                    {
                        threshold = distance;
                    }
                }
                else if (e.p2.Value == original)
                {
                    Tin_Point p = trace.GetValuePoint(e, original + theEpsilon, 0);
                    double distance = Math.Abs((e.p2.X - p.X) * (e.p2.X - p.X) + (e.p2.Y - p.Y) * (e.p2.Y - p.Y));
                    if (distance > threshold)
                    {
                        threshold = distance;
                    }
                }
            }
        }
        /*
        public List<List<Tin_Point>> TraceModify(List<List<Tin_Point>> tp,double original)//改进算法，避免出现,以18作为例子
        {
            //首先要计算临界距离，在这个距离内的点表示与当前等值点几乎重合。
            List<Tin_Point> p18 = new List<Tin_Point>();
            List<bool> flag = new List<bool>();//用来记录那几条等值线包含特殊点
            Tin_Point p1 = null;
            Tin_Point p2 = null ;
            Tin_Point p3 = null;
            Tin_Point p4 = null;
            List<Tin_Point> sps = new List<Tin_Point>();
            int l1, l2;
            l1 = l2 = -1;//两条等值线的序号
            sps = trace.GetSpecialPoint(original);//value = 18
            foreach (var points in sps)
            {
                for (int i = 0; i < tp.Count; i++)
                {
                    flag.Add(false);
                    foreach(var p in tp[i])
                    {
                        double distance = Math.Sqrt((p.X - points.X) * (p.X - points.X) + (p.Y - points.Y) * (p.Y - points.Y));
                        if(distance <= threshold)
                        {
                            p18.Add(p);
                            flag[i] = true;
                            break;
                        }
                    }
                    if (flag[i] == false)
                        p18.Add(null);
                }
                for(int i = 0;i<flag.Count;i++)
                {
                    if(flag[i] == true)
                    {
                        for(int j = 0; j < tp[i].Count; j++)
                        {
                            if (tp[i][j].Equals(p18[i]))
                            {
                                if(p1 == null&& p3 == null)
                                {
                                    p1 = tp[i][j - 1];
                                    p3 = tp[i][j + 1];
                                    l1 = i;
                                }
                                else
                                {
                                    p2 = tp[i][j - 1];
                                    p4 = tp[i][j + 1];
                                    l2 = i;
                                }
                                break;
                            }
                        }
                    }
                }
                if(l1!=-1&& l2 != -1)//得到tp中的两条等值线
                {
                    List<Tin_Point> newline1, newline2;//共同构建出两条新的等值线
                    newline1 = new List<Tin_Point>();
                    newline2 = new List<Tin_Point>();
                    bool sign = false;
                    if ((GetDistance(p1,p2) + GetDistance(p3, p4)) < (GetDistance(p1, p4) + GetDistance(p2, p3)))//12 34分别为两组
                    {
                        if (GetDistance(p1, p2) < GetDistance(p3, p4))
                        {
                            for(int k = tp[l1].Count - 1; k >= 0; k--)
                            {
                                if (tp[l1][k].Equals(p3))
                                {
                                    newline1.Add(p3);
                                    newline1.Add(points);
                                    break;
                                }
                                newline1.Add(tp[l1][k]);
                            }
                            for(int k = 0; k < tp[l2].Count; k++)
                            {
                                if (sign)
                                {
                                    newline1.Add(tp[l2][k]);
                                }
                                if (tp[l2][k].Equals(p4))
                                {
                                    newline1.Add(p4);
                                    sign = true;
                                }
                            }
                            sign = false;
                            for (int k = 0; k < tp[l1].Count; k++)
                            {
                                if (tp[l1][k].Equals(p1))
                                {
                                    newline2.Add(p1);
                                    break;
                                }
                                newline2.Add(tp[l1][k]);
                            }
                            for(int k = tp[l2].Count - 1; k >= 0; k--)
                            {
                                if (sign)
                                {
                                    newline2.Add(tp[l2][k]);
                                }
                                if (tp[l2][k].Equals(p2))
                                {
                                    newline2.Add(p2);
                                    sign = true;
                                }
                            }
                            sign = false;
                        }
                        else
                        {
                            for (int k = 0; k <tp[l1].Count; k++)
                            {
                                if (tp[l1][k].Equals(p1))
                                {
                                    newline1.Add(p1);
                                    newline1.Add(points);
                                    break;
                                }
                                newline1.Add(tp[l1][k]);
                            }
                            for (int k = tp[l2].Count-1; k >=0; k--)
                            {
                                if (sign)
                                {
                                    newline1.Add(tp[l2][k]);
                                }
                                if (tp[l2][k].Equals(p2))
                                {
                                    newline1.Add(p2);
                                    sign = true;
                                }
                            }
                            sign = false;
                            for (int k = tp[l1].Count - 1; k >= 0; k--)
                            {
                                if (tp[l1][k].Equals(p3))
                                {
                                    newline2.Add(p3);
                                    break;
                                }
                                newline2.Add(tp[l1][k]);
                            }
                            for (int k = 0; k < tp[l2].Count; k++)
                            {
                                if (sign)
                                {
                                    newline2.Add(tp[l2][k]);
                                }
                                if (tp[l2][k].Equals(p4))
                                {
                                    newline2.Add(p4);
                                    sign = true;
                                }
                            }
                            sign = false;
                        }
                    }
                    else//14 23分别为两组
                    {
                        if (GetDistance(p1, p4) < GetDistance(p3, p2))//p3后，p3，p18，p2，p2前（p1前，p1，p4，p4后）
                        {
                            for(int k = 0; k < tp[l2].Count; k++)//newl1，p2前 p2 p18
                            {
                                if (tp[l2][k].Equals(p2))
                                {
                                    newline1.Add(p2);
                                    newline1.Add(points);
                                    break;
                                }
                                newline1.Add(tp[l2][k]);
                            }
                            for(int k = 0; k < tp[l1].Count; k++)//newl1，p3 p3后
                            {
                                if(sign == true)
                                {
                                    newline1.Add(tp[l1][k]);
                                }
                                if (tp[l1][k].Equals(p3))
                                {
                                    newline1.Add(p3);
                                    sign = true;
                                }
                            }
                            sign = false;
                            for(int k = 0; k < tp[l1].Count; k++)//newl2，p1前，p1
                            {
                                if (tp[l1][k].Equals(p1))
                                {
                                    newline2.Add(p1);
                                    break;
                                }
                                newline2.Add(tp[l2][k]);
                            }
                            for(int k = 0; k < tp[l2].Count; k++)//newl2，p4，p4后
                            {
                                if (sign)
                                {
                                    newline2.Add(tp[l2][k]);
                                }
                                if (tp[l2][k].Equals(p4))
                                {
                                    newline2.Add(p4);
                                    sign = true;
                                }
                            }
                            sign = false;
                        }
                        else//p1前，p1，p18，p4，p4后（p3后，p3，p2，p2前）
                        {
                            for (int k = 0; k < tp[l1].Count; k++)//newl1加入p1，p18以及之前
                            {
                                if (tp[l1][k].Equals(p1))
                                {
                                    newline1.Add(p1);
                                    newline1.Add(points);
                                    break;
                                }
                                newline1.Add(tp[l1][k]);
                            }
                            for (int k = 0; k < tp[l2].Count; k++)//newl1加入p4以及之后
                            {
                                if (sign == true)
                                {
                                    newline1.Add(tp[l2][k]);
                                }
                                if (tp[l2][k].Equals(p4))
                                {
                                    newline1.Add(p4);
                                    sign = true;
                                }
                            }
                            sign = false;
                            for (int k = 0; k < tp[l2].Count; k++)//newl2加入p2以及之前
                            {
                                if (tp[l2][k].Equals(p2))
                                {
                                    newline2.Add(p2);
                                    break;
                                }
                                newline2.Add(tp[l2][k]);
                            }
                            for (int k = 0; k < tp[l1].Count; k++)//newl2加入p3以及之后
                            {
                                if (sign)
                                {
                                    newline2.Add(tp[l1][k]);
                                }
                                if (tp[l1][k].Equals(p3))
                                {
                                    newline2.Add(p3);
                                    sign = true;
                                }
                            }
                            sign = false;
                        }
                    }
                    //将newl1和newl2加入等值线
                    tp.Add(newline1);
                    tp.Add(newline2);
                    tp.RemoveAt(l1);
                    l2--;
                    tp.RemoveAt(l2);
                    
                }



            }
            return tp;
        }
        */
        //一条等值线上两个点距离因为某等值点的原因太近可以进行移除
        public List<Tin_Point> RemoveInvalid(List<Tin_Point> lines, Tin_Point point)
        {
            //Console.WriteLine("Spetial Point: "+point.ToString());
            Tin_Point p1, p2;
            bool flag = false;
            //Console.WriteLine("*******************************");
            for (int i = 0; i < lines.Count - 1; i++)
            {
                p1 = lines[i];
                p2 = lines[i + 1];
                //Console.WriteLine("p2: " + p2.ToString());

                if (!flag)
                {
                    if (GetDistance(p1, point) <= threshold)
                    {
                        flag = true;
                        if (GetDistance(p2, point) <= threshold)
                        {
                            if (GetDistance(p1, p2) < 2 * threshold)
                            {
                                lines.RemoveAt(i + 1);
                                i--;
                            }
                        }
                    }
                }
                else
                {
                    /*if (GetDistance(lines[0], point) <= threshold)
                    {
                        lines.RemoveAt(0);
                        i--;
                    }
                    else if (GetDistance(lines[lines.Count - 1], point) <= threshold)
                    {
                        lines.RemoveAt(lines.Count - 1);
                        i--;
                    }*/
                    if (GetDistance(lines[0], point) <= threshold)
                    {
                        lines.RemoveAt(0);
                        i--;
                        flag = false;
                    }
                    else if (GetDistance(lines[lines.Count - 1], point) <= threshold)
                    {
                        lines.RemoveAt(lines.Count - 1);
                        i--;
                        flag = false;
                    }
                    if (GetDistance(p2, point) <= threshold)
                    {
                        lines.RemoveAt(i + 1);
                        //Console.WriteLine("Remove Point: " + point.ToString());
                        i--;
                    }
                }
            }
            return lines;
        }
        //
        public List<List<Tin_Point>> TraceModify(List<List<Tin_Point>> tp, double original)//改进算法，避免出现,以18作为例子
        {
            threshold = trace.GetThreshold(original);
            Console.WriteLine("阈值的大小为：" + threshold);
            //首先要计算临界距离，在这个距离内的点表示与当前等值点几乎重合。
            List<Tin_Point> p18 = new List<Tin_Point>();
            List<bool> flag = new List<bool>();//用来记录那几条等值线包含特殊点
            Tin_Point p1 = null;
            Tin_Point p2 = null;
            Tin_Point p3 = null;
            Tin_Point p4 = null;
            List<Tin_Point> sps = new List<Tin_Point>();
            int l1, l2;
            l1 = l2 = -1;//两条等值线的序号
            sps = trace.GetSpecialPoint(original);//value = 18
            foreach (var points in sps)
            {
                p18.Clear();
                flag.Clear();
                for (int i = 0; i < tp.Count; i++)
                {
                    flag.Add(false);
                    tp[i] = RemoveInvalid(tp[i], points);//移除距离太近的且靠近等值点的点
                    foreach (var p in tp[i])
                    {
                        double distance = Math.Sqrt((p.X - points.X) * (p.X - points.X) + (p.Y - points.Y) * (p.Y - points.Y));
                        if (distance <= threshold)
                        {
                            p18.Add(p);
                            flag[i] = true;
                            break;
                        }
                    }
                    if (flag[i] == false)
                        p18.Add(null);
                }
                for (int i = 0; i < flag.Count; i++)
                {
                    if (flag[i] == true)
                    {
                        for (int j = 0; j < tp[i].Count; j++)
                        {
                            if (tp[i][j].Equals(p18[i]))
                            {
                                if (p1 == null && p3 == null)
                                {
                                    if (j - 1 < 0 || j + 1 == tp[i].Count)
                                    {
                                        break;
                                    }
                                    p1 = tp[i][j - 1];
                                    p3 = tp[i][j + 1];
                                    l1 = i;
                                }
                                else
                                {
                                    p2 = tp[i][j - 1];
                                    p4 = tp[i][j + 1];
                                    l2 = i;
                                }
                                break;
                            }
                        }
                    }
                }
                if (l1 != -1 && l2 != -1)//得到tp中的两条等值线
                {
                    List<Tin_Point> newline1, newline2;//共同构建出两条新的等值线
                    newline1 = new List<Tin_Point>();
                    newline2 = new List<Tin_Point>();
                    bool sign = false;
                    if ((GetDistance(p1, p2) + GetDistance(p3, p4)) < (GetDistance(p1, p4) + GetDistance(p2, p3)))//12 34分别为两组
                    {
                        Console.WriteLine("12 34 < 14 23");
                        if (GetDistance(p1, p2) < GetDistance(p3, p4))
                        {
                            Console.WriteLine("12 < 43");
                            for (int k = tp[l1].Count - 1; k >= 0; k--)
                            {
                                if (tp[l1][k].Equals(p3))
                                {
                                    newline1.Add(p3);
                                    newline1.Add(points);
                                    break;
                                }
                                newline1.Add(tp[l1][k]);
                            }
                            for (int k = 0; k < tp[l2].Count; k++)
                            {
                                if (sign)
                                {
                                    newline1.Add(tp[l2][k]);
                                }
                                if (tp[l2][k].Equals(p4))
                                {
                                    newline1.Add(p4);
                                    sign = true;
                                }
                            }
                            sign = false;
                            for (int k = 0; k < tp[l1].Count; k++)
                            {
                                if (tp[l1][k].Equals(p1))
                                {
                                    newline2.Add(p1);
                                    break;
                                }
                                newline2.Add(tp[l1][k]);
                            }
                            for (int k = tp[l2].Count - 1; k >= 0; k--)
                            {
                                if (sign)
                                {
                                    newline2.Add(tp[l2][k]);
                                }
                                if (tp[l2][k].Equals(p2))
                                {
                                    newline2.Add(p2);
                                    sign = true;
                                }
                            }
                            sign = false;
                        }
                        else
                        {
                            Console.WriteLine("12 > 43");
                            for (int k = 0; k < tp[l1].Count; k++)
                            {
                                if (tp[l1][k].Equals(p1))
                                {
                                    newline1.Add(p1);
                                    newline1.Add(points);
                                    break;
                                }
                                newline1.Add(tp[l1][k]);
                            }
                            for (int k = tp[l2].Count - 1; k >= 0; k--)
                            {
                                if (sign)
                                {
                                    newline1.Add(tp[l2][k]);
                                }
                                if (tp[l2][k].Equals(p2))
                                {
                                    newline1.Add(p2);
                                    sign = true;
                                }
                            }
                            sign = false;
                            for (int k = tp[l1].Count - 1; k >= 0; k--)
                            {
                                if (tp[l1][k].Equals(p3))
                                {
                                    newline2.Add(p3);
                                    break;
                                }
                                newline2.Add(tp[l1][k]);
                            }
                            for (int k = 0; k < tp[l2].Count; k++)
                            {
                                if (sign)
                                {
                                    newline2.Add(tp[l2][k]);
                                }
                                if (tp[l2][k].Equals(p4))
                                {
                                    newline2.Add(p4);
                                    sign = true;
                                }
                            }
                            sign = false;
                        }
                    }
                    else//14 23分别为两组
                    {
                        Console.WriteLine("14 23 < 12 24");
                        if (GetDistance(p1, p4) < GetDistance(p3, p2))//p3后，p3，p18，p2，p2前（p1前，p1，p4，p4后）
                        {
                            Console.WriteLine("14 < 32");
                            for (int k = 0; k < tp[l2].Count; k++)//newl1，p2前 p2 p18
                            {
                                if (tp[l2][k].Equals(p2))
                                {
                                    newline1.Add(p2);
                                    Console.WriteLine("P2: " + p2.ToString());
                                    newline1.Add(points);
                                    break;
                                }
                                newline1.Add(tp[l2][k]);
                            }
                            for (int k = 0; k < tp[l1].Count; k++)//newl1，p3 p3后
                            {
                                if (sign == true)
                                {
                                    newline1.Add(tp[l1][k]);
                                }
                                if (tp[l1][k].Equals(p3))
                                {
                                    Console.WriteLine("P3: " + p3.ToString());
                                    newline1.Add(p3);
                                    sign = true;
                                }
                            }
                            sign = false;
                            for (int k = 0; k < tp[l1].Count; k++)//newl2，p1前，p1
                            {
                                if (tp[l1][k].Equals(p1))
                                {
                                    newline2.Add(p1);
                                    break;
                                }
                                newline2.Add(tp[l1][k]);
                            }
                            for (int k = 0; k < tp[l2].Count; k++)//newl2，p4，p4后
                            {
                                if (sign)
                                {
                                    newline2.Add(tp[l2][k]);
                                }
                                if (tp[l2][k].Equals(p4))
                                {
                                    newline2.Add(p4);
                                    sign = true;
                                }
                            }
                            sign = false;
                        }
                        else//p1前，p1，p18，p4，p4后（p3后，p3，p2，p2前）
                        {
                            Console.WriteLine("23 < 14");
                            for (int k = 0; k < tp[l1].Count; k++)//newl1加入p1，p18以及之前
                            {
                                if (tp[l1][k].Equals(p1))
                                {
                                    newline1.Add(p1);
                                    newline1.Add(points);
                                    break;
                                }
                                newline1.Add(tp[l1][k]);
                            }
                            for (int k = 0; k < tp[l2].Count; k++)//newl1加入p4以及之后
                            {
                                if (sign == true)
                                {
                                    newline1.Add(tp[l2][k]);
                                }
                                if (tp[l2][k].Equals(p4))
                                {
                                    newline1.Add(p4);
                                    sign = true;
                                }
                            }
                            sign = false;
                            for (int k = 0; k < tp[l2].Count; k++)//newl2加入p2以及之前
                            {
                                if (tp[l2][k].Equals(p2))
                                {
                                    newline2.Add(p2);
                                    //newline2.Add(points);
                                    break;
                                }
                                newline2.Add(tp[l2][k]);
                            }
                            for (int k = 0; k < tp[l1].Count; k++)//newl2加入p3以及之后
                            {
                                if (sign)
                                {
                                    newline2.Add(tp[l1][k]);
                                }
                                if (tp[l1][k].Equals(p3))
                                {
                                    newline2.Add(p3);
                                    sign = true;
                                }
                            }
                            sign = false;
                        }
                    }
                    //将newl1和newl2加入等值线
                    tp.Add(newline1);
                    tp.Add(newline2);
                    tp.RemoveAt(l1);
                    l2--;
                    tp.RemoveAt(l2);
                }
                p1 = p2 = p3 = p4 = null;
                l1 = l2 = -1;
            }

            return tp;
        }
        //
        public List<List<Tin_Point>> ClassifyLine(List<Tin_Point> lines, bool IsSpetialPoint)//将等值线进行合并归类。
        {
            //确认开放等值线和闭合等值线。
            List<List<Tin_Point>> tp = new List<List<Tin_Point>>();
            Console.WriteLine("进入ClassifyLine！");
            //Console.WriteLine("Value & Count: " + lines[0].Value + " & " +lines.Count);

            for (int i = 0; i <= lines[lines.Count - 1].Type; i++)
            {
                tp.Add(new List<Tin_Point>());
                for (int j = 0; j < lines.Count; j++)
                {
                    if (lines[0].Type == i)
                    {
                        tp[i].Add(lines[0]);
                        lines.RemoveAt(0);
                        j--;
                    }
                    else
                    {
                        //Console.WriteLine("loop: " + lines[j].Type);
                        break;
                    }
                }
                if (lines.Count == 0)
                    break;
            }

            /*for (int i = 0; i < tp.Count; i++)
            {
                Console.WriteLine("****************" + i);
                foreach (var p in tp[i])
                {
                    Console.WriteLine(p.ToString());
                }
            }*/
            for (int i = 0; i < tp.Count; i++)
            {
                for (int j = i + 1; j < tp.Count; j++)
                {
                    List<Tin_Point> newone = new List<Tin_Point>();
                    bool Ismodify = false;//是否发生过合并操作
                    if (tp[i][0].Equals(tp[j][0]))//首位相同
                    {
                        //tp.Add(new List<Tin_Point>());
                        Console.WriteLine("首位相同！");
                        for (int k = tp[i].Count - 1; k > 0; k--)
                        {
                            //Tin_Point newpoint = new Tin_Point(tp[i][k].X, tp[i][k].Y, tp[i][k].Value, tp[i][k].Num);//值传递防止地址传递
                            newone.Add(tp[i][k]);
                        }
                        /*for(int k = 0; k < tp[j].Count; k++)
                        {
                            //Tin_Point newpoint = new Tin_Point(tp[j][k].X, tp[j][k].Y, tp[j][k].Value, tp[j][k].Num);
                            newone.Add(newpoint);
                        }*/
                        foreach (var p in tp[j])
                        {
                            newone.Add(p);
                        }
                        Ismodify = true;
                    }
                    else if (tp[i][tp[i].Count - 1].Equals(tp[j][tp[j].Count - 1]))//末位相同
                    {
                        Console.WriteLine("末位相同！");
                        foreach (var p in tp[i])
                        {
                            newone.Add(p);
                        }
                        for (int k = tp[j].Count - 1; k > 0; k--)
                        {
                            newone.Add(tp[j][k]);
                        }
                        Ismodify = true;
                    }
                    else if (tp[i][0].Equals(tp[j][tp[j].Count - 1]))//i首等于j尾
                    {
                        Console.WriteLine("首末相同！");
                        foreach (var p in tp[j])
                        {
                            //Tin_Point newpoint = new Tin_Point(p.X, p.Y, p.Value, p.Num);
                            newone.Add(p);
                        }
                        tp[i].RemoveAt(0);//移除首位避免重复
                        foreach (var p in tp[i])
                        {
                            newone.Add(p);
                        }
                        Ismodify = true;
                    }
                    else if (tp[i][tp[i].Count - 1].Equals(tp[j][0]))//i尾等于j首
                    {
                        Console.WriteLine("末首相同！");
                        foreach (var p in tp[i])
                        {
                            newone.Add(p);
                        }
                        tp[j].RemoveAt(0);//移除首位避免重复
                        foreach (var p in tp[j])
                        {
                            newone.Add(p);
                        }
                        Ismodify = true;
                    }
                    if (Ismodify)
                    {
                        tp.Add(newone);
                        //Console.WriteLine("i,j: " + i + " , " + j);
                        tp.RemoveAt(i);
                        j--;
                        tp.RemoveAt(j);
                        i--;
                        //Console.WriteLine("更改后的tp数目：" + tp.Count);
                        break;
                    }
                }
            }
            /*foreach (var intp in tp)
            {
                if (intp[0].Equals(intp[intp.Count - 1]))
                {
                    for (int k = 0; k < intp.Count; k++)
                    {
                        intp[k].Type = -2;//闭合式
                    }
                }
                else
                {
                    for (int k = 0; k < intp.Count; k++)
                    {
                        intp[k].Type = -1;//暂定为开放式
                    }
                }
            }
            //Console.WriteLine("Over Value & Count: " + tp[0][0].Value + " & " + lines.Count);
            return tp;*/

            /*for (int i = 0; i < tp.Count; i++)
            {
                Console.WriteLine("************************" + i);
                foreach (var points in tp[i])
                {
                    Console.WriteLine(points.ToString());
                }
            }*/
            if (IsSpetialPoint)
            {
                tp = TraceModify(tp, 18);
            }
            //tp = TraceModify(tp, 18);
            return SetLinesType(tp);
        }
    }
}