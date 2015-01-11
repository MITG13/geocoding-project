using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.ArcMapUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;


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



        private void dockWindow_load(object sender, EventArgs e)
        {
            // Code to get List of Geocoders goes here:
            cmb_geocoder.Items.Add("Bing");
            cmb_geocoder.Items.Add("Google");

        }

        private void btn_geocode_Click(object sender, EventArgs e)
        {

            drawAddressPoint(16, 48);

            // get Address:

            // make REST Request:

            // get results:


            // display results:

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
                    graphicsContainer.AddElement(element, 0);
                }

            envelope.CenterAt(address);
            activeView.Extent = envelope;
            activeView.Refresh();

        }

        

    }
}
