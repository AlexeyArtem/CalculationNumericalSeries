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
        private Project project;

        public ChangeSeriesWindow(string oldFunction, string oldName, Project project)
        {
            InitializeComponent();
            this.oldFunction = oldFunction;
            this.project = project;
            TbFunction.Text = oldFunction;
            TbNameFunction.Text = oldName;
        }

        private void BtChange_Click(object sender, RoutedEventArgs e)
        {
            if (TbFunction.Text == string.Empty || TbNameFunction.Text == string.Empty) 
            {
                MessageBox.Show("Чтобы сохранить функцию, необходимо ввести функцию и название", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            project.ChangeFunction(oldFunction, TbFunction.Text, TbNameFunction.Text);
            Close();
        }

        private void BtConstructor_Click(object sender, RoutedEventArgs e)
        {
            ConstructorWindow constructorWindow = new ConstructorWindow();
            constructorWindow.BtAddFunction.Click += delegate (object s, RoutedEventArgs args)
            {
                string func = constructorWindow.TbInputFunction.Text;
                TbFunction.Text = func != "" ? func : TbFunction.Text;
            };

            constructorWindow.ShowDialog(TbFunction.Text);
        }
    }
}
