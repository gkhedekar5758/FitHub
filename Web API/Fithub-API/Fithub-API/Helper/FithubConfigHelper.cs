using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fithub_API.Helper
{
    public interface IFithubConfigHelper
    {
        public string FithubConnectionString { get; }
        public string GetConnectionString(string connectionName);
    }
    public class FithubConfigHelper : IFithubConfigHelper
    {
        private readonly IConfiguration _configuration;

        public FithubConfigHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string FithubConnectionString => _configuration["ConnectionStrings:FithubSQLConnectionString"];

        public string GetConnectionString(string connectionName)
        {
            return _configuration.GetConnectionString(connectionName);
        }
    }
}
