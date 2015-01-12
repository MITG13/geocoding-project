using System;
using System.Data;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
            //string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase.ToString();
            string path = System.IO.Directory.GetCurrentDirectory();
            MessageBox.Show(ShapePlg.test_shape(path));
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

                if (openFileDialog1.ShowDialog() == openFileDialog1.FileOk)
                {
                    // check here, what extension is used-> use corresponding class method for import
                    switch (System.IO.Path.GetExtension(openFileDialog1.FileName))
                    {
                        case "shp":
                            ShapePlg.import(openFileDialog1.FileName);
                            break;
                        case "csv":
                            //code for CSV export
                            break;
                        default:
                            break;
                    }
                    openFileDialog1.FileName;
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

                if (saveFileDialog1.ShowDialog() == saveFileDialog1.FileOk)
                {
                    // check here, what extension is used -> use corresponding class method for export
                    switch (System.IO.Path.GetExtension(saveFileDialog1.FileName))
                    {
                        case ".shp":
                            ShapePlg.export(dataGridTable,saveFileDialog1.FileName);
                            break;
                        case ".csv":
                            //code for CSV export
                            break;
                        default:
                            break;
                    }
                        saveFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }
    }
}
