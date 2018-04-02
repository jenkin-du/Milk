namespace milk
{
    partial class Main
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clickMeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图浏览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnZoomInStep = new System.Windows.Forms.ToolStripMenuItem();
            this.btnZoomOutStep = new System.Windows.Forms.ToolStripMenuItem();
            this.箭头ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPan = new System.Windows.Forms.ToolStripMenuItem();
            this.全图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemQueryByAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemQueryBySpatial = new System.Windows.Forms.ToolStripMenuItem();
            this.地图编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStartEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSaveEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEndEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelFeat = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelMove = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMoveVertex = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddVertex = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelVertex = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBufferAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axMap = new ESRI.ArcGIS.Controls.AxMapControl();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSelLayer = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMap)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(480, 318);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clickMeToolStripMenuItem,
            this.地图浏览ToolStripMenuItem,
            this.地图查询ToolStripMenuItem,
            this.地图编辑ToolStripMenuItem,
            this.btnBufferAnalysis});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(884, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // clickMeToolStripMenuItem
            // 
            this.clickMeToolStripMenuItem.Image = global::milk.Properties.Resources.plus;
            this.clickMeToolStripMenuItem.Name = "clickMeToolStripMenuItem";
            this.clickMeToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            this.clickMeToolStripMenuItem.Click += new System.EventHandler(this.clickMeToolStripMenuItem_Click);
            this.clickMeToolStripMenuItem.MouseEnter += new System.EventHandler(this.clickMeToolStripMenuItem_MouseEnter);
            // 
            // 地图浏览ToolStripMenuItem
            // 
            this.地图浏览ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnZoomInStep,
            this.btnZoomOutStep,
            this.箭头ToolStripMenuItem,
            this.btnPan,
            this.全图ToolStripMenuItem});
            this.地图浏览ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("地图浏览ToolStripMenuItem.Image")));
            this.地图浏览ToolStripMenuItem.Name = "地图浏览ToolStripMenuItem";
            this.地图浏览ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.地图浏览ToolStripMenuItem.Text = "逛逛";
            this.地图浏览ToolStripMenuItem.Visible = false;
            // 
            // btnZoomInStep
            // 
            this.btnZoomInStep.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomInStep.Image")));
            this.btnZoomInStep.Name = "btnZoomInStep";
            this.btnZoomInStep.Size = new System.Drawing.Size(124, 22);
            this.btnZoomInStep.Text = "逐级放大";
            this.btnZoomInStep.Click += new System.EventHandler(this.btnZoomInStep_Click);
            // 
            // btnZoomOutStep
            // 
            this.btnZoomOutStep.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomOutStep.Image")));
            this.btnZoomOutStep.Name = "btnZoomOutStep";
            this.btnZoomOutStep.Size = new System.Drawing.Size(124, 22);
            this.btnZoomOutStep.Text = "逐级缩小";
            this.btnZoomOutStep.Click += new System.EventHandler(this.btnZoomOutStep_Click);
            // 
            // 箭头ToolStripMenuItem
            // 
            this.箭头ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("箭头ToolStripMenuItem.Image")));
            this.箭头ToolStripMenuItem.Name = "箭头ToolStripMenuItem";
            this.箭头ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.箭头ToolStripMenuItem.Text = "箭头";
            this.箭头ToolStripMenuItem.Click += new System.EventHandler(this.箭头ToolStripMenuItem_Click);
            // 
            // btnPan
            // 
            this.btnPan.Image = ((System.Drawing.Image)(resources.GetObject("btnPan.Image")));
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(124, 22);
            this.btnPan.Text = "漫游";
            this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
            this.btnPan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPan_MouseDown);
            // 
            // 全图ToolStripMenuItem
            // 
            this.全图ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("全图ToolStripMenuItem.Image")));
            this.全图ToolStripMenuItem.Name = "全图ToolStripMenuItem";
            this.全图ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.全图ToolStripMenuItem.Text = "全图";
            this.全图ToolStripMenuItem.Click += new System.EventHandler(this.全图ToolStripMenuItem_Click);
            // 
            // 地图查询ToolStripMenuItem
            // 
            this.地图查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemQueryByAttribute,
            this.ToolStripMenuItemQueryBySpatial});
            this.地图查询ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("地图查询ToolStripMenuItem.Image")));
            this.地图查询ToolStripMenuItem.Name = "地图查询ToolStripMenuItem";
            this.地图查询ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.地图查询ToolStripMenuItem.Text = "探索";
            this.地图查询ToolStripMenuItem.Visible = false;
            // 
            // ToolStripMenuItemQueryByAttribute
            // 
            this.ToolStripMenuItemQueryByAttribute.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemQueryByAttribute.Image")));
            this.ToolStripMenuItemQueryByAttribute.Name = "ToolStripMenuItemQueryByAttribute";
            this.ToolStripMenuItemQueryByAttribute.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemQueryByAttribute.Text = "属性查询";
            this.ToolStripMenuItemQueryByAttribute.Click += new System.EventHandler(this.ToolStripMenuItemQueryByAttribute_Click);
            // 
            // ToolStripMenuItemQueryBySpatial
            // 
            this.ToolStripMenuItemQueryBySpatial.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemQueryBySpatial.Image")));
            this.ToolStripMenuItemQueryBySpatial.Name = "ToolStripMenuItemQueryBySpatial";
            this.ToolStripMenuItemQueryBySpatial.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemQueryBySpatial.Text = "空间查询";
            this.ToolStripMenuItemQueryBySpatial.Click += new System.EventHandler(this.ToolStripMenuItemQueryBySpatial_Click);
            // 
            // 地图编辑ToolStripMenuItem
            // 
            this.地图编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编辑ToolStripMenuItem,
            this.btnSelFeat,
            this.btnSelMove,
            this.btnAddFeature,
            this.btnDelFeature,
            this.btnMoveVertex,
            this.btnAddVertex,
            this.btnDelVertex,
            this.btnUndo,
            this.btnRedo});
            this.地图编辑ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("地图编辑ToolStripMenuItem.Image")));
            this.地图编辑ToolStripMenuItem.Name = "地图编辑ToolStripMenuItem";
            this.地图编辑ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.地图编辑ToolStripMenuItem.Text = "编辑";
            this.地图编辑ToolStripMenuItem.Visible = false;
            this.地图编辑ToolStripMenuItem.Click += new System.EventHandler(this.地图编辑ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStartEdit,
            this.btnSaveEdit,
            this.btnEndEdit});
            this.编辑ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("编辑ToolStripMenuItem.Image")));
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // btnStartEdit
            // 
            this.btnStartEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnStartEdit.Image")));
            this.btnStartEdit.Name = "btnStartEdit";
            this.btnStartEdit.Size = new System.Drawing.Size(124, 22);
            this.btnStartEdit.Text = "开始编辑";
            this.btnStartEdit.Click += new System.EventHandler(this.btnStartEdit_Click);
            // 
            // btnSaveEdit
            // 
            this.btnSaveEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveEdit.Image")));
            this.btnSaveEdit.Name = "btnSaveEdit";
            this.btnSaveEdit.Size = new System.Drawing.Size(124, 22);
            this.btnSaveEdit.Text = "保存编辑";
            this.btnSaveEdit.Click += new System.EventHandler(this.btnSaveEdit_Click);
            // 
            // btnEndEdit
            // 
            this.btnEndEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEndEdit.Image")));
            this.btnEndEdit.Name = "btnEndEdit";
            this.btnEndEdit.Size = new System.Drawing.Size(124, 22);
            this.btnEndEdit.Text = "停止编辑";
            this.btnEndEdit.Click += new System.EventHandler(this.btnEndEdit_Click);
            // 
            // btnSelFeat
            // 
            this.btnSelFeat.Image = ((System.Drawing.Image)(resources.GetObject("btnSelFeat.Image")));
            this.btnSelFeat.Name = "btnSelFeat";
            this.btnSelFeat.Size = new System.Drawing.Size(124, 22);
            this.btnSelFeat.Text = "选择要素";
            this.btnSelFeat.Click += new System.EventHandler(this.btnSelFeat_Click);
            // 
            // btnSelMove
            // 
            this.btnSelMove.Image = ((System.Drawing.Image)(resources.GetObject("btnSelMove.Image")));
            this.btnSelMove.Name = "btnSelMove";
            this.btnSelMove.Size = new System.Drawing.Size(124, 22);
            this.btnSelMove.Text = "移动要素";
            this.btnSelMove.Click += new System.EventHandler(this.btnSelMove_Click);
            // 
            // btnAddFeature
            // 
            this.btnAddFeature.Image = ((System.Drawing.Image)(resources.GetObject("btnAddFeature.Image")));
            this.btnAddFeature.Name = "btnAddFeature";
            this.btnAddFeature.Size = new System.Drawing.Size(124, 22);
            this.btnAddFeature.Text = "添加要素";
            this.btnAddFeature.Click += new System.EventHandler(this.btnAddFeature_Click);
            // 
            // btnDelFeature
            // 
            this.btnDelFeature.Image = ((System.Drawing.Image)(resources.GetObject("btnDelFeature.Image")));
            this.btnDelFeature.Name = "btnDelFeature";
            this.btnDelFeature.Size = new System.Drawing.Size(124, 22);
            this.btnDelFeature.Text = "删除要素";
            this.btnDelFeature.Click += new System.EventHandler(this.btnDelFeature_Click);
            // 
            // btnMoveVertex
            // 
            this.btnMoveVertex.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveVertex.Image")));
            this.btnMoveVertex.Name = "btnMoveVertex";
            this.btnMoveVertex.Size = new System.Drawing.Size(124, 22);
            this.btnMoveVertex.Text = "移动节点";
            this.btnMoveVertex.Click += new System.EventHandler(this.btnMoveVertex_Click);
            // 
            // btnAddVertex
            // 
            this.btnAddVertex.Image = ((System.Drawing.Image)(resources.GetObject("btnAddVertex.Image")));
            this.btnAddVertex.Name = "btnAddVertex";
            this.btnAddVertex.Size = new System.Drawing.Size(124, 22);
            this.btnAddVertex.Text = "添加节点";
            this.btnAddVertex.Click += new System.EventHandler(this.btnAddVertex_Click);
            // 
            // btnDelVertex
            // 
            this.btnDelVertex.Image = ((System.Drawing.Image)(resources.GetObject("btnDelVertex.Image")));
            this.btnDelVertex.Name = "btnDelVertex";
            this.btnDelVertex.Size = new System.Drawing.Size(124, 22);
            this.btnDelVertex.Text = "删除节点";
            this.btnDelVertex.Click += new System.EventHandler(this.btnDelVertex_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Image = ((System.Drawing.Image)(resources.GetObject("btnUndo.Image")));
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(124, 22);
            this.btnUndo.Text = "撤销";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Image = ((System.Drawing.Image)(resources.GetObject("btnRedo.Image")));
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(124, 22);
            this.btnRedo.Text = "恢复";
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // btnBufferAnalysis
            // 
            this.btnBufferAnalysis.Image = ((System.Drawing.Image)(resources.GetObject("btnBufferAnalysis.Image")));
            this.btnBufferAnalysis.Name = "btnBufferAnalysis";
            this.btnBufferAnalysis.Size = new System.Drawing.Size(96, 21);
            this.btnBufferAnalysis.Text = "附近人消息";
            this.btnBufferAnalysis.Visible = false;
            this.btnBufferAnalysis.Click += new System.EventHandler(this.btnBufferAnalysis_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 53);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(633, 365);
            this.splitContainer1.SplitterDistance = 238;
            this.splitContainer1.TabIndex = 5;
            // 
            // axMap
            // 
            this.axMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMap.Location = new System.Drawing.Point(0, 24);
            this.axMap.Name = "axMap";
            this.axMap.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMap.OcxState")));
            this.axMap.Size = new System.Drawing.Size(884, 471);
            this.axMap.TabIndex = 0;
            this.axMap.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMap.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl1_OnMouseUp);
            this.axMap.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            this.axMap.OnViewRefreshed += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnViewRefreshedEventHandler(this.axMap_OnViewRefreshed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(367, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "图层选择：";
            this.label1.Visible = false;
            // 
            // cmbSelLayer
            // 
            this.cmbSelLayer.FormattingEnabled = true;
            this.cmbSelLayer.Location = new System.Drawing.Point(427, 3);
            this.cmbSelLayer.Name = "cmbSelLayer";
            this.cmbSelLayer.Size = new System.Drawing.Size(85, 20);
            this.cmbSelLayer.TabIndex = 7;
            this.cmbSelLayer.Visible = false;
            this.cmbSelLayer.SelectedIndexChanged += new System.EventHandler(this.cmbSelLayer_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.Location = new System.Drawing.Point(518, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 20);
            this.button1.TabIndex = 8;
            this.button1.Text = "刷新";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 473);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 495);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbSelLayer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.axMap);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "milk";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMap)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ESRI.ArcGIS.Controls.AxMapControl axMap;
        private System.Windows.Forms.ToolStripMenuItem 地图浏览ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnZoomOutStep;
        private System.Windows.Forms.ToolStripMenuItem btnPan;
        private System.Windows.Forms.ToolStripMenuItem 地图查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemQueryByAttribute;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemQueryBySpatial;
        private System.Windows.Forms.ToolStripMenuItem btnBufferAnalysis;
        private System.Windows.Forms.ToolStripMenuItem 地图编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnStartEdit;
        private System.Windows.Forms.ToolStripMenuItem btnSaveEdit;
        private System.Windows.Forms.ToolStripMenuItem btnEndEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSelLayer;
        private System.Windows.Forms.ToolStripMenuItem btnSelFeat;
        private System.Windows.Forms.ToolStripMenuItem btnSelMove;
        private System.Windows.Forms.ToolStripMenuItem btnAddFeature;
        private System.Windows.Forms.ToolStripMenuItem btnDelFeature;
        internal System.Windows.Forms.ToolStripMenuItem btnZoomInStep;
        private System.Windows.Forms.ToolStripMenuItem btnMoveVertex;
        private System.Windows.Forms.ToolStripMenuItem btnAddVertex;
        private System.Windows.Forms.ToolStripMenuItem btnDelVertex;
        private System.Windows.Forms.ToolStripMenuItem btnUndo;
        private System.Windows.Forms.ToolStripMenuItem btnRedo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem 全图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clickMeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 箭头ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

