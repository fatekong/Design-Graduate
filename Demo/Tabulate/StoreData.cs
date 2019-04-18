using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Demo.Tabulate
{
    class StoreData
    {
        string filename = "MyChart.json";
        public StoreData(DataTable dt, bool flag)
        {
            //非2D，flag表示是否为2D
            if(!flag)
                filename = "MyChart3D.json";
            if (dt != null)
            {
                FileInfo fi = new FileInfo(filename);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                FileStream fs = new FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                string data = "{";
                sw.WriteLine(data);
                Console.WriteLine(data);
                data = "\"Attribute\": [";
                sw.WriteLine(data);
                Console.WriteLine(data);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i != dt.Columns.Count - 1)
                        data = "\"" + dt.Columns[i].ColumnName.ToString() + "\",";
                    else
                        data = "\"" + dt.Columns[i].ColumnName.ToString() + "\"";
                    sw.WriteLine(data);
                    Console.WriteLine(data);
                    //column.Add(dt.Columns[i].ColumnName.ToString());
                }
                data = "],";
                sw.WriteLine(data);
                Console.WriteLine(data);
                data = "\"Values\": [";
                sw.WriteLine(data);
                Console.WriteLine(data);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sw.WriteLine("{");
                    Console.WriteLine("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j != dt.Columns.Count - 1)
                            data = "\"" + dt.Columns[j].ColumnName.ToString() + "\": " + dt.Rows[i][j] + ",";
                        else
                            data = "\"" + dt.Columns[j].ColumnName.ToString() + "\": " + dt.Rows[i][j];
                        sw.WriteLine(data);
                        Console.WriteLine(data);
                    }
                    if (i != dt.Rows.Count - 1)
                    {
                        sw.WriteLine("},");
                        Console.WriteLine("},");
                    }
                    else
                    {
                        sw.WriteLine("}");
                        Console.WriteLine("}");
                    }
                }
                sw.WriteLine("]");
                Console.WriteLine("]");
                sw.WriteLine("}");
                Console.WriteLine("}");
                sw.Close();
                fs.Close();
            }
        }
        public StoreData(DataTable dt)
        {
            if (dt != null)
            {
                FileInfo fi = new FileInfo(filename);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                FileStream fs = new FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                string data = "{";
                sw.WriteLine(data);
                Console.WriteLine(data);
                data = "\"Attribute\": [";
                sw.WriteLine(data);
                Console.WriteLine(data);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i != dt.Columns.Count - 1)
                        data = "\"" + dt.Columns[i].ColumnName.ToString() + "\",";
                    else
                        data = "\"" + dt.Columns[i].ColumnName.ToString() + "\"";
                    sw.WriteLine(data);
                    Console.WriteLine(data);
                    //column.Add(dt.Columns[i].ColumnName.ToString());
                }
                data = "],";
                sw.WriteLine(data);
                Console.WriteLine(data);
                data = "\"Values\": [";
                sw.WriteLine(data);
                Console.WriteLine(data);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sw.WriteLine("{");
                    Console.WriteLine("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j != dt.Columns.Count - 1)
                            data = "\"" + dt.Columns[j].ColumnName.ToString() + "\": " + dt.Rows[i][j] + ",";
                        else
                            data = "\"" + dt.Columns[j].ColumnName.ToString() + "\": " + dt.Rows[i][j];
                        sw.WriteLine(data);
                        Console.WriteLine(data);
                    }
                    if (i != dt.Rows.Count - 1)
                    {
                        sw.WriteLine("},");
                        Console.WriteLine("},");
                    }
                    else
                    {
                        sw.WriteLine("}");
                        Console.WriteLine("}");
                    }
                }
                sw.WriteLine("]");
                Console.WriteLine("]");
                sw.WriteLine("}");
                Console.WriteLine("}");
                sw.Close();
                fs.Close();
            }
        }
    }
}