using System.Web.Http;

using Sec.Business.Models;

namespace Swagger.Controllers
{

    public class LoginController : ApiController
    {

        /// <summary>
        /// Conectar o usuário.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Route("api/Login")]
        public IHttpActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        /// <summary>
        /// Desconectar o usuário.
        /// </summary>
        /// <returns></returns>
        [Route("api/Login/Logout")]
        public IHttpActionResult Logout()
        {
            return Ok();
        }

        /// <summary>
        /// Alterar a senha do usuário.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Route("api/Login/Change")]
        public IHttpActionResult Change(ChangePasswordViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        /// <summary>
        /// Apagar a senha do usuário.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Route("api/Login/Reset")]
        public IHttpActionResult Reset(ResetPasswordViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        /// <summary>
        /// Definir a senha do usuário.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Route("api/Login/Set")]
        public IHttpActionResult Set(SetPasswordViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        /// <summary>
        /// Cadastrar um novo usuário.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/Login/Register")]
        public IHttpActionResult Register(RegisterViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

    }
}
