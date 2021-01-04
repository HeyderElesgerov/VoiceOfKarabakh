using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VoiceOfKarabakh.Application.ViewModels.Tag;

namespace VoiceOfKarabakh.Application.Interfaces.Tag
{
    public interface ITagService
    {
        IEnumerable<TagViewModel> GetAllTags(
            string cultureCode, string includes = null, Expression<Func<Domain.Models.Tag, bool>>[] filters = null);
        IEnumerable<TagWithAllTranslationsViewModel> GetAllTagsWithTranslations(
            string includes = null, Expression<Func<Domain.Models.Tag, bool>>[] filters = null);
        TagViewModel GetTag(int id, string cultureCode, string includes = null);
        TagWithAllTranslationsViewModel GetTagWithAllTranslations(int id, string includes = null);
        void AddNewTag(NewTagViewModel newTagViewModel);
        void DeleteTag(int id);

        void Save();
    }
}
