namespace Generics.Dal
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security;

    /// <summary>
    /// Mais um helper para o contexto de persistência.
    /// </summary>
    internal static class DbHelper
    {
        /// <summary>
        /// Salvar a linha de ação executada.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="message"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public static void DataLog(string component, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            string pattern = "Component: {0} Message: {1} ";
            using (DataLogManager dlm = new DataLogManager(
                string.Format(pattern, component, message),
                memberName, sourceFilePath, sourceLineNumber))
                dlm.Save();
        }

        ///// <summary>
        ///// Inicializar a base de dados já ao inicializar o aplicativo.
        ///// </summary>
        //public static void Register<T>() where T: IdentityUser
        //{
        //    int p = 0;
        //    using (DB<T> db = DB<T>)
        //        p = db.Pessoas.Count();
        //}
        /// <summary>
        /// Tenta retornar o conector com a conexão armazenada nas configurações, 
        /// sendo que as credenciais de acesso são criptografadas aqui, e portanto, 
        /// não precisam / não devem estar expostas nessas configurações.
        /// </summary>
        internal static SqlConnection Connection
        {
            get
            {
                SqlConnection ret = null;
                SecureString ss = new SecureString();
                string cs = "";
                if (ConfigurationManager.AppSettings["dbu"] != null)
                {
                    cs = ConfigurationManager.AppSettings["dbu"];
                    if (cs.Contains(';'))
                    {
                        foreach (char c in cs.Split(';')[1])
                            ss.AppendChar(c);
                        ss.MakeReadOnly();
                        ret = new SqlConnection(ConnectionString)
                        { Credential = new SqlCredential(cs.Split(';')[0], ss) };
                    }
                }
                return ret;
            }
        }        

        /// <summary>
        /// Retorna o comando de conexão armazenado nas configurações.
        /// A pesquisa principal é sob o nome "dbs", em "appSettings".
        /// Se não encontrado, será pesquisado o nome "DefaultConnection" em "connectionsStrings". 
        /// Se não existir com esse nome, tenta retornar o primeiro comando de conexão armazenado 
        /// nas configurações de conexão aos dados. E finalmente, não encontrando nada, gera um erro 
        /// de confuguação.
        /// </summary>
        internal static string ConnectionString
        {
            get
            {
                string cs = "";
                if (ConfigurationManager.AppSettings["dbs"] != null)
                {
                    cs = ConfigurationManager.AppSettings["dbs"];
                    if (cs.Contains(";"))
                    {
                        SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
                        sb.InitialCatalog = cs.Split(';')[1];
                        sb.DataSource = cs.Split(';')[0];
                        sb.PersistSecurityInfo = true;
                        sb.IntegratedSecurity = false;
                        sb.ApplicationName = AppDomain.CurrentDomain.FriendlyName;
                        sb.CurrentLanguage = "brazilian";
                        sb.Enlist = true;
                        sb.MultipleActiveResultSets = true;
                        sb.Pooling = true;
                        sb.WorkstationID = Environment.MachineName;
                        return sb.ToString();
                    }
                }
                else
                {
                    if (ConfigurationManager.ConnectionStrings["DefaultConnection"] != null)
                        cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    if (string.IsNullOrEmpty(cs))
                        cs = ConfigurationManager.ConnectionStrings[0].ConnectionString;
                    if (string.IsNullOrEmpty(cs))
                        cs = ConfigurationManager.AppSettings["ds"];
                    if (!string.IsNullOrEmpty(cs))
                    {
                        SqlConnectionStringBuilder sb = null;
                        if ((cs.Contains("source")) || ((cs.Contains("Source"))))
                            sb = new SqlConnectionStringBuilder(cs);
                        else
                            sb = new SqlConnectionStringBuilder() { DataSource = cs };
                        sb.InitialCatalog = "SistemaSec";
                        sb.UserID = "";
                        sb.Password = "";
                        sb.PersistSecurityInfo = true;
                        sb.MultipleActiveResultSets = true;
                        sb.ApplicationIntent = ApplicationIntent.ReadWrite;
                        sb.ApplicationName = AppDomain.CurrentDomain.FriendlyName;
                        sb.ConnectRetryCount = 5;
                        sb.ConnectRetryInterval = 2;
                        sb.CurrentLanguage = "brazilian";
                        sb.WorkstationID = Environment.MachineName;
                        sb.Pooling = true;
                        sb.Enlist = true;
                        return sb.ToString();
                    }
                }
                throw new SettingsPropertyNotFoundException("Não foram encontradas as configurações de acesso aos dados");
                //return "";
            }
        }
    }
}
