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
				// in Datatable = dataGridView1.DataSource = dtOriginal.DefaultView;
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
                foreach (DataColumn item in table)
                {
                    fs.DataTable.Columns.Add(item);
                }
                fs.DataTable.Columns.Add("geom", typeof(Point));
                foreach (DataRow item in table)
	            {
                    // get x and y coodrinates from (DataTable)table
                    Coordinate coordinate = new Coordinate(item["x"], item["y"]);
                    Point geom = new Point(coordinate);
                    IFeature feature = fs.AddFeature(geom);
                    feature.DataRow.BeginEdit();
                    //is geom index 0 or last index????
                    for (int i = 0; i < fs.DataTable.Columns.Count; i++)
                    {
                        feature.DataRow[i] = item[i];
                    }
                    feature.DataRow.EndEdit();
	            }
                fs.DataTable.Columns.Remove("x");
                fs.DataTable.Columns.Remove("y");
                fs.SaveAs(save_path, true);
            }
            catch (Exception ex)
            {
                //debug
                Debug.WriteLine(ex.Message);
            }
        }
        
    }


}
