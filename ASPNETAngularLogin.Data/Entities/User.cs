using ASPNETAngularLogin.Data.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNETAngularLogin.Data.Entities
{
    public class User : IdentityUser<int>, IBaseEntity
    {

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
