using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{
    [Table("Imagens", Schema = "Sec")]
    public class Imagem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Nome", TypeName = "VARCHAR")]
        [Display(Name = "Nome", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Nome da imagem")]
        public string Nome { get; set; } = "";

        [Required]
        [DefaultValue(false)]
        [Display(Name = "Referencia", AutoGenerateField = true, AutoGenerateFilter = true, Description ="Imagem de referência", Prompt = "Principal")]
        public bool Principal { get; set; } = false;

        [Required]
        [Column("Imagem")]
        [Display(Name = "Imagem", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Imagem")]
        public byte[] File { get; set; }
       
        [Display(Name ="Pessoas")]
        public virtual List<Pessoa> Pessoas { get; set; } = new List<Pessoa>();

        [Display(Name = "Equipamentos")]
        public virtual List<Equipamento> Equipamentos { get; set; } = new List<Equipamento>();

    }
}