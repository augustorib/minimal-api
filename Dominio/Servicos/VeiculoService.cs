using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.Infraestrutura.Db;

namespace minimal_api.Dominio.Servicos
{
    public class VeiculoService : IVeiculosService
    {

        private readonly DbContexto _context;

        public VeiculoService(DbContexto context)
        {
            _context = context;
        }

        public List<Veiculo> ListAll(int pagina = 1, string? nome = null, string? marca = null)
        {
            var query = _context.Veiculos.AsQueryable();

            if(!string.IsNullOrEmpty(nome))
            {
                query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome}%"));
            }

            int itensPorPagina = 10;

            query = query.Skip((pagina - 1) * itensPorPagina).Take(itensPorPagina);

            return query.ToList();

        }

        public void Create(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            _context.SaveChanges();
        }


        public Veiculo? FindById(int id)
        {
            return _context.Veiculos.Where(v => v.Id == id).FirstOrDefault();
        }

        public void Update(Veiculo veiculo)
        {
            _context.Veiculos.Update(veiculo);
            _context.SaveChanges();
        }
        public void Delete(Veiculo veiculo)
        {
            _context.Veiculos.Remove(veiculo);
            _context.SaveChanges();
        }
    }
}