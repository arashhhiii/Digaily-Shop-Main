using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Class.DTOs.Account
{
    public class UserDetailDTOs
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
        public string? FullName
        {
            get;
            set;
        }
        public string MobileNumber
        {
            get;
            set;
        }
        public string MobileActivationNumber
        {
            get;
            set;
        }
        public string? Email
        {
            get;
            set;
        }
        public string? Adress
        {
            get;
            set;
        }
        public string? postCode
        {
            get;
            set;
        }

    }
}
