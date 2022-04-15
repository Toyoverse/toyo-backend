FROM mcr.microsoft.com/dotnet/sdk:5.0.403

COPY ./src /home/full-backend/
COPY ./scriptDocker.sh /home/

RUN cd /home && \
    wget https://github.com/jwilder/dockerize/releases/download/v0.6.1/dockerize-linux-amd64-v0.6.1.tar.gz && \
    tar -C /usr/local/bin -xzvf dockerize-linux-amd64-v0.6.1.tar.gz && \
    rm dockerize-linux-amd64-v0.6.1.tar.gz

RUN chmod +x /home/scriptDocker.sh
RUN mkdir /tmp/toyoverse
RUN dotnet dev-certs https --trust

ENV ASPNETCORE_URLS=https://+
ENV ASPNETCORE_HTTPS_PORT=443

RUN dotnet tool install --global dotnet-ef --version 5.0.12
RUN export PATH="$PATH:/root/.dotnet/tools"
RUN cd /home/full-backend/

RUN dotnet publish  /home/full-backend/BackendToyo.csproj -c Release
RUN mkdir /home/backend/
RUN cp -r /home/full-backend/bin/Release/net5.0/publish/* /home/backend

WORKDIR /home/backend/

ENTRYPOINT ["dotnet","BackendToyo.dll"]

EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001
