#nullable disable

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BLL.Services.Bases
{
    public abstract class HttpServiceBase
    {
        const string SESSIONKEY = "SESSIONKEY";

        protected readonly HttpContextAccessor _httpContextAccessor;

        protected HttpServiceBase(HttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual T GetSession<T>() where T : class, new()
        {
            T instance = null;
            string json = _httpContextAccessor.HttpContext.Session.GetString(SESSIONKEY);
            if (!string.IsNullOrWhiteSpace(json))
                instance = JsonConvert.DeserializeObject<T>(json);
            return instance;
        }

        public virtual void SetSession<T>(T instance) where T : class, new()
        {
            string json = JsonConvert.SerializeObject(instance);
            _httpContextAccessor.HttpContext.Session.SetString(SESSIONKEY, json);
        }
    }

    public class HttpService : HttpServiceBase
    {
        public HttpService(HttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}
