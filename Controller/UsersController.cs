using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace baseline_service.Controller
{
    [Route("[controller]")]
    public class UsersController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly IConfiguration configuration;

        public UsersController(IConfiguration config)
        {
            configuration = config;
        }

        public string getConnectionstring()
        {
            var connectionString1 = configuration.GetConnectionString("users-db");
            SqlConnectionStringBuilder connstringbuilder = new SqlConnectionStringBuilder(connectionString1);
            connstringbuilder.Encrypt = false;
            connstringbuilder.Pooling = true;

            return connstringbuilder.ToString();

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var method = "";
            Stopwatch stopwatch = Stopwatch.StartNew();
            StringBuilder stringLogger = new StringBuilder();
            try
            {
                var req = Request;
                var res = Response;
                // echo headers
                foreach (var header in req.Headers)
                {
                    res.Headers.Add(header);
                }

                var queryDictionary = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(req.QueryString.Value);
                if (queryDictionary.ContainsKey("method") == false)
                    queryDictionary.Add("method", "plain");

                switch (queryDictionary["method"])
                {
                    case "sql-client":

                        method = "sql-client";
                        var stringBuilder = new StringBuilder();

                        using (var conn = new System.Data.SqlClient.SqlConnection(getConnectionstring()))
                        {
                            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT TOP 350 * FROM dbo.[users-test];", conn))
                            {
                                cmd.CommandType = System.Data.CommandType.Text;

                                conn.Open();
                                var reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {
                                    Guid id = reader.GetGuid(0);
                                    stringBuilder.AppendLine(id.ToString());
                                }
                            }
                        }
                        break;

                    case "sql-client-async":

                        method = "sql-client-async";
                        var stringBuilder2 = new StringBuilder();

                        using (var conn = new System.Data.SqlClient.SqlConnection(getConnectionstring()))
                        {
                            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT TOP 350 * FROM dbo.[users-test];", conn))
                            {
                                cmd.CommandType = System.Data.CommandType.Text;

                                conn.Open();
                                var reader = await cmd.ExecuteReaderAsync();

                                while (await reader.ReadAsync())
                                {
                                    Guid id = reader.GetGuid(0);
                                    stringBuilder2.AppendLine(id.ToString());
                                }
                            }
                        }
                        break;

                    default:

                        method = "plain";
                        break;
                }

            }
            catch (System.Exception ex)
            {
                dynamic errorResponse = new System.Dynamic.ExpandoObject();
                errorResponse.Message = ex.Message;
                errorResponse.StackTrace = ex.StackTrace;
                errorResponse.Url = HttpContext.Request.GetDisplayUrl();
                errorResponse.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                Console.WriteLine(JsonConvert.SerializeObject(errorResponse));
                return StatusCode(500, errorResponse);
            }

            return Ok(method + $" took {stopwatch.ElapsedMilliseconds}ms" + Environment.NewLine + stringLogger.ToString());
        }
    }
}


