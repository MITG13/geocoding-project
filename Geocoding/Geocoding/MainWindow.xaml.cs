using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Shape_Plugin;
using Microsoft.Win32;
using System.Diagnostics;
using DotSpatial.Controls;
using BruTile;
using DotSpatial.Plugins.SimpleMap;
using DotSpatial.Plugins.WebMap;
using DotSpatial.Data;
using DotSpatial.Topology;


namespace Geocoding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable myDataTable;

        public MainWindow()
        {
            InitializeComponent();

            //map_dotNet.AddLayer();
            //FELIX TEST "CSV_Plugin"
            string currentDir = Environment.CurrentDirectory;
            DirectoryInfo directory = new DirectoryInfo(currentDir);
            string fullDirectory = directory.FullName;
            //MessageBox.Show(fullDirectory);
            map_dotNet.Layers.Add(BruTileLayer.CreateBingHybridLayer());
            //string fullFile = file.FullName;

            //MessageBox.Show(CSV_Plugin.CSV.get100().ToString()); //nur eine methode zum testen die 100 als int wert liefert
            //String testpath2 = "C:\\Temp\\GitHub\\geocoding-project\\Geocoding\\CSV_Plugin\\test.csv";
            //String testpath = @"C:\Users\Ilja\Documents\git\Geocoding\CSV_Plugin\test.csv";

            /*if (File.Exists(testpath))
            {
                //myDataTable = CSV_Plugin.CSV.importCSV(testpath);
            }
            else
            {
                MessageBox.Show("File does not exist: " + testpath);
            }*/


            //ILJA
            /*DataTable myDataTable = new DataTable();
            var comboColumn = new DataGridComboBoxColumn();
            comboColumn.Header = new ComboBox();

            myDataTable.Columns.Add("Column A");
            myDataTable.Columns.Add("Column B");

            // Add some rows to the DataTable.
            myDataTable.Rows.Add("A1", "B1");
            myDataTable.Rows.Add("A2", "B2");
            myDataTable.Rows.Add("A3", "B3");*/

            // Bind DataTable to DataGrid.

            //grid1.ItemsSource = myDataTable.DefaultView;

            //grid1.Columns[0].Header = comboColumn;



            //string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase.ToString();
            string path = System.IO.Directory.GetCurrentDirectory();
            //MessageBox.Show(ShapePlg.test_shape(path));
            // add onclick listeners for import, export, geocode
        }

        private void open_Dialog()
        {
            // do it with onclick event
            // maybe move code to onclick event
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.Filter = "shape file (*.shp)|*.shp|csv file (*.csv)|*.csv";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == true)
                {
                    // check here, what extension is used-> use corresponding class method for import
                    switch (System.IO.Path.GetExtension(openFileDialog1.FileName))
                    {
                        case ".shp":
                            myDataTable = ShapePlg.import_Shape(openFileDialog1.FileName);
                            grid1.ItemsSource = myDataTable.DefaultView;
                            break;
                        case ".csv":
                            //code for CSV export
                            myDataTable = CSV_Plugin.CSV.importCSV(openFileDialog1.FileName);
                            // CSV_Plugin.CSV.importCSV(@"C:\Users\Ilja\Documents\testexport.csv");
                            grid1.ItemsSource = myDataTable.DefaultView;
                            break;
                        default:
                            break;
                    }
                    //openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private void save_Dialog()
        {
            // do it with onclick event
            // maybe move code to onclick event
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "shape file (*.shp)|*.shp|csv file (*.csv)|*.csv";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == true)
                {
                    // check here, what extension is used -> use corresponding class method for export
                    switch (System.IO.Path.GetExtension(saveFileDialog1.FileName))
                    {
                        case ".shp":

                            ShapePlg.export_Shape(myDataTable, saveFileDialog1.FileName);
                            break;
                        case ".csv":
                            //code for CSV export
                            break;
                        default:
                            break;
                    }
                    //saveFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            open_Dialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            save_Dialog();
        }
        public static DataTable DataGridtoDataTable(DataGrid dg)
        {


            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string[] Lines = result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);
            DataTable dt = new DataTable();
            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToUpper(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0) - 1; i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                {
                    Row[f] = Fields[f];
                }
                dt.Rows.Add(Row);
            }
            return dt;

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            foreach (DataRow raw in myDataTable.Rows)
            {               
                object[] allValues = raw.ItemArray;
                String allValuesInString = "";
                for (int i = 0; i < allValues.Length; i++)
                {
                    allValuesInString += allValues[i] + ", ";
                }
                //MessageBox.Show(allValuesInString);
                
            }
            //myDataTable.Rows[2]["STREET"] = "yoyoyo";

            myDataTable.Columns.Add("x", typeof(String));
            myDataTable.Columns.Add("y", typeof(String));
           
            FeatureSet _myPoints = new FeatureSet(FeatureType.Point);
            // Randomly generate 10 points that are in the map extent
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                double x = rnd.NextDouble() * (50 - 10);
                double y = rnd.NextDouble() * (50 - 10);
                Coordinate c = new Coordinate(x, y);
                _myPoints.Features.Add(c);

                myDataTable.Rows[i]["x"] = x.ToString();
                myDataTable.Rows[i]["y"] = y.ToString();
            }
            grid1.ItemsSource = null;
            grid1.ItemsSource = myDataTable.DefaultView;

            IMapPointLayer pointLayer = map_dotNet.Layers.Add(_myPoints) as IMapPointLayer;
            if (pointLayer != null)
            {
                map_dotNet.ViewExtents = pointLayer.DataSet.Extent;
            }


        }
    }
}
