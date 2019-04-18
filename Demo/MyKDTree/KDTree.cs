using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Demo.MyKDTree
{
    class KDTree
    {
        public KDTreeNode rootNode;

        private Stack<KDTreeNode> backtrackStack = new Stack<KDTreeNode>();

        public void CreateByPointList(List<KD_Point> pointList)
        {
            rootNode = CreateTreeNode(pointList);
        }

        private KDTreeNode CreateTreeNode(List<KD_Point> pointList)
        {
            if (pointList.Count > 0)
            {
                // 计算方差
                double xObtainVariance = ObtainVariance(CreateXList(pointList));
                double yObtainVariance = ObtainVariance(CreateYList(pointList));

                // 根据方差确定分裂维度
                EnumDivisionType divisionType = SortListByXOrYVariances(xObtainVariance, yObtainVariance, ref pointList);

                // 获得中位数
                KD_Point medianPoint = ObtainMedian(pointList);
                int medianIndex = pointList.Count / 2;

                // 构建节点
                KDTreeNode treeNode = new KDTreeNode()
                {
                    DivisionPoint = medianPoint,
                    DivisionType = divisionType,
                    LeftChild = CreateTreeNode(pointList.Take(medianIndex).ToList()),
                    RightChild = CreateTreeNode(pointList.Skip(medianIndex + 1).ToList())
                };
                return treeNode;
            }
            else
            {
                return null;
            }
        }
        //计算方差
        private double ObtainVariance(List<Double> numbers)
        {
            double average = numbers.Average();
            double sumValue = 0.0;
            numbers.ForEach(number =>
            {
                sumValue += Math.Pow((number - average), 2);
            });
            return sumValue / (double)numbers.Count;
        }

        //获得所有点集的x值
        private List<double> CreateXList(List<KD_Point> pointList)
        {
            List<double> list = new List<double>();
            pointList.ForEach(item => list.Add(item.X));
            return list;
        }
        //获得所有点集的x值
        private List<double> CreateYList(List<KD_Point> pointList)
        {
            List<double> list = new List<double>();
            pointList.ForEach(item => list.Add(item.Y));
            return list;
        }

        private EnumDivisionType SortListByXOrYVariances(double xVariance, double yVariance, ref List<KD_Point> pointList)
        {
            if (xVariance > yVariance)
            {
                pointList.Sort((point1, point2) => point1.X.CompareTo(point2.X));
                return EnumDivisionType.X;
            }
            else
            {
                pointList.Sort((point1, point2) => point1.Y.CompareTo(point2.Y));
                return EnumDivisionType.Y;
            }
        }

        private KD_Point ObtainMedian(List<KD_Point> pointList)
        {
            return pointList[pointList.Count / 2];
        }
        //上面是构建KD树
        //下面是寻找最近点
        public KD_Point FindNearest(KD_Point searchPoint)
        {
            KD_Point nearestPoint = DFSSearch(this.rootNode, searchPoint);
            return BacktrcakSearch(searchPoint, nearestPoint);
        }

        private KD_Point DFSSearch(KDTreeNode node, KD_Point searchPoint, bool pushStack = true)
        {
            if (pushStack == true)
            {
                backtrackStack.Push(node);
            }
            if (node.DivisionType == EnumDivisionType.X)
            {
                return DFSXsearch(node, searchPoint);
            }
            else
            {
                return DFSYsearch(node, searchPoint);
            }
        }
        //向上回溯搜索
        private KD_Point DFSBackTrackingSearch(KDTreeNode node, KD_Point searchPoint)
        {
            backtrackStack.Push(node);

            if (node.DivisionType == EnumDivisionType.X)
            {
                return DFSBackTrackingXsearch(node, searchPoint);
            }
            else
            {
                return DFSBackTrackingYsearch(node, searchPoint);
            }
        }
        //按照X节点回溯搜寻
        private KD_Point DFSBackTrackingXsearch(KDTreeNode node, KD_Point searchPoint)
        {
            if (node.DivisionPoint.X > searchPoint.X)
            {
                node.LeftChild = null;
                KD_Point rightSearchPoint = DFSBackTrackRightSearch(node, searchPoint);
                node.RightChild = null;
                return rightSearchPoint;
            }
            else
            {
                node.RightChild = null;
                KD_Point leftSearchPoint = DFSBackTrackLeftSearch(node, searchPoint);
                node.LeftChild = null;
                return leftSearchPoint;
            }
        }
        //向左节点回溯搜寻
        private KD_Point DFSBackTrackLeftSearch(KDTreeNode node, KD_Point searchPoint)
        {
            if (node.LeftChild != null)
            {
                return DFSSearch(node.LeftChild, searchPoint, false);
            }
            else
            {
                return node.DivisionPoint;
            }
        }
        //向右节点回溯搜寻
        private KD_Point DFSBackTrackRightSearch(KDTreeNode node, KD_Point searchPoint)
        {
            if (node.RightChild != null)
            {
                return DFSSearch(node.RightChild, searchPoint, false);
            }
            else
            {
                return node.DivisionPoint;
            }
        }
        //按照Y节点回溯搜寻
        private KD_Point DFSBackTrackingYsearch(KDTreeNode node, KD_Point searchPoint)
        {
            if (node.DivisionPoint.Y > searchPoint.Y)
            {
                node.LeftChild = null;
                KD_Point rightSearchPoint = DFSBackTrackRightSearch(node, searchPoint);
                node.RightChild = null;
                return rightSearchPoint;
            }
            else
            {
                node.RightChild = null;
                KD_Point leftSearchPoint = DFSBackTrackLeftSearch(node, searchPoint);
                node.LeftChild = null;
                return leftSearchPoint;
            }
        }
        //按照X节点搜寻
        private KD_Point DFSXsearch(KDTreeNode node, KD_Point searchPoint)
        {
            if (node.DivisionPoint.X > searchPoint.X)
            {
                return DFSLeftSearch(node, searchPoint);
            }
            else
            {
                return DFSRightSearch(node, searchPoint);
            }
        }
        //按照Y节点搜寻
        private KD_Point DFSYsearch(KDTreeNode node, KD_Point searchPoint)
        {
            if (node.DivisionPoint.Y > searchPoint.Y)
            {
                return DFSLeftSearch(node, searchPoint);
            }
            else
            {
                return DFSRightSearch(node, searchPoint);
            }
        }
        //向左节点搜寻
        private KD_Point DFSLeftSearch(KDTreeNode node, KD_Point searchPoint)
        {
            if (node.LeftChild != null)
            {
                return DFSSearch(node.LeftChild, searchPoint);
            }
            else
            {
                return node.DivisionPoint;
            }
        }
        //向右节点搜寻
        private KD_Point DFSRightSearch(KDTreeNode node, KD_Point searchPoint)
        {
            if (node.RightChild != null)
            {
                return DFSSearch(node.RightChild, searchPoint);
            }
            else
            {
                return node.DivisionPoint;
            }
        }
        //回溯搜索
        private KD_Point BacktrcakSearch(KD_Point searchPoint, KD_Point nearestPoint)
        {
            if (backtrackStack.IsEmpty())
            {
                return nearestPoint;
            }
            else
            {
                KDTreeNode trackNode = backtrackStack.Pop();
                double backtrackDistance = ObtainDistanFromTwoPoint(searchPoint, trackNode.DivisionPoint);
                double nearestPointDistance = ObtainDistanFromTwoPoint(searchPoint, nearestPoint);
                if (backtrackDistance < nearestPointDistance)
                {
                    KDTreeNode searchNode = new KDTreeNode()
                    {
                        DivisionPoint = trackNode.DivisionPoint,
                        DivisionType = trackNode.DivisionType,
                        LeftChild = trackNode.LeftChild,
                        RightChild = trackNode.RightChild
                    };
                    nearestPoint = DFSBackTrackingSearch(searchNode, searchPoint);
                }
                return BacktrcakSearch(searchPoint, nearestPoint);
            }
        }

        private double ObtainDistanFromTwoPoint(KD_Point start, KD_Point end)
        {
            return Math.Sqrt(Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2));
        }

        public List<KD_Point> K_Nearest(KD_Point position, int K)
        {
            KDTreeNodeCollection collection = new KDTreeNodeCollection(K, position);
            collection = K_Nearest(this.rootNode, position, collection);
            return collection.GetPoints();
        }

        public KDTreeNodeCollection K_Nearest(KDTreeNode current, KD_Point position, KDTreeNodeCollection collection)
        {
            double d = ObtainDistanFromTwoPoint(current.DivisionPoint, position);
            collection.Add(current, d);
            double value;
            double median;
            if (current.DivisionType == EnumDivisionType.X)
            {
                value = position.X;
                median = current.DivisionPoint.X;
            }
            else
            {
                value = position.Y;
                median = current.DivisionPoint.Y;
            }
            double u = value - median;
            if (u <= 0)
            {
                if (current.LeftChild != null)
                    K_Nearest(current.LeftChild, position, collection);
                if (current.RightChild != null && Math.Abs(u) <= collection.max)
                    K_Nearest(current.RightChild, position, collection);
            }
            else
            {
                if (current.RightChild != null)
                    K_Nearest(current.RightChild, position, collection);
                if (current.LeftChild != null && Math.Abs(u) <= collection.max)
                    K_Nearest(current.LeftChild, position, collection);
            }
            return collection;
        }
    }
}