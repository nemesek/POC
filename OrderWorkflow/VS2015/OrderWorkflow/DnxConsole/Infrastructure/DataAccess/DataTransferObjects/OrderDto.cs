using DnxConsole.Domain.Common;

namespace DnxConsole.Infrastructure.DataAccess.DataTransferObjects
{
    public abstract class OrderDto
    {
        
        public string VendorPk { get; set; }
        
        public OrderStatus Status { get; set; }
        public string ZipCode { get; set; }
        
        public int CmsId { get; set; }
    }
}
