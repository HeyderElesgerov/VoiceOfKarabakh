using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace VoiceOfKarabakh.Domain.Interfaces.Tag
{
    public interface ITagRepository
    {
        IEnumerable<Models.Tag> GetTags(Expression<Func<Models.Tag, bool>>[] filters = null, string includes = null);
        Models.Tag GetTag(int id, string includes = null);
        void Add(Models.Tag newTag);
        void Delete(int id);
        bool Exists(int id);

        void Save();
    }
}
