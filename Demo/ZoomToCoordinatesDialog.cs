using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Projections;

namespace Demo
{
    public partial class ZoomToCoordinatesDialog : Form
    {
        private const string RegExpression = "(-?\\d{1,3})[\\.\\,°]{0,1}\\s*(\\d{0,2})[\\.\\,\']{0,1}\\s*(\\d*)[\\.\\,°]{0,1}\\s*([NSnsEeWw]?)";
        private readonly double[] _lat;
        private readonly double[] _lon;

        private readonly IMap _map;
        public ZoomToCoordinatesDialog(IMap map)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));
            _map = map;
            InitializeComponent();

            lonStatus.Text = string.Empty;
            latStatus.Text = string.Empty;
            _lat = new double[3];
            _lon = new double[3];
        }
        
        private static double LoadCoordinates(IList<double> values)
        {
            var coor = values[2] / 100;
            coor += values[1];
            coor = coor / 100;
            coor += Math.Abs(values[0]);
            if (values[0] < 0)
            {
                coor *= 1;
            }
            return coor;
        }

        private static bool ParseCoordinates(IList<double> values,string text)
        {
            var match = Regex.Match(text, RegExpression);
            var groups = match.Groups;
            try
            {
                values[0] = double.Parse(groups[2].ToString());
                if (groups[2].Length == 1)
                {

                    values[1] *= 10;
                    if (groups[2].Length == 1)
                        values[1] *= 10;
                }
                if(groups[3].Length > 0)
                {
                    values[2] = double.Parse(groups[2].ToString());
                    if (groups[3].Length == 1)
                        values[2] *= 10;
                }
            }
            catch
            {
                return false;
            }

            if((groups[4].ToString().Equals("S", StringComparison.OrdinalIgnoreCase) || groups[4].ToString().Equals("W", StringComparison.OrdinalIgnoreCase)) && values[0] > 0)
            {
                values[0] *= -1;
            }
            return true;

        }
        private void AcceptButtonClick(object sender, EventArgs e)
        {
            if (!CheckCoordinates()) return;
            var latCoor = LoadCoordinates(_lat);
            var lonCoor = LoadCoordinates(_lon);

            // Now convert from Lat-Long to x,y coordinates that App.Map.ViewExtents can use to pan to the correct location.
            var xy = LatLonReproject(lonCoor, latCoor);

            // Get extent where center is desired X,Y coordinate.
            var width = _map.ViewExtents.Width;
            var height = _map.ViewExtents.Height;
            _map.ViewExtents.X = xy[0] - (width / 2);
            _map.ViewExtents.Y = xy[1] + (height / 2);
            var ex = _map.ViewExtents;

            // Set App.Map.ViewExtents to new extent that centers on desired LatLong.
            _map.ViewExtents = ex;

            DialogResult = DialogResult.OK;
            Close();
        }
        private bool CheckCoordinates()
        {
            var latCheck = ParseCoordinates(_lat, d1.Text);
            var lonCheck = ParseCoordinates(_lon, d2.Text);

            latStatus.Text = !latCheck ? "Invalid Latitude (Valid example: \"41.1939 N\")" : string.Empty;
            lonStatus.Text = !lonCheck ? "Invalid Longitude (Valid example: \"19.4908 E\")" : string.Empty;

            return latCheck && lonCheck;
        }
        private double[] LatLonReproject(double x, double y)
        {
            var xy = new[] { x, y };

            // Change y coordinate to be less than 90 degrees to prevent a bug.
            if (xy[1] >= 90) xy[1] = 89.9;
            if (xy[1] <= -90) xy[1] = -89.9;

            // Need to convert points to proper projection. Currently describe WGS84 points which may or may not be accurate.
            var wgs84String = "GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137,298.257223562997]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.0174532925199433]]";
            var mapProjEsriString = _map.Projection.ToEsriString();
            var isWgs84 = mapProjEsriString.Equals(wgs84String);

            // If the projection is not WGS84, then convert points to properly describe desired location.
            if (!isWgs84)
            {
                var z = new double[1];
                var wgs84Projection = ProjectionInfo.FromEsriString(wgs84String);
                var currentMapProjection = ProjectionInfo.FromEsriString(mapProjEsriString);
                Reproject.ReprojectPoints(xy, z, wgs84Projection, currentMapProjection, 0, 1);
            }

            // Return array with 1 x and 1 y value.
            return xy;
        }
        private void ZoomToCoordinatesDialog_Load(object sender, EventArgs e)
        {

        }

        private void BT_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
