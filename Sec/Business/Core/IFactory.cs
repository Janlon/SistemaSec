namespace Sec.Business
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Contrato de ações de fábrica genérica.
    /// </summary>
    /// <typeparam name="T">Definição do tipo</typeparam>
    public interface IFactory<T> where T : class
    {
        CrudResult<T> Create(T value);
        CrudResult<T> Retrieve(Expression<Func<T, bool>> value);
        CrudResult<T> Update(T value);
        CrudResult<T> Delete(T value);
        CrudResult<T> Deactivate(T value);
        CrudResult<T> Reactivate(T value);
        CrudResult<T> List();
        CrudResult<T> Filter(Expression<Func<T, bool>> value);
    }
}
