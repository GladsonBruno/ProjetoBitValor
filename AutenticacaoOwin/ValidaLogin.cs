using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AutenticacaoOwin
{
    public static class ValidaLogin
    {
        //Classe responsável por verificar se existe algum usuario com aquele login e senha no banco
        public static bool validarLogin(string email, string senha)
        {
            SqlConnection minhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["minhaConexao"].ConnectionString);
            minhaConexao.Open();

            string query = "select * from Usuarios where Email = @email and Password = @senha";
            SqlCommand comando = minhaConexao.CreateCommand();
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@senha", senha);

            SqlDataReader ler = comando.ExecuteReader();

            //Se não existir um usuario correspondente
            if(ler.Read() == false)
            {
                return false;
            }//Se existir um usuario correspondente
            else
            {
                return true;
            }
        }
    }
}