using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace adad.Models
{
    //this class was created to add feilds to the identity user table.
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "longtext")]
        public int Id {  get; set; }
        [Column(TypeName = "longtext")]
        public string? ScreenName { get; set; } = "";
        [Column(TypeName = "longtext")]
        public string? FirstName { get; set; } = "";
        [Column(TypeName = "longtext")]
        public string? LastName { get; set; } = "";
        [Column(TypeName = "longtext")]
        public string? Email { get; set; } = "";
        [Column(TypeName = "longtext")]
        public string? SMS { get; set; } = "";
        [Column(TypeName = "longtext")]
        public string? Phone { get; set; } = "";



    }
}
