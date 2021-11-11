using System.Collections.ObjectModel;

namespace Business.Helpers
{
    public interface IOptionsLoader<T>
    {
        ObservableCollection<T> LoadOptions();
    }
}