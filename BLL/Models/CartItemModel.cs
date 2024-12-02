#nullable disable

using System.ComponentModel;

namespace BLL.Models
{
    public class CartItemModel
    {
        public int ProductId { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [DisplayName("Product Unit Price")]
        public string ProductUnitPrice { get; set; }

        public int UserId { get; set; }
    }
}
