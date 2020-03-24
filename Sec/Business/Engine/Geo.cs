namespace Sec.Business
{
    using System.Collections.Generic;
    using Generics.Helpers.IBGE.Geo;
    using Generics.Helpers.IBGE;

    public partial class Engine
    {
        public static class Geo
        {
            public static Endereco EnderecoByCEP(string cep)
            {
                Generics.Helpers.IBGE.Geo.Endereco ret = new Generics.Helpers.IBGE.Geo.Endereco();
                using (IbgeClient db = new IbgeClient())
                    ret = db.EnderecoDoCEP(cep);
                return ret;
            }

            /// <summary>
            /// Retorna a lista de unidades federativas do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Uf"/>.</returns>
            public static IEnumerable<Uf> Ufs()
            {
                return (new IbgeClient()).Ufs();
            }

            /// <summary>
            /// Retorna os dados de uma unidade federativa.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Uf"/>.</returns>
            public static Uf Uf(int id)
            {
                return (new IbgeClient()).Ufs(id);
            }

            /// <summary>
            /// Retorna a lista de unidades federativas de uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Uf"/>.</returns>
            public static IEnumerable<Uf> UfByReg(int id)
            {
                return (new IbgeClient()).UfsPorRegiao(id);
            }

            /// <summary>
            /// Retorna a lista completa de distritos do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static IEnumerable<Distrito> Distritos()
            {
                return (new IbgeClient()).Distritos();
            }

            /// <summary>
            /// Retorna os dados de um distrito do Brasil.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static Distrito Distrito(int id)
            {
                return (new IbgeClient()).DistritoPorId(id);
            }

            /// <summary>
            /// Retorna a lista completa de distritos do Brasil em uma unidade federativa.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static IEnumerable<Distrito> DistritosPorUf(int id)
            {
                return (new IbgeClient()).DistritoPorUf(id);
            }

            /// <summary>
            /// Retorna a lista completa de distritos do Brasil em uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static IEnumerable<Distrito> DistritosPorRegiao(int id)
            {
                return (new IbgeClient()).DistritosPorRegiao(id);
            }

            /// <summary>
            /// Retorna a lista completa de distritos do Brasil em uma mesorregião.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static IEnumerable<Distrito> DistritosPorMesorregiao(int id)
            {
                return (new IbgeClient()).DistritosPorMesorregiao(id);
            }

            /// <summary>
            /// Retorna a lista completa de distritos do Brasil em uma microrregião.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static IEnumerable<Distrito> DistritosPorMicrorregiao(int id)
            {
                return (new IbgeClient()).DistritosPorMicrorregiao(id);
            }

            /// <summary>
            /// Retorna a lista completa de distritos do Brasil em um município.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Distrito"/>.</returns>
            public static IEnumerable<Distrito> DistritosPorMunicipio(int id)
            {
                return (new IbgeClient()).DistritosPorMunicipio(id);
            }

            /// <summary>
            /// Retorna a lista completa de mesorregiões do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Mesorregiao"/>.</returns>
            public static IEnumerable<Mesorregiao> Mesorregioes()
            {
                return (new IbgeClient()).Mesorregioes();
            }

            /// <summary>
            /// Retorna a lista completa de mesorregiões do Brasil em uma unidade federativa.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Mesorregiao"/>.</returns>
            public static IEnumerable<Mesorregiao> MesorregioesPorUf(int id)
            {
                return (new IbgeClient()).MesorregioesPorUf(id);
            }

            /// <summary>
            /// Retorna a lista completa de mesorregiões do Brasil em uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Mesorregiao"/>.</returns>
            public static IEnumerable<Mesorregiao> MesorregioesPorRegiao(int id)
            {
                return (new IbgeClient()).MesorregioesPorRegiao(id);
            }

            /// <summary>
            /// Retorna os dados de uma mesorregião do Brasil.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Mesorregiao"/>.</returns>
            public static Mesorregiao Mesorregiao(int id)
            {
                return (new IbgeClient()).MesorregioesPorId(id);
            }

            /// <summary>
            /// Retorna a lista completa de Microrregiões do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
            public static IEnumerable<Microrregiao> Microrregioes()
            {
                return (new IbgeClient()).Microrregioes();
            }

            /// <summary>
            /// Retorna a lista completa de Microrregiões do Brasil em uma unidade federativa.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
            public static IEnumerable<Microrregiao> MicrorregioesPorUf(int id)
            {
                return (new IbgeClient()).MicrorregioesPorUf(id);
            }

            /// <summary>
            /// Retorna a lista completa de Microrregiões do Brasil em uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
            public static IEnumerable<Microrregiao> MicrorregioesPorRegiao(int id)
            {
                return (new IbgeClient()).MicrorregioesPorRegiao(id);
            }

            /// <summary>
            /// Retorna a lista completa de Microrregiões do Brasil em uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
            public static IEnumerable<Microrregiao> MicrorregioesPorMesorregiao(int id)
            {
                return (new IbgeClient()).MicrorregioesPorMesorregiao(id);
            }

            /// <summary>
            /// Retorna os dados de uma Microrregião do Brasil.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
            public static Microrregiao Microrregiao(int id)
            {
                return (new IbgeClient()).Microrregioes(id);
            }

            /// <summary>
            /// Retorna a lista completa de Municípios do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Municipio"/>.</returns>
            public static IEnumerable<Municipio> Municipios()
            {
                return (new IbgeClient()).Municipios();
            }

            /// <summary>
            /// Retorna a lista completa de Municípios do Brasil em uma unidade federativa.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Municipio"/>.</returns>
            public static IEnumerable<Municipio> MunicipiosPorUf(int id)
            {
                return (new IbgeClient()).GetMunicipiosDaUf(id);
            }

            /// <summary>
            /// Retorna a lista completa de Municípios do Brasil em uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Municipio"/>.</returns>
            public static IEnumerable<Municipio> MunicipiosPorRegiao(int id)
            {
                return (new IbgeClient()).MunicipiosPorRegiao(id);
            }

            /// <summary>
            /// Retorna a lista completa de Municípios do Brasil em uma região.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Municipio"/>.</returns>
            public static IEnumerable<Municipio> MunicipiosPorMesorregiao(int id)
            {
                return (new IbgeClient()).MunicipiosPorMesorregiao(id);
            }

            /// <summary>
            /// Retorna os dados de uma Microrregião do Brasil.
            /// </summary>
            /// <param name="id">Identificador.</param>
            /// <returns>Lista de <see cref="Municipio"/>.</returns>
            public static Municipio Municipio(int id)
            {
                return (new IbgeClient()).MunicipiosPorId(id);
            }

            /// <summary>
            /// Retorna a lista completa de regiões do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Regiao"/>.</returns>
            public static IEnumerable<Regiao> Regioes()
            {
                return (new IbgeClient()).Regioes();
            }

            /// <summary>
            /// Retorna a lista completa de regiões do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Regiao"/>.</returns>
            public static Regiao Regiao(int id)
            {
                return (new IbgeClient()).Regioes(id);
            }

            /// <summary>
            /// Retorna a lista completa de subdistritos do Brasil.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static IEnumerable<Subdistrito> Subdistritos()
            {
                return (new IbgeClient()).Subdistritos();
            }

            /// <summary>
            /// Retorna os dados de um subdistrito por sua respectiva identificação.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static Subdistrito Subdistrito(int id)
            {
                return (new IbgeClient()).SubdistritosPorId(id);
            }

            /// <summary>
            /// Retorna a lista de subdistritos em uma unidade federativa.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static List<Subdistrito> SubdistritoPorUf(int id)
            {
                return (new IbgeClient()).SubdistritosPorUf(id);
            }
            /// <summary>
            /// Retorna a lista de subdistritos em uma região.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static List<Subdistrito> SubdistritoPorRegiao(int id)
            {
                return (new IbgeClient()).SubdistritosPorRegiao(id);
            }
            /// <summary>
            /// Retorna a lista de subdistritos em uma mesorregião.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static List<Subdistrito> SubdistritoPorMesorregiao(int id)
            {
                return (new IbgeClient()).SubdistritosPorMesorregiao(id);
            }
            /// <summary>
            /// Retorna a lista de subdistritos em uma microrregião.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static List<Subdistrito> SubdistritoPorMicrorregiao(int id)
            {
                return (new IbgeClient()).SubdistritosPorMicrorregiao(id);
            }
            /// <summary>
            /// Retorna a lista de subdistritos em um distrito.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static List<Subdistrito> SubdistritoPorDistrito(int id)
            {
                return (new IbgeClient()).SubdistritosPorDistrito(id);
            }
            /// <summary>
            /// Retorna a lista de subdistritos em um municipio.
            /// </summary>
            /// <returns>Lista de <see cref="Sec.Helpers.IBGE.Geo.Subdistrito"/>.</returns>
            public static List<Subdistrito> SubdistritoPorMunicipio(int id)
            {
                return (new IbgeClient()).SubdistritosPorMunicipio(id);
            }

        }
    }
}
