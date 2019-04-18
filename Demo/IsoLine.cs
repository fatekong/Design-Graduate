using Demo.ISOLinePackage;
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
    public partial class IsoLine : Form
    {
        private Map map;
        //private List<ILayer> layerList;
        IMapPointLayer[] ipl;
        IFeatureSet ifeatureset;
        private List<string> layername;
        string FileName = null;
        bool flag = false;//flag为false时未选择文件，true则时通过选择文件来进行插值
        Dictionary<string, MapPointLayer> file_layer = new Dictionary<string, MapPointLayer>();
        public IsoLine(Map map1)
        {
            this.map = map1;
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            richTextBox1.Text = "等值线处理";
            richTextBox2.Text = "等值线是制图对象某一数量指标值相等的各点连成的平滑曲线，由地图上标出的表示制图对象数量的各点，采用内插法找出各整数点绘制而成的。";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((int)MessageBox.Show("是否退出等值线处理", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == 1)
            {
                this.Close();
            }
        }

        private void IsoLine_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("是否退出等值线处理", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                e.Cancel = false;  //点击OK   
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void groupBox3_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "等值线间距";
            richTextBox2.Text = "设计每一条等值线Z值的间隔";
        }

        private void groupBox2_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "Z高程值";
            richTextBox2.Text = "选择点集中合适的属性绘制等值线";
        }

        private void groupBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "点要素集";
            richTextBox2.Text = "选择合适的点集绘制等值线";
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

        private void IsoLine_Load(object sender, EventArgs e)
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
            foreach (string s in column)
            {
                comboBox2.Items.Add(s);
            }
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool sign = true;
            if(comboBox1.Text == "")
            {
                MessageBox.Show("要素信息不能为空", "提示信息", MessageBoxButtons.OKCancel);
                sign = false;
            }
            if(comboBox2.Text == "")
            {
                MessageBox.Show("该要素文件的属性值为空", "提示信息", MessageBoxButtons.OKCancel);
                sign = false;
            }
            if (textBox2.Text == "" || Convert.ToDouble(textBox2.Text) == 0)
            {
                MessageBox.Show("等值线间隔不能为空或为0", "提示信息", MessageBoxButtons.OKCancel);
                sign = false;
            }
            if (sign && ifeatureset != null)
            {
                CreateIsoLine cil = new CreateIsoLine(map);
                bool over = cil.Execute(ifeatureset,comboBox2.Text,Convert.ToDouble(textBox1.Text),Convert.ToDouble(textBox2.Text));
                //bool over = cil.Execute(12, 3);
                if(over == true)
                {
                    MessageBox.Show("等值线生成完成", "提示信息", MessageBoxButtons.OKCancel);
                    this.Close();
                }
                else
                    MessageBox.Show("等值线生成失败", "提示信息", MessageBoxButtons.OKCancel);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
