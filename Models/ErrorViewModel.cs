using System.ComponentModel.DataAnnotations.Schema;

namespace adad.Models
{
    public class ErrorViewModel
    {
        [NotMapped]
        public string? RequestId { get; set; }

        [NotMapped]
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}