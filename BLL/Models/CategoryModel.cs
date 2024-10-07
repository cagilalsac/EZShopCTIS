#nullable disable

using BLL.DAL;

namespace BLL.Models
{
    public class CategoryModel
    {
        public Category Record { get; set; }

        public string Name => Record.Name;

        public string Description => Record.Description;
    }
}
