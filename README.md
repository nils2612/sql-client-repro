# sql-client-repro

Steps:
 1. Create Database
 2. Run CreateTable.sql (no data needed in the table)
 3. dotnet restore
 4. dotnet run
 5. open browser http://host:port/users?method=sql-client
 6. See that a response is generated
 7. Run performance tests, make 150 req/sec on windows -> no problem
 8. Run perfrmance tests, make 150 req/sec on none windows system -> high failure rate with: A connection was successfully established with the server, but then an error occurred during the pre-login handshake. (provider: SSL Provider, error: 31 - Encryption(ssl/tls) handshake failed)
 
 
 Tests with Azure Germany SQL
