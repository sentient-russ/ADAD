using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace adad.Models
{
    public class ViewModelBundle
    {
        [NotMapped]
        public string? FirstName { get; set; } = string.Empty;

        [NotMapped]
        public string? LastName { get; set; } = string.Empty;

        [NotMapped]
        public string? Email { get; set; } = string.Empty;

        [NotMapped]
        //public string[][] sites {  get; set; }
        public List<SiteModel> sites { get; set; }

    }
}
