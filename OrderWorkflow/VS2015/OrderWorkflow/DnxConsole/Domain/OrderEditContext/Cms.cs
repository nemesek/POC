using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;

namespace DnxConsole.Domain.OrderEditContext
{
    public class Cms : Common.Cms
    {
        public Cms(int id, ILogEvents logger, ISendExternalMessenges messenger) : base(id, logger, messenger)
        {
        }

        public void EditOrderAddress(Address newAddress)
        {
            var order = OrderEditRepository.GetOrder(base.Id);
            order.UpdateAddress(newAddress);
        }
    }
}
