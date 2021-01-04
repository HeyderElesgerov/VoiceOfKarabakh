using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VoiceOfKarabakh.Domain.Interfaces.Category
{
    public interface ICategoryRepository
    {
        IEnumerable<Models.Category> GetCategories(
            Expression<Func<Models.Category, bool>>[] filters = null, string includes = null);

        Models.Category GetCategory(int id, string includes = null);

        void Add(Models.Category category);

        void Delete(int id);

        void Save();
    }
}
