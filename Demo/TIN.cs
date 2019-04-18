using DotSpatial.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Data;
using DotSpatial.Topology;
using DotSpatial.Symbology;

namespace Demo
{
    public partial class TIN : Form
    {
        private Map map;
        private IMapPointLayer[] ipl;
        private List<string> layername;
        private string FileName = "";
        private IFeatureSet ifeatureset;
        private bool flag = false;
        private Dictionary<string, MapPointLayer> file_layer = new Dictionary<string, MapPointLayer>();
        public TIN(Map map1)
        {
            InitializeComponent();
            map = map1;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void TIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("是否退出TIN处理", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                e.Cancel = false;  //点击OK   
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((int)MessageBox.Show("是否退出TIN处理", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == 1)
            {
                this.Close();
            }
        }

        private void groupBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "点要素集";
            richTextBox2.Text = "选择合适的点集文件或者图层进行TIN计算";
        }

        private void groupBox3_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "X坐标选择";
            richTextBox2.Text = "在图层中X轴的位置";
        }

        private void groupBox4_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "Y坐标选择";
            richTextBox2.Text = "在图层中Y轴的位置";
        }

        private void groupBox7_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "栅格";
            richTextBox2.Text = "选择输出以栅格的文件形式储存";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "")
            {
                MessageBox.Show("要素信息不能为空", "TIN提示信息", MessageBoxButtons.OKCancel);
            }
            if(comboBox5.Text == "")
            {
                MessageBox.Show("纬度选择", "TIN提示信息", MessageBoxButtons.OKCancel);
            }
            if (comboBox6.Text == "")
            {
                MessageBox.Show("经度选择", "TIN提示信息", MessageBoxButtons.OKCancel);
            }
            if(flag == false)
            {
                FeatureSet delaunayline = new FeatureSet(FeatureType.Line);
                MapLineLayer ml = (MapLineLayer)map.Layers.Add(delaunayline);
                LineSymbolizer symbol = new LineSymbolizer(Color.Black, 2);
                ml.Symbolizer = symbol;
                ml.LegendText = "delaunay";
                List<Tin_Point> tin_Points = new List<Tin_Point>();
                for (int i = 0; i < ifeatureset.Features.Count; i++)
                {
                    //Console.WriteLine("x: " + ifeatureset.Features[i].BasicGeometry.Coordinates[0].X + ", y: " + ifeatureset.Features[i].BasicGeometry.Coordinates[0].Y);
                    tin_Points.Add(new Tin_Point(ifeatureset.Features[i].BasicGeometry.Coordinates[0].X, ifeatureset.Features[i].BasicGeometry.Coordinates[0].Y));
                }
                Delaunay delaunay = new Delaunay();
                List<Triangle> triangles = new List<Triangle>();
                triangles = delaunay.ConstructionDelaunay(tin_Points);
                foreach(var tri in triangles)
                {
                    Coordinate a = new Coordinate(tri.p1.X, tri.p1.Y);
                    Coordinate b = new Coordinate(tri.p2.X, tri.p2.Y);
                    Coordinate c = new Coordinate(tri.p3.X, tri.p3.Y);
                    List<Coordinate> lineArray = new List<Coordinate>();
                    ILineString lineGeometry = new LineString(lineArray);
                    IFeature lineFeature = delaunayline.AddFeature(lineGeometry);
                    lineArray.Add(a);
                    lineArray.Add(b);
                    lineArray.Add(c);
                    DotSpatial.Topology.LineString ls = new LineString(lineArray);
                    lineFeature.Coordinates.Add(a);
                    lineFeature.Coordinates.Add(b);
                    lineFeature.Coordinates.Add(c);
                    delaunayline.InitializeVertices();
                    map.ResetBuffer();
                }
            }
            if(FileName != "")
            {
                ifeatureset.SaveAs(FileName, true);
                if(checkBox1.CheckState == CheckState.Checked)
                {
                    var shp = Shapefile.OpenFile(FileName);
                    shp.Projection = ifeatureset.Projection;
                    map.Layers.Add(shp);
                }
            }
            MessageBox.Show("TIN操作完成", "TIN提示信息", MessageBoxButtons.OK);
            this.Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox10.Image = bp;
            }
            else if (comboBox1.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox10.Image = bp;
            }
        }

        private void comboBox5_TextChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox2.Image = bp;
            }
            else if (comboBox5.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox2.Image = bp;
            }
        }

        private void comboBox6_TextChanged(object sender, EventArgs e)
        {
            if (comboBox6.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox4.Image = bp;
            }
            else if (comboBox6.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox4.Image = bp;
            }
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox8.Image = bp;
            }
            else if (comboBox4.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox8.Image = bp;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string filename = ReadFile();
            var shp = Shapefile.OpenFile(filename);
            IMapLayer layer = null;
            layer = map.Layers.Add(shp) as IMapLayer;
            if (layer is MapPointLayer)
            {
                file_layer.Add(filename, layer as MapPointLayer);
                comboBox1.Items.Add(filename);
                flag = true;
            }
            else
            {
                map.Layers.Remove(layer);
                MessageBox.Show("要素集格式错误，需要点类型图层！", "TIN提示信息", MessageBoxButtons.OKCancel);
            }
        }

        private string ReadFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "IDW要素文件选择";
            fileDialog.Filter = "矢量文件(*.shp)|*.shp";
            string file = "";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                file = fileDialog.FileName;//返回文件的完整路径                
            }
            return file;
        }

        private string saveFile()
        {
            string localFilePath = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "矢量文件(*.shp) |*.shp";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString();//get the path
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
            }
            return localFilePath;
        }

        private void TIN_Load(object sender, EventArgs e)
        {
            ipl = map.GetPointLayers();
            layername = new List<string>();
            foreach (IMapPointLayer pl in ipl)
            {
                layername.Add(pl.LegendText);
            }
            foreach (string name in layername)
            {
                comboBox1.Items.Add(name);
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string cv = comboBox1.Text;
            IMapPointLayer pointlayer = null;
            //List<string> column = new List<string>();
            if(flag == true)
            {
                pointlayer = file_layer[cv];
            }
            if(pointlayer == null)
            {
                foreach (IMapPointLayer l in ipl)
                {
                    if (l.LegendText == cv)
                    {
                        pointlayer = l;
                    }
                }
            }         
            if (pointlayer != null&&flag == false)
            {
                ifeatureset = pointlayer.DataSet;
                /*IFeatureLayer fl = pointlayer as IFeatureLayer;
                //fl.ShowAttributes();
                //column = fl.DataSet.GetColumnNames;
                //IFeatureSet featureSet = new FeatureSet();
                ifeatureset = pointlayer.DataSet;
                DataTable dt = pointlayer.DataSet.DataTable;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    column.Add(dt.Columns[i].ColumnName.ToString());
                }*/
                comboBox6.Items.Add("已选定。");
                comboBox5.Items.Add("已选定。");
                comboBox6.SelectedIndex = 0;
                comboBox5.SelectedIndex = 0;
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FileName = saveFile();
            comboBox4.Items.Add(FileName);
            comboBox4.SelectedIndex = 0;
            
        }
    }
}
