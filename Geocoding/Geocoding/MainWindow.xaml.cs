using System;
using System.Collections.Generic;
using System.Data;
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
            MessageBox.Show(fullDirectory);

            //string fullFile = file.FullName;

            //MessageBox.Show(CSV_Plugin.CSV.get100().ToString()); //nur eine methode zum testen die 100 als int wert liefert
            //String testpath2 = "C:\\Temp\\GitHub\\geocoding-project\\Geocoding\\CSV_Plugin\\test.csv";
            String testpath = @"C:\Temp\GitHub\geocoding-project\Geocoding\CSV_Plugin\test.csv";
                        
            if (File.Exists(testpath))
            {
                myDataTable = CSV_Plugin.CSV.importCSV(testpath);
            }
            else
            {
                MessageBox.Show("File does not exist: " + testpath);
            }
            
            
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
            grid1.ItemsSource = myDataTable.DefaultView;
            //grid1.Columns[0].Header = comboColumn;
        }
    }
}
