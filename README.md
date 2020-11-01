# StarWarsApi
To start local database

`docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=StarWars2020" -p 1433:1433 --name sql4 -d microsoft/mssql-server-linux:2017-CU12`
