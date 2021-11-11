using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Business.Helpers
{
    public class OptionsLoader<T> : IOptionsLoader<T>
    {
        public ObservableCollection<T> LoadOptions()
        {
            var oc = new ObservableCollection<T>();
            var values = Enum.GetValues(typeof(T)).Cast<T>();

            foreach (var value in values)
            {
                oc.Add(value);
            }

            return oc;
        }
    }
}
