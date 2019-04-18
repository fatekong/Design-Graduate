using Demo.Properties;
using DotSpatial.Controls;
using DotSpatial.Controls.Header;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Projections;
using Demo.RasterLinePackage;

namespace Demo
{
    public partial class Form1 : Form
    {
        //string MeasureMode = "Line";//Line or Area
        ShapeOption so;
        //private DotSpatial.Controls.ToolManager toolManager;
        public Form1()
        {
            InitializeComponent();
            appManager1.LoadExtensions();
            //smap1.Projection = KnownCoordinateSystems.Geographic.Asia.Beijing1954;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //map1.AddLayer();
        }

        private void toolStripContainer1_TopToolStripPanel_SizeChanged(object sender, EventArgs e)
        {
            toolStripContainer1.Height = toolStripContainer1.TopToolStripPanel.Height;
        }


        private void toolManager1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            Attribute attribute = new Attribute();
            attribute.ShowDialog();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            //map1.AddLayer();
        }

        private void clearMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //map1.ClearLayers();
        }

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //map1.AddLayer();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((int)MessageBox.Show("是否退出应用？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == 1)
            {
                this.Close();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //map1.ClearLayers();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //map1.FunctionMode = FunctionMode.Pan;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            DotSpatial.Controls.LayoutForm frm = new DotSpatial.Controls.LayoutForm();
            //frm.MapControl = map1;
            //frm.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            map1.FunctionMode = FunctionMode.None;
            map1.Cursor = Cursors.Default;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            //map1.FunctionMode = FunctionMode.Info;
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("http://www.baidu.com");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {           
            //map1.FunctionMode = FunctionMode.ZoomIn;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //map1.FunctionMode = FunctionMode.ZoomOut;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            //map1.ZoomToMaxExtent();
        }

        private void map1_GeoMouseMove(object sender, GeoMouseArgs e)
        {
            string loacation = "X: " + e.GeographicLocation.X + " ";
            loacation += "Y: " + e.GeographicLocation.Y.ToString();
            toolStripStatusLabel1.Text = loacation;
            string location = "x: " + e.Location.X + " ";
            location += "y: " + e.Location.Y + " ";
            toolStripStatusLabel2.Text = location;
        }
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            //map1.FunctionMode = FunctionMode.Measure;
            
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            //map1.FunctionMode = FunctionMode.Select;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            //using (var dialog = new ZoomToCoordinatesDialog(map1))
                //dialog.ShowDialog();
        }
        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void ToolManager1_NodeMouseDoubleClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            string name = e.Node.Name;
            if (name.Equals("IDW"))
            {
                IDW iDW = new IDW(map1);
                iDW.ShowDialog();
            }
            else if (name.Equals("Kriging"))
            {
                Kriging kriging = new Kriging(map1);
                kriging.ShowDialog();
                Console.WriteLine("kriging");
            }
            else if (name.Equals("TIN"))
            {
                TIN tIN = new TIN(map1);
                tIN.ShowDialog();
            }
            else if (name.Equals("IsoLine"))
            {
                Console.WriteLine("isoline");
                IsoLine isoLine = new IsoLine(map1);
                isoLine.ShowDialog();
            }
            else if (name.Equals("RasterLine"))
            {
                Console.WriteLine("RasterLine");
                RasterLine rasterLine = new RasterLine(map1);
                rasterLine.ShowDialog();
            }
            else if (name.Equals("dimension2"))
            {
                Charts_2D charts_2D = new Charts_2D(map1);
                charts_2D.ShowDialog();
            }
            else if (name.Equals("dimension3"))
            {
                Charts_3D charts_3D = new Charts_3D(map1);
                charts_3D.ShowDialog();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            so = new ShapeOption(map1);
            //appManager1.HeaderControl.Add(new RootItem(HelpMenu, "Help"));
            //appManager1.HeaderControl.Add(new SimpleActionItem("View Help", toolStripButton4_Click) {SmallImage = Resources.cursor_arrow_16x16, LargeImage = Resources.cursor_arrow_32x32 });
        }

        private void toolStripContainer1_TopToolStripPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                map1.FunctionMode = FunctionMode.None;
                map1.Cursor = Cursors.Default;
            }
        }

        private void map1_MouseDown(object sender, MouseEventArgs e)
        {
            so.MouseDown(sender, e);
        }

        private void toolStripContainer1_TopToolStripPanel_Click_1(object sender, EventArgs e)
        {

        }

        private void pointToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void creatPointFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map1.Cursor = Cursors.Cross;
            so.createPointShape();
        }

        private void savePointFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            so.savaPointShape();
        }

        private void creatPolylineFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map1.Cursor = Cursors.Cross;
            so.createPolyLineShape();
        }

        private void savePolylineFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            so.savaPolylineShape();
        }

        private void creatPolygonFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map1.Cursor = Cursors.Cross;
            so.createPolygonShape();
        }

        private void creatPolygonFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            so.savePolygonShape();
        }

        private void loadCSVFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
            CSVFile cSVFile = new CSVFile(map1);
            cSVFile.Show();
        }
    }
}
