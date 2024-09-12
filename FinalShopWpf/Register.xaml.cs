using FinalShopWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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

namespace FinalShopWpf
{

    public partial class Register : Window
    {

        public string _finishemail;

        AppDbContext _db;


        public Register()
        {
            InitializeComponent();
            _db = new AppDbContext();


        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
			authorization goTo = new authorization();
			goTo.Show();
			this.Close();
		}

        private void UserRegister_Click(object sender, RoutedEventArgs e)
        {
            string login = UserLogin.Text.Trim();
            string password = UserPassword.Password.Trim();
            string email = UserEmail.Text.Trim();
            if (login.Equals("") && login.Length < 3)
            {
                MessageBox.Show("Short login");
                return;
            }
            if(password.Length < 3)
            {
                MessageBox.Show("Short password");
                return;
            }
            if(email.Length < 3) 
            {
                MessageBox.Show("Short email");
                return;
            }
            else if (email.Contains("@") )
            {
                _finishemail = email;
            }
            else
            {
                MessageBox.Show("Еmail must @");
                return;
            }

            Users users = new Users(_finishemail, login,Hash(password));
            _db.Users.Add(users);
            _db.SaveChanges();
            UserLogin.Text = "";
            UserEmail.Text = "";
            UserPassword.Password = "";
            UserRegister.Content = "Ready";
        }
		private string Hash(string input)
		{
			byte[] temp = Encoding.UTF8.GetBytes(input);
			using (SHA1Managed sha1 = new SHA1Managed())
			{
				var hash = sha1.ComputeHash(temp);
				return Convert.ToBase64String(hash);
			}
		}
	}
}
