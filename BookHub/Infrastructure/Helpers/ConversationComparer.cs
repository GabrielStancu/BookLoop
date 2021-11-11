using Infrastructure.DTOs;
using System.Collections.Generic;

namespace Infrastructure.Helpers
{
    public class ConversationComparer : Comparer<ConversationDTO>, IConversationComparer
    {
        public override int Compare(ConversationDTO x, ConversationDTO y)
        {
            //sort them descending
            if (x.LastMessageSendTime < y.LastMessageSendTime)
            {
                return 1;
            }

            return -1;
        }
    }
}
