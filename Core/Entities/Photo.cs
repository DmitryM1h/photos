using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.entities
{
    public class Photo
    {
        public int Id { get; set; }
        public byte[]? Picture { get; set; }
        public int PublisherId { get; set; }
        public required User Publisher { get; set; }
    }
}
