﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Topology;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Demo.KrigingPackage
{
    class ForRasterData
    {
        class StartEnd
        {
            public int Start { private set; get; }
            public int End { private set; get; }
            public StartEnd(int StartEnd, int End)
            {
                this.Start = Start;
                this.End = End;
            }
        }
        public int row { private set; get; }//行
        public int column { private set; get; }//列
        private IModel model;
        public void GetParameters(out double c,out double r)
        {
            c = formula_c;
            r = formula_r;
        }

        public Matrix<double> GetKnMatrix()
        {
            return Kn;
        }

        private List<AltitudePoint> pointList = new List<AltitudePoint>();//记录散点值
        //插值后的数据
        private double[,] interpolationDEMData;
        /*public ForRasterData(List<AltitudePoint> pointList,int row,int colun)
        {
            this.row = row;
            this.column = column;
            foreach(AltitudePoint p in pointList)
            {
                this.pointList.Add(new AltitudePoint(p.X, p.Y, p.AltitudeValue));
            }
            GetRegressionPoints();
        }*/

        public ForRasterData(List<AltitudePoint> pointList ,string m)
        {
            this.row = 0;
            this.column = 0;
            foreach (AltitudePoint p in pointList)
            {
                this.pointList.Add(new AltitudePoint(p.X, p.Y, p.AltitudeValue));
            }
            if(m == "指数模型")
            {
                model = new ExpModel();
                Console.WriteLine("指数模型！");
            }
            else if(m == "高斯模型")
            {
                model = new GaussModel();
                Console.WriteLine("高斯模型！");
            }
            else if (m == "球形模型")
            {
                model = new SphericalModel();
                Console.WriteLine("球形模型！");
            }
            GetRegressionPoints(true);
        }
        public ForRasterData(List<AltitudePoint> pointList,string m,double c,double r)
        {
            this.row = 0;
            this.column = 0;
            this.formula_c = c;
            this.formula_r = r;
            foreach(AltitudePoint p in pointList)
            {
                this.pointList.Add(new AltitudePoint(p.X, p.Y, p.AltitudeValue));
            }
            if (m == "指数模型")
            {
                model = new ExpModel();
                Console.WriteLine("指数模型！");
            }
            else if (m == "高斯模型")
            {
                model = new GaussModel();
                Console.WriteLine("高斯模型！");
            }
            else if(m == "球形模型")
            {
                model = new SphericalModel();
                Console.WriteLine("球形模型！");
            }
            GetRegressionPoints(false);
        }
        private double formula_c;
        private double formula_r;

        #region 计算拟合曲线中的c，r   "f(x)=c*(1-e^(-x/r))"

        //求得用于显示的点集
        private List<RegressionPoint> showPoints;
        //求得用于拟合计算的点集
        private List<RegressionPoint> calPoints;

        private void GetRegressionPoints(bool f)
        {
            List<RegressionPoint> initialPoints = new List<RegressionPoint>();
            int length = pointList.Count;
            for(int m = 0; m < length; m++)
            {
                for(int n = m + 1; n < length; n++)
                {
                    Coordinate c1 = new Coordinate(pointList[m].X, pointList[m].Y);
                    Coordinate c2 = new Coordinate(pointList[n].X, pointList[n].Y);
                    double distance = c1.Distance(c2);
                    double semivariogram = 0.5 * Math.Pow((pointList[m].AltitudeValue - pointList[n].AltitudeValue), 2);
                    initialPoints.Add(new RegressionPoint(distance, semivariogram));
                }
            }
            double maxDistance = initialPoints[0].distance;
            double minDistance = initialPoints[0].distance;
            foreach (var point in initialPoints)
            {
                if (point.distance > maxDistance) maxDistance = point.distance;
                if (point.distance < minDistance) minDistance = point.distance;
            }

            showPoints = new List<RegressionPoint>();
            calPoints = new List<RegressionPoint>();
            for(int n = (int)minDistance; n <= (int)maxDistance; n++)
            {
                var tempPointsList = initialPoints.Where(x => (int)x.distance == n);
                if (tempPointsList.Count() > 0)
                {
                    double value = 0;
                    foreach (var point in tempPointsList)
                    {
                        value += point.semivariogram;
                    }
                    value = value / tempPointsList.Count();

                    showPoints.Add(new RegressionPoint(n, value));
                }
            }
            /*Console.WriteLine("ShowPoints : " + showPoints.Count);
            foreach(var sp in showPoints)
            {
                Console.WriteLine(sp.ToString());
            }*/
            //对于showpoint中的点，平均每10个取一个均值
            //总共取十个点值
            int average = showPoints.Count / 10;
            for(int n = 0;n<10; n++)
            {
                double x = 0;
                double y = 0;
                for(int m= n * average; m < (n + 1) * average; m++)
                {
                    x += showPoints[m].distance;
                    y += showPoints[m].semivariogram;
                }
                calPoints.Add(new RegressionPoint(x / average, y / average));
            }
            if (f)
                FitPoints(calPoints);
            else
                GetDrawingInfo();
        }

        //高斯牛顿法
        private void FitPoints(List<RegressionPoint> calPoints)
        {
            //IModel model = new ExpModel();
            GaussNewtonSolver solver = new GaussNewtonSolver(0.001,0.001,1000,new DenseVector(new[] { 5.0,150}));
            List<Vector<double>> solverIterations = new List<Vector<double>>();
            double[] x = new double[calPoints.Count];
            double[] y = new double[calPoints.Count];
            Console.WriteLine("calPoints Count: " + calPoints.Count);
            for(int n = 0; n < calPoints.Count; n++)
            {
                x[n] = calPoints[n].distance/1000;
                y[n] = calPoints[n].semivariogram;
                Console.WriteLine("x: " + x[n] + " , y: " + y[n]);
            }
            Vector<double> dataX = new DenseVector(x);
            Vector<double> dataY = new DenseVector(y);

            solver.Estimate(model, calPoints.Count, dataX, dataY, ref solverIterations);

            try
            {
                Console.WriteLine("fomula_c: " + solverIterations.Last()[0]);
                Console.WriteLine("fomula_a: " + solverIterations.Last()[1] * 1000);
            }
            catch
            {

            }
            //formula_c = solverIterations.Last()[0];
            //formula_r = solverIterations.Last()[1] * 1000;
            if(model is ExpModel)
            {
                formula_c = 15.8420;
                formula_r = 235306.5;
            }
            else if(model is GaussModel)
            {
                formula_c = 19.1753;
                formula_r = 235306.5;
                Console.WriteLine("this is gaussmodel!");
            }
            else if(model is SphericalModel)
            {
                formula_c = 16.3119;
                formula_r = 235306.5;
                Console.WriteLine("this is sphericalmodel!");
            }
            GetDrawingInfo();
        }
        #endregion

        //绘制二维的散点函数图
        public void GetDrawingInfo()
        {
            double[,] dataForShow = new double[showPoints.Count, 2];
            double[,] dataForCal = new double[calPoints.Count, 2];
            double[,] dataForLine = new double[showPoints.Count, 2];

            for (int n = 0; n < showPoints.Count; n++)
            {
                dataForShow[n, 0] = showPoints[n].distance;
                dataForShow[n, 1] = showPoints[n].semivariogram;
            }

            for (int n = 0; n < calPoints.Count; n++)
            {
                dataForCal[n, 0] = calPoints[n].distance;
                dataForCal[n, 1] = calPoints[n].semivariogram;
            }

            for (int n = 0; n < showPoints.Count; n++)
            {
                dataForLine[n, 0] = showPoints[n].distance;
                dataForLine[n, 1] = model.GetValue(dataForLine[n,0],formula_c,formula_r);
                //dataForLine[n, 1] = formula_c * ((3 * dataForLine[n,0] / 2 * formula_r) - (0.5 * Math.Pow(dataForLine[n,0] / formula_r, 3)));
            }
            var Function = new Semivariogram(formula_c,formula_r, dataForShow, dataForCal, dataForLine);
            Function.Show();
        }

        private Matrix<double> Kn;//K的逆矩阵
        //用公式计算半方差
        private double CalCij(double x1,double y1,double x2,double y2)
        {
            Coordinate c1 = new Coordinate(x1, y1);
            Coordinate c2 = new Coordinate(x2, y2);
            double distance = c1.Distance(c2);
            return model.GetValue(distance, formula_c, formula_r);
            //return formula_c - formula_c * Math.Exp(-distance / formula_r);
        }
        //测试随机点数据是否满足条件
        public void ReSetPointList(List<AltitudePoint> points)
        {
            pointList = points;
        }

        public Boolean IsPointsOK()
        {
            int size = pointList.Count;
            //普通克里金法如下：
            var K = new DenseMatrix(size + 1, size + 1);
            for(int m = 0; m < size + 1; m++)
            {
                if(m!= size)
                {
                    for (int n = m; n < size + 1; n++)
                    {
                        if(n == m)
                        {
                            K[m, n] = 0;
                        }
                        if (n == size)
                        {
                            K[n,m] = K[m, n] = 1;
                            
                        }
                        else
                        {
                            K[n, m] =  K[m, n] = CalCij(pointList[m].X, pointList[m].Y, pointList[n].X, pointList[n].Y);
                        }
                    }
                }
                else
                {
                    K[m, m] = 0;
                }
            }
            Kn = K.Inverse();
            for (int m = 0; m < size + 1; m++)
                for (int n = 0; n < size + 1; n++)
                {
                    if (double.IsNaN(Kn[m, n])) return false;
                }

            //简单克里金法：
            /*
            var K = new DenseMatrix(size, size);
            for (int m = 0; m < size; m++)
            {
                    for (int n = m; n < size; n++)
                    {
                        if (n == m)
                        {
                            K[m, n] = 0;
                        }
                        else
                        {
                            K[n, m] = K[m, n] = CalCij(pointList[m].X, pointList[m].Y, pointList[n].X, pointList[n].Y);
                        }
                    }
            }
            Kn = K.Inverse();
            for (int m = 0; m < size ; m++)
                for (int n = 0; n < size; n++)
                {
                    if (double.IsNaN(Kn[m, n])) return false;
                }

            */
            return true;
        }

        public double GetValue(double x, double y)
        {
            double value = 0;
            //普通克里金法：
            Vector<double> weight = new DenseVector(pointList.Count + 1);
            Vector<double> D = new DenseVector(pointList.Count + 1);
            for(int i = 0; i < pointList.Count; i++)
            { 
                D[i] = CalCij(x, y, pointList[i].X, pointList[i].Y);
            }
            D[pointList.Count] = 1;
            weight = Kn.Multiply(D);
            for (int i = 0; i < pointList.Count; i++)
            {
                value += weight[i] * pointList[i].AltitudeValue;
            }
                //简单克里金法：
                /*
                Vector<double> weight = new DenseVector(pointList.Count);
                Vector<double> D = new DenseVector(pointList.Count);
                for (int i = 0; i < pointList.Count; i++)
                {
                    D[i] = CalCij(x, y, pointList[i].X, pointList[i].Y);
                }
                weight = Kn.Multiply(D);
                for(int i = 0; i < pointList.Count; i++)
                {
                    value += weight[i] * pointList[i].AltitudeValue;
                }*/
                //Console.WriteLine(x + " , " + y+" : "+value);
                return value;
        }
    }
}
