using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Http.Results;

public class ApiRetorno : IDisposable
{
    public string delay { get; set; }
    public int action { get; set; }
    public List<object> result { get; set; }
    public List<object> relatedItems { get; set; }
    public List<Error> errors { get; set; }
    public bool success { get; set; }
    public int affected { get; set; }
    public string type { get; set; }
    public string mensagem { get; set; }
    public Origin origin { get;  set; }

    public class Error
    {
        public string propertyName { get; set; }
        public string message { get; set; }
    }

    #region IDisposable Support
    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing == true) { }
        }

        disposedValue = true;
    }

    ~ApiRetorno() { }
    public ApiRetorno()
    {
        disposedValue = false;
    }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}

public class Origin
{
    public int Id { get; set; }
}