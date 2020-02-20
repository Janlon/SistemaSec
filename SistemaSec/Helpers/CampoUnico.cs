using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SistemaSec.Helpers
{
    internal class CampoUnico : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string strValue = value as string;

            if (!string.IsNullOrEmpty(strValue))
            {
               
            }
            return true;
        }
    }
}