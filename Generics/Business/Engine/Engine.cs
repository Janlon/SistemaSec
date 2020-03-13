using Microsoft.AspNet.Identity;
using Sec.Business.Models;
using Sec.Dados;
using Sec.Helpers.Errors;
using Sec.Helpers.IBGE.CNAE;
using Sec.IdentityGroup;
using Sec.Interfaces;
using Sec.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Generic.Business
{

    public class Engine
    {
        public class CrudError
        {
            public string PropertyName { get; set; }
            public string Message { get; set; }
        }

        public class CrudResult<T> where T : class
        {
            public CrudResult() { Model = null; }
            public T Model { get; internal set; }
            public List<CrudError> Errors { get; internal set; } = new List<CrudError>();
            public int Affected { get; internal set; } = 0;

            internal CrudResult(T value) { Model = value; }
            internal void AddError(string propertyName, string message) { Errors.Add(new CrudError() { Message = message, PropertyName = propertyName }); }
            internal bool IsValid
            {
                get
                {
                    return Errors.Count == 0;
                }
            }
            internal void SetAffected(int value) { Affected = value; }
        }

        public static class Users
        {
            public static async Task<CrudResult<RegisterViewModel>> Register(RegisterViewModel model)
            {
                CrudResult<RegisterViewModel> ret = new CrudResult<RegisterViewModel>(model);
                try
                {
                    // Para manter a conta de acesso.
                    ApplicationUser u00 = null;
                    // Primeiro passo: Criar a conta de acesso.
                    try
                    {
                        string senha = model.Password;
                        using (ApplicationManager am = new ApplicationManager())
                        {
                            u00 = await am.UM.FindAsync(model.Name, senha);
                            if (u00 != null)
                                ret.AddError("Usuário", "A conta não pode ser criada com os dados informados. Por favor, altere os dados e tente novamente.");
                            u00 = await am.UM.FindByEmailAsync(model.Email);
                            if (u00 != null)
                                ret.AddError("Usuário", "O email informado já está em uso. Por favor, altere os dados e tente novamente.");
                            u00 = await am.UM.FindByNameAsync(model.Name);
                            if (u00 != null)
                                ret.AddError("Usuário", "A conta informada já está em uso. Por favor, altere os dados e tente novamente.");

                            if (am.Db.Pessoas.Where(p => p.Apelido.Equals(model.NickName)).Count() > 0)
                                ret.AddError("Usuário", "A conta informada já está em uso. Por favor, altere os dados e tente novamente.");

                            if (am.Db.Pessoas.Where(p => p.Email.Equals(model.Email)).Count() > 0)
                                ret.AddError("Usuário", "O email informado já está em uso. Por favor, altere os dados e tente novamente.");

                            if (ret.IsValid)
                            {
                                IdentityResult resp = await am.UM.CreateAsync(new ApplicationUser() { Email = model.Email, UserName = model.Name }, model.Password);
                                if (resp.Succeeded == false)
                                    foreach (string erro in resp.Errors)
                                        ret.AddError("Usuário", erro);
                            }
                            else
                                u00 = await am.UM.FindAsync(model.Name, senha);
                        }
                    }
                    catch (Exception ex) { ex.Log(); }

                    // Segundo passo: Criar a pessoa.
                    try
                    {
                        if (u00 != null)
                        {
                            Pessoa p = new Pessoa() { Apelido = model.NickName.Trim().ToUpper(), Ativo = true, Cadastro = DateTime.Now, Email = model.Email.Trim().ToLower(), Nascimento = model.Nascimento, Nome = model.Name, PessoaFisica = true };
                            using (Dal.DB db = new Dal.DB())
                            {
                                db.Pessoas.Add(p);
                                var vr = db.Entry(p).GetValidationResult();
                                if (!vr.IsValid)
                                    foreach (var ve in vr.ValidationErrors)
                                        ret.AddError(ve.PropertyName, ve.ErrorMessage);
                                else
                                    ret.SetAffected(db.SaveChanges());
                            }
                        }
                    }
                    catch (Exception ex) { ret.AddError("Usuário", ex.Message); ex.Log(); }
                }
                catch (Exception ex) { ret.AddError("Usuário", ex.Message); ex.Log(); }
                return ret;
            }
        }

        public static class Geo
        {
            public static Models.Endereco EnderecoByCEP(string cep)
            {
                Models.Endereco ret = new Models.Endereco();
                using (Helpers.IBGE.IbgeClient db = new Helpers.IBGE.IbgeClient())
                    ret = new Models.Endereco(db.EnderecoDoCEP(cep));
                return ret;
            }

            /// <summary>
            /// Retorna a lista de unidades federativas do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Uf"/>.</returns>
            public static IEnumerable<Uf> Ufs()
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).Ufs().Select(p => new Models.Uf(p));
            }

            /// <summary>
            /// Retorna os dados de uma unidade federativa.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Uf"/>.</returns>
            public static Uf Uf(int id)
            {
                return new Uf((new Sec.Helpers.IBGE.IbgeClient()).Ufs(id));
            }

            /// <summary>
            /// Retorna a lista de unidades federativas de uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Uf"/>.</returns>
            public static IEnumerable<Uf> UfByReg(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).UfsPorRegiao(id).Select(p => new Models.Uf(p));
            }

            /// <summary>
            /// Retorna a lista completa de distritos do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static IEnumerable<Distrito> Distritos()
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).Distritos().Select(p => new Models.Distrito(p));
            }

            /// <summary>
            /// Retorna os dados de um distrito do Brasil.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static Distrito Distrito(int id)
            {
                return new Distrito((new Sec.Helpers.IBGE.IbgeClient()).DistritoPorId(id));
            }

            /// <summary>
            /// Retorna a lista completa de distritos do Brasil em uma unidade federativa.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static IEnumerable<Distrito> DistritosPorUf(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).DistritoPorUf(id).Select(p => new Models.Distrito(p)); ;
            }

            /// <summary>
            /// Retorna a lista completa de distritos do Brasil em uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static IEnumerable<Distrito> DistritosPorRegiao(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).DistritosPorRegiao(id).Select(p => new Models.Distrito(p)); ;
            }

            /// <summary>
            /// Retorna a lista completa de distritos do Brasil em uma mesorregião.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static IEnumerable<Distrito> DistritosPorMesorregiao(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).DistritosPorMesorregiao(id).Select(p => new Models.Distrito(p)); ;
            }

            /// <summary>
            /// Retorna a lista completa de distritos do Brasil em uma microrregião.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static IEnumerable<Distrito> DistritosPorMicrorregiao(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).DistritosPorMicrorregiao(id).Select(p => new Models.Distrito(p)); ;
            }

            /// <summary>
            /// Retorna a lista completa de distritos do Brasil em um município.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static IEnumerable<Distrito> DistritosPorMunicipio(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).DistritosPorMunicipio(id).Select(p => new Models.Distrito(p)); ;
            }

            /// <summary>
            /// Retorna a lista completa de mesorregiões do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Mesorregiao"/>.</returns>
            public static IEnumerable<Mesorregiao> Mesorregioes()
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).Mesorregioes().Select(p => new Models.Mesorregiao(p));
            }

            /// <summary>
            /// Retorna a lista completa de mesorregiões do Brasil em uma unidade federativa.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Mesorregiao"/>.</returns>
            public static IEnumerable<Mesorregiao> MesorregioesPorUf(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).MesorregioesPorUf(id).Select(p => new Models.Mesorregiao(p)); ;
            }

            /// <summary>
            /// Retorna a lista completa de mesorregiões do Brasil em uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Mesorregiao"/>.</returns>
            public static IEnumerable<Mesorregiao> MesorregioesPorRegiao(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).MesorregioesPorRegiao(id).Select(p => new Models.Mesorregiao(p)); ;
            }

            /// <summary>
            /// Retorna os dados de uma mesorregião do Brasil.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Mesorregiao"/>.</returns>
            public static Mesorregiao Mesorregiao(int id)
            {
                return new Mesorregiao((new Sec.Helpers.IBGE.IbgeClient()).MesorregioesPorId(id));
            }

            /// <summary>
            /// Retorna a lista completa de Microrregiões do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
            public static IEnumerable<Microrregiao> Microrregioes()
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).Microrregioes().Select(p => new Models.Microrregiao(p));
            }

            /// <summary>
            /// Retorna a lista completa de Microrregiões do Brasil em uma unidade federativa.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
            public static IEnumerable<Microrregiao> MicrorregioesPorUf(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).MicrorregioesPorUf(id).Select(p => new Models.Microrregiao(p));
            }

            /// <summary>
            /// Retorna a lista completa de Microrregiões do Brasil em uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
            public static IEnumerable<Microrregiao> MicrorregioesPorRegiao(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).MicrorregioesPorRegiao(id).Select(p => new Models.Microrregiao(p));
            }

            /// <summary>
            /// Retorna a lista completa de Microrregiões do Brasil em uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
            public static IEnumerable<Microrregiao> MicrorregioesPorMesorregiao(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).MicrorregioesPorMesorregiao(id).Select(p => new Models.Microrregiao(p));
            }

            /// <summary>
            /// Retorna os dados de uma Microrregião do Brasil.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
            public static Microrregiao Microrregiao(int id)
            {
                return new Microrregiao((new Sec.Helpers.IBGE.IbgeClient()).Microrregioes(id));
            }

            /// <summary>
            /// Retorna a lista completa de Municípios do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Municipio"/>.</returns>
            public static IEnumerable<Municipio> Municipios()
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).Municipios().Select(p => new Models.Municipio(p));
            }

            /// <summary>
            /// Retorna a lista completa de Municípios do Brasil em uma unidade federativa.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Municipio"/>.</returns>
            public static IEnumerable<Municipio> MunicipiosPorUf(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).GetMunicipiosDaUf(id).Select(p => new Models.Municipio(p)); ;
            }

            /// <summary>
            /// Retorna a lista completa de Municípios do Brasil em uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Municipio"/>.</returns>
            public static IEnumerable<Municipio> MunicipiosPorRegiao(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).MunicipiosPorRegiao(id).Select(p => new Models.Municipio(p)); ;
            }

            /// <summary>
            /// Retorna a lista completa de Municípios do Brasil em uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Municipio"/>.</returns>
            public static IEnumerable<Municipio> MunicipiosPorMesorregiao(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).MunicipiosPorMesorregiao(id).Select(p => new Models.Municipio(p)); ;
            }

            /// <summary>
            /// Retorna os dados de uma Microrregião do Brasil.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Municipio"/>.</returns>
            public static Municipio Municipio(int id)
            {
                return new Municipio((new Sec.Helpers.IBGE.IbgeClient()).MunicipiosPorId(id));
            }

            /// <summary>
            /// Retorna a lista completa de regiões do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Regiao"/>.</returns>
            public static IEnumerable<Regiao> Regioes()
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).Regioes().Select(p => new Models.Regiao(p));
            }

            /// <summary>
            /// Retorna a lista completa de regiões do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Regiao"/>.</returns>
            public static Regiao Regiao(int id)
            {
                return new Regiao((new Sec.Helpers.IBGE.IbgeClient()).Regioes(id));
            }

            /// <summary>
            /// Retorna a lista completa de subdistritos do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static IEnumerable<Subdistrito> Subdistritos()
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).Subdistritos().Select(p => new Models.Subdistrito(p));
            }

            /// <summary>
            /// Retorna os dados de um subdistrito por sua respectiva identificação.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static Subdistrito Subdistrito(int id)
            {
                return new Subdistrito((new Sec.Helpers.IBGE.IbgeClient()).SubdistritosPorId(id));
            }

            /// <summary>
            /// Retorna a lista de subdistritos em uma unidade federativa.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static List<Subdistrito> SubdistritoPorUf(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).SubdistritosPorUf(id).Select(p => new Models.Subdistrito(p)).ToList();
            }
            /// <summary>
            /// Retorna a lista de subdistritos em uma região.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static List<Subdistrito> SubdistritoPorRegiao(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).SubdistritosPorRegiao(id).Select(p => new Models.Subdistrito(p)).ToList();
            }
            /// <summary>
            /// Retorna a lista de subdistritos em uma mesorregião.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static List<Subdistrito> SubdistritoPorMesorregiao(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).SubdistritosPorMesorregiao(id).Select(p => new Models.Subdistrito(p)).ToList();
            }
            /// <summary>
            /// Retorna a lista de subdistritos em uma microrregião.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static List<Subdistrito> SubdistritoPorMicrorregiao(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).SubdistritosPorMicrorregiao(id).Select(p => new Models.Subdistrito(p)).ToList();
            }
            /// <summary>
            /// Retorna a lista de subdistritos em um distrito.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static List<Subdistrito> SubdistritoPorDistrito(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).SubdistritosPorDistrito(id).Select(p => new Models.Subdistrito(p)).ToList();
            }
            /// <summary>
            /// Retorna a lista de subdistritos em um municipio.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static List<Subdistrito> SubdistritoPorMunicipio(int id)
            {
                return (new Sec.Helpers.IBGE.IbgeClient()).SubdistritosPorMunicipio(id).Select(p => new Models.Subdistrito(p)).ToList();
            }

        }

        public static class Cargo
        { 
            public static IEnumerable<Models.Cargo> List()
            {
                List<Models.Cargo> ret = new List<Models.Cargo>();
                ret.AddRange(Helpers
                    .IBGE
                    .IbgeClient
                    .CBO2002()
                    .Select(p => new Models.Cargo(p.id, p.descricao)));
                return ret;
            }
            public static Models.Cargo Select(int id)
            {
                if (id <= 0) id = 0;
                string sId = id
                    .ToString()
                    .PadLeft(6, '0');
                int value = 0;
                if (int.TryParse(sId, out value))
                {
                    return List()
                        .Where(p => p.Id.Equals(value))
                        .FirstOrDefault();
                }
                return List().FirstOrDefault();
            }
            public static IEnumerable<Models.Cargo> Filter(FiltroViewModel value)
            {
                return Filter(value.Descricao, value.Filtro, value.PageSize, value.StartAt);
            }
            public static IEnumerable<Models.Cargo> Filter(string value, StringFilter filter = StringFilter.StartsWith, int pageSize = -1, int pageStart = -1)
            {
                value = ("" + value + "").Trim().ToUpper();
                IEnumerable<Models.Cargo> ret = List();
                switch (filter)
                {
                    case StringFilter.Contains:
                        ret = ret
                            .Where(p => p.Descricao.Trim().ToUpper()
                            .Contains(value));
                        break;

                    case StringFilter.Different:
                        ret = ret
                            .Where(p => !(p.Descricao.Trim().ToUpper()
                            .Equals(value)));
                        break;

                    case StringFilter.EndsWith:
                        ret = ret
                            .Where(p => p.Descricao.Trim().ToUpper()
                            .EndsWith(value));
                        break;

                    case StringFilter.Equals:
                        ret = ret
                            .Where(p => p.Descricao.Trim().ToUpper()
                            .Equals(value));
                        break;

                    case StringFilter.IsEmpty:
                        ret = ret
                            .Where(p => p.Descricao.Trim().ToUpper()
                            .Equals(""));
                        break;

                    case StringFilter.IsNull:
                        ret = ret
                            .Where(p => p.Descricao == null);
                        break;

                    case StringFilter.IsNullOrEmpty:
                        ret = ret
                            .Where(p =>
                            string.IsNullOrEmpty(p.Descricao) ||
                            string.IsNullOrWhiteSpace(p.Descricao));
                        break;

                    case StringFilter.NotContains:
                        ret = ret
                            .Where(p => !(p.Descricao.Trim().ToUpper()
                            .Contains(value)));
                        break;

                    case StringFilter.NotEndsWith:
                        ret = ret
                            .Where(p => !(p.Descricao.Trim().ToUpper()
                            .EndsWith(value)));
                        break;

                    case StringFilter.NotIsEmpty:
                        ret = ret
                            .Where(p => !(p.Descricao.Trim().ToUpper()
                            .Equals("")));
                        break;

                    case StringFilter.NotIsNull:
                        ret = ret
                            .Where(p => !(p.Descricao == null));
                        break;

                    case StringFilter.NotIsNullOrEmpty:
                        ret = ret
                            .Where(p =>
                            !(string.IsNullOrEmpty(p.Descricao) ||
                              string.IsNullOrWhiteSpace(p.Descricao)));
                        break;

                    case StringFilter.NotStartsWith:
                        ret = ret
                            .Where(p => !(p.Descricao.Trim().ToUpper()
                            .StartsWith(value)));
                        break;

                    case StringFilter.StartsWith:
                        ret = ret
                            .Where(p => p.Descricao.Trim().ToUpper()
                            .StartsWith(value));
                        break;

                    default:
                        ret = ret
                            .Where(p => p.Descricao.Trim().ToUpper()
                            .Equals(value));
                        break;
                }
                if ((pageStart > -1) && (pageStart <= ret.Count()))
                    ret = ret.Skip(pageStart);
                if ((pageSize > -1) && (pageSize <= ret.Count()))
                    ret = ret.Take(pageSize);
                return ret;
            }

            internal static IEnumerable<classe> CNAE()
            {
                IEnumerable<classe> ret = new List<classe>();
                try
                {
                    using (Helpers.IBGE.IbgeClient c = new Helpers.IBGE.IbgeClient())
                    {
                        ret = c.CNAE()
                           .OrderBy(p => p.descricao);

                    }
                }
                catch (Exception ex) { ex.Log(); ret = new List<classe>(); }
                return ret;
            }

            internal static IEnumerable<Helpers.IBGE.CBO.Cargo> CBO()
            {
                return Helpers.IBGE.IbgeClient.CBO2002()
                   .OrderBy(p => p.descricao);
            }

            public static IEnumerable<Atividade> Atividades()
            {
                List<Atividade> atividades = new List<Atividade>();
                var cnaes = CNAE();
                foreach (var item in cnaes)
                    atividades.Add(new Atividade() { Descricao = item.descricao, Id = item.id, PessoaFisica = false });
                var cbos = CBO();
                foreach (var item in cbos)
                    atividades.Add(new Atividade() { Descricao = item.descricao, Id = item.id, PessoaFisica = true });
                return atividades
                    .OrderBy(p => p.PessoaFisica)
                    .ThenBy(p => p.Descricao);
            }
            public static IEnumerable<Atividade> Atividades(FiltroViewModel value)
            {
                return Atividades(value.Descricao, value.Filtro, value.StartAt, value.PageSize);
            }
            public static IEnumerable<Atividade> Atividades(string value, StringFilter filter, int pageStart = -1, int pageSize = -1)
            {
                IEnumerable<Atividade> ret = Atividades();
                switch (filter)
                {
                    case StringFilter.Contains:
                        ret = ret
                            .Where(p => p.Descricao.Trim().ToUpper()
                            .Contains(value));
                        break;

                    case StringFilter.Different:
                        ret = ret
                            .Where(p => !(p.Descricao.Trim().ToUpper()
                            .Equals(value)));
                        break;

                    case StringFilter.EndsWith:
                        ret = ret
                            .Where(p => p.Descricao.Trim().ToUpper()
                            .EndsWith(value));
                        break;

                    case StringFilter.Equals:
                        ret = ret
                            .Where(p => p.Descricao.Trim().ToUpper()
                            .Equals(value));
                        break;

                    case StringFilter.IsEmpty:
                        ret = ret
                            .Where(p => p.Descricao.Trim().ToUpper()
                            .Equals(""));
                        break;

                    case StringFilter.IsNull:
                        ret = ret
                            .Where(p => p.Descricao == null);
                        break;

                    case StringFilter.IsNullOrEmpty:
                        ret = ret
                            .Where(p =>
                            string.IsNullOrEmpty(p.Descricao) ||
                            string.IsNullOrWhiteSpace(p.Descricao));
                        break;

                    case StringFilter.NotContains:
                        ret = ret
                            .Where(p => !(p.Descricao.Trim().ToUpper()
                            .Contains(value)));
                        break;

                    case StringFilter.NotEndsWith:
                        ret = ret
                            .Where(p => !(p.Descricao.Trim().ToUpper()
                            .EndsWith(value)));
                        break;

                    case StringFilter.NotIsEmpty:
                        ret = ret
                            .Where(p => !(p.Descricao.Trim().ToUpper()
                            .Equals("")));
                        break;

                    case StringFilter.NotIsNull:
                        ret = ret
                            .Where(p => !(p.Descricao == null));
                        break;

                    case StringFilter.NotIsNullOrEmpty:
                        ret = ret
                            .Where(p =>
                            !(string.IsNullOrEmpty(p.Descricao) ||
                              string.IsNullOrWhiteSpace(p.Descricao)));
                        break;

                    case StringFilter.NotStartsWith:
                        ret = ret
                            .Where(p => !(p.Descricao.Trim().ToUpper()
                            .StartsWith(value)));
                        break;

                    case StringFilter.StartsWith:
                        ret = ret
                            .Where(p => p.Descricao.Trim().ToUpper()
                            .StartsWith(value));
                        break;

                    default:
                        ret = ret
                            .Where(p => p.Descricao.Trim().ToUpper()
                            .Equals(value));
                        break;
                }
                if ((pageStart > -1) && (pageStart <= ret.Count()))
                    ret = ret.Skip(pageStart);
                if ((pageSize > -1) && (pageSize <= ret.Count()))
                    ret = ret.Take(pageSize);
                return ret;
            }
        }
    }
}

