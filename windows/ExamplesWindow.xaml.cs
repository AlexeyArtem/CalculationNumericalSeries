using System.Windows;

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
