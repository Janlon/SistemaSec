namespace Sec.Business
{
    using Sec.Business.Models;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Classe para encapsular o retorno de chamnadas à métodos CRUD.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CrudResult<T> :  IDisposable where T : class
    {
        #region Manutenção
        private int mAffected = -1;
        private bool mDisposed = false;
        private Stopwatch sw { get; set; } = new Stopwatch();
        private void StartMe()
        {
            Action = CrudAction.List;
            mDisposed = false;
            sw = new Stopwatch();
            sw.Start();
        }
        #endregion

        #region Instância
        public CrudResult()
        {
            Result = new List<T>();
            StartMe();
        }
        internal CrudResult(T value)
        {
            Result = new List<T>();
            Origin = value;
            StartMe();
        }
        ~CrudResult() { Dispose(false); }
        private void CleanUp()
        {
            try { if ((sw != null) && (!sw.IsRunning)) sw.Stop(); } catch { }
            try { if (sw != null) sw = null; } catch { }
        }
        protected virtual void Dispose(bool disposing) { if (!mDisposed) { if (disposing) { CleanUp(); } mDisposed = true; } }
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        #endregion

        #region Propriedades
        /// <summary>
        /// Tempo utilizado pelo processo.
        /// </summary>
        public TimeSpan Delay { get { return sw != null ? sw.Elapsed : TimeSpan.FromSeconds(0); } }
        /// <summary>
        /// Ação efetuada.
        /// </summary>
        public CrudAction Action { get; set; } = CrudAction.List;
        /// <summary>
        /// Resultado (sempre uma lista).
        /// </summary>
        public List<T> Result { get; internal set; } = new List<T>();
        /// <summary>
        /// Item de origem do processamento.
        /// </summary>
        public T Origin { get; set; } = null;
        /// <summary>
        /// Outros itens relacionados para o processo.
        /// </summary>
        public List<dynamic> RelatedItems { get; internal set; } = new List<dynamic>();
        /// <summary>
        /// Erros detectados (de validação ou dados).
        /// </summary>
        public List<CrudError> Errors { get; internal set; } = new List<CrudError>();
        /// <summary>
        /// Processado sem erros?
        /// </summary>
        public bool Success { get { return Errors.Count == 0; } }
        /// <summary>
        /// Registros afetados.
        /// </summary>
        public int Affected { get { return mAffected; } }
        /// <summary>
        /// Tipo de ítem processado.
        /// </summary>
        public Type Type { get { return typeof(T); } }
        #endregion

        #region Suporte
        internal void Finish() { try { sw.Stop(); } catch { } }
        internal void SetResult(dynamic result) { Result.Add(result); }
        internal void SetAffected(int value) { mAffected = value; }
        internal void AddError(string propertyName, string message) { Errors.Add(new CrudError() { Message = message, PropertyName = propertyName }); }
        internal void AddError(Exception ex) { Errors.Add(new CrudError() { Message = ex.Message, PropertyName = Type.Name }); }
        internal void Include(dynamic result) 
        {
            if (!result.Success)
                Errors.AddRange(result.Errors);
            RelatedItems.Add(result.Origin);
        }
        #endregion
    }
}
