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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Solutec
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource usersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("usersViewSource")));
            // Cargar datos estableciendo la propiedad CollectionViewSource.Source:
            // usersViewSource.Source = [origen de datos genérico]
        }

        private void UserTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
