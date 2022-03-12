# **Toyo Backend**

## **To run this project loccaly on docker**
###  Prerequisite:
+ Docker: https://www.docker.com/products/docker-desktop
+ Dotnet Core SDK 5: https://dotnet.microsoft.com/en-us/download/dotnet/5.0

### Running this project on docker containers:
--
1. #### Download this project

2. #### On project folder, go to *env-var* folder and run the respective shell script (**win.bat** for **windows** or **lin.sh** for **linux**) to set the needed environment variables

3. #### Open the terminal on project folder and run this command:
    ```
    docker compose up -d
    ``` 
- #### Now you can look if docker containers is running with this command on terminal
    ```
    docker container ps --format "{{.ID}} - {{.Image}} - {{.Names}}"
    ```
- #### And the result will be something like that

> db127b238ecc - toyo-backend_api - toyo-backend \
1d1e8013ecac - mysql:latest - toyo-db

### Create databases:
1. #### At this point, the containers will be running, but the database has no tables, so it's not ready to use the api yet. To create the tables, this project uses Entity Framework Migrations, that will create tables as described on DataContext classes. So you will need install dotnet ef tools running this command on terminal:
    ```
    dotnet tool install --global dotnet-ef
    ```
2. #### Now will put the terminal cli on *src* folder and use the dotnet-ef command to create databases:
    ```
    cd ./src
    dotnet-ef database update
    ```
- If this messages appears on terminal, the tables was created and the application is ready to be used
    ```
    Build started...
    Build succeeded.
    The Entity Framework tools version '5.0.4' is older than that of the runtime '5.0.12'. Update the tools for the latest features and bug fixes.
    Done.
    ```