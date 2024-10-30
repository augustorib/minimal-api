
using minimal_api.Dominio.Entidades;

namespace minimal_api.Dominio.Interfaces
{   
    public interface IVeiculosService
    {
        List<Veiculo> ListAll(int pagina = 1, string? nome = null, string? marca = null );

        Veiculo? FindById(int id);

        void Create(Veiculo veiculo);
        void Update(Veiculo veiculo);
        void Delete(Veiculo veiculo);
    }
}