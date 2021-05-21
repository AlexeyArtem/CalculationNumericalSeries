using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using Newtonsoft.Json;

namespace CalculationNumericalSeries
{
    /// <summary>
    /// Логика взаимодействия для ProjectsWindow.xaml
    /// </summary>
    public partial class ProjectsWindow : BaseDialogWindow
    {
        public ProjectsWindow() : base()
        {
            InitializeComponent();
            ListProjects.ItemsSource = Projects.ListProjects;
        }

        private void BtAddProject_Click(object sender, RoutedEventArgs e)
        {
            AddProjectWindow addProjectWindow = new AddProjectWindow();
            addProjectWindow.ShowDialog();
        }

        private void BtDelProject_Click(object sender, RoutedEventArgs e)
        {
            //Использовать обработку этого исключения в разделе тестирования
            if (ListProjects.SelectedIndex == -1) 
            {
                MessageBox.Show("Чтобы удалить проект, выберите его из списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                Project project = ListProjects.SelectedItem as Project;
                if (!(project is null)) Projects.RemoveProject(project);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtSelectionProject_Click(object sender, RoutedEventArgs e)
        {
            if (ListProjects.SelectedIndex == -1)
            {
                MessageBox.Show("Для установки текущего проекта, выберите его из списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Project project = ListProjects.SelectedItem as Project;
            if (!(project is null)) 
            {
                Projects.CurrentProject = project;
            }
        }

        private void ListProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Project project = ListProjects.SelectedItem as Project;
            if (!(project is null)) 
            {
                DgFunctions.ItemsSource = project.Functions;
            }
        }

        private void BtAddFunction_Click(object sender, RoutedEventArgs e)
        {
            if (ListProjects.SelectedIndex == -1) 
            {
                MessageBox.Show("Для добавления новой функции, выберите проект из списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            SavingSeriesWindow savingSeriesWindow = new SavingSeriesWindow(ListProjects.SelectedItem as Project);
            savingSeriesWindow.ShowDialog();
        }

        private void BtDelFunction_Click(object sender, RoutedEventArgs e)
        {
            if (DgFunctions.SelectedIndex == -1) 
            {
                MessageBox.Show("Для удаления ряда, выберите его из списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            KeyValuePair<string, string> keyValueFunc = (KeyValuePair<string, string>)DgFunctions.SelectedItem;
            Projects.RemoveFunction(keyValueFunc.Key);
        }

        private void BtChangeFunction_Click(object sender, RoutedEventArgs e)
        {
            if (DgFunctions.SelectedIndex == -1)
            {
                MessageBox.Show("Для редактирования ряда, выберите его из списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            KeyValuePair<string, string> keyValueFunc = (KeyValuePair<string, string>)DgFunctions.SelectedItem;
            ChangeSeriesWindow changeSeriesWindow = new ChangeSeriesWindow();
            changeSeriesWindow.ShowEditDialog(keyValueFunc.Key, keyValueFunc.Value);
        }
    }
}
