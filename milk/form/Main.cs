using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using QueryAndStatistics;
using GISEditor.EditTool.Tool;
using GISEditor.EditTool.Command;
using System.Threading;
using GISEditor.EditTool;
using SocketTest;
using milk.model;
using System.Diagnostics;
using milk.dao;
using SocketTest.tool;
using Newtonsoft.Json;
using SocketTest.model;
using System.Net.Sockets;
using System.Net;
using SocketTest.Configration;

namespace milk
{
    public partial class Main : Form
    {

        private string sMxdPath = Application.StartupPath;
        private List<ILayer> plstLayers = null;

        private INewEnvelopeFeedback pNewEnvelopeFeedback;

        private IMap pMap = null;
        private IActiveView pActiveView = null;
        private IFeatureLayer pCurrentLyr = null;
        private IEngineEditor pEngineEditor = null;
        private IEngineEditTask pEngineEditTask = null;
        private IEngineEditLayers pEngineEditLayers = null;

        private Boolean isBuffering;
        private List<MyTip> mStoryTips = new List<MyTip>();
        List<IFeature> mFeatures;
        private IMap mMap;

        int flag = 0;

        private double bufferSize = 0.005;

        public Main()
        {
            InitializeComponent();
            InitObject();

           
        }
        private void InitObject()
        {
            try
            {
                ChangeButtonState(false);
                pEngineEditor = new EngineEditorClass();
                MapManager.EngineEditor = pEngineEditor;
                pEngineEditTask = pEngineEditor as IEngineEditTask;
                pEngineEditLayers = pEngineEditor as IEngineEditLayers;

                mMap = axMap.Map;

                //Server server = new Server();
                //server.handler += handleMessage;
                //server.start();

            }
            catch (Exception ex)
            {

            }
        }
        ////private string sMxdPath = Application.StartupPath;
        ////private List<ILayer> plstLayers = null;
        ////private IPoint m_PointPt = null;
        //private INewEnvelopeFeedback pNewEnvelopeFeedback;
        //private IPoint m_MovePt = null;
        //private IMap pMap = null;
        //private IActiveView pActiveView = null;
        //private IFeatureLayer pCurrentLyr = null;
        //private IEngineEditor pEngineEditor = null;
        //private IEngineEditTask pEngineEditTask = null;
        //private IEngineEditLayers pEngineEditLayers = null;


