using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace CorsoCoreGabriel.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        public async Task<DataSet> Query(FormattableString Formattablequery)
        {

            var queryArguments = Formattablequery.GetArguments();

            var sqliparameters = new List<SqliteParameter>();


            for (int i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliparameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }

            var query = Formattablequery.ToString();

            using (var conn = new SqliteConnection("Data Source=Data/MyCourse.db"))
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
