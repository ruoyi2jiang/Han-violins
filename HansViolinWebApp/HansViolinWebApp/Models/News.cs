using System;
using System.ComponentModel.DataAnnotations;

namespace HansViolinWebApp.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsPublished { get; set; }
    }
}
