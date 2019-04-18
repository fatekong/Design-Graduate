using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Data;
using System.Reflection;
using System.Windows.Forms;
using DotSpatial.Topology;
using Demo.MyKDTree;

namespace Demo
{
    class InverseDistanceWeight
    {
        ProgressBar progress;
        //RichTextBox richText;
        //MyProgressBar mpb;
        public InverseDistanceWeight(ProgressBar progressBar)
        {
            /*this.richText = my.GetRichTextBox();
            this.progress = my.GetProgress();
            this.mpb = my;*/
            this.progress = progressBar;
        }
        public bool Execute(IFeatureSet input,string zField,double cellSize,double power,int neighborType,int pointCount,double distance,IRaster output)
        {
            if(input == null || output == null)
            {
                return false;
            }
            if(cellSize == 0)
            {
                cellSize = input.Extent.Width / 255;
            }
            int numColumns = Convert.ToInt32(Math.Round(input.Extent.Width / cellSize));
            int numRows = Convert.ToInt32(Math.Round(input.Extent.Height / cellSize));
            output = Raster.CreateRaster(output.Filename, string.Empty, numColumns, numRows, 1, typeof(double), new[] { string.Empty });//error
            output.CellHeight = cellSize;
            output.CellWidth = cellSize;
            output.Xllcenter = input.Extent.MinX + (cellSize / 2);
            output.Yllcenter = input.Extent.MinY + (cellSize / 2);
            progress.Maximum = numColumns * numRows;
            progress.Value = 0;
            #region 构建KD树
            List<KD_Point> lists = new List<KD_Point>();//构建KD-Tree的点集列表
            foreach(var p in input.Features)
            {
                KD_Point kd = new KD_Point(p.BasicGeometry.Coordinates[0].X,p.BasicGeometry.Coordinates[0].Y,Convert.ToDouble(p.DataRow[zField]));
                lists.Add(kd);
            }
            KDTree kDTree = new KDTree();
            kDTree.CreateByPointList(lists);
            /*
            double gdistance = input.Features[0].BasicGeometry.Coordinates[0].Distance(input.Features[1].BasicGeometry.Coordinates[0]);
            double tdistance = Math.Sqrt(Math.Pow(input.Features[0].BasicGeometry.Coordinates[0].X- input.Features[1].BasicGeometry.Coordinates[0].X, 2) + Math.Pow(input.Features[0].BasicGeometry.Coordinates[0].Y- input.Features[1].BasicGeometry.Coordinates[0].Y, 2));
            Console.WriteLine("gdistance: " + gdistance + " , tdistance: " + tdistance);
            */
            #endregion
            if (neighborType == 0)//固定数目
            {
                for(int x = 0; x < numColumns; x++)
                {
                    for(int y = 0; y < numRows; y++)
                    {
                        //IDW算法的分子和分母
                        double top = 0;
                        double bottom = 0;
                        if (pointCount > 0)
                        {
                            Coordinate coord = output.CellToProj(y, x);
                            KD_Point kD_Point = new KD_Point(coord.X, coord.Y);
                            List<KD_Point> points = kDTree.K_Nearest(kD_Point, pointCount);
                            //Console.WriteLine("KDTree points count: " + points.Count);
                            for(int i = 0; i < points.Count; i++)
                            {
                                if (points[i] != null)
                                {
                                    Coordinate kd = new Coordinate(points[i].X,points[i].Y);
                                    double distanceToCell = kd.Distance(coord);
                                    if (distanceToCell <= distance || distance == 0)
                                    {
                                        //Console.WriteLine(points[i].Z);
                                        if (power == 2)
                                        {
                                            top += (1 / (distanceToCell * distanceToCell)) * points[i].Z;
                                            bottom += 1 / (distanceToCell * distanceToCell);
                                        }
                                        else
                                        {
                                            top += (1 / Math.Pow(distanceToCell, power)) * points[i].Z;
                                            bottom += 1 / Math.Pow(distanceToCell, power);
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            for (int i = 0; i < input.Features.Count; i++)
                            {
                                Coordinate cellCenter = output.CellToProj(y, x);
                                //Coordinate coord = output.CellToProj(y, x);
                                var featurePt = input.Features[i];
                                if (featurePt != null)
                                {

                                    double distanceToCell = cellCenter.Distance(featurePt.BasicGeometry.Coordinates[0]);
                                    if (distanceToCell <= distance || distance == 0)
                                    {
                                        try
                                        {
                                            Convert.ToDouble(featurePt.DataRow[zField]);
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        if (power == 2)
                                        {
                                            top += (1 / (distanceToCell * distanceToCell)) * Convert.ToDouble(featurePt.DataRow[zField]);
                                            bottom += 1 / (distanceToCell * distanceToCell);
                                        }
                                        else
                                        {
                                            top += (1 / Math.Pow(distanceToCell, power)) * Convert.ToDouble(featurePt.DataRow[zField]);
                                            bottom += 1 / Math.Pow(distanceToCell, power);
                                        }
                                    }
                                }
                            }
                        }
                        //Console.WriteLine("top: " + top + " , bottom: " +bottom);
                        output.Value[y, x] = top / bottom;
                        //Console.WriteLine(y + " , " + x + " : " + output.Value[y, x]);
                        //richText.Text += output.Value[y, x] + "\n";
                        progress.Value++;
                    }
                }
            }
            output.Save();
            return true;
        }
        private string saveFile()
        {
            string localFilePath = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Raster文件(*.adf,*.bgd,*tif,*tiff) |*.bgd";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString();//get the path
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
            }
            return localFilePath;
        }
    }

    /*class KdTreeEx<T> : KdTree<T>
        where T : class
    {
        private readonly MethodInfo _findBestMatchNodeMethod;
        #region  Constructors
        public KdTreeEx()
        {
            _findBestMatchNodeMethod = typeof(KdTree<T>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(_ => _.Name == "FindBestMatchNode");
        }

        public bool MethodFindBestMatchNodeFound => _findBestMatchNodeMethod != null;
        #endregion

        #region Methods
        public T Search(GeoAPI.Geometries.Coordinate coord)
        {
            var node = (KdNode<T>)_findBestMatchNodeMethod.Invoke(this, new object[] { coord });
            return node?.Data;
        }
        #endregion
    }*/
}