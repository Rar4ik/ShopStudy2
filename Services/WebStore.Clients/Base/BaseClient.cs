using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected readonly HttpClient _Client;
        protected readonly string _ServiceAdress;

        protected BaseClient(IConfiguration config, string ServiceAdress)
        {
            _ServiceAdress = ServiceAdress;
            _Client = new HttpClient()
            {
                BaseAddress = new Uri(config["WebApiUrl"])
            };
            var header = _Client.DefaultRequestHeaders.Accept;
            header.Clear();
            header.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Disposing
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _Disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed || !disposing) return;
            _Disposed = true;
            _Client.Dispose();
        }
        #endregion
    }
}
