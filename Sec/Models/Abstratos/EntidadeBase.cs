namespace Sec.Models
{
    using Generics;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class EntidadeBase : IEntidadeBase
    {
        [ScaffoldColumn(false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(256)]
        public string DataKey { get; set; } = SuperKey.Create();

        [ScaffoldColumn(false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(256)]
        public string LifeKey { get; set; } = SuperKey.Create();
    }

}
