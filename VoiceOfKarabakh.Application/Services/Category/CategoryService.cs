using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VoiceOfKarabakh.Application.Interfaces.Category;
using VoiceOfKarabakh.Application.ViewModels.Category;
using VoiceOfKarabakh.Application.ViewModels.Localization;
using VoiceOfKarabakh.Domain.Interfaces.Category;
using VoiceOfKarabakh.Domain.Interfaces.Localization;
using VoiceOfKarabakh.Domain.Interfaces.LocalizationSet;

namespace VoiceOfKarabakh.Application.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private Domain.Factory.Category.CategoryFactory _categoryFactory;
        private Domain.Factory.LocalizationSet.LocalizationSetFactory _localizationSetFactory;
        private Domain.Factory.Localization.LocalizationFactory _localizationFactory;

        public CategoryService(ICategoryRepository categoryRepository, Domain.Factory.Category.CategoryFactory categoryFactory, Domain.Factory.Localization.LocalizationFactory localizationFactory, Domain.Factory.LocalizationSet.LocalizationSetFactory localizationSetFactory)
        {
            _categoryRepository = categoryRepository;

            _categoryFactory = categoryFactory;
            _localizationFactory = localizationFactory;
            _localizationSetFactory = localizationSetFactory;
        }

        public void AddNewCategory(NewCategoryViewModel newCategoryViewModel)
        {
            Domain.Models.LocalizationSet titleLocalizationSet = _localizationSetFactory.GetLocalizationSet();

            foreach (var translation in newCategoryViewModel.CategoryTitleTranslations)
            {
                if (translation.Value == null)
                    translation.Value = "";

                var localization = _localizationFactory.GetLocalization(
                    translation.CultureCode, translation.Value, titleLocalizationSet);

                titleLocalizationSet.Localizations.Add(localization);
            }

            Domain.Models.Category newCategory = _categoryFactory.GetCategory(titleLocalizationSet);
            _categoryRepository.Add(newCategory);
        }

        public void DeleteCategory(int id)
        {
            _categoryRepository.Delete(id);
        }

        public IEnumerable<CategoryViewModel> GetAllCategories(string cultureCode, string includes = null, Expression<Func<Domain.Models.Category, bool>>[] filters = null)
        {
            List<CategoryViewModel> categoryViewModels = new List<CategoryViewModel>();
            IEnumerable<Domain.Models.Category> categories = _categoryRepository.GetCategories(filters, includes);

            foreach(var category in categories)
            {
                var categoryTitleLocSet = category.TitleLocalizationSet;

                if(categoryTitleLocSet != null && categoryTitleLocSet.Localizations != null)
                {
                    var loc = categoryTitleLocSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);
                    
                    if(loc != null)
                    {
                        categoryViewModels.Add(new CategoryViewModel()
                        {
                            CategoryId = category.Id,
                            CategoryTitle = loc.Value
                        });
                    }
                }
            }

            return categoryViewModels;
        }

        public IEnumerable<CategoryWithAllTranslationsViewModel> GetAllCategoriesWithTranslations(string includes = null, Expression<Func<Domain.Models.Category, bool>>[] filters = null)
        {
            List<CategoryWithAllTranslationsViewModel> categoryWithAllTranslationsVMs = new List<CategoryWithAllTranslationsViewModel>();
            IEnumerable<Domain.Models.Category> categories = _categoryRepository.GetCategories(filters, includes);

            foreach (var category in categories)
            {
                var categoryTitleLocSet = category.TitleLocalizationSet;

                if (categoryTitleLocSet != null && categoryTitleLocSet.Localizations != null)
                {
                    CategoryWithAllTranslationsViewModel categoryWithAllTranslationsVM = new CategoryWithAllTranslationsViewModel()
                    {
                        CategoryId = category.Id,
                        LocalizationViewModels = new List<LocalizationViewModel>()
                    };

                    foreach(var categoryTitleLoc in categoryTitleLocSet.Localizations)
                    {
                        LocalizationViewModel localizationVM = new LocalizationViewModel()
                        {
                            CultureCode = categoryTitleLoc.CultureCode,
                            LocalizationSetId = categoryTitleLocSet.Id,
                            Value = categoryTitleLocSet != null ? categoryTitleLoc.Value : ""
                        };
                        categoryWithAllTranslationsVM.LocalizationViewModels.Add(localizationVM);
                    }

                    categoryWithAllTranslationsVMs.Add(categoryWithAllTranslationsVM);
                }
            }

            return categoryWithAllTranslationsVMs;
        }

        public CategoryViewModel GetCategory(int id, string cultureCode, string includes = null)
        {
            Domain.Models.Category category = _categoryRepository.GetCategory(id, includes);

            if(category != null)
            {
                var categoryTitleLocSet = category.TitleLocalizationSet;

                if (categoryTitleLocSet != null && categoryTitleLocSet.Localizations != null)
                {
                    var categoryTitleLoc = categoryTitleLocSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    if (categoryTitleLoc != null)
                    {
                        CategoryViewModel categoryViewModel = new CategoryViewModel()
                        {
                            CategoryId = category.Id,
                            CategoryTitle = categoryTitleLoc.Value
                        };

                        return categoryViewModel;
                    }
                }
            }

            return null;
        }

        public CategoryWithAllTranslationsViewModel GetCategoryWithAllTranslations(int id, string includes = null)
        {
            Domain.Models.Category category = _categoryRepository.GetCategory(id, includes);

            if (category != null)
            {
                var categoryTitleLocSet = category.TitleLocalizationSet;

                if (categoryTitleLocSet != null && categoryTitleLocSet.Localizations != null)
                {
                    CategoryWithAllTranslationsViewModel categoryWithAllTranslationsVM = new CategoryWithAllTranslationsViewModel()
                    {
                        CategoryId = id,
                        LocalizationViewModels = new List<LocalizationViewModel>()
                    };

                    foreach(var categoryTitleLoc in categoryTitleLocSet.Localizations)
                    {
                        LocalizationViewModel localizationVM = new LocalizationViewModel()
                        {
                            LocalizationSetId = categoryTitleLocSet.Id,
                            CultureCode = categoryTitleLoc.CultureCode,
                            Value = categoryTitleLoc != null ? categoryTitleLoc.Value : ""
                        };

                        categoryWithAllTranslationsVM.LocalizationViewModels.Add(localizationVM);
                    }
                }
            }

            return null;
        }

        public void Save()
        {
            _categoryRepository.Save();
        }

        public void UpdateCategory(EditCategoryViewModel editCategoryViewModel)
        {
            throw new NotImplementedException();
        } 
    }
}
