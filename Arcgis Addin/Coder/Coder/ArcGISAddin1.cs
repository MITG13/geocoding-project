using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Newtonsoft;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;


namespace Coder
{
    public class ArcGISAddin1 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ArcGISAddin1()
        {
        }

        protected override void OnClick()
        {
            UID dockWinID = new UIDClass();
            dockWinID.Value = ThisAddIn.IDs.FHWN_GeoCoder;

            // Use GetDockableWindow directly as we want the client IDockableWindow not the internal class  
            IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
            dockWindow.Show(true);  

        }

        protected override void OnUpdate()
        {
        }
    }
}
