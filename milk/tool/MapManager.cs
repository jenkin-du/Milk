using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using milk.model;
using milk;
using System.Threading;
using System.Diagnostics;

namespace GISEditor.EditTool
{
    public class MapManager
    {
        // 初始化

        public MapManager()
        {

        }

        

        //变量定义

        public static Form ToolPlatForm = null;
        private static IEngineEditor _engineEditor;
        public static IEngineEditor EngineEditor
        {
            get { return MapManager._engineEditor; }
            set { MapManager._engineEditor = value; }
        }
        // 获取颜色
        public static IRgbColor GetRgbColor(int intR, int intG, int intB)
        {
            IRgbColor pRgbColor = null;
            pRgbColor = new RgbColorClass();
            if (intR < 0) pRgbColor.Red = 0;
            else pRgbColor.Red = intR;
            if (intG < 0) pRgbColor.Green = 0;
            else pRgbColor.Green = intG;
            if (intB < 0) pRgbColor.Blue = 0;
            else pRgbColor.Blue = intB;
            return pRgbColor;
        }

        //计算两点之间X轴方向和Y轴方向上的距离
        public static bool CalDistance(IPoint lastpoint, IPoint firstpoint, out double deltaX, out double deltaY)
        {
            deltaX = 0; deltaY = 0;
            if (lastpoint == null || firstpoint == null)
                return false;
            deltaX = lastpoint.X - firstpoint.X;
            deltaY = lastpoint.Y - firstpoint.Y;
            return true;
        }
        
        // 单位转换
 
        public static double ConvertPixelsToMapUnits(IActiveView activeView, double pixelUnits)
        {
            int pixelExtent = activeView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().right
                - activeView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().left;

            double realWorldDisplayExtent = activeView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
            double sizeOfOnePixel = realWorldDisplayExtent / pixelExtent;

            return pixelUnits * sizeOfOnePixel;
        }

        //获取选择要
        public static IFeatureCursor GetSelectedFeatures(IFeatureLayer pFeatLyr)
        {
            ICursor pCursor = null;
            IFeatureCursor pFeatCur = null;
            if (pFeatLyr == null) return null;
            IFeatureSelection pFeatSel = pFeatLyr as IFeatureSelection;
            ISelectionSet pSelSet = pFeatSel.SelectionSet;
            if (pSelSet.Count == 0) return null;
            pSelSet.Search(null, false, out pCursor);
            pFeatCur = pCursor as IFeatureCursor;
            return pFeatCur;
        }

        // 获取当前地图文档所有图层集合
        public static List<ILayer> GetLayers(IMap pMap)
        {
            ILayer plyr = null;
            List<ILayer> pLstLayers = null;
            try
            {
                pLstLayers = new List<ILayer>();
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    plyr = pMap.get_Layer(i);
                    if (!pLstLayers.Contains(plyr))
                    {
                        pLstLayers.Add(plyr);
                    }
                }
            }
            catch (Exception ex)
            { }
            return pLstLayers;
        }

