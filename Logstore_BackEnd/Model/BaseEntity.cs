using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Logstore_BackEnd.Model
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
