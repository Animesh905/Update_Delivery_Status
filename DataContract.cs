using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update_Delivery_Status
{
    internal class DataContract
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        string InsertSP_Name = ConfigurationManager.AppSettings["InsertSP_Name"].ToString();
        public DataTable SelectData()
        {
            DataTable dataTable = new DataTable();
            string SP_Name = ConfigurationManager.AppSettings["SP_Name"].ToString();
            
            int i = 1;
            while(i<4)
            {
                if (i == 1)
                {
                    int j = 1;
                    while (j < 3)
                    {
                        DataTable dt = new DataTable();
                        string status1 = ConfigurationManager.AppSettings["status1"].ToString();
                        string status2 = ConfigurationManager.AppSettings["status2"].ToString();
                        try
                        {
                            using (var con = new SqlConnection(connectionString))
                            using (var cmd = new SqlCommand(SP_Name, con))
                            using (var da = new SqlDataAdapter(cmd))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@status1", SqlDbType.VarChar).Value = status1;
                                cmd.Parameters.Add("@status2", SqlDbType.VarChar).Value = status2;
                                cmd.Parameters.Add("@Read1", SqlDbType.Int).Value = j;
                                da.Fill(dt);
                                dataTable.Merge(dt);
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                }
                i++;
            }

            return dataTable;
        }
        public int InsertData()
        {
            try
            {
                using (var con=new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand(InsertSP_Name,con))
                    {
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType= CommandType.StoredProcedure;
                            cmd.Parameters.Add("");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return -1;
        }

        public int UpdateData()
        {
            return 0;
        }

        public int DeleteData()
        {
            
        }
    }
}
