using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.KrigingPackage
{
    class GaussNewtonSolver
    {
        private double minimumDeltaValue;
        private double minimumDeltaParameters;
        private int maximumIterations;
        private Vector<double> guess;

        private Vector<double> dataX;
        private Vector<double> dataY;
        public GaussNewtonSolver(double minimumDeltaValue, double minimumDeltaParameters, int maximumIterations, Vector<double> guess)
        {
            this.minimumDeltaValue = minimumDeltaValue;
            this.minimumDeltaParameters = minimumDeltaParameters;
            this.maximumIterations = maximumIterations;
            this.guess = guess;
        }
        //minimumDeltaValue = 0.001，minimumDeltaPatameters = 0.001，maximumIterations = 1000，guess = [50,1.5]

        private void GetObjectiveValue(IModel model, int pointCount, Vector<double> parameters, out double value)
        {
            value = 0.0;

            double y = 0.0;

            for (int j = 0; j < pointCount; j++)
            {
                model.GetValue(
                    dataX[j],
                    parameters,
                    out y);

                value += Math.Pow(
                    y - dataY[j],
                    2.0);
            }

            value *= 0.5;
        }

        private void GetObjectiveJacobian(IModel model, int pointCount, Vector<double> parameters, ref Matrix<double> jacobian)
        {
            int parameterCount = parameters.Count;
            //parameters 为c和a 的参数
            // fill rows of the Jacobian matrix
            // j-th row of a Jacobian is the gradient of model function in j-th measurement
            for (int j = 0; j < pointCount; j++)
            {
                Vector<double> gradient = new DenseVector(parameterCount);

                model.GetGradient(
                    dataX[j],
                    parameters,
                    ref gradient);
                Console.WriteLine("gradient0: " + gradient[0] + " , gradient1: " + gradient[1]);
                jacobian.SetRow(j, gradient);
            }
        }

        public void Estimate(IModel model, int pointCount, Vector<double> dataX, Vector<double> dataY, ref List<Vector<double>> iterations)
        {
            this.dataX = dataX;
            this.dataY = dataY;

            int n = guess.Count;

            Vector<double> parametersCurrent = guess;
            Vector<double> parametersNew = new DenseVector(n);

            double valueCurrent;
            double valueNew;

            GetObjectiveValue(model, pointCount, parametersCurrent, out valueCurrent);

            while (true)
            {
                Matrix<double> jacobian = new DenseMatrix(pointCount, n);
                Vector<double> residual = new DenseVector(pointCount);

                GetObjectiveJacobian(model, pointCount, parametersCurrent, ref jacobian);//构建jacobian矩阵

                model.GetResidualVector(pointCount, dataX, dataY, parametersCurrent, ref residual);//得到残差

                //Vector<double> step = jacobian.Transpose().Multiply(jacobian).Cholesky().Solve(jacobian.Transpose().Multiply(residual));//设置步数
                //我的高斯牛顿法
                Vector<double> step = (jacobian.Transpose().Multiply(jacobian)).Inverse().Multiply(jacobian.Transpose()).Multiply(residual);//设置步数
                parametersCurrent.Add(step, parametersNew);//parametersCurrent + step = parametersNew,原来是parameters Current - step = parametersNew

                GetObjectiveValue(model, pointCount, parametersNew, out valueNew);//通过model计算通过parametersNew得到的半方差的和

                iterations.Add(parametersNew);//List<Vector>中加入新的参数值向量
                Console.WriteLine("c: " + parametersNew[0] + ", a: " + parametersNew[1]);
                if (ShouldTerminate(valueCurrent, valueNew, iterations.Count, parametersCurrent, parametersNew))
                {
                    //当向量收敛时退出计算。
                    break;
                }
                //新的向量值等于当前向量
                parametersNew.CopyTo(parametersCurrent);
                valueCurrent = valueNew;
            }
        }
        //判断是收敛的条件
        private bool ShouldTerminate(double valueCurrent, double valueNew, int iterationCount, Vector<double> parametersCurrent, Vector<double> parametersNew)
        {
            return (
                       Math.Abs(valueNew - valueCurrent) <= minimumDeltaValue ||
                       parametersNew.Subtract(parametersCurrent).Norm(2.0) <= minimumDeltaParameters ||
                       iterationCount >= maximumIterations);
        }
    }

}
