using System;

namespace Wimt.DomainDrivenArchitecture.Domain.Models
{
    public interface IValueObject<T> : IEquatable<T>
    { }
}