namespace Sec.IdentityGroup
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Sec.Business;
    using System;

    public class ApplicationManager : IDisposable
    {
        #region Manutenção
        private bool disposedValue;
        public Db Db { get; private set; }
        public ApplicationRoleManager RM { get; private set; }
        public ApplicationUserManager UM { get; private set; }
        #endregion

        #region Instância
        public ApplicationManager()
        {
            disposedValue = false;
            Db = new Db();
            RM = new ApplicationRoleManager(new RoleStore<IdentityRole>(Db));
            UM = new ApplicationUserManager(new UserStore<ApplicationUser>(Db));
            UM.UserValidator = new ApplicationUserValidator(UM);
            UM.PasswordValidator = new ApplicationPasswordValidator();
        }

        internal void Commit()
        {
            if(Db!=null)
            {
                Db.ChangeTracker.DetectChanges();
                Db.SaveChanges();
            }
        }

        private void CleanUp()
        {
            try { if (RM != null) RM.Dispose(); RM = null; } catch { }
            try { if (UM != null) UM.Dispose(); UM = null; } catch { }
            try { if (Db != null) Db.Dispose(); Db = null; } catch { }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { CleanUp(); }
                disposedValue = true;
            }
        }
        ~ApplicationManager() { Dispose(false); }
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        #endregion
    }
}