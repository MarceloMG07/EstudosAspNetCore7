namespace DemoRotas.Models
{
    public class Filme
    {
        public string Titulo { get; set; } = string.Empty;
        public DateTime DataLancamento { get; set; } = DateTime.Now;
        public string Genero { get; set; } = string.Empty;
        public decimal Valor { get; set; } = decimal.Zero;
        public int Avaliacao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
