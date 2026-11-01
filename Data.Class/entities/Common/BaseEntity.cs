using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Class.entities.Common
{
    public class BaseEntity
    {
        [Key]
        public long id
        {
            get;
            set;
        }
        public DateTime CreatedTime
        {
            get;
            set;
        }
        public DateTime UpdatedTime
        {
            get;
            set;
        }
        public bool isDelete
        {
            get;
            set;
        }
    }
}
