# WebApiCoreDocker

[![Build Status](https://andriusviederis.visualstudio.com/WebApiCoreDocker/_apis/build/status/andvied.WebApiCoreDocker?branchName=master)](https://andriusviederis.visualstudio.com/WebApiCoreDocker/_build/latest?definitionId=7&branchName=master)

## Used for Development and Deploy
Docker for Windows  
Visual Studio 2019

## Run application  
 
  
### In Docker
Clone application
```
git clone https://github.com/andvied/WebApiCoreDocker.git
```
Open CommandPrompt  

Create network using CMD
```
docker network create -d bridge --subnet 192.168.0.0/24 --gateway 192.168.0.1 mynet
```

Run MSSQL server in docker
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=StrongPassword!" --net=mynet --name slq1 --cap-add SYS_PTRACE -u 4000:0 -p 1401:1433 -d mcr.microsoft.com/mssql/server:2019-latest

```


Navigate to project location
```
cd <project_location>
```

Build WebApi application
```
docker build -t aspnetapp .
```

Run WebApi application
```
docker run -d -p 8080:80 --net=mynet --name myapp aspnetapp
```

Check in the browser
```
http://localhost:8080/swagger/index.html
```


### From Visual Studio

Run SQL in docker
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=StrongPassword!" --cap-add SYS_PTRACE -u 4000:0 -p 1401:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```
*Or*  
  
To connect to any other MSSQL,
update **connectionstring** in **appsettings.json**  

  
Run application


