namespace SistemaSec
{
    namespace Dal
    {
        using Microsoft.AspNet.Identity.EntityFramework;
        using Newtonsoft.Json;
        using Sec.Models;
        using System;
        using System.Collections.Generic;
        using System.Data.Entity;
        using System.Data.Entity.Migrations;
        using System.Linq;

        /// <summary>
        /// Inicialização (criação, semeadura etc). Esta classe é, portanto, um helper do contexto de persistência.
        /// </summary>
        public class SistemaSecInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                // Perfís de acesso padrão:
                try
                {
                    List<IdentityRole> regras = new List<IdentityRole>();
                    regras.Add(new IdentityRole() { Name = "Administrativo", Id = Guid.NewGuid().ToString() });
                    regras.Add(new IdentityRole() { Name = "Técnico", Id = Guid.NewGuid().ToString() });
                    regras.Add(new IdentityRole() { Name = "Financeiro", Id = Guid.NewGuid().ToString() });
                    regras.Add(new IdentityRole() { Name = "Desenvolvimento", Id = Guid.NewGuid().ToString() });
                    foreach (IdentityRole rg in regras)
                        context.Roles.Add(rg);
                    context.SaveChanges();
                }
                catch (Exception ex) { var p = ex; }

                // Tipos de documento padrão:
                try
                {
                    List<TipoDeDocumento> documentos = new List<TipoDeDocumento>
                {
                    new TipoDeDocumento { Descricao = "Cadastro de Pessoa Física", Sigla = "CPF" },
                    new TipoDeDocumento { Descricao = "Cadastro Nacional de Pessoa Jurídica", Sigla = "CNPJ" },
                    new TipoDeDocumento { Descricao = "Registro Geral", Sigla = "RG" },
                    new TipoDeDocumento { Descricao = "Carteira Nacional de Habilitação", Sigla = "CNH" },
                    new TipoDeDocumento { Descricao = "Carteira de Trabalho e Previdência Social", Sigla = "CTPS" }
                };
                    context.TiposDeDocumentos.AddRange(documentos);
                    context.SaveChanges();
                }
                catch (Exception ex) { var p = ex; }

                // Cargos (CBO2002):
                try
                {
                    context.Cargos.AddRange(JsonConvert
                        .DeserializeObject<List<Cargo>>(Properties.Resources.cbo2002)
                        .Where(p => !string.IsNullOrEmpty(p.Descricao))
                        .ToList());
                    context.SaveChanges();
                }
                catch (Exception ex) { var p = ex; }

                // Criar algumas pessoas, apenas para viabilizar o acesso inicial com dados de exemplo.
                try
                {
                    if (context.Pessoas.Count() == 0)
                    {
                        List<Pessoa> pessoas = new List<Pessoa>();
                        pessoas.Add(new Pessoa { Nome = "Janlon de Carvalho Rodrigues", PessoaFisica= Resposta.Sim });
                        pessoas.Add(new Pessoa { Nome = "Santa Casa de Misericordia", PessoaFisica = Resposta.Não });
                        pessoas.ForEach(s => context.Pessoas.AddOrUpdate(s));
                        context.SaveChanges();
                    }
                }
                catch (Exception ex) { var p = ex; }

                //List<Cargo> cargos = new List<Cargo>
                //{
                //    new Cargo { Descricao = "Analista de Sistemas", Sigla = "ASIS" },
                //    new Cargo { Descricao = "Suporte", Sigla = "SUP" },
                //    new Cargo { Descricao = "Auxiliar de Suporte", Sigla = "SUP-AUX" },
                //    new Cargo { Descricao = "Médico/a", Sigla = "MED" },
                //    new Cargo { Descricao = "Enfermeiro/a", Sigla = "ENF" },
                //    new Cargo { Descricao = "Auxiliar de Enfermagem", Sigla = "ENF-AUX" },
                //    new Cargo { Descricao = "Maqueiro/a", Sigla = "MAQ" },
                //    new Cargo { Descricao = "Estagiário/a", Sigla = "ESTAG" }
                //};
                //cargos.ForEach(s => context.Cargos.AddOrUpdate(s));
                //context.SaveChanges();
                //List<Setor> setores = new List<Setor>
                //{
                //    new Setor { Descricao = "Laboratório", Sigla = "LAB" },
                //    new Setor { Descricao = "Unidade de Terapia Intensiva", Sigla = "UTI" },
                //    new Setor { Descricao = "Enfermagem", Sigla = "ENF" }
                //};
                //setores.ForEach(s => context.Setores.AddOrUpdate(s));
                //context.SaveChanges();            
            }
        }        
    }
}