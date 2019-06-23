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
    /// Lógica de interacción para new_service.xaml
    /// </summary>
    public partial class new_service : Page
    {
        private MainWindow mainw;
        Solutec.Models.services service = new Solutec.Models.services();
        Models.solutecEntities context = new Models.solutecEntities();
        public new_service(MainWindow mainwin)
        {
            InitializeComponent();
            mainw = mainwin;
            DataContext = this;
            group.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
            btnModify.Visibility = Visibility.Hidden;
            direct_serviceCmb.IsChecked = true;
            lblNotification.Visibility = Visibility.Hidden;
        }

        public new_service(MainWindow mainwin, Solutec.Models.services newService)
        {
            InitializeComponent();
            mainw = mainwin;
            service = newService;
            DataContext = this;
            group.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Visible;
            btnModify.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Hidden;
            lblNotification.Visibility = Visibility.Hidden;
            dtPopulation();
            if(service.is_warranty == true)
            {
                thirdPArty_serviceCmb.IsChecked = true;
                brandCmb.SelectedItem = context.brands.Find(service.id_brand);
                commercial_invoiceTextBox.Text = service.commercial_invoice;
                purchase_dateDatePicker.SelectedDate = service.purchase_date.Value;
            }
            else
            {
                direct_serviceCmb.IsChecked = true;
            }
           
            clientCmb.SelectedItem = context.customers.Find(service.id_customer);
            unique_referenceTextBox.Text = service.unique_reference;
            modelTextBox.Text = service.model;
            userCmb.SelectedItem = context.users.Find(service.technical_operator);

            in_dateDatePicker.SelectedDate = service.in_date.Date;

        }


        public void dtPopulation()
        {
            context.advances.Load();
            var advance_query = from advance in context.advances where advance.id_service == service.id_service select advance;
            advancesDataGrid.ItemsSource = advance_query.ToList();
        }


        private void disableAll()
        {
            direct_serviceCmb.IsEnabled = false;
            thirdPArty_serviceCmb.IsEnabled = false;
            clientCmb.IsEnabled = false;
            brandCmb.IsEnabled = false;
            modelTextBox.IsEnabled = false;
            commercial_invoiceTextBox.IsEnabled = false;
            unique_referenceTextBox.IsEnabled = false;
            purchase_dateDatePicker.IsEnabled = false;
            in_dateDatePicker.IsEnabled = false;
            userCmb.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnModify.IsEnabled = false;
            btnSave.IsEnabled = false;

        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            if(thirdPArty_serviceCmb.IsChecked == true)
            {
                if (clientCmb.SelectedItem == null || brandCmb.SelectedItem == null || string.IsNullOrEmpty(unique_referenceTextBox.Text) || string.IsNullOrEmpty(commercial_invoiceTextBox.Text) || purchase_dateDatePicker.DisplayDate.ToString() == "" || userCmb.SelectedItem == null || string.IsNullOrEmpty(in_dateDatePicker.DisplayDate.ToString()))
                { 
                MessageBox.Show("Los campos cliente, marca, imei o serie, factura, fecha de compra, tecnico encargado y fecha de entrada no deben ser nulas");
                return;
                }
            }
            else
            {
                if (clientCmb.SelectedItem == null || string.IsNullOrEmpty(unique_referenceTextBox.Text) || userCmb.SelectedItem == null || string.IsNullOrEmpty(in_dateDatePicker.DisplayDate.ToString()))
                {
                    MessageBox.Show("Los campos cliente, imei o serie, tecnico encargado y fecha de entrada no deben estar vacios");
                    return;
                }
            }

            using (context)
            {
                Models.services newService = new Models.services();
                Models.customers newCustomer = (Models.customers)clientCmb.SelectedItem;
                Models.users newUser = (Models.users)userCmb.SelectedItem;
                newService.id_customer = newCustomer.id_customer;
                if (thirdPArty_serviceCmb.IsChecked == true)
                {
                    Models.brands newBrand = (Models.brands)brandCmb.SelectedItem;
                    newService.id_brand = newBrand.id_brand;
                    newService.is_warranty = true;
                    newService.purchase_date = purchase_dateDatePicker.DisplayDate;
                    newService.commercial_invoice = commercial_invoiceTextBox.Text;
                }
                else
                {
                    newService.is_warranty = false;
                }
                newService.unique_reference = unique_referenceTextBox.Text;
                newService.model = modelTextBox.Text;
                newService.technical_operator = newUser.id_user;
                newService.in_date = in_dateDatePicker.DisplayDate;
                context.services.Add(newService);
                context.SaveChanges();
                disableAll();
                mainw.Succesful("service");
                lblNotification.Content = "Nuevo servicio registrado correctamente";
                lblNotification.Visibility = Visibility.Visible;
        }


        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {

            if (thirdPArty_serviceCmb.IsChecked == true)
            {
                if (clientCmb.SelectedItem == null || brandCmb.SelectedItem == null || string.IsNullOrEmpty(unique_referenceTextBox.Text) || string.IsNullOrEmpty(commercial_invoiceTextBox.Text) || purchase_dateDatePicker.DisplayDate.ToString() == "" || userCmb.SelectedItem == null || string.IsNullOrEmpty(in_dateDatePicker.DisplayDate.ToString()))
                {
                    MessageBox.Show("Los campos cliente, marca, imei o serie, factura, fecha de compra, tecnico encargado y fecha de entrada no deben ser nulas");
                    return;
                }
            }
            else
            {
                if (clientCmb.SelectedItem == null || string.IsNullOrEmpty(unique_referenceTextBox.Text) || userCmb.SelectedItem == null || string.IsNullOrEmpty(in_dateDatePicker.DisplayDate.ToString()))
                {
                    MessageBox.Show("Los campos cliente, imei o serie, tecnico encargado y fecha de entrada no deben estar vacios");
                    return;
                }
            }

            var mservice = new Models.services { id_service = service.id_service };

            using(var context = new Models.solutecEntities())
            {
               
                Models.customers newCustomer = (Models.customers)clientCmb.SelectedItem;
                Models.users newUser = (Models.users)userCmb.SelectedItem;
                context.services.Attach(mservice);
                if (thirdPArty_serviceCmb.IsChecked == true)
                {
                    Models.brands newBrand = (Models.brands)brandCmb.SelectedItem;
                    mservice.id_brand = newBrand.id_brand;
                    mservice.is_warranty = true;
                    mservice.purchase_date = purchase_dateDatePicker.DisplayDate;
                    mservice.commercial_invoice = commercial_invoiceTextBox.Text;
                }
                else
                {
                    mservice.is_warranty = false;
                }
                mservice.unique_reference = unique_referenceTextBox.Text;
                mservice.model = modelTextBox.Text;
                mservice.technical_operator = newUser.id_user;
                mservice.in_date = in_dateDatePicker.DisplayDate;
                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
            }
            mainw.Succesful("service");
        }

        private void CommandBinding_Executed_2(object sender, ExecutedRoutedEventArgs e)
        {
            var dcservice = new Solutec.Models.services { id_service = service.id_service };
            using (context)
            {
                context.services.Attach(dcservice);
                context.services.Remove(dcservice);
                context.SaveChanges();
            }

            disableAll();
            group.Visibility = Visibility.Hidden;
            mainw.Succesful("service");
        }

        private void Direct_serviceCmb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if(rb.Content.ToString() == "Servicio directo")
            {
                brandCmb.IsEnabled = false;
                purchase_dateDatePicker.IsEnabled = false;
                commercial_invoiceTextBox.IsEnabled = false;
            }
            else
            {
                brandCmb.IsEnabled = true;
                purchase_dateDatePicker.IsEnabled = true;
                commercial_invoiceTextBox.IsEnabled = true;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            context.customers.Load();
            context.brands.Load();
            context.users.Load();
            var user_query = from user in context.users where user.user_type == 3 select user;
            var brand_query = from brand in context.brands select brand;
            var customer_query = from customer in context.customers select customer;
            clientCmb.ItemsSource = customer_query.ToList();
            clientCmb.SelectedValuePath = "id_customer";
            clientCmb.DisplayMemberPath = "full_name";
            brandCmb.ItemsSource = brand_query.ToList();
            brandCmb.SelectedValuePath = "id_brand";
            brandCmb.DisplayMemberPath = "commercial_name";
            userCmb.ItemsSource = user_query.ToList();
            userCmb.SelectedValuePath = "id_user";
            userCmb.DisplayMemberPath = "user";
            


        }

        private void CommandBinding_Executed_3(object sender, ExecutedRoutedEventArgs e)
        {
            if (detailTxt.Text == "")
            {
                MessageBox.Show("El campo detalle no debe estar vacio");
                return;
            }
            Models.advances adv = new Models.advances();

            using(context)
            {
                adv.advance_detail = detailTxt.Text;
                short adv_type = 0;
                switch (nivel_cmb.Text)
                {
                    case "Diagnostico":
                        adv_type = 1;
                        break;
                    case "En proceso":
                        adv_type = 2;
                        break;
                    case "Finalizado":
                        adv_type = 3;
                        break;
                }
                adv.advance_type = adv_type;
                adv.id_service = service.id_service;
                context.advances.Add(adv);
                context.SaveChanges();
                var adv_query = from advance in context.advances where advance.id_service == service.id_service select advance;
                advancesDataGrid.ItemsSource = adv_query.ToList();
            }
           
        }
    }
}
