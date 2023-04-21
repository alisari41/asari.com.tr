using Application;
using asari.com.tr.Application;
using asari.com.tr.Persistence;
using Core.Security.Encryption;
using Core.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Her projenin kendi servisleri olacak bunlar� bir b�t�n olarak s�n�flar� sadece buraya ekliyoruz

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddSecurityServices(); //JWT
builder.Services.AddPersistenceServices(builder.Configuration);
//builder.Services.AddInfrastructureServices();
builder.Services.AddHttpContextAccessor(); // Jwt

#endregion

#region JWT i�lemleri i�in entegrasyonlar
TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>(); // TokenOption lar� okumak i�in -appsettings.json i�erisinden okur

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => // JWT i�in otantikasyon ejensikyonu ekleniyor. 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});

builder.Services.AddSwaggerGen(opt => // Swagger i�in otantikasyon ejensikyonu ekleniyor. 
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345.54321\""
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[] { }
        }
    });
});
#endregion

#region Cache
builder.Services.AddDistributedMemoryCache(); // InMemory
                                              //builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");


builder.Services.AddSession();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(opt => opt.AddDefaultPolicy(p => { p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); })); // JWT


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#region MiddleWare'i ekleyerek hata mesaj�n� d�zeltiyor�z

if (app.Environment.IsProduction()) // Bunu a��klama sat�r� yap�p �al��t�r�rsak sadece hata mesaj�n�n sade halini al�r�z ama bu �ekilde �al��t�r�rsak detaytl� bir �ekilde hata mesaj�n� al�r�z
    app.ConfigureCustomExceptionMiddleware();

#endregion

app.UseHttpsRedirection();


//Session icin kullandim hafizida deger tutmak
app.UseSession();
app.Use(async (context, next) =>
{
    var token = context.Session.GetString("Token");
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + token);
    }
    await next();
});


app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // JWT i�in �nce Yetkilendirme �a��r�l�r
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(// Area paneli icin olusturdum ilk seferde Default kismi acilsin
        name: "Admin_Default",
        pattern: "{area:exists}/{controller=Default}/{action=Index}"
    );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();