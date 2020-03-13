using System.Collections.Generic;
using System.Web.Http;

using Generics.Helpers.IBGE.Geo;
using Sec.Business;         // Serviços
using Sec.Business.Models;  // Modelos de transporte

namespace Swagger.Controllers
{

    [RoutePrefix("api/geo")]
    public class GeoController : ApiController
    {

        /// <summary>
        /// Retorna, se localizar, o endereço correspondente ao CEP indicado.
        /// </summary>
        /// <param name="cep"><see cref="CEP"/></param>
        /// <returns>Objeto do tipo <see cref="Endereco"/>.</returns>
        [Route("EnderecoDoCep")]
        public Endereco Post([FromBody] CEP cep)
        {
            Endereco ret = Engine.Geo.EnderecoByCEP(cep.Codigo);
            if (ret == null) ret = new Endereco();
            return ret;
        }

        /// <summary>
        /// Retorna a lista de unidades federativas do Brasil.
        /// </summary>
        /// <returns>Lista de <see cref="Uf"/>.</returns>
        [Route("UFs")]
        [HttpGet()]
        public IEnumerable<Uf> Ufs()
        {
            return Engine.Geo.Ufs();
        }

        /// <summary>
        /// Retorna os dados de uma unidade federativa.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Uf"/>.</returns>
        [Route("UF")]
        [HttpGet()]
        public Uf Uf(int id)
        {
            return Engine.Geo.Uf(id);
        }

        /// <summary>
        /// Retorna a lista de unidades federativas de uma região.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Uf"/>.</returns>
        [Route("UFsDaRegiao")]
        [HttpGet()]
        public IEnumerable<Uf> UfByReg(int id)
        {
            return Engine.Geo.UfByReg(id);
        }

        /// <summary>
        /// Retorna a lista completa de distritos do Brasil.
        /// </summary>
        /// <returns>Lista de <see cref="Distrito"/>.</returns>
        [Route("Distritos")]
        [HttpGet()]
        public IEnumerable<Distrito> Distritos()
        {
            return Engine.Geo.Distritos();
        }

        /// <summary>
        /// Retorna os dados de um distrito do Brasil.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Distrito"/>.</returns>
        [Route("Distrito")]
        [HttpGet()]
        public Distrito Distrito(int id)
        {
            return Engine.Geo.Distrito(id);
        }

        /// <summary>
        /// Retorna a lista completa de distritos do Brasil em uma unidade federativa.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Distrito"/>.</returns>
        [Route("DistritosPorUf")]
        [HttpGet()]
        public IEnumerable<Distrito> DistritosPorUf(int id)
        {
            return Engine.Geo.DistritosPorUf(id);
        }

        /// <summary>
        /// Retorna a lista completa de distritos do Brasil em uma região.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Distrito"/>.</returns>
        [Route("DistritosPorRegiao")]
        [HttpGet()]
        public IEnumerable<Distrito> DistritosPorRegiao(int id)
        {
            return Engine.Geo.DistritosPorRegiao(id);
        }

        /// <summary>
        /// Retorna a lista completa de distritos do Brasil em uma mesorregião.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Distrito"/>.</returns>
        [Route("DistritosPorMesorregiao")]
        [HttpGet()]
        public IEnumerable<Distrito> DistritosPorMesorregiao(int id)
        {
            return Engine.Geo.DistritosPorMesorregiao(id);
        }

        /// <summary>
        /// Retorna a lista completa de distritos do Brasil em uma microrregião.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Distrito"/>.</returns>
        [Route("DistritosPorMicrorregiao")]
        [HttpGet()]
        public IEnumerable<Distrito> DistritosPorMicrorregiao(int id)
        {
            return Engine.Geo.DistritosPorMicrorregiao(id);
        }

        /// <summary>
        /// Retorna a lista completa de distritos do Brasil em um município.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Distrito"/>.</returns>
        [Route("DistritosPorMunicipio")]
        [HttpGet()]
        public IEnumerable<Distrito> DistritosPorMunicipio(int id)
        {
            return Engine.Geo.DistritosPorMunicipio(id);
        }

