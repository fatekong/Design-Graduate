using DotSpatial.Modeling.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using DotSpatial.Data;
using DotSpatial.Topology;
using System.Collections;

namespace Demo
{
    public partial class IDW : Form
    {
        private Map map;
        //private List<ILayer> layerList;
        IMapPointLayer[] ipl;
        IRaster raster;
        IFeatureSet ifeatureset;
        string FileName = null;
        bool flag = false;//flag为false时未选择文件，true则时通过选择文件来进行插值
        //string zFiled;
        private List<string> layername;
        Dictionary<string,MapPointLayer> file_layer = new Dictionary<string, MapPointLayer>();
        public IDW(Map map1)
        {
            map = map1;
            InitializeComponent();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            string filename = ReadFile();
            var shp = Shapefile.OpenFile(filename);
            IMapLayer layer = null;
            layer = map.Layers.Add(shp) as IMapLayer;
            if (layer is MapPointLayer)
            {
                file_layer.Add(filename,layer as MapPointLayer);
                comboBox1.Items.Add(filename);
                flag = true;
            }
            else
            {
                map.Layers.Remove(layer);
                MessageBox.Show("要素集格式错误，需要点类型图层！", "IDW", MessageBoxButtons.OKCancel);
            }
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "IDW（空间插值）";
            //richTextBox2.
            richTextBox1.Text = "IDW(Inverse Distance Weighted)是一种常用而简便的空间插值方法，它以插值点与样本点间的距离为权重进行加权平均，离插值点越近的样本点赋予的权重越大。 设平面上分布一系列离散点，已知其坐标和值为Xi，Yi, Zi （i = 1，2，…，n）通过距离加权值求z点值。\n" +
                "IDW通过对邻近区域的每个采样点值平均运算获得内插单元。这一方法要求离散点均匀分布，并且密度程度足以满足在分析中反映局部表面变化。";
        }

        private void groupBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = "点要素集";
            richTextBox1.Text = "选择合适的点集进行插值";
        }

        private void IDW_Load(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = 0;
            //List<FeatureSet> fss = map.
            ipl = map.GetPointLayers();
            layername = new List<string>();
            foreach (IMapPointLayer pl in ipl)
            {
                layername.Add(pl.LegendText);
            }
            /*foreach (ILayer layer in layerList)
            {
                IFeatureLayer fl = layer as IFeatureLayer;
                if(fl is PointLayer)
                    layername.Add(layer.LegendText);
                else
                {
                    layerList.Remove(fl);
                }
            }*/
            foreach(string name in layername)
            {
                comboBox1.Items.Add(name);
            }
            if(comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = "Z高程值";
            richTextBox1.Text = "选择点集中合适的需要进行插值的属性";
        }

        private void groupBox3_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = "像元大小";
            richTextBox1.Text = "在输入数据层中地理单位的像元大小。如果为0则宽度为输入图层的范围大小除以255";
        }

