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
    /// Логика взаимодействия для ConstructorWindow.xaml
    /// </summary>
    public partial class ConstructorWindow : Window
    {
        public ConstructorWindow()
        {
            InitializeComponent();
        }

        private void BtAddFunction_Click(object sender, RoutedEventArgs e)
        {
            if (TbInputFunction.Text == string.Empty)
            {
                MessageBox.Show("Создайте функцию для добавления", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Close();
        }

        private void AddFunctionElement(string element) 
        {
            int length = TbInputFunction.Text.Length; 
            TbInputFunction.Text = TbInputFunction.Text.Insert(TbInputFunction.SelectionStart, element);
            TbInputFunction.SelectionStart = length + element.Length;
            TbInputFunction.Focus();
        }

        private void BtSquare_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("^");
        }

        private void BtFactorial_Click(object sender, RoutedEventArgs e)
        {

            AddFunctionElement("!");
        }

        private void BtSin_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("sin(");
        }

        private void BtCos_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("cos(");
        }

        private void BtAsin_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("asin(");
        }

        private void BtAcos_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("acos(");
        }

        private void BtAtan_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("atan(");
        }

        private void BtTan_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("tan(");
        }

        private void BtFraction_Click(object sender, RoutedEventArgs e)
        {
            if (TbInputFunction.Text.Length > 0) 
            {
                string text = "1/(" + TbInputFunction.Text + ")";
                TbInputFunction.Text = "";
                AddFunctionElement(text);
            }
        }

        private void BtSquareRoot_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("sqrt(");
        }

        private void BtNaturalLog_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("ln(");
        }

        private void BtDecimalLog_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("lg(");
        }

        private void BtOne_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("1");
        }

        private void BtFoo_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("4");
        }

        private void BtSeven_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("7");
        }

        private void BtZero_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("0");
        }

        private void BtTwo_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("2");
        }

        private void BtFive_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("5");
        }

        private void BtEight_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("8");
        }

        private void BtThree_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("3");
        }

        private void BtSix_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("6");
        }

        private void BtNine_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("9");
        }

        private void BtComma_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement(".");
        }

        private void BtClear_Click(object sender, RoutedEventArgs e)
        {
            TbInputFunction.Text = "";
        }

        private void BtMinus_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("-");
        }

        private void BtDivision_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("/");
        }

        private void BtPi_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("pi");
        }

        private void BtDel_Click(object sender, RoutedEventArgs e)
        {
            TbInputFunction.Text = TbInputFunction.Text.Remove(TbInputFunction.Text.Length - 1, 1);
        }

        private void BtPlus_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("+");
        }

        private void BtMultiplication_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("*");
        }

        private void BtE_Click(object sender, RoutedEventArgs e)
        {
            AddFunctionElement("e");
        }
    }
}
