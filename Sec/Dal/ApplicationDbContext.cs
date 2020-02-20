using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sec.Dal
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Sec.Models;
    using System.Data.Entity;

    /// <summary>
    /// Contexto dos dados
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ApplicationDbContext() : base(DbHelper.Connection, true)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.ValidateOnSaveEnabled = true;
            Configuration.EnsureTransactionsForFunctionsAndCommands = true;
            Configuration.UseDatabaseNullSemantics = true;
            Database.SetInitializer<ApplicationDbContext>(new SistemaSecInitializer());
            //Database.CreateIfNotExists();
        }

        /// <summary>
        /// Construtor estático.
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
        /// Vincular o inicializador ao contexto:
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ApplicationDbContext>(new SistemaSecInitializer());
            base.OnModelCreating(modelBuilder);
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