        /// <summary>
        /// 处理来自服务器的消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private String handleMessage(String message)
        {
            try
            {
                Datagram data = JsonConvert.DeserializeObject<Datagram>(message);
                String request = data.Request;
                String type = data.Type;
                String jsonStream = data.JsonStream;

                Debug.WriteLine("request=" + request);
                Debug.WriteLine("type=" + type);

                if (request == "sendStory" && type == "story")
                {
                    SharedStory story = JsonConvert.DeserializeObject<SharedStory>(jsonStream);
                    DAO.insert(story);

                    IFeature feature = MapManager.addPointInLayer(axMap, story);

                    return "TURE";
                }
                else if (request == "sendImage" && type == "image")
                {

                    model.Image image = JsonConvert.DeserializeObject<model.Image>(jsonStream);
                    DAO.insertImage(image);

                    return "TURE";
                }

                if (request == "getStory" && type == "story")
                {
                    SharedStory story = JsonConvert.DeserializeObject<SharedStory>(jsonStream);
                    double longitude = Convert.ToDouble(story.Longitude);
                    double latitude = Convert.ToDouble(story.Latitude);

                    IFeature feature = MapManager.addPointInLayer(axMap, "invisibleLayer", longitude, latitude);
                    ILayer layer = MapManager.getLayerByName(axMap.Map, "invisibleLayer");

                    if (feature != null && layer != null)
                    {


                        mMap.ClearSelection();
                        mMap.SelectFeature(layer, feature);

                        mFeatures = MapManager.createBuffer(axMap, bufferSize, "story", true);
                        List<String> ids = new List<string>();

                        for (int i = 0; i < mFeatures.Count; i++)
                        {
                            String id = MapManager.getFieldByName(mFeatures[i], "story_id") as String;
                            ids.Add(id);

                        }
                        //删除添加的点
                        MapManager.deleteAllFeature("invisibleLayer", axMap);

                        List<SharedStory> stories = DAO.query(ids);

                        String jsonData = DatagramPaser.toJsonDatagram(stories, "stories", "respone");

                        Debug.WriteLine(jsonData);

                        return jsonData;
                    }
                }
                else if (request == "getImage" && type == "image")
                {
                    String imageName = jsonStream;

                    model.Image image = DAO.getImageByName(imageName);
                    String jsonString = JsonConvert.SerializeObject(image);

                    return jsonString;
                }
            }
            catch
            {
                Debug.WriteLine("error!!");
            }


            return "FALSE";
        }


        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //逐级放大
        private void btnZoomInStep_Click(object sender, EventArgs e)
        {
            IEnvelope pEnvelope;
            pEnvelope = axMap.Extent;
            pEnvelope.Expand(0.5, 0.5, true);     //这里设置放大为2倍，可以根据需要具体设置
            axMap.Extent = pEnvelope;
            axMap.ActiveView.Refresh();
        }
        //逐级缩小
        private void btnZoomOutStep_Click(object sender, EventArgs e)
        {
            IActiveView pActiveView = axMap.ActiveView;
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords((pActiveView.Extent.XMin + pActiveView.Extent.XMax) / 2, (pActiveView.Extent.YMax + pActiveView.Extent.YMin) / 2);
            IEnvelope envlope = pActiveView.Extent;
            envlope.Expand(1.5, 1.5, true);       //和放大的区别在于Expand函数的参数不同
            pActiveView.Extent.CenterAt(centerPoint);
            pActiveView.Extent = envlope;
            pActiveView.Refresh();
        }
        string pMouseOperate = null;
        private void btnPan_Click(object sender, EventArgs e)
        {
            axMap.CurrentTool = null;
            pMouseOperate = "Pan";
            axMap.MousePointer = esriControlsMousePointer.esriPointerPan;
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (axMap.MousePointer == esriControlsMousePointer.esriPointerPan)
            {
                axMap.Pan();
            }

            if (e.button == 4)
            {
                //     axMap.MousePointer = esriControlsMousePointer.esriPointerPan;
                axMap.Pan();
            }

            if (isBuffering && e.button == 1)
            {
                //选择要素
                selectFeatrue(e.x, e.y);

                if (mFeatures != null)
                {
                    mFeatures.Clear();
                }

                ////获取缓冲区的要素
                mFeatures = MapManager.createBuffer(axMap, bufferSize, "story", false);

                //删除以前生成的缓冲区要素
                for (int i = 0; i < mStoryTips.Count; i++)
                {
                    MyTip tip = mStoryTips[i];
                    tip.Close();

                }
                mStoryTips.Clear();

                List<String> ids = new List<string>();
                for (int i = 0; i < mFeatures.Count; i++)
                {
                    IFeature feature = mFeatures[i];
                    if (feature != null)
                    {
                        String id = (string)MapManager.getFieldByName(feature, "story_id");

                        MyTip storyTip = new MyTip(id);

                        storyTip.TopMost = true;
                        storyTip.Location = MapManager.getCurrentCoord(feature, axMap, this, storyTip);

                        if (isCanTipShow(storyTip))
                        {
                            storyTip.Show();
                        }

                       

                        mStoryTips.Add(storyTip);
                    }

                }
            }
        }


        /// <summary>
        /// 判断提示是否可显示
        /// </summary>
        /// <param name="tip"></param>
        /// <returns></returns>
        private bool isCanTipShow(MyTip tip)
        {
            if (tip.Location.X < Location.X + axMap.Location.X - tip.Width)
            {
                return false;
            }
            else if (tip.Location.X > Location.X + axMap.Location.X + axMap.Width)
            {
                return false;
            }
            else if (tip.Location.Y < Location.Y + axMap.Location.Y - tip.Height)
            {
                return false;
            }
            else if (tip.Location.Y > Location.Y + axMap.Location.Y + axMap.Height)
            {
                return false;
            }


            return true;
        }

        /// <summary>
        /// 选择要素
        /// </summary>
        private void selectFeatrue(int x, int y)
        {

            //try
            //{
            mMap = axMap.Map;

            //获取目标图层
            IFeatureLayer featureLayer = MapManager.getLayerByName(mMap, "invisibleLayer") as IFeatureLayer;
            //删除该图层的所有的点
            MapManager.deleteAllFeature(featureLayer, axMap);


            IFeatureClass featureClass = featureLayer.FeatureClass;
            //获取地图上的坐标点
            IPoint point = axMap.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

            IFeature feature = MapManager.addPointInLayer(axMap, "invisibleLayer", point.X, point.Y);
            mMap.ClearSelection();
            mMap.SelectFeature(featureLayer as ILayer, feature);
            axMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);

            //}
            //catch (Exception ex)
            //{

