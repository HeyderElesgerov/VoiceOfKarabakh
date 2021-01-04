using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VoiceOfKarabakh.Application.Interfaces.Tag;
using VoiceOfKarabakh.Application.ViewModels.Localization;
using VoiceOfKarabakh.Application.ViewModels.Tag;
using VoiceOfKarabakh.Domain.Factory.Localization;
using VoiceOfKarabakh.Domain.Factory.LocalizationSet;
using VoiceOfKarabakh.Domain.Factory.Tag;
using VoiceOfKarabakh.Domain.Interfaces.Tag;
using VoiceOfKarabakh.Domain.Models;

namespace VoiceOfKarabakh.Application.Services.Tag
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        private TagFactory _tagFactory;
        private LocalizationSetFactory _localizationSetFactory;
        private LocalizationFactory _localizationFactory;

        public TagService(ITagRepository tagRepository, TagFactory tagFactory, LocalizationFactory localizationFactory, LocalizationSetFactory localizationSetFactory)
        {
            _tagRepository = tagRepository;
            _tagFactory = tagFactory;
            _localizationFactory = localizationFactory;
            _localizationSetFactory = localizationSetFactory;
        }

        public void AddNewTag(NewTagViewModel newTagViewModel)
        {
            Domain.Models.LocalizationSet localizationSet = _localizationSetFactory.GetLocalizationSet();
            foreach (var translation in newTagViewModel.TagTitleTranslations)
            {
                if (translation.Value == null)
                    translation.Value = "";

                var localization = _localizationFactory.GetLocalization(translation.CultureCode, translation.Value, localizationSet);
                localizationSet.Localizations.Add(localization);
            }

            var newTag = _tagFactory.GetTag(localizationSet);
            _tagRepository.Add(newTag);
        }

        public void DeleteTag(int id)
        {
            _tagRepository.Delete(id);
        }

        public IEnumerable<TagViewModel> GetAllTags(string cultureCode, string includes = null, Expression<Func<Domain.Models.Tag, bool>>[] filters = null)
        {
            List<TagViewModel> tagViewModels = new List<TagViewModel>();
            IEnumerable<Domain.Models.Tag> tags = _tagRepository.GetTags(filters, includes);

            foreach (var tag in tags)
            {
                var tagTitleLocSet = tag.TitleLocalizationSet;

                if (tagTitleLocSet != null && tagTitleLocSet.Localizations != null)
                {
                    var loc = tagTitleLocSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    if (loc != null)
                    {
                        tagViewModels.Add(new TagViewModel()
                        {
                            TagId = tag.Id,
                            TagTitle = loc.Value
                        });
                    }
                }
            }

            return tagViewModels;
        }

        public IEnumerable<TagWithAllTranslationsViewModel> GetAllTagsWithTranslations(string includes = null, Expression<Func<Domain.Models.Tag, bool>>[] filters = null)
        {
            List<TagWithAllTranslationsViewModel> tagWithAllTranslationsVMs = new List<TagWithAllTranslationsViewModel>();

            foreach (var tag in _tagRepository.GetTags(filters, includes))
            {
                TagWithAllTranslationsViewModel tagWithAllTranslationsVM = new TagWithAllTranslationsViewModel();
                tagWithAllTranslationsVM.LocalizationViewModels = new List<LocalizationViewModel>();
                tagWithAllTranslationsVM.TagId = tag.Id;

                var titleLocSet = tag.TitleLocalizationSet;
                if (titleLocSet != null && titleLocSet.Localizations != null)
                {
                    foreach (var loc in titleLocSet.Localizations)
                    {
                        var localizationVM = new LocalizationViewModel()
                        {
                            CultureCode = loc.CultureCode,
                            LocalizationSetId = titleLocSet.Id,
                            Value = loc != null ? loc.Value : ""
                        };
                        tagWithAllTranslationsVM.LocalizationViewModels.Add(localizationVM);
                    }

                    tagWithAllTranslationsVMs.Add(tagWithAllTranslationsVM);
                }
            }

            return tagWithAllTranslationsVMs;
        }

        public TagViewModel GetTag(int id, string cultureCode, string includes = null)
        {
            var tag = _tagRepository.GetTag(id, includes);

            if (tag != null)
            {
                var tagTitleLocSet = tag.TitleLocalizationSet;

                if (tagTitleLocSet != null && tagTitleLocSet.Localizations != null)
                {
                    var tagTitleLoc = tagTitleLocSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    if (tagTitleLoc != null)
                    {
                        var tagVM = new TagViewModel()
                        {
                            TagId = id,
                            TagTitle = tagTitleLoc.Value
                        };

                        return tagVM;
                    }
                }
            }

            return null;
        }

        public TagWithAllTranslationsViewModel GetTagWithAllTranslations(int id, string includes = null)
        {
            Domain.Models.Tag tag = _tagRepository.GetTag(id, includes);

            if (tag != null)
            {
                var tagTitleLocSet = tag.TitleLocalizationSet;

                if (tagTitleLocSet != null && tagTitleLocSet.Localizations != null)
                {
                    TagWithAllTranslationsViewModel tagWithAllTranslationsVM = new TagWithAllTranslationsViewModel()
                    {
                        TagId = id,
                        LocalizationViewModels = new List<LocalizationViewModel>()
                    };

                    foreach (var localization in tagTitleLocSet.Localizations)
                    {
                        LocalizationViewModel localizationVM = new LocalizationViewModel()
                        {
                            LocalizationSetId = tagTitleLocSet.Id,
                            CultureCode = localization.CultureCode,
                            Value = localization != null ? localization.Value : ""
                        };

                        tagWithAllTranslationsVM.LocalizationViewModels.Add(localizationVM);
                    }
                }
            }

            return null;
        }

        public void Save()
        {
            _tagRepository.Save();
        }
    }
}
