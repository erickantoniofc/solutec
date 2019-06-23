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
    /// Lógica de interacción para services.xaml
    /// </summary>
    public partial class services : Page
    {
        private Solutec.MainWindow mainw;
        Models.solutecEntities context = new Models.solutecEntities();
        CollectionViewSource servicesViewSource;
        public services(Solutec.MainWindow mainwin)
        {
            InitializeComponent();
            servicesViewSource = ((CollectionViewSource)FindResource("solutecEntitiesservicesViewSource"));
            DataContext = this;
            mainw = mainwin;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            context.services.Load();
            servicesViewSource.Source = context.services.Local;
        }

        private void Txt_ServiceSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var services_search = from service in context.services where service.model.Contains(txt_ServiceSearch.Text) select service;
            servicesDataGrid.ItemsSource = services_search.ToList();
        }

        private void ServicesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(servicesDataGrid.SelectedItem != null)
            {
                Solutec.Models.services service = (Solutec.Models.services)servicesDataGrid.SelectedItem;

                mainw.serviceSelected(service);
            }
        }
    }
}
