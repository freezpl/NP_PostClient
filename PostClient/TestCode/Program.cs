using S22.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TestCode
{
    class Program
    {

        static string login = "";
        static string pass = "";

        static void Main(string[] args)
        {
            //SmtpClient client = new SmtpClient("aspmx.l.google.com", 25);
            //https://myaccount.google.com/lesssecureapps?utm_source=google-account&utm_medium=web
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            //client.Credentials = new System.Net.NetworkCredential(login, pass);
            //client.EnableSsl = true;
            //MailMessage message = new MailMessage("freezpl@gmail.com", "pldesign@ukr.net", 
            //    "test message", "Test message body");
            //client.Send(message);

            //Console.WriteLine("Message send");
            //using (ImapClient client = new ImapClient("imap.gmail.com", 993,
            //    login, pass, AuthMethod.Login, true))
            using (ImapClient client = new ImapClient("imap.ukr.net", 993,
            login, pass, AuthMethod.Login, true))
            {
                while (true)
                {
                    //List<uint> uids = client.Search(SearchCondition.All()).ToList();
                    //Console.WriteLine(uids.Count);
                    //List<uint> uidsOnPage = new List<uint>();

                    //for (int i = uids.Count - 1; i > uids.Count - 5 && i >= 0; i--)
                    //{
                    //    uidsOnPage.Add(uids[i]);
                    //}

                    //IEnumerable<MailMessage> messages = client.GetMessages(uidsOnPage);

                    //foreach (var msg in messages)
                    //{
                    //    Console.WriteLine(msg.Subject);
                    //}

                    //for (int i = uids.Count - 1; i > uids.Count - 5 && i >= 0; i--)
                    //{
                    //    client.RemoveMessageFlags(uids[i], null, MessageFlag.Seen);

                    //}
                    client.NewMessage += new EventHandler<IdleMessageEventArgs>((object sender, IdleMessageEventArgs e) =>
                    {
                        Console.WriteLine("Received a new message!");
                        Console.WriteLine("Total number of messages in the mailbox: " +
                            e.MessageCount);
                    });
                    
                    Console.WriteLine(client.GetMailboxInfo().Messages);
                    Console.ReadLine();
                    Console.ReadLine();
                    Console.ReadLine();
                }
            }
        }
    }
}
