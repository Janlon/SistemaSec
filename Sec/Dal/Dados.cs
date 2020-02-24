namespace Sec.Dados
{
    using Sec.Dal;
    using Sec.Helpers.Errors;
    using Sec.Models;
    using System;
    using System.Data.SqlClient;
    using System.Linq;

    public static class Pessoas
    {

        public static DbResponse<Pessoa> InsertFuncionario(Pessoa pessoa)
        {
            DbResponse<Pessoa> ret = new DbResponse<Pessoa>(DbAction.Insert);
            try 
            {
                Pessoa duplicado;
                using(DB db=new DB())
                {
                    duplicado = db.Pessoas.Where(p => (p.Nome.Equals(pessoa.Nome) &&
                                                    (p.Documento.Equals(pessoa.Documento) &&
                                                    (p.Nascimento.Equals(pessoa.Nascimento)))))
                                .FirstOrDefault();
                                                  
                }
                if (duplicado == null)
                    using (SqlConnection db = DbHelper.Connection)
                    { }
                else
                    return UpdateFuncionario(pessoa);
            }
            catch (Exception ex)
            {
                ex.Log();
                ret.AddError("Genérico", ex.Message);
            }
            return ret;
        }

        private static DbResponse<Pessoa> UpdateFuncionario(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }
        private static DbResponse<Pessoa> ReactivateFuncionario(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }
        private static DbResponse<Pessoa> DeactivateFuncionario(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }
    }


}
