namespace Sec.Business.Models
{

    public class CEP
    {
        public string Codigo { get; set; }
    }

    public class FiltroViewModel
    {
        public string Descricao { get; set; } = "";
        public StringFilter Filtro { get; set; } = StringFilter.Contains;
        public int StartAt { get; set; } = -1;
        public int PageSize { get; set; } = -1;
    }
    public class FiltroDeColunaViewModel
    {
        public string Descricao { get; set; } = "";
        public StringFilter Filtro { get; set; } = StringFilter.Contains;
    }
    public class FiltroDePaginacaoViewModel
    {
        public int StartAt { get; set; } = -1;
        public int PageSize { get; set; } = -1;
    }
}
