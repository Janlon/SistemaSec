namespace Sec.Dal
{
    using Newtonsoft.Json;
    using Sec.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Security;

    /// <summary>
    /// Mais um helper para o contexto de persistência.
    /// </summary>
    public static class DbHelper
    {
        /// <summary>
        /// Inicializar a base de dados já ao inicializar o aplicativo.
        /// </summary>
        public static void Register()
        {
            int p = 0;
            using (ApplicationDbContext db = new ApplicationDbContext())
                p = db.Pessoas.Count();
        }
        /// <summary>
        /// Tenta retornar o conector com a conexão armazenada nas configurações, 
        /// sendo que as credenciais de acesso são criptografadas aqui, e portanto, 
        /// não precisam / não devem estar expostas nessas configurações.
        /// </summary>
        internal static SqlConnection Connection
        {
            get
            {
                SecureString ss = new SecureString();
                foreach (char c in "Senha@123")
                    ss.AppendChar(c);
                ss.MakeReadOnly();
                return new SqlConnection(ConnectionString)
                { Credential = new SqlCredential("sa", ss) };
            }
        }
        /// <summary>
        /// Retorna a lista basica de cargos para a tabela CBO-2002, originada 
        /// por meio do arquivo de texto obtido junto ao Ministério do Trabalho.
        /// </summary>
        internal static List<Cargo> CBO2002
        {
            get
            {
                List<Cargo> ret = new List<Cargo>();
                try
                {
                    string json = Properties.Resources.cbo2002;
                    ret.AddRange(JsonConvert
                        .DeserializeObject<List<Cargo>>(json)
                        .Where(p => !string.IsNullOrEmpty(p.Descricao)));
                }
                catch (Exception ex){ var p = ex; }
                return ret;
            }
        }
        /// <summary>
        /// Retorna o comando de conexão armazenado nas configurações sob o 
        /// nome "DefaultConnection". Se não existir com esse nome, tenta retornar 
        /// o primeiro comando de conexão armazenado nas configurações.
        /// </summary>
        internal static string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder sb = null;
                string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                if (string.IsNullOrEmpty(cs))
                    cs = ConfigurationManager.ConnectionStrings[0].ConnectionString;
                if (string.IsNullOrEmpty(cs))
                    cs = ConfigurationManager.AppSettings["ds"];
                if (!string.IsNullOrEmpty(cs))
                {
                    if ((cs.Contains("source")) || ((cs.Contains("Source"))))
                        sb = new SqlConnectionStringBuilder(cs);
                    else
                        sb = new SqlConnectionStringBuilder() { DataSource = cs };
                    sb.InitialCatalog = "SistemaSec";
                    sb.UserID = "";
                    sb.Password = "";
                    sb.PersistSecurityInfo = true;
                    sb.MultipleActiveResultSets = true;
                    sb.Pooling = true;
                    sb.Enlist = true;
                    return sb.ToString();
                }
                return "";
            }
        }
    }
}
