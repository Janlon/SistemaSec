namespace Sec.Dal
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Sec.Models;
    using System.Data.Entity;

    /// <summary>
    /// Contexto dos dados
    /// </summary>
    public partial class DB : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public DB() : base(DbHelper.Connection, true)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.ValidateOnSaveEnabled = true;
            Configuration.EnsureTransactionsForFunctionsAndCommands = true;
            Configuration.UseDatabaseNullSemantics = true;
            Database.SetInitializer<DB>(new SistemaSecInitializer());
        }

        /// <summary>
        /// Construtor estático.
        /// </summary>
        /// <returns></returns>
        public static DB Create()
        {
            return new DB();
        }        

        #region Coleções
        public DbSet<TipoDeDocumento> TiposDeDocumentos { get; set; }
        public DbSet<Cargo> Cargos { get; set; }


        //public DbSet<Funcionario> Funcionarios { get; set; }
        //public DbSet<Fornecedor> Fornecedores { get; set; }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        //public DbSet<Setor> Setores { get; set; }        
        //public DbSet<PessoaDocumento> PessoasDocumentos { get; set; }
        #endregion
    }
}
