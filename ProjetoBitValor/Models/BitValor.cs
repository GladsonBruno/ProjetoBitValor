using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoBitValor.Models
{
    public class BitValor
    {
        public int Id{ get; set; }
        public string OpcaoCompra { get; set; }
        public float ValorCompra { get; set; }
        public string OpcaoVenda { get; set; }
        public float ValorVenda { get; set; }
        public float Montante { get; set; }
        public double QuantidadeLucro { get; set; }
        public double PorcentagemLucro { get; set; }
        
        public BitValor(int id, string opcCompra, float valCompra, string opcVenda, float valVenda, float montante, double qtdeLucro, double porcLucro)
        {
            Id = id;
            OpcaoCompra = opcCompra;
            ValorCompra = valCompra;
            OpcaoVenda = opcVenda;
            ValorVenda = valVenda;
            Montante = montante;
            QuantidadeLucro = qtdeLucro;
            PorcentagemLucro = porcLucro;
        }
        
    }
}