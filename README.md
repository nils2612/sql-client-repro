# sql-client-repro

Steps:
 1. Create Database
 2. Run CreateTable.sql (no data needed in the table)
 3. Add connection string to appsettings.json
 4. dotnet restore
 5. dotnet run
 6. open browser http://host:port/users?method=sql-client
 7. See that a response is generated
 8. Run performance tests, make 150 req/sec on windows -> no problem
 9. Run perfrmance tests, make 150 req/sec on none windows system -> high failure rate with: A connection was successfully established with the server, but then an error occurred during the pre-login handshake. (provider: SSL Provider, error: 31 - Encryption(ssl/tls) handshake failed)
 
 
 Tests with Azure Germany SQL
