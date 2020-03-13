namespace Sec.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("EntregasDasOrdensDeServiços", Schema = "Sec")]
    public class EntregaDoItemDaOrdemDeServico
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int ItemId { get; set; }

        [ScaffoldColumn(false)]
        public int PessoaId { get; set; }

        [Display(Name = "Observações", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Observações", Description = "Digite se houver uma observação a fazer")]
        [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [Column(TypeName = "Text")]
        public string Descricao { get; set; }

        [Display(Name = "Cadastro", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Cadastro", Description = "Data de cadastro")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        DateTime Cadastro { get; set; }

        [Display(Name = "Execução", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Execução", Description = "Data de execução")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        DateTime Execucao { get; set; }



        [ForeignKey("ItemId")]
        public virtual ItemDaOrdemDeServico Item { get; set; }

        [ForeignKey("PessoaId")]
        public virtual Pessoa Pessoa { get; set; }
    }

    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }


    //    [Display(Name = "Observação", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Observação", Description = "Digite se houver uma observação a fazer")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
    //    public string Descricao { get; set; }

    //    [Display(Name = "Data de garantia")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    //    [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Data inválida")]
    //    public DateTime DtGarantia { get; set; }

    //    [ScaffoldColumn(false)]
    //    [NotMapped]
    //    public DateTime DataCadastro
    //    {
    //        get { return dateCreated ?? DateTime.Now; }
    //        set { dateCreated = value; }
    //    }
    //    private DateTime? dateCreated = null;

    //    [Display(Name = "Ordem", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Ordem")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Ordem")]
    //    public int OrdensId { get; set; }
    //    public virtual Ordem Ordem { get; set; }

    //    [Display(Name = "Servico", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Servico")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Servico")]
    //    public int ServicosId { get; set; }
    //    public virtual Servico Servico { get; set; }

    //    [Display(Name = "Equipamento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Equipamento")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Equipamento")]
    //    public int EquipamentosId { get; set; }
    //    public virtual Equipamento Equipamento { get; set; }
    //}
}