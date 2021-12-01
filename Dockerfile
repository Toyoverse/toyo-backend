FROM mcr.microsoft.com/dotnet/aspnet:5.0.12

COPY bin/Release/net5.0/publish/ /home/BackEnd

WORKDIR /home/BackEnd

EXPOSE 80

ENTRYPOINT ["dotnet", "BackendToyo.dll"]