using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace Solutec
{
    /// <summary>
    /// Lógica de interacción para login.xaml
    /// </summary>
    public partial class login : Window
    {
        Models.solutecEntities context = new Models.solutecEntities();

        public login()
        {
            InitializeComponent();
            context.users.Load();
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

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string hash = GetSHA1(txtPassword.Password);
            var login_query = from user in context.users where user.user == txtusername.Text && user.password == hash select user;
            if(login_query.Any())
            {
                Models.users user = new Models.users();
                user = login_query.First();
                MainWindow mainwin = new MainWindow(user);
                mainwin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña son incorrectos");
            }
        }
    }
}
