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

namespace Solutec.Views
{
    /// <summary>
    /// Lógica de interacción para new_customer.xaml
    /// </summary>
    public partial class new_customer : Page
    {
        public MainWindow mainw;
        public Models.customers custom;
        public new_customer(MainWindow mainwin, Models.customers customer)
        {
            InitializeComponent();
            mainw = mainwin;
            custom = customer;
            if(custom.customer_type == 1)
            {
                direct_customer.IsChecked = true;
            }
            else
            {
                thirdParty_customer.IsChecked = true;
            }
            full_nameTextBox.Text = custom.full_name;
            phone_numberTextBox.Text = custom.phone_number;
            addressTextBox.Text = custom.address;
            duiTextBox.Text = custom.dui;
            nitTextBox.Text = custom.nit;
            nrcTextBox.Text = custom.nrc;
            btnDelete.Visibility = Visibility.Visible;
            btnModify.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Hidden;
            notification.Visibility = Visibility.Hidden;
        }

        public new_customer(MainWindow mainwin)
        {
            InitializeComponent();
            mainw = mainwin;
            direct_customer.IsChecked = true;
            btnDelete.Visibility = Visibility.Hidden;
            btnModify.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
            notification.Visibility = Visibility.Hidden;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(full_nameTextBox.Text) || string.IsNullOrEmpty(phone_numberTextBox.Text))
            {
                MessageBox.Show("Los campos nombre y telefono no deben estar vacios");
                return;
            }
            
            using (Models.solutecEntities context = new Models.solutecEntities())
            {
                Models.customers newCustomer = new Models.customers();
                newCustomer.full_name = full_nameTextBox.Text;
                newCustomer.phone_number = phone_numberTextBox.Text;
                newCustomer.is_active = true;
                newCustomer.nit = nitTextBox.Text;
                
                newCustomer.dui = duiTextBox.Text;
                newCustomer.address = addressTextBox.Text;
                
                if(direct_customer.IsChecked == true)
                {
                    newCustomer.customer_type = 1;
                    newCustomer.nrc = "";
                }
                else
                {
                    newCustomer.customer_type = 2;
                    newCustomer.nrc = nrcTextBox.Text;
                }
                context.customers.Add(newCustomer);
                context.SaveChanges();
                lblNotification.Content = "Cliente registrado correctamente";
                notification.Visibility = Visibility.Visible;
                direct_customer.IsEnabled = false;
                thirdParty_customer.IsEnabled = false;
                full_nameTextBox.IsEnabled = false;
                phone_numberTextBox.IsEnabled = false;
                nitTextBox.IsEnabled = false;
                nrcTextBox.IsEnabled = false;
                duiTextBox.IsEnabled = false;
                addressTextBox.IsEnabled = false;
                btnSave.IsEnabled = false;

                mainw.Succesful("customer");
            }
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(full_nameTextBox.Text) || string.IsNullOrEmpty(phone_numberTextBox.Text))
            {
                MessageBox.Show("Los campos nombre y telefono no deben estar vacios");
                return;
            }

            var mcustomer = new Solutec.Models.customers { id_customer = custom.id_customer };

            using (var context = new Solutec.Models.solutecEntities())
            {
                context.customers.Attach(mcustomer);
                if(direct_customer.IsChecked == true)
                {
                    mcustomer.customer_type = 1;
                    mcustomer.nrc = "";
                }
                else
                {
                    mcustomer.customer_type = 2;
                    mcustomer.nrc = nrcTextBox.Text;
                }
                mcustomer.full_name = full_nameTextBox.Text;
                mcustomer.address = addressTextBox.Text;
                mcustomer.nit = nitTextBox.Text;
                mcustomer.dui = duiTextBox.Text;
                mcustomer.phone_number = phone_numberTextBox.Text;
                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
            }
            lblNotification.Content = "El cliente ha sido modificado";
            notification.Visibility = Visibility.Visible;
            mainw.Succesful("customer");
        }

        private void CommandBinding_Executed_2(object sender, ExecutedRoutedEventArgs e)
        {
            var dcustomer = new Solutec.Models.customers { id_customer = custom.id_customer };
            using (var context = new Solutec.Models.solutecEntities())
            {
                context.customers.Attach(dcustomer);
                context.customers.Remove(dcustomer);
                context.SaveChanges();
            }

            lblNotification.Content = "El cliente ha sido eliminado";
            notification.Visibility = Visibility.Visible;
            mainw.Succesful("customer");
            thirdParty_customer.IsEnabled = false;
            direct_customer.IsEnabled = false;
            full_nameTextBox.IsEnabled = false;
            phone_numberTextBox.IsEnabled = false;
            nitTextBox.IsEnabled = false;
            nrcTextBox.IsEnabled = false;
            duiTextBox.IsEnabled = false;
            addressTextBox.IsEnabled = false;
            btnModify.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if(rb.Content.ToString() == "Directo")
            {
                nrcTextBox.IsEnabled = false;
                
            }
            else
            {
                nrcTextBox.IsEnabled = true;
            }
        }

        
    }
}
