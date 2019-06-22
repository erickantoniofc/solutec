using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Solutec.Views
{
    /// <summary>
    /// Lógica de interacción para customers.xaml
    /// </summary>
    public partial class customers : Page
    {
        private Solutec.MainWindow mainw;
        Models.solutecEntities context = new Models.solutecEntities();
        CollectionViewSource customersViewSource;
        public customers(Solutec.MainWindow mainwin)
        {
            InitializeComponent();
            customersViewSource = ((CollectionViewSource)(FindResource("solutecEntitiescutomersViewSource")));
            DataContext = this;
            mainw = mainwin;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            context.customers.Load();
            customersViewSource.Source = context.customers.Local;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var customer_search = from customer in context.customers where customer.full_name.Contains(txt_CustomerSearch.Text) select customer;
            customersDataGrid.ItemsSource = customer_search.ToList();
        }

        private void CustomersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(customersDataGrid.SelectedItem != null)
            {
                Solutec.Models.customers customer = (Solutec.Models.customers)customersDataGrid.SelectedItem;
                mainw.customerSelected(customer);
            }
        }
    }
}
