namespace asari.com.tr.Application.Features.Auths.Constants;

public static class AuthMessages
{
    #region İş Kuralı - Business Role
    public const string KullaniciMevcutDegil = "Kullanıcı mevcut değildir.";
    public const string MailMevcut = "Bu E Posta Adresine Ait Bir Hesap Zaten Var.";
    public const string PasswordError = "Hatalı şifre girdiniz.";
    #endregion

    #region Formatlama - Fluent Validation
        #region Adı
        public const string AdBosOlmamali = "'Ad' boş olmamalıdır.";
        public const string AdYanlizcaHarfIcermeli = "'Ad' yalnızca harf içermelidir..";
        public const string AdMinMaxKarakter = "'Ad' 3 ve 30 arasında karakter uzunluğunda olmalı. Toplam {TotalLength} adet karakter girdiniz.";
        #endregion
        #region Soyadı
        public const string SoyadBosOlmamali = "'Soyad' boş olmamalıdır.";
        public const string SoyadYanlizcaHarfIcermeli = "'Soyad' yalnızca harf içermelidir..";
        public const string SoyadMinMaxKarakter = "'Soyad' 2 ve 30 arasında karakter uzunluğunda olmalı. Toplam {TotalLength} adet karakter girdiniz.";
        #endregion
        #region E Posta
        public const string EPostaBosOlmamali = "'E-posta' boş olmamalıdır.";
        public const string GecerliEPosta = "Geçerli bir e-posta değeri giriniz!";
        #endregion
        #region Şifre
        public const string SifreMinKarakter = "'Şifre' en az 8 karakter olmalıdır.";
        public const string SifreMinBuyukKarakter = "'Şifre' bir veya daha fazla büyük harf içermelidir.";
        public const string SifreMinKucukKarakter = "'Şifre' bir veya daha fazla küçük harf içermelidir.";
        public const string SifreMinRakam = "'Şifre' bir veya daha fazla rakam içermelidir.";
        public const string SifreMinOzelKarakter = "'Şifre' bir veya daha fazla özel karakter içermelidir.";
        public const string SifreIcermemeli = "\"'Şifre' £ # “” karakterleri veya boşluk içermemelidir.";
        #endregion
    #endregion
}