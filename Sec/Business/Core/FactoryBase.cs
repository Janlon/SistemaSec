namespace Sec.Business
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Implementação abstrata de fábrica de entidades.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FactoryBase<T> : IFactory<T> where T : class
    {
        public abstract CrudResult<T> Create(T value);
        public abstract CrudResult<T> Retrieve(Expression<Func<T, bool>> value);
        public abstract CrudResult<T> Update(T value);
        public abstract CrudResult<T> Delete(T value);
        public abstract CrudResult<T> Deactivate(T value);
        public abstract CrudResult<T> Reactivate(T value);
        public abstract CrudResult<T> List();
        public abstract CrudResult<T> Filter(Expression<Func<T, bool>> value);
    }    
}
