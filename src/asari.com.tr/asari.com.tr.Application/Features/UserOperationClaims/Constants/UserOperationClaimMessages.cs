namespace asari.com.tr.Application.Features.UserOperationClaims.Constants;

public class UserOperationClaimMessages
{
    #region İş Kuralı - Business Role
    public const string KullaniciRoluMevcutDegil = "Kullanıcı Rolü mevcut değildir.";
    public const string KullaniciRoluMevcut = "Kullanıcı Rolü sistemde zaten mevcuttur.";
    #endregion

    #region Formatlama - Fluent Validation
        #region Zorunlu Alanlar
        public const string IdBosOlmamali = "'Kullanıcı Rol Id' boş olmamalıdır.";
        public const string UserIdBosOlmamali = "'Kullanıcı Id' boş olmamalıdır.";
        public const string OperationClaimIdBosOlmamali = "'Rol Id' boş olmamalıdır.";
        #endregion
    #endregion
}