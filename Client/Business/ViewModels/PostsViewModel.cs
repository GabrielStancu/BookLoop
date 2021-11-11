using Business.Services;
using Business.Shared;
using Data.Models;
using Data.Specification;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Business.ViewModels
{
    public class PostsViewModel: BaseViewModel
    {
        public ObservableCollection<BookPost> BookPosts { get; set; } = new ObservableCollection<BookPost>();

        private BookPost _selectedBookPost;
        public BookPost SelectedBookPost
        {
            get { return _selectedBookPost; }
            set
            {
                _selectedBookPost = value;
                SetProperty<BookPost>(ref _selectedBookPost, value);
                DataContainer.BookPost = value;
            }
        }

        private int _postsCount;
        public int  PostsCount
        {
            get { return _postsCount; }
            set
            {
                _postsCount = value;
                SetProperty<int>(ref _postsCount, value);
            }
        }

        private int _currentPage;
        public int CurrentPage
        {
            get { return _currentPage; }
            set 
            {
                _currentPage = value;
                SetProperty<int>(ref _currentPage, value);
            }
        }

        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }


        private string _paginationInfo;
        public string PaginationInfo
        {
            get { return _paginationInfo; }
            set
            {
                _paginationInfo = value;
                SetProperty<string>(ref _paginationInfo, value);
            }
        }

        private bool _leftNavigationEnabled;
        public bool LeftNavigationEnabled
        {
            get { return _leftNavigationEnabled; }
            set 
            { 
                _leftNavigationEnabled = value;
                SetProperty<bool>(ref _leftNavigationEnabled, value);    
            }
        }

        private bool _rightNavigationEnabled;
        public bool RightNavigationEnabled
        {
            get { return _rightNavigationEnabled; }
            set
            {
                _rightNavigationEnabled = value;
                SetProperty<bool>(ref _rightNavigationEnabled, value);
            }
        }

        private readonly IPostsService _postsService;
        public PostsViewModel(IPostsService postsService)
        {
            _postsService = postsService;
        }

        public void DecrementPage()
        {
            DataContainer.Specification.PageNumber--;
            LoadBookPosts();
        }

        public void IncrementPage()
        {
            DataContainer.Specification.PageNumber++;
            LoadBookPosts();
        }

        public async void LoadBookPosts()
        {
            var specification = DataContainer.Specification;
            
            if(specification.MinimumPrice is null)
            {
                specification.MinimumPrice = Specification.DefaultMinimumPrice;
            }
            if(specification.MaximumPrice is null)
            {
                specification.MaximumPrice = Specification.DefaultMaximumPrice;
            }
  
            var searchResult = await _postsService.GetBooksWithSpecificationAsync(specification);
            PostsCount = searchResult.PostsCount;

            BookPosts.Clear();
            foreach (var bookPost in searchResult.BookPosts)
            {
               BookPosts.Add(bookPost);
            }

            PageSize = Specification.PageSize;
            CurrentPage = specification.PageNumber;
            FormatPaginationInfo();
        }

        private void FormatPaginationInfo()
        {
            int pageStart = (CurrentPage - 1) * PageSize + 1;
            int pageEnd = pageStart + BookPosts.Count - 1;
            int pageOffset = (PostsCount % PageSize == 0) ? 0 : 1;

            if(PostsCount == 0)
            {
                PaginationInfo = $"Showing 0 of 0 posts";
            }
            else
            {
                PaginationInfo = $"Showing {pageStart} - {pageEnd} of {PostsCount} posts";
            }
            
            LeftNavigationEnabled = (CurrentPage > 1);
            RightNavigationEnabled = (CurrentPage < PostsCount / PageSize + pageOffset);
        }
    }
}
