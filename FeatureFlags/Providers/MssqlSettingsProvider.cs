using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Providers
{
    public class MssqlSettingsProvider : IFeatureProvider
    {
        private string _connectionStringName;
        public Dictionary<string, string> ParameterValues;

        public MssqlSettingsProvider(string connectionStringName, Dictionary<string, string> parameterKVPs)
        {
            this._connectionStringName = connectionStringName;
            this.ParameterValues = parameterKVPs;
        }

        public MssqlSettingsProvider(string connectionStringName) : this(connectionStringName, new Dictionary<string, string>()) { }

        public MssqlSettingsProvider() : this("default", new Dictionary<string, string>()) { }

        /// <summary>
        /// Get the value from a SQL command
        /// </summary>
        /// <example>
        ///     select dbo.IsFeatureOn from features where featureId = 1001
        /// </example>
        /// <param name="key">The name of the config key</param>
        /// <returns>string</returns>
        public string GetValue(string key)
        {
            string result = null;
            var sqlQuery = ConfigurationManager.AppSettings[key];

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sqlQuery, connection))
                {
                    cmd.CommandType = CommandType.Text;

                    foreach (var parameter in ParameterValues)
                    {
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = parameter.Key, SqlValue = parameter.Value });
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.FieldCount.Equals(1))
                        {
                            while (reader.Read())
                            {
                                result = reader[0].ToString();
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
