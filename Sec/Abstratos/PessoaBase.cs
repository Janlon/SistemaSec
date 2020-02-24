namespace Sec.Abstratos
{
    using Sec.Interfaces;
    using System.ComponentModel.DataAnnotations;

    public abstract class PessoaBase<T> : EntidadeBase<T> where T:class
    {
        [EncryptionKey()]
        public string DataKey { get; set; } = SuperKey.Create();

        [RecoverKey()]
        public string LifeKey { get; set; } = SuperKey.Create();
    }
}
