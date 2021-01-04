using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceOfKarabakh.Application.ViewModels.Common
{
    public class PaginatedElements<TElement>
    {
        public PaginatedElements(IEnumerable<TElement> elements, int elementCountPerPage, int currentPage, int allElementsCount)
        {
            Elements = elements;
            MaxPage = (allElementsCount / elementCountPerPage) + (allElementsCount % elementCountPerPage != 0 ? 1 : 0);
            CurrentPage = currentPage;
            ElementCountPerPage = elementCountPerPage;
        }

        public IEnumerable<TElement> Elements { get; set; }

        public int CurrentPage { get; set; }

        public int MaxPage { get; set; }

        public int ElementCountPerPage { get; set; }

        public bool NextPageExists { get { return CurrentPage < MaxPage; } }

        public bool PreviousPageExists { get{ return CurrentPage > 1; } }
    }
}
