using DnxConsole.Domain.Common;
using DnxConsole.Domain.Common.Contracts;

namespace DnxConsole.Domain.OrderEditContext
{
    public class Cms : Common.Cms
    {
        private readonly IOrderEditRepository _repository;

        public Cms(int id, ILogEvents logger, ISendExternalMessenges messenger, IOrderEditRepository repository) : base(id, logger, messenger)
        {
            _repository = repository;
        }

        public void EditOrderAddress(Address newAddress)
        {
            var order = _repository.GetOrder(base.Id);
            order.UpdateAddress(newAddress);
        }
    }
}
