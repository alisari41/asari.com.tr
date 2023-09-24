namespace Application.Tests.Constants;

public static class TestCategories
{
    #region Kategoriler
    public const string CQRSCategori = "Command - Query Test";
    public const string FluentValidationCategori = "FluentValidation - Formatlama Testi";
    public const string BusinessRulesCategori = "BusinessRules - İş Kuralı Testi";
    #endregion

    #region CQRS
    public const string CreateCategori = "Create Test";
    public const string DeleteCategori = "Delete Test";
    public const string UpdateCategori = "Update Test";
    public const string GetByIdCategori = "Get By Id Test";
    public const string GetListCategori = "Get List Test";
    #endregion

    #region FluentValidation - Formatlama Kategorisi
    public const string BosVeriCategori = "Boş Veri Testi";
    public const string MinKarakterCategori = "Min Karakter Testi";
    public const string MaxKarakterCategori = "Max Karakter Testi";
    #endregion

    #region BusinessRules - İş Kuralı Kategorisi
    public const string OlmayanVeriCategori = "Olmayan Veri Testi";
    #endregion
}