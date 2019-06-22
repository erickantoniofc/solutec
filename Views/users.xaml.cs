using System;
using System.Collections.Generic;
using System.Data;
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
using Solutec;
namespace Solutec.Views
{
    /// <summary>
    /// Lógica de interacción para users.xaml
    /// </summary>
    /// 
    
    public partial class users : Page
    {
        private Solutec.MainWindow mainw;
        Models.solutecEntities context = new Models.solutecEntities();
        CollectionViewSource usersViewSource;
        public users(MainWindow mainWin)
        {
            InitializeComponent();
            usersViewSource = ((CollectionViewSource)(FindResource("solutecEntitiesusersViewSource")));
            DataContext = this;
            mainw = mainWin;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            context.users.Load();
            usersViewSource.Source = context.users.Local;
            
        }

        private void UsersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Solutec.Models.users user = (Solutec.Models.users)usersDataGrid.SelectedItem;

            mainw.userSelected(user);
        }

        private void Txt_UserSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            var user_search = from user in context.users where user.user.Contains(txt_UserSearch.Text) select user;
            
            usersDataGrid.ItemsSource = user_search.ToList();
        }
    }
}
