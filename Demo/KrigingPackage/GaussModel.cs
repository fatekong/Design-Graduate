using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Demo.KrigingPackage
{
    class GaussModel:IModel
    {
        public void GetValue(double x, Vector<double> parameters, out double y)
        {
            //parameter[0] -> c , parameter[1] -> a/r
            y = parameters[0] * (1 - Math.Exp(-Math.Pow(x /parameters[1],2)));
        }
        public double GetValue(double x, double c, double r)
        {
            return c * (1 - Math.Exp(-Math.Pow(x/r,2)));
        }
        public void GetGradient(double x, Vector<double> parameters, ref Vector<double> gradient)
        {
            gradient[0] = 1 - Math.Exp(-Math.Pow(x / parameters[1], 2));
            gradient[1] = -2 * parameters[0] * x * x / Math.Pow(parameters[1], 3) * Math.Exp(x * x / parameters[1] * parameters[1]);
        }
        public void GetResidualVector(int pointCount, Vector<double> dataX, Vector<double> dataY, Vector<double> parameters, ref Vector<double> residual)
        {
            double y;
            for (int j = 0; j < pointCount; j++)
            {
                GetValue(dataX[j], parameters, out y);//通过指数模型得到值

                residual[j] = (y - dataY[j]);
            }
        }
    }
}