using System.Windows;

namespace CalculationNumericalSeries
{
    /// <summary>
    /// Логика взаимодействия для AddProjectWindow.xaml
    /// </summary>
    public partial class AddProjectWindow : Window
    {
        public AddProjectWindow()
        {
            InitializeComponent();
        }

        private void BtAddProject_Click(object sender, RoutedEventArgs e)
        {
            if (TbNameProject.Text == string.Empty) 
            {
                MessageBox.Show("Для добавление нового проекта введите его название", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Projects.Add(TbNameProject.Text);
            Close();
        }
    }
}
