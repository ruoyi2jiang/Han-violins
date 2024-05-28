using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HansViolinWebApp.Models
{
    public partial class Category
    {
        public int CategoryId { get; set;}

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(20)]
        public string CategoryName { get; set; }
        public string CategoryNameZh { get; set; }

        [Required(ErrorMessage = "Path name is required")]
        [StringLength(20)]
        public string PathName { get; set; }
        public string Description { get; set; }
        public string DescriptionZh { get; set; }
        public string CoverUrl { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public Category()
        {
            Items = new HashSet<Item>();
        }
    }
}