            //}

        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            //try
            //{
            //    if (pNewEnvelopeFeedback != null)
            //    {
            //        m_MovePt = (axMap.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            //        pNewEnvelopeFeedback.MoveTo(m_MovePt);
            //    }
            //}
            //catch (Exception ex)
            //{
            //}

            toolStripStatusLabel1.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.######"), e.mapY.ToString("#######.######"), axMap.MapUnits.ToString());
        }
        private void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {

            if (pNewEnvelopeFeedback != null)
            {
                IActiveView pActiveView = null;
                pActiveView = axMap.Map as IActiveView;
                IEnvelope pEnvelope = pNewEnvelopeFeedback.Stop();
                pNewEnvelopeFeedback = null;
            }
        }

        private void ToolStripMenuItemQueryByAttribute_Click(object sender, EventArgs e)
        {
            //新创建属性查询窗体
            FormQueryByAttribute formQueryByAttribute = new FormQueryByAttribute();
            //将当前主窗体中MapControl控件中的Map对象赋值给FormQueryByAttribute窗体的CurrentMap属性
            formQueryByAttribute.CurrentMap = axMap.Map;
            //显示属性查询窗体
            formQueryByAttribute.Show();
        }

        private void ToolStripMenuItemQueryBySpatial_Click(object sender, EventArgs e)
        {
            //新创建空间查询窗体
            FormQueryBySpatial formQueryBySpatial = new FormQueryBySpatial();
            //将当前主窗体中MapControl控件中的Map对象赋值给FormSelection窗体的CurrentMap属性
            formQueryBySpatial.CurrentMap = axMap.Map;
            //显示空间查询窗体
            formQueryBySpatial.Show();
        }

        private void btnBufferAnalysis_Click(object sender, EventArgs e)
        {
            if (!isBuffering)
            {
                isBuffering = true;

                btnBufferAnalysis.Text = "停止寻找";
            }
            else
            {
                isBuffering = false;
                for (int i = 0; i < mStoryTips.Count; i++)
                {
                    MyTip tip = mStoryTips[i];
                    tip.Close();

                }
                mStoryTips.Clear();

                MapManager.deleteAllFeature("invisibleLayer", axMap);

                (axMap.Map as IGraphicsContainer).DeleteAllElements();
                axMap.ActiveView.Refresh();



                btnBufferAnalysis.Text = "附近人消息";
            }
        }

        private void cmbSelLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sLyrName = cmbSelLayer.SelectedItem.ToString();
                pCurrentLyr = MapManager.GetLayerByName(pMap, sLyrName) as IFeatureLayer;
                //设置编辑目标层
                pEngineEditLayers.SetTargetLayer(pCurrentLyr, 0);

