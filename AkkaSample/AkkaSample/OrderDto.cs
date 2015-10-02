using System;

namespace AkkaSample
{
    public class OrderDto
    {
        private readonly string _portId;
        private readonly int _orderId;
        private readonly string _zip;

        public OrderDto(int orderId, string zip, string portId)
        {
            _orderId = orderId;
            _zip = zip;
            _portId = portId;
        }

        public int OrderId => _orderId;
        public string Zip => _zip;
        public string PortId => _portId;
    }
}
