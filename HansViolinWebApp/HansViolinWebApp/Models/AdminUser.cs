using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HansViolinWebApp.Models
{
    public class AdminUser
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string username { get; set; }
        [Required]
        [StringLength(255)]
        public string password { get; set; }
        [Required]
        [StringLength(50)]
        public string Role { get; set; }
    }
}
