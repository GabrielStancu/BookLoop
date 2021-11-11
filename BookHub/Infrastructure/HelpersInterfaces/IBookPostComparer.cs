using Core.Helpers;
using Core.Models;
using System.Collections.Generic;

namespace Infrastructure.Helpers
{
    public interface IBookPostComparer: IComparer<BookPost>
    {
        PriceSorting PriceSorting { get; set; }
        TitleSorting TitleSorting { get; set; }
        public void SetCriteria(PriceSorting priceSorting, TitleSorting titleSorting);
        new int Compare(BookPost x, BookPost y);
    }
}