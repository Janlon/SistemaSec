namespace Sec.Dal
{
    #region Espaços de trabalho
    using Sec.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    #endregion

    /// <summary>
    /// Contexto dos dados: Customizações de trabalho.
    /// </summary>
    public partial class DB : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Vincular o inicializador ao contexto:
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Definir o inicializador do ORM:
            Database.SetInitializer<DB>(new SistemaSecInitializer());
            // Criar as estruturas:
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Ao ser solicitada a gravação dos dados, aplicar o processo de criptografia.
        /// </summary>
        /// <returns>Registros afetados.</returns>
        public override int SaveChanges()
        {
            var filtro = (EntityState.Added | EntityState.Modified);
            IObjectContextAdapter contextAdapter = this;
            contextAdapter.ObjectContext.DetectChanges();
            ObjectStateManager gerente = contextAdapter
                .ObjectContext
                .ObjectStateManager;
            List<ObjectStateEntry> pendingEntities = gerente
                .GetObjectStateEntries(filtro)
                .Where(en => !en.IsRelationship).ToList();
            // Criptografar pendentes:
            foreach (var entry in pendingEntities)
                EncryptEntity(entry.Entity);
            // Contabilizar atualizações:
            int result = base.SaveChanges();
            // Decriptografar os atualizados para prosseguir o uso:
            foreach (var entry in pendingEntities)
                DecryptEntity(entry.Entity);
            return result;
        }

        /// <summary>
        /// Ao ser solicitada a gravação dos dados, aplicar o processo de criptografia.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento da tarefa.</param>
        /// <returns>Registros afetados.</returns>
        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            var filtro = (EntityState.Added | EntityState.Modified);
            IObjectContextAdapter contextAdapter = this;
            contextAdapter.ObjectContext.DetectChanges();
            ObjectStateManager gerente = contextAdapter
                .ObjectContext
                .ObjectStateManager;
            var pendingEntities = gerente
                .GetObjectStateEntries(filtro)
                .Where(en => !en.IsRelationship).ToList();
            // Criptografar pendentes;
            foreach (var entry in pendingEntities)
                EncryptEntity(entry.Entity);
            var result = await base.SaveChangesAsync(cancellationToken);
            // Decriptografar os atualizados para prosseguir o uso.
            foreach (var entry in pendingEntities)
                DecryptEntity(entry.Entity);
            return result;
        }

        /// <summary>
        /// Para a carga dos dados criptografados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ObjectMaterialized(object sender, ObjectMaterializedEventArgs e) 
        { DecryptEntity(e.Entity); }

        /// <summary>
        /// Criptografia dos valores das propriedades criptografáveis.
        /// </summary>
        /// <param name="entity">Entidade a ser processada.</param>
        private void EncryptEntity(object entity)
        {
            // Obter a chave de criptografia:
            PropertyInfo propriedadeChave = entity
                .GetType()
                .GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(EncryptionKeyAttribute), true)
                .Any(a => p.PropertyType == typeof(String)))
                .FirstOrDefault();
            if (propriedadeChave != null)
            {
                string chave = propriedadeChave
                    .GetValue(entity)
                    .ToString();
                // Obter todas as propriedades que precisam ser criptografadas:
                var criptografaveis = entity.GetType()
                    .GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(EncryptedAttribute), true)
                    .Any(a => p.PropertyType == typeof(String)));
                foreach (var propriedade in criptografaveis)
                {
                    // Valor da propriedade:
                    string value = propriedade.GetValue(entity) as string;
                    if ((!String.IsNullOrEmpty(value)) && (!String.IsNullOrWhiteSpace(value)))
                        propriedade.SetValue(entity, Cryptis.Text.AESEncrypt(value, chave));
                }
            }
        }

        /// <summary>
        /// Decriptografia dos valores das propriedades criptografáveis.
        /// </summary>
        /// <param name="entity">Entidade a ser processada.</param>
        private void DecryptEntity(object entity)
        {
            // Obter a chave de criptografia:
            PropertyInfo propriedadeChave = entity
                .GetType()
                .GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(EncryptionKeyAttribute), true)
                .Any(a => p.PropertyType == typeof(String)))
                .FirstOrDefault();
            if (propriedadeChave != null)
            {
                string chave = propriedadeChave
                    .GetValue(entity)
                    .ToString();
                // Obter todas as propriedades que precisam ser criptografadas:
                var criptografaveis = entity.GetType()
                    .GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(EncryptedAttribute), true)
                    .Any(a => p.PropertyType == typeof(String)));
                foreach (var propriedade in criptografaveis)
                {
                    string encryptedValue = propriedade.GetValue(entity) as string;
                    if ((!String.IsNullOrEmpty(encryptedValue)) && (!String.IsNullOrWhiteSpace(encryptedValue)))
                    {
                        string value = Sec.Cryptis.Text.AESDecrypt(encryptedValue, chave);
                        Entry(entity).Property(propriedade.Name).OriginalValue = value;
                        Entry(entity).Property(propriedade.Name).IsModified = false;
                    }
                }
            }
        }
    }
}
