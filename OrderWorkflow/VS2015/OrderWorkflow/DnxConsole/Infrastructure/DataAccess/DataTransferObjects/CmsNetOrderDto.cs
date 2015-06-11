using System;

namespace DnxConsole.Infrastructure.DataAccess.DataTransferObjects
{
    public class CmsNetOrderDto : OrderDto
    {
        public Guid DocId { get; set; }
    }
}
