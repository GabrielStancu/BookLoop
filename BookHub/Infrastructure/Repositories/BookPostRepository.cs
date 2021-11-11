using AutoMapper;
using Core.Helpers;
using Core.Models;
using Infrastructure.Contexts;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookPostRepository : GenericRepository<BookPost>, IBookPostRepository
    {
        private readonly IBookPostComparer _bookPostComparer;
        private readonly IMapper _mapper;

        public BookPostRepository(BookContext context, IBookPostComparer bookPostComparer, IMapper mapper) : base(context)
        {
            _bookPostComparer = bookPostComparer;
            _mapper = mapper;
        }

        public async Task<BooksSearchResultDTO> GetPostsWithSpecificationAsync(Specification specification)
        {
            var bookPosts = await Context.BookPost
                .Where(bp => bp.Author.ToUpper().Contains(specification.Author.ToUpper()) &&
                             bp.Title.ToUpper().Contains(specification.Title.ToUpper()) &&
                             bp.Location.ToUpper().Contains(specification.Location.ToUpper()) &&
                             bp.Genre.ToUpper().Contains(specification.Genre.ToUpper()) &&
                             ((bp.OfferType == specification.OfferType && specification.OfferType != OfferType.AnyOffer) 
                                || (specification.OfferType == OfferType.AnyOffer)) &&
                             bp.Price >= specification.MinimumPrice &&
                             bp.Price <= specification.MaximumPrice &&
                             bp.PostOwnerId != specification.UserId)
                .Include(bp => bp.PostOwner)
                .ToListAsync();

            _bookPostComparer.SetCriteria(specification.PriceSorting, specification.TitleSorting);
            bookPosts.Sort(_bookPostComparer);

            var crtBooksPage = new List<BookPost>();
            crtBooksPage = bookPosts
                .Skip((specification.PageNumber - 1) * Specification.PageSize)
                .Take(Specification.PageSize)
                .ToList();

            var crtMappedPage = new List<BookPostDTO>();
            crtBooksPage.ForEach(bp => crtMappedPage.Add(_mapper.Map<BookPost, BookPostDTO>(bp)));

            var searchResult = new BooksSearchResultDTO()
            {
                BookPosts = crtMappedPage,
                PostsCount = bookPosts.Count
            };

            return searchResult;
        }

        public async Task<bool> DeletePostByIdAsync(int id)
        {
            var bookPost = await SelectByIdAsync(id);
            await DeleteAsync(bookPost);
            return true;
        }
    }
}
