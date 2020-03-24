namespace Generics.Helpers
{
    namespace Errors
    {
        using Generics.Extensoes;
        using Newtonsoft.Json;
        using System;
        using System.Collections.Generic;
        using System.IO;
        using System.Runtime.CompilerServices;
        using System.Text;
        using System.Threading;        

        

        /// <summary>
        /// Administrador de históricos de erros.
        /// </summary>
        public class ErrorManager : IDisposable
        {

            #region Manutenção
            /// <summary>
            /// Mantém o status de chamadas à destruição.
            /// </summary>
            private bool disposedValue;
            /// <summary>
            /// Mantém o bloqueador de arquivos entre threads.
            /// </summary>
            private ReaderWriterLockSlim locker { get; set; } = new ReaderWriterLockSlim();
            /// <summary>
            /// Mantém a excessão a ser salva no log.
            /// </summary>
            private ErrorBlock ActiveException { get; set; } = null;
            #endregion

            #region Instância
            /// <summary>
            /// Construtor padrão.
            /// </summary>
            public ErrorManager()
            {
                disposedValue = false;
                locker = new ReaderWriterLockSlim();
            }

            /// <summary>
            /// Construtor com a <see cref="Exception"/> a ser salva.
            /// </summary>
            /// <param name="ex">Objeto do tipo <see cref="Exception"/>.</param>
            internal ErrorManager(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            {
                disposedValue = false;
                locker = new ReaderWriterLockSlim();
                ActiveException = new ErrorBlock()
                {
                    Caller = memberName,
                    FileName = sourceFilePath,
                    DetectionDate = DateTime.Now,
                    ErrorMessage = ex.Message,
                    JSonError = JsonConvert.SerializeObject(ex),
                    LineNumer = sourceLineNumber
                };
            }

            /// <summary>
            /// Construtor com a <see cref="Exception"/> a ser salva.
            /// </summary>
            /// <param name="errorMessge">Texto com a mensagem de erro.</param>
            internal ErrorManager(string errorMessge, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            {
                disposedValue = false;
                locker = new ReaderWriterLockSlim();
                Exception ex = new Exception(errorMessge);
                ActiveException = new ErrorBlock()
                {
                    Caller = memberName,
                    FileName = sourceFilePath,
                    DetectionDate = DateTime.Now,
                    ErrorMessage = ex.Message,
                    JSonError = JsonConvert.SerializeObject(ex),
                    LineNumer = sourceLineNumber
                };
            }

            /// <summary>
            /// Construtor que recebe um <see cref="ErrorBlock"/> como parâmetro.
            /// </summary>
            /// <param name="eb">Objeto do tipo <see cref="ErrorBlock"/>.</param>
            internal ErrorManager(ErrorBlock eb)
            {
                disposedValue = false;
                locker = new ReaderWriterLockSlim();
                ActiveException = eb;
            }

            /// <summary>
            /// Destruidor efetivo.
            /// </summary>
            /// <param name="disposing"></param>
            protected virtual void Dispose(bool disposing)
            { if (!disposedValue) { if (disposing) { CleanUp(); } disposedValue = true; } }

            /// <summary>
            /// Equivalente ao Finalize do VB.
            /// </summary>
            ~ErrorManager() { Dispose(false); }

            /// <summary>
            /// Limpesa da memória.
            /// </summary>
            private void CleanUp() { try { if (locker != null) locker.Dispose(); locker = null; } catch { } }

            /// <summary>
            /// Destruidor padrão.
            /// </summary>
            public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
            #endregion

            #region Propriedades internas
            /// <summary>
            /// Pasta para gravação dos arquivos de log.
            /// </summary>
            private string Folder
            {
                get
                {
                    string ret = "";
                    ret = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorLog");
                    if (!Directory.Exists(ret)) Directory.CreateDirectory(ret);
                    return ret;
                }
            }

            /// <summary>
            /// Caminho completo do arquivo de log deste dia.
            /// </summary>
            private string CurrentFile
            {
                get
                {
                    DateTime dt = DateTime.Now;
                    return Path.Combine(Folder,
                        string.Format("Log_{0}{1}{2}.log",
                        dt.Year.ToString().PadLeft(4, '0'),
                        dt.Month.ToString().PadLeft(2, '0'),
                        dt.Day.ToString().PadLeft(2, '0')));
                }
            }
            #endregion

            /// <summary>
            /// Retorna a lista de erros detectados no intervalo definido.
            /// </summary>
            /// <param name="startDate"></param>
            /// <param name="endDate"></param>
            /// <returns>Lista de <see cref="ErrorBlock"/>.</returns>
            internal List<ErrorBlock> GetErrors(DateTime startDate, DateTime endDate)
            {
                List<ErrorBlock> ret = new List<ErrorBlock>();
                FileInfo[] arquivos = (new DirectoryInfo(Folder)).GetFiles("*.log");
                foreach (FileInfo fi in arquivos)
                    if (fi.IsBetween(startDate, endDate, true))
                        ret.AddRange(JsonConvert.DeserializeObject<List<ErrorBlock>>(File.ReadAllText(fi.FullName)));
                return ret;
            }

            /// <summary>
            /// Retorna a lista de erros detectados no intervalo definido.
            /// </summary>
            /// <param name="startDate"></param>
            /// <param name="endDate"></param>
            /// <returns>Lista de <see cref="ErrorBlock"/>.</returns>
            public static List<ErrorBlock> ErrorList(DateTime startDate, DateTime endDate)
            {
                List<ErrorBlock> ret = null;
                using (ErrorManager em = new ErrorManager())
                    ret = em.GetErrors(startDate, endDate);
                return ret;
            }
            /// <summary>
            /// Salva a mensagem de erro.
            /// </summary>
            internal void Save() { Append(JsonConvert.SerializeObject(ActiveException)); }

            /// <summary>
            /// Escreve a linha enviada no arquivo de log.
            /// </summary>
            /// <param name="line"></param>
            internal void Append(string line)
            {

                try
                {
                    locker.EnterWriteLock();
                    using (StreamWriter sw = new StreamWriter(path: CurrentFile,
                                                              append: true,
                                                              encoding: Encoding.Unicode))
                        sw.WriteLine(line);
                }
                catch { }
                finally { locker.ExitWriteLock(); }
            }
        }
    }
}