using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace BillingSoftware.Core.Entities
{
    [Index(nameof(FileName), IsUnique = true)]
    public class BSFile : EntityObject
    {
        [Required]
        public string FileName { get; set; }
        [Required]
        public byte[] Bytes { get; set; }
        [Required]
        public string ContentType { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
    }
}
