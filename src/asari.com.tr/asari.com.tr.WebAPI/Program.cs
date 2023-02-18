using Application;
using asari.com.tr.Application;
using asari.com.tr.Persistence;
using Core.CrossCuttingConcerns.Exceptions;

var builder = WebApplication.CreateBuilder(args);



#region Her projenin kendi servisleri olacak bunlar� bir b�t�n olarak s�n�flar� sadece buraya ekliyoruz

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddSecurityServices(); //JWT
builder.Services.AddPersistenceServices(builder.Configuration);
//builder.Services.AddInfrastructureServices();
builder.Services.AddHttpContextAccessor(); // Jwt

#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


#region MiddleWare'i ekleyerek hata mesaj�n� d�zeltiyor�z

if (app.Environment.IsProduction()) // Bunu a��klama sat�r� yap�p �al��t�r�rsak sadece hata mesaj�n�n sade halini al�r�z ama bu �ekilde �al��t�r�rsak detaytl� bir �ekilde hata mesaj�n� al�r�z
    app.ConfigureCustomExceptionMiddleware();

#endregion




//app.UseHttpsRedirection();

app.UseAuthentication(); // JWT i�in �nce Yetkilendirme �a��r�l�r
app.UseAuthorization();

app.MapControllers();

app.Run();
