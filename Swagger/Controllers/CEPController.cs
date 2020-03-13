using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sec.Business;
using Sec.Business.Models;

namespace Swagger.Controllers
{
    [RoutePrefix("api/CEP")]
    public class CEPController : ApiController
    {
        /// <summary>
        /// Retorna, se localizar, o endereço correspondente ao CEP indicado.
        /// </summary>
        /// <param name="cep"><see cref="CEP"/></param>
        /// <returns>Objeto do tipo <see cref="Endereco"/>.</returns>
        [Route("EnderecoDoCep")]
        public Endereco Post([FromBody] CEP cep )
        {
            Endereco ret = Engine.Geo.EnderecoByCEP(cep.Codigo);
            if (ret == null) ret = new Endereco();
            return ret;
        }
    }
}
