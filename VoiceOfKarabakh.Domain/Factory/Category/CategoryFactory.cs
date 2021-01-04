using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceOfKarabakh.Domain.Factory.Category
{
    public class CategoryFactory
    {
        public Models.Category GetCategory()
        {
            return new Models.Category();
        }

        public Models.Category GetCategory(Models.LocalizationSet localizationSet)
        {
            var category = GetCategory();
            category.TitleLocalizationSet = localizationSet;
            return category;
        }
    }
}
