using minimal_api.Dominio.Enums;

namespace minimal_api.Dominio.DTOs
{
    public class AdministradorDTO
    {
        public String Email { get; set; } = default!;
        public String Senha { get; set; } = default!;
        public Perfil? Perfil { get; set; } = default!;
    }
}