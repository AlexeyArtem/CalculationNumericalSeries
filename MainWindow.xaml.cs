using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LiveCharts.Wpf;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using LiveCharts;
using LiveCharts.Defaults;
using System.Windows.Controls;

namespace CalculationNumericalSeries
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SeriesCollection partialSumSeries;
        private SeriesCollection derivativeSeries;
        private LineSeries partialSumLine;
        private LineSeries derivativeLine;

        private PromptingsWindow promptingsWindow;
        private ProjectsWindow projectsWindow;
        
        public MainWindow()
        {
            InitializeComponent();

            //
            //Projects.AddFunction(Projects.CurrentProject, "1/n", "Сепенная 1");
            //Projects.AddFunction(Projects.CurrentProject, "1/n*2", "Сепенная 2");
            //


            promptingsWindow = new PromptingsWindow();
            projectsWindow = new ProjectsWindow();
            CbSelectUpperLimit.SelectedIndex = 0;

            partialSumSeries = new SeriesCollection();
            partialSumLine = new LineSeries { Values = new ChartValues<ObservablePoint>(), PointGeometrySize = 0, Title = "Частичная сумма" };
            partialSumSeries.Add(partialSumLine);
            PartialSumsChart.Series = partialSumSeries;

            derivativeSeries = new SeriesCollection();
            derivativeLine = new LineSeries { Values = new ChartValues<ObservablePoint>(), PointGeometrySize = 0, Title = "Производная функции" };
            derivativeSeries.Add(derivativeLine);
            DerivativeChart.Series = derivativeSeries;
        }

        private void BtFindSolution_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NumericalSeries numericalSeries = new NumericalSeries(TbInputFunc.Text, "n");
                SumNumericalSeries result;
                if (UdInputUpperLimit.IsEnabled)
                {
                    result = numericalSeries.GetSum((int)UdInputLowerLimit.Value, (int)UdInputUpperLimit.Value);
                }
                else
                {
                    result = numericalSeries.GetSum((int)UdInputLowerLimit.Value, (double)UdAccuracy.Value);
                }
                LbResult.Content = result.ValueLimit.ToString();

                partialSumLine.Values.Clear();
                for (int i = 0; i < result.PointsPartialSums.Length; i++)
                    partialSumLine.Values.Add(new ObservablePoint(result.PointsPartialSums[i].X, result.PointsPartialSums[i].Y));

                derivativeLine.Values.Clear();
                for (int i = 0; i < result.DerivativePoints.Length; i++)
                    derivativeLine.Values.Add(new ObservablePoint(result.DerivativePoints[i].X, result.DerivativePoints[i].Y));
            }
            catch (Exception ex) 
            {
                LbResult.Content = "";
                partialSumLine.Values.Clear();
                derivativeLine.Values.Clear();

                if (ex is NegativeMemberNumberException)
                    MessageBox.Show(ex.Message + ".", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (ex is NumericalSeriesNotConvergent)
                    MessageBox.Show(ex.Message + ".", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show(ex.Message + ".", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MenuItemExamples_Click(object sender, RoutedEventArgs e)
        {
            ExamplesWindow examplesWindow = new ExamplesWindow();
            if (!(bool)examplesWindow.ShowDialog()) 
            {
                string exampleFunc = (examplesWindow.ListExamples.SelectedItem as ListBoxItem)?.Content as string;
                TbInputFunc.Text = exampleFunc ?? TbInputFunc.Text;
            }
        }

        private void MenuItemСonstructor_Click(object sender, RoutedEventArgs e)
        {
            ConstructorWindow constructorWindow = new ConstructorWindow();
            constructorWindow.BtAddFunction.Click += delegate (object s, RoutedEventArgs args)
            {
                string func = constructorWindow.TbInputFunction.Text;
                TbInputFunc.Text = func != "" ? func : TbInputFunc.Text;
            };

            constructorWindow.ShowDialog();
        }

        private void MenuItemPrompting_Click(object sender, RoutedEventArgs e)
        {
            promptingsWindow?.Show();
        }

        private void BtCopy_Click(object sender, RoutedEventArgs e)
        {
            string result = LbResult.Content != null ? LbResult.Content.ToString() : "";
            Clipboard.SetText(result);
            BtCopy.Content = "Скопировано";
        }

        private void BtCopy_MouseLeave(object sender, MouseEventArgs e)
        {
            BtCopy.Content = "Копировать";
        }

        private void CbSelectUpperLimit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CbSelectUpperLimit.SelectedIndex) 
            {
                case 0:
                    UdInputUpperLimit.Visibility = Visibility.Collapsed;
                    UdInputUpperLimit.IsEnabled = false;
                    UdAccuracy.IsEnabled = true;
                    break;
                case 1:
                    UdInputUpperLimit.Visibility = Visibility.Visible;
                    UdInputUpperLimit.IsEnabled = true;
                    CbItemUpperLimit.IsSelected = false;
                    UdAccuracy.IsEnabled = false;
                    break;
            }
        }

        private void MenuItemProjects_Click(object sender, RoutedEventArgs e)
        {
            projectsWindow?.ShowDialog();
        }

        private void MenuItemSavedSeries_Click(object sender, RoutedEventArgs e)
        {
            SavedSeriesWindow savedSeriesWindow = new SavedSeriesWindow(Projects.CurrentProject);
            if (!(bool)savedSeriesWindow.ShowDialog() && savedSeriesWindow.DgSeries.SelectedItem != null) 
            {
                KeyValuePair<string, string> keyValueFunc = (KeyValuePair<string, string>)savedSeriesWindow.DgSeries.SelectedItem;
                TbInputFunc.Text = keyValueFunc.Key;
            }
        }
    }
}
