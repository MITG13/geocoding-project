using System;
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
        }

        public void Save_Dialog()
        {
            // do it with onclick event
            // maybe move code to onclick event
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "shape files (*.shp)|*.shp|csv files (*.csv)|*.csv";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == saveFileDialog1.FileOk)
                {
                    // check here, what extension is used
                    //string ext = Path.GetExtension(saveFileDialog1.FileName);
                    switch (Path.GetExtension(saveFileDialog1.FileName))
                    {
                        case "shp":
                            ShapePlg.export_Shape(saveFileDialog1.FileName);
                            break;
                        case "csv":
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
