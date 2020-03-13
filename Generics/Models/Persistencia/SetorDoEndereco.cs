namespace Generic.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SetoresDosEnderecos", Schema = "Sec")]
    public class SetorDoEndereco
    {
        [ScaffoldColumn(false), Key(), DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 2)]
        public int SetorId { get; set; }

        [ScaffoldColumn(false), Key(), DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 1)]
        public int EnderecoId { get; set; }

        [ForeignKey("SetorId")]
        public virtual Setor Setor { get; set; }

        [ForeignKey("EnderecoId")]
        public virtual Endereco Endereco { get; set; }
    }
}
