using System.Configuration;
using System.Data;
using System.IO;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;

namespace Update_Delivery_Status
{

    public static class ReadWrite_ExcelFile
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ExcelConnection"].ToString();

        public static DataTable ReadSheet()
        {
            using (OleDbConnection conn = new OleDbConnection())
            {
                DataTable dt = new DataTable();
                string Import_FileName = @"C:\Users\Animesh\Downloads\sep25_to_26_2000187576(1).xlsx";
                string fileExtension = Path.GetExtension(Import_FileName);
                if (fileExtension == ".xls")
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                if (fileExtension == ".xlsx")
                    conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0;HDR=NO;'";
                
                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * FROM [Sheet1$]";

                    comm.Connection = conn;

                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                        return dt;
                    }

                }
            }
        }
    }
}
