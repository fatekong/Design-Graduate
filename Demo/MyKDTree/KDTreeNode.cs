using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Demo.MyKDTree
{
    class KDTreeNode
    {
        /// <summary>
        /// 分裂点
        /// </summary>
        public KD_Point DivisionPoint { get; set; }

        /// <summary>
        /// 分裂类型
        /// </summary>
        public EnumDivisionType DivisionType { get; set; }

        /// <summary>
        /// 左子节点
        /// </summary>
        public KDTreeNode LeftChild { get; set; }

        /// <summary>
        /// 右子节点
        /// </summary>
        public KDTreeNode RightChild { get; set; }
    }
}