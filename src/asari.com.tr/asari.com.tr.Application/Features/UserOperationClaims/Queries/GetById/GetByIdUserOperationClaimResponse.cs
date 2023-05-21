namespace asari.com.tr.Application.Features.UserOperationClaims.Queries.GetById;

public class GetByIdUserOperationClaimResponse
{
    // Join İşlemi için kullanacağım sınıf. Hangi Dataları koymak istiyorsak onları yazıyoruz

    #region Tablo Id
    public int Id { get; set; }
    #endregion

    #region Kullanıcı Tablosundan Alınacaklar
    public int UserId { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string UserEmail { get; set; }
    #endregion

    #region Rol Tablosundan Alınacaklar
    public int OperationClaimId { get; set; }
    public string OperationClaimName { get; set; }
    #endregion
}