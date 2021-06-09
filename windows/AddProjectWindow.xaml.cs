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

            Projects.AddProject(TbNameProject.Text);
            Close();
        }
    }
}
