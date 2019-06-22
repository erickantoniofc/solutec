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
using System.Data.Entity;
using System.Windows.Media.Animation;
using System.Data.Entity.Validation;

namespace Solutec.Views
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class new_brand : Page
    {
        private Solutec.MainWindow mainw;
        private Solutec.Models.brands brand;
        public new_brand(Solutec.MainWindow mainwin)
        {
            InitializeComponent();
            mainw = mainwin;
            notification.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
            btnModify.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
            newBrand.Content = "Nueva Marca";
        }

        public new_brand(Solutec.MainWindow mainwin, Solutec.Models.brands nbrand)
        {
            InitializeComponent();
            notification.Visibility = Visibility.Hidden;
            brand = nbrand;
            mainw = mainwin;
            btnSave.Visibility = Visibility.Hidden;
            commercial_nameTextBox.Text = brand.commercial_name;
            nitTextBox.Text = brand.nit;
            nrcTextBox.Text = brand.nrc;
            start_dateDatePicker.Text = brand.start_date.ToString();
            btnDelete.Visibility = Visibility.Visible;
            btnModify.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Hidden;
            newBrand.Content = "Modificar marca";
        }

        private void SaveCommandHandler (Object sender, ExecutedRoutedEventArgs e)
        {
           
            if(commercial_nameTextBox.Text == "")
            {
                MessageBox.Show("El campo nombre comercial no puede estar vacio)");
                return;
            }
            try
            {
                using (Models.solutecEntities context = new Models.solutecEntities())
                {
                    Models.brands newBrand = new Models.brands();
                    newBrand.commercial_name = commercial_nameTextBox.Text;
                    newBrand.is_active = true;
                    newBrand.nit = nitTextBox.Text;
                    newBrand.nrc = nrcTextBox.Text;
                    newBrand.start_date = start_dateDatePicker.DisplayDate;
                    context.brands.Add(newBrand);
                    context.SaveChanges();
                    lblNotification.Content = "Marca registrada correctamente";
                    notification.Visibility = Visibility.Visible;
                    commercial_nameTextBox.IsEnabled = false;
                    nitTextBox.IsEnabled = false;
                    nrcTextBox.IsEnabled = false;
                    start_dateDatePicker.IsEnabled = false;
                    btnSave.IsEnabled = false;
                    
                    mainw.Succesful("brand");
                    
                }
            }
            catch (DbEntityValidationException i)
            {
                foreach (var eve in i.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyCommand_Handler(object sender, ExecutedRoutedEventArgs e)
        {
            if (commercial_nameTextBox.Text == "")
            {
                MessageBox.Show("El campo nombre comercial no puede estar vacio)");
                return;
            }

            var mbrand = new Solutec.Models.brands { id_brand = brand.id_brand };

            using (var context = new Solutec.Models.solutecEntities())

            {

                context.brands.Attach(mbrand);

                mbrand.commercial_name = commercial_nameTextBox.Text;
                mbrand.nit = nitTextBox.Text;
                mbrand.nrc = nrcTextBox.Text;
                mbrand.start_date = start_dateDatePicker.DisplayDate;
                context.Configuration.ValidateOnSaveEnabled = false;

                context.SaveChanges();

            }

            lblNotification.Content = "La marca ha sido modificada";
            notification.Visibility = Visibility.Visible;
            mainw.Succesful("brand");
        }

        private void DeleteCommand_Handler(object sender, ExecutedRoutedEventArgs e)
        {
            var dbrand = new Solutec.Models.brands { id_brand = brand.id_brand};

            using (var context = new Solutec.Models.solutecEntities())

            {

                context.brands.Attach(dbrand);

                context.brands.Remove(dbrand);

                context.SaveChanges();

            }

            lblNotification.Content = "La marca ha sido eliminada";
            notification.Visibility = Visibility.Visible;
            mainw.Succesful("brand");
            commercial_nameTextBox.IsEnabled = false;
            nitTextBox.IsEnabled = false;
            nrcTextBox.IsEnabled = false;
            start_dateDatePicker.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnModify.IsEnabled = false;
        }

        private void NitTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Back)
            {
                var nit = nitTextBox.Text;
                if (nit.Count() == 4)
                {
                    nitTextBox.Text += "-";
                    nitTextBox.SelectionStart = nitTextBox.Text.Length;
                }
                else if (nit.Count() == 11)
                {
                    nitTextBox.Text += "-";
                    nitTextBox.SelectionStart = nitTextBox.Text.Length;
                }
                else if (nit.Count() == 15)
                {
                    nitTextBox.Text += "-";
                    nitTextBox.SelectionStart = nitTextBox.Text.Length;
                }
            }
        }

        private void NrcTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Back)
            {
                var nrc = nrcTextBox.Text;

                if (nrc.Count() == 5)
                {
                    nrcTextBox.Text += "-";
                    nrcTextBox.SelectionStart = nrcTextBox.Text.Length;
                }
            }
        }
    }
}