        private void groupBox4_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = "权重";
            richTextBox1.Text = "权重的大小将影响不同距离的点的影响力。" +
                "定义更高的幂值，可进一步强调最近点，因此，邻近数据将受到更大影响，表面会变得更加详细；" +
                "指定较小的权重将对距离较远的周围点产生更大的影响，从而导致平面更加平滑";
        }

        private void groupBox5_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = "临近类型";
            richTextBox1.Text = "当需要查找点要素时请选择临近区域的类型。" +
                "固定距离: 当使用一个固定距离进行查询但是仍得不到点数的最小值时，查询半径在自动变大直到满足最小值。输入0或无极小值。" +
                "固定数目: 当使用固定值进行查询时，将会查找半径范围内的所有点。";
        }

        private void groupBox6_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = "数目/距离";
            richTextBox1.Text = "当需要查找点要素时请选择临近区域的类型。" +
                "固定距离: 当使用一个固定距离进行查询但是仍得不到点数的最小值时，查询半径在自动变大直到满足最小值。输入0或无极小值。" +
                "固定数目: 当使用固定值进行查询时，将会查找半径范围内的所有点。";
        }

        private void groupBox7_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = "栅格";
            richTextBox1.Text = "已栅格文件的格式对形成的内插点集进行保存";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("要素信息不能为空", "提示信息", MessageBoxButtons.OKCancel);
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Z高程值不能为空", "提示信息", MessageBoxButtons.OKCancel);
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("像元大小不能为空", "提示信息", MessageBoxButtons.OKCancel);
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("权重不能为空", "提示信息", MessageBoxButtons.OKCancel);
            }
            else if (comboBox4.Text == "")
            {
                MessageBox.Show("输出栅格文件不能为空", "提示信息", MessageBoxButtons.OKCancel);
            }
            InverseDistanceWeight idw;
            if (ifeatureset != null)
            {
                int neighborType = 0;
                idw = new InverseDistanceWeight(this.progressBar1);
                if (comboBox3.Text == "固定距离")
                {
                    neighborType = 1;
                    idw.Execute(ifeatureset, comboBox2.Text, System.Convert.ToDouble(textBox1.Text), System.Convert.ToDouble(textBox2.Text), neighborType, 0, System.Convert.ToDouble(textBox3.Text), raster);
                }
                //var myprogress = new MyProgressBar();
                idw.Execute(ifeatureset, comboBox2.Text, System.Convert.ToDouble(textBox1.Text), System.Convert.ToDouble(textBox2.Text), neighborType, System.Convert.ToInt32(textBox3.Text), 0, raster);
            }
            MessageBox.Show("IDW分析完成", "IDW提示");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if((int)MessageBox.Show("是否取消IDW内插", "IDW提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == 1)
            {
                this.Close();
            }          
        }

        private void IDW_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*DialogResult result = MessageBox.Show("是否退出IDW内插", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                e.Cancel = false;  //点击OK   
            }
            else
            {
                e.Cancel = true;
            }*/
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || e.KeyChar > '9')
            {
                e.Handled = true;
            }
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || e.KeyChar > '9')
            {
                e.Handled = true;
            }
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || e.KeyChar > '9')
            {
                e.Handled = true;
            }
            if (e.KeyChar == 8)
                e.Handled = false;
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

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox3.Image = bp;
            }
            else if (comboBox2.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox3.Image = bp;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox4.Image = bp;
            }
            else if (textBox1.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox4.Image = bp;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox5.Image = bp;
            }
            else if (textBox2.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox5.Image = bp;
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

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string cv = comboBox1.Text;
            IMapPointLayer pointlayer = null;
            List<string> column = new List<string>();
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
            if(pointlayer != null)
            {
                /*FeatureSet fs = new FeatureSet(FeatureType.Point);
                fs = layer as FeatureSet;
                for(int i = 0; i < fs.DataTable.Columns.Count; i++)
                {
                    column.Add(fs.DataTable.Columns[i].ColumnName.ToString());
                }*///为空，错误
                
                IFeatureLayer fl = pointlayer as IFeatureLayer;
                //fl.ShowAttributes();
                //column = fl.DataSet.GetColumnNames;
                //IFeatureSet featureSet = new FeatureSet();
                ifeatureset = pointlayer.DataSet;
                DataTable dt = pointlayer.DataSet.DataTable;
                for(int i = 0; i < dt.Columns.Count; i++)
                {
                    column.Add(dt.Columns[i].ColumnName.ToString());
                }
            }
            //Console.WriteLine("column_count:"+column.Count);
            //comboBox2.Items.Add("xxxx");
            foreach(string s in column)
            {
                comboBox2.Items.Add(s);
            }
            //comboBox2.Items.Add("adasdad");
            if(comboBox2.Items.Count>0)
                comboBox2.SelectedIndex = 0;
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

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FileName = saveFile();
            comboBox4.Items.Add(FileName);
            comboBox4.SelectedIndex = 0;
            raster = new Raster();
            raster.Filename = FileName;
            //raster.SaveAs(filename);
            
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
    }
}
