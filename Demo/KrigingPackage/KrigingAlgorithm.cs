using Demo.MyKDTree;
using DotSpatial.Data;
using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Demo.KrigingPackage
{
    class KrigingAlgorithm
    {
        ProgressBar progress;
        public KrigingAlgorithm(ProgressBar progress1)
        {
            this.progress = progress1;
        }
        public bool ExecuteByParam(IFeatureSet input,string zField ,double cellSize,string model,int neighborType,int pointCount,double distance ,IRaster output,double c,double r)
        {
            if (input == null || output == null)
            {
                return false;
            }
            if (cellSize == 0)
            {
                cellSize = input.Extent.Width / 255;
            }
            int numColumns = Convert.ToInt32(Math.Round(input.Extent.Width / cellSize));
            int numRows = Convert.ToInt32(Math.Round(input.Extent.Height / cellSize));
            progress.Maximum = numColumns * numRows;
            progress.Value = 0;
            output = Raster.CreateRaster(output.Filename, string.Empty, numColumns, numRows, 1, typeof(double), new[] { string.Empty });//error
            output.CellHeight = cellSize;
            output.CellWidth = cellSize;
            output.Xllcenter = input.Extent.MinX + (cellSize / 2);
            output.Yllcenter = input.Extent.MinY + (cellSize / 2);
            List<AltitudePoint> points = new List<AltitudePoint>();
            if (neighborType == 0)
            {
                #region 构建KD树
                List<KD_Point> lists = new List<KD_Point>();//构建KD-Tree的点集列表
                foreach (var p in input.Features)
                {
                    KD_Point kd = new KD_Point(p.BasicGeometry.Coordinates[0].X, p.BasicGeometry.Coordinates[0].Y, Convert.ToDouble(p.DataRow[zField]));
                    lists.Add(kd);
                }
                KDTree kDTree = new KDTree();
                kDTree.CreateByPointList(lists);
                #endregion
                if (pointCount > 0)
                {
                    for (int i = 0; i < input.Features.Count; i++)
                    {
                        var featurePt = input.Features[i];
                        points.Add(new AltitudePoint(featurePt.BasicGeometry.Coordinates[0].X, featurePt.BasicGeometry.Coordinates[0].Y, Convert.ToDouble(featurePt.DataRow[zField])));
                    }
                    ForRasterData forRasterData = new ForRasterData(points, model);
                    for (int x = 0; x < numColumns; x++)
                    {
                        for (int y = 0; y < numRows; y++)
                        {
                            points.Clear();
                            Coordinate coord = output.CellToProj(y, x);
                            KD_Point kD_Point = new KD_Point(coord.X, coord.Y);
                            List<KD_Point> kdpoints = kDTree.K_Nearest(kD_Point, pointCount);
                            foreach (var p in kdpoints)
                            {
                                points.Add(new AltitudePoint(p.X, p.Y, p.Z));
                            }
                            forRasterData.ReSetPointList(points);
                            if (!forRasterData.IsPointsOK())
                                return false;
                            output.Value[y, x] = forRasterData.GetValue(coord.X, coord.Y);
                            progress.Value++;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < input.Features.Count; i++)
                    {
                        var featurePt = input.Features[i];
                        points.Add(new AltitudePoint(featurePt.BasicGeometry.Coordinates[0].X, featurePt.BasicGeometry.Coordinates[0].Y, Convert.ToDouble(featurePt.DataRow[zField])));
                    }
                    ForRasterData forRasterData = new ForRasterData(points,model,c,r);
                    if (!forRasterData.IsPointsOK())
                        return false;
                    for (int x = 0; x < numColumns; x++)
                    {
                        for (int y = 0; y < numRows; y++)
                        {
                            Coordinate coordinate = output.CellToProj(y, x);
                            output.Value[y, x] = forRasterData.GetValue(coordinate.X, coordinate.Y);
                            progress.Value++;
                        }
                    }
                }
            }
            output.Save();
            return true;
        }
        public bool Execute(IFeatureSet input, string zField, double cellSize, string model, int neighborType, int pointCount, double distance, IRaster output)
        {
            if (input == null || output == null)
            {
                return false;
            }
            if (cellSize == 0)
            {
                cellSize = input.Extent.Width / 255;
            }
            int numColumns = Convert.ToInt32(Math.Round(input.Extent.Width / cellSize));
            int numRows = Convert.ToInt32(Math.Round(input.Extent.Height / cellSize));
            progress.Maximum = numColumns * numRows;
            progress.Value = 0;
            output = Raster.CreateRaster(output.Filename, string.Empty, numColumns, numRows, 1, typeof(double), new[] { string.Empty });//error
            output.CellHeight = cellSize;
            output.CellWidth = cellSize;
            output.Xllcenter = input.Extent.MinX + (cellSize / 2);
            output.Yllcenter = input.Extent.MinY + (cellSize / 2);
            List<AltitudePoint> points = new List<AltitudePoint>();
            if(neighborType == 0)
            {
                #region 构建KD树
                List<KD_Point> lists = new List<KD_Point>();//构建KD-Tree的点集列表
                foreach (var p in input.Features)
                {
                    KD_Point kd = new KD_Point(p.BasicGeometry.Coordinates[0].X, p.BasicGeometry.Coordinates[0].Y, Convert.ToDouble(p.DataRow[zField]));
                    lists.Add(kd);
                }
                KDTree kDTree = new KDTree();
                kDTree.CreateByPointList(lists);
                #endregion
                if (pointCount > 0)
                {
                    for (int i = 0; i < input.Features.Count; i++)
                    {
                        var featurePt = input.Features[i];
                        points.Add(new AltitudePoint(featurePt.BasicGeometry.Coordinates[0].X, featurePt.BasicGeometry.Coordinates[0].Y, Convert.ToDouble(featurePt.DataRow[zField])));
                    }
                    ForRasterData forRasterData = new ForRasterData(points, model);
                    for (int x = 0; x < numColumns; x++)
                    {
                        for (int y = 0; y < numRows; y++)
                        {
                            points.Clear();
                            Coordinate coord = output.CellToProj(y, x);

                            KD_Point kD_Point = new KD_Point(coord.X, coord.Y);
                            List<KD_Point> kdpoints = kDTree.K_Nearest(kD_Point, pointCount);
                            foreach (var p in kdpoints)
                            {
                                points.Add(new AltitudePoint(p.X, p.Y, p.Z));
                            }
                            forRasterData.ReSetPointList(points);
                            /*KD_Point kD_Point = new KD_Point(coord.X, coord.Y);
                            List<KD_Point> kdpoints = kDTree.K_Nearest(kD_Point, pointCount);
                            foreach(var p in kdpoints)
                            {
                                points.Add(new AltitudePoint(p.X, p.Y, p.Z));
                            }
                            ForRasterData forRasterData = new ForRasterData(points,model);*/
                            if (!forRasterData.IsPointsOK())
                                return false;
                            output.Value[y, x] = forRasterData.GetValue(coord.X, coord.Y);
                            progress.Value++;
                        }
                    }
                    
                }
                else
                {
                    for (int i = 0; i < input.Features.Count; i++)
                    {
                        var featurePt = input.Features[i];
                        points.Add(new AltitudePoint(featurePt.BasicGeometry.Coordinates[0].X, featurePt.BasicGeometry.Coordinates[0].Y, Convert.ToDouble(featurePt.DataRow[zField])));
                    }
                    ForRasterData forRasterData = new ForRasterData(points,model);
                    if (!forRasterData.IsPointsOK())
                    {
                        Console.WriteLine("ForRasterData faild!");
                        return false;
                    }
                        
                    for (int x = 0; x < numColumns; x++)
                    {
                        for (int y = 0; y < numRows; y++)
                        {
                            Coordinate coordinate = output.CellToProj(y, x);
                            output.Value[y, x] = forRasterData.GetValue(coordinate.X, coordinate.Y);
                            progress.Value++;
                        }
                    }
                }
            }
            output.Save();
            return true;
        }
    }
}
