using System;
using System.Windows;

namespace CalculationNumericalSeries
{
    /// <summary>
    /// Логика взаимодействия для FunctionMoveWindow.xaml
    /// </summary>
    public partial class FunctionMoveWindow : Window
    {
        public FunctionMoveWindow()
        {
            InitializeComponent();
            ListProjects.ItemsSource = Projects.ListProjects;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string function = LbFunction.Content.ToString();
            string name = LbNameFunction.Content.ToString();
            if (function == string.Empty || name == string.Empty) return;

            try
            {
                if (!(!(ListProjects.SelectedItem is Project project)))
                {
                    Projects.AddFunction(project, function, name);
                    Projects.RemoveFunction(function);
                    Close();
                }
                else MessageBox.Show("Чтобы переместить функцию, выберите проект", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ArgumentException) { MessageBox.Show("Функция уже существует в проекте", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        public bool? ShowDialog(string function, string name) 
        {
            LbFunction.Content = function;
            LbNameFunction.Content = name;
            return ShowDialog();
        }
    }
}
