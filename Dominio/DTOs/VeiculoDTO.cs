
namespace minimal_api.Dominio.DTOs
{
    public record VeiculoDTO
    {
        public string Nome { get; set; } = default!;
  
        public string Marca { get; set; } = default!;

        public string Ano { get; set; } = default!;
        
    }
}