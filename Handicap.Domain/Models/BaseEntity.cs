using System;

namespace Handicap.Domain.Models{
    public class BaseEntity{
        public Guid Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}