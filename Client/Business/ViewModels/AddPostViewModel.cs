using Business.Helpers;
using Business.Services;
using Business.Shared;
using Data.Models;
using Data.Specification;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Business.ViewModels
{
    public class AddPostViewModel: BaseViewModel
    {
        public ObservableCollection<OfferType> OfferTypes { get; set; }
        public OfferType OfferType { get; set; } = OfferType.Sell;
        public string Title { get; set; } 
        public string Author { get; set; }
        public string Genre { get; set; } 
        public string Location { get; set; }
        public double? Price { get; set; } = null;
        public int PostOwnerId { get; set; } 
        public Stream PhotoStream { get; set; } 
        public string PhotoFileName { get; set; }
        public string Description { get; set; }
        private bool _enabledPrice = true;
        public bool EnabledPrice
        {
            get { return _enabledPrice; }
            set
            {
                _enabledPrice = value;
                SetProperty<bool>(ref _enabledPrice, value);
            }
        }

        private ImageSource _bookPhoto = ImageSource.FromFile("no_image.jpg");
        public ImageSource BookPhoto 
        {
            get { return _bookPhoto; }
            set
            {
                _bookPhoto = value;
                SetProperty<ImageSource>(ref _bookPhoto, value);
            }
        }

        private readonly IPostsService _postsService;
        private readonly IPickPhotoService _pickPhotoService;
        private readonly int _minLength = 5;

        public AddPostViewModel(IPostsService postsService, IPickPhotoService pickPhotoService)
        {
            InitOfferTypes();
            _postsService = postsService;
            _pickPhotoService = pickPhotoService;
            PostOwnerId = DataContainer.GetUserId();
        }

        public async Task<bool> MakePostAsync()
        {
            if (!ValidFields())
            {
                return false;
            }

            var bookPost = new BookPost()
            {
                Author = Author,
                Genre = Genre,
                Location = Location,
                OfferType = OfferType,
                PostOwnerId = PostOwnerId,
                PhotoFileName = PhotoFileName,
                Price = (OfferType == OfferType.Sell) ? (Price ?? 0): 0,
                Title = Title,
                Description = Description,
                TimeOfPosting = DateTime.UtcNow
            };

            await _postsService.UploadPhotoAsync(PhotoStream, PhotoFileName);
            return await _postsService.MakePostAsync(bookPost);
        }

        public async Task PickBookPhoto()
        {
            (ImageSource bookPhoto, Stream photoStream, string photoFileName) = await _pickPhotoService.PickPhotoAsync();
            if(bookPhoto != null)
            {
                (BookPhoto, PhotoStream, PhotoFileName) = (bookPhoto, photoStream, photoFileName);
            }  
        }

        public void EnablePrice()
        {
            EnabledPrice = (OfferType == OfferType.Sell);
        }

        private bool ValidFields()
        {
            if(Title.Length < _minLength ||
               Author.Length < _minLength ||
               Genre.Length.Equals(string.Empty) ||
               Location.Length < _minLength)
            {
                return false;
            }

            return true;
        }

        private void InitOfferTypes()
        {
            OfferTypes = new OptionsLoader<OfferType>().LoadOptions();
            OfferTypes.Remove(OfferType.AnyOffer);
        }
    }
}
