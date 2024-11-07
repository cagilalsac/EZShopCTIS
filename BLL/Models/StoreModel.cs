#nullable disable

using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class StoreModel
    {
        public Store Record { get; set; }

        public string Name => Record.Name;

        [DisplayName("Virtual")]
        public string IsVirtual => Record.IsVirtual ? "Yes" : "No";

        [DisplayName("Country / City")]
        public string CountryAndCity => Record.Country?.Name + " " + Record.City?.Name;
    }
}
