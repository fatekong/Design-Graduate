using Demo.Tabulate;
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
    public partial class Charts_3D : Form
    {
        private Map map;
        ILayer[] ipl;
        IFeatureSet ifeatureSet;
        public Charts_3D(Map map1)
        {
            this.map = map1;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((int)MessageBox.Show("是否退出3D图表分析", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == 1)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool sign = true;
            if(comboBox1.Text == "")
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
                StoreData sd = new StoreData(dt, false);
                MessageBox.Show("json数据保存完毕", "提示信息", MessageBoxButtons.OKCancel);
                System.Diagnostics.Process.Start("Tables\\Mycharts3D.html");
                this.Close();
            }
            
        }

        private void Charts_3D_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("是否退出3维图表绘制", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                e.Cancel = false;  //点击OK   
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            richTextBox1.Text = "3D图表绘制";
            richTextBox2.Text = "选择图层或文件的三个属性，对其进行三维坐标图绘制";
        }

        private void groupBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "点要素集";
            richTextBox2.Text = "选择合适的点集文件或者图层进行三维坐标绘图分析";
        }

        private void groupBox2_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "横坐标属性";
            richTextBox2.Text = "在绘制图表时将其中的某个属性作为横坐标（x轴）";
        }

        private void groupBox3_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "X轴属性";
            richTextBox2.Text = "在绘制图表时将其中的某个属性作为x轴";
        }

        private void groupBox4_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "Y轴属性";
            richTextBox2.Text = "在绘制图表时将其中的某个属性作为y轴";
        }

        private void groupBox7_MouseCaptureChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "Z轴属性";
            richTextBox2.Text = "在绘制图表时将其中的某个属性作为z轴";
        }

        private void Charts_3D_Load(object sender, EventArgs e)
        {
            ipl = map.GetAllLayers().ToArray();
            foreach (var p in ipl)
            {
                comboBox1.Items.Add(p.LegendText);
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            string cv = comboBox1.Text;
            ILayer il = null;
            List<string> column = new List<string>();
            foreach (ILayer l in ipl)
            {
                if (cv == l.LegendText)
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
