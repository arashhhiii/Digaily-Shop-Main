using Data.Class.entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Class.entities.Account
{
    public class User : BaseEntity
    {
        
        public string? FullName{
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

        public int RoleId { get; set; } = 2;
        public Role Role { get; set; } = null!;

    }
}
