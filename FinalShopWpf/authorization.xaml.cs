using FinalShopWpf.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace FinalShopWpf
{
	
	public partial class authorization : Window
	{
		public authorization()
		{
			InitializeComponent();
			if(File.Exists("user.xml"))
			{
				goToMain();
			}
		}

		private void InsideBtn_Click(object sender, RoutedEventArgs e)
		{
			
			string password = UserPasswordInside.Password.Trim();
			string login = UserLoginInside.Text.Trim();
			Users aut = null;
			if (login.Length < 3 && password.Length < 3) 
			{
				MessageBox.Show("Слишком короткие данные");
				return;
			}
			
			using (AppDbContext db = new AppDbContext())
			{
				aut = db.Users.Where(user => user.login == login && user.password == Hash(password)).FirstOrDefault();
			}
			if (aut == null)
			{
				MessageBox.Show("У вас нет аккаунта зарегестрируйтесь");
				goToReg();
			}
			else
			{
				AuthUser user = new AuthUser(login,aut.email);
				XmlSerializer xml = new	XmlSerializer(typeof(AuthUser));
				using(FileStream file = new FileStream ("user.xml", FileMode.Create))
				{
					xml.Serialize(file,user);
				}
				MessageBox.Show("Вы вошли");
				goToMain();
			}


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

		private void GoToRegBtn_Click(object sender, RoutedEventArgs e)
		{
			goToReg();
		}

		public void goToMain()
		{
			MainWindow goTo = new MainWindow();
			goTo.Show();
			Hide();
			this.Close();
		}
		public void goToReg()
		{
			Register goTo = new Register();
			goTo.Show();
			this.Close();
		}
	}
}
