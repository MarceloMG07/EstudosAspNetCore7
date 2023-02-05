using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoRotas.Models
{
    public class Filme
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato incorreto.")]
        [Display(Name = "Data de Lançamento")]
        public DateTime DataLancamento { get; set; } = DateTime.Now;

        [RegularExpression(@"^[A-Z]+[a-zA-Z\u00C0-\u00FF""'\w-]*$", ErrorMessage = "Genero em formato inválido.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Genero { get; set; } = string.Empty;

        [Range(1, 1000, ErrorMessage = "Valor de {1} a {2}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; } = decimal.Zero;

        [
            Required(ErrorMessage = "O campo {0} é obrigatório."), 
            RegularExpression(
                @"^[0-5]*$", 
                ErrorMessage = "O valor deve ser {1} de 0 a 5."
            ), 
            Display(Name = "Avaliação")
        ]
        public int Avaliacao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
