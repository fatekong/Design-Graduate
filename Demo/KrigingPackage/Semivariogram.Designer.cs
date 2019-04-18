namespace Demo.KrigingPackage
{
    partial class Semivariogram
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_PartialSill = new System.Windows.Forms.Label();
            this.label_MajorRange = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chart_Data = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "偏基台值：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "主变程：";
            // 
            // label_PartialSill
            // 
            this.label_PartialSill.AutoSize = true;
            this.label_PartialSill.Location = new System.Drawing.Point(99, 0);
            this.label_PartialSill.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_PartialSill.Name = "label_PartialSill";
            this.label_PartialSill.Size = new System.Drawing.Size(46, 17);
            this.label_PartialSill.TabIndex = 2;
            this.label_PartialSill.Text = "label2";
            // 
            // label_MajorRange
            // 
            this.label_MajorRange.AutoSize = true;
            this.label_MajorRange.Location = new System.Drawing.Point(255, 0);
            this.label_MajorRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_MajorRange.Name = "label_MajorRange";
            this.label_MajorRange.Size = new System.Drawing.Size(46, 17);
            this.label_MajorRange.TabIndex = 4;
            this.label_MajorRange.Text = "label4";
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.Location = new System.Drawing.Point(673, 460);
            this.button_OK.Margin = new System.Windows.Forms.Padding(4);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(100, 36);
            this.button_OK.TabIndex = 5;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label_PartialSill);
            this.panel1.Controls.Add(this.label_MajorRange);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(16, 472);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 29);
            this.panel1.TabIndex = 6;
            // 
            // chart_Data
            // 
            this.chart_Data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart_Data.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_Data.Legends.Add(legend1);
            this.chart_Data.Location = new System.Drawing.Point(16, 16);
            this.chart_Data.Margin = new System.Windows.Forms.Padding(4);
            this.chart_Data.Name = "chart_Data";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Color = System.Drawing.Color.Red;
            series1.Legend = "Legend1";
            series1.LegendText = "已知点";
            series1.Name = "已知点";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Legend = "Legend1";
            series2.LegendText = "均值点";
            series2.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            series2.MarkerSize = 12;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series2.Name = "均值点";
            series2.YValuesPerPoint = 4;
            series3.BackImageTransparentColor = System.Drawing.Color.White;
            series3.BackSecondaryColor = System.Drawing.Color.Transparent;
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series3.Legend = "Legend1";
            series3.LegendText = "拟合曲线";
            series3.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series3.Name = "拟合曲线";
            series3.ShadowOffset = 2;
            this.chart_Data.Series.Add(series1);
            this.chart_Data.Series.Add(series2);
            this.chart_Data.Series.Add(series3);
            this.chart_Data.Size = new System.Drawing.Size(757, 436);
            this.chart_Data.TabIndex = 7;
            this.chart_Data.Text = "chart1";
            // 
            // Semivariogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 512);
            this.Controls.Add(this.chart_Data);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_OK);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Semivariogram";
            this.Text = "半变异函数拟合";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label_PartialSill;
        private System.Windows.Forms.Label label_MajorRange;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Data;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}