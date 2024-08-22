#Build Stage
# We will be building our file binary with the help of dotnet sdk so will be using 
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal As build  
WORKDIR /source  
#Copying data 
COPY . .  
# now makig the project to restore some dependencies to be runned in that container
RUN dotnet restore "BankingSystemAPI.csproj" --disable-parallel
RUN dotnet publish "BankingSystemAPI.csproj" -c release -o /app --no-restore


#Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./ 
#copying Everything built at build stage to the current loaction
ENTRYPOINT ["dotnet", "BankingSystemAPI.dll"] 
#EntryPoint of the start