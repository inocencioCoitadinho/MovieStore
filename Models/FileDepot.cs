using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class FileDepot
    {
        [Key]
        public Guid FileId { get; set; }
        public string Name { get; set; }
        public byte[] File { get; set; }
    }
}
