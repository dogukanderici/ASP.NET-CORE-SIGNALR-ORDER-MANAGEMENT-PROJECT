using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Constants
{
    public class BusinessValidaitonMessageConstants
    {
        public static string NotStringMessage = "string Tipinde Değil!";
        public static string NotIntegerMessage = "int Tipinde Değil!";
        public static string NotDecimalMessage = "decimal Tipinde Değil!";
        public static string NotBooleanMessage = "bool Tipinde Değil!";

        public static string NotEmptyMessage = "Alanı Boş Bırakılamaz!";
        public static string MinCharLengthMessage = "Karakterden Daha Az Olamaz!";
        public static string MaxCharLengthMessage = "Karakterden Daha Fazla Olamaz!";
        public static string NotValidEmailMessage = "Lütfen çerli Bir E-Posta Adresi Giriniz!";
    }
}
