using Demo.KrigingPackage;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Demo
{
    public partial class Kriging : Form
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
        Dictionary<string, MapPointLayer> file_layer = new Dictionary<string, MapPointLayer>();
        public Kriging(Map map1)
        {
            map = map1;
            InitializeComponent();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "Kriging（空间插值）";
            //richTextBox2.
            richTextBox1.Text = "克里金法（Kriging）是依据协方差函数对随机过程/随机场进行空间建模和预测（插值）的回归算法。" +
                "该算法基于一般最小二乘算法的随机插值技术，用方差图作为权重函数；这一技术可被应用于任何需要用点数据估计其在地表上分布的现象。";
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


        private void Kriging_Load(object sender, EventArgs e)
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
            foreach (string name in layername)
            {
                comboBox1.Items.Add(name);
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void groupBox5_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = "临近类型";
            richTextBox1.Text = "当需要查找点要素时请选择临近区域的类型。" +
                "固定距离: 当使用一个固定距离进行查询但是仍得不到点数的最小值时，查询半径在自动变大直到满足最小值。输入0或无极小值。" +
                "固定数目: 当使用固定值进行查询时，将会查找半径范围内的所有点。";
        }

        private void groupBox4_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = "模型";
            richTextBox1.Text = "变差函数与随机变量的距离h存在一定的关系，这种关系可以用理论模型表示。常用的变差函数理论模型包括球状模型、高斯模型与指数模型。";
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

        private void groupBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = "点要素集";
            richTextBox1.Text = "选择合适的点集进行插值";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((int)MessageBox.Show("是否退出Kriging内插", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == 1)
            {
                this.Close();
            }
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
            else if (comboBox4.Text == "")
            {
                MessageBox.Show("输出栅格文件不能为空", "提示信息", MessageBoxButtons.OKCancel);
            }
            KrigingAlgorithm kriging;
            if (ifeatureset != null)
            {
                kriging = new KrigingAlgorithm(this.progressBar1);
                int neighborType = 0;
                if (comboBox3.Text == "固定距离")
                {
                    neighborType = 1;
                    bool flag = kriging.Execute(ifeatureset, comboBox2.Text, System.Convert.ToDouble(textBox1.Text), comboBox5.Text, neighborType, 0, System.Convert.ToDouble(textBox3.Text), raster);
                }
               
                if (kriging.Execute(ifeatureset, comboBox2.Text, System.Convert.ToDouble(textBox1.Text), comboBox5.Text, neighborType, System.Convert.ToInt32(textBox3.Text), 0, raster))
                {
                    MessageBox.Show("Kriging分析完成", "Kriging提示");
                }
                else
                    MessageBox.Show("Kriging分析失败", "Kriging提示");
            }
            this.Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox10.Image = bp;
            }
            else if(comboBox1.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox10.Image = bp;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox4.Image = bp;
                //Console.WriteLine("有");
            }
            else if (textBox1.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox4.Image = bp;
                //Console.WriteLine("空");
            }
            
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || e.KeyChar > '9')
            {
                e.Handled = true;
            }
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox3.Image = bp;
                //Console.WriteLine("有");
            }
            else if (comboBox2.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox3.Image = bp;
                //Console.WriteLine("空");
            }
        }

        private void comboBox5_TextChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox5.Image = bp;
            }
            else if (comboBox5.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox5.Image = bp;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox7.Image = bp;
                //Console.WriteLine("有");
            }
            else if (textBox3.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox7.Image = bp;
                //Console.WriteLine("空");
            }
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox8.Image = bp;
                //Console.WriteLine("有");
            }
            else if (comboBox4.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox8.Image = bp;
                //Console.WriteLine("空");
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
                MessageBox.Show("要素集格式错误，需要点类型图层！", "IDW", MessageBoxButtons.OKCancel);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string cv = comboBox1.Text;
            IMapPointLayer pointlayer = null;
            List<string> column = new List<string>();
            if (flag == true)
            {
                pointlayer = file_layer[cv];
            }
            if (pointlayer == null)
            {
                foreach (IMapPointLayer l in ipl)
                {
                    if (l.LegendText == cv)
                    {
                        pointlayer = l;
                    }
                }
            }
            if (pointlayer != null)
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
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    column.Add(dt.Columns[i].ColumnName.ToString());
                }
            }
            //Console.WriteLine("column_count:"+column.Count);
            //comboBox2.Items.Add("xxxx");
            foreach (string s in column)
            {
                comboBox2.Items.Add(s);
            }
            //comboBox2.Items.Add("adasdad");
            if (comboBox2.Items.Count > 0)
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
