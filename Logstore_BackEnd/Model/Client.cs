using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Logstore_BackEnd.Model
{
    public class Client : BaseEntity
    {     
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

    }
}
