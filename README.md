# WOC-Back-End 

Back-end van onze applicatie

Standaard accounts om in te loggen:
email1, pw1 (standaard rol)
email3, pw3 (ook standaard rol)
admin, admin (admin account)

## Om de back-end te runnen:
Zorg dat er een sql container en mongodb container draaien via onderstaande instructies:

docker run --name database -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=1Secure*Password1" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest

docker run -d -p 27017:27017 -v data:/data/db --name mongodb mongo

Start dan de api op met:

docker run -d -e ASPNETCORE_ENVIRONMENT=Development –e ASPNETCORE_URLS=https://+:443;http://+:80 –e ASPNETCORE_HTTPS_PORT=5000  -e ASPNETCORE_Kestrel__Certificates__Default__Password=mypass123  -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v %USERPROFILE%/.aspnet/https:/https:ro -p 5000:80 -p 5001:443 --name woc-api wocapi

Als je nog geen docker image hebt, of andere probleem volg dan de instructies hieronder:

## Ssl certificate
Om de api te kunnen runnen moet je een certificate hebben in de %USERPROFILE%\.aspnet\https folder. Als je dat niet hebt voer dan onderstaand commando uit:

dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p mypass123  

## SQL database 
Als er nog geen sql image is:   
docker pull mcr.microsoft.com/mssql/server:2017-latest  

## SQL container
docker run --name database -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=1Secure*Password1" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest

## MongoDB database
docker pull mongo

## MongoDB container
docker run -d -p 27017:27017 -v data:/data/db --name mongodb mongo

## Api  
### Build docker image
Build docker image (in de folder; open cmd)
docker build -t wocapi . 

## Run image (op port 5001)
docker run -d -e ASPNETCORE_ENVIRONMENT=Development –e ASPNETCORE_URLS=https://+:443;http://+:80 –e ASPNETCORE_HTTPS_PORT=5000  -e ASPNETCORE_Kestrel__Certificates__Default__Password=mypass123  -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v %USERPROFILE%/.aspnet/https:/https:ro -p 5000:80 -p 5001:443 --name woc-api wocapi

### Pull image van dockerhub
docker pull kerimcan/woc-api

### Run image van dockerhub
docker run -d -e ASPNETCORE_ENVIRONMENT=Development –e ASPNETCORE_URLS=https://+:443;http://+:80 –e ASPNETCORE_HTTPS_PORT=5000  -e ASPNETCORE_Kestrel__Certificates__Default__Password=mypass123  -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v %USERPROFILE%/.aspnet/https:/https:ro -p 5000:80 -p 5001:443 --name woc-api kerimcan/woc-api