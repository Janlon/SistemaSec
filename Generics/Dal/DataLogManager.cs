namespace Generics.Dal
{
    using Generics.Extensoes;
    using Generics.Helpers.Errors;
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
    internal class DataLogManager : IDisposable
    {

        #region Manutenção
        /// <summary>
        /// Mantém o status de chamadas à destruição.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Mantém o bloqueador de arquivos entre threads.
        /// </summary>
        private static ReaderWriterLockSlim locker { get; set; } = new ReaderWriterLockSlim();
        #endregion

        #region Propriedades públicas
        /// <summary>
        /// Ação corrente.
        /// </summary>
        public ActionBlock ActiveAction { get; internal set; }
        #endregion

        #region Instância
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public DataLogManager()
        {
            disposedValue = false;
            locker = new ReaderWriterLockSlim();
        }

        /// <summary>
        /// Construtor com a <see cref="Exception"/> a ser salva.
        /// </summary>
        /// <param name="ex">Objeto do tipo <see cref="Exception"/>.</param>
        internal DataLogManager(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            disposedValue = false;
            locker = new ReaderWriterLockSlim();
            ActiveAction = new ActionBlock()
            {
                Caller = memberName,
                FileName = sourceFilePath,
                ExecutionDate = DateTime.Now,
                Message = message,
                LineNumer = sourceLineNumber
            };
        }

        /// <summary>
        /// Construtor que recebe um <see cref="ErrorBlock"/> como parâmetro.
        /// </summary>
        /// <param name="eb">Objeto do tipo <see cref="ErrorBlock"/>.</param>
        internal DataLogManager(ActionBlock eb)
        {
            disposedValue = false;
            locker = new ReaderWriterLockSlim();
            ActiveAction = eb;
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
        ~DataLogManager() { Dispose(false); }

        /// <summary>
        /// Limpesa da memória.
        /// </summary>
        private void CleanUp() { try { if (locker != null) locker = null; } catch { } }

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
                ret = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataLog");
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

        #region Métodos públicos
        /// <summary>
        /// Retorna a lista de erros detectados no intervalo definido.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Lista de <see cref="ErrorBlock"/>.</returns>
        public static List<ActionBlock> ActionsList(DateTime startDate, DateTime endDate)
        {
            List<ActionBlock> ret = null;
            using (DataLogManager em = new DataLogManager())
                ret = em.GetActions(startDate, endDate);
            return ret;
        }
        #endregion

        #region Métodos de suporte
        /// <summary>
        /// Retorna a lista de erros detectados no intervalo definido.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Lista de <see cref="ErrorBlock"/>.</returns>
        internal List<ActionBlock> GetActions(DateTime startDate, DateTime endDate)
        {
            List<ActionBlock> ret = new List<ActionBlock>();
            FileInfo[] arquivos = (new DirectoryInfo(Folder)).GetFiles("*.log");
            foreach (FileInfo fi in arquivos)
                if (fi.IsBetween(startDate, endDate, true))
                    ret.AddRange(JsonConvert.DeserializeObject<List<ActionBlock>>(File.ReadAllText(fi.FullName)));
            return ret;
        }
        /// <summary>
        /// Salva a mensagem de erro.
        /// </summary>
        internal void Save()
        {
            Append(JsonConvert.SerializeObject(ActiveAction));
        }

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
        #endregion
    }
}
