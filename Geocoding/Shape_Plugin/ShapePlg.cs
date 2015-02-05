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
                foreach (DataColumn column in table.Columns)
                {
                    fs.DataTable.Columns.Add(column);
                }
                fs.DataTable.Columns.Add("geom", typeof(Point));
                foreach (DataRow item in table.Rows)
	            {
                    // get x and y coordinates from (DataTable)table
                    // coordinates array will contain x and y
                    // check if coordinates are present
                    if (item["coordinates"] != null)
                    {
                        double[] coordArray = item["coordiantes"] as double[];                        
                        Coordinate coordinate = new Coordinate(coordArray[0] as double? ?? 0.0, coordArray[1] as double? ?? 0.0);
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
	            }
                fs.DataTable.Columns.Remove("coordinates");
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
