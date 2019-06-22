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
using System.Data;
using System.Data.Entity;
namespace Solutec.Views
{
    /// <summary>
    /// Lógica de interacción para brands.xaml
    /// </summary>
    public partial class brands : Page
    {
        private Solutec.MainWindow mainw;
        Models.solutecEntities context = new Models.solutecEntities();
        CollectionViewSource brandsViewSource;
        
        public brands(Solutec.MainWindow mainwin)
        {
            InitializeComponent();
            brandsViewSource = ((CollectionViewSource)(FindResource("solutecEntitiesbrandsViewSource")));
            DataContext = this;
            mainw = mainwin;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            context.brands.Load();
            brandsViewSource.Source = context.brands.Local;
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            var brand_search = from brand in context.brands where brand.commercial_name.Contains(txt_BrandSearch.Text) select brand;
            brandsDataGrid.ItemsSource = brand_search.ToList();
        }

        private void BrandsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            //DataRowView row = brandsDataGrid.SelectedItem as DataRowView;
            
            if (brandsDataGrid.SelectedItem != null)
            {
                //mainw.brandSelected(row["id_brand"].ToString());
                Solutec.Models.brands brand = (Solutec.Models.brands)brandsDataGrid.SelectedItem;

                mainw.brandSelected(brand);
            }
            
        }

        private void BrandsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

      
    }
}
