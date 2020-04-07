using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Generics.Helpers.IBGE.CBO;    // ViewModels genéricos
using Sec.Business;                 // Serviços
using Sec.Business.Models;          // Modelos de transporte

namespace Swagger.Controllers
{

    // Aqui é que eu tinha comentado. 
    // Pode ser que hajam vários "Gets", 
    // por exemplo, por ID, por nome completo, por parte do nome, etc.
   // [RoutePrefix("api/Atividades")]
    public class CargoController : ApiController
    {

        /// <summary>
        /// Retorna a lista completa de cargos do CBO2002 e as atividades do CNAE.
        /// </summary>
        /// <returns>Lista de <see cref="Atividade"/>.</returns>
       // [Route("Atividades")]
        public IEnumerable<Atividade> GetCargos()
        {
            return Engine.Cargos.Atividades();
        }
        ///// <summary>
        ///// Retorna os itens da lista completa de cargos do CBO2002 e as atividades do CNAE que satisfaçam o identificador.
        ///// </summary>
        ///// <param name="id">Identifiador de registro à ser localizado.</param>
        ///// <returns>Lista de <see cref="Atividade"/>.</returns>
        //[Route("AtividadesPorId")]
        //public IEnumerable<Atividade> GET(int id)
        //{
        //    return Engine.Cargos.Atividades().Where(p => p.Id.Equals(id));
        //}
        ///// <summary>
        ///// Retorna os itens da lista completa de cargos do CBO2002 e as atividades do CNAE que satisfaçam a descrição.
        ///// </summary>
        ///// <param name="description">Descrição do registro à ser localizado.</param>
        ///// <returns>Lista de <see cref="Atividade"/>.</returns>
        //[Route("AtividadesPorDescricao")]
        //public Atividade GET(string description)
        //{
        //    return Engine.Cargos.Atividades().Where(p => p.Descricao
        //        .Equals(description))
        //        .FirstOrDefault();
        //}
        ///// <summary>
        ///// Retorna os itens da lista completa de cargos do CBO2002 e as atividades do CNAE que satisfaçam o filtro.
        ///// </summary>
        ///// <param name="filtro">Valor da enumeração <see cref="FiltroViewModel"/>.</param>
        ///// <returns>Lista de <see cref="Atividade"/>.</returns>
        //[Route("AtibvidadesFiltrar")]
        //public IEnumerable<Atividade> GET([FromBody]FiltroViewModel filtro)
        //{
        //    return Engine.Cargos.Atividades(filtro);
        //}





        //// GET: api/Cargo
        ///// <summary>
        ///// Retorna a lista completa de cargos do Código Brasileiro de Ocupações.
        ///// </summary>
        ///// <returns></returns>
        //[Route("CargoListar")]
        //public IEnumerable<Cargo> Get()
        //{
        //    return Engine.Cargos.List();
        //}

        //// GET: api/Cargo/5
        ///// <summary>
        ///// Retorna cargo do Código Brasileiro de Ocupações que possua o código indicado.
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[Route("CargoPorId")]
        //public Cargo Get(int id)
        //{
        //    return Engine.Cargos.Select(id);
        //}

        //// GET: api/Cargo/general&2
        ///// <summary>
        ///// Retorna a lista completa de cargos do Código Brasileiro de Ocupações que satisfaçam os critérios de filtro indicados.
        ///// </summary>
        ///// <param name="filtro">Valor da enumeração (<see cref="FiltroViewModel"/>). Tipo de filtro a ser aplicado.</param>
        ///// <returns></returns>
        //[Route("CargoFiltrar")]
        //public IEnumerable<Cargo> Listar([FromBody]FiltroViewModel filtro)
        //{ 
        //    return Engine.Cargos.Filter(filtro); 
        //}
    }
}
