using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sec.Models;

namespace Swagger.Controllers
{
    public class PessoaController : ApiController
    {

        /// <summary>
        /// Retornar uma lista de informações
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Pessoa> Get()
        {
            return new List<Pessoa>();
        }

        /// <summary>
        /// Retornar o registro selecionado
        /// </summary>
        /// <param name="id">{id}</param>
        /// <returns></returns>
        public Pessoa Get(int id)
        {
            return new Pessoa() { Id = id };
        }

        /// <summary>
        /// Cadastrar um novo registro
        /// </summary>
        /// <param name="pessoa"></param>
        public void Post([FromBody] Pessoa pessoa)
        {

        }

        /// <summary>
        /// Alterar o registro selecionado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pessoa"></param>
        public void Put(int id, [FromBody]Pessoa pessoa)
        {
        }

        /// <summary>
        /// Apagar o registro selecionado
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {

        }
    }
}
