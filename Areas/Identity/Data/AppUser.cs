using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LoginPage.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AppUser class
    public class AppUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [PersonalData]
        public Address Address { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Education { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(255)")]
        public string Hobbies { get; set; }

        public AppUser()
        {
            Address = new Address();
        }
    }

    public class Address
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Miejscowość")]
        public string City { get; set; }

        [Column(TypeName = "nvarchar(6)")]
        [Display(Name = "Kod pocztowy")]
        public string ZipCode { get; set; }

        public ICollection<AppUser> Users { get; set; }

        public Address()
        {
            Users = new List<AppUser>();
        }

    }
}
