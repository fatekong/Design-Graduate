namespace Demo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("IDW");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Kriging");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("空间分析", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("TIN");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("等值线处理（三角）");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("等值线处理（网格）");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("等值线处理", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("二维图表");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("三维图表");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("图表分析", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9});
            this.appManager1 = new DotSpatial.Controls.AppManager();
            this.spatialDockManager1 = new DotSpatial.Controls.SpatialDockManager();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.legend1 = new DotSpatial.Controls.Legend();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ToolManager1 = new System.Windows.Forms.TreeView();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.spatialStatusStrip1 = new DotSpatial.Controls.SpatialStatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.map1 = new DotSpatial.Controls.Map();
            this.spatialHeaderControl1 = new DotSpatial.Controls.SpatialHeaderControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.lToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCSVFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadNetCDFFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shapeFileOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creatPointFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePointFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polylineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creatPolylineFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePolylineFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creatPolygonFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creatPolygonFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            ((System.ComponentModel.ISupportInitialize)(this.spatialDockManager1)).BeginInit();
            this.spatialDockManager1.Panel1.SuspendLayout();
            this.spatialDockManager1.Panel2.SuspendLayout();
            this.spatialDockManager1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.spatialStatusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spatialHeaderControl1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // appManager1
            // 
            this.appManager1.Directories = ((System.Collections.Generic.List<string>)(resources.GetObject("appManager1.Directories")));
            this.appManager1.DockManager = this.spatialDockManager1;
            this.appManager1.HeaderControl = this.spatialHeaderControl1;
            this.appManager1.Legend = this.legend1;
            this.appManager1.Map = this.map1;
            this.appManager1.ProgressHandler = this.spatialStatusStrip1;
            this.appManager1.ShowExtensionsDialogMode = DotSpatial.Controls.ShowExtensionsDialogMode.Default;
            // 
            // spatialDockManager1
            // 
            this.spatialDockManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spatialDockManager1.Location = new System.Drawing.Point(0, 83);
            this.spatialDockManager1.Name = "spatialDockManager1";
            // 
            // spatialDockManager1.Panel1
            // 
            this.spatialDockManager1.Panel1.Controls.Add(this.tabControl1);
            // 
            // spatialDockManager1.Panel2
            // 
            this.spatialDockManager1.Panel2.Controls.Add(this.tabControl2);
            this.spatialDockManager1.Size = new System.Drawing.Size(800, 367);
            this.spatialDockManager1.SplitterDistance = 193;
            this.spatialDockManager1.TabControl1 = this.tabControl1;
            this.spatialDockManager1.TabControl2 = this.tabControl2;
            this.spatialDockManager1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(193, 367);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.legend1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(185, 338);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Legend";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // legend1
            // 
            this.legend1.BackColor = System.Drawing.Color.White;
            this.legend1.ControlRectangle = new System.Drawing.Rectangle(0, 0, 179, 332);
            this.legend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.legend1.DocumentRectangle = new System.Drawing.Rectangle(0, 0, 187, 428);
            this.legend1.HorizontalScrollEnabled = true;
            this.legend1.Indentation = 30;
            this.legend1.IsInitialized = false;
            this.legend1.Location = new System.Drawing.Point(3, 3);
            this.legend1.MinimumSize = new System.Drawing.Size(5, 5);
            this.legend1.Name = "legend1";
            this.legend1.ProgressHandler = null;
            this.legend1.ResetOnResize = false;
            this.legend1.SelectionFontColor = System.Drawing.Color.Black;
            this.legend1.SelectionHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(238)))), ((int)(((byte)(252)))));
            this.legend1.Size = new System.Drawing.Size(179, 332);
            this.legend1.TabIndex = 0;
            this.legend1.Text = "legend1";
            this.legend1.VerticalScrollEnabled = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ToolManager1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(185, 338);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tool";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ToolManager1
            // 
            this.ToolManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolManager1.Location = new System.Drawing.Point(3, 3);
            this.ToolManager1.Name = "ToolManager1";
            treeNode1.Name = "IDW";
            treeNode1.Text = "IDW";
            treeNode2.Name = "Kriging";
            treeNode2.Text = "Kriging";
            treeNode3.Name = "Analysis";
            treeNode3.Text = "空间分析";
            treeNode4.Name = "TIN";
            treeNode4.Text = "TIN";
            treeNode5.Name = "IsoLine";
            treeNode5.Text = "等值线处理（三角）";
            treeNode6.Name = "RasterLine";
            treeNode6.Text = "等值线处理（网格）";
            treeNode7.Name = "ISOLine";
            treeNode7.Text = "等值线处理";
            treeNode8.Name = "dimension2";
            treeNode8.Text = "二维图表";
            treeNode9.Name = "dimension3";
            treeNode9.Text = "三维图表";
            treeNode10.Name = "Charts";
            treeNode10.Text = "图表分析";
            this.ToolManager1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode7,
            treeNode10});
            this.ToolManager1.Size = new System.Drawing.Size(179, 332);
            this.ToolManager1.TabIndex = 0;
            this.ToolManager1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ToolManager1_NodeMouseDoubleClick_1);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(603, 367);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.spatialStatusStrip1);
            this.tabPage3.Controls.Add(this.map1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(595, 338);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Map";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // spatialStatusStrip1
            // 
            this.spatialStatusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.spatialStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.spatialStatusStrip1.Location = new System.Drawing.Point(3, 310);
            this.spatialStatusStrip1.Name = "spatialStatusStrip1";
            this.spatialStatusStrip1.ProgressBar = null;
            this.spatialStatusStrip1.ProgressLabel = this.toolStripStatusLabel1;
            this.spatialStatusStrip1.Size = new System.Drawing.Size(589, 25);
            this.spatialStatusStrip1.TabIndex = 1;
            this.spatialStatusStrip1.Text = "spatialStatusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(167, 20);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // map1
            // 
            this.map1.AllowDrop = true;
            this.map1.BackColor = System.Drawing.Color.White;
            this.map1.CollectAfterDraw = false;
            this.map1.CollisionDetection = false;
            this.map1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map1.ExtendBuffer = false;
            this.map1.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.map1.IsBusy = false;
            this.map1.IsZoomedToMaxExtent = false;
            this.map1.Legend = this.legend1;
            this.map1.Location = new System.Drawing.Point(3, 3);
            this.map1.Name = "map1";
            this.map1.ProgressHandler = null;
            this.map1.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.map1.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.map1.RedrawLayersWhileResizing = false;
            this.map1.SelectionEnabled = true;
            this.map1.Size = new System.Drawing.Size(589, 332);
            this.map1.TabIndex = 0;
            this.map1.ZoomOutFartherThanMaxExtent = false;
            this.map1.GeoMouseMove += new System.EventHandler<DotSpatial.Controls.GeoMouseArgs>(this.map1_GeoMouseMove);
            this.map1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.map1_MouseDown);
            // 
            // spatialHeaderControl1
            // 
            this.spatialHeaderControl1.ApplicationManager = this.appManager1;
            this.spatialHeaderControl1.MenuStrip = this.menuStrip1;
            this.spatialHeaderControl1.ToolbarsContainer = this.toolStripContainer1.TopToolStripPanel;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lToolStripMenuItem,
            this.shapeFileOptionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // lToolStripMenuItem
            // 
            this.lToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadCSVFileToolStripMenuItem,
            this.loadNetCDFFileToolStripMenuItem});
            this.lToolStripMenuItem.Name = "lToolStripMenuItem";
            this.lToolStripMenuItem.Size = new System.Drawing.Size(125, 27);
            this.lToolStripMenuItem.Text = "My Extensions";
            // 
            // loadCSVFileToolStripMenuItem
            // 
            this.loadCSVFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadCSVFileToolStripMenuItem.Image")));
            this.loadCSVFileToolStripMenuItem.Name = "loadCSVFileToolStripMenuItem";
            this.loadCSVFileToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.loadCSVFileToolStripMenuItem.Text = "Load CSV File";
            this.loadCSVFileToolStripMenuItem.Click += new System.EventHandler(this.loadCSVFileToolStripMenuItem_Click_1);
            // 
            // loadNetCDFFileToolStripMenuItem
            // 
            this.loadNetCDFFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadNetCDFFileToolStripMenuItem.Image")));
            this.loadNetCDFFileToolStripMenuItem.Name = "loadNetCDFFileToolStripMenuItem";
            this.loadNetCDFFileToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.loadNetCDFFileToolStripMenuItem.Text = "Load NetCDF File";
            // 
            // shapeFileOptionToolStripMenuItem
            // 
            this.shapeFileOptionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pointToolStripMenuItem,
            this.polylineToolStripMenuItem,
            this.polygonToolStripMenuItem});
            this.shapeFileOptionToolStripMenuItem.Name = "shapeFileOptionToolStripMenuItem";
            this.shapeFileOptionToolStripMenuItem.Size = new System.Drawing.Size(146, 27);
            this.shapeFileOptionToolStripMenuItem.Text = "ShapeFile Option";
            // 
            // pointToolStripMenuItem
            // 
            this.pointToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creatPointFileToolStripMenuItem,
            this.savePointFileToolStripMenuItem});
            this.pointToolStripMenuItem.Image = global::Demo.Properties.Resources.FilePoint;
            this.pointToolStripMenuItem.Name = "pointToolStripMenuItem";
            this.pointToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.pointToolStripMenuItem.Text = "Point";
            this.pointToolStripMenuItem.Click += new System.EventHandler(this.pointToolStripMenuItem_Click);
            // 
            // creatPointFileToolStripMenuItem
            // 
            this.creatPointFileToolStripMenuItem.Image = global::Demo.Properties.Resources.AddLayer;
            this.creatPointFileToolStripMenuItem.Name = "creatPointFileToolStripMenuItem";
            this.creatPointFileToolStripMenuItem.Size = new System.Drawing.Size(248, 26);
            this.creatPointFileToolStripMenuItem.Text = "Create Point ShapeFile";
            this.creatPointFileToolStripMenuItem.Click += new System.EventHandler(this.creatPointFileToolStripMenuItem_Click);
            // 
            // savePointFileToolStripMenuItem
            // 
            this.savePointFileToolStripMenuItem.Image = global::Demo.Properties.Resources.save_as_32x32;
            this.savePointFileToolStripMenuItem.Name = "savePointFileToolStripMenuItem";
            this.savePointFileToolStripMenuItem.Size = new System.Drawing.Size(248, 26);
            this.savePointFileToolStripMenuItem.Text = "Save Point ShapeFile";
            this.savePointFileToolStripMenuItem.Click += new System.EventHandler(this.savePointFileToolStripMenuItem_Click);
            // 
            // polylineToolStripMenuItem
            // 
            this.polylineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creatPolylineFileToolStripMenuItem,
            this.savePolylineFileToolStripMenuItem});
            this.polylineToolStripMenuItem.Image = global::Demo.Properties.Resources.FileLine;
            this.polylineToolStripMenuItem.Name = "polylineToolStripMenuItem";
            this.polylineToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.polylineToolStripMenuItem.Text = "Polyline";
            // 
            // creatPolylineFileToolStripMenuItem
            // 
            this.creatPolylineFileToolStripMenuItem.Image = global::Demo.Properties.Resources.AddLayer;
            this.creatPolylineFileToolStripMenuItem.Name = "creatPolylineFileToolStripMenuItem";
            this.creatPolylineFileToolStripMenuItem.Size = new System.Drawing.Size(267, 26);
            this.creatPolylineFileToolStripMenuItem.Text = "Create Polyline ShapeFile";
            this.creatPolylineFileToolStripMenuItem.Click += new System.EventHandler(this.creatPolylineFileToolStripMenuItem_Click);
            // 
            // savePolylineFileToolStripMenuItem
            // 
            this.savePolylineFileToolStripMenuItem.Image = global::Demo.Properties.Resources.save_as_32x32;
            this.savePolylineFileToolStripMenuItem.Name = "savePolylineFileToolStripMenuItem";
            this.savePolylineFileToolStripMenuItem.Size = new System.Drawing.Size(267, 26);
            this.savePolylineFileToolStripMenuItem.Text = "Save Polyline ShapeFile";
            this.savePolylineFileToolStripMenuItem.Click += new System.EventHandler(this.savePolylineFileToolStripMenuItem_Click);
            // 
            // polygonToolStripMenuItem
            // 
            this.polygonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creatPolygonFileToolStripMenuItem,
            this.creatPolygonFileToolStripMenuItem1});
            this.polygonToolStripMenuItem.Image = global::Demo.Properties.Resources.FilePolygon;
            this.polygonToolStripMenuItem.Name = "polygonToolStripMenuItem";
            this.polygonToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.polygonToolStripMenuItem.Text = "Polygon";
            // 
            // creatPolygonFileToolStripMenuItem
            // 
            this.creatPolygonFileToolStripMenuItem.Image = global::Demo.Properties.Resources.AddLayer;
            this.creatPolygonFileToolStripMenuItem.Name = "creatPolygonFileToolStripMenuItem";
            this.creatPolygonFileToolStripMenuItem.Size = new System.Drawing.Size(270, 26);
            this.creatPolygonFileToolStripMenuItem.Text = "Create Polygon ShapeFile";
            this.creatPolygonFileToolStripMenuItem.Click += new System.EventHandler(this.creatPolygonFileToolStripMenuItem_Click);
            // 
            // creatPolygonFileToolStripMenuItem1
            // 
            this.creatPolygonFileToolStripMenuItem1.Image = global::Demo.Properties.Resources.save_as_32x32;
            this.creatPolygonFileToolStripMenuItem1.Name = "creatPolygonFileToolStripMenuItem1";
            this.creatPolygonFileToolStripMenuItem1.Size = new System.Drawing.Size(270, 26);
            this.creatPolygonFileToolStripMenuItem1.Text = "Save Polygon ShapeFile";
            this.creatPolygonFileToolStripMenuItem1.Click += new System.EventHandler(this.creatPolygonFileToolStripMenuItem1_Click);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(800, 25);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(800, 83);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.SizeChanged += new System.EventHandler(this.toolStripContainer1_TopToolStripPanel_SizeChanged);
            this.toolStripContainer1.TopToolStripPanel.Click += new System.EventHandler(this.toolStripContainer1_TopToolStripPanel_Click_1);
            this.toolStripContainer1.TopToolStripPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripContainer1_TopToolStripPanel_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.spatialDockManager1);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "Form1";
            this.Text = "MyDotspatial";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.spatialDockManager1.Panel1.ResumeLayout(false);
            this.spatialDockManager1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spatialDockManager1)).EndInit();
            this.spatialDockManager1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.spatialStatusStrip1.ResumeLayout(false);
            this.spatialStatusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spatialHeaderControl1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DotSpatial.Controls.AppManager appManager1;
        private DotSpatial.Controls.SpatialHeaderControl spatialHeaderControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem lToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private DotSpatial.Controls.SpatialDockManager spatialDockManager1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DotSpatial.Controls.Legend legend1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private DotSpatial.Controls.SpatialStatusStrip spatialStatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DotSpatial.Controls.Map map1;
        private System.Windows.Forms.TreeView ToolManager1;
        private System.Windows.Forms.ToolStripMenuItem loadCSVFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadNetCDFFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shapeFileOptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creatPointFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePointFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polylineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creatPolylineFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePolylineFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creatPolygonFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creatPolygonFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}

