using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PostClient.Helpers
{
    public enum CheckType { Mail, Empty, LengthMoreThan }

    class Checker
    {
        
        public static string CheckInput(CheckType checkType, string text, params int[] par)
        {
            string err = String.Empty;
            switch (checkType)
            {
                case CheckType.Mail:
                    Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = reg.Match(text);
                    if (!match.Success)
                        err = "Wrong mail!";
                    break;
                case CheckType.Empty:
                    if (text.Length == 0)
                        err = "Empty field!";
                    break;
                case CheckType.LengthMoreThan:
                    break;
                default:
                    break;
            }

            return err;
        }

    }
}
