using System;

namespace DnxConsole.Infrastructure.DataAccess.DataTransferObjects
{
    public class CmsNextOrderDto : OrderDto
    {
        public Guid OrderId { get; set; }
    }
}
