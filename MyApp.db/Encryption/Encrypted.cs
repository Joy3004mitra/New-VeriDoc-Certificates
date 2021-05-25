using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.db.Encryption
{
   public class Encrypted
    {
        private static char getMetaValue(char data)
        {
            if (char.IsDigit(data))
            {
                if (data == '1') return 'm'; if (data == '7') return 'r'; if (data == '4') return 'i'; if (data == '0') return 'c'; if (data == '3') return 't';
                if (data == '2') return 'e'; if (data == '5') return 'a'; if (data == '9') return 'z'; if (data == '6') return 'b'; if (data == '8') return 'u'; else return '0';
            }
            else
            {
                if (data == 'm') return '1'; if (data == 'r') return '7'; if (data == 'i') return '4'; if (data == 'c') return '0'; if (data == 't') return '3';
                if (data == 'e') return '2'; if (data == 'a') return '5'; if (data == 'z') return '9'; if (data == 'b') return '6'; if (data == 'u') return '8'; else return '0';
            }
        }

        public static String Encryptdata(String strText)
        {
            byte[] EncodeAsBytes = Encoding.UTF8.GetBytes(strText);
            String returnValue = System.Convert.ToBase64String(EncodeAsBytes);
            return returnValue;
        }

        public static String Decryptdata(String strText)
        {
            byte[] DecodeAsBytes = System.Convert.FromBase64String(strText);
            String returnValue = Encoding.UTF8.GetString(DecodeAsBytes);
            return returnValue;
        }

        public static string EncryptASCII(string strText)
        {
            string tex = String.Empty;

            //First get ASCII Code of every letter in byte array
            byte[] asciiBytes = Encoding.ASCII.GetBytes(strText);

            //For every integer of letter's ASCII convert to own letterFormat by getMetaValue(), seperated by 'x' then concat in a single string
            foreach (byte b in asciiBytes) { foreach (char c in b.ToString()) { tex += getMetaValue(c); } tex += "x"; }

            //Encode it to Base64
            byte[] EncodeAsBytes = Encoding.UTF8.GetBytes(tex.Substring(0, tex.Length - 1));
            tex = System.Convert.ToBase64String(EncodeAsBytes);

            //Finally return that string
            return tex;
        }

        public static string DecryptASCII(string strText)
        {
            string tex = String.Empty, ascii;
            //First decode string from Base64
            byte[] DecodeAsBytes = System.Convert.FromBase64String(strText);
            string stringData = Encoding.UTF8.GetString(DecodeAsBytes);

            //Split up string by seperator 'x'
            string[] splitedChar = stringData.Split('x');

            //For every char(ASCII) getting its integerFormat by getMetaValue() then concat in a single string
            foreach (string asc in splitedChar)
            {
                ascii = String.Empty;
                foreach (char c in asc) ascii += getMetaValue(c);
                tex += (char)Convert.ToInt32(ascii);
            }

            //Finally return that string
            return tex;
        }
    }
}
