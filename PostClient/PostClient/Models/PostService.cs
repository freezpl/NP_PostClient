using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostClient.Models
{
    public class PostService
    {
        public string Name { get; set; }
        public string Imap { get; set; }
        public int ImapPort { get; set; }
        public string Smtp { get; set; }
        public int SmtpPort { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
