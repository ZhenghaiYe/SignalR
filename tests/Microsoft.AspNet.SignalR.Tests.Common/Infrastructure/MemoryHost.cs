using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Testing;
using Owin;

namespace Microsoft.AspNet.SignalR.Hosting.Memory
{
    public class MemoryHost : DefaultHttpClient, IDisposable
    {
        private static int instanceId;
        private TestServer _host;

        public string InstanceName { get; set; }        

        public MemoryHost()
        {
            var id = Interlocked.Increment(ref instanceId);
            InstanceName = Process.GetCurrentProcess().ProcessName + id;
        }

        public void Configure(Action<IAppBuilder> startup)
        {
            _host = TestServer.Create(startup);
        }


        protected override HttpMessageHandler CreateHandler()
        {
            return _host.Handler;
        }

        public void Dispose()
        {
            _host.Close();
        }        
    }
}
