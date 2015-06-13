using System;
using DnxConsole.Domain.Common.Utilities;

namespace DnxConsole.Domain.OrderCreationContext
{
    public class Loan
    {
        private readonly int _id;

        public Loan(int id)
        {
            _id = id;
        }
        public int LoanId => _id;

        public void AddOrder(Order order)
        {
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkCyan, ConsoleColor.White,
                $"Adding order {order.Id} with service id {order.ServiceId} to loan number {_id}");
        }
    }
}
