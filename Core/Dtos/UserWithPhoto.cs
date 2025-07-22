using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Dtos
{
    public class UserWithPhoto
    {
        public required string Name { get; set; }
        public int? Age { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Role Role { get; set; }
        public List<byte[]?> Pictures { get; set; } = [];
    }
}
