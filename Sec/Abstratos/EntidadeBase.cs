namespace Sec.Abstratos
{

    using Sec.Interfaces;
    using System;

    public abstract class EntidadeBase<T> :  IDisposable
    {
        #region Manutenção
        private bool disposedValue;
        #endregion
        #region Limpeza de memória
        private void CleanUp() { }
        #endregion
        #region Instância
        public EntidadeBase() { disposedValue = false; }
        protected virtual void Dispose(bool disposing)
        { if (!disposedValue) { if (disposing) { CleanUp(); } disposedValue = true; } }
        ~EntidadeBase() { Dispose(false); }
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        #endregion
    }
}
