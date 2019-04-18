using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Demo.RasterLinePackage
{
    class CreateRasterLine
    {
        private double maxvalue = 0;
        private double minvalue = 0;
        IRaster raster;
        IMap map;
        public Dictionary<double ,List<MyLine>> contourData { private set;get; }
        public CreateRasterLine(IRaster input,IMap map1)
        {
            this.map = map1;
            this.raster = input;
            this.maxvalue = this.minvalue = input.Value[0, 0];
            for(int i = 0; i < input.NumColumns; i++)
            {
                for(int j = 0; j < input.NumRows; j++)
                {
                    if(minvalue > input.Value[j, i])
                    {
                        this.minvalue = input.Value[j, i];
                    }
                    if(maxvalue < input.Value[j, i])
                    {
                        this.maxvalue = input.Value[j, i];
                    }
                }
            }
        }
        public bool Execute(double start,double space,out FeatureSet lines)//输入栅格文件，起始值，间距
        {
            if (start < this.minvalue)
            {
                start = this.minvalue;
            }
            contourData = new Dictionary<double, List<MyLine>>();
            for(double value = start;value<maxvalue;value += space)
            {
                contourData.Add(value, new List<MyLine>());
            }
            int length = 1;
            for(int i =0;i< raster.NumRows - length; i++)
            {
                for(int j=0;j<raster.NumColumns - length; j++)
                {
                    foreach(var contour in contourData)
                    {
                        switch (GetBinaryIndex(contour.Key, j, i))
                        {
                            //第一 二行
                            case "0001":
                            case "1110":
                                contour.Value.Add(
                                    new MyLine(CalExactPosition(contour.Key, j, i, j, i + length),
                                    CalExactPosition(contour.Key, j + length, i + length, j, i + length)));
                                break;
                            case "0010":
                            case "1101":
                                contour.Value.Add(
                                    new MyLine(CalExactPosition(contour.Key, j + length, i, j + length, i + length),
                                    CalExactPosition(contour.Key, j + length, i + length, j, i + length)));
                                break;
                            case "0100":
                            case "1011":
                                contour.Value.Add(
                                    new MyLine(CalExactPosition(contour.Key, j, i, j + length, i),
                                    CalExactPosition(contour.Key, j + length, i + length, j + length, i)));
                                break;
                            case "1000":
                            case "0111":
                                contour.Value.Add(
                                    new MyLine(CalExactPosition(contour.Key, j, i, j + length, i),
                                    CalExactPosition(contour.Key, j, i, j, i + length)));
                                break;

                                //第三行
                            case "0011":
                            case "1100":
                                contour.Value.Add(
                                    new MyLine(CalExactPosition(contour.Key, j, i, j, i + length),
                                    CalExactPosition(contour.Key, j + length, i, j + length, i + length)));
                                break;
                            case "0110":
                            case "1001":
                                contour.Value.Add(
                                    new MyLine(CalExactPosition(contour.Key, j, i, j + length, i),
                                    CalExactPosition(contour.Key, j + length, i + length, j, i + length)));
                                break;
                        }
                    }
                }
            }
            lines = DrawLine();
            return true;
        }

        public FeatureSet DrawLine()
        {
            FeatureSet lineF = new FeatureSet(FeatureType.Line);
            lineF.Projection = map.Projection;
            lineF.DataTable.Columns.Add("Value", typeof(double));//方便之后在等值线上标注
            MapLineLayer lineLayer = default(MapLineLayer);
            lineLayer = (MapLineLayer)map.Layers.Add(lineF);
            LineSymbolizer symnol = new LineSymbolizer(Color.Black,2);
            lineLayer.Symbolizer = symnol;
            string[] thename = raster.Filename.Split('\\');
            lineLayer.LegendText = thename[thename.Length-1] + "_line";
            //MapLabelLayer
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
            lineLayer.ShowLabels = true;
            lineLayer.LabelLayer = labelLayer;
            /*double min_X, min_Y, max_X, max_Y;
            min_X = raster.Xllcenter - raster.CellWidth / 2;
            min_Y = raster.Yllcenter - raster.CellHeight / 2;
            max_X = raster.CellToProj(0, raster.NumColumns-1).X + raster.CellWidth / 2;
            max_Y = raster.CellToProj(0, 0).Y + raster.CellHeight/2;
            double interspace = (raster.CellToProj(1, 1).X - raster.CellToProj(0, 0).X) *
                (raster.CellToProj(1, 1).X - raster.CellToProj(0, 0).X) + (raster.CellToProj(1, 1).Y - raster.CellToProj(0, 0).Y) *
                (raster.CellToProj(1, 1).Y - raster.CellToProj(0, 0).Y);
            Console.WriteLine("minX: " + min_X + " , minY: "+ min_Y + " , maxX: " + max_X + " , maxY: " + max_Y);*/
            List<Tin_Point> lpoints = new List<Tin_Point>();
            foreach (var lines in contourData)
            {
                //if(lines.Key == 18)
                //{
                    //int i = 0;
                    foreach (var line in lines.Value)
                    {

                        Tin_Point p1 = new Tin_Point(line.startPoint.X, line.startPoint.Y,lines.Key);
                        Tin_Point p2 = new Tin_Point(line.endPoint.X, line.endPoint.Y,lines.Key);
                        //p1.Type = i;
                        //p2.Type = i;
                        lpoints.Add(p1);
                        lpoints.Add(p2);
                        List<Coordinate> lineArray = new List<Coordinate>();
                        LineString lineGeometry = new LineString(lineArray);
                        IFeature lineFeature = lineF.AddFeature(lineGeometry);
                        lineFeature.Coordinates.Add(line.startPoint);
                        lineFeature.Coordinates.Add(line.endPoint);
                        lineFeature.DataRow["Value"] = lines.Key;
                        lineF.InitializeVertices();
                        //i++;
                    }
                //}
                
            }
            /*
            Polish polish = new Polish();
            List<List<Tin_Point>> new_tin_points = polish.ClassifyLine(lpoints);
            Console.WriteLine("new_tin_point num: " + new_tin_points.Count);
            foreach(var lines in new_tin_points)
            {
                //int i = 0;
                foreach(var p in lines)
                {
                    Console.Write(p.X + " , " + p.Y + "||");
                }
                Console.WriteLine("*********************************");
            }
            foreach (var lines in new_tin_points)
            {
                //int i = 0;

                 Console.WriteLine(lines[0].X + " , " + lines[0].Y + "||" + lines[lines.Count-1].X + " , " + lines[lines.Count - 1].Y);
            }


            foreach (var freeline in new_tin_points)
            {
                List<Coordinate> lineArray = new List<Coordinate>(); ;
                ILineString lineGeometry = new LineString(lineArray);
                IFeature lineFeature = lineF.AddFeature(lineGeometry);
                lineFeature.DataRow["Value"] = (int)freeline[0].Value;
                foreach (var p in freeline)
                {
                    Coordinate coordinate = new Coordinate(p.X, p.Y);
                    lineArray.Add(coordinate);
                    lineFeature.Coordinates.Add(coordinate);
                    lineF.InitializeVertices();
                }
            }   
            */
            map.ResetBuffer();
            return lineF;
        }

        public string GetBinaryIndex(double value,int X,int Y)
        {
            string result = "";
            result += raster.Value[Y, X] >= value ? "1" : "0";
            result += raster.Value[Y, X+1] >= value ? "1" : "0";
            result += raster.Value[Y + 1, X + 1] >= value ? "1" : "0";
            result += raster.Value[Y+1, X] >= value ? "1" : "0";
            return result;
        }

        private Coordinate CalExactPosition(double value,int startX,int startY,int endX,int endY)
        {
            double radio = ((value - raster.Value[startY, startX]) / (raster.Value[endY, endX] - raster.Value[startY, startX]));
            double cellsize = raster.CellHeight;
            double S_X = raster.CellToProj(startY, startX).X;
            double S_Y = raster.CellToProj(startY, startX).Y;
            double E_X = raster.CellToProj(endY, endX).X;
            double E_Y = raster.CellToProj(endY, endX).Y;
            return new Coordinate(S_X + radio * (E_X - S_X), S_Y + radio * (E_Y - S_Y));
        }
        public class MyLine
        {
            public Coordinate startPoint;
            public Coordinate endPoint;

            public MyLine(Coordinate start,Coordinate end)
            {
                this.startPoint = start;
                this.endPoint = end;
            }
        }
    }
}
