using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Business.Models
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

public class Cargo
    {
        public Cargo() { }
        internal Cargo(string id, string descricao) 
        {
            Id = int.Parse(id.JustNumbers());
            Descricao = descricao;
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }


    /// <summary>
    /// Subclasse CNAE.
    /// </summary>
    public class Subclasse
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Classe")]
        public string Classe { get; set; }

        [Display(Name = "Observações")]
        public string[] Observacoes { get; set; }
    }
    /// <summary>
    /// Classe CNAE.
    /// </summary>
    public class Classe
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public string Grupo { get; set; }
        public string[] Observacoes { get; set; }
    }
    /// <summary>
    /// Grupo CNAE.
    /// </summary>
    public class Grupo
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public divisao divisao { get; set; }
    }
    /// <summary>
    /// Divisão CNAE.
    /// </summary>
    public class divisao
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public string Secao { get; set; }
    }
    /// <summary>
    /// Seção CNAE.
    /// </summary>
    public class secao
    {

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }



    public class Distrito
    {

        public Distrito() { }
        public Distrito(Sec.Helpers.IBGE.Geo.Distrito item)
        {
            Id = int.Parse(item.Id);
            Nome = item.Nome;
            Municipio = item.Municipio.Nome;
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Municipio")]
        public string Municipio { get; set; }
    }



    /// <summary>
    /// representa um endereço para respsota do serviço de CEP.
    /// </summary>
    public class Endereco
    {
        public Endereco() { }
        internal Endereco(Helpers.IBGE.Geo.Endereco endereco)
        {
            if (endereco != null)
            {
                Bairro = endereco.Bairro;
                CEP = endereco.CEP;
                Complemento = endereco.Complemento;
                Localidade = endereco.Localidade;
                Logradouro = endereco.Logradouro;
                UF = endereco.UF;
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name="Código postal")]
        public string CEP { get; set; } = "Não localizado";

        [Display(Name="Logradouro")]
        public string Logradouro { get; set; } = "Não localizado";

        [Display(Name="Complementos")]
        public string Complemento { get; set; } = "Não localizado";

        [Display(Name="Bairro")]
        public string Bairro { get; set; } = "Não localizado";

        [Display(Name="Localidade")]
        public string Localidade { get; set; } = "Não localizado";

        [Display(Name="Unidade Federativa")]
        public string UF { get; set; } = "Não localizado";
    }

    /// <summary>
    /// Representa uma mesorregião administrativa do Brasil.
    /// </summary>
    public class Mesorregiao
    {
        public Mesorregiao() { }
        public Mesorregiao(Sec.Helpers.IBGE.Geo.Mesorregiao item) 
        {
            Id = int.Parse(item.Id.JustNumbers());
            Nome = item.Nome;
            Uf = item.Uf.Nome;
        }
        internal Mesorregiao(string id, string nome, Helpers.IBGE.Geo.Uf uf)
        {
            Id = int.Parse(id.JustNumbers());
            Nome = nome;
            Uf = uf.Nome;
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name="Nome")]
        public string Nome { get; set; }

        [Display(Name = "Unidade Federativa")]
        public string Uf { get; set; }
    }


    /// <summary>
    /// Representa um subdistrito.
    /// </summary>
    public class Subdistrito
    {
        public Subdistrito() { }
        public Subdistrito(Sec.Helpers.IBGE.Geo.Subdistrito item)
        {
            Id = int.Parse(item.Id);
            Nome = item.Nome;
            Distrito = item.Distrito.Nome;
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Distrito")]
        public string Distrito { get; set; }
    }

    /// <summary>
    /// Representa uma microrregião administrativa.
    /// </summary>
    public class Microrregiao
    {
        public Microrregiao() { }
        public Microrregiao(Sec.Helpers.IBGE.Geo.Microrregiao item)
        {
            Id = int.Parse(item.Id.JustNumbers());
            Nome = item.Nome;
            Mesorregiao = item.Mesorregiao.Nome;
        }
        internal Microrregiao(string id, string nome, Helpers.IBGE.Geo.Mesorregiao mesorregiao)
        {
            Id = int.Parse(id.JustNumbers());
            Nome = nome;
            Mesorregiao = mesorregiao.Nome;
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name="Mesorregião")]
        public string Mesorregiao { get; set; }
    }

    /// <summary>
    /// Representa um município.
    /// </summary>
    public class Municipio
    {
        public Municipio() { }
        public Municipio(Sec.Helpers.IBGE.Geo.Municipio item)
        {
            Id = int.Parse(item.Id.JustNumbers());
            Nome = item.Nome;
            Microrregiao = item.Microrregiao.Nome;
        }
        internal Municipio(string id, string nome, Helpers.IBGE.Geo.Microrregiao microrregiao)
        {
            Id = int.Parse(id.JustNumbers());
            Nome = nome;
            Microrregiao = microrregiao.Nome;
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name="Microrregião")]
        public string Microrregiao { get; set; }
    }

    /// <summary>
    /// Representa uma região administrativa do Brasil.
    /// </summary>
    public class Regiao
    {
        public Regiao() { }
        public Regiao(Sec.Helpers.IBGE.Geo.Regiao item)
        {
            Id = int.Parse(item.Id.JustNumbers());
            Sigla = item.Sigla;
            Nome = item.Nome;
        }
        internal Regiao(string id, string sigla, string nome)
        {
            Id = int.Parse(id.JustNumbers());
            Sigla = sigla;
            Nome = nome;
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Sigla")]
        public string Sigla { get; set; }
    }


    /// <summary>
    /// Representa uma UF.
    /// </summary>
    public class Uf
    {
        public Uf() { }

        public Uf(Helpers.IBGE.Geo.Uf uf)
        {
            Id = int.Parse(uf.Id.JustNumbers());
            Sigla = uf.Sigla;
            Nome = uf.Nome;
            Regiao = uf.Regiao.Nome;
        }

        internal Uf(string id, string sigla, string nome, Helpers.IBGE.Geo.Regiao regiao)
        {
            Id = int.Parse(id.JustNumbers());
            Sigla = sigla;
            Nome = nome;
            Regiao = regiao.Nome;
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Sigla")]
        public string Sigla { get; set; }

        [Display(Name = "Região")]
        public string Regiao { get; set; }
    }
}
