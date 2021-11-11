using Core.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class BookPost: BaseModel
    {
        public OfferType OfferType { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        [ForeignKey("PostOwner")]
        public int PostOwnerId { get; set; }
        public BookUser PostOwner { get; set; }
        public string PhotoFileName { get; set; }
        public string Description { get; set; }
        public DateTime TimeOfPosting { get; set; }
    }
}
