using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CalculationNumericalSeries
{
    /// <summary>
    /// Логика взаимодействия для ProjectsWindow.xaml
    /// </summary>
    public partial class ProjectsWindow : Window
    {
        private Label lbNameCurrentProject;
        
        public ProjectsWindow() : base()
        {
            InitializeComponent();
            ListProjects.ItemsSource = Projects.ListProjects;
        }

        private void BaseDialogWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeLabelNameCurrentProject();
        }

        private void BtAddProject_Click(object sender, RoutedEventArgs e)
        {
            AddProjectWindow addProjectWindow = new AddProjectWindow();
            addProjectWindow.ShowDialog();
        }

        private void BtDelProject_Click(object sender, RoutedEventArgs e)
        {
            if (ListProjects.SelectedIndex == -1) 
            {
                MessageBox.Show("Чтобы удалить проект, выберите его из списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                Project project = ListProjects.SelectedItem as Project;
                if (!(project is null)) Projects.Remove(project);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
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
            (ListProjects.SelectedItem as Project)?.RemoveFunction(keyValueFunc.Key);
        }

        private void BtChangeFunction_Click(object sender, RoutedEventArgs e)
        {
            if (DgFunctions.SelectedIndex == -1)
            {
                MessageBox.Show("Для редактирования ряда, выберите его из списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            KeyValuePair<string, string> keyValueFunc = (KeyValuePair<string, string>)DgFunctions.SelectedItem;
            ChangeSeriesWindow changeSeriesWindow = new ChangeSeriesWindow(keyValueFunc.Key, keyValueFunc.Value, ListProjects.SelectedItem as Project);
            changeSeriesWindow.ShowDialog();
        }

        private void ListProjects_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Project project = ListProjects.SelectedItem as Project;
            if (!(project is null))
            {
                Projects.CurrentProject = project;
                ChangeLabelNameCurrentProject();
            }
        }

        private void BtMoveFunction_Click(object sender, RoutedEventArgs e)
        {
            if (DgFunctions.SelectedIndex == -1) 
            {
                MessageBox.Show("Чтобы переместить функцию, выберите её из таблицы", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (ListProjects.SelectedItem is Project project)
            {
                KeyValuePair<string, string> keyValue = (KeyValuePair<string, string>)DgFunctions.SelectedItem;
                FunctionMoveWindow functionMoveWindow = new FunctionMoveWindow(project, keyValue.Key, keyValue.Value);
                functionMoveWindow.ShowDialog();
            }
        }

        private void ChangeLabelNameCurrentProject() 
        {
            if (lbNameCurrentProject != null)
                lbNameCurrentProject.FontWeight = FontWeights.Normal;
            
            List<Label> labelsNameProject = new List<Label>();
            FindVisualChild<Label>(ListProjects, ref labelsNameProject);
            foreach (Label l in labelsNameProject)
            {
                string name = l.Content.ToString();
                if (name == Projects.CurrentProject.Name)
                {
                    lbNameCurrentProject = l;
                    lbNameCurrentProject.FontWeight = FontWeights.Bold;
                }
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj, ref List<childItem> childItems) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is childItem)
                {
                    childItems.Add((childItem)child);
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child, ref childItems);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
