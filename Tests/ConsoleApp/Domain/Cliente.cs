using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest.Domain
{
    //[Table("Clientes")]
    public class Cliente
    {
        //[Key] 
        public int Id { get; set; }
        //[Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; } = string.Empty;
        //[Column(name: "Phone")]
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
    }
}
