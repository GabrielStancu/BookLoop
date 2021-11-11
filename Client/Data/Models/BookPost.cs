using Data.Specification;
using System;

namespace Data.Models
{
    public class BookPost: BaseModel
    {
        public OfferType OfferType { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        public int PostOwnerId { get; set; }
        public BookUser PostOwner { get; set; }
        public string PhotoFileName { get; set; }
        public string Description { get; set; }
        public DateTime TimeOfPosting { get; set; }
        public string PriceTag
        {
            get
            {
                if (OfferType == OfferType.Sell)
                {
                    return $"{Math.Round(Price, 2)} RON";
                }
                else
                {
                    return OfferType.ToString();
                }
            }
        }
    }
}
