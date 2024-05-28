using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HansViolinWebApp.Models.Mapper
{
    public class ItemMapper
    {
        public ItemModel map(Item item, string language)
        {
            var model = new ItemModel();
            model.CategoryId = item.CategoryId;
            model.ItemId = item.ItemId;
            model.PriceRange = item.PriceRange;
            model.CoverId = item.CoverId;
            model.Status = item.Status;
            model.DateAdded = item.DateAdded;
            model.CategoryName = item.CategoryName;
            if (language == "zh")
            {
                model.ItemName = item.ItemNameZh;
                model.Description = item.DescriptionZh;
                model.OriginInfo = item.OriginInfoZh;
            }
            else
            {
                model.ItemName = item.ItemName;
                model.Description = item.Description;
                model.OriginInfo = item.OriginInfo;
            }
            return model;
        }

        public List<ItemModel> map(List<Item> items, string language)
        {
            var models = new List<ItemModel>();
            for (int i = 0; i < items.Count; i++)
            {
                models.Add(map(items[i], language));
            }
            return models;
        }
    }
}
