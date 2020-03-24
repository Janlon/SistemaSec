namespace Sec.Business
{
    /// <summary>
    /// Classe para encapsular mensagens de erro de validação.
    /// </summary>
    public class CrudError
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
