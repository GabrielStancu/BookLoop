using Core.Helpers;

namespace Infrastructure.Helpers
{
    public class Specification
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Location { get; set; }
        public double MinimumPrice { get; set; }
        public double MaximumPrice { get; set; }
        public OfferType OfferType { get; set; }
        public PriceSorting PriceSorting { get; set; }
        public TitleSorting TitleSorting { get; set; }
        public int PageNumber { get; set; }
        public static int PageSize { get; set; } = 3;
        public int UserId { get; set; }
    }
}
