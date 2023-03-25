using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CvsDatagrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public double Width_DataGrid
        {
            get
            {
                double width = 0;
                if (DataGrid_Main != null)
                {
                    foreach (var column in DataGrid_Main.Columns)
                    {
                        width += column.ActualWidth;
                    }
                }
                return width;
            }
        }
        public MainWindow()
        {
            InitializeComponent();

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void DataGrid_Main_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Header.Bin
        }
    }
}
