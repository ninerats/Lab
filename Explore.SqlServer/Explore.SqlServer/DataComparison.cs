using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Explore.SqlServer
{
    [TestFixture]
    class DataComparison
    {
        [Test]
        public void DectectDifferences()
        {
            var table = DB.DeserializeTable("Production.Product", @"Resources\product.xml");
      //      table.Rows[0]["Color"] = "Mauve";
           table.AcceptChanges();

            using (var conn = DB.GetConn())
            {
                var adapter = new SqlDataAdapter("select * from production.product", conn);

                var updateTable = new DataTable("Production.Product");
                adapter.FillLoadOption = LoadOption.PreserveChanges;
                adapter.AcceptChangesDuringFill = true;

                adapter.Fill(updateTable);
                //var tableFromDb = ds.Tables["Production.Product"];
                table.Merge(updateTable);
                //DataTable d3 = dt1.GetChanges();
                DataTable d3 = table.DefaultView.ToTable(true, "ProductID");
               // table.Merge(tableFromDb, false);
                var changes = table.GetChanges();
                

            }
        }
    }
}
