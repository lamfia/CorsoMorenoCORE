using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CorsoCoreGabriel.Models.Options;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CorsoCoreGabriel.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        public ILogger<SqliteDatabaseAccessor> Logger { get; }
        public IOptionsMonitor<ConnectionStringsOptions> Connectionstringsoptions { get; }


        public SqliteDatabaseAccessor(ILogger<SqliteDatabaseAccessor> logger, IOptionsMonitor<ConnectionStringsOptions> connectionstringsoptions)
        {
            Logger = logger;
            Connectionstringsoptions = connectionstringsoptions;
        }


        public async Task<DataSet> QueryAsync(FormattableString Formattablequery)
        {
            this.Logger.LogInformation(Formattablequery.Format, Formattablequery.GetArguments());

            var queryArguments = Formattablequery.GetArguments();

            var sqliparameters = new List<SqliteParameter>();


            for (int i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliparameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }

            var query = Formattablequery.ToString();

            using (var conn = new SqliteConnection(Connectionstringsoptions.CurrentValue.Default))
            {


                await conn.OpenAsync();

                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddRange(sqliparameters);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        var dataSet = new DataSet();

                        do
                        {
                            var dataTable = new DataTable();
                            dataSet.Tables.Add(dataTable);
                            dataTable.Load(reader);

                        } while (!reader.IsClosed);

                        return dataSet;
                    }
                }


            }



        }
    }
}
