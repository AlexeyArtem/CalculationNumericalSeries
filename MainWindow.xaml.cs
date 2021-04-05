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

namespace CalculationNumericalSeries
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NumericalSeries numericalSeries;
        public MainWindow()
        {
            InitializeComponent();
            numericalSeries = new NumericalSeries("2*n");
            LabelResult.Content += numericalSeries.GetSum(1, 3).ToString();
        }
    }
}
