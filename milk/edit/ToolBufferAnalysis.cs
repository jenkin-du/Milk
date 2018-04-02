using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Diagnostics;

namespace milk
{
    /// <summary>
    /// Summary description for ToolBufferAnalysis.
    /// </summary>
    [Guid("e5ef3a66-9262-41de-8ba9-9f61e2e9ecc1")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("milk.ToolBufferAnalysis")]
    public sealed class ToolBufferAnalysis : BaseTool
    {
        private IHookHelper m_hookHelper = null;
        public ToolBufferAnalysis()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "矢量分析"; //localizable text 
            base.m_caption = "缓冲区分析";  //localizable text 
            base.m_message = "点击选择要素生成缓冲区";  //localizable text
            base.m_toolTip = "点击选择要素生成缓冲区";  //localizable text
            base.m_name = "ToolBufferAnalysis";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;
            // TODO:  Add ToolBufferAnalysis.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            //判断MapControl是否有图层
            if (m_hookHelper.FocusMap.LayerCount <= 0)
            {
                MessageBox.Show("请先加载图层数据！");
                return;
            }
            //修改鼠标样式
            (this.m_hookHelper.Hook as MapControl).MousePointer = esriControlsMousePointer.esriPointerHand;
            // TODO: Add ToolBufferAnalysis.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button != 1 || m_hookHelper.FocusMap.LayerCount <= 0)
                return;
            IActiveView pActiveView = m_hookHelper.ActiveView;
            IGraphicsContainer pGraCont = (IGraphicsContainer)pActiveView;
            //删除地图上添加的所有Element
            pGraCont.DeleteAllElements();
            //获得点击位置并转化为点图形要素
            IPoint pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);

            for (int i = 0; i < m_hookHelper.FocusMap.LayerCount;i++ ){
                //获得地图中图层
            IFeatureLayer pFeatureLayer = m_hookHelper.FocusMap.get_Layer(i) as IFeatureLayer;
            if (pFeatureLayer == null)
            {
                return;
            }
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            //进行点击，查询图层要素
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.Geometry = pPoint;
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            IFeatureCursor featureCursor = pFeatureClass.Search(pSpatialFilter, false);
            //获得点击查询的要素
            IFeature pFeature = featureCursor.NextFeature();
            if (pFeature != null && pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
            {

               // Debug.WriteLine("========================================");
                IGeometry pGeometry = pFeature.Shape as IGeometry;
                //通过ITopologicalOperator接口进行多边形的简单化处理
                ITopologicalOperator pTopoOpe = (ITopologicalOperator)pGeometry;
                pTopoOpe.Simplify();
                //通过创建缓冲区相关
                IGeometry pBufferGeo = pTopoOpe.Buffer(200);
                //创建多边形符号样式并添加到地图上
                IScreenDisplay pdisplay = pActiveView.ScreenDisplay;
                ISimpleFillSymbol pSymbol = new SimpleFillSymbolClass();
                pSymbol.Style = esriSimpleFillStyle.esriSFSCross;
                RgbColor pColor = new RgbColorClass();
                pColor.Blue = 200;
                pColor.Red = 211;
                pColor.Green = 100;
                pSymbol.Color = (IColor)pColor;
                //创建多边形渲染效果的Element
                IFillShapeElement pFillShapeElm = new PolygonElementClass();
                IElement pElm = (IElement)pFillShapeElm;
                pElm.Geometry = pBufferGeo;
                pFillShapeElm.Symbol = pSymbol;
                //将渲染之后的多边形Element添加到地图IGraphicsContainer层中
                pGraCont.AddElement((IElement)pFillShapeElm, 0);
            }
        }
            
            //刷新地图
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            // TODO:  Add ToolBufferAnalysis.OnMouseDown implementation
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolBufferAnalysis.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolBufferAnalysis.OnMouseUp implementation
        }
        #endregion
    }
}
