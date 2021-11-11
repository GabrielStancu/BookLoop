using Infrastructure.DTOs;
using System.Collections.Generic;

namespace Infrastructure.Helpers
{
    public interface IConversationComparer: IComparer<ConversationDTO>
    {
        new int Compare(ConversationDTO x, ConversationDTO y);
    }
}