using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Demo.ISOLinePackage
{
    class CreateIsoLine
    {
        Map map;
        FeatureSet LineF;
        public CreateIsoLine(Map map1)
        {
            this.map = map1;
            LineF = new FeatureSet(FeatureType.Line);
            LineF.Projection = map.Projection;
        }
        /*public bool Execute(double start ,double space) {
            LineF.DataTable.Columns.Add("Value", typeof(double));
            MapLineLayer ml = (MapLineLayer)map.Layers.Add(LineF);
            /*MapLabelLayer labelLayer = new MapLabelLayer();
            ILabelCategory category = labelLayer.Symbology.Categories[0];
            category.Expression = "[Value]";
            category.SelectionSymbolizer.BackColorEnabled = true;
            category.Symbolizer.BorderVisible = false;
            category.Symbolizer.BackColor = Color.FromArgb(128, Color.LightBlue); ;
            category.Symbolizer.FontStyle = FontStyle.Regular;
            category.Symbolizer.FontColor = Color.Black;
            category.Symbolizer.FontSize = 8.5f;
            category.Symbolizer.Orientation = ContentAlignment.MiddleCenter;
            category.Symbolizer.Alignment = StringAlignment.Center;
            ml.ShowLabels = true;
            ml.LabelLayer = labelLayer;
            LineSymbolizer symbol = new LineSymbolizer(Color.Black, 2);
            ml.Symbolizer = symbol;
            ml.LegendText = "test_isoLine";

            List<Tin_Point> tin_Points = new List<Tin_Point>();
            tin_Points.Add(new Tin_Point(572561.497857, 3078751.00159, 11.8, 1));
            tin_Points.Add(new Tin_Point(970365.002389, 3031211.249, 12.3, 2));
            tin_Points.Add(new Tin_Point(626125.750737, 2974587.498, 22, 3));
            tin_Points.Add(new Tin_Point(1011572.07339, 2913623.49696, 21, 4));
            tin_Points.Add(new Tin_Point(922526.062753, 2894449.74788, 19, 5));
            tin_Points.Add(new Tin_Point(491210.876499, 2860727.9972, 17.7, 6));
            tin_Points.Add(new Tin_Point(986676.942363, 2828322.24684, 16, 7));
            tin_Points.Add(new Tin_Point(628325.875511, 2828106.99668, 23, 8));
            tin_Points.Add(new Tin_Point(518668.440134, 2778797.99885, 22, 9));
            tin_Points.Add(new Tin_Point(878961.377011, 2777357.74535, 25, 10));
            tin_Points.Add(new Tin_Point(759651.063273, 2771206.25014, 13.6, 11));
            tin_Points.Add(new Tin_Point(460062.560062, 2705433.24783, 18, 12));//18
            tin_Points.Add(new Tin_Point(862951.066087, 2697174.99834, 24, 13));
            tin_Points.Add(new Tin_Point(408326.283205, 2666913.24635, 16, 14));
            tin_Points.Add(new Tin_Point(384889.188632, 2658932.49923, 23, 15));
            tin_Points.Add(new Tin_Point(610997.4361, 2641258.00046, 21, 16));
            tin_Points.Add(new Tin_Point(1042075.00856, 2595949.24905, 18, 17));
            tin_Points.Add(new Tin_Point(929238.438265, 2590020.00052, 19, 18));
            tin_Points.Add(new Tin_Point(708181.56137, 2524888.74999, 22.3, 19));
            tin_Points.Add(new Tin_Point(685935.251121, 2437299.24516, 18, 20));
            Delaunay delaunay = new Delaunay();
            List<Triangle> triangles = new List<Triangle>();
            triangles = delaunay.ConstructionDelaunay(tin_Points);
            Trace trace = new Trace();
            List<List<Tin_Point>> line_list = new List<List<Tin_Point>>();//多条等值线
            line_list = trace.MyTrace(triangles, tin_Points, space, start);
            PolishLine pl = new PolishLine(map, LineF, trace);
            Console.WriteLine("等值线的条数为:" + line_list.Count);
            foreach (var lines in line_list)
            {
                if (lines == null)
                    break;
                //if(lines[0].Value)
                List<List<Tin_Point>> tp = null;
                if (lines[0].Value - (int)lines[0].Value > 0)//value为特殊值，后面有小数
                {
                    tp = pl.ClassifyLine(lines, true);
                }
                else
                {
                    tp = pl.ClassifyLine(lines, false);
                }
                foreach (var p in tp)
                {
                    pl.SelectLine(p);
                }
            }
            map.ResetBuffer();
            return true;
            return true;
        }*/
        public bool Execute(IFeatureSet input , String zField ,double start,double space)
        {
            LineF.DataTable.Columns.Add("Value", typeof(double));
            MapLineLayer ml = (MapLineLayer)map.Layers.Add(LineF);
            /*
            MapLabelLayer labelLayer = new MapLabelLayer();
            ILabelCategory category = labelLayer.Symbology.Categories[0];
            category.Expression = "[Value]";
            category.SelectionSymbolizer.BackColorEnabled = true;
            category.Symbolizer.BorderVisible = false;
            category.Symbolizer.BackColor = Color.FromArgb(128, Color.LightBlue); ;
            category.Symbolizer.FontStyle = FontStyle.Regular;
            category.Symbolizer.FontColor = Color.Black;
            category.Symbolizer.FontSize = 8.5f;
            category.Symbolizer.Orientation = ContentAlignment.MiddleCenter;
            category.Symbolizer.Alignment = StringAlignment.Center;
            ml.ShowLabels = true;
            ml.LabelLayer = labelLayer;
            LineSymbolizer symbol = new LineSymbolizer(Color.Black, 2);
            ml.Symbolizer = symbol;
            */
            string[] inputname = input.Filename.Split('\\');
            inputname = inputname[inputname.Length - 1].Split('.');
            ml.LegendText = inputname[0] + "_isoLine";

            List<Tin_Point> tin_Points = new List<Tin_Point>();
            for(int i = 0; i < input.Features.Count; i++)
            {
                tin_Points.Add(new Tin_Point(input.Features[i].BasicGeometry.Coordinates[0].X, input.Features[i].BasicGeometry.Coordinates[0].Y, Convert.ToDouble(input.Features[i].DataRow[zField]),i));
            }
            Delaunay delaunay = new Delaunay();
            List<Triangle> triangles = new List<Triangle>();
            triangles = delaunay.ConstructionDelaunay(tin_Points);
            Trace trace = new Trace();
            List<List<Tin_Point>> line_list = new List<List<Tin_Point>>();//多条等值线
            line_list = trace.MyTrace(triangles, tin_Points, space, start);
            PolishLine pl = new PolishLine(map, LineF, trace);
            Console.WriteLine("等值线的条数为:" + line_list.Count);
            foreach (var lines in line_list)
            {
                if (lines == null)
                    break;
                //if(lines[0].Value)
                List<List<Tin_Point>> tp = null;
                if (lines[0].Value - (int)lines[0].Value > 0)//value为特殊值，后面有小数
                {
                    tp = pl.ClassifyLine(lines, true);
                }
                else
                {
                    tp = pl.ClassifyLine(lines, false);
                }
                foreach (var p in tp)
                {
                    pl.SelectLine(p);
                }
            }
            map.ResetBuffer();
            return true;
        }
    }
}