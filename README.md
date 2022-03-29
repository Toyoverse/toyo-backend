# **Toyo Backend**

## **To run this project loccaly on docker**
###  Prerequisite:
+ Docker: https://www.docker.com/products/docker-desktop
+ Dotnet Core SDK 5: https://dotnet.microsoft.com/en-us/download/dotnet/5.0

### **Environment Variables:**
+ *Toyo_ConnectionStrings__DefaultConnection*
    + Database connection string
+ *Toyo_Json_Folder*
    + json files repository
+ *Toyo_Timeout_Swap_Milliseconds*
    + time in milliseconds to wait the swap contract before get a timeout
+ *Toyo_Swap_Interval_Milliseconds*
    + time in milliseconds between each query on database while wait swap timeout
+ *Toyo_chain_id*
    + transactions environment chain id (test = 80001, prod = 137)
+ *Toyo_Jwt_Secret*
    + key to generate jwt
+ *Toyo_Jwt_Expiring_Time_Minutes* - (optional)
    + time in minutes to expires jwt token. If no inform, default value is 60
+ *Toyo_Jwt_RefreshToken_Expires* - (optional)
    + time in minutes to revalidate jwt token after expires. Default value is 60;
### **Running this project on docker containers:**

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

## **To deploy on dev and prod**
1. ### Run this command line
#### - on dev environment
> docker build -f "Dockerfile" --force-rm -t toyobackend:dev .
> docker run -d -p 443:443 -p 80:80 --env-file "./dev.env" --name toyo-backend toyobackend:dev

#### - on prod environment
> docker build -f "Dockerfile" --force-rm -t toyobackend:release .
> docker run -d -p 443:443 -p 80:80 --env-file "./prod.env" --name toyo-backend toyobackend:release