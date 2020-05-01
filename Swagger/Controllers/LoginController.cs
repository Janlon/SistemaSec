using System.Web.Http;
using Sec.Business;
using Sec.Business.Models;

namespace Swagger.Controllers
{

    public class LoginController : ApiController
    {

        [Route("api/Login")]
        public CrudResult<LoginViewModel> Login(LoginViewModel login)
        {
            return null;
        }

        [Route("~/api/Login/Logout")]
        public CrudResult<LoginViewModel> Logout(LoginViewModel login)
        {
            return null;
        }

        [Route("~/api/Login/Change")]
        public CrudResult<LoginViewModel> Change(ChangePasswordViewModel login)
        {
            return null;
        }

        [Route("~/api/Login/Reset")]
        public CrudResult<LoginViewModel> Reset(ResetPasswordViewModel login)
        {
            return null;
        }


        [Route("~/api/Login/Set")]
        public CrudResult<LoginViewModel> Set(SetPasswordViewModel login)
        {
            return null;
        }


        [AllowAnonymous]
        [Route("~/api/Login/Register")]
        public CrudResult<LoginViewModel> Register(RegisterViewModel login)
        {
            return null;
        }
    }
}
