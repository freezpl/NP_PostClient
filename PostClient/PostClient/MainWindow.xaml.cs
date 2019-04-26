using MahApps.Metro.Controls;
using Microsoft.Win32;
using PostClient.Helpers;
using PostClient.Models;
using PostClient.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PostClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        bool mailErr;
        PostService post;
        string login;
        string pass;
        List<Attachment> attachments;

        public MainWindow()
        {
            Login loginForm = new Login();
            loginForm.ShowDialog();
            if (loginForm.DialogResult != true)
                Close();
            else
            {
                post = loginForm.postService;
                login = loginForm.Log;
                pass = loginForm.Pass;
            }

            mailErr = true;
            attachments = new List<Attachment>();

            InitializeComponent();
            MailErr.Visibility = Visibility.Collapsed;
            SubjectWarn.Visibility = Visibility.Collapsed;
            TextWarn.Visibility = Visibility.Collapsed;

            BtnTrigger();
        }

        private void Address_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;

            string err = Checker.CheckInput(CheckType.Empty, text);

            if (err.Length == 0)
                err = Checker.CheckInput(CheckType.Mail, text);

            if (err.Length > 0)
            {
                MailErr.Visibility = Visibility.Visible;
                MailErr.Text = err;
                mailErr = true;
            }
            else
            {
                MailErr.Visibility = Visibility.Collapsed;
                mailErr = false;
            }
            BtnTrigger();
        }

        void BtnTrigger()
        {
            SendBtn.IsEnabled = (mailErr) ? false : true;
        }

        private void Subject_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;

            string err = Checker.CheckInput(CheckType.Empty, text);

            if ((sender as TextBox).Name == "Subject")
            {
                if (err.Length > 0)
                {
                    SubjectWarn.Visibility = Visibility.Visible;
                    SubjectWarn.Text = err;
                }
                else
                    SubjectWarn.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (err.Length > 0)
                {
                    TextWarn.Visibility = Visibility.Visible;
                    TextWarn.Text = err;
                }
                else
                    TextWarn.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    attachments.Add(new Attachment(ofd.FileName));
                    if (attachments.Count > 1)
                        Attachment.Text += ", ";
                    Attachment.Text += ofd.FileName;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient(post.Smtp, post.SmtpPort);
                client.Credentials = new System.Net.NetworkCredential(login, pass);
                client.EnableSsl = true;

                string subject = (Subject.Text.Length > 0) ? Subject.Text : "No subject";
                string msg = (Message.Text.Length > 0) ? Message.Text : "No text";

                MailMessage message = new MailMessage(login + post.Name, Address.Text,
                subject, msg);

                foreach (var att in attachments)
                    message.Attachments.Add(att);

                client.Send(message);
                OnMsgSended();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        void OnMsgSended()
        {
            Address.Text = Subject.Text = Message.Text = Attachment.Text = String.Empty;
            attachments.Clear();

            MessageBox.Show("Message is sended!");
        }
    }
}