        /// <summary>
        /// Retorna a lista completa de mesorregiões do Brasil.
        /// </summary>
        /// <returns>Lista de <see cref="Mesorregiao"/>.</returns>
        [Route("Mesorregioes")]
        [HttpGet()]
        public IEnumerable<Mesorregiao> Mesorregioes()
        {
            return Engine.Geo.Mesorregioes();
        }

        /// <summary>
        /// Retorna a lista completa de mesorregiões do Brasil em uma unidade federativa.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Mesorregiao"/>.</returns>
        [Route("MesorregioesPorUf")]
        [HttpGet()]
        public IEnumerable<Mesorregiao> MesorregioesPorUf(int id)
        {
            return Engine.Geo.MesorregioesPorUf(id);
        }

        /// <summary>
        /// Retorna a lista completa de mesorregiões do Brasil em uma região.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Mesorregiao"/>.</returns>
        [Route("MesorregioesPorRegiao")]
        [HttpGet()]
        public IEnumerable<Mesorregiao> MesorregioesPorRegiao(int id)
        {
            return Engine.Geo.MesorregioesPorRegiao(id);
        }

        /// <summary>
        /// Retorna os dados de uma mesorregião do Brasil.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Mesorregiao"/>.</returns>
        [Route("Mesorregiao")]
        [HttpGet()]
        public Mesorregiao Mesorregiao(int id)
        {
            return Engine.Geo.Mesorregiao(id);
        }

        /// <summary>
        /// Retorna a lista completa de Microrregiões do Brasil.
        /// </summary>
        /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
        [Route("Microrregioes")]
        [HttpGet()]
        public IEnumerable<Microrregiao> Microrregioes()
        {
            return Engine.Geo.Microrregioes();
        }

        /// <summary>
        /// Retorna a lista completa de Microrregiões do Brasil em uma unidade federativa.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
        [Route("MicrorregioesPorUf")]
        [HttpGet()]
        public IEnumerable<Microrregiao> MicrorregioesPorUf(int id)
        {
            return Engine.Geo.MicrorregioesPorUf(id);
        }

        /// <summary>
        /// Retorna a lista completa de Microrregiões do Brasil em uma região.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
        [Route("MicrorregioesPorRegiao")]
        [HttpGet()]
        public IEnumerable<Microrregiao> MicrorregioesPorRegiao(int id)
        {
            return Engine.Geo.MicrorregioesPorRegiao(id);
        }

        /// <summary>
        /// Retorna a lista completa de Microrregiões do Brasil em uma região.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
        [Route("MicrorregioesPorMesorregiao")]
        [HttpGet()]
        public IEnumerable<Microrregiao> MicrorregioesPorMesorregiao(int id)
        {
            return Engine.Geo.MicrorregioesPorMesorregiao(id);
        }

        /// <summary>
        /// Retorna os dados de uma Microrregião do Brasil.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Microrregiao"/>.</returns>
        [Route("Microrregiao")]
        [HttpGet()]
        public Microrregiao Microrregiao(int id)
        {
            return Engine.Geo.Microrregiao(id);
        }

        /// <summary>
        /// Retorna a lista completa de Municípios do Brasil.
        /// </summary>
        /// <returns>Lista de <see cref="Municipio"/>.</returns>
        [Route("Municipios")]
        [HttpGet()]
        public IEnumerable<Municipio> Municipios()
        {
            return Engine.Geo.Municipios();
        }

        /// <summary>
        /// Retorna a lista completa de Municípios do Brasil em uma unidade federativa.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Municipio"/>.</returns>
        [Route("MunicipiosPorUf")]
        [HttpGet()]
        public IEnumerable<Municipio> MunicipiosPorUf(int id)
        {
            return Engine.Geo.MunicipiosPorUf(id);
        }

        /// <summary>
        /// Retorna a lista completa de Municípios do Brasil em uma região.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Municipio"/>.</returns>
        [Route("MunicipiosPorRegiao")]
        [HttpGet()]
        public IEnumerable<Municipio> MunicipiosPorRegiao(int id)
        {
            return Engine.Geo.MunicipiosPorRegiao(id);
        }

        /// <summary>
        /// Retorna a lista completa de Municípios do Brasil em uma região.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Municipio"/>.</returns>
        [Route("MunicipiosPorMesorregiao")]
        [HttpGet()]
        public IEnumerable<Municipio> MunicipiosPorMesorregiao(int id)
        {
            return Engine.Geo.MunicipiosPorMesorregiao(id);
        }

