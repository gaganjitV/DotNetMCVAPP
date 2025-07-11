
if [ "$(docker ps -a -q -f name=sql)" ]; then
  echo "Container 'sql' already exists. Starting it instead..."
  docker start sql
else
  echo "Creating and starting new SQL Server container..."
  docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Singh12!" -p 1433:1433 --name sql -d mcr.microsoft.com/mssql/server:2022-latest
fi
