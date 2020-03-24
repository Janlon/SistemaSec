namespace Generics.Extensoes
{
    using Generics.Helpers.Errors;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Métodos de extensão para <see cref="Exception"/> e <see cref="string"/>.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Retorna se este <see cref="FileInfo"/> está na faixa de criação indicada.
        /// </summary>
        /// <param name="fi">Este <see cref="FileInfo"/>.</param>
        /// <param name="start">Data inicial.</param>
        /// <param name="end">Data final.</param>
        /// <param name="inclusive">Indica se a faixa inclui ou não as datas-limite.</param>
        /// <returns>Booleano.</returns>
        public static bool IsBetween(this FileInfo fi, DateTime start, DateTime end, bool inclusive)
        {
            bool ret = false;
            if ((fi != null) && (fi.CreationTime != null))
            {
                if (inclusive)
                    ret = (fi.CreationTime >= start && fi.CreationTime <= end);
                else
                    ret = (fi.CreationTime > start && fi.CreationTime < end);
            }
            return ret;
        }
        /// <summary>
        /// Salva este <see cref="Exception"/> no histórico.
        /// </summary>
        /// <param name="value">Este <see cref="Exception"/>.</param>
        /// <param name="memberName">Método cjamador.</param>
        /// <param name="sourceFilePath">Arquivo com o código fonte.</param>
        /// <param name="sourceLineNumber">Linha do arquivo onde o erro foi encontrado.</param>
        public static void Log(this Exception value, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            using (ErrorManager em = new ErrorManager(new ErrorBlock()
            {
                Caller = memberName,
                FileName = sourceFilePath,
                DetectionDate = DateTime.Now,
                ErrorMessage = value.Message,
                JSonError = JsonConvert.SerializeObject(value),
                LineNumer = sourceLineNumber
            }))
                em.Save();
        }
        /// <summary>
        /// Salva esta mensagem de erro no histórico.
        /// </summary>
        /// <param name="value">Esta mensagem de erro.</param>
        /// <param name="memberName">Método cjamador.</param>
        /// <param name="sourceFilePath">Arquivo com o código fonte.</param>
        /// <param name="sourceLineNumber">Linha do arquivo onde o erro foi encontrado.</param>
        public static void Log(this string errorMessage, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Exception value = new Exception(errorMessage);
            using (ErrorManager em = new ErrorManager(new ErrorBlock()
            {
                Caller = memberName,
                FileName = sourceFilePath,
                DetectionDate = DateTime.Now,
                ErrorMessage = value.Message,
                JSonError = JsonConvert.SerializeObject(value),
                LineNumer = sourceLineNumber
            }))
                em.Save();
        }
    }
}
