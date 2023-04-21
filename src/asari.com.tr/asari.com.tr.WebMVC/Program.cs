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

#region Her projenin kendi servisleri olacak bunlarý bir bütün olarak sýnýflarý sadece buraya ekliyoruz

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddSecurityServices(); //JWT
builder.Services.AddPersistenceServices(builder.Configuration);
//builder.Services.AddInfrastructureServices();
builder.Services.AddHttpContextAccessor(); // Jwt

#endregion

#region JWT iþlemleri için entegrasyonlar
TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>(); // TokenOption larý okumak için -appsettings.json içerisinden okur

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => // JWT için otantikasyon ejensikyonu ekleniyor. 
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

builder.Services.AddSwaggerGen(opt => // Swagger için otantikasyon ejensikyonu ekleniyor. 
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

#region MiddleWare'i ekleyerek hata mesajýný düzeltiyorüz

if (app.Environment.IsProduction()) // Bunu açýklama satýrý yapýp çalýþtýrýrsak sadece hata mesajýnýn sade halini alýrýz ama bu þekilde çalýþtýrýrsak detaytlý bir þekilde hata mesajýný alýrýz
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

app.UseAuthentication(); // JWT için Önce Yetkilendirme çaðýrýlýr
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