using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Model

{
    [Table("Carros")]
    public class CarroModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [NotNull]
        [Column("Nome")]
        public string Nome { get; set; }
        [Column("DataLocacao")]
        public DateTime DataLocacao { get; set; }
        [Column("IdMarca")]
        [ForeignKey("MarcaModel")]
        public int MarcaId {get; set;}
        public MarcaModel Marca { get; set; }
    }
}
