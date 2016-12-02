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
    public class SqlSettingsProvider : IFeatureProvider
    {
        private string _connectionStringName;
        private Dictionary<string, string> _parameterValues;

        public SqlSettingsProvider(string connectionStringName, Dictionary<string, string> parameterKVPs)
        {
            this._connectionStringName = connectionStringName;
            this._parameterValues = parameterKVPs;
        }

        public SqlSettingsProvider(string connectionStringName) : this(connectionStringName, new Dictionary<string, string>()) { }

        public SqlSettingsProvider() : this("default", new Dictionary<string, string>()) { }

        /// <summary>
        /// Get the value from a SQL command, something very generic and not instance-specific.
        /// </summary>
        /// <example>
        ///     select dbo.IsFeatureOn from features where featureId = 1001
        /// </example>
        /// <param name="key">The name of the config key</param>
        /// <returns>string</returns>
        public string GetValue(string key)
        {
            var sqlQuery = ConfigurationManager.AppSettings[key];

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sqlQuery, connection))
                {
                    cmd.CommandType = CommandType.Text;

                    foreach (var parameter in _parameterValues)
                    {
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = parameter.Key, SqlValue = parameter.Value });
                    }

                    var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.HasRows && reader.FieldCount.Equals(1))
                    {
                        return reader[0].ToString();
                    }
                }
            }

            return null;
        }
    }
}
