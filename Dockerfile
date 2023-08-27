#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80 
# Expose iþlemi yapýlmazsa microservisler haberleþemez yani bu container içerisinde çalýþan yukarýdaki base servisine baþka bir container içersinde çalýþan microservisler 80 portuyla eriþebilecek
# 3 durum vardýr:
	#1. Hiç bir iþlem yapmadýk Expose bile yapmadýk. Ne bu servise dýþarýdan eriþebilir ne docker içerisinde baþka bir microservis buraya eriþebilir. Sadece bu container içerisinde baðlanýp bu container içersinden eriþebiliriz
	#2. Expose ettik fakat portforvet yapmadýk (publish: docker run  -p 5000:80) oortforvet yapmadýðým için dýþarýdan eriþemem ama container  içerisidenki microservisler bu container'a eriþebilir
	#3. Expose yapamdým fakat portforver yaptýysak gizli bir expose oluyor. 
EXPOSE 443 
# HTTPS portu 443 dür

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/asari.com.tr/asari.com.tr.WebMVC/asari.com.tr.WebMVC.csproj", "src/asari.com.tr/asari.com.tr.WebMVC/"]
# Yukarýdaki satýrda sað tarafta baþta "/" yok. Eðer olursa /src klasörüyle ayný seviyede olur içerisinde olmaz
COPY ["src/asari.com.tr/asari.com.tr.Application/asari.com.tr.Application.csproj", "src/asari.com.tr/asari.com.tr.Application/"]
COPY ["src/corePackages/Core.Application/Core.Application.csproj", "src/corePackages/Core.Application/"]
COPY ["src/corePackages/Core.CrossCuttingConcerns/Core.CrossCuttingConcerns.csproj", "src/corePackages/Core.CrossCuttingConcerns/"]
COPY ["src/corePackages/Core.Security/Core.Security.csproj", "src/corePackages/Core.Security/"]
COPY ["src/corePackages/Core.Persistence/Core.Persistence.csproj", "src/corePackages/Core.Persistence/"]
COPY ["src/asari.com.tr/asari.com.tr.Domain/asari.com.tr.Domain.csproj", "src/asari.com.tr/asari.com.tr.Domain/"]
COPY ["src/asari.com.tr/asari.com.tr.Infrastructure/asari.com.tr.Infrastructure.csproj", "src/asari.com.tr/asari.com.tr.Infrastructure/"]
COPY ["src/asari.com.tr/asari.com.tr.Persistence/asari.com.tr.Persistence.csproj", "src/asari.com.tr/asari.com.tr.Persistence/"]
RUN dotnet restore "src/asari.com.tr/asari.com.tr.WebMVC/asari.com.tr.WebMVC.csproj"
# Restore o anki kütüphanleri/paketleri kullanýlabilir hale getiriyor
COPY . .
# COPY .. aslýnda bize WORKDIR da ne yazýyorsa ona kopyalar
# COPY kýsmýný ikiye ayýrmasýnýn sebebi bizim yazmýþ olduðumuz her bir komut docker tarafýndan cacheleniyor. Ama build sürecini kýsaltmak. Build sürecinden sonra Image oluþuyor. Image'lerimizin daha hýzlý oluþmasýný saðlýyor. 
# Yukarýda yazýlan copy komut satýrlarý içerisinde deðiþiklik olmadýðý sürece cache içerisinde okuyor.
WORKDIR "/src/src/asari.com.tr/asari.com.tr.WebMVC"
RUN dotnet build "asari.com.tr.WebMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "asari.com.tr.WebMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "asari.com.tr.WebMVC.dll"]