namespace System.ComponentModel.DataAnnotations
{
    using Generics.Extensoes;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// Validação customizada para CNPJ
    /// </summary>
    public class CNPJAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public CNPJAttribute() { }

        /// <summary>
        /// Validação server
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;
            bool valido = value.ToString().IsCNPJ();
            return valido;
        }

        /// <summary>
        /// Validação client
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "CNPJ"
            };
        }
    }

    /// <summary>
    /// Validação customizada para CPF
    /// </summary>
    public class CPFAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public CPFAttribute() { }

        /// <summary>
        /// Validação server
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;
            bool valido = value.ToString().IsCPF();
            return valido;
        }

        /// <summary>
        /// Validação client
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "cpf"
            };
        }
    }

    /// <summary>
    /// Validação customizada para IPAddress
    /// </summary>
    public class IPAddressAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public IPAddressAttribute() { }

        /// <summary>
        /// Validação server
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;
            bool valido = value.ToString().IsIPAddress();
            return valido;
        }

        /// <summary>
        /// Validação client
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "IPAddress"
            };
        }
    }

    /// <summary>
    /// A propriedade deve ter valores inteiros que estejam na lista pré definida.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class BlackListAttribute : ValidationAttribute
    {
        /// <summary>
        /// The White List 
        /// </summary>
        public IEnumerable<dynamic> BlackList { get; internal set; }

        private IEnumerable<string> textList
        {
            get
            {
                List<string> ret = new List<string>();
                try
                {
                    foreach (dynamic item in BlackList)
                        ret.Add(Convert.ToString(item));
                }
                catch { }
                return ret;
            }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="BlackList"></param>
        public BlackListAttribute(params dynamic[] values)
        {
            BlackList = new List<dynamic>(values);            
        }

        /// <summary>
        /// Validation occurs here
        /// </summary>
        /// <param name="value">Value to be validate</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return (BlackList.Contains((int)value) == false);
        }

        /// <summary>
        /// Get the proper error message
        /// </summary>
        /// <param name="name">Name of the property that has error</param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return $"{name} não deve conter nenhum dos valores: {String.Join(",", textList)}";
        }
    }

    /// <summary>
    /// A propriedade deve ter valores inteiros que estejam na lista pré definida.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class WhiteListAttribute : ValidationAttribute
    {
        /// <summary>
        /// The White List 
        /// </summary>
        public IEnumerable<dynamic> WhiteList { get; }

        private IEnumerable<string> textList
        {
            get
            {
                List<string> ret = new List<string>();
                try
                {
                    foreach (dynamic item in WhiteList)
                        ret.Add(Convert.ToString(item));
                }
                catch { }
                return ret;
            }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="values"></param>
        public WhiteListAttribute(params dynamic[] values)
        {
            WhiteList = new List<dynamic>(values);
        }

        /// <summary>
        /// Validation occurs here
        /// </summary>
        /// <param name="value">Value to be validate</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return WhiteList.Contains((int)value);
        }

        /// <summary>
        /// Get the proper error message
        /// </summary>
        /// <param name="name">Name of the property that has error</param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return $"{name} deve conter ao menos um dos valores: {String.Join(",", textList)}";
        }
    }
}
