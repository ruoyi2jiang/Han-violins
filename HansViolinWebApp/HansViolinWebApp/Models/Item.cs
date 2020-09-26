using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HansViolinWebApp.Models
{
    public partial class Item
    {
        public int ItemId { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(20)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(80)]
        public string ItemName { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        [StringLength(50)]
        public string PriceRange { get; set; }

        public int? CoverId { get; set; }

        [StringLength(50)]
        public string OriginInfo { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(15)]
        public string Status { get; set; }

        public DateTime? DateAdded { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public Item()
        {
            Images = new HashSet<Image>();
        }
    }

    public enum Status
    {
        DRAFT = 1,
        ACTIVE = 2,
        NOTABLE_SALE = 3
    }
}
