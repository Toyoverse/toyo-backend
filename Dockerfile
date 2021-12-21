FROM mcr.microsoft.com/dotnet/sdk:5.0.403

COPY . /home/full-backend

RUN cd /home && \
    wget https://github.com/jwilder/dockerize/releases/download/v0.6.1/dockerize-linux-amd64-v0.6.1.tar.gz && \
    tar -C /usr/local/bin -xzvf dockerize-linux-amd64-v0.6.1.tar.gz && \
    rm dockerize-linux-amd64-v0.6.1.tar.gz

RUN cd /home/full-backend && \
    chmod +x scriptDocker.sh && \
    mv scriptDocker.sh /home/

RUN mkdir /tmp/toyoverse

RUN dotnet dev-certs https --trust

ENV ASPNETCORE_URLS=https://+
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_HTTPS_PORT=443

EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001