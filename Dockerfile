FROM mcr.microsoft.com/dotnet/aspnet:3.1

WORKDIR /app

COPY out/ ./

ENTRYPOINT ["dotnet", "back_end.dll"]
