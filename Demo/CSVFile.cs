using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Topology;
//using GeoAPI.Geometries;
//using NetTopologySuite.Geometries;
//using DotSpatial.Topology;

namespace Demo
{
    public partial class CSVFile : Form
    {
        string file = null;
        FeatureSet fs = null;
        CSVFileHelp csv = new CSVFileHelp();
        DataTable DT = new DataTable();
        string X = null;
        string Y = null;
        Map map = null;
        string FileName = "";
        public CSVFile(Map map1)
        {
            this.map = map1;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((int)MessageBox.Show("是否退出TIN处理", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == 1)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("文件信息不能为空", "提示信息", MessageBoxButtons.OKCancel);
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("经度信息不能为空", "提示信息", MessageBoxButtons.OKCancel);
            }
            else if (comboBox3.Text == "")
            {
                MessageBox.Show("纬度信息不能为空", "提示信息", MessageBoxButtons.OKCancel);
            }
            X = comboBox2.Text;
            Y = comboBox3.Text;
            fs = new FeatureSet(FeatureType.Point);
            //fs.Projection = map.Projection;
            for (int i = 0; i < DT.Columns.Count; i++)
            {
                fs.DataTable.Columns.Add(DT.Columns[i].ToString());
            }
            double x = 0;
            double y = 0;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                for (int j = 0; j < DT.Columns.Count; j++)
                {
                    if (DT.Columns[j].ColumnName.ToString() == X)
                    {
                        x = System.Convert.ToDouble(DT.Rows[i][j].ToString());
                    }
                    if (DT.Columns[j].ColumnName.ToString() == Y)
                    {
                        y = System.Convert.ToDouble(DT.Rows[i][j].ToString());
                    }
                }

                Coordinate c = new Coordinate(x, y);
                //Point point = map.ProjToPixel(c);
                Point point = new Point(c);
                IFeature feature = fs.AddFeature(point);
                //feature f = new Feature(c);
                for (int l = 0; l < DT.Columns.Count; l++)
                {
                    feature.DataRow[l] = DT.Rows[i][l];
                }
            }
            if(FileName != "")
            {
                fs.SaveAs(FileName, true);
                var shp = Shapefile.OpenFile(FileName);
                shp.Projection = map.Projection;
                map.Layers.Add(shp);
            }
            else
            {
                MapPointLayer pointLayer = (MapPointLayer)map.Layers.Add(fs);
                pointLayer.LegendText = "csv point";
                //pointLayer.Projection = KnownCoordinateSystems.Geographic.Asia.Beijing1954;
                //pointLayer.Projection = map.Projection;
                //pointLayer.ProjectionString = " +x_0=500000 +y_0=0 +lat_0=0 +lon_0=99 +proj=tmerc +a=6378245 +b=6356863.01877305 +no_defs";
            }
            this.Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox10.Image = bp;
                //Console.WriteLine("有");
            }
            else if (comboBox1.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox10.Image = bp;
                //Console.WriteLine("空");
            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox2.Image = bp;
                //Console.WriteLine("有");
            }
            else if (comboBox2.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox2.Image = bp;
                //Console.WriteLine("空");
            }
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox3.Image = bp;
                //Console.WriteLine("有");
            }
            else if (comboBox3.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox3.Image = bp;
                //Console.WriteLine("空");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择.csv文件";
            fileDialog.Filter = ".csv文件(*.csv)|*.csv";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                file = fileDialog.FileName;//返回文件的完整路径                
            }
            DT = csv.OpenCSV(file);
            comboBox1.Items.Add(file);
            comboBox1.SelectedIndex = 0;
            List<string> TableName = new List<string>();
            for(int i = 0; i < DT.Columns.Count; i++)
            {
                TableName.Add(DT.Columns[i].ColumnName.ToString());
            }
            foreach(string header in TableName)
            {
                comboBox2.Items.Add(header);
                comboBox3.Items.Add(header);
            }
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

        }

        private void CSVFile_Load(object sender, EventArgs e)
        {
            //comboBox4.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private string saveFile()
        {
            string localFilePath = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Shape文件(*.shp) |*.shp";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString();//get the path
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
            }
            return localFilePath;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            FileName = saveFile();
            comboBox4.Items.Add(FileName);
            comboBox4.SelectedIndex = 0;
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox4.Image = bp;
                //Console.WriteLine("有");
            }
            else if (comboBox4.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox4.Image = bp;
                //Console.WriteLine("空");
            }
        }
    }
}
