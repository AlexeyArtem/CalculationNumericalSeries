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
using System.Windows.Shapes;

namespace CalculationNumericalSeries
{
    /// <summary>
    /// Логика взаимодействия для PromptingsWindow.xaml
    /// </summary>
    public partial class PromptingsWindow : Window
    {
        public PromptingsWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Collapsed;
            IsEnabled = false;
        }

        public new void Show()
        {
            Visibility = Visibility.Visible;
            IsEnabled = true;
        }
    }
}
