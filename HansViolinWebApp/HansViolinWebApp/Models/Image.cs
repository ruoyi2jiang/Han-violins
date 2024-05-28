using System.ComponentModel.DataAnnotations;


namespace HansViolinWebApp.Models
{
    public class Image
    {
        public int ImageId { get; set; }

        public int? ItemId { get; set; }
        
        [Required(ErrorMessage = "Image link is required")]
        [StringLength(1024)]
        public string Url { get; set; }

        public int SequenceNumber { get; set; }

        public virtual Item Item { get; set; }
    }
}
