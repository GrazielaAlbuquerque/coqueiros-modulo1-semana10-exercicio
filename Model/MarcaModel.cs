using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace Model
{
    [Table("Marcas")]
    public class MarcaModel
    {
        [Key]
        [Column("Id")]
        public int Id {get; set; }
        [NotNull]
        [Column("Nome")]
        public string Nome {get; set;}
    }
}
