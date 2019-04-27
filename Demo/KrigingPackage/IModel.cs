using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Demo.KrigingPackage
{
    public interface IModel
    {
        //通过具体的参数值计算得到y值
        double GetValue(double x, double c, double r);
        void GetValue(double x, Vector<double> parameters, out double y);
        //牛顿高斯法得到梯度，对两个参数求偏导
        void GetGradient(double x, Vector<double> parameters, ref Vector<double> gradient);
        //求公式计算和原来值的残差
        void GetResidualVector(int pointCount, Vector<double> dataX, Vector<double> dataY, Vector<double> parameters, ref Vector<double> residual);

    }
}
