using Server.Data.Models;

namespace Server.Data.interfaces
{
    public interface IOrderValidator
    {
        public bool ValidateName(Order order = null, string name = null);

        public bool ValidateId(string id = null);

        public bool ValidateProductId(Order order = null);

        public bool ValidateCount(Order order = null);

    }
}
