using DotSpatial.Controls;
using DotSpatial.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Symbology;
using Demo.Tabulate;

namespace Demo
{
    public partial class Charts_2D : Form
    {
        private Map map;
        ILayer[] ipl;
        IFeatureSet ifeatureSet;
        public Charts_2D(Map map1)
        {
            this.map = map1;
            InitializeComponent();
        }

        private void Charts_2D_Load(object sender, EventArgs e)
        {
            ipl = map.GetAllLayers().ToArray();
            foreach(var p in ipl)
            {
                comboBox1.Items.Add(p.LegendText);
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool sign = true;
            if (comboBox1.Text == "")
            {
                MessageBox.Show("要素信息不能为空", "提示信息", MessageBoxButtons.OKCancel);
                sign = false;
            }
            else if (comboBox3.Text == "")
            {
                MessageBox.Show("属性数据不能为空，请检查文件", "提示信息", MessageBoxButtons.OKCancel);
                sign = false;
            }
            if (sign)
            {
                DataTable dt = ifeatureSet.DataTable;
                if (MessageBox.Show("是否需要在属性中增加X，Y（经纬度）属性？（如果已有的话则不需要）", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    dt.Columns.Add("X(纬度)", typeof(double));
                    dt.Columns.Add("Y(经度)", typeof(double));
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["X(纬度)"] = ifeatureSet.Features[i].BasicGeometry.Coordinates[0].X;
                        dr["Y(经度)"] = ifeatureSet.Features[i].BasicGeometry.Coordinates[0].Y;
                        i++;
                    }
                }
                //List<string> names = new List<string>();
                StoreData sd = new StoreData(dt);
                MessageBox.Show("json数据保存完毕", "提示信息", MessageBoxButtons.OKCancel);
                System.Diagnostics.Process.Start("Tables\\Mycharts2D.html");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((int)MessageBox.Show("是否退出2维图表分析", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == 1)
            {
                this.Close();
            }
        }

        private void groupBox7_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "输出文件";
            richTextBox2.Text = "将图层中的属性数据输出为可读文件格式(默认以.csv格式保存)";
        }

        private void Charts_2D_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("是否退出图表绘制", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                e.Cancel = false;  //点击OK   
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void groupBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "点要素集";
            richTextBox2.Text = "选择合适的点集文件或图层进行二维坐标绘图分析";
        }

        private void groupBox2_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "横坐标属性";
            richTextBox2.Text = "在绘制图表时将其中的某个属性作为横坐标（x轴）";
        }

        private void groupBox3_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "纵坐标属性";
            richTextBox2.Text = "在绘制图表时将其中的某个属性作为纵坐标（y轴）";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            richTextBox1.Text = "2D图表绘制";
            richTextBox2.Text = "选择图层或文件的两个属性，对其进行二维坐标图绘制";
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

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox2.Image = bp;
                //Console.WriteLine("有");
            }
            else if (comboBox3.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox2.Image = bp;
                //Console.WriteLine("空");
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            string cv = comboBox1.Text;
            ILayer il = null;
            List<string> column = new List<string>();
            foreach (ILayer l in ipl)
            {
                if(cv == l.LegendText)
                {
                    il = l;
                    break;
                }
            }
            if (il != null)
            {
                IFeatureLayer fl = il as IFeatureLayer;
                ifeatureSet = fl.DataSet;
                DataTable dt = ifeatureSet.DataTable;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    column.Add(dt.Columns[i].ColumnName.ToString());
                }
            }
            
            foreach (string s in column)
            {
                comboBox3.Items.Add(s);
            }
            if (comboBox3.Items.Count > 0)
                comboBox3.SelectedIndex = 0;
        }
    }
}
