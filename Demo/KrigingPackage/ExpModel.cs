using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Demo.KrigingPackage
{
    class ExpModel : IModel
    {
        public void GetValue(double x, Vector<double> parameters, out double y)
        {
            //parameter[0] -> c , parameter[1] -> a/r
            y = parameters[0] * (1 - Math.Exp(-(x / parameters[1])));
        }
        public void GetGradient(double x, Vector<double> parameters, ref Vector<double> gradient)
        {
            //原来的偏导求解方法
            //gradient[0] = Math.Pow(x, parameters[1]);//c
            //gradient[1] = (parameters[0] * Math.Pow(x, parameters[1]) * Math.Log(x));//r
            //我的偏导求法如下：
            gradient[0] = 1 - Math.Exp(-x / parameters[1]);
            gradient[1] = -parameters[0] * x / Math.Pow(parameters[1], 2) * Math.Exp(-x / parameters[1]);
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
