﻿namespace DnxConsole.Domain.Contracts
{
    public interface IProcessAutoAssign
    {
        Vendor FindBestVendor(ICanBeAutoAssigned order);
    }
}
