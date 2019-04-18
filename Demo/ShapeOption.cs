using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Controls;
using System.Windows.Forms;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Topology;
using System.Data;
using System.Drawing;

//using GeoAPI.Geometries;
//using NetTopologySuite.Geometries;

namespace Demo
{
    class ShapeOption
    {
        private string shapeType = null;
        FeatureSet pointF = null;
        int pointID = 0;
        bool pointmouseClick = false;
        MapLineLayer lineLayer = default(MapLineLayer);
        FeatureSet lineF = null;
        int lineID = 0;
        bool firstClick = false;
        FeatureSet polygonF = null;
        int polygonID = 0;
        Map map = null;
        public ShapeOption(Map map1)
        {
            this.map = map1;
            //this.cursor = cursor1;
        }
        public void createPointShape()
        {
            //map.Cursor = cursor
            //map.Cursor = Cursors.Cross;
            //set the shape type to the classlevel string variable
            //we are going to use this variable in select case statement
            shapeType = "Point";
            pointF = new FeatureSet(FeatureType.Point);
            //set projection
            pointF.Projection = map.Projection;
            //initialize the featureSet attribute table
            DataColumn column = new DataColumn("ID");
            pointF.DataTable.Columns.Add(column);
            //add the featureSet as map layer
            MapPointLayer pointLayer = (MapPointLayer)map.Layers.Add(pointF);
            //Create a new symbolizer
            PointSymbolizer symbol = new PointSymbolizer(Color.Red, DotSpatial.Symbology.PointShape.Ellipse, 3);
            //Set the symbolizer to the point layer
            pointLayer.Symbolizer = symbol;
            //Set the legendText as point
            pointLayer.LegendText = "My Point";
            //Set left mouse click as true
            pointmouseClick = true;
        }
        public void MouseDown(object sender,MouseEventArgs e)
        {
            switch (shapeType)
            {
                case "Point":
                    if(e.Button == MouseButtons.Left)
                    {
                        if (pointmouseClick)
                        {
                            //This method is used to convert the screen coordinate to map coordinate
                            //e.location is the mouse click point on the map control
                            Coordinate coord = map.PixelToProj(e.Location);
                            //Create a new point
                            //Input parameter is clicked point coordinate
                            DotSpatial.Topology.Point point = new DotSpatial.Topology.Point(coord);
                            //Add the point into the Point Feature
                            //assigning the point feature to IFeature because via it only we can set the attributes.
                            IFeature currentFeature = pointF.AddFeature(point);
                            //increase the point id
                            pointID = pointID + 1;
                            //set the ID attribute
                            currentFeature.DataRow["ID"] = pointID;
                            //refresh the map
                            map.ResetBuffer();
                        }
                    }
                    else
                    {
                        map.Cursor = Cursors.Default;
                        pointmouseClick = false;
                    }
                    break;
                case "Line":
                    if(e.Button == MouseButtons.Left)
                    {
                        //left click - fill array of coordinates
                        //coordinate of clicked point
                        Coordinate coord = map.PixelToProj(e.Location);
                        //first time left click - create empty line feature
                        if(firstClick)
                        {
                            //Create a new List called lineArray.
                            //In List we need not define the size and also 
                            //Here this list will store the Coordinates
                            //We are going to store the mouse click coordinates into this array.
                            List<Coordinate> lineArray = new List<Coordinate>();
                            //Create an instance for LineString class.
                            //We need to pass collection of list coordinates
                            LineString lineGeometry = new LineString(lineArray.ToArray());
                            FeatureSet fs = new FeatureSet();
                            //Add the linegeometry to line feature
                            //IFeature lineFeature = lineF.AddFeature(lineGeometry);
                            fs.Features.Add(new Feature(lineGeometry));
                            //add first coordinate to the line feature
                            //lineFeature.Coordinates.Add(coord);
                            
                            //set the line feature attribute
                            lineID = lineID + 1;
                            fs.Features[0].DataRow["ID"] = lineID;
                            //lineFeature.DataRow["ID"] = lineID;
                            firstClick = false;
                        }
                        else
                        {
                            //second or more clicks - add points to the existing feature
                            IFeature existingFeature = lineF.Features[lineF.Features.Count - 1];
                            
                            existingFeature.Coordinates.Add(coord);
                            //refresh the map if line has 2 or more points
                            if (existingFeature.Coordinates.Count >= 2)
                            {
                                lineF.InitializeVertices();
                                map.ResetBuffer();
                            }
                        }
                    }
                    else
                    {
                        //right click - reset first mouse click
                        firstClick = true;
                        map.Cursor = Cursors.Default;
                        map.ResetBuffer();
                    }
                    break;
                case "Polygon":
                    if (e.Button == MouseButtons.Left)
                    {
                        //left click - fill array of coordinates
                        Coordinate coord = map.PixelToProj(e.Location);
                        //first time left click - create empty line feature
                        if (firstClick)
                        {
                            //Create a new List called polygonArray.
                            //Here this list will store the Coordinates
                            //We are going to store the mouse click coordinates into this array.
                            List<Coordinate> polygonArray = new List<Coordinate>();
                            //Create an instance for LinearRing class.
                            //We pass the polygon List to the constructor of this class
                            LinearRing polygonGeometry = new LinearRing(polygonArray.ToArray());
                            //Add the polygonGeometry instance to PolygonFeature
                            IFeature polygonFeature = polygonF.AddFeature(polygonGeometry);
                            //add first coordinate to the polygon feature
                            polygonFeature.Coordinates.Add(coord);
                            //set the polygon feature attribute
                            polygonID = polygonID + 1;
                            polygonFeature.DataRow["ID"] = polygonID;
                            firstClick = false;
                        }
                        else
                        {
                            //second or more clicks - add points to the existing feature
                            if(polygonF.Features.Count > 0)
                            {
                                IFeature existingFeature = (IFeature)polygonF.Features[polygonF.Features.Count - 1];
                                existingFeature.Coordinates.Add(coord);
                                //refresh the map if line has 2 or more points
                                if (existingFeature.Coordinates.Count >= 3)
                                {
                                    //refresh the map
                                    polygonF.InitializeVertices();
                                    map.ResetBuffer();
                                }
                            }
                            else
                            {
                                createPolygonShape();
                                break;
                            }
                            
                        }
                    }
                    else
                    {
                        //right click - reset first mouse click
                        map.Cursor = Cursors.Default;
                        firstClick = true;
                    }
                    break;
            }
        }
        public void savaPointShape()
        {
            if(pointF == null)
            {
                MessageBox.Show("There is no point shape file.");
                
            }
            else
            {
                string filename = saveFile();
                pointF.SaveAs(filename, true);
                MessageBox.Show("The point shapefile has been saved.");
            }
            map.Cursor = Cursors.Arrow;
        }

