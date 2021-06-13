using System;
using System.Windows;

namespace CalculationNumericalSeries
{
    /// <summary>
    /// Логика взаимодействия для FunctionMoveWindow.xaml
    /// </summary>
    public partial class FunctionMoveWindow : Window
    {
        private Project projectSource;

        public FunctionMoveWindow(Project projectSource, string function, string name)
        {
            InitializeComponent();
            ListProjects.ItemsSource = Projects.ListProjects;
            this.projectSource = projectSource;
            LbFunction.Content = function;
            LbNameFunction.Content = name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string function = LbFunction.Content.ToString();
            string name = LbNameFunction.Content.ToString();
            if (function == string.Empty || name == string.Empty) return;

            try
            {
                Project recipientProject = ListProjects.SelectedItem as Project;
                if (recipientProject != null)
                {
                    recipientProject.Functions.Add(function, name);
                    projectSource.Functions.Remove(function);
                    Close();
                }
                else MessageBox.Show("Чтобы переместить функцию, выберите проект", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ArgumentException) { MessageBox.Show("Функция уже существует в проекте", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
