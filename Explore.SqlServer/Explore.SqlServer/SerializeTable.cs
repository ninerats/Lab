using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;

namespace Explore.SqlServer
{
    [TestFixture]
    public class SerializeTableTest
    {
        [Test]
        public void AllTypesRoundTrip()
        {
            DB.ResetAllTypes();
            var fileName = "alltypes.xml";
            var table = DB.GetDataTableForTable("AllTypes");
            DB.SerializeTable(table, fileName);
            var readTable = DB.DeserializeTable("AllTypes", fileName);
            var adapter = new SqlDataAdapter("SELECT * FROM AllTypes", DB.GetConn());
            var cb = new SqlCommandBuilder(adapter);
            new SqlCommand("DELETE FROM AllTypes WHERE ID=2",DB.GetConn()).ExecuteNonQuery();
            readTable.Rows[0]["ID"] = 2;
            adapter.Update(readTable);

            // compare the data
            DataTable updatedTable = DB.GetDataTableForQuery("select * from AllTypes", "AllTypes");
            Assert.AreEqual(2, updatedTable.Rows.Count);
            foreach (DataColumn column in updatedTable.Columns)
            {
                if (column.ColumnName != "Mytimestamp" && column.ColumnName != "ID")
                {
                    Assert.AreEqual(updatedTable.Rows[0][column.ColumnName], updatedTable.Rows[1][column.ColumnName]);
                }
            }

        }

        [Test]
        public void DumpTable()
        {
            DB.ResetAllTypes();
            var conn = DB.GetConn();
            var adapter = new SqlDataAdapter("SELECT * FROM Person.Person Person", conn);
            var dataSet = new DataSet();
            adapter.Fill(dataSet);


            foreach (DataTable table in dataSet.Tables)
            {
                var path = string.Format("{0}.xml", table.TableName);
                Console.WriteLine("path={0}", path);
                DB.SerializeTable(table,path);
            }
        }

        [Test, Ignore]
        public void ResetAllTypes()
        {
            DB.ResetAllTypes();
        }

        [Test]
        public void UseSqlBulkCopy()
        {
            var bcp = new SqlBulkCopy(DB.GetConn());
        }
    }
}