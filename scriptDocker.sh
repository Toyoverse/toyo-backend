#!/bin/bash

dotnet tool install --global dotnet-ef --version 5.0.12
export PATH="$PATH:/root/.dotnet/tools"
cd /home/full-backend
dotnet ef database update
dotnet publish -c Release
mkdir /home/backend
cp -r bin/Release/net5.0/publish/* /home/backend
rm -rf /home/full-backend
cd /home/backend
dotnet BackendToyo.dll