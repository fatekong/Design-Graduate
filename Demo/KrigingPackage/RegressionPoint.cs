using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.KrigingPackage
{
    class RegressionPoint
    {
        public double distance;//距离
        public double semivariogram;//半方差
        public RegressionPoint(double distance,double semivariogram)
        {
            this.distance = distance;
            this.semivariogram = semivariogram;
        }
        public override string ToString()
        {
            return "Distance : " + distance + " , " + "semivariogram : " + semivariogram;
        }
    }
}