        // 根据图层名获取图层
        //<param name="pMap">地图文档</param>
        // <param name="sLyrName">图层名</param>
        public static ILayer GetLayerByName(IMap pMap, string sLyrName)
        {
            ILayer pLyr = null;
            ILayer pLayer = null;
            try
            {
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    pLyr = pMap.get_Layer(i);
                    if (pLyr.Name.ToUpper() == sLyrName.ToUpper())
                    {
                        pLayer = pLyr;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return pLayer;
        }


        /// <summary>
        /// 将经纬度转换成屏幕坐标XY
        /// </summary>
        /// <param name="axMap"></param>
        /// <param name="longtitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public static System.Drawing.Point getScreenCoord(AxMapControl axMap, double longtitude, double latitude)
        {

            int screenWidth = axMap.Width;
            int screenHeight = axMap.Height;

            IEnvelope worldCoord = axMap.ActiveView.ScreenDisplay.DisplayTransformation.FittedBounds;
            double maxLon = worldCoord.XMax;
            double minLon = worldCoord.XMin;
            double maxLat = worldCoord.YMax;
            double minLat = worldCoord.YMin;

            //比例因子
            double scaleX = ((maxLon - minLon) * 3600) / screenWidth;
            double scaleY = ((maxLat - minLat) * 3600) / screenHeight;

            System.Drawing.Point screenPoint = new System.Drawing.Point();
            screenPoint.X = (int)((longtitude - minLon) * 3600 / scaleX);
            screenPoint.Y = (int)((maxLat - latitude) * 3600 / scaleY);

            return screenPoint;
        }

        /// <summary>
        ///添加点图层 
        /// </summary>
        /// <param name="axMap"></param>
        /// <param name="longtitude"></param>
        /// <param name="latitude"></param>
        /// <param name="fieldsMap"></param>
        public static IFeature addPointInLayer(AxMapControl axMap, String layerName, double longtitude, double latitude)
        {

            ILayer layer = getLayerByName(axMap.Map, layerName);

            //将ILayer转换为IFeaturelayer，为了对图层上的要素进行编辑  
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            //定义一个要素集合，并获取图层的要素集合  
            IFeatureClass featureClass = featureLayer.FeatureClass;
            //定义一个实现新增要素的接口实例，并该实例作用于当前图层的要素集  
            IFeatureClassWrite writer = (IFeatureClassWrite)featureClass;
            //定义一个工作编辑工作空间，用于开启前图层的编辑状态  
            IWorkspaceEdit edit = (featureClass as IDataset).Workspace as IWorkspaceEdit;
            //定义一个IFeature实例，用于添加到当前图层上 
            IFeature feature;
            //开启编辑状态  
            edit.StartEditing(true);
            //开启编辑操作
            edit.StartEditOperation();
            //定义一个点，用来作为IFeature实例的形状属性，即shape属性  
            IPoint point;
            //下面是设置点的坐标和参考系  
            point = new PointClass();
            point.SpatialReference = axMap.SpatialReference;
            point.X = longtitude;
            point.Y = latitude;

            //将IPoint设置为IFeature的shape属性时，需要通过中间接口IGeometry转换  
            IGeometry geometry = point;
            //实例化IFeature对象， 这样IFeature对象就具有当前图层上要素的字段信息  
            feature = featureClass.CreateFeature();
            //设置IFeature对象的形状属性  
            feature.Shape = geometry;

            feature.Store();//保存IFeature对象  
            writer.WriteFeature(feature);//将IFeature对象，添加到当前图层上  
            edit.StopEditOperation();//停止编辑操作  
            edit.StopEditing(true);//关闭编辑状态，并保存修改  

            axMap.Refresh();//刷新地图 

            return feature;
        }


        /// <summary>
        ///添加点图层 
        /// </summary>
        /// <param name="axMap"></param>
        /// <param name="longtitude"></param>
        /// <param name="latitude"></param>
        /// <param name="fieldsMap"></param>
        public static IFeature addPointInLayer(AxMapControl axMap, SharedStory story)
        {
            try
            {
                double longitude = Convert.ToDouble(story.Longitude);
                double latitude = Convert.ToDouble(story.Latitude);
                String storyId = story.Id;
                String message = story.Message;

                ILayer layer = getLayerByName(axMap.Map, "story");

                //将ILayer转换为IFeaturelayer，为了对图层上的要素进行编辑  
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                //定义一个要素集合，并获取图层的要素集合  
                IFeatureClass featureClass = featureLayer.FeatureClass;
                //定义一个实现新增要素的接口实例，并该实例作用于当前图层的要素集  
                IFeatureClassWrite writer = (IFeatureClassWrite)featureClass;
                //定义一个工作编辑工作空间，用于开启前图层的编辑状态  
                IWorkspaceEdit edit = (featureClass as IDataset).Workspace as IWorkspaceEdit;
                //定义一个IFeature实例，用于添加到当前图层上 
                IFeature feature;
                //开启编辑状态  
                edit.StartEditing(true);
                //开启编辑操作
                edit.StartEditOperation();
                //定义一个点，用来作为IFeature实例的形状属性，即shape属性  
                IPoint point;
                //下面是设置点的坐标和参考系  
                point = new PointClass();
                point.SpatialReference = axMap.SpatialReference;
                point.X = longitude;
                point.Y = latitude;

                //将IPoint设置为IFeature的shape属性时，需要通过中间接口IGeometry转换  
                IGeometry geometry = point;
                //实例化IFeature对象， 这样IFeature对象就具有当前图层上要素的字段信息  
                feature = featureClass.CreateFeature();
                //设置IFeature对象的形状属性  
                feature.Shape = geometry;

                //添加字段
                int index = featureClass.FindField("story_id");
                feature.Value[index] = storyId;

               

                feature.Store();//保存IFeature对象  
                writer.WriteFeature(feature);//将IFeature对象，添加到当前图层上  
                edit.StopEditOperation();//停止编辑操作  
                edit.StopEditing(true);//关闭编辑状态，并保存修改  

                axMap.Refresh();//刷新地图 

                return feature;
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// 根据名字获取图层
        /// </summary>
        /// <param name="map"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILayer getLayerByName(IMap map, String name)
        {
            ILayer layer = null;

            //对Map中的每个图层进行判断并加载名称
            for (int i = 0; i < map.LayerCount; i++)
            {
                //如果该图层为图层组类型，则分别对所包含的每个图层进行操作
                if (map.get_Layer(i) is GroupLayer)
                {
                    //使用ICompositeLayer接口进行遍历操作
                    ICompositeLayer compositeLayer = map.get_Layer(i) as ICompositeLayer;
                    for (int j = 0; j < compositeLayer.Count; j++)
                    {

                        string layerName = compositeLayer.Layer[j].Name;
                        if (layerName == name)
                        {
                            layer = compositeLayer.Layer[j];
                            break;
                        }
                    }
                }
                else
                {
                    String layerName = map.Layer[i].Name;
                    if (layerName == name)
                    {
                        layer = map.Layer[i];
                        break;
                    }
                }
            }
            return layer;
        }


        /// <summary>
        /// 生成缓冲区
        /// </summary>
        public static List<IFeature> createBuffer(AxMapControl axMap, double bufferSize, String targetLayerName,Boolean auto)
        {
            IMap map = axMap.Map;

            ITopologicalOperator topo = null;
            IElement element = null;
            IGeometry buffer = null;
            IGeometry geo = null;
            ISelection selection = null;
            IEnumFeatureSetup enumFeatureSetup = null;
            IEnumFeature enumFeature = null;
            IFillSymbol fillSymbol = null;
            IRgbColor color = null;

            IFeature feat = null;
            IFeature feature = null;
            IFeatureLayer pFeaLayer = null;
            IFeatureClass pFeaClass = null;
            IFeatureCursor featureCursor = null;

            ISpatialFilter spatialfilter = null;

            List<IFeature> features = new List<IFeature>();

            if (map != null)
            {
                (map as IGraphicsContainer).DeleteAllElements();
            }

            ////    获得选中要素  
            selection = map.FeatureSelection;
            enumFeatureSetup = selection as IEnumFeatureSetup;
            enumFeatureSetup.AllFields = true;

            enumFeature = enumFeatureSetup as IEnumFeature;
            enumFeature.Reset();
            feat = enumFeature.Next();

            ////    遍历选中要素  
            while (feat != null)
            {
                geo = feat.ShapeCopy;
                topo = geo as ITopologicalOperator;
                buffer = topo.Buffer(bufferSize);

                element = new PolygonElementClass();
                element.Geometry = buffer;

                if (!auto)
                {
                    ////    设置缓冲区颜色  
                    fillSymbol = new SimpleFillSymbolClass();
                    color = new RgbColorClass();

                    color.Transparency = 0;
                    fillSymbol.Color = color;
                    (element as IFillShapeElement).Symbol = fillSymbol;
                    (map as IGraphicsContainer).AddElement(element, 0);
                }
               

                //////    设置空间过滤器  
                spatialfilter = new SpatialFilterClass();
                spatialfilter.Geometry = buffer;


                pFeaLayer = getLayerByName(axMap.Map, targetLayerName) as IFeatureLayer;
                pFeaClass = pFeaLayer.FeatureClass;

                switch (pFeaClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        {
                            spatialfilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                            break;
                        }
                        //case esriGeometryType.esriGeometryPolyline:
                        //    {
                        //        spatialfilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                        //        break;
                        //    }
                        //case esriGeometryType.esriGeometryPolygon:
                        //    {
                        //        spatialfilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                        //        break;
                        //    }
                }

                spatialfilter.GeometryField = pFeaClass.ShapeFieldName;
                featureCursor = pFeaClass.Search(spatialfilter, false);
                feature = featureCursor.NextFeature();


                while (feature != null)
                {


                    features.Add(feature);

                    feature = featureCursor.NextFeature();
                }

                feat = enumFeature.Next();
            }

            IActiveView activeView = map as IActiveView;
            activeView.Refresh();

            return features;
        }


        /// <summary>
        /// 删除自定图层的所有要素
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="axMap"></param>
        public static void deleteAllFeature(IFeatureLayer featureLayer, AxMapControl axMap)
        {
            //try
            //{
               
                // 定义一个要素集合，并获取图层的要素集合
                IFeatureClass featureClass = featureLayer.FeatureClass;
                ITable table = (ITable)featureClass;
                table.DeleteSearchedRows(null);
                axMap.ActiveView.Refresh();
            //}
            //catch
            //{

            //}


        }

        /// <summary>
        /// 删除自定图层的所有要素
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="axMap"></param>
        public static void deleteAllFeature(String layerName, AxMapControl axMap)
        {
            try
            {
                ILayer layer = GetLayerByName(axMap.Map, layerName);
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                // 定义一个要素集合，并获取图层的要素集合
                IFeatureClass featureClass = featureLayer.FeatureClass;
                ITable table = (ITable)featureClass;
                table.DeleteSearchedRows(null);
                axMap.ActiveView.Refresh();
            }
            catch
            {

            }


        }

        /// <summary>
        /// 从Feature类中获取值
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object getFieldByName(IFeature feature, String name)
        {
            IFeatureClass featureClass = feature.Class as IFeatureClass;
            int index = featureClass.FindField(name);

            object value = feature.Value[index];

            return value;
        }

        /// <summary>
        /// 获得当前坐标
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="axMap"></param>
        /// <param name="mainForm"></param>
        /// <returns></returns>
        public static System.Drawing.Point getCurrentCoord(IFeature feature, AxMapControl axMap, Form mainForm, MyTip tip)
        {
            //获得经纬度坐标
            IPoint point = feature.Shape as IPoint;
            System.Drawing.Point tipLocation = new System.Drawing.Point();
           

            try
            {
                if (point != null)
                {
                    //获得屏幕坐标
                    System.Drawing.Point screenPoint = getScreenCoord(axMap, point.X, point.Y);
                    System.Drawing.Point formLocation = mainForm.Location;
                    System.Drawing.Point axMapLocation = axMap.Location;

                    tipLocation = new System.Drawing.Point();
                    tipLocation.X = formLocation.X + axMapLocation.X + screenPoint.X +10;
                    tipLocation.Y = formLocation.Y + axMapLocation.Y + screenPoint.Y ;

                    return tipLocation;
                }
            }
            catch
            {
                System.Drawing.Point p = new System.Drawing.Point();
                p.X = mainForm.Location.X + axMap.Width / 2+axMap.Location.X;
                p.Y = mainForm.Location.Y + axMap.Height / 2+axMap.Location.Y;
                return p;
            }
           

            return tipLocation;
          
        }


    }
}

