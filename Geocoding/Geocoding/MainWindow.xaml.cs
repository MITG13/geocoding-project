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

            //FELIX TEST "CSV_Plugin"

            string currentDir = Environment.CurrentDirectory;
            DirectoryInfo directory = new DirectoryInfo(currentDir);
            string fullDirectory = directory.FullName;
            //MessageBox.Show(fullDirectory);

            //string fullFile = file.FullName;

            //MessageBox.Show(CSV_Plugin.CSV.get100().ToString()); //nur eine methode zum testen die 100 als int wert liefert
            //String testpath2 = "C:\\Temp\\GitHub\\geocoding-project\\Geocoding\\CSV_Plugin\\test.csv";
            String testpath = @"C:\Users\Ilja\Documents\git\Geocoding\CSV_Plugin\test.csv";
                        
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
                            ShapePlg.import_Shape(openFileDialog1.FileName);
                            break;
                        case ".csv":
                            //code for CSV export
                            myDataTable = CSV_Plugin.CSV.importCSV(openFileDialog1.FileName);
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
                            //ShapePlg.export_Shape(grid1,saveFileDialog1.FileName);
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
    }
}
