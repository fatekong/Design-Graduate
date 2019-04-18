using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.KrigingPackage
{
    class AltitudePoint
    {
        //X坐标
        public double X { private set; get; }
        //Y坐标
        public double Y { private set; get; }
        //高程值
        public double AltitudeValue { private set; get; }

        public AltitudePoint(double x, double y, double altitudeValue)
        {
            this.X = x;
            this.Y = y;
            this.AltitudeValue = altitudeValue;
        }

        public override string ToString()
        {
            return X.ToString() + "," + Y.ToString() + "," + AltitudeValue.ToString();
        }
    }
}
