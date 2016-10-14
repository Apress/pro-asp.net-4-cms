using System;

namespace Business.Operations
{
    public class Rewrite
    {
        /// <summary>
        /// Checks incoming request against a list of excluded types
        /// </summary>
        /// <param name="request">the incoming request</param>
        /// <returns>true if the current request should be excluded</returns>
        public bool IsExcludedRequest(string request)
        {
            if (request.Contains(".css") ||
                request.Contains(".js") ||
                request.Contains(".png") ||
                request.Contains(".gif") ||
                request.Contains(".bmp") ||
                request.Contains(".jpg") ||
                request.Contains(".jpeg") ||
                request.Contains(".mov") ||
                request.Contains(".ashx") ||
                request.Contains(".asmx") ||
                request.Contains(".axd") ||
                request.Contains("/admin/") ||
                request.Contains("login.aspx"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Attempts to pull up a content ID for an incoming friendly URL
        /// </summary>
        /// <param name="request">the incoming request</param>
        /// <param name="host">the host for this request (i.e. "localhost")</param>
        /// <returns>a nullable Guid for the content ID if available</returns>
        public Guid? GetIDByPrimaryUrl(string request, string host)
        {
            var data = new Data.RewriteRepository();
            return data.GetIDByPrimaryUrl(request, host);
        }

        /// <summary>
        /// Attempts to pull up a friendly URL for a content ID
        /// </summary>
        /// <param name="contentID">the ID of the content</param>
        /// <param name="host">the host for this request (i.e. "localhost")</param>
        /// <returns>the primary URL for this content ID</returns>
        public string GetPrimaryUrlByID(Guid contentID, string host)
        {
            var data = new Data.RewriteRepository();
            return data.GetPrimaryUrlByID(contentID, host);
        }

        /// <summary>
        ///  Attempts to pull up a primary URL based on an incoming alias
        /// </summary>
        /// <param name="request">the incoming request</param>
        /// <param name="host">the host for this request (i.e. "localhost")</param>
        /// <returns>a primary URL string, if available</returns>
        public string GetPrimaryUrlByAlias(string request, string host)
        {
            var data = new Data.RewriteRepository();
            return data.GetPrimaryUrlByAlias(request, host);
        }
    }
}