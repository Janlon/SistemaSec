using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sec.Dal
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
    public class SistemaSecInitializer : DropCreateDatabaseIfModelChanges<DB>
    {
        protected override void Seed(DB context)
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
                    new TipoDeDocumento { Descricao = "Certidão de Nascimento", Sigla = "CN" },
                    new TipoDeDocumento { Descricao = "Certidão de Casamento", Sigla = "CUM" },
                    new TipoDeDocumento { Descricao = "Registro Geral", Sigla = "RG" },
                    new TipoDeDocumento { Descricao = "Autorização de Retorno ao Brasil", Sigla = "ARB" },
                    new TipoDeDocumento { Descricao = "Cartão do Cidadão", Sigla = "CC" },
                    new TipoDeDocumento { Descricao = "Cartão SUS", Sigla = "CS" },
                    new TipoDeDocumento { Descricao = "Carteira de Habilitação de Amador", Sigla = "CHA" },
                    new TipoDeDocumento { Descricao = "Carteira de Trabalho e Previdência Social", Sigla = "CTPS" },
                    new TipoDeDocumento { Descricao = "Carteira Nacional de Habilitação", Sigla = "CNH" },
                    new TipoDeDocumento { Descricao = "Cédula de Identidade", Sigla = "CI" },
                    new TipoDeDocumento { Descricao = "Certificado de Alistamento Militar", Sigla = "CAM" },
                    new TipoDeDocumento { Descricao = "Certificado de Dispensa de Incorporação", Sigla = "CDC" },
                    new TipoDeDocumento { Descricao = "Certificado de Registro e Licenciamento de Veículo", Sigla = "CRLV" },
                    new TipoDeDocumento { Descricao = "Conhecimento de Transporte Eletrônico", Sigla = "CTE" },
                    new TipoDeDocumento { Descricao = "Cadastro de Pessoas Físicas", Sigla = "CPF" },
                    new TipoDeDocumento { Descricao = "Passaporte", Sigla = "Passaporte" },
                    new TipoDeDocumento { Descricao = "Registro Nacional de Estrangeiros", Sigla = "RNE" },
                    new TipoDeDocumento { Descricao = "Título Eleitoral", Sigla = "TE" },
                    new TipoDeDocumento { Descricao = "Alvará de Funcionamento", Sigla = "AF" },
                    new TipoDeDocumento { Descricao = "Cadastro Nacional de Pessoa Jurídica", Sigla = "CNPJ" },
                    new TipoDeDocumento { Descricao = "Inscrição Estadual", Sigla = "IE" },
                    new TipoDeDocumento { Descricao = "Inscrição Municipal", Sigla = "IM" },
                    new TipoDeDocumento { Descricao = "Relação Anual de Informações Sociais", Sigla = "RAIS" },
                    new TipoDeDocumento { Descricao = "Cadastro Geral de Empregados e Desempregados", Sigla = "CAGED" },
                    new TipoDeDocumento { Descricao = "Guia de Recolhimento do FGTS", Sigla = "GRTS" },
                };
                context.TiposDeDocumentos.AddRange(documentos);
                context.SaveChanges();
            }
            catch (Exception ex) { var p = ex; }
            // Cargos (CBO2002):
            try
            {
                context.Cargos.AddRange(DbHelper.CBO2002);
                context.SaveChanges();
            }
            catch (Exception ex) { var p = ex; }





            ////// Criar algumas pessoas, apenas para viabilizar o acesso inicial com dados de exemplo.
            ////try
            ////{
            ////    if (context.Pessoas.Count() == 0)
            ////    {
            ////        List<Pessoa> pessoas = new List<Pessoa>();
            ////        var pp = new Pessoa { Nome = "Janlon de Carvalho Rodrigues", Documento = "00000000000", Apelido = "Janlon", PessoaFisica = true };
            ////        pessoas.Add(pp);
            ////        pessoas.Add(new Pessoa { Nome = "Santa Casa de Misericordia", Documento="00000000001", Apelido="",  PessoaFisica = true });
            ////        pessoas.ForEach(s => context.Pessoas.AddOrUpdate(s));
            ////        context.SaveChanges();
            ////    }
            ////}
            ////catch (Exception ex) { var p = ex; }
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
