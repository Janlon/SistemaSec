namespace Generics.Dal
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    /// <summary>
    /// Contexto dos dados
    /// </summary>
    public abstract class DB<T> : IdentityDbContext<T> where T: IdentityUser
    {
        #region Inicialização
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public DB() : base(DbHelper.Connection, true)
        {
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.EnsureTransactionsForFunctionsAndCommands = true;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = true;
            Configuration.UseDatabaseNullSemantics = true;
            Configuration.ValidateOnSaveEnabled = true;
            Database.Log = s => DbHelper.DataLog(Database.Connection.Database, s);
        }

        /// <summary>
        /// Vincular o inicializador ao contexto:
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            PrepareModel(modelBuilder);
            // Criar as estruturas:
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        protected abstract void PrepareModel(DbModelBuilder modelBuilder);
    }
}
