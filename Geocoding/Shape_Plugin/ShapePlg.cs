using DotSpatial.Data;
using DotSpatial.Topology;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape_Plugin
{
    public static class ShapePlg
    {

        /*public static string NameOfPlugin
        {
            get { return "Shape_Plugin"; }
        }

        public static string getName()
        {
            return this.NameOfPlugin;
        }*/

        public static string test_shape(string path)
        {
            //System.Reflection.Assembly.GetCallingAssembly().Location;
            //return System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            return path;           
            //return "Shape Plugin works";
        }

        public static DataTable import_Shape(string path)
        {
            try
            {
                FeatureSet fs = (FeatureSet)FeatureSet.Open(path);
                fs.FillAttributes();
                DataTable dtOriginal = fs.DataTable;
                return dtOriginal;
                //run through each row
                /*for (int row = 0; row < dtOriginal.Rows.Count; row++)
                {
                    object[] original = dtOriginal.Rows[row].ItemArray;
                }*/
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            
        }

        public static void export_Shape(DataTable table, string save_path)
        {
            try
            {
                // define the feature type for this file
                FeatureSet fs = new FeatureSet(FeatureType.Point);


                // Add Some Columns
                // get Column Names from (DataTable)table
                // Test Case:
                fs.DataTable.Columns.Add(new DataColumn("ID", typeof(int)));
                fs.DataTable.Columns.Add(new DataColumn("Text", typeof(string)));

                // create a geometry (Point)
                // get x and y coodrinates from (DataTable)table
                double x = 14.23;
                double y = 47.234;
                Coordinate coordinate = new Coordinate(x,y);
                Point geom = new Point(coordinate);
                //Polygon geom = new Polygon(vertices);

                // add the geometry to the featureset. 
                IFeature feature = fs.AddFeature(geom);

                // now the resulting features knows what columns it has
                // add values for the columns
                // following code is for testing
                feature.DataRow.BeginEdit();
                feature.DataRow["ID"] = 1;
                feature.DataRow["Text"] = "Hello World";
                feature.DataRow.EndEdit();


                // save the feature set
                // Option 1: give absolut path
                fs.SaveAs(save_path, true);
                // Option 2: use savefiledialog in main wpf
            }
            catch (Exception ex)
            {
                //debug
                Debug.WriteLine(ex.Message);
            }
        }
        
    }


}
