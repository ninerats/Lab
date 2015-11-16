using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace Explore.SqlServer
{
    [TestFixture]
    public class ScratchPad
    {
        private string ColumnDataSerializer(Type dataType)
        {
            if (dataType.Name == "Byte")
            {
                return "(Byte)127";
            }
            if (dataType.Name == "Byte[]")
            {
                return "Encoding.ASCII.GetBytes(\"a_bytes\")";
            }
            if (dataType.Name == "Boolean")
            {
                return "true";
            }
            if (dataType.Name == "DateTime")
            {
                return "new DateTime(2015, 1, 1, 4, 15, 16).AddMilliseconds(66)";
            }
            if (dataType.Name == "DateTimeOffset")
            {
                return "new DateTimeOffset(new DateTime(2015, 1, 1,5,6,0), new TimeSpan(1,2,0))";
            }
            if (dataType.Name == "Decimal")
            {
                return "123456789.123456789";
            }
            if (dataType.Name == "Double")
            {
                return "123456789.123456789";
            }
            if (dataType.Name == "Guid")
            {
                return "new Guid(\"1234567890ABCDEF1234567890ABCDEF\")";
            }
            if (dataType.Name == "Int16")
            {
                return "(Int16)16";
            }
            if (dataType.Name == "Int32")
            {
                return "32";
            }
            if (dataType.Name == "Int64")
            {
                return "1234567890123245";
            }
            if (dataType.Name == "Object")
            {
                return "(Object)\"Object\"";
            }

            if (dataType.Name == "Single")
            {
                return "12345.6789";
            }
            if (dataType.Name == "SqlGeography")
            {
                return "new SqlGeography {STSrid = new SqlInt32(4326)}";
            }
            if (dataType.Name == "SqlGeometry")
            {
                return "new SqlGeometry {STSrid = new SqlInt32(1234)}";
            }
            if (dataType.Name == "SqlHierarchyId")
            {
                return "new SqlHierarchyId()";
            }
            if (dataType.Name == "String")
            {
                return "\"1245678\"";
            }
            if (dataType.Name == "TimeSpan")
            {
                return "new TimeSpan(1,2,3,4,678)";
            }


            return "unknown";
        }

        [Test]
        public void DumpTypeNames()
        {
            using (var conn = DB.GetConn())
            {
                var adapter = new SqlDataAdapter("select * from AllTypes", conn);
                var ds = new DataSet();
                adapter.FillSchema(ds, SchemaType.Source, "AllTypes");
                var table = ds.Tables["AllTypes"];
                foreach (DataColumn column in table.Columns)
                {
                    Console.WriteLine(column.DataType.Name);
                }
            }
        }

        [Test]
        public void GenerateFieldSettingCode()
        {
            using (var conn = DB.GetConn())
            {
                var adapter = new SqlDataAdapter("select * from AllTypes", conn);
                var ds = new DataSet();
                adapter.FillSchema(ds, SchemaType.Source, "AllTypes");
                var table = ds.Tables["AllTypes"];
                foreach (DataColumn column in table.Columns)
                {
                    Console.WriteLine("row[\"{0}\"] = {1};", column.ColumnName, ColumnDataSerializer(column.DataType));
                }
            }
        }
    }
}