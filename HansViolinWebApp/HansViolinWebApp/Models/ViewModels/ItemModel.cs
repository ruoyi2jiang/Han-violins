using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HansViolinWebApp.Models
{
    public partial class ItemModel
    {
        public int ItemId { get; set; }

        public int? CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string ItemName { get; set; }

        public string Description { get; set; }

        public string PriceRange { get; set; }

        public int? CoverId { get; set; }

        public string CoverUrl { get; set; }

        public string OriginInfo { get; set; }

        public string SoldInfo { get; set; }

        public string Status { get; set; }

        public DateTime? DateAdded { get; set; }

    }
}
