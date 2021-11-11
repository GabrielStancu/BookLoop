using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Business.Services
{
    public interface IPickPhotoService
    {
        Task<(ImageSource ImageSource, Stream Stream, string fileName)> PickPhotoAsync();
    }
}