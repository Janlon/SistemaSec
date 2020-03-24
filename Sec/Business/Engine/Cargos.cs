namespace Sec.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Generics.Extensoes;
    using Generics.Helpers.IBGE;
    using Generics.Helpers.IBGE.CBO;
    using Generics.Helpers.IBGE.CNAE;
    using Sec.Business.Models;

    public static partial class Engine
    {

        public static class Cargos
        {
            public static IEnumerable<Cargo> List()
            {
                return IbgeClient.CBO2002();
            }
            public static Cargo Select(int id)
            {
                if (id <= 0) id = 0;
                string sId = id
                    .ToString()
                    .PadLeft(6, '0');
                int value = 0;
                if (int.TryParse(sId, out value))
                {
                    return List()
                        .Where(p => p.id.Equals(value))
                        .FirstOrDefault();
                }
                return List().FirstOrDefault();
            }
            public static IEnumerable<Cargo> Filter(FiltroViewModel value)
            {
                return Filter(value.Descricao, value.Filtro, value.PageSize, value.StartAt);
            }
            public static IEnumerable<Cargo> Filter(string value, StringFilter filter = StringFilter.StartsWith, int pageSize = -1, int pageStart = -1)
            {
                value = ("" + value + "").Trim().ToUpper();
                IEnumerable<Cargo> ret = List();
                switch (filter)
                {
                    case StringFilter.Contains:
                        ret = ret
                            .Where(p => p.descricao.Trim().ToUpper()
                            .Contains(value));
                        break;

                    case StringFilter.Different:
                        ret = ret
                            .Where(p => !(p.descricao.Trim().ToUpper()
                            .Equals(value)));
                        break;

                    case StringFilter.EndsWith:
                        ret = ret
                            .Where(p => p.descricao.Trim().ToUpper()
                            .EndsWith(value));
                        break;

                    case StringFilter.Equals:
                        ret = ret
                            .Where(p => p.descricao.Trim().ToUpper()
                            .Equals(value));
                        break;

                    case StringFilter.IsEmpty:
                        ret = ret
                            .Where(p => p.descricao.Trim().ToUpper()
                            .Equals(""));
                        break;

                    case StringFilter.IsNull:
                        ret = ret
                            .Where(p => p.descricao == null);
                        break;

                    case StringFilter.IsNullOrEmpty:
                        ret = ret
                            .Where(p =>
                            string.IsNullOrEmpty(p.descricao) ||
                            string.IsNullOrWhiteSpace(p.descricao));
                        break;

                    case StringFilter.NotContains:
                        ret = ret
                            .Where(p => !(p.descricao.Trim().ToUpper()
                            .Contains(value)));
                        break;

                    case StringFilter.NotEndsWith:
                        ret = ret
                            .Where(p => !(p.descricao.Trim().ToUpper()
                            .EndsWith(value)));
                        break;

                    case StringFilter.NotIsEmpty:
                        ret = ret
                            .Where(p => !(p.descricao.Trim().ToUpper()
                            .Equals("")));
                        break;

                    case StringFilter.NotIsNull:
                        ret = ret
                            .Where(p => !(p.descricao == null));
                        break;

                    case StringFilter.NotIsNullOrEmpty:
                        ret = ret
                            .Where(p =>
                            !(string.IsNullOrEmpty(p.descricao) ||
                              string.IsNullOrWhiteSpace(p.descricao)));
                        break;

                    case StringFilter.NotStartsWith:
                        ret = ret
                            .Where(p => !(p.descricao.Trim().ToUpper()
                            .StartsWith(value)));
                        break;

                    case StringFilter.StartsWith:
                        ret = ret
                            .Where(p => p.descricao.Trim().ToUpper()
                            .StartsWith(value));
                        break;

                    default:
                        ret = ret
                            .Where(p => p.descricao.Trim().ToUpper()
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
                    using (IbgeClient c = new IbgeClient())
                    {
                        ret = c.CNAE()
                           .OrderBy(p => p.descricao);

                    }
                }
                catch (Exception ex) { ex.Log(); ret = new List<classe>(); }
                return ret;
            }

            internal static IEnumerable<Cargo> CBO()
            {
                return IbgeClient.CBO2002()
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
