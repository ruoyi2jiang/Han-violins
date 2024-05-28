using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HansViolinWebApp.Models
{
    public partial class CategoryModel
    {
        public int CategoryId { get; set;}

        public string CategoryName { get; set; }

        public string PathName { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }

    }
}
