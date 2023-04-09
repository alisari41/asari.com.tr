namespace asari.com.tr.Application.Features.OperationClaims.Contants;

public class OperationClaimMessages
{
    #region İş Kuralı - Business Role
    public const string RolMevcutDegil = "Rol mevcut değildir.";
    public const string RolMevcut = "Rol sistemde zaten mevcuttur.";
    #endregion

    #region Formatlama - Fluent Validation
        #region Zorunlu Alanlar
        public const string IdBosOlmamali = "'Id'si boş olmamalıdır.";
        public const string NameBosOlmamali = "'Rol Adı' boş olmamalıdır.";
        #endregion
    #endregion
}