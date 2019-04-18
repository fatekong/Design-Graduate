using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.RasterLinePackage
{
    class Polish
    {
        public List<List<Tin_Point>> ClassifyLine(List<Tin_Point> lines)//将等值线进行合并归类。
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
                            newone.Add(tp[i][k]);
                        }
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
            return SetLinesType(tp);
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
    }
}
