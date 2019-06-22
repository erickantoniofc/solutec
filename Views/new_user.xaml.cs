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
using System.Data.Entity.Validation;
using System.Security.Cryptography;

namespace Solutec.Views
{
    /// <summary>
    /// Lógica de interacción para new_user.xaml
    /// </summary>
    public partial class new_user : Page
    {
        private MainWindow mainw = new MainWindow();
        private Solutec.Models.users user = new Solutec.Models.users();
        //Create constructor
        public new_user(MainWindow mainwin)
        {
            InitializeComponent();
            mainw = mainwin;
            btnSave.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Hidden;
            btnModify.Visibility = Visibility.Hidden;
            notification.Visibility = Visibility.Hidden;
            change_password.Visibility = Visibility.Hidden;
            password.IsEnabled = true;
        }

        //Modify or Delete constructor
        public new_user(MainWindow mainwin, Solutec.Models.users nuser)
        {
            InitializeComponent();
            mainw = mainwin;
            user = nuser;
            userTextBox.Text = user.user;
            password.IsEnabled = false;
            int index = 0;
            switch(nuser.user_type)
            {
                case 1:
                    index = 0;
                    break;
                case 2:
                    index = 1;
                    break;
                case 3:
                    index = 2;
                    break;
            }
            user_typeComboBox.SelectedIndex = index; 
            btnSave.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Visible;
            btnModify.Visibility = Visibility.Visible;
            notification.Visibility = Visibility.Hidden;
            change_password.Visibility = Visibility.Visible;
            change_password.IsChecked = false;
        }

        

        private void SaveCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
            if (userTextBox.Text == "" || password.Password == "")
            {
                MessageBox.Show("Los campos usuario y contraseña no deben estar vacios");
                return;
            }

            try
            {
                using (Models.solutecEntities context = new Models.solutecEntities())
                {
                    Models.users newUser = new Models.users();
                    newUser.user = userTextBox.Text;
                    newUser.is_active = true;
                    newUser.password = mainw.GetSHA1(password.Password);
                    short user_type = 0;
                    switch (user_typeComboBox.Text)
                    {
                        case "Administrador":
                            user_type = 1;

                        break;
                        case "Atencion al cliente":
                            user_type = 2;
                            break;

                        case "Tecnico":
                            user_type = 3;
                            break;
                        default:
                            user_type = 1;
                            break;
                    }
                    newUser.user_type = user_type;
                    context.users.Add(newUser);
                    context.SaveChanges();
                    lblNotification.Content = "Usuario registrado correctamente";
                    notification.Visibility = Visibility.Visible;
                    userTextBox.IsEnabled = false;
                    password.IsEnabled = false;
                    user_typeComboBox.IsEnabled = false;
                    btnSave.IsEnabled = false;

                    mainw.Succesful("user");

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

        private void ModifyCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
            if (userTextBox.Text == "")
            {
                MessageBox.Show("El campo usuario no debe estar vacio");
                return;
            }
            else if (change_password.IsChecked == true)
            {
                if(string.IsNullOrEmpty(password.Password))
                {
                    MessageBox.Show("El campo contraseña no debe estar vacio");
                    return;
                }
            } 

            var mUser = new Solutec.Models.users { id_user = user.id_user};

            using (var context = new Solutec.Models.solutecEntities())

            {

                context.users.Attach(mUser);

                mUser.user = userTextBox.Text;
                if(change_password.IsChecked == true) { 
                mUser.password = mainw.GetSHA1(password.Password);
                }
                short user_type = 0;
                switch (user_typeComboBox.Text)
                {
                    case "Administrador":
                        user_type = 1;

                        break;
                    case "Atencion al cliente":
                        user_type = 2;
                        break;

                    case "Tecnico":
                        user_type = 3;
                        break;
                    default:
                        user_type = 1;
                        break;
                }
                mUser.user_type = user_type;
                context.Configuration.ValidateOnSaveEnabled = false;

                context.SaveChanges();

            }

            lblNotification.Content = "El usuario ha sido modificado";
            notification.Visibility = Visibility.Visible;
            mainw.Succesful("user");
        }

        private void DeleteCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
            var dUser = new Solutec.Models.users { id_user = user.id_user };

            using (var context = new Solutec.Models.solutecEntities())

            {

                context.users.Attach(dUser);

                context.users.Remove(dUser);

                context.SaveChanges();

            }

            lblNotification.Content = "El usuario ha sido eliminado";
            notification.Visibility = Visibility.Visible;
            mainw.Succesful("user");
            userTextBox.IsEnabled = false;
            password.IsEnabled = false;
            user_typeComboBox.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnModify.IsEnabled = false;

        }

        private void Change_password_Checked(object sender, RoutedEventArgs e)
        {
            password.IsEnabled = true;
        }

        private void Change_password_Unchecked(object sender, RoutedEventArgs e)
        {
            password.IsEnabled = false;
        }
    }



    }

