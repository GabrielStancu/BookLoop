using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Business.Services
{
    public class PickPhotoService : IPickPhotoService
    {
        public async Task<(ImageSource ImageSource, Stream Stream, string fileName)> PickPhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            //if we want to allow the user to take photos on the spot,
            //we will use StoreCameraMediaOptions inseatd of PickMediaOptions
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            //if we want to take a photo, we will use TakePhotoAsync instead of PickPhotoAsync 
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if(selectedImageFile is null)
            {
                return (null, null, null);
            }

            var fileName = selectedImageFile.Path.Substring(selectedImageFile.Path.LastIndexOf('/') + 1);

            return (ImageSource.FromStream(() => selectedImageFile.GetStream()), selectedImageFile.GetStream(), fileName);
        }
    }
}
