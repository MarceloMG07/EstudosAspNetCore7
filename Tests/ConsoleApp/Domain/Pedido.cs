using ConsoleAppTest.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public DateTime IniciadoEm { get; set; }
        public DateTime FinalizadoEm { get; set; }
        public TipoFreteEnum TipoFrete { get; set; }
        public StatusPedidoEnum Status { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public ICollection<PedidoItem>? Itens { get; set; }


    }
}
