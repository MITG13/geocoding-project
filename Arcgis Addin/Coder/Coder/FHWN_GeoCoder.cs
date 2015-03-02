using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.ArcMapUI;
using RestSharp;
using Newtonsoft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using GeoCodingInterface;


namespace Coder
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class FHWN_GeoCoder : UserControl
    {


        public FHWN_GeoCoder(object hook)
        {
            InitializeComponent();
            this.Hook = hook;
        }

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }

        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private FHWN_GeoCoder m_windowUI;

            public AddinImpl()
            {
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new FHWN_GeoCoder(this.Hook);
                return m_windowUI.Handle;

                 

            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }

        }

        private void getProviders() {

            RestAPI requester = new RestAPI();
            servicelist providers = requester.getServices();

            foreach (string prov in providers.providers)
            {
                cmb_geocoder.Items.Add(prov);
            }

            cmb_geocoder.SelectedIndex = 0;
        
        }

        private void dockWindow_load(object sender, EventArgs e)
        {
            // Code to get List of Geocoders goes here:
            getProviders();

        }

        private void btn_geocode_Click(object sender, EventArgs e)
        {
            string adress = txt_address.Text;

            RestAPI request = new RestAPI();
            codingObject myadress = new codingObject();

            myadress.properties = new addressdata();
            myadress.properties.address = adress;

            codingObject thoseCoords = request.getCoordinates(cmb_geocoder.SelectedItem.ToString(), myadress);

            txt_lat.Clear();
            txt_lng.Clear();

            txt_lat.Text = thoseCoords.geometry.coordinates[0];
            txt_lng.Text = thoseCoords.geometry.coordinates[1];

            drawAddressPoint(Convert.ToDouble(thoseCoords.geometry.coordinates[0]), Convert.ToDouble(thoseCoords.geometry.coordinates[0]));

        }

        private void drawAddressPoint(double lat, double lng)
        {

            IPoint address = new PointClass();

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxdoc.ActivatedView;

            IMap map = mxdoc.FocusMap;

            IEnvelope envelope = activeView.Extent;

            ISpatialReferenceFactory spatialRefereceFactory = new SpatialReferenceEnvironmentClass();

            IGeographicCoordinateSystem geographicCoordinateSystem = spatialRefereceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
            ISpatialReference spatialReference = geographicCoordinateSystem;

            IScreenDisplay screenDisplay = activeView.ScreenDisplay;
            short screenCache = Convert.ToInt16(esriScreenCache.esriNoScreenCache);

            address.SpatialReference = spatialReference;
            address.PutCoords(lat, lng);


            ISpatialReference outgoingCoordSystem = map.SpatialReference;
            //IDisplayTransformation datumConversion = ((IActiveView)map).ScreenDisplay.DisplayTransformation.TransformCoords;
            address.Project(outgoingCoordSystem);

            IRgbColor color = new RgbColorClass();
            color.Green = 80;
            color.Red = 22;
            color.Blue = 68;

            IGraphicsContainer graphicsContainer = map as IGraphicsContainer;

            IElement element = null;

            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
            simpleMarkerSymbol.Color = color;
            simpleMarkerSymbol.Size = 15;
            simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;

            IMarkerElement markerElement = new MarkerElementClass();

            markerElement.Symbol = simpleMarkerSymbol;
            element = markerElement as IElement;

            if (!(element == null))
                {
                    element.Geometry = address;
                    //graphicsContainer.AddElement(element, 0);
                }

            IGraphicsLayer graphicsLayer = new CompositeGraphicsLayerClass();
            ((ILayer)graphicsLayer).Name = "New Layer";
            ((ILayer)graphicsLayer).SpatialReference = spatialReference;
            (graphicsLayer as IGraphicsContainer).AddElement(element, 0);
            //map.AddLayer(graphicsLayer as ILayer);  

            FlashGeometry(address, color, mxdoc.ActiveView.ScreenDisplay, 500);

            envelope.CenterAt(address);
            activeView.Extent = envelope;
            activeView.Refresh();

        }

        public void FlashGeometry(ESRI.ArcGIS.Geometry.IGeometry geometry, ESRI.ArcGIS.Display.IRgbColor color, ESRI.ArcGIS.Display.IDisplay display, System.Int32 delay)
        {
            if (geometry == null || color == null || display == null)
            {
                return;
            }

            display.StartDrawing(display.hDC, (System.Int16)ESRI.ArcGIS.Display.esriScreenCache.esriNoScreenCache); // Explicit Cast


            switch (geometry.GeometryType)
            {
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                    {
                        //Set the flash geometry's symbol.
                        ESRI.ArcGIS.Display.ISimpleFillSymbol simpleFillSymbol = new ESRI.ArcGIS.Display.SimpleFillSymbolClass();
                        simpleFillSymbol.Color = color;
                        ESRI.ArcGIS.Display.ISymbol symbol = simpleFillSymbol as ESRI.ArcGIS.Display.ISymbol; // Dynamic Cast
                        symbol.ROP2 = ESRI.ArcGIS.Display.esriRasterOpCode.esriROPNotXOrPen;

                        //Flash the input polygon geometry.
                        display.SetSymbol(symbol);
                        display.DrawPolygon(geometry);
                        System.Threading.Thread.Sleep(delay);
                        display.DrawPolygon(geometry);
                        break;
                    }

                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                    {
                        //Set the flash geometry's symbol.
                        ESRI.ArcGIS.Display.ISimpleLineSymbol simpleLineSymbol = new ESRI.ArcGIS.Display.SimpleLineSymbolClass();
                        simpleLineSymbol.Width = 4;
                        simpleLineSymbol.Color = color;
                        ESRI.ArcGIS.Display.ISymbol symbol = simpleLineSymbol as ESRI.ArcGIS.Display.ISymbol; // Dynamic Cast
                        symbol.ROP2 = ESRI.ArcGIS.Display.esriRasterOpCode.esriROPNotXOrPen;

                        //Flash the input polyline geometry.
                        display.SetSymbol(symbol);
                        display.DrawPolyline(geometry);
                        System.Threading.Thread.Sleep(delay);
                        display.DrawPolyline(geometry);
                        break;
                    }

                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                    {
                        //Set the flash geometry's symbol.
                        ESRI.ArcGIS.Display.ISimpleMarkerSymbol simpleMarkerSymbol = new ESRI.ArcGIS.Display.SimpleMarkerSymbolClass();
                        simpleMarkerSymbol.Style = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSCircle;
                        simpleMarkerSymbol.Size = 12;
                        simpleMarkerSymbol.Color = color;
                        ESRI.ArcGIS.Display.ISymbol symbol = simpleMarkerSymbol as ESRI.ArcGIS.Display.ISymbol; // Dynamic Cast
                        symbol.ROP2 = ESRI.ArcGIS.Display.esriRasterOpCode.esriROPNotXOrPen;

                        //Flash the input point geometry.
                        display.SetSymbol(symbol);
                        display.DrawPoint(geometry);
                        System.Threading.Thread.Sleep(delay);
                        display.DrawPoint(geometry);
                        break;
                    }

                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryMultipoint:
                    {
                        //Set the flash geometry's symbol.
                        ESRI.ArcGIS.Display.ISimpleMarkerSymbol simpleMarkerSymbol = new ESRI.ArcGIS.Display.SimpleMarkerSymbolClass();
                        simpleMarkerSymbol.Style = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSCircle;
                        simpleMarkerSymbol.Size = 12;
                        simpleMarkerSymbol.Color = color;
                        ESRI.ArcGIS.Display.ISymbol symbol = simpleMarkerSymbol as ESRI.ArcGIS.Display.ISymbol; // Dynamic Cast
                        symbol.ROP2 = ESRI.ArcGIS.Display.esriRasterOpCode.esriROPNotXOrPen;

                        //Flash the input multipoint geometry.
                        display.SetSymbol(symbol);
                        display.DrawMultipoint(geometry);
                        System.Threading.Thread.Sleep(delay);
                        display.DrawMultipoint(geometry);
                        break;
                    }
            }
            display.FinishDrawing();
        }

    }
}
