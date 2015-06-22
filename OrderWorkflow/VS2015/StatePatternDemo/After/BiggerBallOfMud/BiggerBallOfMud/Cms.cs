using System;

namespace BiggerBallOfMud
{
    public class Cms
    {
        private readonly int _id;

        public Cms(int id)
        {
            _id = id;
        }

        public Order CreateNewOrder()
        {
            var order = new Order { OrderId = Guid.NewGuid(), ClientId = _id, ZipCode = "38655", ServiceId = 1 };
            return order;
        }
    }
}
