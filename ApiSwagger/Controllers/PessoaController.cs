using Sec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiSwagger.Controllers
{
    public class PessoaController : ApiController
    {
        /// <summary>
        /// Retorna uma lista de pessoas cadastradas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Pessoa> Get()
        {
            return new List<Pessoa>();
        }

        /// <summary>
        /// Retorna o registro de uma pessoa.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Pessoa Get(int id)
        {
            return new Pessoa { Id = id };
        }

        /// <summary>
        /// Realiza o cadastro de uma pessoa.
        /// </summary>
        /// <param name="pessoa">Objeto do tipo Pessoa</param>
        public void Post([FromBody]Pessoa pessoa)
        {
        }

        /// <summary>
        /// Realiza a alteração no registro de uma pessoa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pessoa">Objeto do tipo Pessoa</param>
        public void Put(int id,[FromBody]Pessoa pessoa)
        {
        }

        /// <summary>
        /// Realiza a exclusão do cadastro de uma pessoa
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
        }
    }
}
