namespace Generics.Dal
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Para manter log de ações junto ao banco de dados.
    /// </summary>
    public class ActionBlock
    {
        public string Caller { get; internal set; }
        public string FileName { get; internal set; }
        public DateTime ExecutionDate { get; internal set; }
        public string Message { get; internal set; }
        public int LineNumer { get; internal set; }
    }
}
