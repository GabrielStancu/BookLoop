using Core.Models;
using System.Collections;

namespace BookHub.Tests
{
    public class MessageComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if(!(x is Message) || !(y is Message))
            {
                return -1;
            }

            var msg1 = (Message)x;
            var msg2 = (Message)y;

            if (msg1.Id == msg2.Id &&
               msg1.ConversationType == msg2.ConversationType &&
               msg1.ConversationId == msg2.ConversationId &&
               msg1.MessageContent == msg2.MessageContent &&
               msg1.SenderId == msg2.SenderId &&
               msg1.SendTime == msg2.SendTime)
            {
                return 0;
            }

            return (msg1.Id < msg2.Id) ? -1 : 1;
        }
    }
}
