using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoBitValor.Models
{
    public class Usuario
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public Usuario()
        {

        }

        public Usuario(Usuario user)
        {
            this.Id = user.Id;
            this.Nome = user.Nome;
            this.Email = user.Email;
            this.Senha = user.Senha;
        }

        public Usuario(int id, string nome, string email, string senha)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
        }

        public Usuario Cadastrar()
        {
            SqlConnection minhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["minhaConexao"].ConnectionString);
            minhaConexao.Open();

            String Query = "insert into Usuarios(UserName, Email, Password) values(@NomeUsuario, @Email, @Senha)";

            SqlCommand comandoInsert = minhaConexao.CreateCommand();
            comandoInsert.CommandType = CommandType.Text;
            comandoInsert.CommandText = Query;
            comandoInsert.Parameters.AddWithValue("@NomeUsuario", this.Nome);
            comandoInsert.Parameters.AddWithValue("@Email", this.Email);
            comandoInsert.Parameters.AddWithValue("Senha", this.Senha);

            comandoInsert.ExecuteNonQuery();

            minhaConexao.Close();

            return this;
        }

        public Usuario Recuperar(string email, string senha)
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

            if (ler.Read())
            {
                this.Id = Convert.ToInt32(ler["Id"]);
                this.Nome = ler["UserName"].ToString();
                this.Email = email;
                this.Senha = senha;
            }
            return this;
        }
    }
}