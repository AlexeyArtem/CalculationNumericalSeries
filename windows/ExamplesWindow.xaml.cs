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
using System.Windows.Shapes;

namespace CalculationNumericalSeries
{
    /// <summary>
    /// Логика взаимодействия для ExamplesWindow.xaml
    /// </summary>
    public partial class ExamplesWindow : Window
    {
        public ExamplesWindow()
        {
            InitializeComponent();
        }

        private void BtSelectExample_Click(object sender, RoutedEventArgs e)
        {
            if (ListExamples.SelectedIndex == -1) 
            {
                MessageBox.Show("Для использования примера ряда, выберите его из списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Close();
        }

    }
}
