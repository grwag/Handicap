using System;

namespace Handicap.Application.Entities{
    public class BaseEntity{
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}