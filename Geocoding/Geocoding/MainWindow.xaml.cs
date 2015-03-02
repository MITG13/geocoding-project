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
using RestSharp;
using Newtonsoft;
using GeoCodingInterface;
using CSV_Plugin;

namespace Geocoding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable myDataTable;
        RestAPI test;
        servicelist providers;

        public MainWindow()
        {
            InitializeComponent();

            test = new RestAPI();
            providers = test.getServices();
            foreach (string prov in providers.providers)
            {
                ComboBoxProvider.Items.Add(prov);
            }
            ComboBoxProvider.SelectedIndex = 0;
            //map_dotNet.AddLayer();
            //FELIX TEST "CSV_Plugin"
            string currentDir = Environment.CurrentDirectory;
            DirectoryInfo directory = new DirectoryInfo(currentDir);
            string fullDirectory = directory.FullName;
            map_dotNet.Layers.Add(BruTileLayer.CreateBingHybridLayer());
            string path = System.IO.Directory.GetCurrentDirectory();
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
                            myDataTable = CSV.importCSV(openFileDialog1.FileName);
                            grid1.ItemsSource = myDataTable.DefaultView;
                            break;
                        default:
                            break;
                    }
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
                            if (ShapePlg.export_Shape(myDataTable, saveFileDialog1.FileName))
                            {
                                MessageBox.Show("Shapefile created!");
                            }
                            else MessageBox.Show("Table is empty!");
                            break;
                        case ".csv":
                            //code for CSV export
                            if (CSV.exportCSV(myDataTable, saveFileDialog1.FileName))
                            {
                                MessageBox.Show("CSV created!");
                            }
                            else MessageBox.Show("Table is empty!");
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
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

        private void GeocodeClick(object sender, RoutedEventArgs e)
        {
            FeatureSet _myPoints = new FeatureSet(FeatureType.Point);

            if (CheckBoxReverse.IsChecked == false)
            {
                normalGeocoder(_myPoints);
            }
            else reverseGeocoder(_myPoints);

            IMapPointLayer pointLayer = map_dotNet.Layers.Add(_myPoints) as IMapPointLayer;
            if (pointLayer != null)
            {
                map_dotNet.ViewExtents = pointLayer.DataSet.Extent;
            }

            grid1.ItemsSource = null;
            grid1.ItemsSource = myDataTable.DefaultView;
        }

        public void normalGeocoder(FeatureSet _myPoints)
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
                codingObject myadress = new codingObject();
                myadress.properties = new addressdata();
                //myadress.properties.address = "Sonnbergstraße 58, 2380 Perchtoldsdorf, Austria";
                myadress.properties.address = allValuesInString;

                codingObject thoseCoords = test.getCoordinates(ComboBoxProvider.SelectedItem.ToString(), myadress);

                //MessageBox.Show(allValuesInString);  

                //myDataTable.Rows[2]["STREET"] = "yoyoyo";
                if (!myDataTable.Columns.Contains("x"))
                {
                    myDataTable.Columns.Add("x", typeof(String));
                }
                if (!myDataTable.Columns.Contains("y"))
                {
                    myDataTable.Columns.Add("y", typeof(String));
                }

                if (thoseCoords.geometry != null)
                {
                    //MessageBox.Show("x: " + thoseCoords.geometry.coordinates.First().ToString()
                    // + " y: " + thoseCoords.geometry.coordinates[1].ToString());
                    double x = Convert.ToDouble(thoseCoords.geometry.coordinates[0]);
                    double y = Convert.ToDouble(thoseCoords.geometry.coordinates[1]);
                    Coordinate c = new Coordinate(x, y);
                    _myPoints.Features.Add(c);
                    raw["x"] = x.ToString();
                    raw["y"] = y.ToString();
                }
            }

        }


        public void reverseGeocoder(FeatureSet _myPoints)
        {
            if (myDataTable.Columns.Contains("x") && myDataTable.Columns.Contains("y") ||
                myDataTable.Columns.Contains("lon") && myDataTable.Columns.Contains("lat"))
            {
                foreach (DataRow raw in myDataTable.Rows)
                {
                    codingObject myadress = new codingObject();
                    myadress.properties = new addressdata();
                    myadress.geometry = new geometrydetails();
                    myadress.geometry.coordinates = new List<string>();

                    if (raw["x"] != null && myDataTable.Columns.Contains("x"))
                    {
                        myadress.geometry.coordinates.Add(raw["x"].ToString().Replace(',', '.'));
                    }
                    if (raw["y"] != null && myDataTable.Columns.Contains("y"))
                    {
                        myadress.geometry.coordinates.Add(raw["y"].ToString().Replace(',', '.'));
                    }
                    codingObject thoseCoords = test.getAdress(providers.providers[0], myadress);


                    if (!myDataTable.Columns.Contains("adress"))
                    {
                        myDataTable.Columns.Add("adress", typeof(String));
                    }

                    if (thoseCoords.properties != null)
                    {
                        double x = Convert.ToDouble(thoseCoords.geometry.coordinates[0]);
                        double y = Convert.ToDouble(thoseCoords.geometry.coordinates[1]);
                        Coordinate c = new Coordinate(x, y);
                        _myPoints.Features.Add(c);
                        raw["adress"] = thoseCoords.properties.address;
                    }
                }
            }
            else MessageBox.Show("Keine Koordinaten vorhanden");
        }
    }
}
