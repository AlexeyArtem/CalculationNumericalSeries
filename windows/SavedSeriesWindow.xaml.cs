using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CalculationNumericalSeries
{
    /// <summary>
    /// Логика взаимодействия для SavedSeriesWindow.xaml
    /// </summary>
    public partial class SavedSeriesWindow : Window
    {
        private Project currentProject;
        public SavedSeriesWindow(Project currentProject)
        {
            InitializeComponent();
            DgSeries.ItemsSource = currentProject.Functions;
            this.currentProject = currentProject;
        }

        private void BtUseSeries_Click(object sender, RoutedEventArgs e)
        {
            if (DgSeries.SelectedIndex == -1)
            {
                MessageBox.Show("Для использования ряда, выберите его из списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Close();
        }

        private void BtAddSeries_Click(object sender, RoutedEventArgs e)
        {
            SavingSeriesWindow savingSeriesWindow = new SavingSeriesWindow(currentProject);
            savingSeriesWindow.ShowDialog();
        }

        private void BtChangeSeries_Click(object sender, RoutedEventArgs e)
        {
            if (DgSeries.SelectedIndex == -1)
            {
                MessageBox.Show("Для изменения ряда, выберите его из списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            KeyValuePair<string, string> keyValueFunc = (KeyValuePair<string, string>)DgSeries.SelectedItem;
            ChangeSeriesWindow changeSeriesWindow = new ChangeSeriesWindow(keyValueFunc.Key, keyValueFunc.Value, currentProject);
            changeSeriesWindow.ShowDialog();
        }

        private void BtDelSeries_Click(object sender, RoutedEventArgs e)
        {
            if (DgSeries.SelectedItem == null)
            {
                MessageBox.Show("Для удаления ряда, выберите его из списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            KeyValuePair<string, string> keyValueFunc = (KeyValuePair<string, string>)DgSeries.SelectedItem;
            currentProject.RemoveFunction(keyValueFunc.Key);
        }
    }
}