        public void createPolyLineShape()
        {
            //initialize polyline feature set
            //map.Cursor = Cursors.Cross;
            lineF = new FeatureSet(FeatureType.Line);
            //set shape type
            shapeType = "Line";
            //set projection
            lineF.Projection = map.Projection;
            //initialize the featureSet attributr table
            DataColumn column = new DataColumn("ID");
            lineF.DataTable.Columns.Add(column);
            //add the featureSet as map layer
            lineLayer = (MapLineLayer)map.Layers.Add(lineF);
            LineSymbolizer symbol = new LineSymbolizer(Color.Black, 3);
            lineLayer.Symbolizer = symbol;
            lineLayer.LegendText = "My Line";
            firstClick = true;
        }

         public void savaPolylineShape()
        {
            if (lineF == null)
            {
                MessageBox.Show("There is no line shape file.");

            }
            else
            {
                string filename = saveFile();
                lineF.SaveAs(filename, true);
                MessageBox.Show("The line shapefile has been saved.");
            }
            map.Cursor = Cursors.Arrow;
        }

        public void createPolygonShape()
        {
            //initialize polyline feature set
            //map.Cursor = Cursors.Cross;
            polygonF = new FeatureSet(FeatureType.Polygon);
            //set shape type
            shapeType = "Polygon";
            //set projection
            polygonF.Projection = map.Projection;
            //initialize the featureSet attribute table
            DataColumn column = new DataColumn("ID");
            polygonF.DataTable.Columns.Add(column);
            //add the featureSet as map layer
            MapPolygonLayer polygonLayer = (MapPolygonLayer)map.Layers.Add(polygonF);
            PolygonSymbolizer symbol = new PolygonSymbolizer(Color.Green);
            polygonLayer.Symbolizer = symbol;
            polygonLayer.LegendText = "polygon";
            firstClick = true;
        }

        public void savePolygonShape()
        {
            if(polygonF == null)
            {
                MessageBox.Show("There is no polygon shape file.");
            }
            else
            {
                string filename = saveFile();
                polygonF.SaveAs(filename, true);
                MessageBox.Show("The polygon shapefile has been saved.");
            }
            map.Cursor = Cursors.Arrow;
        }

        private string saveFile()
        {
            string localFilePath = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Shp文件(*.shp) |*.shp";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString();//get the path
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
            }
            return localFilePath;
        }
    }
}
