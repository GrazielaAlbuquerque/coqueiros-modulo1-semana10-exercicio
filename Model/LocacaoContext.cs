using Microsoft.EntityFrameworkCore;
using Semana10.Model;

namespace Model
{
    public class LocacaoContext : DbContext
    {
        public LocacaoContext(DbContextOptions<LocacaoContext> options) : base(options)
        {
        }
        public DbSet<CarroModel> Carro { get; set; }
        public DbSet<MarcaModel> Marca { get; set; }
    }
}