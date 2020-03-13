namespace Generics.Dados
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class DbResponse<T> where T : class
    {
        private Stopwatch sw;
        public DbResponse() { sw = new Stopwatch(); }
        internal DbResponse(DbAction action) { sw = new Stopwatch(); RequestAction = action; sw.Start(); }
        public bool Success { get { return (ErrorList.Count == 0); } }
        public DbAction RequestAction { get; internal set; } = DbAction.Insert;
        public TimeSpan Delay { get; internal set; }
        public Dictionary<string, string> ErrorList { get; internal set; } = new Dictionary<string, string>();
        public IEnumerable<T> Result { get; internal set; } = null;
        public int Affected
        {
            get
            {
                try { return Result.Count(); } catch { return (Result == null) ? 0 : 1; }
            }
        }
        internal void AddError(string propertyName, string errorMessage)
        {
            if (ErrorList.ContainsKey(propertyName)) ErrorList.Remove(propertyName);
            ErrorList.Add(propertyName, errorMessage);
        }
        internal void RemoveError(string propertyName) { if (ErrorList.ContainsKey(propertyName)) ErrorList.Remove(propertyName); }
        internal void AddResult(T newResult, bool finished = false)
        {
            Result = (new List<T>() { newResult }).AsEnumerable();
            if (finished) Finish();
        }
        internal void AddResult(IEnumerable<T> newResult, bool finished = false)
        {
            Result = newResult.AsEnumerable();
            if (finished) Finish();
        }
        internal void AddResult(List<T> newResult, bool finished = false)
        {
            Result = newResult.AsEnumerable();
            if (finished) Finish();
        }
        internal void ClearResult() { Result = null; }
        internal void Finish()
        {
            sw.Stop();
            Delay = sw.Elapsed;
        }
    }
}
