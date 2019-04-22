using PostClient.Models;
using PostClient.Pages;
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

namespace PostClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            PostService post;
            string login;
            string pass;

            //Login loginForm = new Login();
            //loginForm.ShowDialog();
            //if (loginForm.DialogResult != true)
            //    Close();
            //else
            //{
            //    post = loginForm.postService;
            //    login = loginForm.Log;
            //    pass = loginForm.Pass;
            //}

            InitializeComponent();

            ImapPage imapPage = new ImapPage();
            //Content = imapPage;
        }
    }
}
