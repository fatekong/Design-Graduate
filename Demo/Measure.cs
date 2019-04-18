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
    public enum MeasureMode
    {
        Distance,
        Area,
    }
    public partial class Measure : Form
    {
        //private double _distance = 0.0;
        public MeasureMode MeasureMode { get; set; }
        public Measure()
        {
            InitializeComponent();
            MeasureMode = MeasureMode.Distance;
        }

        private void Measure_Load(object sender, EventArgs e)
        {

        }

        
    }
}
