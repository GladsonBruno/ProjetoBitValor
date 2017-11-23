using ProjetoBitValor.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjetoBitValor.Controllers
{
    [EnableCors("*", "*", "*")]
    public class BitValorController : ApiController
    {
        // GET: api/BitValor
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
                    Convert.ToInt32(ler["IdUsuario"]),
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

        // GET: api/BitValor/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BitValor
        public BitValor Post(BitValor value)
        {
            double taxaPrimeiraExchange = 0;
            double Fee = 0;
            switch (value.OpcaoCompra)
            {
                case "FB":
                    taxaPrimeiraExchange = 1.4;
                    Fee = 0.0003;
                    break;
                case "MB":
                    taxaPrimeiraExchange = 1.6;
                    Fee = 0.0002;
                    break;
                case "B2U":
                    taxaPrimeiraExchange = 1.8;
                    Fee = 0.0007;
                    break;
            }

            double taxaSegundaExchange = 0;
            switch (value.OpcaoVenda)
            {
                case "FB":
                    taxaSegundaExchange = 1.4;
                    break;
                case "MB":
                    taxaSegundaExchange = 1.6;
                    break;
                case "B2U":
                    taxaSegundaExchange = 1.8;
                    break;
            }

            //Calculando quando bitcoins foram comprados
            double bitCoinComprado = (1 / value.ValorCompra) * value.Montante;

            //Cobrando Taxa de Transferencia

            //Taxa de Fee de acordo com a Exchange
            bitCoinComprado = bitCoinComprado - Fee;

            double bitCoinDepoisDeTransferido = (bitCoinComprado) - ((bitCoinComprado / 100) * taxaPrimeiraExchange);

            //Calculando ganho após vender o bitcoin
            double ganhoComVendaDoBitCoin = value.ValorVenda * bitCoinDepoisDeTransferido;

            //Calculando valor ganho depois do saque incluindo a taxa de saque
            double dinheiroGanhoDepoisDaVenda = ganhoComVendaDoBitCoin - ((ganhoComVendaDoBitCoin / 100) * taxaSegundaExchange);

            //Calculando Porcentagem de Lucro
            double porcentagemDeLucro = 0;

            porcentagemDeLucro = (ganhoComVendaDoBitCoin / value.Montante) - 1;

            //Atribuo ao objeto as propriedades de quantidade e porcentagem de lucro
            value.QuantidadeLucro = dinheiroGanhoDepoisDaVenda - value.Montante;
            value.PorcentagemLucro = porcentagemDeLucro * 100;

            return value;
        }

        // PUT: api/BitValor/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BitValor/5
        public void Delete(int id)
        {
        }
    }
}
