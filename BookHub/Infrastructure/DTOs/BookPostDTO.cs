using Core.Helpers;
using System;

namespace Infrastructure.DTOs
{
    public class BookPostDTO
    {
        public int Id { get; set; }
        public OfferType OfferType { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        public BookUserDTO PostOwner { get; set; }
        public string PhotoFileName { get; set; }
        public string Description { get; set; }
        public DateTime TimeOfPosting { get; set; }
    }
}
