using ProjetoBitValor.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjetoBitValor.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CompraController : ApiController
    {
        // GET: api/Compra
        public IEnumerable<BitValor> Get()
        {
            SqlConnection minhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["minhaConexao"].ConnectionString);

            minhaConexao.Open();

            String select = "SELECT * FROM Compras";
            SqlCommand selectCommand = new SqlCommand(select, minhaConexao);

            SqlDataReader ler = selectCommand.ExecuteReader();

            List<BitValor> compras = new List<BitValor>();

            while (ler.Read())
            {
                compras.Add(new BitValor(Convert.ToInt32(ler["Id"]),
                    ler["OpcaoCompra"].ToString(),
                    float.Parse(ler["ValorCompra"].ToString()),
                    ler["OpcaoVenda"].ToString(),
                    float.Parse(ler["ValorVenda"].ToString()),
                    float.Parse(ler["Montante"].ToString()),
                    Convert.ToDouble(ler["QtdeLucro"].ToString()),
                    Convert.ToDouble(ler["PercentualLucro"].ToString())
                    ));
            }
            minhaConexao.Close();
            return (compras);
        }

        // GET: api/Compra/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Compra
        public void Post(BitValor value)
        {
            SqlConnection minhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["minhaConexao"].ConnectionString);
            minhaConexao.Open();
            String query = $"insert into Compras(OpcaoCompra, ValorCompra, OpcaoVenda, ValorVenda, Montante, QtdeLucro, PercentualLucro) values (@OpCompra, @ValCompra, @OpVenda, @ValVenda, @Montante, @QtdLucro, @PercLucro)";


            SqlCommand comandoInsert = minhaConexao.CreateCommand();
            comandoInsert.CommandType = CommandType.Text;
            comandoInsert.CommandText = query;

            comandoInsert.Parameters.AddWithValue("@OpCompra", value.OpcaoCompra);
            comandoInsert.Parameters.AddWithValue("@ValCompra", value.ValorCompra);
            comandoInsert.Parameters.AddWithValue("@OpVenda", value.OpcaoVenda);
            comandoInsert.Parameters.AddWithValue("@ValVenda", value.ValorVenda);
            comandoInsert.Parameters.AddWithValue("@Montante", value.Montante);
            comandoInsert.Parameters.AddWithValue("@QtdLucro", value.QuantidadeLucro);
            comandoInsert.Parameters.AddWithValue("@PercLucro", value.PorcentagemLucro);

            comandoInsert.ExecuteNonQuery();
            minhaConexao.Close();
        }

        // PUT: api/Compra/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Compra/5
        public void Delete(int id)
        {
            SqlConnection minhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["minhaConexao"].ConnectionString);
            minhaConexao.Open();
            
            string queryComandoDelete = "delete from compras where id = " + id + "";
            SqlCommand comandoDelete = minhaConexao.CreateCommand();
            comandoDelete.CommandType = CommandType.Text;
            comandoDelete.CommandText = queryComandoDelete;
            comandoDelete.ExecuteNonQuery();

            minhaConexao.Close();
        }
    }
}
