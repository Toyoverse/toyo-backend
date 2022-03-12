#!/bin/bash

cd /home/full-backend/ 
dotnet-ef database update
dotnet /home/backend/BackendToyo.dll