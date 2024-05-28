
using System.Collections.Generic;

namespace HansViolinWebApp.Models.Mapper
{
    public class CategoryMapper
    {
        public CategoryModel map(Category category, string language)
        {
            var model = new CategoryModel();
            model.CategoryId = category.CategoryId;
            model.PathName = category.PathName;
            model.CoverUrl = category.CoverUrl;
            if(language=="zh")
            {
                model.CategoryName = category.CategoryNameZh;
                model.Description = category.DescriptionZh;
            }
            else
            {
                model.CategoryName = category.CategoryName;
                model.Description = category.Description;
            }
            return model;
        }

        public List<CategoryModel> map(List<Category> categories, string language)
        {
            var models = new List<CategoryModel>();
            for(int i = 0; i< categories.Count; i++)
            {
                models.Add(map(categories[i], language));
            }
            return models;
        }
    }
}
