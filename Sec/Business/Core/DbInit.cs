﻿namespace Sec.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sec.Models;
    using System.Data.Entity;
    using Generics.Extensoes;

    /// <summary>
    /// Inicialização (criação, semeadura etc). Esta classe é, portanto, um helper do contexto de persistência.
    /// </summary>
    internal class DbInit : DropCreateDatabaseIfModelChanges<Db>
    {
        protected override void Seed(Db context)
        {
            // TipoDeDocumento
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
            // TipoDeEquipamento
            try
            {
                List<TipoDeEquipamento> equipamentos = new List<TipoDeEquipamento>
                {
                    new TipoDeEquipamento { Descricao = "Aparelho de raio X", Sigla = "RX"},
                    new TipoDeEquipamento { Descricao = "Aparelho de hemodiálise", Sigla = "HMD"},
                    new TipoDeEquipamento { Descricao = "Cardioversor", Sigla = "CV"},
                    new TipoDeEquipamento { Descricao = "Central de monitorização", Sigla = "CM"},
                    new TipoDeEquipamento { Descricao = "Desfibrilador", Sigla = "DFI"},
                    new TipoDeEquipamento { Descricao = "Incubadora", Sigla = "ICB"},
                    new TipoDeEquipamento { Descricao = "Oxicapnógrafo", Sigla = "OXCP"}

                };
                context.TiposDeEquipamentos.AddRange(equipamentos);
                context.SaveChanges();
            }
            catch (Exception ex) { ex.Log(); }
            // TipoDeSetor
            try
            {
                List<TipoDeSetor> setores = new List<TipoDeSetor>
                {
                    new TipoDeSetor { Descricao = "Ambulatório", Sigla = "AMB"},
                    new TipoDeSetor { Descricao = "Unidade de Terapia Intensiva", Sigla = "UTI"},
                    new TipoDeSetor { Descricao = "Setor de Traumatologia", Sigla = "TRM"},
                    new TipoDeSetor { Descricao = "Consultório Médico", Sigla = "CM"},
                    new TipoDeSetor { Descricao = "Farmácia", Sigla = "FM"},
                    new TipoDeSetor { Descricao = "Pediatria", Sigla = "PD"},
                    new TipoDeSetor { Descricao = "Almoxarifado", Sigla = "ALMX"}

                };
                context.TiposDeSetores.AddRange(setores);
                context.SaveChanges();
            }
            catch (Exception ex) { ex.Log(); }
            // Servico
            try
            {
                List<Servico> servicos = new List<Servico>
                {
                    new Servico { Descricao = "Limpeza"},
                    new Servico { Descricao = "Manutenção"},
                    new Servico { Descricao = "Teste"},
                    new Servico { Descricao = "Orçamento"}
                };
                context.Servicos.AddRange(servicos);
                context.SaveChanges();
            }
            catch (Exception ex) { ex.Log(); }
            // Pessoa
            try
            {
                IEnumerable<Models.Atividade> Atividades = Engine.Cargos.Atividades();
                if (context.Pessoas.Count() == 0)
                {
                    Models.Atividade pf = Atividades.Where(p => p.Id.StartsWith("21240")).FirstOrDefault();
                    context.Pessoas.Add(new Pessoa()
                    {
                        Nome = "Janlon de Carvalho Rodrigues",
                        Email = "janloncavalchi@msn.com",
                        Atividade = ((pf == null) ? "" : pf.Descricao.Trim().ToUpper()),
                        CPF = "81285060130"               
                    });
                    context.SaveChanges();


                    context.Pessoas.Add(new Pessoa()
                    {
                        Nome = "Olga Cavalchi de Carvalho",
                        Email = "olga@email.com",
                        Atividade = ((pf == null) ? "" : pf.Descricao.Trim().ToUpper()),
                        CPF = "37930464008"
                    });
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { ex.Log(); }
            // Empresa
            try
            {
                IEnumerable<Models.Atividade> Atividades = Engine.Cargos.Atividades();
                if (context.Empresas.Count() == 0)
                {
                    Endereco endereco = new Endereco() { Localidade = "Santos", Bairro = "Jabaquara", CEP = "11075900", Complemento = "", Logradouro = "Av. Dr. Cláudio Luís da Costa, 50", UF = "SP" };
                    context.Enderecos.Add(endereco);
                    context.SaveChanges();

                    Models.Atividade pj = Atividades.Where(p => p.Id.StartsWith("86101")).FirstOrDefault();                    
                    context.Empresas.Add(
                    new Empresa()
                    {
                        RazaoSocial = "Santa Casa de Misericordia",
                        NomeFantasia = "Santa Casa",
                        Atividade = ((pj == null) ? "" : pj.Descricao.Trim().ToUpper()),
                        CNPJ = "25503424000108",
                        Endereco = endereco
                    });
                    context.SaveChanges();


                    Endereco endereco1 = new Endereco() { Localidade = "Praia Grande", Bairro = "Vila Mirim", CEP = "11704700", Complemento = "Casa 1077", Logradouro = "Av. 31 de Março", UF = "SP" };
                    context.Enderecos.Add(endereco1);
                    context.SaveChanges();


                    context.Empresas.Add(
                    new Empresa()
                    {
                        RazaoSocial = "Hospital Municipal Irmã Dulce",
                        NomeFantasia = "Irmã Dulce",
                        Atividade = ((pj == null) ? "" : pj.Descricao.Trim().ToUpper()),
                        CNPJ = "25503424000108",
                        Endereco = endereco1
                    });
                    context.SaveChanges();
                }
            }
            catch (Exception ex) { ex.Log(); }      
        }
    }
}
