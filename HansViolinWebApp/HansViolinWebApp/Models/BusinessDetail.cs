using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HansViolinWebApp.Models
{
    public class BusinessDetail
    {
        public int Id { get; set; }

        public string About { get; set; }
        public string AboutZh { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(10)]
        public string Postcode { get; set; }

        [StringLength(255)]
        public string ImageLink { get; set; }

        [StringLength(255)]
        public string Twitter { get; set; }

        [StringLength(255)]
        public string Facebook { get; set; }

        [StringLength(255)]
        public string Instagram { get; set; }

        [StringLength(255)]
        public string Wechat { get; set; }
    }
}
