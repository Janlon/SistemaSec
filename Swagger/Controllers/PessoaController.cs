using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sec.Business;
using Sec.Models;

namespace Swagger.Controllers
{
    public class PessoaController : ApiController
    {
        public CrudResult<Pessoa> Get()
        {
            return Engine.Pessoas.List();
        }

        public CrudResult<Pessoa> Get(int id)
        {
            return Engine.Pessoas.Find(new object[] { id });
        }

        /// <summary>
        /// Retorna a lista de imagens de uma pessoa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("~/api/Imagem/{Id:int}/imagens")]
        public CrudResult<Pessoa> GetImagensDaPessoa(int id)
        {
            var imagens = Engine.Imagens.Filter(p => p.Pessoas.FirstOrDefault().Id.Equals(id)).Result;
            if (imagens.Count < 1)
                return new CrudResult<Pessoa>();

            var pessoas = Engine.Pessoas.Find(new object[] { id });
            foreach (var item in pessoas.Result)
            {
                item.Imagens = imagens;
            }

            return pessoas;
        }


        public CrudResult<Pessoa> Post(Pessoa obj)
        {
            return Engine.Pessoas.Insert(obj);
        }

        public CrudResult<Pessoa> Put(Pessoa obj)
        {
            return Engine.Pessoas.Update(obj);
        }

        public CrudResult<Pessoa> Delete(int id)
        {
            return Engine.Pessoas.Delete(Engine.Pessoas.Find(new object[] { id }).Result.FirstOrDefault());
        }
    }
}
