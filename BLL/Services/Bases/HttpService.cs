#nullable disable

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BLL.Services.Bases
{
    public abstract class HttpServiceBase
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected HttpServiceBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual T GetSession<T>(string key) where T : class, new()
        {
            T instance = null;
            string json = _httpContextAccessor.HttpContext.Session.GetString(key);
            if (!string.IsNullOrWhiteSpace(json))
                instance = JsonConvert.DeserializeObject<T>(json);
            return instance;
        }

        public virtual void SetSession<T>(string key, T instance) where T : class, new()
        {
            string json = JsonConvert.SerializeObject(instance);
            _httpContextAccessor.HttpContext.Session.SetString(key, json);
        }

        public virtual void RemoveSession(string key)
        {
            _httpContextAccessor.HttpContext.Session.Remove(key);
        }

        public virtual void ClearSession()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }
    }

    public class HttpService : HttpServiceBase
    {
        public HttpService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}
