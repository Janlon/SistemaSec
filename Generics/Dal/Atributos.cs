namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Este atributo indica que a propriedade não deve ser visualizada por ninguém exceto seu proprietário.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class SigilousAttribute : Attribute { }

    /// <summary>
    /// Este atributo indica que esta propriedade deve ser criptografada para a persistência e decriptografada para uso.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class EncryptedAttribute : Attribute { }

    /// <summary>
    /// Este atributo indica que esta propriedade é a propriedade utilizada como chave criptográfica do registro/instância.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EncryptionKeyAttribute : Attribute { }

    /// <summary>
    /// Este atributo indica que esta propriedade deve manter a chave de segurança requerida para alteração de dados e recuperação de informações sigilosas.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RecoverKeyAttribute : Attribute { }

    /// <summary>
    /// Este atributo indica que esta propriedade é a responsável pela manutenção de operações de "soft exclusion", ou seja, marca o registro pertinente como excluído sem efetivamente removê-lo da persistência.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ActivationAttribute : Attribute { }
}
