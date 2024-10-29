using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.Infraestrutura.Db;

namespace minimal_api.Dominio.Servicos
{
    public class AdministradorService : IAdministradorService
    {

        private readonly DbContexto _context;

        public AdministradorService(DbContexto context)
        {
            _context = context;
        }

        public Administrador? Login(LoginDTO loginDTO)
        {
            var adm = _context.Administradores.Where(a => a.Email == loginDTO.Email  && a.Senha == loginDTO.Senha).FirstOrDefault();
        
            return adm;
        }
    }
}