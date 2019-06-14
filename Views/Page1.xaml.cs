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
using System.Data.Entity.Validation;

namespace Solutec.Views
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            
        }

        private void saveCommandHandler (Object sender, ExecutedRoutedEventArgs e)
        {
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
                    MessageBox.Show("Nueva marca ingresada correctamente.");
                    
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

    }
}
