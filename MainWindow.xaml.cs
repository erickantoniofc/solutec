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
            
          
        }

        private void UserTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }

     

        private void Rectangle_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }

        private void setStyleToButtons()
        {
            //home_btn.Style = Application.Current.Resources["menu_button"] as Style;
            //services_btn.Style = Application.Current.Resources["menu_button"] as Style;
            //customers_btn.Style = Application.Current.Resources["menu_button"] as Style;
            //brands_btn.Style = Application.Current.Resources["menu_button"] as Style;
            //users_btn.Style = Application.Current.Resources["menu_button"] as Style;

            home_btn.Background = Brushes.Transparent;
            services_btn.Background = Brushes.Transparent;
            customers_btn.Background = Brushes.Transparent;
            brands_btn.Background = Brushes.Transparent;
            users_btn.Background = Brushes.Transparent;

           
        }

        private void Services_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            setStyleToButtons();
            grid.Background = new SolidColorBrush(Color.FromArgb(99, 59, 183, 189));
            switch (grid.Name)
            {
                case "home_btn":
                    home_txt.Text = "Homepage";
                    btnAction.Visibility = Visibility.Hidden;
                    primary_frame.Source = new Uri("Views/home.xaml", UriKind.Relative);
                    break;
                case "services_btn":
                    home_txt.Text = "Servicios";
                    btnAction.Visibility = Visibility.Visible;
                    btnAction.Content = "Registrar servicio";
                    
                    break;
                case "customers_btn":
                    home_txt.Text = "Clientes";
                    btnAction.Visibility = Visibility.Visible;
                    btnAction.Content = "Registrar cliente";
                    break;
                case "brands_btn":
                    home_txt.Text = "Marcas";
                    Solutec.Views.brands brand = new Solutec.Views.brands(this);
                    primary_frame.NavigationService.Navigate(brand);
                    btnAction.Visibility = Visibility.Visible;
                    btnAction.Content = "Registrar marca";
                    break;
                case "users_btn":
                    home_txt.Text = "Usuarios";
                    btnAction.Visibility = Visibility.Visible;
                    btnAction.Content = "Registrar usuario";
                    break;
            }
            
        }

        public void brandSuccesful()
        {
            Solutec.Views.brands brand = new Solutec.Views.brands(this);
            primary_frame.NavigationService.Navigate(brand);
        }

        public void brandSelected(Solutec.Models.brands brand)
        {
            Solutec.Views.new_brand nb = new Solutec.Views.new_brand(this, brand);
            secondary_frame.NavigationService.Navigate(nb);
            
        }
        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Content)
            {
                case "Registrar marca":
                    Solutec.Views.new_brand nb = new Solutec.Views.new_brand(this);
                    secondary_frame.NavigationService.Navigate(nb);
                    break;
            }
        }

        
    }
}
