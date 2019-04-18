using DotSpatial.Data;
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

namespace Demo.RasterLinePackage
{
    public partial class RasterLine : Form
    {
        private IRaster raster;
        private Map map;
        private IMapRasterLayer[] rasterLayer;
        private List<string> layername;
        private string outputname = "";
        public RasterLine(Map map1)
        {
            this.map = map1;
            InitializeComponent();
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

        private void RasterLine_Load(object sender, EventArgs e)
        {
            rasterLayer = map.GetRasterLayers();
            layername = new List<string>();
            foreach(var r in rasterLayer)
            {
                layername.Add(r.LegendText);
            }
            foreach (string name in layername)
            {
                comboBox1.Items.Add(name);
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
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

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string cv = comboBox1.Text;
            foreach (var r in rasterLayer)
            {
                if(r.LegendText == cv)
                {
                    raster = r.DataSet;
                    break;
                }
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("栅格数据不能为空", "提示信息", MessageBoxButtons.OKCancel);
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("等值线初始值将替换为最小值", "提示信息", MessageBoxButtons.OKCancel);
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("等值线间隔不能为空", "提示信息", MessageBoxButtons.OKCancel);
            }
            if (textBox2.Text == "0")
            {
                MessageBox.Show("等值线间隔不能为0", "提示信息", MessageBoxButtons.OKCancel);
            }
            /*for(int i = 0; i < raster.NumColumns; i++)
            {
                for(int j = 0; j < raster.NumRows; j++)
                {
                    Console.Write(raster.Value[j,i] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(raster.CellToProj(0,0).X + "," + raster.CellToProj(0,0).Y);
            Console.WriteLine(raster.CellToProj(0,1).X + "," + raster.CellToProj(0,1).Y);
            Console.WriteLine(raster.CellToProj(1,0).X + "," + raster.CellToProj(1,0).Y);*/
            CreateRasterLine CR = new CreateRasterLine(raster,map);
            FeatureSet lines;
            bool sign = CR.Execute(Convert.ToDouble(textBox1.Text),Convert.ToDouble(textBox2.Text),out lines);
            if (sign)
            {
                MessageBox.Show("等值线操作完成", "提示信息", MessageBoxButtons.OKCancel);
                if (outputname != "")
                {
                    lines.SaveAs(outputname, true);
                }
            }
            else
            {
                MessageBox.Show("等值线操作失败", "提示信息", MessageBoxButtons.OKCancel);
            }
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox2.Image = bp;
            }
            else if (textBox1.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox2.Image = bp;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\valid.png");
                pictureBox2.Image = bp;
            }
            else if (textBox2.Text == "")
            {
                var bp = new System.Drawing.Bitmap("C:\\Users\\54943\\Desktop\\www\\Demo\\Demo\\Resources\\Caution.png");
                pictureBox2.Image = bp;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            outputname = saveFile();
            textBox3.Text = outputname;
        }

        private string saveFile()
        {
            string localFilePath = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "要素文件(*.shp) |*.shp";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString();//get the path
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
            }
            return localFilePath;
        }
    }
}
