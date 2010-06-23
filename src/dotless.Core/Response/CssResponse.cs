namespace dotless.Core.Response
{
    using System.Web;
    using Abstractions;

    public class CssResponse : IResponse
    {
        private readonly IHttp _http;

        public CssResponse(IHttp http)
        {
            _http = http;
        }

        public void WriteCss(string css)
        {
            var response = _http.Context.Response;
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.ContentType = "text/css";
            response.Write(css);
//            response.End();
        }
    }
}