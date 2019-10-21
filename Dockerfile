FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env

WORKDIR /app


RUN curl -sL https://deb.nodesource.com/setup_12.x | bash - &&\
        apt-get install -y nodejs &&\
        npm install -g @angular/cli

COPY . .

RUN cd ClientApp &&\
    npm install &&\
    ng build --prod --output-path ./dist

RUN dotnet restore &&\
    dotnet build


ENV ASPNETCORE_ENVIRONMENT="Production"
ENV ML_SERVER__URL="http://localhost:8080"
ENV ML_CONNECTIONSTRINGS__SQLITE="Data Source=Database.db"
ENV ML_LOGGING__LOGLEVEL__DEFAULT="Information"

EXPOSE 8080

CMD ["dotnet", "bin/Debug/netcoreapp2.1/musicList2.dll"]
