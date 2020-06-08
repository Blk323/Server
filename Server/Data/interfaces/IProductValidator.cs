using Server.Data.Models;

namespace Server.Data.interfaces
{
    public interface IProductValidator
    {
        public bool ValidateTitle(Product product = null);

        public bool ValidateId(string id = null);

        public bool ValidatePrice(Product product = null);
    }
}
