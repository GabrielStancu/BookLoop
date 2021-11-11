using Business.Services;
using Data.Helpers;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Business.ViewModels
{
    public class SignupViewModel: BaseViewModel
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set 
            {
                _username = value;
                SetProperty<string>(ref _username, value);
            }
        }

        private string _firstname;
        public string FirstName
        {
            get { return _firstname; }
            set 
            {
                _firstname = value;
                SetProperty<string>(ref _firstname, value);
            }
        }

        private string _lastname;
        public string LastName
        {
            get { return _lastname; }
            set
            {
                _lastname = value;
                SetProperty<string>(ref _lastname, value);
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                SetProperty<string>(ref _email, value);
            }
        }

        private ImageSource _profilePhoto = ImageSource.FromFile("no_image.jpg");
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

        private readonly ISignupService _signupService;
        private readonly IPickPhotoService _pickPhotoService;

        public SignupViewModel(ISignupService signupService, IPickPhotoService pickPhotoService)
        {
            _signupService = signupService;
            _pickPhotoService = pickPhotoService;
        }

        public async Task PickProfilePhoto()
        {
            (ImageSource profilePhoto, Stream photoStream, string photoFileName) = await _pickPhotoService.PickPhotoAsync();
            if (profilePhoto != null)
            {
                (ProfilePhoto, PhotoStream, PhotoFileName) = (profilePhoto, photoStream, photoFileName);
            }
        }

        public async Task<SignupResult> SignupAsync(string password, string repeatedPassword)
        {
            var signupUser = new SignupUserDTO()
            {
                Username = Username,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = password,
                RepeatedPassword = repeatedPassword,
                PhotoFileName = PhotoFileName
            };

            await _signupService.UploadPhotoAsync(PhotoStream, PhotoFileName);
            return await _signupService.SignupAsync(signupUser);
        }
    }
}
