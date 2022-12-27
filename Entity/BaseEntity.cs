using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int State { get; set; }
    }
}
