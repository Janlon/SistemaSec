using Sec.Business;
using Sec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class ImagemController : ApiController
    {
        /// <summary>
        /// Retorna a lista de imagens
        /// </summary>
        /// <returns></returns>
        public CrudResult<Imagem> Get()
        {
            return Engine.Imagens.List();
        }

        /// <summary>
        /// Retorna um item específico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CrudResult<Imagem> Get(int id)
        {
            return Engine.Imagens.Find(new object[] { id });
        }

        /// <summary>
        /// Cadastra uma imagem
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CrudResult<Imagem> Post(Imagem obj)
        {
            return Engine.Imagens.Insert(obj);
        }

        /// <summary>
        /// Apaga uma imagem 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CrudResult<Imagem> Delete(int id)
        {
            return Engine.Imagens.Delete(Engine.Imagens.Find(new object[] { id }).Result.FirstOrDefault());
        }
    }
}
