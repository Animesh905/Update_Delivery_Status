﻿using System.Configuration;
using System.Data;
using System.IO;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;

namespace Update_Delivery_Status
{

    public static class ReadWrite_ExcelFile
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ExcelConnection"].ToString();

        public static DataTable ReadSheet(string FilePath)
        {
            using (OleDbConnection conn = new OleDbConnection())
            {
                DataTable dt = new DataTable();
                string Import_FileName = FilePath;
                string fileExtension = Path.GetExtension(Import_FileName);
                if (fileExtension == ".xls")
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                if (fileExtension == ".xlsx")
                    conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0;HDR=YES;'";
                
                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select MSG FROM [Sheet1$]";

                    comm.Connection = conn;

                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                        foreach(DataRow dr in dt.Rows)
                        {
                            string data = dr["MSG"].ToString();
                            data = data.Replace("'", "");
                            dr["MSG"] = data;
                        }
                        return dt;
                    }

                }
            }
        }
    }
}
