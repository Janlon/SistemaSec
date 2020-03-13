namespace Sec.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Generics.Helpers.Errors;
    using Sec.Models;
    using System.Data.Entity;

    /// <summary>
    /// Inicialização (criação, semeadura etc). Esta classe é, portanto, um helper do contexto de persistência.
    /// </summary>
    internal class DbInit : DropCreateDatabaseIfModelChanges<Db>
    {
        protected override void Seed(Db context)
        {
            // Tipos de documento padrão:
            try
            {
                List<TipoDeDocumento> documentos = new List<TipoDeDocumento>
                {
                    new TipoDeDocumento { Descricao = "Certidão de Nascimento", Sigla = "CN", PessoaFisica=true, Identificador=false},
                    new TipoDeDocumento { Descricao = "Certidão de Casamento", Sigla = "CUM", PessoaFisica=true, Identificador=false},
                    new TipoDeDocumento { Descricao = "Registro Geral", Sigla = "RG", PessoaFisica=true, Identificador=true},
                    new TipoDeDocumento { Descricao = "Autorização de Retorno ao Brasil", Sigla = "ARB", PessoaFisica=true, Identificador=false},
                    new TipoDeDocumento { Descricao = "Cartão do Cidadão", Sigla = "CC", PessoaFisica=true, Identificador=false},
                    new TipoDeDocumento { Descricao = "Cartão SUS", Sigla = "CS", PessoaFisica=true, Identificador=false},
                    new TipoDeDocumento { Descricao = "Carteira de Habilitação de Amador", Sigla = "CHA", PessoaFisica=true, Identificador=true},
                    new TipoDeDocumento { Descricao = "Carteira de Trabalho e Previdência Social", Sigla = "CTPS", PessoaFisica=true, Identificador=true},
                    new TipoDeDocumento { Descricao = "Carteira Nacional de Habilitação", Sigla = "CNH", PessoaFisica=true, Identificador=true},
                    new TipoDeDocumento { Descricao = "Cédula de Identidade", Sigla = "CI", PessoaFisica=true, Identificador=true},
                    new TipoDeDocumento { Descricao = "Certificado de Alistamento Militar", Sigla = "CAM", PessoaFisica=true, Identificador=false},
                    new TipoDeDocumento { Descricao = "Certificado de Dispensa de Incorporação", Sigla = "CDC", PessoaFisica=true , Identificador=false},
                    new TipoDeDocumento { Descricao = "Certificado de Registro e Licenciamento de Veículo", Sigla = "CRLV", PessoaFisica=true , Identificador=false},
                    new TipoDeDocumento { Descricao = "Conhecimento de Transporte Eletrônico", Sigla = "CTE", PessoaFisica=true , Identificador=false},
                    new TipoDeDocumento { Descricao = "Cadastro de Pessoas Físicas", Sigla = "CPF", PessoaFisica=true , Identificador=false},
                    new TipoDeDocumento { Descricao = "Passaporte", Sigla = "Passaporte", PessoaFisica=true, Identificador=true},
                    new TipoDeDocumento { Descricao = "Registro Nacional de Estrangeiros", Sigla = "RNE", PessoaFisica=true, Identificador=true},
                    new TipoDeDocumento { Descricao = "Título Eleitoral", Sigla = "TE", PessoaFisica=true, Identificador=false},
                    new TipoDeDocumento { Descricao = "Alvará de Funcionamento", Sigla = "AF", PessoaFisica=false, Identificador=false},
                    new TipoDeDocumento { Descricao = "Cadastro Nacional de Pessoa Jurídica", Sigla = "CNPJ", PessoaFisica=false, Identificador=true},
                    new TipoDeDocumento { Descricao = "Inscrição Estadual", Sigla = "IE", PessoaFisica=false, Identificador=true},
                    new TipoDeDocumento { Descricao = "Inscrição Municipal", Sigla = "IM", PessoaFisica=false, Identificador=true},
                    new TipoDeDocumento { Descricao = "Relação Anual de Informações Sociais", Sigla = "RAIS", PessoaFisica=false, Identificador=false},
                    new TipoDeDocumento { Descricao = "Cadastro Geral de Empregados e Desempregados", Sigla = "CAGED", PessoaFisica=false, Identificador=false},
                    new TipoDeDocumento { Descricao = "Guia de Recolhimento do FGTS", Sigla = "GRTS", PessoaFisica=false, Identificador=false},
                };
                context.TiposDeDocumentos.AddRange(documentos);
                context.SaveChanges();
            }
            catch (Exception ex) { ex.Log(); }
            try
            {
                if (context.Pessoas.Count() == 0)
                {
                    IEnumerable<Models.Atividade> Atividades = Engine.Cargos.Atividades();
                    Models.Atividade pf = Atividades
                        .Where(p => p.Id.StartsWith("21240"))
                        .FirstOrDefault();
                    Models.Atividade pj = Atividades
                        .Where(p => p.Id.StartsWith("86101"))
                        .FirstOrDefault();
                    List<Pessoa> pessoas = new List<Pessoa>();
                    pessoas.Add(new Pessoa()
                    {
                        Nome = "Janlon de Carvalho Rodrigues",
                        Email = "janloncavalchi@msn.com",
                        Apelido = "Janlon",
                        PessoaFisica = pf.PessoaFisica,
                        Atividade = pf.Descricao.Trim().ToUpper(),
                        Nascimento = new DateTime(1980, 1, 1)
                    });
                    pessoas.Add(new Pessoa()
                    {
                        Nome = "Santa Casa de Misericordia",
                        Email = "provedoria@scsantos.com.br",
                        Apelido = "Santa Casa",
                        PessoaFisica = pj.PessoaFisica,
                        Atividade = pj.Descricao.Trim().ToUpper(),
                        Nascimento = new DateTime(1912, 1, 1)
                    });
                    context.Pessoas.AddRange(pessoas);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { ex.Log(); }
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
