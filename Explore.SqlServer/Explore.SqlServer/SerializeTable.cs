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
    public class SerializeTable
    {
        [Test]
        public void AllTypesRoundTrip()
        {
            DB.ResetAllTypes();
            var fileName = "alltypes.xml";
            var table = DB.GetAllTypesDataTable();
            using (var stream = new FileStream(fileName,FileMode.Create))
            {
                table.WriteXml(stream,XmlWriteMode.WriteSchema);
            }
            var readTable = new DataTable("AllTypes");
            using (var reader = new XmlTextReader(new FileStream(fileName, FileMode.Open)))
            {
                var mode = readTable.ReadXml(reader);
               
            }
            var adapter = new SqlDataAdapter("SELECT * FROM AllTypes", DB.GetConn());
            var cb = new SqlCommandBuilder(adapter);
            new SqlCommand("DELETE FROM AllTypes WHERE ID=2",DB.GetConn()).ExecuteNonQuery();
            readTable.Rows[0]["ID"] = 2;
            adapter.Update(readTable);
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
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    table.WriteXml(fs, XmlWriteMode.WriteSchema, false);
                }
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