        /// <summary>
        /// Retorna os dados de uma Microrregião do Brasil.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns>Lista de <see cref="Municipio"/>.</returns>
        [Route("Municipio")]
        [HttpGet()]
        public Municipio Municipio(int id)
        {
            return Engine.Geo.Municipio(id);
        }

        /// <summary>
        /// Retorna a lista completa de regiões do Brasil.
        /// </summary>
        /// <returns>Lista de <see cref="Regiao"/>.</returns>
        [Route("Regioes")]
        [HttpGet()]
        public IEnumerable<Regiao> Regioes()
        {
            return Engine.Geo.Regioes();
        }

        /// <summary>
        /// Retorna a lista completa de regiões do Brasil.
        /// </summary>
        /// <returns>Lista de <see cref="Regiao"/>.</returns>
        [Route("Regiao")]
        [HttpGet()]
        public Regiao Regiao(int id)
        {
            return Engine.Geo.Regiao(id);
        }

        /// <summary>
        /// Retorna a lista completa de subdistritos do Brasil.
        /// </summary>
        /// <returns>Lista de <see cref="Subdistrito"/>.</returns>
        [Route("Subdistritos")]
        [HttpGet()]
        public IEnumerable<Subdistrito> Subdistritos()
        {
            return Engine.Geo.Subdistritos();
        }

        /// <summary>
        /// Retorna os dados de um subdistrito por sua respectiva identificação.
        /// </summary>
        /// <returns>Lista de <see cref="Subdistrito"/>.</returns>
        [Route("Subdistrito")]
        [HttpGet()]
        public Subdistrito Subdistrito(int id)
        {
            return Engine.Geo.Subdistrito(id);
        }

        /// <summary>
        /// Retorna a lista de subdistritos em uma unidade federativa.
        /// </summary>
        /// <returns>Lista de <see cref="Subdistrito"/>.</returns>
        [Route("SubdistritoPorUf")]
        [HttpGet()]
        public List<Subdistrito> SubdistritoPorUf(int id)
        {
            return Engine.Geo.SubdistritoPorUf(id);
        }

        /// <summary>
        /// Retorna a lista de subdistritos em uma região.
        /// </summary>
        /// <returns>Lista de <see cref="Subdistrito"/>.</returns>
        [Route("SubdistritoPorRegiao")]
        [HttpGet()]
        public List<Subdistrito> SubdistritoPorRegiao(int id)
        {
            return Engine.Geo.SubdistritoPorRegiao(id);
        }

        /// <summary>
        /// Retorna a lista de subdistritos em uma mesorregião.
        /// </summary>
        /// <returns>Lista de <see cref="Subdistrito"/>.</returns>
        [Route("SubdistritoPorMesorregiao")]
        [HttpGet()]
        public List<Subdistrito> SubdistritoPorMesorregiao(int id)
        {
            return Engine.Geo.SubdistritoPorMesorregiao(id);
        }

        /// <summary>
        /// Retorna a lista de subdistritos em uma microrregião.
        /// </summary>
        /// <returns>Lista de <see cref="Subdistrito"/>.</returns>
        [Route("SubdistritoPorMicrorregiao")]
        [HttpGet()]
        public List<Subdistrito> SubdistritoPorMicrorregiao(int id)
        {
            return Engine.Geo.SubdistritoPorMicrorregiao(id);
        }

        /// <summary>
        /// Retorna a lista de subdistritos em um distrito.
        /// </summary>
        /// <returns>Lista de <see cref="Subdistrito"/>.</returns>
        [Route("SubdistritoPorDistrito")]
        [HttpGet()]
        public List<Subdistrito> SubdistritoPorDistrito(int id)
        {
            return Engine.Geo.SubdistritoPorDistrito(id);
        }

        /// <summary>
        /// Retorna a lista de subdistritos em um municipio.
        /// </summary>
        /// <returns>Lista de <see cref="Subdistrito"/>.</returns>
        [Route("SubdistritoPorMunicipio")]
        [HttpGet()]
        public List<Subdistrito> SubdistritoPorMunicipio(int id)
        {
            return Engine.Geo.SubdistritoPorMunicipio(id);
        }
    }
}
