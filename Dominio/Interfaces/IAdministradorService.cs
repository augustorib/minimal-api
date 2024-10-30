
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;

namespace minimal_api.Dominio.Interfaces
{
    public interface IAdministradorService
    {
        Administrador? Login(LoginDTO loginDTO);
        Administrador? FindById(int id);
        Administrador Create(Administrador administrador);
        List<Administrador> ListAll(int? pagina);
    }
}