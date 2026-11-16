using Data.Class.entities.Account;
using Data.Class.entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Class.entities.ProductEntities
{
    public class ProductComment:BaseEntity
    {
        public long ProductId {  get; set; }
        public long UserId {  get; set; }
        public CommmentState CommmentState { get; set; }
        [Range(1,5)]
        public int Rate { get; set; }
        public string Description { get; set; }
        public Product Product { get; set; }
        public User User {  get; set; }

        
    }
    public enum CommmentState
    {
        submitted,
        Rejected,
        Accepted
    }
}
