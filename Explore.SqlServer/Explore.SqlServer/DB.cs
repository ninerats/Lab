using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Explore.SqlServer
{
    public class DB
    {
        public static SqlConnection GetConn()
        {
            var conn = new SqlConnection(@"Data Source=(localdb)\v11.0;Initial Catalog=NSGWE;Integrated Security=True");
            conn.Open();
            return conn;
        }

        public static void ResetAllTypes()
        {
            using (var conn = GetConn())
            {
                var result = new SqlCommand("DELETE FROM AllTypes", conn).ExecuteNonQuery();
                var adapter = new SqlDataAdapter("select * from AllTypes", conn);
                var ds = new DataSet();
                adapter.Fill(ds, "AllTypes");
                var cb = new SqlCommandBuilder(adapter);
                var table = ds.Tables["AllTypes"];
                var row = table.NewRow();
                row["ID"] = 1;
                row["Mybigint"] = 1234567890123245;
                row["Mybinary256"] = new byte[]
                {
                    0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xA, 0xB, 0xC, 0xD, 0xE, 0xF, 0x10, 0x11, 0x12, 0x13,
                    0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20, 0x21, 0x22, 0x23, 0x24,
                    0x25, 0x26, 0x27, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35,
                    0x36, 0x37, 0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46,
                    0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57,
                    0x58, 0x59, 0x5A, 0x5B, 0x5C, 0x5D, 0x5E, 0x5F, 0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68,
                    0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F, 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79,
                    0x7A, 0x7B, 0x7C, 0x7D, 0x7E, 0x7F, 0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8A,
                    0x8B, 0x8C, 0x8D, 0x8E, 0x8F, 0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9A, 0x9B,
                    0x9C, 0x9D, 0x9E, 0x9F, 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC,
                    0xAD, 0xAE, 0xAF, 0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8, 0xB9, 0xBA, 0xBB, 0xBC, 0xBD,
                    0xBE, 0xBF, 0xC0, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xCA, 0xCB, 0xCC, 0xCD, 0xCE,
                    0xCF, 0xD0, 0xD1, 0xD2, 0xD3, 0xD4, 0xD5, 0xD6, 0xD7, 0xD8, 0xD9, 0xDA, 0xDB, 0xDC, 0xDD, 0xDE, 0xDF,
                    0xE0, 0xE1, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0xE7, 0xE8, 0xE9, 0xEA, 0xEB, 0xEC, 0xED, 0xEE, 0xEF, 0xF0,
                    0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB, 0xFC, 0xFD, 0xFE, 0xFF
                };
                row["Mybit"] = true;
                row["Mychar10"] = "1245678";
                row["Mydate"] = new DateTime(2015, 1, 1, 4, 15, 16).AddMilliseconds(66);
                row["Mydatetime"] = new DateTime(2015, 1, 1, 4, 15, 16).AddMilliseconds(66);
                row["Mydatetime2"] = new DateTime(2015, 1, 1, 4, 15, 16).AddMilliseconds(66);
                row["Mydatetimeoffset"] = new DateTimeOffset(new DateTime(2015, 1, 1, 5, 6, 0), new TimeSpan(1, 2, 0));
                row["Mydecimal18_0"] = 12345678912345678;
                row["Mydecimal4_4"] = 0.5678;
                row["Myfloat"] = 123456789.123456789;
                //row["Mygeography"] = SqlGeography.STGeomFromText(new SqlChars("POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))"),4326);
                //row["Mygeometry"] = SqlGeometry.STGeomFromText(new SqlChars("POLYGON((0 0,0 8,8 8,8 4,4 4,4 0,0 0))"), 0);
                //row["Myhierarchyid"] = new SqlHierarchyId();
                row["Myimage"] = Encoding.ASCII.GetBytes("a_bytes");
                row["Myint"] = 32;
                row["Mymoney"] = 123456789.12;
                row["Mynchar10"] = "1245678";
                row["Myntext"] = "1245678";
                row["Mynumeric"] = 123456789.123456789;
                row["Mynvarchar50"] = "1245678";
                row["Myreal"] = 12345.6789;
                row["Mysmalldatetime"] = new DateTime(2015, 1, 1, 4, 15, 16).AddMilliseconds(66);
                row["Mysmallint"] = (short) 16;
                row["Mysmallmoney"] = 12345.12;
                row["Mysql_variant"] = "Object";
                row["Mytext"] = "1245678";
                row["Mytime7"] = new TimeSpan(0, 2, 3, 4, 678);
                row["Mytimestamp"] = Encoding.ASCII.GetBytes("a_bytes");
                row["Mytinyint"] = (byte) 127;
                row["Myuniqueidentifier"] = new Guid("1234567890ABCDEF1234567890ABCDEF");
                row["Myvarbinary50"] = Encoding.ASCII.GetBytes("a_bytes");
                row["Myvarchar50"] = "1245678";
                row["Myxml"] = "<root><elem attrib='value'>content</elem></root>";
                row["MyFlagbit"] = true;
                row["MyNameStylebit"] = true;
                row["MyNamenvarchar50"] = "1245678";


                table.Rows.Add(row);
                adapter.Update(table);
            }
        }

        public static DataTable GetAllTypesDataTable()
        {
            using (var conn = GetConn())
            {
                var adapter = new SqlDataAdapter("select * from AllTypes", conn);
                var ds = new DataSet();
                adapter.Fill(ds, "AllTypes");
                return ds.Tables["AllTypes"];
            }
        }
    }
}