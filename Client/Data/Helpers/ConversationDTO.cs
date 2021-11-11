using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data.Helpers
{
    public class ConversationDTO: INotifyPropertyChanged
    {
        public int Id { get; set; }
        public ConversationType ConversationType { get; set; }
        public string ConversationPhoto { get; set; }
        public string ConversationName { get; set; }
        private string _conversationLastMessage;
        public string ConversationLastMessage 
        {
            get { return _conversationLastMessage; } 
            set
            {
                _conversationLastMessage = value;
                SetProperty<string>(ref _conversationLastMessage, value);
            }
        } 
        public DateTime LastMessageSendTime { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;

            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
