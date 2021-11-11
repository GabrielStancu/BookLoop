namespace Data.Specification
{
    public class Specification
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public double? MinimumPrice { get; set; } = null;
        public static double DefaultMinimumPrice { get; set; } = 0;
        public double? MaximumPrice { get; set; } = null;
        public static double DefaultMaximumPrice { get; set; } = 1000;
        public OfferType OfferType { get; set; } = OfferType.AnyOffer;
        public PriceSorting PriceSorting { get; set; } = PriceSorting.NoOrder;
        public TitleSorting TitleSorting { get; set; } = TitleSorting.NoOrder;
        public int PageNumber { get; set; } = 1;
        public static int PageSize { get; set; } = 3;
        public int UserId { get; set; }
    }
}
