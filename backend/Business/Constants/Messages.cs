using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";

        public static string TestAdded = "Test verisi eklendi";
        public static string TestNotAdded = "Test verisi eklenemedi";
        public static string TestsAdded = "Test verileri başarıyla eklendi";
        public static string TestNotFound = "Test verisi bulunamadı";
        public static string TestsFetched = "Test verileri getirildi";
        public static string TestsNotFetched = "Test bulunamadı";
        public static string TestDeleted = "Test başarıyla silindi";
        public static string TestNotDeleted = "Test silinemedi";
        public static string TestReported = "Test verileri raporlandı";
        public static string TestStatistic = "Testlerin istatistikleri getirildi";




        public static string AuthorizationDenied = "Yetkiniz yok";
    }
}
