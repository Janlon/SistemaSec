namespace Sec.Business
{
    using Generics.Dal;
    using Generics.Extensoes;
    using Sec.IdentityGroup;
    using Sec.Models;

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Net;
    using System.Net.Configuration;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Configuration;

    /// <summary>
    /// Implementação da BI.
    /// </summary>
    public partial class Db : DB<ApplicationUser>
    {
        #region Instância
        public Db()
        {
            Database.SetInitializer(new DbInit());
        }
        public Db Create() { return new Db(); }
        protected override void PrepareModel(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<EntregaDoItemDaOrdemDeServico>()
            //    .HasRequired(p => p.Pessoa)
            //    .WithMany(p => p.OrdensDeServicoEntregues)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<RetiradaDoItemDaOrdemDeServico>()
            //    .HasRequired(p => p.Pessoa)
            //    .WithMany(p => p.OrdensDeServicoRetiradas)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contato>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_Contato"))
                .Delete(d => d.HasName("del_Contato"))
                .Insert(i => i.HasName("ins_Contato")));

            modelBuilder.Entity<Documento>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_Documento"))
                .Delete(d => d.HasName("del_Documento"))
                .Insert(i => i.HasName("ins_Documento")));

            modelBuilder.Entity<Empresa>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_Empresa"))
                .Delete(d => d.HasName("del_Empresa"))
                .Insert(i => i.HasName("ins_Empresa")))
                .HasMany(p => p.Setores)
                .WithRequired(p => p.Empresa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmpresaFilial>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_Filial"))
                .Delete(d => d.HasName("del_Filial"))
                .Insert(i => i.HasName("ins_Filial")));

            modelBuilder.Entity<EmpresaMatriz>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_Matriz"))
                .Delete(d => d.HasName("del_Matriz"))
                .Insert(i => i.HasName("ins_Matriz")));

            modelBuilder.Entity<EmpresaMatrizFilial>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_MatrizFilial"))
                .Delete(d => d.HasName("del_MatrizFilial"))
                .Insert(i => i.HasName("ins_MatrizFilial")));

            modelBuilder.Entity<Endereco>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_Endereco"))
                .Delete(d => d.HasName("del_Endereco"))
                .Insert(i => i.HasName("ins_Endereco")));

            modelBuilder.Entity<EnderecoDaPessoa>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_EnderecoDaPessoa"))
                .Delete(d => d.HasName("del_EnderecoDaPessoa"))
                .Insert(i => i.HasName("ins_EnderecoDaPessoa")));

            modelBuilder.Entity<EntregaDoItemDaOrdemDeServico>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_EntregaDoItemDaOrdemDeServico"))
                .Delete(d => d.HasName("del_EntregaDoItemDaOrdemDeServico"))
                .Insert(i => i.HasName("ins_EntregaDoItemDaOrdemDeServico")));

            modelBuilder.Entity<Equipamento>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_Equipamento"))
                .Delete(d => d.HasName("del_Equipamento"))
                .Insert(i => i.HasName("ins_Equipamento")))
                .HasMany(t => t.Imagens)
                .WithMany(t => t.Equipamentos)
                .Map(m =>
                {
                    m.ToTable("ImagensDosEquipamentos", "Sec");
                    m.MapLeftKey("EquipamentoId");
                    m.MapRightKey("ImagemId");
                });

            modelBuilder.Entity<Imagem>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_Imagem"))
                .Delete(d => d.HasName("del_Imagem"))
                .Insert(i => i.HasName("ins_Imagem")));

            modelBuilder.Entity<ItemDaOrdemDeServico>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_ItemDaOrdemDeServico"))
                .Delete(d => d.HasName("del_ItemDaOrdemDeServico"))
                .Insert(i => i.HasName("ins_ItemDaOrdemDeServico")));

            modelBuilder.Entity<OrdemDeServico>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_OrdemDeServico"))
                .Delete(d => d.HasName("del_OrdemDeServico"))
                .Insert(i => i.HasName("ins_OrdemDeServico")));

            modelBuilder.Entity<Pessoa>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_Pessoa"))
                .Delete(d => d.HasName("del_Pessoa"))
                .Insert(i => i.HasName("ins_Pessoa")))
                .HasMany(t => t.Imagens)
                .WithMany(t => t.Pessoas)
                .Map(m =>
                {
                    m.ToTable("ImagensDasPessoas", "Sec");
                    m.MapLeftKey("PessoaId");
                    m.MapRightKey("ImagemId");
                });

            modelBuilder.Entity<RetiradaDoItemDaOrdemDeServico>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_RetiradaDoItemDaOrdemDeServico"))
                .Delete(d => d.HasName("del_RetiradaDoItemDaOrdemDeServico"))
                .Insert(i => i.HasName("ins_RetiradaDoItemDaOrdemDeServico")));

            modelBuilder.Entity<Servico>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_Servico"))
                .Delete(d => d.HasName("del_Servico"))
                .Insert(i => i.HasName("ins_Servico")));

            modelBuilder.Entity<Setor>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_Setor"))
                .Delete(d => d.HasName("del_Setor"))
                .Insert(i => i.HasName("ins_Setor")));

            modelBuilder.Entity<TipoDeDocumento>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_TipoDeDocumento"))
                .Delete(d => d.HasName("del_TipoDeDocumento"))
                .Insert(i => i.HasName("ins_TipoDeDocumento")));

            modelBuilder.Entity<TipoDeEquipamento>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_TipoDeEquipamento"))
                .Delete(d => d.HasName("del_TipoDeEquipamento"))
                .Insert(i => i.HasName("ins_TipoDeEquipamento")));

            modelBuilder.Entity<TipoDeSetor>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_TipoDeSetor"))
                .Delete(d => d.HasName("del_TipoDeSetor"))
                .Insert(i => i.HasName("ins_TipoDeSetor")));

            modelBuilder.Entity<Usuario>()
                .MapToStoredProcedures(s => s
                .Update(u => u.HasName("upd_Usuario"))
                .Delete(d => d.HasName("del_Usuario"))
                .Insert(i => i.HasName("ins_Usuario")));

        }
        #endregion

        #region Coleções
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<EnderecoDaPessoa> EnderecosDasPessoas { get; set; }
        public DbSet<EntregaDoItemDaOrdemDeServico> Entregas { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<ItemDaOrdemDeServico> ItensDasOrdensDeServico { get; set; }
        public DbSet<OrdemDeServico> OrdensDeServico { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EmpresaMatriz> Matrizes { get; set; }
        public DbSet<EmpresaFilial> Filiais { get; set; }
        public DbSet<EmpresaMatrizFilial> MatrizesFiliais { get; set; }
        public DbSet<RetiradaDoItemDaOrdemDeServico> Retiradas { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<TipoDeSetor> TiposDeSetores { get; set; }
        public DbSet<TipoDeDocumento> TiposDeDocumentos { get; set; }
        public DbSet<TipoDeEquipamento> TiposDeEquipamentos { get; set; }
        #endregion

        #region Extensões
        /// <summary>
        /// Enviar emails para endereços.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="fromMail"></param>
        /// <param name="fromName"></param>
        /// <param name="toMails"></param>
        /// <returns></returns>
        internal async static Task SendEmailAsync(string body, string subject, string fromMail, string fromName, MailAddress[] toMails)
        {
            Action a = new Action(() =>
            {
                foreach (MailAddress ma in toMails)
                    SendEmail(body, subject, fromMail, fromName, ma.Address, ma.DisplayName);
            });
            await Task.Run(a);
        }
        /// <summary>
        /// Enviar emails para endereços.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="fromMail"></param>
        /// <param name="fromName"></param>
        /// <param name="toMail"></param>
        /// <param name="toName"></param>
        /// <returns></returns>
        internal async static Task SendEmailAsync(string body, string subject, string fromMail, string fromName, string toMail, string toName)
        {
            Action a = new Action(() =>
            {
                SendEmail(body, subject, fromMail, fromName, toMail, toName);
            });
            await Task.Run(a);
        }
        /// <summary>
        /// Enviar emails para endereços.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="from"></param>
        /// <param name="toMails"></param>
        internal static void SendEmail(string body, string subject, MailAddress from,             IEnumerable<MailAddress> toMails)
        {
            foreach (MailAddress to in toMails)
                SendEmail(body, subject, from.Address, from.DisplayName, to.Address, to.DisplayName);
        }
        /// <summary>
        /// Enviar emails para endereços.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        internal static void SendEmail(string body, string subject, MailAddress from, MailAddress to)
        {
            SendEmail(body, subject, from.Address, from.DisplayName, to.Address, to.DisplayName);
        }
        /// <summary>
        /// Enviar emails para endereços.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="from"></param>
        /// <param name="toName"></param>
        /// <param name="toMail"></param>
        internal static void SendEmail(string body, string subject, MailAddress from, string toMail, string toName)
        {
            SendEmail(body, subject, from.Address, from.DisplayName, toMail, toName);
        }
        /// <summary>
        /// Enviar emails para endereços.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="fromMail"></param>
        /// <param name="fromName"></param>
        /// <param name="toMails"></param>
        internal static void SendEmail(string body, string subject, string fromMail, string fromName, IEnumerable<MailAddress> toMails)
        {
            foreach (MailAddress to in toMails)
                SendEmail(body, subject, fromMail, fromName, to.Address, to.DisplayName);
        }
        /// <summary>
        /// Enviar um email usando as configurações.
        /// </summary>
        /// <param name="body">Corpo do e-mail</param>
        /// <param name="subject">Título do email</param>
        /// <param name="fromMail">Endereço de quem envia</param>
        /// <param name="fromName">Nome de quem envia</param>
        /// <param name="toMail">Endereço de quem recebe</param>
        /// <param name="toName">Nome de quem recebe</param>
        internal static void SendEmail(string body, string subject, string fromMail, string fromName, string toMail, string toName)
        {
            NetworkCredential cred = null;
            MailSettingsSectionGroup cfg = null;
            Configuration cfgf = null;
            SmtpClient smtpClient = null;
            MailMessage mm = null;
            cfgf = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            if (cfgf != null)
            {
                cfg = (MailSettingsSectionGroup)cfgf.GetSectionGroup("system.net/mailSettings");
                if (cfg != null)
                {
                    cred = new NetworkCredential(cfg.Smtp.Network.UserName, cfg.Smtp.Network.Password);
                    try
                    {
                        using (smtpClient = new SmtpClient(cfg.Smtp.Network.Host))
                        {
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = cred;
                            using (mm = new MailMessage())
                            {
                                mm.From = new MailAddress(fromMail, fromName);
                                mm.To.Add(new MailAddress(toMail, toName));
                                mm.IsBodyHtml = body.IsHtml();
                                mm.Body = body;
                                mm.Subject = subject;
                                smtpClient.Send(mm);
                            }
                        }
                    }
                    catch (Exception ex) { ex.Log(); }
                    finally
                    {
                        try { if (cred != null) cred = null; } catch { }
                        try { if (cfg != null) cfg = null; } catch { }
                        try { if (cfgf != null) cfgf = null; } catch { }
                        try { if (smtpClient != null) { smtpClient.Dispose(); smtpClient = null; } } catch { }
                        try { if (mm != null) { mm.Dispose(); mm = null; } } catch { }
                        GC.Collect();
                    }
                }
            }
        }
        #endregion
    }
}