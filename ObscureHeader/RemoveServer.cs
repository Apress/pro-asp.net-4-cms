using System;
using System.Web;

namespace ObscureHeader
{
    /// <summary>
    /// Removes the "Server" header from the HTTP response.
    /// If the CMS AppPool is running in Integrated Mode, this will run for all requests.
    /// </summary>
    public class RemoveServer : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += RemoveServerFromHeaders;
        }

        private void RemoveServerFromHeaders(object sender, EventArgs e)
        {
            // strip the "Server" header from the current Response in IIS7
            try
            {
                HttpResponse r = HttpContext.Current.Response;
                r.Headers.Remove("Server");
            }
            catch
            {
                // swallow this exception while running locally
            }
        }

        public void Dispose()
        {
            // no code necessary
        }
    }
}
