using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VoiceOfKarabakh.Application.ViewModels.Category;
using VoiceOfKarabakh.Domain.Models;

namespace VoiceOfKarabakh.Application.Interfaces.Category
{
    public interface ICategoryService
    {
        IEnumerable<CategoryViewModel> GetAllCategories(
            string cultureCode, string includes = null, Expression<Func<Domain.Models.Category, bool>>[] filters = null);

        IEnumerable<CategoryWithAllTranslationsViewModel> GetAllCategoriesWithTranslations(
            string includes = null, Expression<Func<Domain.Models.Category, bool>>[] filters = null);

        CategoryViewModel GetCategory(int id, string cultureCode, string includes = null);

        CategoryWithAllTranslationsViewModel GetCategoryWithAllTranslations(int id, string includes = null);

        void AddNewCategory(NewCategoryViewModel newCategoryViewModel);

        void UpdateCategory(EditCategoryViewModel editCategoryViewModel);

        void DeleteCategory(int id);

        void Save();
    }
}
