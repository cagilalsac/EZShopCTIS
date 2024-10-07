using BLL.DAL;

namespace BLL.Services.Bases
{
    public abstract class Service
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty; // ""

        protected readonly Db _db;

        protected Service(Db db) // Dependency Injection, Constructor Injection
        {
            _db = db;
        }

        public Service Success(string message = "")
        {
            IsSuccessful = true;
            Message = message;
            return this;
        }

        public Service Error(string message = "")
        {
            IsSuccessful = false;
            Message = message;
            return this;
        }
    }
}
