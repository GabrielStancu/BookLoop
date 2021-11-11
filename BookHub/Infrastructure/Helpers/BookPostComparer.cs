using Core.Helpers;
using Core.Models;
using System.Collections.Generic;

namespace Infrastructure.Helpers
{
    public class BookPostComparer : Comparer<BookPost>, IBookPostComparer
    {
        public PriceSorting PriceSorting { get; set; }
        public TitleSorting TitleSorting { get; set; }
        public void SetCriteria(PriceSorting priceSorting, TitleSorting titleSorting)
        {
            PriceSorting = priceSorting;
            TitleSorting = titleSorting;
        }
        public override int Compare(BookPost x, BookPost y)
        {
            if (PriceSorting != PriceSorting.NoOrder)
            {
                return CompareByPrice(x, y);
            }
            else if (TitleSorting != TitleSorting.NoOrder)
            {
                return CompareByTitle(x, y);
            }
            else
            {
                return CompareByPostTime(x, y);
            }
        }

        private int CompareByPrice(BookPost x, BookPost y)
        {
            var priceOrder = x.Price.CompareTo(y.Price);

            if (priceOrder != 0)
            {
                if (PriceSorting == PriceSorting.Descending)
                {
                    priceOrder *= -1;
                }

                return priceOrder;
            }

            return CompareByPostTime(x, y);
        }

        private int CompareByTitle(BookPost x, BookPost y)
        {
            var titleOrder = x.Title.CompareTo(y.Title);

            if (titleOrder != 0)
            {
                if (TitleSorting == TitleSorting.Descending)
                {
                    titleOrder *= -1;
                }

                return titleOrder;
            }

            return CompareByPostTime(x, y);
        }

        //descending always
        private int CompareByPostTime(BookPost x, BookPost y)
        {
            return -1 * x.TimeOfPosting.CompareTo(y.TimeOfPosting);
        }
    }
}
