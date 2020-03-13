using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sec
{

    /// <summary>
    /// Enumeração para tipos de filtros textuais.
    /// </summary>
    public enum StringFilter
    {
        /// <summary>
        /// É igual.
        /// </summary>
        [Display(Name = "Igual á", Description = "Igual á")]
        Equals,
        /// <summary>
        /// É diferente de.
        /// </summary>
        [Display(Name = "Diferente de", Description = "Diferente de")]
        Different,

        /// <summary>
        /// Inicia por.
        /// </summary>
        [Display(Name = "Começa com", Description = "Começa com")]
        StartsWith,
        /// <summary>
        /// Inicia por.
        /// </summary>
        [Display(Name = "Não começa com", Description = "Não começa com")]
        NotStartsWith,

        /// <summary>
        /// Termina com.
        /// </summary>
        [Display(Name = "Termina com", Description = "Termina com")]
        EndsWith,
        /// <summary>
        /// Não termina com.
        /// </summary>
        [Display(Name = "Não termina com", Description = "Não termina com")]
        NotEndsWith,



        /// <summary>
        /// Contém.
        /// </summary>
        [Display(Name = "Contém", Description = "Contém")]
        Contains,
        /// <summary>
        /// Não contém.
        /// </summary>
        [Display(Name = "Não contém", Description = "Não contém")]
        NotContains,

        /// <summary>
        /// É nulo.
        /// </summary>
        [Description("É nulo")]
        [Display(Name = "É nulo", Description = "É nulo")]
        IsNull,
        /// <summary>
        /// Não é nulo.
        /// </summary>
        [Description("Não é nulo")]
        [Display(Name = "Não é nulo", Description = "Não é nulo")]
        NotIsNull,

        /// <summary>
        /// É vazio.
        /// </summary>
        [Description("É vazio")]
        [Display(Name = "É vazio", Description = "É vazio")]
        IsEmpty,
        /// <summary>
        /// Não é vazio.
        /// </summary>
        [Description("Não é vazio")]
        [Display(Name = "Não é vazio", Description = "Não é vazio")]
        NotIsEmpty,


        /// <summary>
        /// É nulo ou vazio.
        /// </summary>
        [Description("É nulo ou vazio")]
        [Display(Name = "É nulo ou vazio", Description = "É nulo ou vazio")]
        IsNullOrEmpty,
        /// <summary>
        /// Não é nulo ou vazio.
        /// </summary>
        [Description("Não é nulo ou vazio")]
        [Display(Name = "Não é nulo ou vazio", Description = "Não é nulo ou vazio")]
        NotIsNullOrEmpty,
    }
    /// <summary>
    /// Enumeração para booleanos. Sim = 1, não =0.
    /// </summary>
    public enum Resposta 
    { 
        /// <summary>
        /// Sim.
        /// </summary>
        [Display(Name="Não",Description= "Não")]
        Não = 0,
        /// <summary>
        /// Não.
        /// </summary>
        [Display(Name = "Sim", Description = "Sim")]
        Sim = 1 
    }
}
