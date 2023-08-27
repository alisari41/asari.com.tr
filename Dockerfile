#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80 
# Expose i�lemi yap�lmazsa microservisler haberle�emez yani bu container i�erisinde �al��an yukar�daki base servisine ba�ka bir container i�ersinde �al��an microservisler 80 portuyla eri�ebilecek
# 3 durum vard�r:
	#1. Hi� bir i�lem yapmad�k Expose bile yapmad�k. Ne bu servise d��ar�dan eri�ebilir ne docker i�erisinde ba�ka bir microservis buraya eri�ebilir. Sadece bu container i�erisinde ba�lan�p bu container i�ersinden eri�ebiliriz
	#2. Expose ettik fakat portforvet yapmad�k (publish: docker run  -p 5000:80) oortforvet yapmad���m i�in d��ar�dan eri�emem ama container  i�erisidenki microservisler bu container'a eri�ebilir
	#3. Expose yapamd�m fakat portforver yapt�ysak gizli bir expose oluyor. 
EXPOSE 443 
# HTTPS portu 443 d�r

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/asari.com.tr/asari.com.tr.WebMVC/asari.com.tr.WebMVC.csproj", "src/asari.com.tr/asari.com.tr.WebMVC/"]
# Yukar�daki sat�rda sa� tarafta ba�ta "/" yok. E�er olursa /src klas�r�yle ayn� seviyede olur i�erisinde olmaz
COPY ["src/asari.com.tr/asari.com.tr.Application/asari.com.tr.Application.csproj", "src/asari.com.tr/asari.com.tr.Application/"]
COPY ["src/corePackages/Core.Application/Core.Application.csproj", "src/corePackages/Core.Application/"]
COPY ["src/corePackages/Core.CrossCuttingConcerns/Core.CrossCuttingConcerns.csproj", "src/corePackages/Core.CrossCuttingConcerns/"]
COPY ["src/corePackages/Core.Security/Core.Security.csproj", "src/corePackages/Core.Security/"]
COPY ["src/corePackages/Core.Persistence/Core.Persistence.csproj", "src/corePackages/Core.Persistence/"]
COPY ["src/asari.com.tr/asari.com.tr.Domain/asari.com.tr.Domain.csproj", "src/asari.com.tr/asari.com.tr.Domain/"]
COPY ["src/asari.com.tr/asari.com.tr.Infrastructure/asari.com.tr.Infrastructure.csproj", "src/asari.com.tr/asari.com.tr.Infrastructure/"]
COPY ["src/asari.com.tr/asari.com.tr.Persistence/asari.com.tr.Persistence.csproj", "src/asari.com.tr/asari.com.tr.Persistence/"]
RUN dotnet restore "src/asari.com.tr/asari.com.tr.WebMVC/asari.com.tr.WebMVC.csproj"
# Restore o anki k�t�phanleri/paketleri kullan�labilir hale getiriyor
COPY . .
# COPY .. asl�nda bize WORKDIR da ne yaz�yorsa ona kopyalar
# COPY k�sm�n� ikiye ay�rmas�n�n sebebi bizim yazm�� oldu�umuz her bir komut docker taraf�ndan cacheleniyor. Ama build s�recini k�saltmak. Build s�recinden sonra Image olu�uyor. Image'lerimizin daha h�zl� olu�mas�n� sa�l�yor. 
# Yukar�da yaz�lan copy komut sat�rlar� i�erisinde de�i�iklik olmad��� s�rece cache i�erisinde okuyor.
WORKDIR "/src/src/asari.com.tr/asari.com.tr.WebMVC"
RUN dotnet build "asari.com.tr.WebMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "asari.com.tr.WebMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "asari.com.tr.WebMVC.dll"]