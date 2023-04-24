using Microsoft.EntityFrameworkCore;

namespace Model
{
    public class LocacaoContext : DbContext
    {
        public LocacaoContext(DbContextOptions<LocacaoContext> options) : base(options)
        {}
        public LocacaoContext() {}
        public DbSet<CarroModel> Carro { get; set; }
        public DbSet<MarcaModel> Marca { get; set; }
    }
}
