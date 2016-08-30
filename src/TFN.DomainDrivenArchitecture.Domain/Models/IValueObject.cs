using System;

namespace TFN.DomainDrivenArchitecture.Domain.Models
{
    public interface IValueObject<T> : IEquatable<T>
    { }
}