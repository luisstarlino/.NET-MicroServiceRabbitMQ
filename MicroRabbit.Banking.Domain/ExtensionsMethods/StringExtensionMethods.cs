using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain.ExtensionsMethods
{
    public static class StringExtensionMethods
    {
        //------------------------------------------------------------------------------------------------
        // --- Convert string to base64 (REF: https://www.macoratti.net/20/01/c_base64.htm)
        //------------------------------------------------------------------------------------------------
        static public string EncodeToBase64(this string text)
        {
            try
            {
                byte[] textAsByte = Encoding.ASCII.GetBytes(text);
                string result = System.Convert.ToBase64String(textAsByte);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //------------------------------------------------------------------------------------------------
        // --- convert base64  to string (REF: https://www.macoratti.net/20/01/c_base64.htm)
        //------------------------------------------------------------------------------------------------
        static public string DecodeFrom64(this string data)
        {
            try
            {
                byte[] dataAsByte = System.Convert.FromBase64String(data);
                string result = System.Text.ASCIIEncoding.ASCII.GetString(dataAsByte);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
