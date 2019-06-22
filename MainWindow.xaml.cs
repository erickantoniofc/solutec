using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// 
    public partial class MainWindow : Window
    {
        public Models.users user = new Models.users();

        public MainWindow(Models.users nuser)
        {
            InitializeComponent();
            user = nuser;
            switch (user.user_type)
            {
                case 1:
                    services_btn.Visibility = Visibility.Visible;
                    customers_btn.Visibility = Visibility.Visible;
                    brands_btn.Visibility = Visibility.Visible;
                    users_btn.Visibility = Visibility.Visible;
                    break;
                case 2:
                    services_btn.Visibility = Visibility.Visible;
                    customers_btn.Visibility = Visibility.Visible;
                    brands_btn.Visibility = Visibility.Visible;
                    users_btn.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    services_btn.Visibility = Visibility.Visible;
                    customers_btn.Visibility = Visibility.Hidden;
                    brands_btn.Visibility = Visibility.Hidden;
                    users_btn.Visibility = Visibility.Hidden;
                    break;
            }
        }

        public string GetSHA1(String text)
        {
            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            Byte[] originalText = ASCIIEncoding.Default.GetBytes(text);
            Byte[] hash = sha1.ComputeHash(originalText);
            StringBuilder str = new StringBuilder();
            foreach (byte i in hash)
            {
                str.AppendFormat("{0:x2}", i);
            }
            return str.ToString();
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

            //home_btn.Background = Brushes.Transparent;
            //services_btn.Background = Brushes.Transparent;
            //customers_btn.Background = Brushes.Transparent;
            //brands_btn.Background = Brushes.Transparent;
            //users_btn.Background = Brushes.Transparent;

           
        }
        public Grid CurrentButtonSelected { get; set; }
        private void Services_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Solutec.Views.blank blank = new Solutec.Views.blank();
            Grid grid = sender as Grid;
            //setStyleToButtons();

            if (CurrentButtonSelected != grid)
            {
                if (CurrentButtonSelected != null)
                {
                    CurrentButtonSelected.Background = Brushes.White;
                    
                }
                CurrentButtonSelected = grid;
            }

            grid.Background = new SolidColorBrush(Color.FromArgb(99, 59, 183, 189));
            switch (grid.Name)
            {
                case "home_btn":
                    home_txt.Text = "Homepage";
                    btnAction.Visibility = Visibility.Hidden;
                    primary_frame.Source = new Uri("Views/home.xaml", UriKind.Relative);
                    secondary_frame.NavigationService.Navigate(blank);
                    break;
                case "services_btn":
                    home_txt.Text = "Servicios";
                    btnAction.Visibility = Visibility.Visible;
                    btnAction.Content = "Registrar servicio";
                    Solutec.Views.services service = new Solutec.Views.services();
                    primary_frame.NavigationService.Navigate(service);
                    secondary_frame.NavigationService.Navigate(blank);
                    break;
                case "customers_btn":
                    home_txt.Text = "Clientes";
                    btnAction.Visibility = Visibility.Visible;
                    btnAction.Content = "Registrar cliente";
                    Solutec.Views.customers customer = new Solutec.Views.customers(this);
                    primary_frame.NavigationService.Navigate(customer);
                    secondary_frame.NavigationService.Navigate(blank);
                    break;
                case "brands_btn":
                    home_txt.Text = "Marcas";
                    Solutec.Views.brands brand = new Solutec.Views.brands(this);
                    primary_frame.NavigationService.Navigate(brand);
                    secondary_frame.NavigationService.Navigate(blank);
                    btnAction.Visibility = Visibility.Visible;
                    btnAction.Content = "Registrar marca";
                    break;
                case "users_btn":
                    home_txt.Text = "Usuarios";
                    Solutec.Views.users user = new Solutec.Views.users(this);
                    primary_frame.NavigationService.Navigate(user);
                    secondary_frame.NavigationService.Navigate(blank);
                    btnAction.Visibility = Visibility.Visible;
                    btnAction.Content = "Registrar usuario";
                    break;
                   
            }
            
        }

        public void Succesful(string type)
        {
            switch(type)
            {
                case "brand":
                    Solutec.Views.brands brand = new Solutec.Views.brands(this);
                    primary_frame.NavigationService.Navigate(brand);
                    break;
                case "user":
                    Solutec.Views.users user = new Solutec.Views.users(this);
                    primary_frame.NavigationService.Navigate(user);
                    break;
                case "customer":
                    Solutec.Views.customers customer = new Solutec.Views.customers(this);
                    primary_frame.NavigationService.Navigate(customer);
                    break;
            }
    }

        public void brandSelected(Solutec.Models.brands brand)
        {
            Solutec.Views.new_brand nb = new Solutec.Views.new_brand(this, brand);
            secondary_frame.NavigationService.Navigate(nb);
            
        }

        public void userSelected(Solutec.Models.users user)
        {
            Solutec.Views.new_user newUser = new Solutec.Views.new_user(this, user);
            secondary_frame.NavigationService.Navigate(newUser);
        }

        public void customerSelected(Solutec.Models.customers customer)
        {
            Solutec.Views.new_customer newCustomer = new Solutec.Views.new_customer(this, customer);
            secondary_frame.NavigationService.Navigate(newCustomer);
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
                case "Registrar usuario":
                    Solutec.Views.new_user newUser = new Solutec.Views.new_user(this);
                    secondary_frame.NavigationService.Navigate(newUser);
                    break;
                case "Registrar cliente":
                    Solutec.Views.new_customer newCustomer = new Solutec.Views.new_customer(this);
                    secondary_frame.NavigationService.Navigate(newCustomer);
                    break;
            }
        }

        
    }
}
