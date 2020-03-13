namespace Generics.Abstratos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public interface IEntidadeBase
    {
        string DataKey { get; set; }
        string LifeKey { get; set; }
    }

    public abstract class EntidadeBase: IEntidadeBase
    {
        [ScaffoldColumn(false)]
        [Column(TypeName ="VARCHAR")]
        [StringLength(256)]
        public string DataKey { get; set; } = SuperKey.Create();

        [ScaffoldColumn(false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(256)]
        public string LifeKey { get; set; } = SuperKey.Create();
    }

    //public abstract class EntidadeBase<T> :  IDisposable
    //{
    //    #region Manutenção
    //    private bool disposedValue;
    //    #endregion
    //    #region Limpeza de memória
    //    private void CleanUp() { }
    //    #endregion
    //    #region Instância
    //    public EntidadeBase() { disposedValue = false; }
    //    protected virtual void Dispose(bool disposing)
    //    { if (!disposedValue) { if (disposing) { CleanUp(); } disposedValue = true; } }
    //    ~EntidadeBase() { Dispose(false); }
    //    public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
    //    #endregion
    //}
}
