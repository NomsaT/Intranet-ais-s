using Microsoft.Extensions.Configuration;
using System;

namespace DAL
{
    public static class DB
    {
        private static string _connectionString;
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    SetConnectionString();
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        private static string _hostPath;
        public static string HostPath
        {
            get
            {
                if (string.IsNullOrEmpty(_hostPath))
                {
                    SetHostPath();
                }
                return _hostPath;
            }
            set
            {
                _hostPath = value;
            }
        }

        private static void SetConnectionString()
        {
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // Directory where the json files are located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private static void SetHostPath()
        {
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // Directory where the json files are located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            _hostPath = configuration["AppSettings:HostPath"];
        }


    }
}
