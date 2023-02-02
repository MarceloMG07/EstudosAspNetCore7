using ConsoleAppTest.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string CodigoBarras { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoProdutoEnum TipoProduto { get; set; }
        public bool Ativo { get; set; }
    }
}
