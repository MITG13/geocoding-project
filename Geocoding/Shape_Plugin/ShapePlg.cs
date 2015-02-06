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
        public static DataTable import_Shape(string path)
        {
            try
            {                
                FeatureSet fs = (FeatureSet)FeatureSet.Open(path);
                fs.FillAttributes();
                DataTable dtOriginal = fs.DataTable;
                return dtOriginal;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            
        }

        public static bool export_Shape(DataTable table, string save_path)
        {
            try
            {
                if (table == null)
                {
                    return false;
                }
                else
                {
                    double coordX;
                    double coordY;
                    bool isXDouble;
                    bool isYDouble;
                    // define the feature type for this file
                    FeatureSet fs = new FeatureSet(FeatureType.Point);
                    // Add WGS84 Projection as default
                    fs.Projection = DotSpatial.Projections.KnownCoordinateSystems.Geographic.World.WGS1984;

                    // Add Some Columns
                    // get Column Names from (DataTable)table
                    // Test Case:
                    foreach (DataColumn column in table.Columns)
                    {
                        //fs.DataTable.Columns.Add(column);
                        //or if above doesn't work
                        fs.DataTable.Columns.Add(new DataColumn(column.ColumnName,column.DataType));
                    }
                            
                    foreach (DataRow item in table.Rows)
	                {
                        // get x and y coordinates from (DataTable)table
                        // coordinates array will contain x and y
                        // check if coordinates are present                    
                        isXDouble = Double.TryParse(item["x"].ToString(), out coordX);
                        isYDouble = Double.TryParse(item["y"].ToString(), out coordY);

                        if (item["x"] != null && item["y"] != null && isXDouble == true && isYDouble == true)
                        {                       
                            Coordinate coordinate = new Coordinate(coordX, coordY);
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
                        // else only if all datatable entries should be exported, regardless if geometry null
                        /*else
                        {
                            Point geom = new Point();
                            IFeature feature = fs.AddFeature(geom);
                            feature.DataRow.BeginEdit();
                            //is geom index 0 or last index????
                            for (int i = 0; i < fs.DataTable.Columns.Count; i++)
                            {
                                feature.DataRow[i] = item[i];
                            }
                            feature.DataRow.EndEdit();
                        }*/
              
	                }
                
                    fs.SaveAs(save_path, true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                //debug
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        
    }


}
