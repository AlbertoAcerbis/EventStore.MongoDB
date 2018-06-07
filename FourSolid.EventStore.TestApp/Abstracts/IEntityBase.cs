using System;

namespace FourSolid.EventStore.TestApp.Abstracts
{
    public interface IEntityBase
    {
        Guid Id { get; }
    }
}