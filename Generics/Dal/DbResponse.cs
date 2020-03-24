namespace Generics.Dados
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class DbResponse<T> : IDisposable where T : class
    {
        #region Manutenção
        private Stopwatch sw;
        private bool disposedValue;
        #endregion

        #region Instância
        public DbResponse() { disposedValue = false; sw = new Stopwatch(); }
        internal DbResponse(DbAction action) { disposedValue = false; sw = new Stopwatch(); RequestAction = action; sw.Start(); }
        private void CleanUp()
        {
            try { if (sw != null) sw.Stop(); { } } catch { }
            try { if (sw != null) sw=null; { } } catch { }
        }        
        protected virtual void Dispose(bool disposing) { if (!disposedValue) { if (disposing) { CleanUp(); } disposedValue = true; } }
        ~DbResponse() { Dispose(false); }
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        #endregion

        #region Propriedades
        /// <summary>
        /// Lista de erros detectados.
        /// </summary>
        public Dictionary<string, string> ErrorList { get; internal set; } = new Dictionary<string, string>();

        /// <summary>
        /// Tipo de requisição efetuada.
        /// </summary>
        public DbAction RequestAction { get; internal set; } = DbAction.Insert;

        /// <summary>
        /// Tempo de demora da execução solicitada.
        /// </summary>
        public TimeSpan Delay { get { return (sw != null) ? sw.Elapsed : TimeSpan.FromSeconds(0); }  }

        /// <summary>
        /// Resultado obtido (pode estar nulo).
        /// </summary>
        public IEnumerable<T> Result { get; internal set; } = null;

        /// <summary>
        /// Informa se a solicitação foi executada com sucesso.
        /// </summary>
        public bool Success { get { return (ErrorList.Count == 0); } }

        /// <summary>
        /// Informa quantos registros foram afetados.
        /// </summary>
        public int Affected
        {
            get
            {
                try { return (Result == null) ? 0 : Result.Count(); }
                catch { return 0; }
            }
        }
        #endregion

        #region Métodos
        internal void AddError(string propertyName, string errorMessage)
        {
            if (ErrorList.ContainsKey(propertyName)) ErrorList.Remove(propertyName);
            ErrorList.Add(propertyName, errorMessage);
        }
        internal void RemoveError(string propertyName) { if (ErrorList.ContainsKey(propertyName)) ErrorList.Remove(propertyName); }
        internal void AddResult(T newResult, bool finished = false)
        {
            Result = (new List<T>() { newResult }).AsEnumerable();
            if (finished) Finish();
        }
        internal void AddResult(IEnumerable<T> newResult, bool finished = false)
        {
            Result = newResult.AsEnumerable();
            if (finished) Finish();
        }
        internal void AddResult(List<T> newResult, bool finished = false)
        {
            Result = newResult.AsEnumerable();
            if (finished) Finish();
        }
        internal void ClearResult() { Result = null; }
        internal void Finish() { sw.Stop(); }
        #endregion
    }




    public class ErrorList 
    {
        private Dictionary<string, string> lista { get; set; } = new Dictionary<string, string>();
        public void Add(string propertyName, string errorMessage) 
        {
            if (lista.ContainsKey(propertyName))
                lista.Remove(propertyName);
            lista.Add(propertyName, errorMessage);
        }
        public void Clear()
        {
            lista.Clear();
            lista = null;
            lista = new Dictionary<string, string>();
        }
    }

}
