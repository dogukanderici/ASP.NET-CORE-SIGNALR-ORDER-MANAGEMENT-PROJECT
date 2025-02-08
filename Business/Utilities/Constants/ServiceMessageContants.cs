using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Constants
{
    public class ServiceMessageContants
    {
        public static string ProcessSuccess = "İşlem Başarılı.";
        public static string AddingProcessSuccess = "Veri Ekleme Başarılı.";
        public static string UpdatingProcessSuccess = "Veri Güncelleme Başarılı.";
        public static string DeletingProcessSuccess = "Veri Silme Başarılı.";
        public static string ProcessFailedWithNull = "Veri Bulunamadı.";
        public static string ProcessFailedWithSQL = "Veritabanı Hatası Meydana Geldi.";
        public static string ProcessFailedWithSystem = "Sistemsel Bir Hata Meydana Geldi.";
    }
}
