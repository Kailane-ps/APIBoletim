using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APIBoletimTarde.Context
{
    public class BoletimContext
    {
        SqlConnection con = new SqlConnection();

        public BoletimContext()
        {
            con.ConnectionString = @"Data Source=LAB105801\SQLEXPRESS2;Initial Catalog=boletim;User ID=sa;Password=sa132";
                            
        }

        public SqlConnection Conectar()
        {
            if(con.State == System.Data.ConnectionState.Closed) 
            {
                con.Open();
            }
            return con;
        }

        public void Desconectar()
        {
            if(con.State == System.Data.ConnectionState.Open)
            {

                if(con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

            }

        }

    }
}
