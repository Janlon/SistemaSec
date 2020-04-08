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
        /// Retorna a lista de imagens de uma pessoa
        /// </summary>
        /// <param name="pessoaId"></param>
        /// <returns></returns>
        [Route("~/api/Imagem/{pessoaId:int}/pessoas")]
        public CrudResult<Imagem> GetImagensDaPessoa(int pessoaId)
        {
            return Engine.Imagens.Filter(p => p.Pessoas.Equals(pessoaId));
        }

        /// <summary>
        /// Retorna a lista de imagens de um equipamento
        /// </summary>
        /// <param name="equipamentoId"></param>
        /// <returns></returns>
        [Route("~/api/Imagem/{equipamentoId:int}/equipamentos")]
        public CrudResult<Imagem> GetImagensDoEquipamento(int equipamentoId)
        {
            return Engine.Imagens.Filter(p => p.Equipamentos.Equals(equipamentoId));
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
