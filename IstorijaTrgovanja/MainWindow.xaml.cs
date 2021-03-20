using IstorijaTrgovanjaLibrary;
using IstorijaTrgovanjaLibrary.Commands;
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

namespace IstorijaTrgovanja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TraziViewModel(this);            
        }

        public void Refresh()
        {
            DataGrid1.Items.Refresh();
        }

        public void SetGridStatus(bool isEnabled = true)
        {
            //DataGrid1.IsEnabled = isEnabled;
            //TraziButton.IsEnabled = isEnabled;
            //SifraTextBox.IsEnabled = isEnabled;
            //BusyIndicatorLabel.Visibility = !isEnabled ? Visibility.Visible : Visibility.Hidden;
            //StopSearchButton.Visibility = !isEnabled ? Visibility.Visible : Visibility.Hidden;            
        }
    }
}
