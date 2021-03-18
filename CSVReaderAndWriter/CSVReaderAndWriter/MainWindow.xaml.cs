using Microsoft.Win32;
using System;
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

namespace CSVReaderAndWriter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Model> empList = new List<Model>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImportCSV(object sender, RoutedEventArgs e)
        {
            string filePath = "";

            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                filePath = openFileDlg.FileName;
            }
            
            using (StreamReader r = new StreamReader(filePath))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    // Skip the column names row
                    if (parts[0] == "EmpId") continue;

                    Model obj = new Model();
                    obj.EmpId = Convert.ToInt32(parts[0]);
                    obj.FirstName = parts[1];
                    obj.LastName = parts[2];
                    obj.Address = parts[3];
                    obj.Salary = Convert.ToInt32(parts[4]);                   
                    empList.Add(obj);
                }
            }
            employeeDataGrid.ItemsSource = null;
            employeeDataGrid.ItemsSource = empList;
            employeeDataGrid.UpdateLayout();
        }

        private void ExportCSV(object sender, RoutedEventArgs e)
        {
            string csvData = "";
            string filePath = @"C:\Users\Santosh\Desktop\ExportFile\File.csv";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                filePath = saveFileDialog.FileName;

            csvData += "EmpId,FirstName,LastName,Address,Salary" + Environment.NewLine;

            for(int i=0;i<empList.Count;i++)
            {
                Model obj = empList[i];
                csvData += obj.EmpId + "," + obj.FirstName + "," + obj.LastName + "," + obj.Address + "," + obj.Salary + Environment.NewLine;
            }
            StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8);
            writer.Write(csvData);
            writer.Close();
        }
    }
}
