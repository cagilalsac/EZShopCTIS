#nullable disable

using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class ProductModel
    {
        public Product Record { get; set; }

        [DisplayName("Product Name")]
        public string Name => Record.Name;

        [DisplayName("Unit Price")]
        public string UnitPrice => Record.UnitPrice.ToString("C2");

        [DisplayName("Stock Amount")]
        public int StockAmount => Record.StockAmount ?? 0;

        [DisplayName("Expiration Date")]
        public string ExpirationDate => Record.ExpirationDate.HasValue ? Record.ExpirationDate.Value.ToString("MM/dd/yyyy") : string.Empty;

        public string Category => Record.Category.Name;

    }
}
