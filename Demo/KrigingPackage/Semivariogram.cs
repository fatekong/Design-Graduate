using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Demo.KrigingPackage
{
    public partial class Semivariogram : Form
    {
        /// <summary>
        /// 显示拟合结果
        /// </summary>
        /// <param name="partialSill">偏基台值</param>
        /// <param name="majorRange">主变程值</param>
        /// <param name="dataForShow">已知点</param>
        /// <param name="dataForCal">均值点</param>
        /// <param name="dataForLine">拟合曲线</param>
        public Semivariogram(double partialSill, double majorRange, double[,] dataForShow, double[,] dataForCal, double[,] dataForLine)
        {
            InitializeComponent();
            label_PartialSill.Text = partialSill.ToString(".00");
            label_MajorRange.Text = majorRange.ToString(".00");

            chart_Data.Series[0].Points.Clear();
            chart_Data.Series[1].Points.Clear();
            chart_Data.Series[2].Points.Clear();

            for (int n = 0; n < dataForShow.GetLength(0); n++)
                chart_Data.Series[0].Points.Add(new DataPoint(dataForShow[n, 0], dataForShow[n, 1]));

            for (int n = 0; n < dataForCal.GetLength(0); n++)
                chart_Data.Series[1].Points.Add(new DataPoint(dataForCal[n, 0], dataForCal[n, 1]));

            for (int n = 0; n < dataForLine.GetLength(0); n++)
                chart_Data.Series[2].Points.Add(new DataPoint(dataForLine[n, 0], dataForLine[n, 1]));
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label_PartialSill_Click(object sender, EventArgs e)
        {

        }
    }
}
