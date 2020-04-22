namespace Sec.Business.Core
{
    #region Espaços
    using Microsoft.AspNet.Identity.EntityFramework;
    using Sec.Business.Models;
    using Sec.IdentityGroup;
    using Sec.Models;
    using System;
    #endregion

    internal class AtividadesFactory : Factory<Atividade> { }
    internal class UsuariosFactory : Factory<Usuario> { }
    internal class ContatosFactory : Factory<Contato>   {    }
    internal class DocumentosFactory : Factory<Documento> {  }
    internal class EnderecosFactory : Factory<Endereco> { }
    internal class EnderecosDasPessoasFactory : Factory<EnderecoDaPessoa> { }
    internal class EntregasFactory : Factory<EntregaDoItemDaOrdemDeServico> { }
    internal class EquipamentosFactory : Factory<Equipamento> { }
    internal class ImagensFactory : Factory<Imagem> { }
    internal class ItensDasOrdensDeServicoFactory : Factory<ItemDaOrdemDeServico> { }
    internal class OrdensDeServicoFactory : Factory<OrdemDeServico> { }
    internal class PessoasFactory : Factory<Pessoa> { }
    internal class EmpresasFactory : Factory<Empresa> { }
    internal class RetiradasFactory : Factory<RetiradaDoItemDaOrdemDeServico> { }
    internal class ServicosFactory : Factory<Servico> { }
    internal class SetoresFactory : Factory<Setor> { }
    internal class TiposDeDocumentosFactory : Factory<TipoDeDocumento> { }
    internal class TiposDeEquipamentosFactory : Factory<TipoDeEquipamento> { }
    internal class TiposDeSetoresFactory : Factory<TipoDeSetor> { }
    internal class RolesFactory : Factory<IdentityRole> { }
    internal class UsersFactory : Factory<IdentityUser> { }
    internal class AppUsersFactory : Factory<ApplicationUser> { }
}
