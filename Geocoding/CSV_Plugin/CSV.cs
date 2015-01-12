using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace CSV_Plugin
{
    public static class CSV
    {
        public static int get100()
        {
            return 1000;
        }

        public static DataTable importCSV(string path)
        {
            //initialising a StreamReader type variable and will pass the file location

            StreamReader oStreamReader = new StreamReader(path);

            DataTable oDataTable = null;
            int RowCount = 0;
            string[] ColumnNames = null;
            string[] oStreamDataValues = null;
            //using while loop read the stream data till end
            while (!oStreamReader.EndOfStream)
            {
                String oStreamRowData = oStreamReader.ReadLine().Trim();
                if (oStreamRowData.Length > 0)
                {
                    oStreamDataValues = oStreamRowData.Split(';');
                    //Bcoz the first row contains column names, we will poluate 
                    //the column name by
                    //reading the first row and RowCount-0 will be true only once
                    if (RowCount == 0)
                    {
                        RowCount = 1;
                        ColumnNames = oStreamRowData.Split(';');
                        oDataTable = new DataTable();

                        //using foreach looping through all the column names
                        foreach (string csvcolumn in ColumnNames)
                        {
                            DataColumn oDataColumn = new DataColumn(csvcolumn.ToUpper(), typeof(string));

                            //setting the default value of empty.string to newly created column
                            oDataColumn.DefaultValue = string.Empty;

                            //adding the newly created column to the table
                            oDataTable.Columns.Add(oDataColumn);
                        }
                    }
                    else
                    {
                        //creates a new DataRow with the same schema as of the oDataTable            
                        DataRow oDataRow = oDataTable.NewRow();

                        //using foreach looping through all the column names
                        for (int i = 0; i < ColumnNames.Length; i++)
                        {
                            oDataRow[ColumnNames[i]] = oStreamDataValues[i] == null ? string.Empty : oStreamDataValues[i].ToString();
                        }

                        //adding the newly created row with data to the oDataTable       
                        oDataTable.Rows.Add(oDataRow);
                    }
                }
            }


            //close the oStreamReader object
            oStreamReader.Close();
            //release all the resources used by the oStreamReader object
            oStreamReader.Dispose();

            return oDataTable;


            ////Looping through all the rows in the Datatable
            //foreach (DataRow oDataRow in oDataTable.Rows)
            //{

            //    string RowValues = string.Empty;

            //    //Looping through all the columns in a row

            //    foreach (string csvcolumn in ColumnNames)
            //    {
            //        //concatenating the values for display purpose
            //        RowValues += csvcolumn + "=" + oDataRow[csvcolumn].ToString() + ";  ";
            //    }
            //    //Displaying the result on the console window
            //    Console.WriteLine(RowValues);
            //}
        }
    }
}
