namespace Generic.Business.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Http.Authentication;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}