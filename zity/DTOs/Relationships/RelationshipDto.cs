using System;
using System.Collections.Generic;
using zity.DTOs.Users;

namespace zity.DTOs.Relationships
{
    public class RelationshipDto
    {
        public int Id { get; set; }
        public string Role { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UserId { get; set; }
        public string ApartmentId { get; set; } = string.Empty;

        public virtual UserDTO? User { get; set; } = null;
       
    }
}
