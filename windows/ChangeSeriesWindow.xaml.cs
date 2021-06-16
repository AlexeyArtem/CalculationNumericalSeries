using System;
using System.Windows;

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
            try
            {
                project.ChangeFunction(oldFunction, TbFunction.Text, TbNameFunction.Text);
                Close();
            }
            catch (ArgumentException) { MessageBox.Show("Функция уже существует в проекте", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
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
