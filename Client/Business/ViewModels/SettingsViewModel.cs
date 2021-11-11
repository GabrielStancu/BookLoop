using Business.Services;
using Business.Shared;
using Data.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Business.ViewModels
{
    public class SettingsViewModel: BaseViewModel
    {
        private BookUser _bookUser;
        public BookUser BookUser
        {
            get { return _bookUser; }
            set
            {
                _bookUser = value;
                SetProperty<BookUser>(ref _bookUser, value);
            }
        }

        private ObservableCollection<BookPost> _bookPosts;
        public ObservableCollection<BookPost> BookPosts
        {
            get { return _bookPosts; }
            set
            {
                _bookPosts = value;
                SetProperty<ObservableCollection<BookPost>>(ref _bookPosts, value);
            }
        }

        private ImageSource _profilePhoto;
        public ImageSource ProfilePhoto
        {
            get { return _profilePhoto; }
            set
            {
                _profilePhoto = value;
                SetProperty<ImageSource>(ref _profilePhoto, value);
            }
        }

        private Stream _photoStream;
        public Stream PhotoStream
        {
            get { return _photoStream; }
            set
            {
                _photoStream = value;
                SetProperty<Stream>(ref _photoStream, value);
            }
        }

        private string _photoFileName;
        public string PhotoFileName
        {
            get { return _photoFileName; }
            set
            {
                _photoFileName = value;
                SetProperty<string>(ref _photoFileName, value);
            }
        }

        private readonly IPickPhotoService _pickPhotoService;
        private readonly IUserService _userService;
        private readonly IPostsService _postsService;

        public SettingsViewModel(IPickPhotoService pickPhotoService, IUserService userService, IPostsService postsService)
        {
            _pickPhotoService = pickPhotoService;
            _userService = userService;
            _postsService = postsService;

            BindModel();
        }

        private async void BindModel()
        {
            await SetBindings(DataContainer.GetUserId());
        }

        private async Task SetBindings(int bookUserId)
        {
            var bookUser = await _userService.SelectUserWithInfoByIdAsync(bookUserId);
            BookUser = bookUser;
            ProfilePhoto = ImageSource.FromUri(new Uri(BookUser.PhotoFileName));
            PhotoFileName = BookUser.PhotoFileName;

            BookPosts = new ObservableCollection<BookPost>();
            BookPosts.Clear();
            foreach (var bookPost in BookUser.BookPosts)
            {
                bookPost.PostOwner = bookUser;
                BookPosts.Add(bookPost);
            }
        }

        public async Task PickProfilePhoto()
        {
            (ImageSource profilePhoto, Stream photoStream, string photoFileName) = await _pickPhotoService.PickPhotoAsync();
            if (profilePhoto != null)
            {
                (ProfilePhoto, PhotoStream, PhotoFileName) = (profilePhoto, photoStream, photoFileName);
            }
        }

        public async Task<bool> SaveProfileChangesAsync()
        {
            if (BookUser.Email.Equals(string.Empty) || !BookUser.Email.Contains("@"))
            {
                return false; 
            }

            if (!BookUser.PhotoFileName.Equals(PhotoFileName))
            {
                await _userService.UploadPhotoAsync(PhotoStream, PhotoFileName);
                BookUser.PhotoFileName = PhotoFileName;
            }

            var updated = await _userService.UpdateProfileChangesAsync(BookUser);
            return updated;
        }

        public async Task<bool> DeletePostAsync(BookPost bookPost)
        {
            await _postsService.DeletePostAsync(bookPost);
            BookPosts.Remove(bookPost);

            return true;
        }
    }
}