                for (int i = 0; i < axMap.LayerCount; i++)
                {
                    axMap.Map.Layer[i].Visible = false;
                }
                axMap.Map.Layer[cmbSelLayer.SelectedIndex].Visible = true;
                axMap.ActiveView.Refresh();
               // MessageBox.Show(cmbSelLayer.SelectedIndex.ToString());
                //pCurrentLyr.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("err");
            }
        }

        private void 地图编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void InitComboBox(List<ILayer> plstLyr)
        {
            cmbSelLayer.Items.Clear();
            for (int i = 0; i < plstLyr.Count; i++)
            {
                if (!cmbSelLayer.Items.Contains(plstLyr[i].Name))
                {
                    cmbSelLayer.Items.Add(plstLyr[i].Name);
                }
            }
            if (cmbSelLayer.Items.Count != 0) cmbSelLayer.SelectedIndex = 0;
        }
        private void ChangeButtonState(bool bEnable)
        {
            btnStartEdit.Enabled = !bEnable;
            btnSaveEdit.Enabled = bEnable;
            btnEndEdit.Enabled = bEnable;

           // cmbSelLayer.Enabled = bEnable;

            btnSelFeat.Enabled = bEnable;
            btnSelMove.Enabled = bEnable;
            btnAddFeature.Enabled = bEnable;
            btnDelFeature.Enabled = bEnable;
            btnUndo.Enabled = bEnable;
            btnRedo.Enabled = bEnable;
            btnMoveVertex.Enabled = bEnable;
            btnAddVertex.Enabled = bEnable;
            btnDelVertex.Enabled = bEnable;
        }
        public static string getPath(string path)
        {
            int t;
            for (t = 0; t < path.Length; t++)
            {
                if (path.Substring(t, 4) == "milk")
                {
                    break;
                }
            }
            string name = path.Substring(0, t-1);
            return name;
        }
        //开始编辑
        private void btnStartEdit_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            cmbSelLayer.Visible = true;
            button1.Visible = true;
            ChangeButtonState(true);
            plstLayers = MapManager.GetLayers(axMap.Map);
            for (int i = 0; i < plstLayers.Count; i++)
            {
                cmbSelLayer.Items.Add(plstLayers[i].Name);
            }
           
            try
            {
                if (plstLayers == null || plstLayers.Count == 0)
                {
                    MessageBox.Show("请加载编辑图层！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                pMap.ClearSelection();
                pActiveView.Refresh();
                InitComboBox(plstLayers);
                ChangeButtonState(true);
                //如果编辑已经开始，则直接退出
                if (pEngineEditor.EditState != esriEngineEditState.esriEngineStateNotEditing)
                    return;
                if (pCurrentLyr == null) return;
                //获取当前编辑图层工作空间
                IDataset pDataSet = pCurrentLyr.FeatureClass as IDataset;
                IWorkspace pWs = pDataSet.Workspace;
                //设置编辑模式，如果是ArcSDE采用版本模式
                if (pWs.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    pEngineEditor.EditSessionMode = esriEngineEditSessionMode.esriEngineEditSessionModeVersioned;
                }
                else
                {
                    pEngineEditor.EditSessionMode = esriEngineEditSessionMode.esriEngineEditSessionModeNonVersioned;
                }
                //设置编辑任务
                pEngineEditTask = pEngineEditor.GetTaskByUniqueName("ControlToolsEditing_CreateNewFeatureTask");
                pEngineEditor.CurrentTask = pEngineEditTask;// 设置编辑任务
                pEngineEditor.EnableUndoRedo(true); //是否可以进行撤销、恢复操作
                pEngineEditor.StartEditing(pWs, pMap); //开始编辑操作
            }
            catch (Exception ex)
            {
            }
        }
        //保存编辑
        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                ICommand m_saveEditCom = new SaveEditCommandClass();
                m_saveEditCom.OnCreate(axMap.Object);
                m_saveEditCom.OnClick();
            }
            catch (Exception ex)
            {
            }
        }
        //停止编辑
        private void btnEndEdit_Click(object sender, EventArgs e)
        {
            ChangeButtonState(false);
            axMap.CurrentTool = null;
            label1.Visible = false;
            cmbSelLayer.Visible = false;
            button1.Visible = false;
            for (int i = 0; i < axMap.LayerCount; i++)
            {
                axMap.Map.Layer[i].Visible = true;
            }
            //try
            //{
                ICommand m_stopEditCom = new StopEditCommandClass();
                m_stopEditCom.OnCreate(axMap.Object);
                m_stopEditCom.OnClick();
                ChangeButtonState(false);
                axMap.CurrentTool = null;
                axMap.MousePointer = esriControlsMousePointer.esriPointerDefault;
                axMap.ActiveView.Refresh();

            //}
            //catch (Exception ex)
            //{
            //}
         }
       
        
        //选择要素
        private void btnSelFeat_Click(object sender, EventArgs e)
        {
            try
            {
                ICommand m_SelTool = new SelectFeatureToolClass();
                m_SelTool.OnCreate(axMap.Object);
                m_SelTool.OnClick();
                axMap.CurrentTool = m_SelTool as ITool;
                axMap.MousePointer = esriControlsMousePointer.esriPointerArrow;
            }
            catch (Exception ex)
            {
            }
        }
        //移动要素
        private void btnSelMove_Click(object sender, EventArgs e)
        {
            try
            {
                ICommand m_moveTool = new MoveFeatureToolClass();
                m_moveTool.OnCreate(axMap.Object);
                m_moveTool.OnClick();
                axMap.CurrentTool = m_moveTool as ITool;
                axMap.MousePointer = esriControlsMousePointer.esriPointerArrow;
            }
            catch (Exception ex)
            {
            }
        }
        //添加要素
        private void btnAddFeature_Click(object sender, EventArgs e)
        {
            try
            {
                ICommand m_CreateFeatTool = new CreateFeatureToolClass();
                m_CreateFeatTool.OnCreate(axMap.Object);
                m_CreateFeatTool.OnClick();
                axMap.CurrentTool = m_CreateFeatTool as ITool;
                axMap.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            }
            catch (Exception ex)
            {
            }
        }
        //删除要素
        private void btnDelFeature_Click(object sender, EventArgs e)
        {
            try
            {
                axMap.MousePointer = esriControlsMousePointer.esriPointerArrow;
                ICommand m_delFeatCom = new DelFeatureCommandClass();
                m_delFeatCom.OnCreate(axMap.Object);
                m_delFeatCom.OnClick();
            }
            catch (Exception ex)
            {
            }
        }
        //移动节点
        private void btnMoveVertex_Click(object sender, EventArgs e)
        {
            try
            {
                ICommand m_MoveVertexTool = new MoveVertexToolClass();
                m_MoveVertexTool.OnCreate(axMap.Object);
                axMap.CurrentTool = m_MoveVertexTool as ITool;
                axMap.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            }
            catch (Exception ex)
            {
            }
        }
        //增加节点
        private void btnAddVertex_Click(object sender, EventArgs e)
        {
            try
            {
                ICommand m_AddVertexTool = new AddVertexToolClass();
                m_AddVertexTool.OnCreate(axMap.Object);
                axMap.CurrentTool = m_AddVertexTool as ITool;
                axMap.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            }
            catch (Exception ex)
            {
            }
        }
        //删除节点
        private void btnDelVertex_Click(object sender, EventArgs e)
        {
            try
            {
                ICommand m_DelVertexTool = new DelVertexToolClass();
                m_DelVertexTool.OnCreate(axMap.Object);
                axMap.CurrentTool = m_DelVertexTool as ITool;
                axMap.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            }
            catch (Exception ex)
            {
            }
        }

        private void btnPan_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            //try
            //{
                axMap.MousePointer = esriControlsMousePointer.esriPointerArrow;
                ICommand m_undoCommand = new UndoCommandClass();
                m_undoCommand.OnCreate(axMap.Object);
                m_undoCommand.OnClick();
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            //try
            //{
                axMap.MousePointer = esriControlsMousePointer.esriPointerArrow;
                ICommand m_redoCommand = new RedoCommandClass();
                m_redoCommand.OnCreate(axMap.Object);
                m_redoCommand.OnClick();
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < axMap.LayerCount; i++)
            {
                axMap.Map.Layer[i].Visible = true;
            }
            axMap.ActiveView.Refresh();
        }

        private void 全图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMap.Extent = axMap.FullExtent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pMap = axMap.Map;
            pActiveView = pMap as IActiveView;
            plstLayers = MapManager.GetLayers(pMap);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
           // this.Visible = false;

         //   Form2 form = new Form2();
         //   form.ShowDialog();

         ////   Thread.Sleep(1000);
         //   form.Close();
         //   this.Visible = true;
        }
     //   int flag = 0;
        private void clickMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
            if (flag == 0)
            {
                地图浏览ToolStripMenuItem.Visible = true;
                地图查询ToolStripMenuItem.Visible = true;
                地图编辑ToolStripMenuItem.Visible = true;
                btnBufferAnalysis.Visible = true;

                clickMeToolStripMenuItem.Image = Util.getResourceBitmap("minus");

                flag = 1;
            }
            else if (flag == 1)
            {
                flag = 0;
                地图浏览ToolStripMenuItem.Visible = false;
                地图查询ToolStripMenuItem.Visible = false;
                地图编辑ToolStripMenuItem.Visible = false;
                btnBufferAnalysis.Visible = false;

                clickMeToolStripMenuItem.Image = Util.getResourceBitmap("plus");
            }

        }

        private void 嘿嘿ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 箭头ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMap.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }

        private void axMap_OnViewRefreshed(object sender, IMapControlEvents2_OnViewRefreshedEvent e)
        {
            try
            {

                if (mFeatures != null)
                {
                    for (int i = 0; i < mStoryTips.Count && i < mFeatures.Count; i++)
                    {
                        MyTip storyTip = mStoryTips[i];
                        storyTip.Location = MapManager.getCurrentCoord(mFeatures[i], axMap, this, mStoryTips[i]);

                        if (isCanTipShow(storyTip))
                        {
                            storyTip.Show();
                        }
                        else
                        {
                            storyTip.Hide();
                        }
                    }
                }


            }
            catch
            {

            }
        }

        /// <summary>
        /// 窗体关闭时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Thread endTread = new Thread(endServer);
            //endTread.Start();
        }

        /// <summary>
        /// 关闭服务器
        /// </summary>
        private void endServer()
        {
            Server.SERVER_RUN = false;

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress myIP = IPAddress.Parse(Config.IP);
            IPEndPoint EPhost = new IPEndPoint(myIP, Config.PORT);
            socket.Connect(EPhost);

            String endStr = "@";

            byte[] sendBytes = Encoding.UTF8.GetBytes(endStr + "`");
            socket.Send(sendBytes, sendBytes.Length, SocketFlags.None);

            socket.Close();
        }


        /// <summary>
        /// 显示提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickMeToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            
        }
    }
}
