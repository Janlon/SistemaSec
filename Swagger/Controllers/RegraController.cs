using Generics.Extensoes;
using Microsoft.AspNet.Identity.EntityFramework;
using Sec;
using Sec.Business;
using Sec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace Swagger.Controllers
{
    public class RegraController : ApiController
    {
        public CrudResult<IdentityRole> Get()
        {
            return Engine.Regras.List();
        }

        public CrudResult<IdentityRole> Get(int id)
        {
            return Engine.Regras.Find(new object[] { id });
        }

        public CrudResult<IdentityRole> Post(IdentityRole obj)
        {
            return Engine.Regras.Insert(obj);
        }

        public CrudResult<IdentityRole> Put(IdentityRole obj)
        {
            return Engine.Regras.Update(obj);
        }

        public CrudResult<IdentityRole> Delete(int id)
        {
            return Engine.Regras.Delete(Engine.Regras.Find(new object[] { id }).Result.FirstOrDefault());
        }
    }
}
