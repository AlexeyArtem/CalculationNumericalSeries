using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CalculationNumericalSeries
{
    public class BaseDialogWindow : Window
    {
        public BaseDialogWindow() 
        {
            ResizeMode = ResizeMode.CanMinimize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Collapsed;
            IsEnabled = false;
        }

        public new bool? ShowDialog()
        {
            Visibility = Visibility.Visible;
            IsEnabled = true;
            return base.ShowDialog();
        }
    }
}
