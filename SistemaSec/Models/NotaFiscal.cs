using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaSec.Models
{
    //[Table("NotasFiscais", Schema = "dbo")]
    //public class NotaFiscal
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Display(Name = "Número", AutoGenerateField = true, AutoGenerateFilter = true, Prompt ="Número", Description = "Número Nota Fiscal")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [RegularExpression("([1-9][0-9]*)")]
    //    [Range(0, int.MaxValue, ErrorMessage = "Digite um número válido")]
    //    public string Numero { get; set; }

    //    [Display(Name = "Data de Emissão")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    //    [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Data inválida")]
    //    public DateTime DtEmissao { get; set; }

    //    [Display(Name = "Data da Compra")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    //    [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Data inválida")]
    //    public DateTime DtCompra { get; set; }

    //    [Display(Name = "Data de Entrega")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    //    [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Data inválida")]
    //    public DateTime DtEntrega { get; set; }

    //    [Display(Name = "Equipamento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Equipamento")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Equipamento")]
    //    public int EquipamentosId { get; set; }
    //    public virtual Equipamento Equipamento { get; set; }

    //}
}