using MahApps.Metro.Controls;
using PostClient.Models;
using S22.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace PostClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        List<PostService> services;
        bool logHasErr;
        bool passHasErr;

        public string Log { get; set; }
        public string Pass { get; set; }
        public PostService postService;

        public Login()
        {
            logHasErr = true;
            passHasErr = true;
            services = new List<PostService>() {
                new PostService {Name="@gmail.com", Imap="imap.gmail.com",
                    ImapPort =993, Smtp="smtp.gmail.com", SmtpPort=587}
            };

            InitializeComponent();
            LoginErr.Visibility = Visibility.Collapsed;
            PassErr.Visibility = Visibility.Collapsed;
            
            ServicesBox.ItemsSource = services;
            SendBtn.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ImapClient client = new ImapClient((ServicesBox.SelectedItem as PostService).Imap,
                    (ServicesBox.SelectedItem as PostService).ImapPort,
                Log + ServicesBox.Text, Pass, AuthMethod.Login, true);
                postService = ServicesBox.SelectedItem as PostService;
                DialogResult = true;
                Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show("Somthing wrong! Make sure that your login and pass is correct!\n" +
                    "REMEMBER you must enable imap protocol in your mailbox!\n" +
                    "If u use gmail - forward that lik and enable checkbox. - https://myaccount.google.com/lesssecureapps?utm_source=google-account&utm_medium=web");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.Length == 0)
            {
                LoginErr.Text = "Empty login";
                logHasErr = true;
            }
            else if (((sender as TextBox).Text).IndexOf('@') >= 0)
            {
                LoginErr.Text = @"Delete '@' symbol";
                logHasErr = true;
            }
            else
            {
                logHasErr = false;
                Log = (sender as TextBox).Text;
            }

            LoginErr.Visibility = (logHasErr) ? Visibility.Visible : Visibility.Collapsed;
            ButtonTrigger();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if ((sender as PasswordBox).Password.Length == 0)
            {
                PassErr.Text = "Empty password";
                passHasErr = true;
            }
            else if (((sender as PasswordBox).Password).IndexOf(' ') >= 0)
            {
                PassErr.Text = @"Delete spaces from password field";
                passHasErr = true;
            }
            else
            {
                passHasErr = false;
                Pass = (sender as PasswordBox).Password;
            }

            PassErr.Visibility = (passHasErr) ? Visibility.Visible : Visibility.Collapsed;
            ButtonTrigger();
        }

        void ButtonTrigger()
        {
            SendBtn.IsEnabled = (logHasErr || passHasErr) ? false : true;
        }
        
    }
}
