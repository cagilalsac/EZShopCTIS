#nullable disable

using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class CategoryModel
    {
        public Category Record { get; set; }

        [DisplayName("Category Name")]
        public string Name => Record.Name;

        public string Description => Record.Description;
    }
}
