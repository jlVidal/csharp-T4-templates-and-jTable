using System;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Remoting.Messaging;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FooMetadataTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IndexInfo()
        {
            var pathConn = @"";
            var tableName = "Foo";

            Server server = new Server(new ServerConnection(new SqlConnection(pathConn)));
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(pathConn);
            var database = new Database(server, String.IsNullOrWhiteSpace(connectionStringBuilder.InitialCatalog) ? connectionStringBuilder.AttachDBFilename : connectionStringBuilder.InitialCatalog);
            var table = new Table(database, tableName);
            table.Refresh();

            var indexInfo = table.Columns.OfType<Column>().Select(a =>
            {
                var obj = new {a.IsFullTextIndexed, EnumIndexes = a.EnumIndexes(), Obj = a};
                return obj;
            }).ToArray();

        }

        [TestMethod]
        public void CreatingMetadata()
        {
            string pathConn = @"";
            var tableName = "Foo";
            var command = "SELECT TOP 0 * FROM " + tableName;

            var data = GetSqlSchema(pathConn, command);

            var schema = data.Rows.Cast<DataRow>().
                                  Select(a => data.Columns.Cast<DataColumn>().Select(b => b.ColumnName).Select(b => new KeyValuePair<string,object>(b, a[b]))).
                                  Select(a => SimpleDbBasedMapper<ColumnSchema>.Map(a, new ColumnSchema())).
                                  ToArray();

            Assert.IsTrue(schema != null && schema.Length > 0);
        }

        private static DataTable GetSqlSchema(string pathConn, string command)
        {
            using (var conn = new SqlConnection(pathConn))
            {
                //FillData(pathConn, command);
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = command;
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (var result = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
                    {
                        var schema = result.GetSchemaTable();
                        return schema;
                    }
                }
            }
        }

        private static DataSet FillData(string pathConn, string command)
        {
            using (var cmd = new SqlConnection(pathConn).CreateCommand())
            {
                cmd.CommandText = command;
                cmd.CommandType = System.Data.CommandType.Text;

                using (var result = new SqlDataAdapter(cmd))
                {
                    //var dsSchema = new DataSet();
                    //result.FillSchema(dsSchema, SchemaType.Source);

                    var dsResult = new DataSet();
                    result.Fill(dsResult);

                    return dsResult;
                }
            }
        }
    }
}
