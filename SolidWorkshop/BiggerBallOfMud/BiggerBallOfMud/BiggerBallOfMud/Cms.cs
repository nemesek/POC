using System;

namespace BiggerBallOfMud
{
    public class Cms
    {
        private int _id;

        public Cms(int id)
        {
            _id = id;
        }

        public Order CreateNewOrder()
        {
            var order = new Order { OrderId = Guid.NewGuid(), Status = OrderStatus.Unassigned, ClientId = _id, ZipCode = "38655" };
            return order;
        }
    }
}
