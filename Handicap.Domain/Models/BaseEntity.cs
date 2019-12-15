using System;

namespace Handicap.Domain.Models{
    public class BaseEntity{
        public string Id { get; set; }
        public string TenantId { get; set; }
    }
}