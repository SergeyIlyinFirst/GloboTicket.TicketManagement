# docker-compose.yml

version: '3.9'

services:
  mssql-container:
    image: mcr.microsoft.com/mssql/server:latest
    container_name: mssql 
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=Test@password
    ports:
      - "1433:1433"
    volumes:
      - mssql-container-data:/var/opt/mssql
volumes:
  mssql-container-data:

# docker commands

docker-compose up --build -d
