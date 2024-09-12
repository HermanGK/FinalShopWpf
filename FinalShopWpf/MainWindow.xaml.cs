using FinalShopWpf.Model;
using MySql.Data.MySqlClient;
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
using CloudIpspSDK;
using CloudIpspSDK.Checkout;
using System.IO;

namespace FinalShopWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _count;
        List<int> count = new List<int>();
        List<int> summ = new List<int>();
        private int _price = 0;
        
        private int total;

        public MainWindow()
        {
            InitializeComponent();
            
            AppDbContext db = new AppDbContext();
            List<Items> items = db.Items.ToList();
            ItemsList.ItemsSource = items;
            
        }
        
        private void AddToClick(object sender, RoutedEventArgs e)
        {
            MyMethod((sender as Button).Tag);
            Count((sender as Button).Tag);


        }

        public void MyMethod(object tag)
        {
            
            using(AppDbContext db = new AppDbContext())
            {
                int tag2 = Convert.ToInt32(tag);
               List<Items> items= db.Items.Where(el=> el.Id == tag2).ToList();
                foreach(Items item in items)
                {
                    _price = Convert.ToInt32(item.Price);
                   
                    summ.Add(_price);
                    foreach(var el in summ)
                    {
                        total = summ.Sum();
                        PriceLable.Content = $"Price : {total}$";
                    } 
                    
                    
                }
            }
        }
        public void Count(object tag)
        {

            using (AppDbContext db = new AppDbContext())
            {
                int tag2 = Convert.ToInt32(tag);
                List<Items> items = db.Items.Where(el => el.Id == tag2).ToList();
                foreach (Items item in items)
                {
                    count.Add(item.Id);
                    _count = count.Count();
                    CountLable.Content = $"product in basket: {_count} шт!";

                }
            }
        }


        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            PriceLable.Content = "Empty basket!";
            CountLable.Content = "";
            count.Clear(); 
            summ.Clear();
        }

        private void ReadyBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((string)PriceLable.Content != "Empty basket!")
            {
                Config.MerchantId = 1396424;
                Config.SecretKey = "test";

                var req = new CheckoutRequest
                {
                    order_id = Guid.NewGuid().ToString("N"),
                    amount = total * 100,
                    order_desc = "checkout json demo",
                    currency = "USD"
                };
                var resp = new Url().Post(req);
                if (resp.Error == null)
                {
                    string url = resp.checkout_url;
                    System.Diagnostics.Process.Start(url);
                }
            }
            else MessageBox.Show("Empty basket!");
        }

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
            File.Delete("user.xml");
            authorization goTo = new authorization();
            goTo.Show();
            Hide();
            this.Close();
		}
	}
    
}
