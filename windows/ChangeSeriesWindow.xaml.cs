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
    /// Логика взаимодействия для ChangeSeriesWindow.xaml
    /// </summary>
    public partial class ChangeSeriesWindow : Window
    {
        private string oldFunction;

        public ChangeSeriesWindow()
        {
            InitializeComponent();
        }

        public bool? ShowEditDialog(string function, string nameFunction) 
        {
            oldFunction = function;
            TbFunction.Text = function;
            TbNameFunction.Text = nameFunction;
            return ShowDialog();
        }

        private void BtChange_Click(object sender, RoutedEventArgs e)
        {
            if (TbFunction.Text == string.Empty || TbNameFunction.Text == string.Empty) 
            {
                MessageBox.Show("Чтобы сохранить функцию, необходимо ввести функцию и название", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Projects.ChangeFunction(oldFunction, TbFunction.Text, TbNameFunction.Text);
            Close();
        }

        private void BtConstructor_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
