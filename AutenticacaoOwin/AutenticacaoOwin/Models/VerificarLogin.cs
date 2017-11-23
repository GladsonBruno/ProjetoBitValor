using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AutenticacaoOwin.Models
{
    public class VerificarLogin
    {

        public static bool verificarLogin(string email, string senha)
        {
            SqlConnection minhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["minhaConexao"].ConnectionString);
            minhaConexao.Open();

            string query = "select * from Usuarios where Email = @email and Password = @senha";
            SqlCommand comando = minhaConexao.CreateCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = query;
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@senha", senha);

            SqlDataReader ler = comando.ExecuteReader();

            if(ler.Read() == false)
            {
                return false;
            }else
            {
                return true;
            }

            minhaConexao.Close();
        }
    }
}